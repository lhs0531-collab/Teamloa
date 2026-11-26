using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Lead : BaseOre
    {
        public override string Name { get { return "납"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 400; } }
        public override script.item.Item OreTaught { get { return new LeadGem(); } }

        public Lead()
            : base(90)
        {
        }

        public Lead(Serial serial)
            : base(serial)
        {
            m_ItemID = 90;
        }
    }
    public class LeadGem : BaseOre
    {
        public override string Name { get { return "납"; } }
        public override int XP { get { return 9; } }
        public override int SellPrice { get { return 400; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public LeadGem()
            : base(51)
        {

        }

        public LeadGem(Serial serial)
            : base(serial)
        {
            m_ItemID = 51;
        }
    }
}
