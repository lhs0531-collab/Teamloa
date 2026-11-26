using LKCamelot.library;
using LKCamelot.model;

namespace LKCamelot.script.item
{
    #region 1~5
    public class MentalityAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF MENTALITY"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 0; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 1 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public MentalityAmulet()
            : base(2)
        {
        }

        public MentalityAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 6~10
    public class MagicAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF MAGIC"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 30; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 6 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public MagicAmulet()
            : base(2)
        {
        }

        public MagicAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 11~15
    public class MindAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF MIND"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 40; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 11 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public MindAmulet()
            : base(2)
        {
        }

        public MindAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 16~20
    public class WitchcraftAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF WITCHCRAFT"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 60; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 16 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public WitchcraftAmulet()
            : base(2)
        {
        }

        public WitchcraftAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 21~25
    public class SoulAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF SOUL"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 70; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 21 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public SoulAmulet()
            : base(2)
        {
        }

        public SoulAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 26~30
    public class BlackartAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF BLACKART"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 80; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 26 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public BlackartAmulet()
            : base(2)
        {
        }

        public BlackartAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 31~35
    public class SpriteAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF SPRITE"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 90; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 31 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public SpriteAmulet()
            : base(2)
        {
        }

        public SpriteAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 2;
        }
    }
    #endregion

    #region 36~40
    public class MagicianAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("S-AMULET OF MAGICIAN"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 120; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 36 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public MagicianAmulet()
            : base(82)
        {
        }

        public MagicianAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 41~45
    public class SorcererAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("S-AMULET OF SORCERER"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int LevelReq { get { return 130; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 41 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public SorcererAmulet()
            : base(82)
        {
        }

        public SorcererAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 46~50
    public class ExorcistAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("H-AMULET OF EXORCIST"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 140; } }
        public override int MenBonus { get { return 46 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public ExorcistAmulet()
            : base(82)
        {
        }

        public ExorcistAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 51~55
    public class WitchAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF WITHCH"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 51 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public WitchAmulet()
            : base(82)
        {
        }

        public WitchAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 56~60
    public class WizardAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF WIZARD"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 56 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public WizardAmulet()
            : base(82)
        {
        }

        public WizardAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 61~65
    public class NecromancerAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF NECROMANCER"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 61 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public NecromancerAmulet()
            : base(82)
        {
        }

        public NecromancerAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 66~70
    public class ShamanAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF SHAMAN"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 66 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public ShamanAmulet()
            : base(82)
        {
        }

        public ShamanAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion

    #region 71~75
    public class ProphetAmulet : BaseAccessory
    {
        public override string Name { get { return string.Format("AMULET OF PROPHET"); } }
        public override AccessoryType Type { get { return AccessoryType.Amulet; } }
        public override int StrBonus { get { return 0; } }
        public override int MenBonus { get { return 71 + this.Stage; } }
        public override int DexBonus { get { return 0; } }
        public override int VitBonus { get { return 0; } }

        public override ulong BuyPrice { get { return 8000; } }

        public ProphetAmulet()
            : base(82)
        {
        }

        public ProphetAmulet(Serial serial)
            : base(serial)
        {
            m_ItemID = 82;
        }
    }
    #endregion
}
