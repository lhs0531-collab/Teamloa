using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.item
{
    public class Iron : BaseOre
    {
        public override string Name { get { return "철"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 300; } }
        public override script.item.Item OreTaught { get { return new IronGem(); } }

        public Iron()
            : base(90)
        {
        }

        public Iron(Serial serial)
            : base(serial)
        {
            m_ItemID = 90;
        }
    }

    public class IronGem : BaseOre
    {
        public override string Name { get { return "철"; } }
        public override int XP { get { return 5; } }
        public override int SellPrice { get { return 300; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public IronGem()
            : base(72)
        {

        }

        public IronGem(Serial serial)
            : base(serial)
        {
            m_ItemID = 72;
        }
    }
}
