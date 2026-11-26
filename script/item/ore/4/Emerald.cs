using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Emerald : BaseOre
    {
        public override string Name { get { return "에메랄드"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 1450; } }

        public Emerald()
            : base(68)
        {
        }

        public Emerald(Serial serial)
            : base(serial)
        {
            m_ItemID = 68;
        }
    }
}
