using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Crystal : BaseOre
    {
        public override string Name { get { return "크리스탈"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 950; } }

        public Crystal()
            : base(76)
        {
        }

        public Crystal(Serial serial)
            : base(serial)
        {
            m_ItemID = 76;
        }
    }
}
