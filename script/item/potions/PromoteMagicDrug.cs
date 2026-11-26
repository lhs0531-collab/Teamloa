using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.item
{
    public class PromoteMagicDrug : BasePotion
    {
        public override string Name { get { return "PROMOTE MAGIC DRUG"; } }
        public override ulong BuyPrice { get { return 50; } }

        public PromoteMagicDrug()
            : base(21)
        {
        }

        public PromoteMagicDrug(Serial serial)
            : base(serial)
        {
            m_ItemID = 21;
        }
    }
}
