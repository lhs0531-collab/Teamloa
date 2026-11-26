using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class 장작 : Item
    {
        public override string Name { get { return "장작"; } }
        public override ulong BuyPrice { get { return 100; } }
        public override bool Stackable { get { return true; } }

        public 장작() : base(115)
        {
        }

        public 장작(Serial serial) : base(serial)
        {
            m_ItemID = 115;
        }
    }
}
