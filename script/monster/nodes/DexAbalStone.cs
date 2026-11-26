using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.monster
{
    public class DexAbalStone : BaseNode
    {
        public override int INDEX { get { return 142; } }
        //public override string Name { get { return "숙련"; } }
        //public override int HP { get { return 6000; } }
        //public override int Dam { get { return 0; } }
        //public override int AC { get { return 0; } }
        //public override int XP { get { return 5; } }
        //public override int Color { get { return 0; } }
        //public override int WalkSpeed { get { return 1200; } }
        public override int SpawnTime { get { return 30 * Settings.Second; } }
        public override int AliveTime { get { return 10 * Settings.Minute; } }
        public override Race Race { get { return Race.Demon; } }

        public override void TakeDamage(Player player)
        {
            base.Hits++;

            if (base.Hits % (50 / Settings.ABALRate[0]) == 0)
            {
                if (player.m_Dex > 0 && (player.m_Extra + 1) < uint.MaxValue)
                {
                    checked
                    {
                        player.m_Dex--;
                        player.m_Extra++;
                        player.client.SendPacket(new UpdateCharStats(player).Compile());
                    }
                }

                //2015-02-12
                //if (Hits >= 100)
                //{
                //    Alive = false;
                //    base.Hits = 0;
                //}
            }

        }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                 //   new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+3000", 40, 1, 1),
                });
            }
        }


        //public DexAbalStone()
        //    : base(8)
        //{
        //}
        //public DexAbalStone(Serial serial)
        //    : base(serial)
        //{
        //}
        public DexAbalStone(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;

            this.LastSpawn = Util.Random(0, 20) * Settings.Minute;
        }
        public DexAbalStone(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.LastAction = Int32.MaxValue;
            this.RandomSpawn = RandomGan;

            this.LastSpawn = Util.Random(0, 20) * Settings.Minute;
        }


    }
}
