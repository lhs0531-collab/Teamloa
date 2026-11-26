using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class 나무쪼가리 : Item
    {
        public override string Name { get { return "나무쪼가리"; } }
        public override bool Stackable { get { return true; } }
        public override ulong BuyPrice { get { return 100; } }
        public override short Limit { get { return 200; } }

        public 나무쪼가리() : base(112)
        {

        }

        public 나무쪼가리(Serial serial)
            : base(serial)
        {
            m_ItemID = 112;
        }
        public override void Event_Give(Player player, Item itemdragdrop)
        {
        }
    }
}
