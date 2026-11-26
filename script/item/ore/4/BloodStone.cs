using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class BloodStone : BaseOre
    {
        public override string Name { get { return "블러드스톤"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 1550; } }

        public BloodStone()
            : base(73)
        {
        }

        public BloodStone(Serial serial)
            : base(serial)
        {
            m_ItemID = 73;
        }
    }
}
