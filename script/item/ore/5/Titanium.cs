using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Titanium : BaseOre
    {
        public override string Name { get { return "티타늄"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 1700; } }
        public override script.item.Item OreTaught { get { return new TitaniumGem(); } }

        public Titanium()
            : base(90)
        {
        }

        public Titanium(Serial serial)
            : base(serial)
        {
            m_ItemID = 90;
        }
    }

    public class TitaniumGem : BaseOre
    {
        public override string Name { get { return "티타늄"; } }
        public override int XP { get { return 420; } }
        public override int SellPrice { get { return 1700; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public TitaniumGem()
            : base(51)
        {

        }

        public TitaniumGem(Serial serial)
            : base(serial)
        {
            m_ItemID = 51;
        }
    }
}
