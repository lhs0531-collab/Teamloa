using LKCamelot.library;
using LKCamelot.model;

namespace LKCamelot.script.item
{
    #region 1~5
    public class VitalityRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF VITALITY"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 0; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 1 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public VitalityRing()
            : base(1)
        {
        }

        public VitalityRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 6~10
    public class LifeRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF LIFE"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 30; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 6 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public LifeRing()
            : base(1)
        {
        }

        public LifeRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 11~15
    public class FreshRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF FRESH"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 40; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 11 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public FreshRing()
            : base(1)
        {
        }

        public FreshRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 16~20
    public class ActivityRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF ACTIVITY"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 60; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 16 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public ActivityRing()
            : base(1)
        {
        }

        public ActivityRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 21~25
    public class StaminaRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF STAMINA"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 70; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 21 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public StaminaRing()
            : base(1)
        {
        }

        public StaminaRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 26~30
    public class VigorRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF VIGOR"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 80; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 26 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public VigorRing()
            : base(1)
        {
        }

        public VigorRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 31~35
    public class AbundanceRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF ABUNDANCE"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 90; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 31 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public AbundanceRing()
            : base(1)
        {
        }

        public AbundanceRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 36~40
    public class YoungsterRing : BaseAccessory
    {
        public override string Name { get { return string.Format("S-RING OF YOUNGSTER"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 120; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 36 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public YoungsterRing()
            : base(83)
        {
        }

        public YoungsterRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 41~45
    public class SeniroRing : BaseAccessory
    {
        public override string Name { get { return string.Format("S-RING OF SENIRO"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 130; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 41 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public SeniroRing()
            : base(83)
        {
        }

        public SeniroRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 46~50
    public class ToughguyRing : BaseAccessory
    {
        public override string Name { get { return string.Format("H-RING OF TOUGHGUY"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 140; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 46 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public ToughguyRing()
            : base(83)
        {
        }

        public ToughguyRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 51~55
    public class RecluseRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF RECLUSE"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 51 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public RecluseRing()
            : base(83)
        {
        }

        public RecluseRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 56~60
    public class LeaderRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF LEADER"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 56 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public LeaderRing()
            : base(83)
        {
        }

        public LeaderRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 61~65
    public class GuruRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF GURU"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 61 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public GuruRing()
            : base(83)
        {
        }

        public GuruRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 66~70
    public class TaoistRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF TAOIST"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 66 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public TaoistRing()
            : base(83)
        {
        }

        public TaoistRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 71~75
    public class HighlanderRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF HIGHLANDER"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 71 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public HighlanderRing()
            : base(83)
        {
        }

        public HighlanderRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion
}
