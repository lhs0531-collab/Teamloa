using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class ItemStone : Monster
    {
        public override int INDEX { get { return 52; } }
        //public override string Name { get { return "Item Stone"; } }
        //public override int HP { get { return 5000; } }
        //public override int Dam { get { return 242; } }
        //public override int AC { get { return 156; } }
        //public override int Hit { get { return 195; } }
        //public override int XP { get { return 1; } }
        //public override int Color { get { return 0; } }
        //public override int WalkSpeed { get { return 600; } }
        public override int SpawnTime { get { return 600000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    // 15.01.31 GM티리엘
                    // 아이템 확률 전체 1 ~ 15 ~20 로 변경
                    new LootPackEntry(8, typeof(script.item.ChainMail), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.LargeHack), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.Maul), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.Tuck), "15d10+225", 1, 1, 1),      
                    //스톤커즈
                    new LootPackEntry(1.5, typeof(script.item.SharpEyeBook), "15d10+225", 1, 1, 1),        //샤프아이
                    //라디에이션
                    //프리징
                    new LootPackEntry(1.5, typeof(script.item.TransparencyBook), "15d10+225", 1, 1, 1),    //은신
                    new LootPackEntry(8, typeof(script.item.BoneMail), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.RingMail), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.BroadAxe), "15d10+225", 1, 1, 1),
                    new LootPackEntry(8, typeof(script.item.ArmShield), "15d10+225", 1, 1, 1),              //15.01.27 추가 (GM티리엘)
                    new LootPackEntry(8, typeof(script.item.Gladius), "15d10+225", 1, 1, 1),                //15.01.27 추가 (GM티리엘)


                    //new LootPackEntry(1.5, typeof(script.item.FireHawkBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(1.5, typeof(script.item.AssassinBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(1.5, typeof(script.item.FlyingSwordBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(1.5, typeof(script.item.DragonOfFireBook), "10d22+250", 40, 1, 1),  //15.01.27 삭제 (GM티리엘)
                    new LootPackEntry(25.0, typeof(script.item.Gold), "10d20+3200", 40, 1, 1),
                });
            }
        }

        //public ItemStone()
        //    : base(5)
        //{
        //}
        //public ItemStone(Serial serial)
        //    : base(serial)
        //{
        //}

        public ItemStone(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public ItemStone(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
