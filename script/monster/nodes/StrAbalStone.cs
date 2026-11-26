using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.monster
{
    public class StrAbalStone : BaseNode
    {
        public override int INDEX { get { return 172; } }
        //public override string Name { get { return "힘"; } }
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

            if (IsAbalRewardHit())
            {
                if (player.m_Str > 0 && (player.m_Extra + 1) < uint.MaxValue)
                {
                    checked
                    {
                        player.m_Str--;
                        player.m_Extra++;
                        player.client.SendPacket(new UpdateCharStats(player).Compile());
                    }
                }

                //if (Hits >= 100)
                //{
                //    base.Hits = 0;
                //    Alive = false;
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


        //public StrAbalStone()
        //    : base(8)
        //{
        //}
        //public StrAbalStone(Serial serial)
        //    : base(serial)
        //{
        //}
        public StrAbalStone(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;

            this.LastSpawn = Util.Random(0, 20) * Settings.Minute;
        }
        public StrAbalStone(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
