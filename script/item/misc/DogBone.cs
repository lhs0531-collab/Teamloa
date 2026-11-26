using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.item
{
    public class DogBone : Item
    {
        public override string Name { get { return "뼈다귀"; } }
        public override int SellPrice { get { return 10; } }
        public override bool Stackable { get { return true; } }

        public DogBone()
            : base(124)
        {
        }

        public DogBone(Serial serial)
            : base(serial)
        {
            m_ItemID = 124;
        }
    }
}
