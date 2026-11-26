using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Nickel : BaseOre
    {
        public override string Name { get { return "니켈"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 1150; } }
        public override script.item.Item OreTaught { get { return new NickelGem(); } }

        public Nickel()
            : base(90)
        {
        }

        public Nickel(Serial serial)
            : base(serial)
        {
            m_ItemID = 90;
        }
    }

    public class NickelGem : BaseOre
    {
        public override string Name { get { return "니켈"; } }
        public override int XP { get { return 160; } }
        public override int SellPrice { get { return 1150; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public NickelGem()
            : base(51)
        {

        }

        public NickelGem(Serial serial)
            : base(serial)
        {
            m_ItemID = 51;
        }
    }
}
