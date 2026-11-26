using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class PKKing : BaseNPC
    {
        public override string Name { get { return "대전장"; } }
        public override List<string> ChatString
        {
            get
            {
                return new List<string>()
                {
                    "전 대전장 관리인입니다.",
                    "\'홍팀참가\'하면 홍팀이 됩니다.",
                    "\'청팀참가\'하면 청팀이 됩니다.",
                    "\'탈퇴\' 하면 참가가 해지 됩니다."
                };
            }
        }
        public override byte Sprite { get { return (int)5; } }
        public override byte aSpeed { get { return 4; } }
        public override byte aFrames { get { return 7; } }

        public PKKing(Serial serial, int Channel, string Map, Point2D Loc)
            : base(serial)
        {
            base.m_Channel = Channel;
            base.m_Map = Map;
            base.m_Loc = new Point2D(Loc.X, Loc.Y);
        }

        public override void Event_Message(Player player, string[] str2, string str)
        {
            if (str == "청팀참가")
            {
                player.Color = library.Color.Blue;
            }
            else if (str == "홍팀참가")
            {
                player.Color = library.Color.Red;
            }
            else if (str == "탈퇴")
            {
                player.Color = library.Color.None;
            }
        }

    }
}
