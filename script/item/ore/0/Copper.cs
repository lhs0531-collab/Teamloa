using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Copper : BaseOre
    {
        public override string Name { get { return "구리"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 350; } }
        public override script.item.Item OreTaught { get { return new CopperGem(); } }

        public Copper()
            : base(90)
        {
        }

        public Copper(Serial serial)
            : base(serial)
        {
            m_ItemID = 90;
        }
    }

    public class CopperGem : BaseOre
    {
        public override string Name { get { return "구리"; } }
        public override int XP { get { return 6; } }
        public override int SellPrice { get { return 350; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public CopperGem()
            : base(79)
        {

        }

        public CopperGem(Serial serial)
            : base(serial)
        {
            m_ItemID = 79;
        }
    }
}
