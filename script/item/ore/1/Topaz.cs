using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Topaz : BaseOre
    {
        public override string Name { get { return "토파즈"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 750; } }

        public Topaz()
            : base(79)
        {
        }

        public Topaz(Serial serial)
            : base(serial)
        {
            m_ItemID = 79;
        }
    }
}
