using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{

    
    public class StrStatPotion : Item
    {
        public override string Name { get { return "STRENGTH DRUG"; } }
        public override ulong BuyPrice { get { return 30000; } }


        public StrStatPotion() : base(24)
        {
        }

        public StrStatPotion(Serial serial)
            : base(serial)
        {
            m_ItemID = 24;
        }
        public override void Use(Player player)
        {
            this.Delete(player);
            if (player.m_Str < 1)
                return;
            player.m_Str--;
            player.m_Extra++;
            player.client.SendPacket(new UpdateCharStats(player).Compile());
        }
    }
    public class MenStatPotion : Item
    {
        public override string Name { get { return "MENTALITY DRUG"; } }
        public override ulong BuyPrice { get { return 30000; } }


        public MenStatPotion() : base(24)
        {
        }

        public MenStatPotion(Serial serial)
            : base(serial)
        {
            m_ItemID = 24;
        }
        public override void Use(Player player)
        {
            this.Delete(player);
            if (player.m_Men < 1)
                return;
            player.m_Men--;
            player.m_Extra++;
            player.client.SendPacket(new UpdateCharStats(player).Compile());
        }
    }
    public class DexStatPotion : Item
    {
        public override string Name { get { return "DEXTERITY DRUG"; } }
        public override ulong BuyPrice { get { return 30000; } }


        public DexStatPotion() : base(24)
        {
        }

        public DexStatPotion(Serial serial)
            : base(serial)
        {
            m_ItemID = 24;
        }
        public override void Use(Player player)
        {
            this.Delete(player);
            if (player.m_Dex < 1)
                return;
            player.m_Dex--;
            player.m_Extra++;
            player.client.SendPacket(new UpdateCharStats(player).Compile());
        }
    }
    public class VitStatPotion : Item
    {
        public override string Name { get { return "VITALITY DRUG"; } }
        public override ulong BuyPrice { get { return 30000; } }


        public VitStatPotion() : base(24)
        {
        }

        public VitStatPotion(Serial serial)
            : base(serial)
        {
            m_ItemID = 24;
        }
        public override void Use(Player player)
        {
            this.Delete(player);
            if (player.m_Vit < 1)
                return;
            player.m_Vit--;
            player.m_Extra++;
            player.client.SendPacket(new UpdateCharStats(player).Compile());
        }
    }
}
