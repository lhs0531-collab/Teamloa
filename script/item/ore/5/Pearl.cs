using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Pearl : BaseOre
    {
        public override string Name { get { return "진주"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 1800; } }

        public Pearl()
            : base(80)
        {
        }

        public Pearl(Serial serial)
            : base(serial)
        {
            m_ItemID = 80;
        }
    }
}
