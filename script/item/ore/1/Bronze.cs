using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Bronze : BaseOre
    {
        public override string Name { get { return "청동"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 600; } }

        public Bronze()
            : base(79)
        {
        }

        public Bronze(Serial serial)
            : base(serial)
        {
            m_ItemID = 79;
        }
    }

}
