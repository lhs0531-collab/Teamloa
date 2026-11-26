using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Zirconium : BaseOre
    {
        public override string Name { get { return "지르코늄"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 1350; } }
        public override script.item.Item OreTaught { get { return new ZirconiumGem(); } }

        public Zirconium()
            : base(90)
        {
        }

        public Zirconium(Serial serial)
            : base(serial)
        {
            m_ItemID = 90;
        }
    }

    public class ZirconiumGem : BaseOre
    {
        public override string Name { get { return "지르코늄"; } }
        public override int XP { get { return 330; } }
        public override int SellPrice { get { return 1350; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public ZirconiumGem()
            : base(51)
        {

        }

        public ZirconiumGem(Serial serial)
            : base(serial)
        {
            m_ItemID = 51;
        }
    }
}
