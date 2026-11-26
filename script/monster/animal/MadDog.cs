using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.monster
{
    public class Beginn_Pigmy : Monster
    {
        public override int INDEX { get { return 76; } }
        //public override string Name { get { return "PIGMY"; } }
        //public override int HP { get { return 10; } }
        //public override int Dam { get { return 2; } }
        //public override int AC { get { return 1; } }
        //public override int Hit { get { return 16; } }
        //public override int XP { get { return 15; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        //public override int MinAggro { get { return 3; } }
        //public override int MaxAggro { get { return 5; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(1.0, typeof(script.item.ViewBook), "15d10+225", 1, 1, 1),//added
                    new LootPackEntry(10.0, typeof(script.item.LifeDrug), "15d10+225", 1, 1, 1),
                    new LootPackEntry(10.0, typeof(script.item.MagicDrug), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+50", 40, 1, 1),
                    
                    new LootPackEntry(30.0, typeof(script.item.DogBone), "15d10+225", 1, 1, 1),
                    new LootPackEntry(100.0, typeof(script.item.조약돌), "15d10+225", 1, 1, 1),//added
                });
            }
        }

        //public Beginn_Pigmy()
        //    : base(15)
        //{
        //}
        //public Beginn_Pigmy(Serial serial)
        //    : base(serial)
        //{
        //}
        public Beginn_Pigmy(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Beginn_Pigmy(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }

    public class Beginn_MadDog : Monster
    {
        public override int INDEX { get { return 58; } }
        //public override string Name { get { return "MAD DOG"; } }
        //public override int HP { get { return 15; } }
        //public override int Dam { get { return 2; } }
        //public override int AC { get { return 0; } }
        //public override int Hit { get { return 6; } }
        //public override int XP { get { return 24; } }
        //public override int Color { get { return 0; } }
        //public override int WalkSpeed { get { return 1500; } }
        //public override int AttackSpeed { get { return 1000; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Animal; } }

        //public override int MinAggro { get { return 3; } }
        //public override int MaxAggro { get { return 5; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.01, typeof(script.item.Gold), "10d10+450", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.Rag), "15d10+225", 1, 1, 1),//added
                    new LootPackEntry(1.0, typeof(script.item.Buckler), "15d10+225", 1, 1, 1),//added
                    new LootPackEntry(1.0, typeof(script.item.ViewBook), "15d10+225", 1, 1, 1),//added
                    new LootPackEntry(5.0, typeof(script.item.LifeDrug), "15d10+225", 1, 1, 1),//added
                    new LootPackEntry(5.0, typeof(script.item.조약돌), "15d10+225", 1, 1, 1),//added
                    new LootPackEntry(5.0, typeof(script.item.DogBone), "15d10+225", 1, 1, 1)
                });
            }
        }

        public Beginn_MadDog()
            : base(19)
        {
        }
        public Beginn_MadDog(Serial serial)
            : base(serial)
        {
        }
        public Beginn_MadDog(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Beginn_MadDog(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }
}
