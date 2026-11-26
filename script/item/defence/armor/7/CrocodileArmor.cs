
using LKCamelot.library;
using LKCamelot.model;

namespace LKCamelot.script.item
{
	public class CrocodileArmor : BaseArmor
	{
		public override string Name { get { return "Crocodile Armor"; } }

		public override int DamBase { get { return 0; } }
		public override int ACBase { get { return 216; } }

		public override int StrReq { get { return 416; } }
		public override int DexReq { get { return 312; } }

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

		public override Class ClassReq { get { return Class.Knight | Class.Swordsman; } }
		public override ArmorType ArmorType { get { return ArmorType.Armor; } }

		public CrocodileArmor() : base (7)
		{
		}

        public CrocodileArmor(Serial serial)
            : base(serial)
		{
            m_ItemID = 7;
		}
	}
}
