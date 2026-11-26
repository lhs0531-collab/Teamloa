using LKCamelot.library;
using LKCamelot.model;

namespace LKCamelot.script.item
{
    #region 1~5
    public class DexterityRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF DEXTERITY"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 0; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 1 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public DexterityRing()
            : base(1)
        {
        }

        public DexterityRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 6~10
    public class SkillRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF SKILL"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 30; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 6 + this.Stage; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public SkillRing()
            : base(1)
        {
        }

        public SkillRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 11~15
    public class TrainingRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF TRAINING"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 40; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 11 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public TrainingRing()
            : base(1)
        {
        }

        public TrainingRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 16~20
    public class TechniqueRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF TECHNIQUE"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 60; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 16 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public TechniqueRing()
            : base(1)
        {
        }

        public TechniqueRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 21~25
    public class AccuracyRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF ACCURACY"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 70; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 21 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public AccuracyRing()
            : base(1)
        {
        }

        public AccuracyRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 26~30
    public class ConcentrationRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF CONCENTRATION"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 80; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 26 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public ConcentrationRing()
            : base(1)
        {
        }

        public ConcentrationRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 31~35
    public class PerfectionRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF PERFECTION"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 90; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 31 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public PerfectionRing()
            : base(1)
        {
        }

        public PerfectionRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 36~40
    public class CraftsmanRing : BaseAccessory
    {
        public override string Name { get { return string.Format("S-RING OF CRAFTSMAN"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 120; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 36 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public CraftsmanRing()
            : base(83)
        {
        }

        public CraftsmanRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 41~45
    public class VeteranRing : BaseAccessory
    {
        public override string Name { get { return string.Format("S-RING OF VETERAN"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 130; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 41 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public VeteranRing()
            : base(83)
        {
        }

        public VeteranRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 46~50
    public class ExpertRing : BaseAccessory
    {
        public override string Name { get { return string.Format("H-RING OF EXPERT"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 140; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 46 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public ExpertRing()
            : base(83)
        {
        }

        public ExpertRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 51~55
    public class SpecialistRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF SPECIALIST"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 51 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public SpecialistRing()
            : base(83)
        {
        }

        public SpecialistRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 56~60
    public class MaestroRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF MAESTRO"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 56 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public MaestroRing()
            : base(83)
        {
        }

        public MaestroRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 61~65
    public class professionalRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF PROFESSIONAL"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 61 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public professionalRing()
            : base(83)
        {
        }

        public professionalRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 66~70
    public class MasterRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF MASTER"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 66 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public MasterRing()
            : base(83)
        {
        }

        public MasterRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 71~75
    public class AceRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF ACE"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 71 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public AceRing()
            : base(83)
        {
        }

        public AceRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion
}
