using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class ItemDummy : Monster
    {
        public override int INDEX { get { return 46; } }
        //public override string Name { get { return "Item Dummy"; } }
        //public override int HP { get { return 5000; } }
        //public override int Dam { get { return 490; } }
        //public override int AC { get { return 300; } }
        //public override int Hit { get { return 467; } }
        //public override int XP { get { return 1; } }
        //public override int Color { get { return 0; } }
        //public override int WalkSpeed { get { return 600; } }
        public override int SpawnTime { get { return 900000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
					new LootPackEntry(25.0, LKCamelot.script.Packs.ItemDummyPark, "10d22+250", 40, 1, 1),
					new LootPackEntry(25.0, typeof(script.item.Gold), "10d20+4200", 40, 1, 1),

                    // 15.01.31 GM티리엘
                    // 아이템 확률 전체2 -> 10~15 로 변경
					/*
                    new LootPackEntry(7, typeof(script.item.WarStaff), "15d10+225", 1, 1, 1),
                    new LootPackEntry(7, typeof(script.item.Javelin), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.BreastPlate), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.BattleAxe), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.GrandShield), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.SamuraiSword), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(2, typeof(script.item.StoneCurseBook), "10d22+250", 40, 1, 1),
                    //샤프아이
                    //라디에이션
                    //new LootPackEntry(2 typeof(script.item.FreezingBook), "10d22+250", 40, 1, 1),
                    //은신
                    new LootPackEntry(15, typeof(script.item.ScaleMail), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.WarHammer), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.GrandeurHelmet), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.FamilyShield), "15d10+225", 1, 1, 1),
					*/
                    //new LootPackEntry(2, typeof(script.item.AssassinBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(2, typeof(script.item.FireHawkBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(2, typeof(script.item.FullHelmet), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(2, typeof(script.item.ToughLeather), "15d10+225", 1, 1, 1),
                });
            }
        }

        //public ItemDummy()
        //    : base(13)
        //{
        //}
        //public ItemDummy(Serial serial)
        //    : base(serial)
        //{
        //}
        public ItemDummy(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public ItemDummy(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
