using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class iButcherGreen : Monster
    {
        public override int INDEX { get { return 16; } }
        //public override string Name { get { return "Butcher"; } }
        //public override int HP { get { return 130; } }

        //public override int AC { get { return 55; } }
        //public override int Hit { get { return 83; } }
        //public override int Dam { get { return 62; } }

        //public override int XP { get { return 342; } }
        //public override int Color { get { return 2; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.4, typeof(script.item.TeleportBook), "10d22+250", 40, 1, 1),
					new LootPackEntry(0.51, typeof(script.item.ExecutionBook), "10d12+150", 40, 1, 1),//added
                    new LootPackEntry(0.51, typeof(script.item.HealBook), "10d12+150", 40, 1, 1),//added
                    new LootPackEntry(0.51, typeof(script.item.MagicArmorBook), "10d12+150", 40, 1, 1),//added
                    new LootPackEntry(0.51, typeof(script.item.ZigZagBook), "10d12+150", 40, 1, 1),//added
                    new LootPackEntry(0.51, typeof(script.item.FireBallBook), "10d12+150", 40, 1, 1),//added
                    new LootPackEntry(1, typeof(script.item.SquareShield), "10d12+150", 40, 1, 1),//added
                    new LootPackEntry(1, typeof(script.item.Surplice), "10d12+150", 40, 1, 1),//added
                    new LootPackEntry(1, typeof(script.item.Headgear), "10d12+150", 40, 1, 1),//added
                    new LootPackEntry(1, typeof(script.item.SquareShield), "10d12+150", 40, 1, 1),//added
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d12+150", 40, 1, 1)
                });
            }
        }

        //public iButcherGreen()
        //    : base(8)
        //{
        //}
        //public iButcherGreen(Serial serial)
        //    : base(serial)
        //{
        //}
        public iButcherGreen(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public iButcherGreen(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
