using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class 각목 : Item
    {
        public override string Name { get { return "각목"; } }
        public override bool Stackable { get { return true; } }
        public override ulong BuyPrice { get { return 100; } }
        public override short Limit { get { return 1000; } }

        public 각목() : base(110)
        {

        }

        public 각목(Serial serial)
            : base(serial)
        {
            m_ItemID = 110;
        }
        public override void Event_Give(Player player, Item itemdragdrop)
        {
        }
    }
}
