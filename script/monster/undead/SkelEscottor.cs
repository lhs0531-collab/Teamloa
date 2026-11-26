using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class SkelEscottor : Monster
    {
        public override int INDEX { get { return 87; } }
        //public override string Name { get { return "Skel Escottor"; } }
        //public override int HP { get { return 500; } }
        //public override int Dam { get { return 76; } }
        //public override int AC { get { return 65; } }
        //public override int Hit { get { return 74; } }
        //public override int XP { get { return 570; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Undead; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.1, typeof(script.item.Saber), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.Cloak), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.TriangleShield), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.Cape), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.Helmet), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.SmallAxe), "10d22+250", 40, 1, 1),
                    new LootPackEntry(5.0, typeof(script.item.통뼈), "15d10+225", 1, 1, 1),                   //15.03.08 통뼈추가 GM티리엘
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d24+300", 40, 1, 1),
                });
            }
        }

        //public SkelEscottor()
        //    : base(23)
        //{
        //}
        //public SkelEscottor(Serial serial)
        //    : base(serial)
        //{
        //}
        public SkelEscottor(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public SkelEscottor(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
