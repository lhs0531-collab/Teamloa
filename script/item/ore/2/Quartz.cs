using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Quartz : BaseOre
    {
        public override string Name { get { return "석영"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 800; } }

        public Quartz()
            : base(75)
        {
        }

        public Quartz(Serial serial)
            : base(serial)
        {
            m_ItemID = 75;
        }
    }
}
