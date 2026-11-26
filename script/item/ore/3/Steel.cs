using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Steel : BaseOre
    {
        public override string Name { get { return "강철"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 1050; } }
        public override script.item.Item OreTaught { get { return new SteelGem(); } }

        public Steel()
            : base(90)
        {
        }

        public Steel(Serial serial)
            : base(serial)
        {
            m_ItemID = 90;
        }
    }

    public class SteelGem : BaseOre
    {
        public override string Name { get { return "강철"; } }
        public override int XP { get { return 160; } }
        public override int SellPrice { get { return 1050; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public SteelGem()
            : base(51)
        {

        }

        public SteelGem(Serial serial)
            : base(serial)
        {
            m_ItemID = 51;
        }
    }
}
