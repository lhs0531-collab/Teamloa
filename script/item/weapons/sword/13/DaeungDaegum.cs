using LKCamelot.library;
using LKCamelot.model;

namespace LKCamelot.script.item
{
    public class DaeungDaegum : BaseWeapon
    {
        public override string Name { get { return "Daeung Daegum"; } }

        public override int DamBase { get { return 350; } }
        public override int ACBase { get { return 0; } }

        public override int StrReq { get { return 4000; } }
        public override int DexReq { get { return 4000; } }
        public override int MenReq { get { return 0; } }

        public override int InitMinHits { get { return 1200; } }
        public override int InitMaxHits { get { return 1200; } }

        public override int LevelReq { get { return 160; } }

        public override Class ClassReq { get { return Class.Swordsman; } }
        public override WeaponType WeaponType { get { return WeaponType.Sword; } }

        public DaeungDaegum()
            : base(13)
        {
        }

        public DaeungDaegum(Serial serial)
            : base(serial)
        {
            m_ItemID = 13;
        }
    }
}
