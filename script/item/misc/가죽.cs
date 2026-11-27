using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class 가죽 : Item
    {
        public override string Name { get { return "가죽"; } }
        public override bool Stackable { get { return true; } }
        public override ulong BuyPrice { get { return 100; } }
        public override short Limit { get { return 1000; } }

        public 가죽()
            : base(71)
        {

        }

        public 가죽(Serial serial)
            : base(serial)
        {
            m_ItemID = 71;
        }
        public override void Event_Give(Player player, Item itemdragdrop)
        {
        }
    }
}
