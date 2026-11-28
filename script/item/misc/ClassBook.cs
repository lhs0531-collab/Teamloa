using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class ShamanBook : Item
    {
        public override string Name { get { return "BOOK OF SHAMAN"; } }
        public override ulong BuyPrice { get { return Settings.ClassBookPrice; } }
        public override int SellPrice { get { return 1; } }

        public ShamanBook()
            : base(3)
        {
        }

        public ShamanBook(Serial serial)
            : base(serial)
        {
            m_ItemID = 3;
        }
        public override void Use(Player player)
        {
            if (player.Promo < 1)
                return;

            this.Delete(player);
            player.ChangeClassByBook(LKCamelot.library.Class.Shaman);
        }
    }
    public class WizardBook : Item
    {
        public override string Name { get { return "BOOK OF WIZARD"; } }
        public override ulong BuyPrice { get { return Settings.ClassBookPrice; } }
        public override int SellPrice { get { return 1; } }

        public WizardBook()
            : base(3)
        {
        }

        public WizardBook(Serial serial)
            : base(serial)
        {
            m_ItemID = 3;
        }
        public override void Use(Player player)
        {
            if (player.Promo < 1)
                return;

            this.Delete(player);
            player.ChangeClassByBook(LKCamelot.library.Class.Wizard);
        }
    }
    public class SwordmanBook : Item
    {
        public override string Name { get { return "BOOK OF SWORDMAN"; } }
        public override ulong BuyPrice { get { return Settings.ClassBookPrice; } }
        public override int SellPrice { get { return 1; } }

        public SwordmanBook()
            : base(3)
        {
        }

        public SwordmanBook(Serial serial)
            : base(serial)
        {
            m_ItemID = 3;
        }
        public override void Use(Player player)
        {
            if (player.Promo < 1)
                return;

            this.Delete(player);
            player.ChangeClassByBook(LKCamelot.library.Class.Swordsman);
        }
    }
    public class KnightBook : Item
    {
        public override string Name { get { return "BOOK OF KNIGHT"; } }
        public override ulong BuyPrice { get { return Settings.ClassBookPrice; } }
        public override int SellPrice { get { return 1; } }

        public KnightBook()
            : base(3)
        {
        }

        public KnightBook(Serial serial)
            : base(serial)
        {
            m_ItemID = 3;
        }
        public override void Use(Player player)
        {
            if (player.Promo < 1)
                return;

            this.Delete(player);
            player.ChangeClassByBook(LKCamelot.library.Class.Knight);
        }
    }



    public class BegginerBook : Item
    {
        public override string Name { get { return "BOOK OF BEGINNER"; } }
        public override ulong BuyPrice { get { return 3000000000000; } }
        public override int SellPrice { get { return 1; } }

        public BegginerBook()
            : base(3)
        {
        }

        public BegginerBook(Serial serial)
            : base(serial)
        {
            m_ItemID = 3;
        }
        public override void Use(Player player)
        {
            this.Delete(player);
            player.ChangeClassByBook(LKCamelot.library.Class.Knight);
        }
    }
}
