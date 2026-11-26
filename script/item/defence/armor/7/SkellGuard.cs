using LKCamelot.library;
using LKCamelot.model;

namespace LKCamelot.script.item
{
	public class SkellGuard : BaseArmor
	{
        public override string Name { get { return "SKELL GUARD"; } }

		public override int DamBase { get { return 0; } }
		public override int ACBase { get { return 222; } }

		public override int StrReq { get { return 406; } }
		public override int DexReq { get { return 305; } }

		public override int InitMinHits { get { return 1350; } }
		public override int InitMaxHits { get { return 1350; } }
        public override int SellPrice { get { return 95000; } }

        public override int APStage
        {
            get
            {
                var ret = 0;
                if (Parent != null)
                {
                    if ((Parent.Class & (Class.Swordsman | Class.Knight)) != 0)
                        ret = 3;
                    else if ((Parent.Class & (Class.Shaman | Class.Wizard)) != 0)
                        ret = 2;
                }
                return ret;
            }
        }

		public override Class ClassReq { get { return Class.Knight | Class.Swordsman | Class.Wizard | Class.Shaman; } }
		public override ArmorType ArmorType { get { return ArmorType.Armor; } }

		public SkellGuard() : base (7)
		{
		}

        public SkellGuard(Serial serial)
            : base(serial)
		{
            m_ItemID = 7;
		}
	}
}
