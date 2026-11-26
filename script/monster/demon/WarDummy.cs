using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class WarDummy : Monster
    {
        public override int INDEX { get { return 111; } }
        //public override string Name { get { return "War Dummy"; } }
        //public override int HP { get { return 2500; } }
        //public override int Dam { get { return 260; } }
        //public override int AC { get { return 171; } }
        //public override int Hit { get { return 235; } }
        //public override int XP { get { return 26266; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.075, typeof(script.item.ThunderStormBook), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.01, typeof(script.item.LongStaff), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.01, typeof(script.item.Harpoon), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.02, typeof(script.item.FireHawkBook), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.FireShotBook), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.BattleArmor), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.LargeAxe), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.FullHelmet), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.Claymore), "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d30+550", 40, 1, 1),

                    //15.02.12 이전데이터
                    //new LootPackEntry(0.02, typeof(script.item.FireHawkBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.02, typeof(script.item.AssassinBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.FireShotBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.ExecutionBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.LongStaff), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.Harpoon), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.075, typeof(script.item.ThunderStormBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.2, typeof(script.item.BattleArmor), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.2, typeof(script.item.LargeAxe), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.2, typeof(script.item.FullHelmet), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.2, typeof(script.item.Claymore), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(15.0, typeof(script.item.Gold), "10d30+550", 40, 1, 1),
                });
            }
        }

        //public WarDummy()
        //    : base(14)
        //{
        //}
        //public WarDummy(Serial serial)
        //    : base(serial)
        //{
        //}
        public WarDummy(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public WarDummy(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
