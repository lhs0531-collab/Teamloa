using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.item
{
    public class Bone : Item
    {
        public override string Name { get { return "뼈다귀"; } }
        public override int SellPrice { get { return 100; } }

        public Bone()
            : base(124)
        {
        }

        public Bone(Serial serial)
            : base(serial)
        {
            m_ItemID = 124;
        }
    }
    public class 통뼈 : Item
    {
        public override string Name { get { return "통뼈"; } }
        public override ulong BuyPrice { get { return 100; } }
        public override int SellPrice { get { return 100; } }

        public 통뼈()
            : base(174)
        {
        }

        public 통뼈(Serial serial)
            : base(serial)
        {
            m_ItemID = 174;
        }
    }
}
