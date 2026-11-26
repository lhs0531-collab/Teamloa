using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class iMummy : Monster
    {
        public override int INDEX { get { return 62; } }
        //public override string Name { get { return "Mummy1"; } }
        //public override int HP { get { return 150; } }
        //public override int Dam { get { return 70; } }
        //public override int AC { get { return 60; } }
        //public override int Hit { get { return 89; } }
        //public override int XP { get { return 397; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Undead; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.2, typeof(script.item.BattleArmor), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.Crook), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.LargeAxe), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.Skewer), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.ThunderCrossBook), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.4, typeof(script.item.TeleportBook), "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d30+550", 40, 1, 1),

                    //new LootPackEntry(0.2, typeof(script.item.LightningWallBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.02, typeof(script.item.TripleBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.02, typeof(script.item.TwisterBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.FireWallBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.FireShotBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.FlyingSwordBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.DragonOfFireBook), "10d22+250", 40, 1, 1),
                });
            }
        }

        //public iMummy()
        //    : base(3)
        //{
        //}
        //public iMummy(Serial serial)
        //    : base(serial)
        //{
        //}
        public iMummy(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public iMummy(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
