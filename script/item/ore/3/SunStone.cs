using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class SunStone : BaseOre
    {
        public override string Name { get { return "썬스톤"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 1200; } }

        public SunStone()
            : base(65)
        {
        }

        public SunStone(Serial serial)
            : base(serial)
        {
            m_ItemID = 65;
        }
    }
}
