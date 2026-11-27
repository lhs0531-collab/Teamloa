using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    class 화강석 : Item
    {
        public override string Name { get { return "화강석"; } }
        public override bool Stackable { get { return true; } }
        public override ulong BuyPrice { get { return 100; } }
        public override short Limit { get { return 1000; } }

        public 화강석()
            : base(130)
        {

        }

        public 화강석(Serial serial)
            : base(serial)
        {
            m_ItemID = 130;
        }
    }
    public class 대리석 : Item
    {
        public override string Name { get { return "대리석"; } }
        public override bool Stackable { get { return true; } }
        public override ulong BuyPrice { get { return 100; } }
        public override short Limit { get { return 1000; } }

        public 대리석()
            : base(121)
        {

        }

        public 대리석(Serial serial)
            : base(serial)
        {
            m_ItemID = 121;
        }
    }
}
