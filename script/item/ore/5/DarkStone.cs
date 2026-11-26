using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class DarkStone : BaseOre
    {
        public override string Name { get { return "다크스톤"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 1900; } }

        public DarkStone()
            : base(56)
        {
        }

        public DarkStone(Serial serial)
            : base(serial)
        {
            m_ItemID = 56;
        }
    }
}
