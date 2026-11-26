using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class AquaMarine : BaseOre
    {
        public override string Name { get { return "아쿠아머린"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 2200; } }

        public AquaMarine()
            : base(67)
        {
        }

        public AquaMarine(Serial serial)
            : base(serial)
        {
            m_ItemID = 67;
        }
    }
}
