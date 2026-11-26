using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class UGloryRing : BaseAccessory
    {
        public override string Name { get { return "U-RING OF GLORY"; } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 30; } }
        public override int InitMaxHits { get { return 1000; } }
        public override int InitMinHits { get { return 1000; } }
        public override int StrBonus { get { return 55; } }
        public override int MenBonus { get { return 55; } }
        public override int DexBonus { get { return 55; } }
        public override int VitBonus { get { return 55; } }

        public override ulong BuyPrice { get { return 8000; } }

        public UGloryRing()
            : base(45)
        {
        }

        public UGloryRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 45;
        }
    }
    public class UGloryAmulet : BaseAccessory
    {
        public override string Name { get { return "U-AMULET OF GLORY"; } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 30; } }
        public override int InitMaxHits { get { return 1000; } }
        public override int InitMinHits { get { return 1000; } }
        public override int StrBonus { get { return 55; } }
        public override int MenBonus { get { return 55; } }
        public override int DexBonus { get { return 55; } }
        public override int VitBonus { get { return 55; } }

        public override ulong BuyPrice { get { return 8000; } }

        public UGloryAmulet()
            : base(47)
        {
        }

        public UGloryAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 47;
        }
    }
}
