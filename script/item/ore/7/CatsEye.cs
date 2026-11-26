using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class CatsEye : BaseOre
    {
        public override string Name { get { return "캣츠아이"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 2700; } }

        public CatsEye()
            : base(54)
        {
        }

        public CatsEye(Serial serial)
            : base(serial)
        {
            m_ItemID = 54;
        }
    }
}
