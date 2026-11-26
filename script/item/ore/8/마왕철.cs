using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class 마왕철 : BaseOre
    {
        public override string Name { get { return " 마왕철"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 4700; } }
        public override script.item.Item OreTaught { get { return new 마왕철Gem(); } }

        public 마왕철()
            : base(93)
        {
        }

        public 마왕철(Serial serial)
            : base(serial)
        {
            m_ItemID = 93;
        }
    }

    public class 마왕철Gem : BaseOre
    {
        public override string Name { get { return "마왕철"; } }
        public override int XP { get { return 330; } }
        public override int SellPrice { get { return 4700; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public 마왕철Gem()
            : base(62)
        {

        }

        public 마왕철Gem(Serial serial)
            : base(serial)
        {
            m_ItemID = 62;
        }
    }
}
