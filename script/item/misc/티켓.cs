using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.item
{
    public class 티켓 : Item
    {
        public override string Name { get { return "랜덤 티켓"; } }
        public override ulong BuyPrice { get { return 100; } }

        public 티켓()
            : base(26)
        {
        }

        public 티켓(Serial serial)
            : base(serial)
        {
            m_ItemID = 26;
        }

        public override void Use(Player player)
        {
            Player p;
            BaseObject o;
            this.Delete(player);
            int rnd = Util.Random(Settings.VillageName.Count());
            PlayerHandler.getSingleton().add[player.m_Channel].TryRemove(player.Name, out p);
            PlayerHandler.getSingleton().add[rnd].TryAdd(player.Name, p);

            var OldPlayers = PlayerHandler.getSingleton().add[player.m_Channel].Where(xe => xe.Value.loggedIn && xe.Value.Map == player.Map && xe.Value.Name != Name).Select(xe => xe.Value).ToList();

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

            player.m_Channel = rnd;
            player.client.LoadPlayer(false);
        }
    }
}
