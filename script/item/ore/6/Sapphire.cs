using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Sapphire : BaseOre
    {
        public override string Name { get { return "사파이어"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 2300; } }

        public Sapphire()
            : base(63)
        {
        }

        public Sapphire(Serial serial)
            : base(serial)
        {
            m_ItemID = 63;
        }
    }
}
