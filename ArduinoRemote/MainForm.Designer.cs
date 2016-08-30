namespace ArduinoRemote
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.address = new System.Windows.Forms.GroupBox();
            this.revertButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.octet1 = new System.Windows.Forms.TextBox();
            this.dot4 = new System.Windows.Forms.Label();
            this.dot1 = new System.Windows.Forms.Label();
            this.dot3 = new System.Windows.Forms.Label();
            this.octet2 = new System.Windows.Forms.TextBox();
            this.dot2 = new System.Windows.Forms.Label();
            this.octet3 = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.octet4 = new System.Windows.Forms.TextBox();
            this.communication = new System.Windows.Forms.GroupBox();
            this.restartButton = new System.Windows.Forms.Button();
            this.powerOffButton = new System.Windows.Forms.Button();
            this.keyButton = new System.Windows.Forms.Button();
            this.shutdownButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.statusButton = new System.Windows.Forms.Button();
            this.powerOnButton = new System.Windows.Forms.Button();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.exitButton = new System.Windows.Forms.Button();
            this.networkThread = new System.ComponentModel.BackgroundWorker();
            this.errorTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.address.SuspendLayout();
            this.communication.SuspendLayout();
            this.SuspendLayout();
            // 
            // address
            // 
            this.address.Controls.Add(this.revertButton);
            this.address.Controls.Add(this.saveButton);
            this.address.Controls.Add(this.octet1);
            this.address.Controls.Add(this.dot4);
            this.address.Controls.Add(this.dot1);
            this.address.Controls.Add(this.dot3);
            this.address.Controls.Add(this.octet2);
            this.address.Controls.Add(this.dot2);
            this.address.Controls.Add(this.octet3);
            this.address.Controls.Add(this.port);
            this.address.Controls.Add(this.octet4);
            this.address.Location = new System.Drawing.Point(10, 12);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(398, 50);
            this.address.TabIndex = 2;
            this.address.TabStop = false;
            this.address.Text = "Arduino IP";
            // 
            // revertButton
            // 
            this.revertButton.Location = new System.Drawing.Point(299, 17);
            this.revertButton.Name = "revertButton";
            this.revertButton.Size = new System.Drawing.Size(84, 23);
            this.revertButton.TabIndex = 7;
            this.revertButton.Text = "Revert";
            this.revertButton.UseVisualStyleBackColor = true;
            this.revertButton.Click += new System.EventHandler(this.revertClick);
            this.revertButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.defaultMouseClick);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(209, 17);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(84, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveClick);
            this.saveButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.defaultMouseClick);
            // 
            // octet1
            // 
            this.octet1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.octet1.Location = new System.Drawing.Point(6, 19);
            this.octet1.MaxLength = 3;
            this.octet1.Name = "octet1";
            this.octet1.Size = new System.Drawing.Size(28, 21);
            this.octet1.TabIndex = 1;
            this.octet1.Tag = "1 223";
            this.octet1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.defaultMouseClick);
            this.octet1.TextChanged += new System.EventHandler(this.addressChange);
            this.octet1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addressKeyDown);
            this.octet1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.addressKeyPress);
            this.octet1.Validating += new System.ComponentModel.CancelEventHandler(this.addressValidating);
            // 
            // dot4
            // 
            this.dot4.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dot4.Location = new System.Drawing.Point(147, 22);
            this.dot4.Name = "dot4";
            this.dot4.Size = new System.Drawing.Size(6, 12);
            this.dot4.TabIndex = 0;
            this.dot4.Text = ":";
            this.dot4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dot1
            // 
            this.dot1.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dot1.Location = new System.Drawing.Point(37, 26);
            this.dot1.Name = "dot1";
            this.dot1.Size = new System.Drawing.Size(6, 12);
            this.dot1.TabIndex = 0;
            this.dot1.Text = ".";
            this.dot1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dot3
            // 
            this.dot3.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dot3.Location = new System.Drawing.Point(111, 25);
            this.dot3.Name = "dot3";
            this.dot3.Size = new System.Drawing.Size(6, 12);
            this.dot3.TabIndex = 0;
            this.dot3.Text = ".";
            this.dot3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // octet2
            // 
            this.octet2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.octet2.Location = new System.Drawing.Point(43, 19);
            this.octet2.MaxLength = 3;
            this.octet2.Name = "octet2";
            this.octet2.Size = new System.Drawing.Size(28, 21);
            this.octet2.TabIndex = 2;
            this.octet2.Tag = "0 255";
            this.octet2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.defaultMouseClick);
            this.octet2.TextChanged += new System.EventHandler(this.addressChange);
            this.octet2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addressKeyDown);
            this.octet2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.addressKeyPress);
            this.octet2.Validating += new System.ComponentModel.CancelEventHandler(this.addressValidating);
            // 
            // dot2
            // 
            this.dot2.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dot2.Location = new System.Drawing.Point(74, 26);
            this.dot2.Name = "dot2";
            this.dot2.Size = new System.Drawing.Size(6, 12);
            this.dot2.TabIndex = 0;
            this.dot2.Text = ".";
            this.dot2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // octet3
            // 
            this.octet3.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.octet3.Location = new System.Drawing.Point(80, 19);
            this.octet3.MaxLength = 3;
            this.octet3.Name = "octet3";
            this.octet3.Size = new System.Drawing.Size(28, 21);
            this.octet3.TabIndex = 3;
            this.octet3.Tag = "0 255";
            this.octet3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.defaultMouseClick);
            this.octet3.TextChanged += new System.EventHandler(this.addressChange);
            this.octet3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addressKeyDown);
            this.octet3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.addressKeyPress);
            this.octet3.Validating += new System.ComponentModel.CancelEventHandler(this.addressValidating);
            // 
            // port
            // 
            this.port.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.port.Location = new System.Drawing.Point(154, 19);
            this.port.MaxLength = 5;
            this.port.Multiline = true;
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(38, 20);
            this.port.TabIndex = 5;
            this.port.Tag = "0 65535";
            this.port.MouseClick += new System.Windows.Forms.MouseEventHandler(this.defaultMouseClick);
            this.port.TextChanged += new System.EventHandler(this.addressChange);
            this.port.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addressKeyDown);
            this.port.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.addressKeyPress);
            this.port.Validating += new System.ComponentModel.CancelEventHandler(this.addressValidating);
            // 
            // octet4
            // 
            this.octet4.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.octet4.Location = new System.Drawing.Point(117, 19);
            this.octet4.MaxLength = 3;
            this.octet4.Name = "octet4";
            this.octet4.Size = new System.Drawing.Size(28, 21);
            this.octet4.TabIndex = 4;
            this.octet4.Tag = "0 255";
            this.octet4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.defaultMouseClick);
            this.octet4.TextChanged += new System.EventHandler(this.addressChange);
            this.octet4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addressKeyDown);
            this.octet4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.addressKeyPress);
            this.octet4.Validating += new System.ComponentModel.CancelEventHandler(this.addressValidating);
            // 
            // communication
            // 
            this.communication.Controls.Add(this.restartButton);
            this.communication.Controls.Add(this.powerOffButton);
            this.communication.Controls.Add(this.keyButton);
            this.communication.Controls.Add(this.shutdownButton);
            this.communication.Controls.Add(this.cancelButton);
            this.communication.Controls.Add(this.statusButton);
            this.communication.Controls.Add(this.powerOnButton);
            this.communication.Location = new System.Drawing.Point(10, 68);
            this.communication.Name = "communication";
            this.communication.Size = new System.Drawing.Size(111, 235);
            this.communication.TabIndex = 3;
            this.communication.TabStop = false;
            this.communication.Text = "Power Option";
            // 
            // restartButton
            // 
            this.restartButton.Location = new System.Drawing.Point(6, 134);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(100, 23);
            this.restartButton.TabIndex = 6;
            this.restartButton.Text = "Restart";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.restartClick);
            this.restartButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.defaultMouseClick);
            // 
            // powerOffButton
            // 
            this.powerOffButton.Location = new System.Drawing.Point(6, 105);
            this.powerOffButton.Name = "powerOffButton";
            this.powerOffButton.Size = new System.Drawing.Size(100, 23);
            this.powerOffButton.TabIndex = 5;
            this.powerOffButton.Text = "Power OFF";
            this.powerOffButton.UseVisualStyleBackColor = true;
            this.powerOffButton.Click += new System.EventHandler(this.powerOffClick);
            this.powerOffButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.defaultMouseClick);
            // 
            // keyButton
            // 
            this.keyButton.Location = new System.Drawing.Point(6, 18);
            this.keyButton.Name = "keyButton";
            this.keyButton.Size = new System.Drawing.Size(100, 23);
            this.keyButton.TabIndex = 0;
            this.keyButton.Text = "Exchange Key";
            this.keyButton.UseVisualStyleBackColor = true;
            this.keyButton.Click += new System.EventHandler(this.keyClick);
            this.keyButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.defaultMouseClick);
            // 
            // shutdownButton
            // 
            this.shutdownButton.Location = new System.Drawing.Point(6, 163);
            this.shutdownButton.Name = "shutdownButton";
            this.shutdownButton.Size = new System.Drawing.Size(100, 23);
            this.shutdownButton.TabIndex = 3;
            this.shutdownButton.Text = "Force Shutdown";
            this.shutdownButton.UseVisualStyleBackColor = true;
            this.shutdownButton.Click += new System.EventHandler(this.shutdownClick);
            this.shutdownButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.defaultMouseClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(6, 204);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(99, 25);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButtonClick);
            // 
            // statusButton
            // 
            this.statusButton.Location = new System.Drawing.Point(6, 47);
            this.statusButton.Name = "statusButton";
            this.statusButton.Size = new System.Drawing.Size(100, 23);
            this.statusButton.TabIndex = 1;
            this.statusButton.Text = "Check Status";
            this.statusButton.UseVisualStyleBackColor = true;
            this.statusButton.Click += new System.EventHandler(this.statusClick);
            this.statusButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.defaultMouseClick);
            // 
            // powerOnButton
            // 
            this.powerOnButton.Location = new System.Drawing.Point(6, 76);
            this.powerOnButton.Name = "powerOnButton";
            this.powerOnButton.Size = new System.Drawing.Size(100, 23);
            this.powerOnButton.TabIndex = 2;
            this.powerOnButton.Text = "Power ON";
            this.powerOnButton.UseVisualStyleBackColor = true;
            this.powerOnButton.Click += new System.EventHandler(this.powerClick);
            this.powerOnButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.defaultMouseClick);
            // 
            // outputBox
            // 
            this.outputBox.BackColor = System.Drawing.SystemColors.Window;
            this.outputBox.CausesValidation = false;
            this.outputBox.HideSelection = false;
            this.outputBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.outputBox.Location = new System.Drawing.Point(123, 68);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputBox.Size = new System.Drawing.Size(465, 235);
            this.outputBox.TabIndex = 4;
            this.outputBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.defaultMouseClick);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(504, 309);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(84, 23);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitClick);
            this.exitButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.defaultMouseClick);
            // 
            // networkThread
            // 
            this.networkThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.networkThreadFunc);
            // 
            // errorTooltip
            // 
            this.errorTooltip.AutomaticDelay = 0;
            this.errorTooltip.IsBalloon = true;
            this.errorTooltip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(600, 342);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.communication);
            this.Controls.Add(this.address);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Arduino Remote Control Program";
            this.address.ResumeLayout(false);
            this.address.PerformLayout();
            this.communication.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox address;
        private System.Windows.Forms.Button revertButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox octet1;
        private System.Windows.Forms.Label dot4;
        private System.Windows.Forms.Label dot1;
        private System.Windows.Forms.Label dot3;
        private System.Windows.Forms.TextBox octet2;
        private System.Windows.Forms.Label dot2;
        private System.Windows.Forms.TextBox octet3;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox octet4;
        private System.Windows.Forms.GroupBox communication;
        private System.Windows.Forms.Button keyButton;
        private System.Windows.Forms.Button shutdownButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button statusButton;
        private System.Windows.Forms.Button powerOnButton;
        private System.Windows.Forms.TextBox outputBox;
        private System.Windows.Forms.Button exitButton;
        private System.ComponentModel.BackgroundWorker networkThread;
        private System.Windows.Forms.ToolTip errorTooltip;
        private System.Windows.Forms.Button powerOffButton;
        private System.Windows.Forms.Button restartButton;
    }
}

