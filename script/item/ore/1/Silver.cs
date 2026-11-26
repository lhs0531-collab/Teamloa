using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Silver : BaseOre
    {
        public override string Name { get { return "은"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 650; } }

        public Silver()
            : base(64)
        {
        }

        public Silver(Serial serial)
            : base(serial)
        {
            m_ItemID = 64;
        }
    }
}
