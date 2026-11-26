using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.item
{
    public class PromoteLifeDrug : BasePotion
    {
        public override string Name { get { return "PROMOTE LIFE DRUG"; } }
        public override ulong BuyPrice { get { return 50; } }

        public PromoteLifeDrug()
            : base(20)
        {
        }

        public PromoteLifeDrug(Serial serial)
            : base(serial)
        {
            m_ItemID = 20;
        }
    }
}
