using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class 헬리도오 : BaseOre
    {
        public override string Name { get { return "헬리도오"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 1750; } }

        public 헬리도오()
            : base(81)
        {
        }

        public 헬리도오(Serial serial)
            : base(serial)
        {
            m_ItemID = 81;
        }
    }
}
