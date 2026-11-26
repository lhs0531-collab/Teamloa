using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class WildDog : Monster
    {
        public override int INDEX { get { return 116; } }
        //public override string Name { get { return "Wild Dog"; } }
        //public override int HP { get { return 15; } }
        //public override int Dam { get { return 2; } }
        //public override int AC { get { return 0; } }
        //public override int Hit { get { return 6; } }
        //public override int XP { get { return 5; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Animal; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.01, typeof(script.item.Gold), "10d10+450", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.SmallShield), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.Hood), "15d10+225", 1, 1, 1),
                    new LootPackEntry(2.0, typeof(script.item.WoodenSword), "15d10+225", 1, 1, 1),
                    new LootPackEntry(2.0, typeof(script.item.BambooKnife), "15d10+225", 1, 1, 1),//added
                    new LootPackEntry(5.0, typeof(script.item.Buckler), "15d10+225", 1, 1, 1),//added
                    new LootPackEntry(5.0, typeof(script.item.Rag), "15d10+225", 1, 1, 1),//added
                    new LootPackEntry(30.0, typeof(script.item.DogBone), "15d10+225", 1, 1, 1),
                });
            }
        }

        //public WildDog()
        //    : base(27)
        //{
        //}

        //public WildDog(Serial serial)
        //    : base(serial)
        //{
        //}

        public WildDog(Serial temps, int Channel, int x, int y, string map)
            : base(temps)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temps;
        }
        public WildDog(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
