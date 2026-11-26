using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class BoneDry : Monster
    {
        public override int INDEX { get { return 4; } }
        //public override string Name { get { return "Bone Dry"; } }
        //public override int HP { get { return 200; } }
        //public override int Dam { get { return 70; } }
        //public override int AC { get { return 60; } }
        //public override int Hit { get { return 89; } }
        //public override int XP { get { return 397; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Undead; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.1, typeof(script.item.Cape), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.SmallAxe), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.SpikedClub), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.Rapier), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.Saber), "10d22+250", 40, 1, 1),

                    //new LootPackEntry(0.1, typeof(script.item.Saber), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.Simitar), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.TriangleShield), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.Mace), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.Cloak), "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d22+250", 40, 1, 1),
                });
            }
        }

        //public BoneDry()
        //    : base(1)
        //{
        //}
        //public BoneDry(Serial serial)
        //    : base(serial)
        //{
        //}
        public BoneDry(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public BoneDry(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
