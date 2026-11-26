using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class Bronze : BaseNPC
    {
        public override string Name { get { return "동상"; } }
        public override List<string> ChatString
        {
            get
            {
                return new List<string>()
                {
                };
            }
        }

        public override byte Sprite { get { return (int)13; } }
        public override byte aSpeed { get { return 1; } }
        public override byte aFrames { get { return 1; } }
        public override byte aStartFrames { get { return 0; } }
        public override int AliveTime { get { return 30 * Settings.Minute; } }
        public override int reSpawnTime { get { return 0; } }

        public override void Event_Message(Player player, string[] str2, string str)
        {
        }

        public Bronze(Serial serial, int Channel, string Map, Point2D Loc)
            : base(serial)
        {
            base.m_Channel = Channel;
            base.m_Map = Map;
            base.m_Loc = new Point2D(Loc.X, Loc.Y);
        }
    }
}
