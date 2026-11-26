using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.item
{
    public class MagicDrug : BasePotion
    {
        public override string Name { get { return "MAGIC DRUG"; } }
        public override ulong BuyPrice { get { return 50; } }
        public override bool Stackable { get { return true; } }

        public MagicDrug() : base(21)
        {
        }

        public MagicDrug(Serial serial): base(serial)
        {
            m_ItemID = 21;
        }

        public override void Use(Player player)
        {
            player.MPCur += player.MP / 2;
            base.Use(player);

            if (Quantity <= 5)
                player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}가 {1}개 남았습니다.", Name, Quantity)).Compile());
        
        }
    }
}
