using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class ItemPigmy : Monster
    {
        public override int INDEX { get { return 50; } }
        //public override string Name { get { return "Item Pigmy"; } }
        //public override int HP { get { return 300; } }
        //public override int Dam { get { return 98; } }
        //public override int AC { get { return 58; } }
        //public override int Hit { get { return 127; } }
        //public override int XP { get { return 1; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 360000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    // 15.01.31 GM티리엘
                    // 아이템 확률 전체 2.0 -> 15 ~25 로 변경
                    new LootPackEntry(15, typeof(script.item.Mantle), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.Rapier), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.Mask), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.Saber), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.Epee), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.SpikedClub), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.SquareShield), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.TraceBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.OneEdgedAxe), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.SmallAxe), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.ComeBackBook), "15d10+225", 1, 1, 1), 
                    new LootPackEntry(15, typeof(script.item.Cape), "15d10+225", 1, 1, 1),          
                    new LootPackEntry(25, typeof(script.item.ThunderCrossBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(25, typeof(script.item.FlameRoundBook), "15d10+225", 1, 1, 1),                     
                    new LootPackEntry(25.0, typeof(script.item.Gold), "5d10+800", 40, 1, 1),
                });
            }
        }

        //public ItemPigmy()
        //    : base(4)
        //{
        //}
        //public ItemPigmy(Serial serial)
        //    : base(serial)
        //{
        //}

        public ItemPigmy(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public ItemPigmy(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
