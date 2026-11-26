using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class ItemMummy : Monster
    {
        public override int INDEX { get { return 49; } }
        //public override string Name { get { return "Item Mummy"; } }
        //public override int HP { get { return 450; } }
        //public override int Dam { get { return 200; } }
        //public override int AC { get { return 115; } }
        //public override int Hit { get { return 181; } }
        //public override int XP { get { return 1863; } }//changed to reflect blue mummy x3
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 600000; } }
        public override Race Race { get { return Race.Undead; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
					new LootPackEntry(25.0, LKCamelot.script.Packs.ItemMummyPark, "10d22+250", 40, 1, 1),
					new LootPackEntry(25.0, typeof(script.item.Gold), "10d20+3200", 40, 1, 1),

                    // 15.01.31 GM티리엘
                    // 아이템 확률 전체 2 ->15로 변경
                    /*
					new LootPackEntry(8, typeof(script.item.TeleportBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.BattleArmor), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.LargeAxe), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.FullHelmet), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.Claymore), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.PlusHealBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.TeagueShieldBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.FlameRoundBook), "15d10+225", 1, 1, 1),
                    */
                    //new LootPackEntry(2, typeof(script.item.ThunderCrossBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(2, typeof(script.item.BattleShield), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(2, typeof(script.item.ChestGuard), "15d10+225", 1, 1, 1),

                    /*should drop Book of Oblivion, (Chest Guard, Large Axe,
                    Full Helmet, Battle Armor, Battle Shield). Shouldn't drop Thunder Cross, Teleport.
                    */
                });
            }
        }

        //public ItemMummy()
        //    : base(5)
        //{
        //}
        //public ItemMummy(Serial serial)
        //    : base(serial)
        //{
        //}

        public ItemMummy(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public ItemMummy(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
