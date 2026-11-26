using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Garnet : BaseOre
    {
        public override string Name { get { return "가넷"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 1100; } }

        public Garnet()
            : base(50)
        {
        }

        public Garnet(Serial serial)
            : base(serial)
        {
            m_ItemID = 50;
        }
    }
}
