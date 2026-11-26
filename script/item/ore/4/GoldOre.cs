using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class GoldOre : BaseOre
    {
        public override string Name { get { return "금"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 1300; } }
        public override script.item.Item OreTaught { get { return new GoldGem(); } }

        public GoldOre()
            : base(90)
        {
        }

        public GoldOre(Serial serial)
            : base(serial)
        {
            m_ItemID = 90;
        }
    }

    public class GoldGem : BaseOre
    {
        public override string Name { get { return "금"; } }
        public override int XP { get { return 330; } }
        public override int SellPrice { get { return 1300; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public GoldGem()
            : base(52)
        {

        }

        public GoldGem(Serial serial)
            : base(serial)
        {
            m_ItemID = 52;
        }
    }
}
