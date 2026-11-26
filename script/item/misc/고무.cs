using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    class 고무 : Item
    {
        public override string Name { get { return "고무"; } }
        public override bool Stackable { get { return true; } }
        public override ulong BuyPrice { get { return 100; } }
        public override short Limit { get { return 200; } }

        public 고무()
            : base(111)
        {

        }

        public 고무(Serial serial)
            : base(serial)
        {
            m_ItemID = 111;
        }
    }
}
