using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class 짱돌 : BaseOre
    {
        public override string Name { get { return "짱돌"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 500; } }

        public 짱돌()
            : base(129)
        {
        }

        public 짱돌(Serial serial)
            : base(serial)
        {
            m_ItemID = 129;
        }
    }
}
