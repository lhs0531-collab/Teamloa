using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Amber : BaseOre
    {
        public override string Name { get { return "호박"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 2100; } }

        public Amber()
            : base(59)
        {
        }

        public Amber(Serial serial)
            : base(serial)
        {
            m_ItemID = 59;
        }
    }
}
