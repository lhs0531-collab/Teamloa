using LKCamelot.library;
using LKCamelot.model;

namespace LKCamelot.script.item
{
    public class DarkPlate : BaseArmor
    {
        public override string Name { get { return "Dark Plate"; } }

        public override int DamBase { get { return 0; } }
        public override int ACBase { get { return 300; } }

        public override int StrReq { get { return 0; } }
        public override int DexReq { get { return 0; } }

        public override int InitMinHits { get { return 300; } }
        public override int InitMaxHits { get { return 300; } }

        public override int SellPrice { get { return 200000; } } 
        public override int APStage { get { return 4; } }

        public override int LevelReq { get { return 100; } }
        public override Class ClassReq { get { return Class.Swordsman; } }
        public override ArmorType ArmorType { get { return ArmorType.Armor; } }

        public DarkPlate()
            : base(8)
        {
        }

        public DarkPlate(Serial serial)
            : base(serial)
        {
            m_ItemID = 8;
        }
    }
}
