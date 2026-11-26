using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.item
{
    public class ShopTicket : Item
    {
        int Channel = 0;
        public override string Name { get { return "상점티켓"; } }
        public override ulong BuyPrice { get { return 100; } }

        public ShopTicket()
            : base(26)
        {
        }

        public ShopTicket(Serial serial)
            : base(serial)
        {
            m_ItemID = 26;
        }

        public override void Use(Player player)
        {
            if (!Settings.VillageName.Contains(player.Map.Split(',')[0]))
            {
                player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("상점은 마을에서만 가능합니다.")).Compile());
                return;
            }
            this.Delete(player);

            if (World.NewAutoShop.Where(xe => xe.Value.owner.m_Serial == player.m_Serial).Any())
            {
                player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("생성된 상점이 있습니다.")).Compile());
            }
            else
            {
                script.npc.AutoShopNPC npc = new script.npc.AutoShopNPC(player);
                World.NewAutoShop.TryAdd(npc.m_Serial, npc);
            }
        }
    }
}
