using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Aluminium : BaseOre
    {
        public override string Name { get { return "알루미늄"; } }
        public override bool IsJewelry { get { return false; } }
        public override int SellPrice { get { return 450; } }
        public override script.item.Item OreTaught { get { return new AluminiumGem(); } }

        public Aluminium()
            : base(90)
        {
        }

        public Aluminium(Serial serial)
            : base(serial)
        {
            m_ItemID = 90;
        }
    }
    public class AluminiumGem : BaseOre
    {
        public override string Name { get { return "알루미늄"; } }
        public override int XP { get { return 13; } }
        public override int SellPrice { get { return 450; } }
        public override bool Stackable { get { return true; } }
        public override bool IsJewelry { get { return true; } }

        public AluminiumGem() : base(64)
        {

        }

        public AluminiumGem(Serial serial)
            : base(serial)
        {
            m_ItemID = 64;
        }
    }

}
