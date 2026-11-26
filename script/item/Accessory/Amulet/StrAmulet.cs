using LKCamelot.library;
using LKCamelot.model;

namespace LKCamelot.script.item
{
    #region 1~5
    public class StrengthAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF STRENGTH"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 0; } }
        public override int StrBonus { get { return 1 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public StrengthAmulet()
            : base(2)
        {
        }

        public StrengthAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 6~10
    public class HealthAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF HEALTH"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 30; } }
        public override int StrBonus { get { return 6 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public HealthAmulet()
            : base(2)
        {
        }

        public HealthAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 11~15
    public class PowerAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF POWER"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 40; } }
        public override int StrBonus { get { return 11 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public PowerAmulet()
            : base(2)
        {
        }

        public PowerAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 16~20
    public class EnergeAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF ENERGE"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 60; } }
        public override int StrBonus { get { return 16 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public EnergeAmulet()
            : base(2)
        {
        }

        public EnergeAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 21~25
    public class ForceAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF FORCE"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 7; } }
        public override int StrBonus { get { return 21 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public ForceAmulet()
            : base(2)
        {
        }

        public ForceAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 26~30
    public class VictoryAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF VICTORY"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 80; } }
        public override int StrBonus { get { return 26 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public VictoryAmulet()
            : base(2)
        {
        }

        public VictoryAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 31~35
    public class MightAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF MIGHT"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 90; } }
        public override int StrBonus { get { return 31 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public MightAmulet()
            : base(2)
        {
        }

        public MightAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 36~40
    public class BarbarrianAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("S-AMULET OF BARBARIAN"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 120; } }
        public override int StrBonus { get { return 36 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public BarbarrianAmulet()
            : base(82)
        {
        }

        public BarbarrianAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 41~45
    public class FighterAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("S-AMULET OF FIGHTER"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 130; } }
        public override int StrBonus { get { return 41 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public FighterAmulet()
            : base(82)
        {
        }

        public FighterAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 46~50
    public class WarriorAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("H-AMULET OF WARRIOR"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 140; } }
        public override int StrBonus { get { return 46 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public WarriorAmulet()
            : base(82)
        {
        }

        public WarriorAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 51~55
    public class BraveAmulet1 : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF BRAVE"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 51 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public BraveAmulet1()
            : base(82)
        {
            int Ran = Util.Random(0,1000);
        }

        public BraveAmulet1(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 56~60
    public class ChampionAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF CHAMPION"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 56 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public ChampionAmulet()
            : base(82)
        {
        }

        public ChampionAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 61~65
    public class SwordmanAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF SWORDMAN"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 61 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public SwordmanAmulet()
            : base(82)
        {
        }

        public SwordmanAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 66~70
    public class KnightAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF KNIGHT"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 66 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public KnightAmulet()
            : base(82)
        {
        }

        public KnightAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 71~75
    public class ValorAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF VALOR"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 71 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public ValorAmulet()
            : base(82)
        {
        }

        public ValorAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion
}
