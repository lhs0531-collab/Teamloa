using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class ItemHardBoil : Monster
    {
        public override int INDEX { get { return 48; } }
        //public override string Name { get { return "Item Hard Boild"; } }
        //public override int HP { get { return 12900; } }
        //public override int Dam { get { return 790; } }
        //public override int AC { get { return 380; } }
        //public override int Hit { get { return 667; } }
        //public override int XP { get { return 1; } }
        //public override int Color { get { return 0; } }
        //public override int WalkSpeed { get { return 600; } }
        public override int SpawnTime { get { return 1200000; } }
        public override Race Race { get { return Race.Undead; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    // 15.01.31 GM티리엘
                    // 아이템 확률 전체 1 ~ 15 ~20 로 변경
                    new LootPackEntry(8, typeof(script.item.MailPlate), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.BastardSword), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.GreatSword), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.SevenStarSword), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.DemonDeathBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.BigBangBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.RevelationBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(2, typeof(script.item.TransparencyBook), "15d10+225", 1, 1, 1),      //은신
                    new LootPackEntry(8, typeof(script.item.SamuraiPlate), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.WidePlate), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.GreatAxe), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.GreatMaul), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.FullPlate), "15d10+225", 1, 1, 1),
                    new LootPackEntry(9, typeof(script.item.RainbowArmorBook), "15d10+225", 1, 1, 1),

                    //new LootPackEntry(1, typeof(script.item.UltraBigBangBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(1, typeof(script.item.MagmaHandBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(1, typeof(script.item.Wand), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(1, typeof(script.item.WarPike), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(1, typeof(script.item.CoreaSword), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(3, typeof(script.item.DeadlyBoomBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(3, typeof(script.item.AssassinBook), "15d10+225", 1, 1, 1),

                    new LootPackEntry(25.0, typeof(script.item.Gold), "10d20+3200", 40, 1, 1),
                });
            }
        }

        //public ItemHardBoil()
        //    : base(9)
        //{
        //}
        //public ItemHardBoil(Serial serial)
        //    : base(serial)
        //{
        //}
        public ItemHardBoil(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public ItemHardBoil(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
