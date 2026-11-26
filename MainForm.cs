using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace LKCamelot
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private static readonly ConcurrentQueue<string> ChatLog = new ConcurrentQueue<string>();
        private static readonly ConcurrentQueue<string> BubbleChatLog = new ConcurrentQueue<string>();
        private static readonly ConcurrentQueue<string> GuildChatLog = new ConcurrentQueue<string>();
        private static readonly ConcurrentQueue<string> WispChatLog = new ConcurrentQueue<string>();
        private static readonly ConcurrentQueue<string> CmdChatLog = new ConcurrentQueue<string>();

        private static readonly ConcurrentQueue<string> CmdGlobalLog = new ConcurrentQueue<string>();

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox2.Text == string.Empty)
                return;

            switch (e.KeyChar)
            {
                case (char)Keys.Enter:
                    {
                        if (textBox2.Text[0] == '#')
                        {

                        }
                        else if (textBox2.Text[0] == '/')
                        {
                            string[] line = textBox2.Text.Split(' ');
                            var player = model.PlayerHandler.getSingleton().Total.Where(xe => xe.Value.loggedIn && xe.Value.Name == line[0].Remove(0, 1).ToUpper()).FirstOrDefault();
                            if (player.Value != null)
                            {
                                this.EnqueueWispChat(textBox2.Text);

                                string message = textBox2.Text.Remove(0, line[0].Length);
                                player.Value.client.SendPacket(new model.UpdateChatBox(0x94, 0x52, 1, "<서버운영자> : " + message).Compile());
                            }
                        }
                        else
                        {
                            this.EnqueueChat(textBox2.Text);
                            LKCamelot.model.World.SendToAll(new model.QueDele("all", new LKCamelot.model.UpdateChatBox(0xff, 0x7e, 15461, textBox2.Text).Compile()));
                        }
                        textBox2.Clear();
                        break;
                    }
            }
        }
        public void EnqueueCmdChat(string msg)
        {
            if (CmdChatLog.Count < 100)
            {
                //SocketHandler.SendMsg(msg);
                CmdChatLog.Enqueue(String.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, msg));
            }
        }
        public void EnqueueWispChat(string msg)
        {
            if (WispChatLog.Count < 100)
            {
                if (SocketHandler.ISGM)
                    SocketHandler.SendMsg(msg + "\r\n");
                WispChatLog.Enqueue(String.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, msg));
            }
        }

        public void EnqueueGuildChat(string msg)
        {
            if (GuildChatLog.Count < 100)
            {
                if (SocketHandler.ISGM)
                    SocketHandler.SendMsg(msg + "\r\n");
                GuildChatLog.Enqueue(String.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, msg));
            }
        }
        public void EnqueueBubbleChat(string msg)
        {
            if (BubbleChatLog.Count < 100)
            {
                if (SocketHandler.ISGM)
                    SocketHandler.SendMsg(msg + "\r\n");
                BubbleChatLog.Enqueue(String.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, msg));
            }
        }
        public void EnqueueChat(string msg)
        {
            if (ChatLog.Count < 100)
            {
                if (SocketHandler.ISGM)
                    SocketHandler.SendMsg(msg + "\r\n");
                ChatLog.Enqueue(String.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, msg));
            }
        }
        public void EnqueueGlobalChat(string msg)
        {
            if (CmdChatLog.Count < 100)
            {
                if (SocketHandler.ISGM)
                    SocketHandler.SendMsg(msg + "\r\n");
                CmdGlobalLog.Enqueue(String.Format("[{0}]: {1}" + Environment.NewLine, "GM", msg));
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int count = 0;
            int Alive = 0;

            if (model.World.NewMonsters != null && model.World.NewMonsters.Count() == Settings.VillageName.Count)
            {
                for (int i = 0; i < Settings.VillageName.Count; i++)
                    count += model.World.NewMonsters[i].Count();
                for (int i = 0; i < Settings.VillageName.Count; i++)
                    Alive += model.World.NewMonsters[i].Where(xe => xe.Value.Alive).Count();
            }
            MonsterCountLabel.Text = Alive.ToString() + " / " + count.ToString();

            GuildCountLabel.Text = "" + model.World.NewGuilds.Count();
            PlayerCountLabel.Text = Server.playerHandler.Total.Where(xe=>xe.Value.loggedIn).Count().ToString() + " / " + Server.playerHandler.Total.Count().ToString();
            VilageCountLabel.Text = Settings.VillageName.Count().ToString();
            GMLabel.Text = Server.playerHandler.Total.Where(xe => xe.Value.loggedIn && Settings.GMS.Contains(xe.Key)).Count() +" / " + Settings.GMS.Count();

            int Day = (int)(Server.tickcount.ElapsedMilliseconds / Settings.Day);
            int Hour = (int)((Server.tickcount.ElapsedMilliseconds%Settings.Day) / Settings.Hour);
            int Min = (int)(((Server.tickcount.ElapsedMilliseconds%Settings.Day) % Settings.Hour) / Settings.Minute);
            int Sec = (int)((((Server.tickcount.ElapsedMilliseconds % Settings.Day) % Settings.Hour)  % Settings.Minute) / Settings.Second);

            Time.Text = Day + "일 " + Hour +"시 " + Min + "분 " +Sec  + "초";
            
            while (!ChatLog.IsEmpty)
            {
                string message;

                if (!ChatLog.TryDequeue(out message)) continue;

                ChatLogTextBox.AppendText(message);
                this.CreateLog("_Log_Chat.txt", message);
            }
            while (!WispChatLog.IsEmpty)
            {
                string message;

                if (!WispChatLog.TryDequeue(out message)) continue;

                WispChatLogTextBox.AppendText(message);
                this.CreateLog("_Log_WispChat.txt", message);
            }
            while (!BubbleChatLog.IsEmpty)
            {
                string message;

                if (!BubbleChatLog.TryDequeue(out message)) continue;

                BubbleChatLogTextBox.AppendText(message);
                this.CreateLog("_Log_BubbleChat.txt", message);
            }
            while (!GuildChatLog.IsEmpty)
            {
                string message;

                if (!GuildChatLog.TryDequeue(out message)) continue;

                GuildChatLogTextBox.AppendText(message);
                this.CreateLog("_Log_GuildChat.txt", message);
            }
            while (!CmdChatLog.IsEmpty)
            {
                string message;

                if (!CmdChatLog.TryDequeue(out message)) continue;

                CommandtextBox.AppendText(message);
                this.CreateLog("_Log_CmdChat.txt", message);
            }
            while (!CmdGlobalLog.IsEmpty)
            {
                string message;

                if (!CmdGlobalLog.TryDequeue(out message)) continue;

                ChatLogTextBox.AppendText(message);

                LKCamelot.model.World.SendToAll(new model.QueDele("all", new LKCamelot.model.UpdateChatBox(0xff, 0x7e, 15461, message).Compile()));

               // this.CreateLog("_Log_GlobalChat.txt", message);
            }
        }


        private object writer = new object();
        public void CreateLog(string fileName, string message)
        {
            lock (writer)
            {
                string path = Settings.LogPath + DateTime.Now.ToString("yyyyMMdd") + "\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                System.IO.StreamWriter sr = new System.IO.StreamWriter(path + DateTime.Now.ToString("yyyyMMdd") + fileName, true);
                sr.Write(message);
                sr.Close(); sr.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            model.World.SaveAll();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = Settings.Version;
            timer1.Start();

            //IPLabel.Text = Settings.ServerIPAddress;
            IPLabel.Text = string.Format("{0} ({1})", Settings.ServerIPAddress, Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString());
            PortLabel.Text = Settings.ServerPort.ToString();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
