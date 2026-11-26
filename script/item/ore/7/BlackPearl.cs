using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class BlackPearl : BaseOre
    {
        public override string Name { get { return "흑진주"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 2900; } }

        public BlackPearl()
            : base(80)
        {
        }

        public BlackPearl(Serial serial)
            : base(serial)
        {
            m_ItemID = 80;
        }
    }
}
