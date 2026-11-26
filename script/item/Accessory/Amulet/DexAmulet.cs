using LKCamelot.library;
using LKCamelot.model;

namespace LKCamelot.script.item
{
    #region 1~5
    public class DexterityAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF DEXTERITY"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 0; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 1 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public DexterityAmulet()
            : base(2)
        {
        }

        public DexterityAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 6~10
    public class SkillAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF SKILL"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 30; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 6 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public SkillAmulet()
            : base(2)
        {
        }

        public SkillAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 11~15
    public class TrainingAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF TRAINING"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 40; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 11 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public TrainingAmulet()
            : base(2)
        {
        }

        public TrainingAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 16~20
    public class TechniqueAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF TECHNIQUE"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 60; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 16 + this.Stage; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public TechniqueAmulet()
            : base(2)
        {
        }

        public TechniqueAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 21~25
    public class AccuracyAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF ACCURACY"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 70; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 21 + this.Stage; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public AccuracyAmulet()
            : base(2)
        {
        }

        public AccuracyAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 26~30
    public class ConcentrationAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF CONCENTRATION"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 80; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 26 + this.Stage; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public ConcentrationAmulet()
            : base(2)
        {
        }

        public ConcentrationAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 31~35
    public class PerfectionAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF PERFECTION"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 90; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 31 + this.Stage; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public PerfectionAmulet()
            : base(2)
        {
        }

        public PerfectionAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 36~40
    public class CraftsmanAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("S-AMULET OF CRAFTSMAN"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 120; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 36 + this.Stage; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public CraftsmanAmulet()
            : base(82)
        {
        }

        public CraftsmanAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 41~45
    public class VeteranAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("S-AMULET OF VETERAN"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 130; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 41 + this.Stage; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public VeteranAmulet()
            : base(82)
        {
        }

        public VeteranAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 46~50
    public class ExpertAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("H-AMULET OF EXPERT"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 140; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 46 + this.Stage; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public ExpertAmulet()
            : base(82)
        {
        }

        public ExpertAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 51~55
    public class SpecialistAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF SPECIALIST"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 51 + this.Stage; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public SpecialistAmulet()
            : base(82)
        {
        }

        public SpecialistAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 56~60
    public class MaestroAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF MAESTRO"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 56 + this.Stage; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public MaestroAmulet()
            : base(82)
        {
        }

        public MaestroAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 61~65
    public class professionalAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF PROFESSIONAL"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 61 + this.Stage; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public professionalAmulet()
            : base(82)
        {
        }

        public professionalAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 66~70
    public class MasterAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF MASTER"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 66 + this.Stage; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public MasterAmulet()
            : base(82)
        {
        }

        public MasterAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 71~75
    public class AceAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF ACE"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 0; } }
        public override int DexBonus { get { return 71 + this.Stage; } }
        public override int VitBonus { get { return 0; } }


        public override ulong BuyPrice { get { return 8000; } }

        public AceAmulet()
            : base(82)
        {
        }

        public AceAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion
}
