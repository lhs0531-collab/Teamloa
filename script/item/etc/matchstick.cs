using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class 성냥 : Item
    {

        public override string Name { get { return "성냥"; } }
        public override ulong BuyPrice { get { return 100; } }


        public 성냥() : base(131)
        {
        }

        public 성냥(Serial serial)
            : base(serial)
        {
            m_ItemID = 131;
        }
    }
}
