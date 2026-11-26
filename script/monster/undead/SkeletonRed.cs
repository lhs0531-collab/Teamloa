using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class SkeletonRed : Monster
    {
        public override int INDEX { get { return 91; } }
        //public override string Name { get { return "Skeleton"; } }
        //public override int HP { get { return 120; } }
        //public override int Dam { get { return 22; } }
        //public override int AC { get { return 10; } }
        //public override int Hit { get { return 25; } }
        //public override int XP { get { return 42; } }
        //public override int Color { get { return 1; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Undead; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.1, typeof(script.item.Saber), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.Rapier), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.SpikedClub), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.5, typeof(script.item.Suit), "10d22+250", 40, 1, 1),//added
                    new LootPackEntry(0.5, typeof(script.item.RoundShield), "10d22+250", 40, 1, 1),//added
                    new LootPackEntry(0.5, typeof(script.item.ElectronicBallBook), "10d22+250", 40, 1, 1),//added
                    new LootPackEntry(0.5, typeof(script.item.Cap), "10d22+250", 40, 1, 1),//added
                    new LootPackEntry(0.5, typeof(script.item.Cape), "10d22+250", 40, 1, 1),//added
                    new LootPackEntry(0.5, typeof(script.item.SmallAxe), "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d18+100", 40, 1, 1),
                    
                    new LootPackEntry(30.0, typeof(script.item.DogBone), "15d10+225", 1, 1, 1),
                });
            }
        }

        //public SkeletonRed()
        //    : base(1)
        //{
        //}
        //public SkeletonRed(Serial serial)
        //    : base(serial)
        //{
        //}
        public SkeletonRed(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public SkeletonRed(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
