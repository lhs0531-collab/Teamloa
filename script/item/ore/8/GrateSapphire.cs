using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class GrateSapphire : BaseOre
    {
        public override string Name { get { return "그레이트사파이어"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 4500; } }

        public GrateSapphire()
            : base(63)
        {
        }

        public GrateSapphire(Serial serial)
            : base(serial)
        {
            m_ItemID = 63;
        }
    }
}
