using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using Uint8 = System.Byte;
using Uint16 = System.UInt16;
using Uint32 = System.UInt32;
using Uint64 = System.UInt64;

namespace ArduinoRemote
{
    public class ArduinoSocket
    {
        public const int DEFAULT_TIMEOUT = 1000;
        public const int REPEAT_TIMEOUT = DEFAULT_TIMEOUT;
        public const int DEFAULT_MAX_TIMEOUTS = 4;
        public const int DEFAULT_MAX_RETRANSMISSIONS = 1;

        public class CancelException : Exception
        {
            public CancelException() : base()
            { }
        }

        public ArduinoSocket(string ip, Uint16 port, Stream output)
        {
            address = new IPEndPoint(IPAddress.Parse(ip), port);
            sendBuffer = new Uint8[common.MESSAGE_SIZE];
            outputStream = output;
            stopMutex = new Mutex();
            stopRequest = false;
            keyValid = false;

            key = mt64.genrand64_int64();
            udp = new UdpClient();
            udp.Connect(address.Address, address.Port);
        }
        ~ArduinoSocket()
        {
            udp.Close();
        }
        public bool exchangeKey()
        {
            output(String.Format("Sending key request to {0}...", getAddress()));

            Uint64 r = mt64.genrand64_int64();
            Uint64 A = calc.powm(common.g, r, common.p) % common.p;
            Uint64 B = 0;
            int timeout = DEFAULT_TIMEOUT + (int)common.WAIT_INTERVAL;
            bool result = sendAndReceive(common.EXCHANGE_KEY, A, ref B, timeout, DEFAULT_MAX_TIMEOUTS, DEFAULT_MAX_RETRANSMISSIONS);
            //output(B.ToString());
            if (result)
            {
                key = calc.powm(B, r, common.p) % common.p;
                output(String.Format("key = {0:x16}", key));
            }
            keyValid = true;
            return result;
        }
        public bool exchangeMessage(Uint64 message, ref Uint64 reply)
        {
            return exchangeMessage(message, ref reply, DEFAULT_TIMEOUT, DEFAULT_MAX_TIMEOUTS, DEFAULT_MAX_RETRANSMISSIONS);
        }
        public bool exchangeMessage(Uint64 message, ref Uint64 reply, int timeout, int maxTimeouts, int maxRetransmissions)
        {
            if (!keyValid) exchangeKey();
            bool result = sendAndReceive(common.MESSAGE, message ^ key, ref reply, timeout, maxTimeouts, maxRetransmissions);
            if (result) reply = reply ^ key;
            keyValid = false;
            return result;
        }
        public string getAddress()
        {
            return address.ToString();
        }
        public void stop()
        {
            stopMutex.WaitOne();
            stopRequest = true;
            stopMutex.ReleaseMutex();
        }
        public void clearStop()
        {
            stopMutex.WaitOne();
            stopRequest = false;
            stopMutex.ReleaseMutex();
        }
        public bool hasKey()
        {
            return keyValid;
        }

        private void checkStopRequest()
        {
            stopMutex.WaitOne();
            bool stopRequested = stopRequest;
            stopRequest = false;
            stopMutex.ReleaseMutex();
            if (stopRequested) throw (new CancelException());
        }
        private void output(String msg)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(msg);
            outputStream.Write(bytes, 0, bytes.Length);
        }
        private Uint8 getSequence()
        {
            if (sequence == 255u) sequence = 0;
            else ++sequence;
            return sequence;
        }
        private bool waitForPacket(int timeout)
        {
            if (timeout < 0) return false;
            var start = DateTime.Now;
            while (udp.Available == 0)
            {
                if ((DateTime.Now - start).TotalMilliseconds > timeout) return false;
                checkStopRequest();
                Thread.Sleep(1);
            }
            return true;
        }
        private bool sendAndReceive(Uint8 command, Uint64 dataIn, ref Uint64 dataOut, int timeout, int maxTimeouts, int maxRetransmissions)
        {
            sendBuffer[0] = command;
            sendBuffer[1] = getSequence();
            byte[] bytesIn = BitConverter.GetBytes(dataIn);
            for (int i = 0; i < Marshal.SizeOf(dataIn); ++i)
                sendBuffer[i + 2] = bytesIn[i];
            udp.Send(sendBuffer, (int)common.MESSAGE_SIZE);
            var transmitTime = DateTime.Now;
            int timeoutCount = 0;
            int waitTime = timeout;
            while (true)
            {
                checkStopRequest();
                int result = 0;
                int msRemaining = waitTime - (int)(DateTime.Now - transmitTime).TotalMilliseconds; // milliseconds until timeout
                if (waitForPacket(msRemaining))
                {
                    byte[] receiveBuffer = udp.Receive(ref address);
                    checkStopRequest();
                    Uint8 seqIn = receiveBuffer[1];
                    if (seqIn == sequence)
                    {
                        if (receiveBuffer[0] == common.WAIT)
                        {
                            output("Please Wait.");
                            transmitTime = DateTime.Now;
                        }
                        else
                        {
                            byte[] bytesOut = BitConverter.GetBytes(dataOut);
                            for (int i = 0; i < Marshal.SizeOf(dataIn); ++i)
                                bytesOut[i] = receiveBuffer[i + 2];
                            dataOut = BitConverter.ToUInt64(bytesOut, 0);
                            checkStopRequest();
                            return true;
                        }
                    }
                }
                else
                {
                    output("Timeout. ");
                    if (timeoutCount < maxTimeouts)
                    {
                        // timeout.  Request retransmission.
                        output("Requesting retransmission...");
                        sendBuffer[0] = common.REPEAT;
                        udp.Send(sendBuffer, (int)common.MESSAGE_SIZE);
                        checkStopRequest();
                        ++timeoutCount;
                        transmitTime = DateTime.Now;
                        waitTime = REPEAT_TIMEOUT;
                    }
                    else if (maxRetransmissions != 0)
                    {
                        output("Retransmitting packet...");
                        return sendAndReceive(command, dataIn, ref dataOut, timeout, maxTimeouts, maxRetransmissions - 1);
                    }
                    else
                    {
                        output("Message aborted.");
                        return false;
                    }
                }
            }
        }
        private UdpClient udp;
        private IPEndPoint address;
        private Uint64 key;
        private Uint8 sequence;
        private Stream outputStream;
        private Uint8[] sendBuffer;
        private Mutex stopMutex;
        private bool stopRequest;
        private bool keyValid;

    }
}

