using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Cadmium : BaseOre
    {
        public override string Name { get { return "카드뮴"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 700; } }
        public override script.item.Item OreTaught { get { return new CadmiumGem(); } }

        public Cadmium()
            : base(90)
        {
        }

        public Cadmium(Serial serial)
            : base(serial)
        {
            m_ItemID = 90;
        }
    }
    public class CadmiumGem : BaseOre
    {
        public override string Name { get { return "카드뮴"; } }
        public override int XP { get { return 32; } }
        public override int SellPrice { get { return 700; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public CadmiumGem()
            : base(72)
        {

        }

        public CadmiumGem(Serial serial)
            : base(serial)
        {
            m_ItemID = 72;
        }
    }
}
