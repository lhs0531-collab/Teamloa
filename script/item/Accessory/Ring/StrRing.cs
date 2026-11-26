using LKCamelot.library;
using LKCamelot.model;

namespace LKCamelot.script.item
{
    #region 1~5
    public class StrengthRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF STRENGTH"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 0; } }
        public override int StrBonus { get { return 1 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public StrengthRing()
            : base(1)
        {
        }

        public StrengthRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 6~10
    public class HealthRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF HEALTH"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 30; } }
        public override int StrBonus { get { return 6 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public HealthRing()
            : base(1)
        {
        }

        public HealthRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 11~15
    public class PowerRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF POWER"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 40; } }
        public override int StrBonus { get { return 11 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public PowerRing()
            : base(1)
        {
        }

        public PowerRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 16~20
    public class EnergeRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF ENERGE"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 60; } }
        public override int StrBonus { get { return 16 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public EnergeRing()
            : base(1)
        {
        }

        public EnergeRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 21~25
    public class ForceRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF FORCE"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 70; } }
        public override int StrBonus { get { return 21 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public ForceRing()
            : base(1)
        {
        }

        public ForceRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 26~30
    public class VictoryRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF VICTORY"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 80; } }
        public override int StrBonus { get { return 26 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public VictoryRing()
            : base(1)
        {
        }

        public VictoryRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 31~35
    public class MightRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF MIGHT"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 90; } }
        public override int StrBonus { get { return 31 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public MightRing()
            : base(1)
        {
        }

        public MightRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 36~40
    public class BarbarrianRing : BaseAccessory
    {
        public override string Name { get { return string.Format("S-RING OF BARBARIAN"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 120; } }
        public override int StrBonus { get { return 36 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public BarbarrianRing()
            : base(83)
        {
        }

        public BarbarrianRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 41~45
    public class FighterRing : BaseAccessory
    {
        public override string Name { get { return string.Format("S-RING OF FIGHTER"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 130; } }
        public override int StrBonus { get { return 41 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public FighterRing()
            : base(83)
        {
        }

        public FighterRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 46~50
    public class WarriorRing : BaseAccessory
    {
        public override string Name { get { return string.Format("H-RING OF WARRIOR"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 140; } }
        public override int StrBonus { get { return 46 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public WarriorRing()
            : base(83)
        {
        }

        public WarriorRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 51~55
    public class BraveRing1 : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF BRAVE"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 51 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public BraveRing1()
            : base(83)
        {
            
        }

        public BraveRing1(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 56~60
    public class ChampionRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF CHAMPION"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 56 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public ChampionRing()
            : base(83)
        {
        }

        public ChampionRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 61~65
    public class SwordmanRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF SWORDMAN"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 61 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public SwordmanRing()
            : base(83)
        {
        }

        public SwordmanRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 66~70
    public class KnightRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF KNIGHT"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 66 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public KnightRing()
            : base(83)
        {
        }

        public KnightRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 71~75
    public class ValorRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF VALOR"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 71 + this.Stage; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public ValorRing()
            : base(83)
        {
        }

        public ValorRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion
}
