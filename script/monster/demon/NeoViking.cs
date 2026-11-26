using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class NeoViking : Monster
    {
        public override int INDEX { get { return 71; } }
        //public override string Name { get { return "Neo Viking"; } }
        //public override int HP { get { return 1000; } }
        //public override int Dam { get { return 65; } }
        //public override int AC { get { return 25; } }
        //public override int Hit { get { return 55; } }
        //public override int XP { get { return 410; } }
        //public override int Color { get { return 1; } }
        public override int SpawnTime { get { return 120000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(5.0, typeof(script.item.Cloak), "10d22+250", 40, 1, 1),
                    new LootPackEntry(5.0, typeof(script.item.Cain), "10d22+250", 40, 1, 1),
                    new LootPackEntry(5.0, typeof(script.item.Mask), "10d22+250", 40, 1, 1),
                    new LootPackEntry(5.0, typeof(script.item.LongSpear), "10d22+250", 40, 1, 1),
                    new LootPackEntry(5.0, typeof(script.item.Epee), "10d22+250", 40, 1, 1),
                    new LootPackEntry(25.0, typeof(script.item.Gold), "10d30+450", 40, 1, 1),
                });
            }
        }

        //public NeoViking()
        //    : base(32)
        //{
        //}
        //public NeoViking(Serial serial)
        //    : base(serial)
        //{
        //}
        public NeoViking(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public NeoViking(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
