using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    class 조가비 : Item
    {
        public override string Name { get { return "조가비"; } }
        public override bool Stackable { get { return true; } }
        public override ulong BuyPrice { get { return 100; } }
        public override short Limit { get { return 1000; } }

        public 조가비()
            : base(127)
        {

        }

        public 조가비(Serial serial)
            : base(serial)
        {
            m_ItemID = 127;
        }
    }
}
