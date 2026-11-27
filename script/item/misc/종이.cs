using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class 종이 : Item
    {
        public override string Name { get { return "종이"; } }
        public override bool Stackable { get { return true; } }
        public override ulong BuyPrice { get { return 100; } }
        public override short Limit { get { return 1000; } }

        public 종이()
            : base(116)
        {

        }

        public 종이(Serial serial)
            : base(serial)
        {
            m_ItemID = 116;
        }
        public override void Event_Give(Player player, Item itemdragdrop)
        {
        }
    }
}
