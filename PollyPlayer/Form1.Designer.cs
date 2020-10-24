namespace PollyPlayer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.voicesListBox = new System.Windows.Forms.ListBox();
            this.speechTextBox = new System.Windows.Forms.TextBox();
            this.sayItButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveToFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.delAfterSay = new System.Windows.Forms.CheckBox();
            this.listenAndChat = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblCredsFile = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSoundDevice = new System.Windows.Forms.ComboBox();
            this.btnNonsense = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // voicesListBox
            // 
            this.voicesListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.voicesListBox.FormattingEnabled = true;
            this.voicesListBox.ItemHeight = 16;
            this.voicesListBox.Location = new System.Drawing.Point(3, 3);
            this.voicesListBox.Name = "voicesListBox";
            this.voicesListBox.Size = new System.Drawing.Size(344, 532);
            this.voicesListBox.TabIndex = 0;
            // 
            // speechTextBox
            // 
            this.speechTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speechTextBox.Location = new System.Drawing.Point(353, 3);
            this.speechTextBox.Multiline = true;
            this.speechTextBox.Name = "speechTextBox";
            this.speechTextBox.Size = new System.Drawing.Size(494, 492);
            this.speechTextBox.TabIndex = 1;
            // 
            // sayItButton
            // 
            this.sayItButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sayItButton.Location = new System.Drawing.Point(355, 501);
            this.sayItButton.Name = "sayItButton";
            this.sayItButton.Size = new System.Drawing.Size(492, 33);
            this.sayItButton.TabIndex = 2;
            this.sayItButton.Text = "Say It (Ctrl+Enter)";
            this.sayItButton.UseVisualStyleBackColor = true;
            this.sayItButton.Click += new System.EventHandler(this.SayItButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pick a Voice";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(355, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Type Some Text or";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.saveToFile);
            this.panel1.Controls.Add(this.voicesListBox);
            this.panel1.Controls.Add(this.speechTextBox);
            this.panel1.Controls.Add(this.sayItButton);
            this.panel1.Location = new System.Drawing.Point(6, 121);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 579);
            this.panel1.TabIndex = 5;
            // 
            // saveToFile
            // 
            this.saveToFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveToFile.Location = new System.Drawing.Point(353, 540);
            this.saveToFile.Name = "saveToFile";
            this.saveToFile.Size = new System.Drawing.Size(492, 33);
            this.saveToFile.TabIndex = 3;
            this.saveToFile.Text = "Save Audio To File";
            this.saveToFile.UseVisualStyleBackColor = true;
            this.saveToFile.Click += new System.EventHandler(this.saveToFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.delAfterSay);
            this.groupBox1.Controls.Add(this.listenAndChat);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.lblCredsFile);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbSoundDevice);
            this.groupBox1.Location = new System.Drawing.Point(6, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(853, 78);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // delAfterSay
            // 
            this.delAfterSay.AutoSize = true;
            this.delAfterSay.Location = new System.Drawing.Point(538, 20);
            this.delAfterSay.Name = "delAfterSay";
            this.delAfterSay.Size = new System.Drawing.Size(120, 17);
            this.delAfterSay.TabIndex = 12;
            this.delAfterSay.Text = "Clear Text After Say";
            this.delAfterSay.UseVisualStyleBackColor = true;
            this.delAfterSay.CheckedChanged += new System.EventHandler(this.delAfterSay_CheckedChanged);
            // 
            // listenAndChat
            // 
            this.listenAndChat.AutoSize = true;
            this.listenAndChat.Location = new System.Drawing.Point(405, 45);
            this.listenAndChat.Name = "listenAndChat";
            this.listenAndChat.Size = new System.Drawing.Size(332, 30);
            this.listenAndChat.TabIndex = 11;
            this.listenAndChat.Text = "Listen And Chat\r\nSelect this to chat and hear what\'s being played at the same tim" +
    "e";
            this.listenAndChat.UseVisualStyleBackColor = true;
            this.listenAndChat.CheckedChanged += new System.EventHandler(this.listenAndChat_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(405, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Pick New Credentials";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblCredsFile
            // 
            this.lblCredsFile.AutoSize = true;
            this.lblCredsFile.Location = new System.Drawing.Point(194, 51);
            this.lblCredsFile.Name = "lblCredsFile";
            this.lblCredsFile.Size = new System.Drawing.Size(163, 13);
            this.lblCredsFile.TabIndex = 9;
            this.lblCredsFile.Text = "(None - Program will not function)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(190, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(208, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "AWS Credentials File";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "Sound Device";
            // 
            // cbSoundDevice
            // 
            this.cbSoundDevice.FormattingEnabled = true;
            this.cbSoundDevice.Location = new System.Drawing.Point(7, 51);
            this.cbSoundDevice.Name = "cbSoundDevice";
            this.cbSoundDevice.Size = new System.Drawing.Size(171, 21);
            this.cbSoundDevice.TabIndex = 0;
            // 
            // btnNonsense
            // 
            this.btnNonsense.Location = new System.Drawing.Point(550, 94);
            this.btnNonsense.Name = "btnNonsense";
            this.btnNonsense.Size = new System.Drawing.Size(114, 23);
            this.btnNonsense.TabIndex = 7;
            this.btnNonsense.Text = "Generate Nonsense";
            this.btnNonsense.UseVisualStyleBackColor = true;
            this.btnNonsense.Click += new System.EventHandler(this.btnNonsense_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 707);
            this.Controls.Add(this.btnNonsense);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Qeslin Player";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox voicesListBox;
        private System.Windows.Forms.TextBox speechTextBox;
        private System.Windows.Forms.Button sayItButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbSoundDevice;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblCredsFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button saveToFile;
        private System.Windows.Forms.CheckBox listenAndChat;
        private System.Windows.Forms.CheckBox delAfterSay;
        private System.Windows.Forms.Button btnNonsense;
    }
}

