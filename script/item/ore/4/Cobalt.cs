using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Cobalt : BaseOre
    {
        public override string Name { get { return "코발트"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 1500; } }
        public override script.item.Item OreTaught { get { return new CobaltGem(); } }

        public Cobalt()
            : base(99)
        {
        }

        public Cobalt(Serial serial)
            : base(serial)
        {
            m_ItemID = 99;
        }
    }

    public class CobaltGem : BaseOre
    {
        public override string Name { get { return "코발트"; } }
        public override int XP { get { return 330; } }
        public override int SellPrice { get { return 1500; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public CobaltGem()
            : base(74)
        {

        }

        public CobaltGem(Serial serial)
            : base(serial)
        {
            m_ItemID = 74;
        }
    }
}
