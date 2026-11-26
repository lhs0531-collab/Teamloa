using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class ItemZombie : Monster
    {
        public override int INDEX { get { return 54; } }
        //public override string Name { get { return "Item Zombie"; } }
        //public override int HP { get { return 300; } }

        //public override int AC { get { return 59; } }
        //public override int Hit { get { return 138; } }
        //public override int Dam { get { return 109; } }

        //public override int XP { get { return 144; } } // changed xp to reflect 3x blue zombie
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 420000; } } //15.01.31  5분 -> 7분으로 (GM티리엘)
        public override Race Race { get { return Race.Undead; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
					new LootPackEntry(25.0, LKCamelot.script.Packs.ItemZombiePark, "10d22+250", 40, 1, 1),
					new LootPackEntry(25.0, typeof(script.item.Gold), "5d10+1200", 40, 1, 1),

                    // 15.01.31 GM티리엘
                    // 아이템 확률 전체 2.0 -> 15.0로 변경
					/*
                    new LootPackEntry(15, typeof(script.item.FlameRoundBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.ComeBackBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.MorningStar), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.Cloak), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.Hanger), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.Mantle), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.Saber), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.ThunderCrossBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.Simitar), "15d10+225", 1, 1, 1),
					*/
                    // shouldn't drop book of trace ,
                    // should drop Book of Comeback
                });
            }
        }

        //public ItemZombie()
        //    : base(9)
        //{
        //}
        //public ItemZombie(Serial serial)
        //    : base(serial)
        //{
        //}
        public ItemZombie(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public ItemZombie(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
