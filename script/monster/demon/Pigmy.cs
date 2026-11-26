using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class Pigmy : Monster
    {
        public override int INDEX { get { return 77; } }
        //public override string Name { get { return "PIGMY"; } }
        //public override int HP { get { return 25; } }
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
                    
                    //new LootPackEntry(0.1, typeof(script.item.FlameRoundBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(1.0, typeof(script.item.Hatchet), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(1.0, typeof(script.item.ExecutionBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(4.0, typeof(script.item.LifeDrug), "15d10+225", 1, 1, 1),
                    new LootPackEntry(4.0, typeof(script.item.ViewBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(5.0, typeof(script.item.Hood), "15d10+225", 1, 1, 1),//added
                    new LootPackEntry(5.0, typeof(script.item.Buckler), "15d10+225", 1, 1, 1),//added
                    new LootPackEntry(5.0, typeof(script.item.Rag), "15d10+225", 1, 1, 1),//added
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+50", 40, 1, 1),
                } );
            }
        }

        //public Pigmy() : base(15)
        //{
        //}
        //public Pigmy(Serial serial) : base(serial)
        //{
        //}
        public Pigmy(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Pigmy(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
