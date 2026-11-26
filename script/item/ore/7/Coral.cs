using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Coral : BaseOre
    {
        public override string Name { get { return "산호"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 2800; } }

        public Coral()
            : base(64)
        {
        }

        public Coral(Serial serial)
            : base(serial)
        {
            m_ItemID = 64;
        }
    }
}
