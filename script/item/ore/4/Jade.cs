using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Jade : BaseOre
    {
        public override string Name { get { return "제이드"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 1650; } }

        public Jade()
            : base(70)
        {
        }

        public Jade(Serial serial)
            : base(serial)
        {
            m_ItemID = 70;
        }
    }
}
