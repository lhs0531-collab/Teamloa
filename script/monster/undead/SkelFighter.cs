using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class SkelFighter : Monster
    {
        public override int INDEX { get { return 88; } }
        //public override string Name { get { return "Skel Fighter"; } }
        //public override int HP { get { return 250; } }
        //public override int Dam { get { return 91; } }
        //public override int AC { get { return 74; } }
        //public override int Hit { get { return 105; } }
        //public override int XP { get { return 807; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.1, typeof(script.item.Simitar), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.Cloak), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.TriangleShield), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.Cape), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.Helmet), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.Cutlass), "10d22+250", 40, 1, 1),
                    new LootPackEntry(5.0, typeof(script.item.통뼈), "15d10+225", 1, 1, 1),                   //15.03.08 통뼈추가 GM티리엘
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d24+300", 40, 1, 1),
                });
            }
        }

        //public SkelFighter()
        //    : base(24)
        //{
        //}
        //public SkelFighter(Serial serial)
        //    : base(serial)
        //{
        //}
        public SkelFighter(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public SkelFighter(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }
}
