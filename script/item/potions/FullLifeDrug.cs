using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.item
{
    public class FullLifeDrug : BasePotion
    {
        public override string Name { get { return "FULL LIFE DRUG"; } }
        public override ulong BuyPrice { get { return 500; } }
        public override int SellPrice { get { return 10; } }
        public override bool Stackable { get { return true; } }

        public FullLifeDrug()
            : base(22)
        {
        }

        public FullLifeDrug(Serial serial)
            : base(serial)
        {
            m_ItemID = 22;
        }

        public override void Use(Player player)
        {
            player.HPCur = player.HP;
            base.Use(player);

            if (Quantity <= 5)
                player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}가 {1}개 남았습니다.", Name, Quantity)).Compile());
        }
    }
}
