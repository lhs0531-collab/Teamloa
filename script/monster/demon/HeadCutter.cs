using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class HeadCutter : Monster
    {
        public override int INDEX { get { return 42; } }
        //public override string Name { get { return "Head Cutter"; } }
        //public override int HP { get { return 150; } }
        //public override int Dam { get { return 26; } }
        //public override int AC { get { return 28; } }
        //public override int Hit { get { return 57; } }
        //public override int XP { get { return 78; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.5, typeof(script.item.RoundShield), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.5, typeof(script.item.ShortStaff), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.5, typeof(script.item.ShortSpear), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.5, typeof(script.item.Club), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.5, typeof(script.item.ShortSword), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.5, typeof(script.item.Surplice), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.5, typeof(script.item.FullDress), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.5, typeof(script.item.BambooHat), "15d10+225", 1, 1, 1),
					new LootPackEntry(0.3, typeof(script.item.FlameRoundBook), "15d10+225", 1, 1, 1),
					new LootPackEntry(0.3, typeof(script.item.ThunderCrossBook), "15d10+225", 1, 1, 1),

                    new LootPackEntry(15, typeof(script.item.Gold), "10d10+450", 40, 1, 1),
                });
            }
        }

        //public HeadCutter()
        //    : base(30)
        //{
        //}
        //public HeadCutter(Serial serial)
        //    : base(serial)
        //{
        //}

        public HeadCutter(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public HeadCutter(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
