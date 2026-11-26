using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKCamelot.model;

namespace LKCamelot.script.npc
{
    public class Sign : BaseNPC
    {
        public Point2D AutoMoveLoc = null;
        public int Direction;

        public List<string> Message = new List<string>();

        public override string Name { get { return "표지판"; } }
        public override List<string> ChatString { get { return Message; } }
        public override Point2D Move { get { return AutoMoveLoc; } }

        public override bool Animation { get { return false; } }

        public override byte Sprite { get { return 12; } }
        public override byte aSpeed { get { return 1; } }
        public override byte aFrames { get { return 1; } }
        public override byte aStartFrames
        {
            get { return (byte)Direction; }
        }

        public Sign(Serial ser, int Channel, string map, Point2D loc, Point2D move, int direction, string message) : base(ser)
        {
            AutoMoveLoc = move;
            Direction = direction % 8;
            Message.Add(message);
            base.m_Channel = Channel;
            base.m_Map = map;
            base.m_Loc = loc;
        }
        public override void Event_Give(Player player, item.Item giveitem, int amount) { }
    }


    public class BoardSign : BaseNPC
    {
        public Point2D AutoMoveLoc = null;
        public int Direction;

        public List<string> Message = new List<string>();

        public override string Name { get { return "표지판"; } }
        public override List<string> ChatString { get { return Message; } }
        public override Point2D Move { get { return AutoMoveLoc; } }

        public override bool Animation { get { return false; } }

        public override byte Sprite { get { return 11; } }
        public override byte aSpeed { get { return 1; } }
        public override byte aFrames { get { return 1; } }
        public override byte aStartFrames
        {
            get { return (byte)Direction; }
        }

        public BoardSign(Serial ser, int Channel, string map, Point2D loc, int direction, string message) : base(ser)
        {
            Direction = direction % 4;
            Message.Add(message);
            base.m_Channel = Channel;
            base.m_Map = map;
            base.m_Loc = loc;
        }
        public override void Event_Give(Player player, item.Item giveitem, int amount){}
    }
}
