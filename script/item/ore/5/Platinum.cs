using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Platinum : BaseOre
    {
        public override string Name { get { return "백금"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 1950; } }
        public override script.item.Item OreTaught { get { return new PlatinumGem(); } }

        public Platinum()
            : base(93)
        {
        }

        public Platinum(Serial serial)
            : base(serial)
        {
            m_ItemID = 93;
        }
    }

    public class PlatinumGem : BaseOre
    {
        public override string Name { get { return "백금"; } }
        public override int XP { get { return 420; } }
        public override int SellPrice { get { return 1950; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public PlatinumGem()
            : base(62)
        {

        }

        public PlatinumGem(Serial serial)
            : base(serial)
        {
            m_ItemID = 62;
        }
    }
}
