using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class 자갈 : Item
    {
        public override string Name { get { return "자갈"; } }
        public override ulong BuyPrice { get { return 1000; } }
        public override int SellPrice { get { return 100; } }
        public override bool Stackable { get { return true; } }
        public override short Limit { get { return 1000; } }

        public 자갈()
            : base(126)
        {

        }

        public 자갈(Serial serial)
            : base(serial)
        {
            m_ItemID = 126;
        }
        public override void Event_Give(Player player, Item itemdragdrop)
        {
        }
    }
}
