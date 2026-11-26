using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class SuperAlloy : BaseOre
    {
        public override string Name { get { return "초합금"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 3700; } }
        public override script.item.Item OreTaught { get { return new SuperAlloyGem(); } }

        public SuperAlloy()
            : base(93)
        {
        }

        public SuperAlloy(Serial serial)
            : base(serial)
        {
            m_ItemID = 93;
        }
    }

    public class SuperAlloyGem : BaseOre
    {
        public override string Name { get { return "초합금"; } }
        public override int XP { get { return 330; } }
        public override int SellPrice { get { return 3700; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public SuperAlloyGem()
            : base(62)
        {

        }

        public SuperAlloyGem(Serial serial)
            : base(serial)
        {
            m_ItemID = 62;
        }
    }
}
