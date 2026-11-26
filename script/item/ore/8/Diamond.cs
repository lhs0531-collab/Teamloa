using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Diamond : BaseOre
    {
        public override string Name { get { return "다이아몬드"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 4000; } }

        public Diamond()
            : base(54)
        {
        }

        public Diamond(Serial serial)
            : base(serial)
        {
            m_ItemID = 54;
        }
    }
}
