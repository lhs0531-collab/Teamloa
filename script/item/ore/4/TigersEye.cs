using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class TigersEye : BaseOre
    {
        public override string Name { get { return "타이거스아이"; } }
        public override bool IsJewelry { get { return true; } }
        public override int SellPrice { get { return 1600; } }

        public TigersEye()
            : base(77)
        {
        }

        public TigersEye(Serial serial)
            : base(serial)
        {
            m_ItemID = 77;
        }
    }
}
