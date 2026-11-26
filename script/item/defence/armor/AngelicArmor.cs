using LKCamelot.library;
using LKCamelot.model;

namespace LKCamelot.script.item
{
    public class AngelicArmor : BaseArmor
    {
        public override string Name { get { return "ANGELIC ARMOR"; } }

        public override int DamBase { get { return 0; } }
        public override int ACBase { get { return 0; } }

        public override int DexBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }

        public override int StrReq { get { return 0; } }
        public override int DexReq { get { return 0; } }
        public override int MenReq { get { return 0; } }


        public override int SellPrice { get { return 500000; } } 

        public override int APStage
        {
            get
            {
                var ret = 3;
                if (Parent != null)
                {
                    if ((Parent.Class & (Class.Swordsman | Class.Knight)) != 0)
                        ret = 4;
                    else if ((Parent.Class & (Class.Shaman | Class.Wizard)) != 0)
                        ret = 3;
                }
                return ret;
            }
        }

        public override int LevelReq { get { return 240; } }
        public override Class ClassReq { get { return Class.Wizard | Class.Shaman; } }
        public override ArmorType ArmorType { get { return ArmorType.Armor; } }

        public AngelicArmor()
            : base(8)
        {
        }

        public AngelicArmor(Serial serial)
            : base(serial)
        {
            m_ItemID = 8;
        }
    }
} 