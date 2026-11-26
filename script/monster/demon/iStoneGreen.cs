using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class iStoneGreen : Monster
    {
        public override int INDEX { get { return 100; } }
        //public override string Name { get { return "Stone"; } }
        //public override int HP { get { return 210; } }
        //public override int Dam { get { return 91; } }
        //public override int AC { get { return 74; } }
        //public override int Hit { get { return 105; } }
        //public override int XP { get { return 807; } }
        //public override int Color { get { return 2; } }
        //public override int WalkSpeed { get { return 1200; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.2, typeof(script.item.HailStormBook), "15d10+225", 1, 1, 1), //15.02.01 추가 (GM티리엘)
                    new LootPackEntry(0.2, typeof(script.item.FireShotBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.AssassinBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.5, typeof(script.item.LongStaff), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.5, typeof(script.item.Harpoon), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.DemonDeathBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.5, typeof(script.item.ToughLeather), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.5, typeof(script.item.LargeShield), "15d10+225", 1, 1, 1),
                    
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+80", 40, 1, 1),
                });
            }
        }

        //public iStoneGreen()
        //    : base(6)
        //{
        //}
        //public iStoneGreen(Serial serial)
        //    : base(serial)
        //{
        //}
        public iStoneGreen(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public iStoneGreen(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
