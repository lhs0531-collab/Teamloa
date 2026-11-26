using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class RockPaperScissors : BaseNPC
    {
        public override string Name { get { return "달인"; } }
        public override List<string> ChatString
        {
            get
            {
                return new List<string>()
                {
                    "저랑 가위바위보 한판하실래요??",
                    "전 가위바위보의 달인입니다.",
                    "이번에 가위를 낼껍니다",
                    "이번에 바위를 낼껍니다",
                    "이번에 보를 낼껍니다"
                };
            }
        }

        public override byte Sprite { get { return 8; } }
        public override byte aSpeed { get { return 4; } }
        public override byte aFrames { get { return 4; } }
        public override int MessageGap { get { return 3 * Settings.Second; } }

        public RockPaperScissors(Serial serial, int Channel, string Map, Point2D Loc)
            : base(serial)
        {
            base.m_Channel = Channel;
            base.m_Map = Map;
            base.m_Loc = new Point2D(Loc.X, Loc.Y);
        }

        public override void Event_Message(Player player, string[] str2, string str)
        {
            int Rand = Util.Random(0, 3);       //가위 바위 보
            if (str == "가위" || str == "바위" || str == "보")
            {
                string temp = string.Empty;
                if (Rand == 0)
                {
                    if (str=="보")
                        temp = " 당연한 결과군..";
                    else if(str =="바위")
                        temp = " 내가 지다니...";
                    else
                        temp = " 새로운 고수인가..?";
                    this.Event_Chat("가위!!!" + temp);
                }
                else if (Rand == 1)
                {
                    if (str == "가위")
                        temp = " 당연한 결과군..";
                    else if (str == "보")
                        temp = " 우연히 이겼군..";
                    else
                        temp = " 제법하는군";
                    this.Event_Chat("바위!!!" + temp);
                }
                else
                {
                    if (str == "바위")
                        temp = " 뻔한 결과군..";
                    else if (str == "가위")
                        temp = " 내가 지다니...";
                    else
                        temp = " 제법하는군";
                    this.Event_Chat("보!!!" + temp);
                }
            }
        }

        public override void Event_Chat(string message, bool flag = false)
        {
            if (Alive == true && Server.tickcount.ElapsedMilliseconds - LastChat > MessageGap)
            {
                LastChat = Server.tickcount.ElapsedMilliseconds;
                World.SendToRange(new QueDele(this.m_Channel, this.m_Map, new UpdateCharChatBox(this.m_Serial, string.Format("{0}: {1}", this.Name, message)).Compile()), this);
            }
        }

    }
}
