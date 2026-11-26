using LKCamelot.library;
using LKCamelot.model;

namespace LKCamelot.script.item
{
    #region 1~5
    public class VitalityAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF VITALITY"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 0; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 1 + this.Stage; } }
        
        public override ulong BuyPrice { get { return 8000; } }

        public VitalityAmulet()
            : base(2)
        {
        }

        public VitalityAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 6~10
    public class LifeAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF LIFE"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 30; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 6 + this.Stage; } }
        
        public override ulong BuyPrice { get { return 8000; } }

        public LifeAmulet()
            : base(2)
        {
        }

        public LifeAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 11~15
    public class FreshAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF FRESH"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 40; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 11 + this.Stage; } }
        
        public override ulong BuyPrice { get { return 8000; } }

        public FreshAmulet()
            : base(2)
        {
        }

        public FreshAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 16~20
    public class ActivityAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF ACTIVITY"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 60; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 16 + this.Stage; } }
        
        public override ulong BuyPrice { get { return 8000; } }

        public ActivityAmulet()
            : base(2)
        {
        }

        public ActivityAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 21~25
    public class StaminaAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF STAMINA"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 70; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 21 + this.Stage; } }
        
        public override ulong BuyPrice { get { return 8000; } }

        public StaminaAmulet()
            : base(2)
        {
        }

        public StaminaAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 26~30
    public class VigorAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF VIGOR"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 80; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 26 + this.Stage; } }
        
        

        public override ulong BuyPrice { get { return 8000; } }

        public VigorAmulet()
            : base(2)
        {
        }

        public VigorAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 31~35
    public class AbundanceAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF ABUNDANCE"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 90; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 31 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public AbundanceAmulet()
            : base(2)
        {
        }

        public AbundanceAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 36~40
    public class YoungsterAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("S-AMULET OF YOUNGSTER"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 120; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 36 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public YoungsterAmulet()
            : base(82)
        {
        }

        public YoungsterAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 41~45
    public class SeniroAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("S-AMULET OF SENIRO"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 130; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 41 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public SeniroAmulet()
            : base(2)
        {
        }

        public SeniroAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 46~50
    public class ToughguyAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("H-AMULET OF TOUGHGUY"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 140; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 46 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public ToughguyAmulet()
            : base(2)
        {
        }

        public ToughguyAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 51~55
    public class RecluseAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF RECLUSE"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 51 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public RecluseAmulet()
            : base(2)
        {
        }

        public RecluseAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 56~60
    public class LeaderAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF LEADER"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 56 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public LeaderAmulet()
            : base(2)
        {
        }

        public LeaderAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 61~65
    public class GuruAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF GURU"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 61 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public GuruAmulet()
            : base(2)
        {
        }

        public GuruAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 66~70
    public class TaoistAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF TAOIST"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 66 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public TaoistAmulet()
            : base(2)
        {
        }

        public TaoistAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 71~75
    public class HighlanderAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF HIGHLANDER"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 71 + this.Stage; } }

        public override ulong BuyPrice { get { return 8000; } }

        public HighlanderAmulet()
            : base(2)
        {
        }

        public HighlanderAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion
}
