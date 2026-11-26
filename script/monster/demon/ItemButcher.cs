using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class ItemButcher : Monster
    {
        public override int INDEX { get { return 45; } }
        //public override string Name { get { return "Item Butcher"; } }
        //public override int HP { get { return 2000; } }
        //public override int Dam { get { return 185; } }
        //public override int AC { get { return 108; } }
        //public override int Hit { get { return 167; } }
        //public override int XP { get { return 1107; } }//updated blue butcher x3
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 600000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    // 15.01.31 GM티리엘
                    // 아이템 확률 전체 2 -> 15로 변경
                     new LootPackEntry(15, typeof(script.item.EnamelLeather), "15d10+225", 1, 1, 1),
                     new LootPackEntry(15, typeof(script.item.BroadAxe), "15d10+225", 1, 1, 1),
                     new LootPackEntry(15, typeof(script.item.FieldCap), "15d10+225", 1, 1, 1),
                     new LootPackEntry(15, typeof(script.item.CoatingShield), "15d10+225", 1, 1, 1),
                     new LootPackEntry(15, typeof(script.item.ThunderCrossBook), "15d10+225", 1, 1, 1),
                     new LootPackEntry(15, typeof(script.item.GuardianSwordBook), "15d10+225", 1, 1, 1),
                     new LootPackEntry(15, typeof(script.item.TeleportBook), "15d10+225", 1, 1, 1),
                     new LootPackEntry(15, typeof(script.item.FireBallBook), "15d10+225", 1, 1, 1),
                     new LootPackEntry(15, typeof(script.item.HardLeather), "15d10+225", 1, 1, 1),
                     new LootPackEntry(15, typeof(script.item.LeatherArmor), "15d10+225", 1, 1, 1),
                     new LootPackEntry(15, typeof(script.item.TowerShield), "15d10+225", 1, 1, 1),
                     new LootPackEntry(15, typeof(script.item.BroadSword), "15d10+225", 1, 1, 1),
                     new LootPackEntry(25.0, typeof(script.item.Gold), "10d20+2200", 40, 1, 1),
                });
            }
        }

        //public ItemButcher()
        //    : base(12)
        //{
        //}
        //public ItemButcher(Serial serial)
        //    : base(serial)
        //{
        //}

        public ItemButcher(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public ItemButcher(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
