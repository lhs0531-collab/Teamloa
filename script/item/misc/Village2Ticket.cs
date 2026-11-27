using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.item
{
    public class Village2Ticket : Item
    {
        int Channel = 2;
        public override string Name { get { return Settings.VillageName.ElementAt(Channel) + " 티켓"; } }
        public override ulong BuyPrice { get { return 100; } }

        public Village2Ticket()
            : base(26)
        {
        }

        public Village2Ticket(Serial serial)
            : base(serial)
        {
            m_ItemID = 26;
        }

        public override void Use(Player player)
        {
            Player p;
            this.Delete(player);
            PlayerHandler.getSingleton().add[player.m_Channel].TryRemove(player.Name, out p);
            PlayerHandler.getSingleton().add[Channel].TryAdd(player.Name, p);

            var OldPlayers = PlayerHandler.getSingleton().add[player.m_Channel].Where(xe => xe.Value.loggedIn && xe.Value.Map == player.Map && xe.Value.Name != Name).Select(xe => xe.Value).ToList();

            BaseObject o;
            foreach (var oldplayer in OldPlayers)
            {
                if (oldplayer == null)
                    continue;
                //이전의 맵 플레이어가 나를 알고 있을때.
                if (oldplayer.InstancedObjects.ContainsKey(player.m_Serial))
                {
                    oldplayer.InstancedObjects.TryRemove(player.m_Serial, out o);
                    oldplayer.InstancedRangeObjects.TryRemove(player.m_Serial, out o);
                    oldplayer.client.SendPacket(new DeleteObject(player.m_Serial).Compile());
                }
            }

            player.m_Channel = Channel;
            player.client.LoadPlayer(false);
        }
    }
}
