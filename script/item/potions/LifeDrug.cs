using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.item
{
    public class LifeDrug : BasePotion
    {
        public override string Name { get { return "LIFE DRUG"; } }
        public override ulong BuyPrice { get { return 50; } }
        public override bool Stackable { get { return true; } }

        public LifeDrug()
            : base(20)
        {
        }

        public LifeDrug(Serial serial)
            : base(serial)
        {
            m_ItemID = 20;
        }

        public override void Use(Player player)
        {
            player.HPCur += player.HP / 2;
            base.Use(player);

            if (Quantity <= 5)
                player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}가 {1}개 남았습니다.", Name, Quantity)).Compile());
        
        }
    }
}
