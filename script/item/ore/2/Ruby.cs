using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Ruby : BaseOre
    {
        public override string Name { get { return "루비"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 900; } }

        public Ruby()
            : base(58)
        {
        }

        public Ruby(Serial serial)
            : base(serial)
        {
            m_ItemID = 58;
        }
    }
}
