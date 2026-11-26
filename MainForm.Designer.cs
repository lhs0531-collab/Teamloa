namespace LKCamelot
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.IPLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Time = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.GuildCountLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.GMLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.MonsterCountLabel = new System.Windows.Forms.Label();
            this.PlayerCountLabel = new System.Windows.Forms.Label();
            this.VilageCountLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ChatLogTextBox = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.BubbleChatLogTextBox = new System.Windows.Forms.TextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.GuildChatLogTextBox = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.WispChatLogTextBox = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.CommandtextBox = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(564, 332);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(556, 306);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "State";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(473, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "저장";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PortLabel);
            this.groupBox2.Controls.Add(this.IPLabel);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(550, 36);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Information";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(256, 17);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(62, 12);
            this.PortLabel.TabIndex = 3;
            this.PortLabel.Text = "Port Label";
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Location = new System.Drawing.Point(36, 17);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(46, 12);
            this.IPLabel.TabIndex = 2;
            this.IPLabel.Text = "IP Addr";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Time);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.GuildCountLabel);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.GMLabel);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.MonsterCountLabel);
            this.groupBox1.Controls.Add(this.PlayerCountLabel);
            this.groupBox1.Controls.Add(this.VilageCountLabel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(3, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 134);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Count";
            // 
            // Time
            // 
            this.Time.Location = new System.Drawing.Point(54, 113);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(135, 12);
            this.Time.TabIndex = 13;
            this.Time.Text = "None";
            this.Time.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "Time :";
            // 
            // GuildCountLabel
            // 
            this.GuildCountLabel.Location = new System.Drawing.Point(100, 38);
            this.GuildCountLabel.Name = "GuildCountLabel";
            this.GuildCountLabel.Size = new System.Drawing.Size(89, 12);
            this.GuildCountLabel.TabIndex = 11;
            this.GuildCountLabel.Text = "None";
            this.GuildCountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "Guild :";
            // 
            // GMLabel
            // 
            this.GMLabel.Location = new System.Drawing.Point(96, 93);
            this.GMLabel.Name = "GMLabel";
            this.GMLabel.Size = new System.Drawing.Size(93, 12);
            this.GMLabel.TabIndex = 9;
            this.GMLabel.Text = "None";
            this.GMLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "GMs :";
            // 
            // MonsterCountLabel
            // 
            this.MonsterCountLabel.Location = new System.Drawing.Point(96, 75);
            this.MonsterCountLabel.Name = "MonsterCountLabel";
            this.MonsterCountLabel.Size = new System.Drawing.Size(93, 12);
            this.MonsterCountLabel.TabIndex = 7;
            this.MonsterCountLabel.Text = "None";
            this.MonsterCountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PlayerCountLabel
            // 
            this.PlayerCountLabel.Location = new System.Drawing.Point(98, 57);
            this.PlayerCountLabel.Name = "PlayerCountLabel";
            this.PlayerCountLabel.Size = new System.Drawing.Size(91, 12);
            this.PlayerCountLabel.TabIndex = 6;
            this.PlayerCountLabel.Text = "None";
            this.PlayerCountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // VilageCountLabel
            // 
            this.VilageCountLabel.Location = new System.Drawing.Point(100, 19);
            this.VilageCountLabel.Name = "VilageCountLabel";
            this.VilageCountLabel.Size = new System.Drawing.Size(89, 12);
            this.VilageCountLabel.TabIndex = 5;
            this.VilageCountLabel.Text = "None";
            this.VilageCountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "Monster :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "Player :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Village :";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(556, 306);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Chat";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.ChatLogTextBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(550, 300);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // ChatLogTextBox
            // 
            this.ChatLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatLogTextBox.Location = new System.Drawing.Point(0, 0);
            this.ChatLogTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.ChatLogTextBox.Multiline = true;
            this.ChatLogTextBox.Name = "ChatLogTextBox";
            this.ChatLogTextBox.ReadOnly = true;
            this.ChatLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatLogTextBox.Size = new System.Drawing.Size(550, 280);
            this.ChatLogTextBox.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(0, 280);
            this.textBox2.Margin = new System.Windows.Forms.Padding(0);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(550, 21);
            this.textBox2.TabIndex = 1;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.BubbleChatLogTextBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(556, 306);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "BubbleChat";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // BubbleChatLogTextBox
            // 
            this.BubbleChatLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BubbleChatLogTextBox.Location = new System.Drawing.Point(3, 3);
            this.BubbleChatLogTextBox.Multiline = true;
            this.BubbleChatLogTextBox.Name = "BubbleChatLogTextBox";
            this.BubbleChatLogTextBox.ReadOnly = true;
            this.BubbleChatLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.BubbleChatLogTextBox.Size = new System.Drawing.Size(550, 300);
            this.BubbleChatLogTextBox.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.GuildChatLogTextBox);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(556, 306);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "GuildChat";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // GuildChatLogTextBox
            // 
            this.GuildChatLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GuildChatLogTextBox.Location = new System.Drawing.Point(3, 3);
            this.GuildChatLogTextBox.Multiline = true;
            this.GuildChatLogTextBox.Name = "GuildChatLogTextBox";
            this.GuildChatLogTextBox.ReadOnly = true;
            this.GuildChatLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GuildChatLogTextBox.Size = new System.Drawing.Size(550, 300);
            this.GuildChatLogTextBox.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.WispChatLogTextBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(556, 306);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "WhsipChat";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // WispChatLogTextBox
            // 
            this.WispChatLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WispChatLogTextBox.Location = new System.Drawing.Point(3, 3);
            this.WispChatLogTextBox.Multiline = true;
            this.WispChatLogTextBox.Name = "WispChatLogTextBox";
            this.WispChatLogTextBox.ReadOnly = true;
            this.WispChatLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.WispChatLogTextBox.Size = new System.Drawing.Size(550, 300);
            this.WispChatLogTextBox.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.CommandtextBox);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(556, 306);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "CmdChat";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // CommandtextBox
            // 
            this.CommandtextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CommandtextBox.Location = new System.Drawing.Point(3, 3);
            this.CommandtextBox.Multiline = true;
            this.CommandtextBox.Name = "CommandtextBox";
            this.CommandtextBox.ReadOnly = true;
            this.CommandtextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CommandtextBox.Size = new System.Drawing.Size(550, 300);
            this.CommandtextBox.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 332);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(580, 370);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(580, 370);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox ChatLogTextBox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox WispChatLogTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Label MonsterCountLabel;
        private System.Windows.Forms.Label PlayerCountLabel;
        private System.Windows.Forms.Label VilageCountLabel;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox BubbleChatLogTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label GMLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label GuildCountLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox CommandtextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TextBox GuildChatLogTextBox;
        private System.Windows.Forms.Label Time;
        private System.Windows.Forms.Label label9;
    }
}