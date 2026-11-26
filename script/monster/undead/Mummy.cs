using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class Mummy060 : Monster
    {
        public override int INDEX { get { return 63; } }
        //public override string Name { get { return "Mummy"; } }
        //public override int HP { get { return 600; } }
        //public override int Dam { get { return 324; } }
        //public override int AC { get { return 200; } }
        //public override int Hit { get { return 283; } }
        //public override int XP { get { return 51172; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.1, typeof(script.item.RevelationBook), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.BigBangBook), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.AssassinBook), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.DragonOfFireBook), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.FlyingSwordBook), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.ThunderStormBook), "10d22+250", 40, 1, 1), 
                    new LootPackEntry(0.2, typeof(script.item.MailPlate), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.Halberd), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.BastardSword), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.GreatSword), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.SevenStarSword), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.SamuraiPlate), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.WidePlate), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.GreatAxe), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.GreatMaul), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.FullPlate), "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d30+5550", 40, 1, 1),

                    //15.02.12 이전데이터
                    //new LootPackEntry(0.05, typeof(script.item.UltraBigBangBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.MagmaHandBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.RevelationBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.BigBangBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.FlyingSwordBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.DragonOfFireBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.12, typeof(script.item.ThunderStormBook), "10d22+250", 40, 1, 1),   //15.02.12 매찬 0.1 -> 0.12 GM티리엘
                    //new LootPackEntry(0.2, typeof(script.item.MailPlate), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.2, typeof(script.item.Halberd), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.2, typeof(script.item.BastardSword), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.2, typeof(script.item.GreatSword), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.2, typeof(script.item.SevenStarSword), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.2, typeof(script.item.SamuraiPlate), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.2, typeof(script.item.WidePlate), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.2, typeof(script.item.GreatAxe), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.2, typeof(script.item.GreatMaul), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.2, typeof(script.item.FullPlate), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(15.0, typeof(script.item.Gold), "10d30+5550", 40, 1, 1),
                });
            }
        }

        //public Mummy060()
        //    : base(3)
        //{
        //}
        //public Mummy060(Serial serial)
        //    : base(serial)
        //{
        //}
        public Mummy060(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Mummy060(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
