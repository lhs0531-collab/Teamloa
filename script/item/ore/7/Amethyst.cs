using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Amethyst : BaseOre
    {
        public override string Name { get { return "아메시스트"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 2600; } }

        public Amethyst()
            : base(66)
        {
        }

        public Amethyst(Serial serial)
            : base(serial)
        {
            m_ItemID = 66;
        }
    }
}
