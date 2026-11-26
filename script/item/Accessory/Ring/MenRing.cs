using LKCamelot.library;
using LKCamelot.model;

namespace LKCamelot.script.item
{
    #region 1~5
    public class MentalityRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF MENTALITY"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 0; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 1 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public MentalityRing()
            : base(1)
        {
        }

        public MentalityRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 6~10
    public class MagicRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF MAGIC"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 30; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 6 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public MagicRing()
            : base(1)
        {
        }

        public MagicRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 11~15
    public class MindRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF MIND"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 40; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 11 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public MindRing()
            : base(1)
        {
        }

        public MindRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 16~20
    public class WitchcraftRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF WITCHCRAFT"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 60; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 16 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public WitchcraftRing()
            : base(1)
        {
        }

        public WitchcraftRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 21~25
    public class SoulRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF SOUL"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 70; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 21 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public SoulRing()
            : base(1)
        {
        }

        public SoulRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 26~30
    public class BlackartRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF BLACKART"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 80; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 26 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public BlackartRing()
            : base(1)
        {
        }

        public BlackartRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 31~35
    public class SpriteRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF SPRITE"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 90; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 31 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public SpriteRing()
            : base(1)
        {
        }

        public SpriteRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 1;
        }
    }
    #endregion

    #region 36~40
    public class MagicianRing : BaseAccessory
    {
        public override string Name { get { return string.Format("S-RING OF MAGICIAN"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 120; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 36 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public MagicianRing()
            : base(83)
        {
        }

        public MagicianRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 41~45
    public class SorcererRing : BaseAccessory
    {
        public override string Name { get { return string.Format("S-RING OF SORCERER"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 130; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 41 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public SorcererRing()
            : base(83)
        {
        }

        public SorcererRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 46~50
    public class ExorcistRing : BaseAccessory
    {
        public override string Name { get { return string.Format("H-RING OF EXORCIST"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int LevelReq { get { return 140; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 46 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public ExorcistRing()
            : base(83)
        {
        }

        public ExorcistRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 51~55
    public class WitchRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF WITHCH"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 51 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public WitchRing()
            : base(83)
        {
        }

        public WitchRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 56~60
    public class WizardRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF WIZARD"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 56 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public WizardRing()
            : base(83)
        {
        }

        public WizardRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 61~65
    public class NecromancerRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF NECROMANCER"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 61 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public NecromancerRing()
            : base(83)
        {
        }

        public NecromancerRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 66~70
    public class ShamanRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF SHAMAN"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 66 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public ShamanRing()
            : base(83)
        {
        }

        public ShamanRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion

    #region 71~75
    public class ProphetRing : BaseAccessory
    {
        public override string Name { get { return string.Format("RING OF PROPHET"); } }
        public override AccessoryType Type { get { return AccessoryType.Ring; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 71 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public ProphetRing()
            : base(83)
        {
        }

        public ProphetRing(Serial serial)
            : base(serial)
        {
            m_ItemID = 83;
        }
    }
    #endregion
}
