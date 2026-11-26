using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class MoonStone : BaseOre
    {
        public override string Name { get { return "문스톤"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 1000; } }

        public MoonStone()
            : base(61)
        {
        }

        public MoonStone(Serial serial)
            : base(serial)
        {
            m_ItemID = 61;
        }
    }
}
