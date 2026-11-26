using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class LeopardJesper : BaseOre
    {
        public override string Name { get { return "레오파드제스퍼"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 1400; } }

        public LeopardJesper()
            : base(57)
        {
        }

        public LeopardJesper(Serial serial)
            : base(serial)
        {
            m_ItemID = 57;
        }
    }
}
