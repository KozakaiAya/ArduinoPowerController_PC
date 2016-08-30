using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft;
using Microsoft.Win32;
using System.Media;
using Uint8 = System.Byte;
using Uint16 = System.UInt16;
using Uint32 = System.UInt32;
using Uint64 = System.UInt64;

namespace ArduinoRemote
{
    public partial class MainForm : Form
    {
        private TextBox[] octets;
        private RegistryKey remoteStartupKey;
        private TextBoxStream outputStream;
        private ArduinoSocket sock;
        enum StatusEnum { Status, Key, Power, Shutdown, PowerOff, Restart };
        const int checkStatusTimeout = 6000;

        public MainForm()
        {
            InitializeComponent();
            octets = new TextBox[4];
            octets[0] = octet1;
            octets[1] = octet2;
            octets[2] = octet3;
            octets[3] = octet4;
            outputStream = new TextBoxStream(outputBox);
            setNetworkControlEnable(true);
            RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            if (softwareKey == null) throw (new Exception("Failed to open registry key HKEY_CURRENT_USER\\Software"));
            remoteStartupKey = softwareKey.OpenSubKey("Remote_Startup", true);
            if (remoteStartupKey == null) remoteStartupKey = softwareKey.CreateSubKey("Remote_Startup");
            if (remoteStartupKey == null) throw (new Exception("Failed to create registry key HKEY_CURRENT_USER\\Software\\Remote_Startup"));
            readAddressFromRegistry();
        }

        void output(string msg)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(msg);
            outputStream.Write(bytes, 0, bytes.Length);
        }

        private static void errorMessage(string msg, bool warn)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, warn ? MessageBoxIcon.Warning : MessageBoxIcon.Error);
        }

        private void setNetworkControlEnable(bool commState, bool addrState)
        {
            keyButton.Enabled = commState && sock != null;
            statusButton.Enabled = commState && sock != null;
            powerOnButton.Enabled = commState && sock != null;
            powerOffButton.Enabled = commState && sock != null;
            restartButton.Enabled = commState && sock != null;
            shutdownButton.Enabled = commState && sock != null;
            cancelButton.Enabled = !commState && sock != null;
            saveButton.Enabled = sock != null;
            address.Enabled = addrState;
        }

        private void setNetworkControlEnable(bool state)
        {
            setNetworkControlEnable(state, state);
        }

        private void showErrorTooltip(string title, string message, Control control)
        {
            errorTooltip.ToolTipTitle = title;
            errorTooltip.Active = false;
            errorTooltip.Show(String.Empty, control);
            errorTooltip.Active = true;
            errorTooltip.Show(message, control);
            SystemSounds.Beep.Play();
        }

        private void hideErrorTooltip(Control control)
        {
            try
            {
                TextBox t = control as TextBox;
                if (t != null) errorTooltip.Hide(t);
            }
            catch (Exception)
            {
            }
        }

        private void hideErrorTooltip()
        {
            hideErrorTooltip(octet1);
            hideErrorTooltip(octet2);
            hideErrorTooltip(octet3);
            hideErrorTooltip(octet4);
        }

        private void defaultMouseClick(object sender, MouseEventArgs e)
        {
            hideErrorTooltip();
        }

        private static int[] getTextBoxRange(TextBox textBox)
        {
            string[] rangeStr = textBox.Tag.ToString().Split(null);
            if (rangeStr.Length != 2) throw (new Exception("Invalid range format"));
            int minVal = Convert.ToInt32(rangeStr[0], 10);
            int maxVal = Convert.ToInt32(rangeStr[1], 10);
            int[] range = { minVal, maxVal };
            return range;
        }

        void readAddressFromRegistry()
        {
            Object ipVal = remoteStartupKey.GetValue("ip");
            bool valid = true;
            if (ipVal == null)
            {
                valid = false;
            }
            else
            {
                int[] ipNum = new int[4];
                string ipVal_dc = ipVal as string;
                string[] ipArray = ipVal_dc.Split('.');
                if (ipArray.Length != 4) valid = false;
                for (int i = 0; i < 4 && valid; ++i)
                {
                    valid = valid && parseInt(ipArray[i], ref ipNum[i]);
                    int[] range = getTextBoxRange(octets[i]);
                    if (ipNum[i] < range[0] || ipNum[i] > range[1]) valid = false;
                }
                if (valid) for (int i = 0; i < 4; ++i) octets[i].Text = Convert.ToString(ipNum[i]);
                else for (int i = 0; i < 4; ++i) octets[i].Clear();
            }
            if (valid)
            {
                object portVal = remoteStartupKey.GetValue("port");
                if (portVal == null)
                {
                    valid = false;
                }
                else
                {
                    string portStr = portVal as string;
                    int portNum = 0;
                    int[] range = getTextBoxRange(port);
                    if (parseInt(portStr, ref portNum) && portNum >= range[0] && portNum <= range[1])
                    {
                        port.Text = Convert.ToString(portNum);
                    }
                    else
                    {
                        port.Clear();
                        valid = false;
                    }
                }
            }
            if (valid) sock = new ArduinoSocket(formatIP(), (ushort)Convert.ToInt16(port.Text), outputStream);
            else sock = null;
        }

        string formatIP()
        {
            string dot = ".";
            return octet1.Text + dot + octet2.Text + dot + octet3.Text + dot + octet4.Text;
        }

        private static bool parseInt(String s, ref int result)
        {
            try
            {
                result = Int32.Parse(s);
                return true;
            }
            catch (Exception)
            {
                result = 0;
                return false;
            }
        }

        private void addressBoxPaste(TextBox textBox)
        {
            bool invalid = false;
            string clipboardText = Clipboard.GetText();
            string[] ipPort = clipboardText.Split(':');
            string[] ip = ipPort[0].Split('.');
            if (ip.Length == 1 && ipPort.Length == 1)
            {
                int ipNum = 0;
                if (parseInt(ip[0], ref ipNum))
                {
                    int[] range = getTextBoxRange(textBox);
                    if (ipNum < range[0] || ipNum > range[1])
                    {
                        if (ipNum < range[0]) textBox.Text = Convert.ToString(range[0]);
                        else textBox.Text = Convert.ToString(range[1]);
                        errorMessage(String.Format("{0} is not a valid value.  Please specify a value between {1} and {2}", ipNum, range[0], range[1]), true);
                    }
                    else
                    {
                        textBox.Text = Convert.ToString(ipNum);
                    }
                }
                else
                {
                    invalid = true;
                }
            }
            else if (ip.Length == 4 && ipPort.Length >= 1 && ipPort.Length <= 2)
            {
                int[] ipNum = new int[4];
                int portNum = 0;
                for (int i = 0; i < 4 && !invalid; ++i)
                {
                    int[] range = getTextBoxRange(octets[i]);
                    if (!parseInt(ip[i], ref ipNum[i]) || ipNum[i] < range[0] || ipNum[i] > range[1]) invalid = true;
                }
                if (!invalid && ipPort.Length == 2)
                {
                    int[] range = getTextBoxRange(port);
                    if (!parseInt(ipPort[1], ref portNum) || portNum < range[0] || portNum > range[1]) invalid = true;
                }
                if (!invalid)
                {
                    for (int i = 0; i < 4; ++i) octets[i].Text = Convert.ToString(ipNum[i]);
                    if (ipPort.Length == 2) port.Text = Convert.ToString(portNum);
                }
            }
            else
            {
                invalid = true;
            }
            if (invalid)
            {
                showErrorTooltip("Malformed IP Address", "You are trying to paste a malformed IP address into this field.", textBox);
            }
        }

        private void addressKeyDown(object sender, KeyEventArgs e)
        {
            hideErrorTooltip();
            TextBox textBox = sender as TextBox;
            bool cancelKey = false;
            if (e.KeyCode == Keys.V && (e.Modifiers & Keys.Control) != Keys.None)
            {
                addressBoxPaste(textBox);
                cancelKey = true;
            }
            else if (e.KeyCode == Keys.Space)
            {
                cancelKey = true;
            }
            if (cancelKey)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private static bool isNumberKey(KeyPressEventArgs e)
        {
            return Char.IsDigit(e.KeyChar);
        }

        static bool isNumberKey(KeyEventArgs e)
        {
            return ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
                    (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)) &&
                    (Control.ModifierKeys & Keys.Shift) == Keys.None;
        }

        private static bool isControlKey(KeyEventArgs e)
        {
            return e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey;
        }

        private static bool isKeyCommand(KeyPressEventArgs e)
        {
            char key = Char.ToLower(e.KeyChar);
            return (key == 'a' || key == 'b' || key == 'c' || key == 'd' || key == 'x' || key == 'z') && (Control.ModifierKeys & Keys.Control) != Keys.None;
        }

        private static bool isKeyCommand(KeyEventArgs e)
        {
            return (e.KeyCode == Keys.A || e.KeyCode == Keys.C ||
                    e.KeyCode == Keys.C || e.KeyCode == Keys.X ||
                    e.KeyCode == Keys.Z) &&
                    (Control.ModifierKeys & Keys.Control) != Keys.None;
        }

        private void addressKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if ((e.KeyChar == '.' && textBox != octet4 && textBox != port) || (e.KeyChar == ':' && textBox == octet4))
            {
                shiftFocus(sender, e);
                e.Handled = true;
            }
            else if (!isNumberKey(e) && !isKeyCommand(e) && !Char.IsControl(e.KeyChar))
            {
                SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }

        private void shiftFocus(Object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            TextBox next = (GetNextControl(textBox, true)) as TextBox;
            next.Focus();
            next.DeselectAll();
        }

        private void addressValidating(object sender, CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length != 0)
            {
                int[] range = getTextBoxRange(textBox);
                TextBox textBox1 = sender as TextBox;
                int currentVal = 0;
                if (!parseInt(textBox1.Text, ref currentVal))
                {
                    bool cancel = e.Cancel;
                    return;
                }
                bool outOfRange = false;
                if (currentVal < range[0])
                {
                    outOfRange = true;
                    textBox1.Text = Convert.ToString(range[0]);
                }
                else if (currentVal > range[1])
                {
                    outOfRange = true;
                    textBox1.Text = Convert.ToString(range[1]);
                }
                if (outOfRange)
                {
                    errorMessage(String.Format("{0} is not a valid value.  Please specify a value between {1} and {2}", currentVal, range[0], range[1]), true);
                }
            }
        }

        private static bool testAddressBox(TextBox addressBox)
        {
            if (addressBox.TextLength == 0) return false;
            int[] range = getTextBoxRange(addressBox);
            int val = Convert.ToInt32(addressBox.Text);
            return val >= range[0] && val <= range[1];
        }

        private bool checkAddress()
        {
            for (int i = 0; i < 4; ++i)
                if (!testAddressBox(octets[i]))
                    return false;
            if (!testAddressBox(port)) return false;
            return true;
        }
        private void addressChange(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != port && textBox.TextLength == textBox.MaxLength) shiftFocus(sender, e);
            bool valid = checkAddress();
            if (valid)
                sock = new ArduinoSocket(formatIP(), (ushort)Convert.ToInt16(port.Text), outputStream);
            else
                sock = null;
            setNetworkControlEnable(valid, true);
        }

        private void saveAddressToRegistry()
        {
            remoteStartupKey.SetValue("ip", formatIP());
            remoteStartupKey.SetValue("port", port.Text);
        }

        private void saveClick(object sender, EventArgs e)
        {
            saveAddressToRegistry();
        }

        private void revertClick(object sender, EventArgs e)
        {
            readAddressFromRegistry();
        }

        private void keyClick(object sender, EventArgs e)
        {
            if (!networkThread.IsBusy)
            {
                sock.clearStop();
                int action = (int)StatusEnum.Key;
                setNetworkControlEnable(false);
                networkThread.RunWorkerAsync(action);
            }
        }

        private void statusClick(object sender, EventArgs e)
        {
            if (!networkThread.IsBusy)
            {
                sock.clearStop();
                int action = (int)StatusEnum.Status;
                setNetworkControlEnable(false);
                networkThread.RunWorkerAsync(action);
            }
        }

        private void powerClick(object sender, EventArgs e)
        {
            if (!networkThread.IsBusy)
            {
                sock.clearStop();
                int action = (int)StatusEnum.Power;
                setNetworkControlEnable(false);
                networkThread.RunWorkerAsync(action);
            }
        }

        private void shutdownClick(object sender, EventArgs e)
        {
            if (!networkThread.IsBusy)
            {
                sock.clearStop();
                int action = (int)StatusEnum.Shutdown;
                setNetworkControlEnable(false);
                networkThread.RunWorkerAsync(action);
            }
        }

        private void cancelButtonClick(object sender, EventArgs e)
        {
            sock.stop();
        }

        private void exitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool checkStatus(ref Uint64 status)
        {
            output(String.Format("Sending status request to {0}...", sock.getAddress()));
            bool result = sock.exchangeMessage(common.CHECK_STATUS, ref status, checkStatusTimeout,
                ArduinoSocket.DEFAULT_MAX_TIMEOUTS, ArduinoSocket.DEFAULT_MAX_RETRANSMISSIONS);
            if (result)
            {
                switch (status)
                {
                    case common.STATUS_ON:
                        output("Response: Status = On");
                        break;
                    case common.STATUS_OFF:
                        output("Response: Status = Off");
                        break;
                    default:
                        output("Invalid response");
                        break;
                }
            }
            return result;
        }

        private void networkThreadFunc(object sender, DoWorkEventArgs e)
        {
            try
            {
                int action = (int)e.Argument;
                Uint64 response = 0;
                if (action == (int)StatusEnum.Key)
                {
                    sock.exchangeKey();
                }
                else
                {
                    if (!sock.hasKey()) sock.exchangeKey();
                    if (action == (int)StatusEnum.Restart)
                    {
                        output(String.Format("Sending restart request to {0}...", sock.getAddress()));
                        Uint32 timeout;
                        Uint64 reply = 0;
                        Uint64 code;
                        timeout = ArduinoSocket.DEFAULT_TIMEOUT + common.WAIT_INTERVAL;
                        code = common.PRESS_RESTART;
                        bool result = sock.exchangeMessage(code, ref reply, (int)timeout, ArduinoSocket.DEFAULT_MAX_TIMEOUTS, 0);
                        if (result) output("Reset button acknowledged.");
                    }
                    else
                    {
                        if (action == (int)StatusEnum.Status || action == (int)StatusEnum.Power || action == (int)StatusEnum.PowerOff) checkStatus(ref response);
                        if ((action == (int)StatusEnum.Power && response == common.STATUS_OFF) || action == (int)StatusEnum.Shutdown)
                        {
                            output(String.Format("Sending power button request to {0}...", sock.getAddress()));
                            Uint32 timeout;
                            Uint64 reply = 0;
                            Uint64 code;
                            timeout = ArduinoSocket.DEFAULT_TIMEOUT + common.WAIT_INTERVAL;
                            if (action == (int)StatusEnum.Power)
                                code = common.PRESS_SHORT;
                            else
                                code = common.PRESS_LONG;
                            bool result = sock.exchangeMessage(code, ref reply, (int)timeout, ArduinoSocket.DEFAULT_MAX_TIMEOUTS, 0);
                            if (result) output("Power button acknowledged.");
                        }
                        if (action == (int)StatusEnum.PowerOff && response == common.STATUS_ON)
                        {
                            output(String.Format("Sending power button request to {0}...", sock.getAddress()));
                            Uint32 timeout;
                            Uint64 reply = 0;
                            Uint64 code;
                            timeout = ArduinoSocket.DEFAULT_TIMEOUT + common.WAIT_INTERVAL;
                            code = common.PRESS_SHORT;
                            bool result = sock.exchangeMessage(code, ref reply, (int)timeout, ArduinoSocket.DEFAULT_MAX_TIMEOUTS, 0);
                            if (result) output("Power button acknowledged.");
                        }
                    }
                }
            }
            catch (ArduinoSocket.CancelException)
            {
                output("Canceled.");
            }

            catch (Exception ee)
            {
                output("Error.");
                errorMessage(ee.Message, false);
            }
            setNetworkControlEnable(true);
        }

        private void powerOffClick(object sender, EventArgs e)
        {
            if (!networkThread.IsBusy)
            {
                sock.clearStop();
                int action = (int)StatusEnum.PowerOff;
                setNetworkControlEnable(false);
                networkThread.RunWorkerAsync(action);
            }
        }

        private void restartClick(object sender, EventArgs e)
        {
            if (!networkThread.IsBusy)
            {
                sock.clearStop();
                int action = (int)StatusEnum.Restart;
                setNetworkControlEnable(false);
                networkThread.RunWorkerAsync(action);
            }
        }
    }
}
