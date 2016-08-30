using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace ArduinoRemote
{
    class TextBoxStream : Stream
    {
        public TextBoxStream(TextBox outputBox) : base()
        {
            box = outputBox;
            mutex = new Mutex();
        }
        private TextBox box;
        private Mutex mutex;
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override long Length
        {
            get
            {
                return box.TextLength;
            }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            String msg = Encoding.Unicode.GetString(buffer, offset, count);
            mutex.WaitOne();
            box.DeselectAll();
            box.AppendText(msg);
            box.AppendText("\n");
            mutex.ReleaseMutex();
        }
    }
}
