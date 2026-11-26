using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class NeoSkelEscottor : Monster
    {
        public override int INDEX { get { return 70; } }
        //public override string Name { get { return "Neo Skel Escottor"; } }
        //public override int HP { get { return 400; } }
        //public override int Dam { get { return 65; } }
        //public override int AC { get { return 35; } }
        //public override int Hit { get { return 55; } }
        //public override int XP { get { return 950; } }
        //public override int Color { get { return 1; } }
        public override int SpawnTime { get { return 120000; } }
        public override Race Race { get { return Race.Undead; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    
                    new LootPackEntry(5.0, typeof(script.item.Saber), "10d22+250", 40, 1, 1),
                    new LootPackEntry(5.0, typeof(script.item.Simitar), "10d22+250", 40, 1, 1),
                    new LootPackEntry(5.0, typeof(script.item.TriangleShield), "10d22+250", 40, 1, 1),
                    new LootPackEntry(5.0, typeof(script.item.Cain), "10d22+250", 40, 1, 1),
                    new LootPackEntry(5.0, typeof(script.item.Mask), "10d22+250", 40, 1, 1),
                    new LootPackEntry(5.0, typeof(script.item.LongSpear), "10d22+250", 40, 1, 1),
                    new LootPackEntry(5.0, typeof(script.item.Mace), "10d22+250", 40, 1, 1),
                    new LootPackEntry(5.0, typeof(script.item.Cloak), "10d22+250", 40, 1, 1),
                    new LootPackEntry(5.0, typeof(script.item.Mantle), "10d22+250", 40, 1, 1),
                    new LootPackEntry(25.0, typeof(script.item.Gold), "10d30+550", 40, 1, 1),
                });
            }
        }

        //public NeoSkelEscottor()
        //    : base(23)
        //{
        //}
        //public NeoSkelEscottor(Serial serial)
        //    : base(serial)
        //{
        //}

        public NeoSkelEscottor(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public NeoSkelEscottor(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
