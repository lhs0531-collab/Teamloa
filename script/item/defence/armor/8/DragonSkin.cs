using LKCamelot.library;
using LKCamelot.model;

namespace LKCamelot.script.item
{
	public class DragonSkin : BaseArmor
	{
		public override string Name { get { return "Dragon Skin"; } }

		public override int DamBase { get { return 0; } }
		public override int ACBase { get { return 250; } }

        public override int StrReq { get { return 925; } }
        public override int MenReq { get { return 450; } }
		public override int DexReq { get { return 752; } }
        public override int LevelReq { get { return 110; } }

		public override int InitMinHits { get { return 1500; } }
		public override int InitMaxHits { get { return 1500; } }

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

        public override int SellPrice { get { return 100000; } } 

		public override Class ClassReq { get { return Class.Swordsman | Class.Shaman; } }
		public override ArmorType ArmorType { get { return ArmorType.Armor; } }

		public DragonSkin() : base (8)
		{
		}

		public DragonSkin(Serial serial) : base (serial)
		{
            m_ItemID = 8;
		}
	}
}