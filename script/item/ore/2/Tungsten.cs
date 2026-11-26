using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Tungsten : BaseOre
    {
        public override string Name { get { return "텅스텐"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 850; } }
        public override script.item.Item OreTaught { get { return new TungstenGem(); } }

        public Tungsten()
            : base(90)
        {
        }

        public Tungsten(Serial serial)
            : base(serial)
        {
            m_ItemID = 90;
        }
    }

    public class TungstenGem : BaseOre
    {
        public override string Name { get { return "텅스텐"; } }
        public override int XP { get { return 60; } }
        public override int SellPrice { get { return 850; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public TungstenGem()
            : base(51)
        {

        }

        public TungstenGem(Serial serial)
            : base(serial)
        {
            m_ItemID = 51;
        }
    }
}
