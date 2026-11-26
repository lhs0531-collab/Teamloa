using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class GoldenDiamond : BaseOre
    {
        public override string Name { get { return "골든 다이아몬드"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 5000; } }

        public GoldenDiamond()
            : base(53)
        {
        }

        public GoldenDiamond(Serial serial)
            : base(serial)
        {
            m_ItemID = 53;
        }
    }
}
