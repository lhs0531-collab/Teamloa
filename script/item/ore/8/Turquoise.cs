using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Turquoise : BaseOre
    {
        public override string Name { get { return "터키석"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 3500; } }

        public Turquoise()
            : base(78)
        {
        }

        public Turquoise(Serial serial)
            : base(serial)
        {
            m_ItemID = 78;
        }
    }
}
