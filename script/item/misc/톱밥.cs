using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class 톱밥 : Item
    {
        public override string Name { get { return "톱밥"; } }
        public override bool Stackable { get { return true; } }
        public override ulong BuyPrice { get { return 100; } }
        public override short Limit { get { return 1000; } }

        public 톱밥()
            : base(117)
        {

        }

        public 톱밥(Serial serial)
            : base(serial)
        {
            m_ItemID = 117;
        }
        public override void Event_Give(Player player, Item itemdragdrop)
        {
        }
    }
}
