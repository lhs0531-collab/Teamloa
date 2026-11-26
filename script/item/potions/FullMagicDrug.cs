using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.item
{
    public class FullMagicDrug : BasePotion
    {
        public override string Name { get { return "FULL MAGIC DRUG"; } }
        public override ulong BuyPrice { get { return 500; } }
        public override int SellPrice { get { return 10; } }
        public override bool Stackable { get { return true; } }

        public FullMagicDrug() : base(23)
        {
        }

        public FullMagicDrug(Serial serial) : base(serial)
        {
            m_ItemID = 23;
        }

        public override void Use(Player player)
        {
            player.MPCur = player.MP;
            base.Use(player);

            if (Quantity <= 5)
                player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}가 {1}개 남았습니다.", Name, Quantity)).Compile());
        
        }
    }
}
