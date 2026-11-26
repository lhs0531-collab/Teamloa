using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Alloy : BaseOre
    {
        public override string Name { get { return "합금"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 2500 ; } }
        public override script.item.Item OreTaught { get { return new AlloyGem(); } }

        public Alloy()
            : base(93)
        {
        }

        public Alloy(Serial serial)
            : base(serial)
        {
            m_ItemID = 93;
        }
    }

    public class AlloyGem : BaseOre
    {
        public override string Name { get { return "합금"; } }
        public override int XP { get { return 330; } }
        public override int SellPrice { get { return 2500; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public AlloyGem()
            : base(62)
        {

        }

        public AlloyGem(Serial serial)
            : base(serial)
        {
            m_ItemID = 62;
        }
    }
}
