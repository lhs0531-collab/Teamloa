using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class Quester : BaseNPC
    {
        public override string Name { get { return "Quester"; } }

        public override byte Sprite { get { return 8; } }
        public override byte aSpeed { get { return 4; } }
        public override byte aFrames { get { return 4; } }
        public override int AliveTime { get { return 0; } }
        public override int reSpawnTime { get { return 0; } }

        public Quester(Serial serial, int Channel, string Map, Point2D Loc)
            : base(serial)
        {
            base.m_Channel = Channel;
            m_Map = Map;
            base.m_Loc = new Point2D(Loc.X, Loc.Y);
        }

        public override void Event_Message(Player player, string[] str2, string str)
        {
            if (str == "시간")
            {
                long time = ((Settings.QuestTime) - (Server.tickcount.ElapsedMilliseconds - World.QuestTime))/Settings.Second;
                Event_Chat(string.Format("{0}초 남았습니다.", time), true);    
            }
        }
    }
}
