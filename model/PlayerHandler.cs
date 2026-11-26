using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using LKCamelot.io;
using System.Collections.Concurrent;

namespace LKCamelot.model
{
    public class PlayerHandler
    {
        public ConcurrentDictionary<string, Player>[] add = new ConcurrentDictionary<string, Player>[Settings.VillageName.Count];

        private static PlayerHandler singleton = null;
        public static PlayerHandler getSingleton()
        {
            if (singleton == null)
            {
                singleton = new PlayerHandler();
                for (int i = 0; i < Settings.VillageName.Count; i++)
                    singleton.add[i] = new ConcurrentDictionary<string, Player>();
            }

            return singleton;
        }

        public void newPlayerClient(IOClient client)
        {
            try
            {
                client.handler = this;
            }
            catch (Exception e)
            {
                Console.WriteLine("E at newplayerclient", e.ToString());
            }

       //     add.TryAdd(client, null);
            Console.WriteLine("Players Online: " + Count);
        }
        public int Count
        {
            get
            {
                int tot = 0;
                for (int i = 0; i < Settings.VillageName.Count; i++)
                {
                    tot += add[i].Count;
                }
                return tot;
            }
        }
        public ConcurrentDictionary<string, Player> Total
        {
            get
            {
                ConcurrentDictionary<string, Player> temp = new ConcurrentDictionary<string,Player>();
                for (int i = 0; i < Settings.VillageName.Count; i++)
                {
                    foreach (var p in add[i])
                        temp.TryAdd(p.Key, p.Value);
                }
                return temp;
            }
        }
        public void removePlayerClient(IOClient client)
        {
            Player cli;
      //      add.TryRemove(client, out cli);
            if (client.player != null)
                World.SendToAll(new QueDele(client.player.m_Channel, client.player.Map, new DeleteObject(client.player.m_Serial).Compile()));
            
            Console.WriteLine("Players Online: " + Count);
        }

        public bool HasPlayer(string name)
        {
            for (int i = 0; i < Settings.VillageName.Count; i++)
            {
                if (add[i].Where(xe => xe.Value.Name == name && xe.Value.loggedIn).FirstOrDefault().Key != null)
                    return true;
            }
            return false;
        }



        public Player GetPlayer(string name)
        {
            Player p = Total.Where(xe => xe.Value.Name == name.ToUpper()).FirstOrDefault().Value;
            
            if (p != null)
                return p;            
            return null;
        }
    }
}
