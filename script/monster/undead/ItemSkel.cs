using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class ItemSkel : Monster
    {
        public override int INDEX { get { return 51; } }
        //public override string Name { get { return "Item Skel"; } }
        //public override int HP { get { return 350; } }

        //public override int AC { get { return 85; } }
        //public override int Hit { get { return 157; } }
        //public override int Dam { get { return 121; } }

        //public override int XP { get { return 459; } } //xp reflects blue skel x3
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 660000; } }
        public override Race Race { get { return Race.Undead; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
					new LootPackEntry(25.0, LKCamelot.script.Packs.ItemSkelPark, "10d22+250", 40, 1, 1),
					new LootPackEntry(25.0, typeof(script.item.Gold), "5d10+1800", 40, 1, 1),

                    // 15.01.31 GM티리엘
                    // 아이템 확률 15 ~20 로 변경
                    /*new LootPackEntry(15, typeof(script.item.Robe), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.Crown), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.LargeShield), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.IronSword), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.ThunderCrossBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.GuardianSwordBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.FireBallBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.QuiltedArmor), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.Mace), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.Helmet), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.Cutlass), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15, typeof(script.item.LongSword), "15d10+225", 1, 1, 1),
                    new LootPackEntry(20, typeof(script.item.TeleportBook), "15d10+225", 1, 1, 1),*/
                    //shouldn't drop Thundercross, Teleport
                });
            }
        }

        //public ItemSkel()
        //    : base(23)
        //{
        //}
        //public ItemSkel(Serial serial)
        //    : base(serial)
        //{
        //}

        public ItemSkel(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public ItemSkel(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
