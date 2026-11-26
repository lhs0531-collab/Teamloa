using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Opal : BaseOre
    {
        public override string Name { get { return "오팔"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 3600; } }

        public Opal()
            : base(59)
        {
        }

        public Opal(Serial serial)
            : base(serial)
        {
            m_ItemID = 59;
        }
    }
}
