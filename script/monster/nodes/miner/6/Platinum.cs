using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.monster
{
    public class Platinum : BaseNode
    {
        public override int INDEX { get { return 136; } }
        //public override string Name { get { return "백금"; } }
        //public override int Level { get { return 63; } }
        //public override int HP { get { return 6000; } }
        //public override int Dam { get { return 0; } }
        //public override int AC { get { return 0; } }
        //public override int XP { get { return 5525; } }
        //public override int Color { get { return 0; } }
        //public override int WalkSpeed { get { return 1200; } }
        public override int SpawnTime { get { return 60000; } }
        public override Race Race { get { return Race.Gem; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(70.0, typeof(script.item.Platinum), "5d10+3000", 40, 1, 1),
                });
            }
        }

        //public Platinum()
        //    : base(3)
        //{
        //}
        //public Platinum(Serial serial)
        //    : base(serial)
        //{
        //}
        public Platinum(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Platinum(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.LastAction = Int32.MaxValue;
            this.RandomSpawn = RandomGan;
        }

    }
}
