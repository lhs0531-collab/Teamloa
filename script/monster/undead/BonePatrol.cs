using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class BonePatrol : Monster
    {
        public override int INDEX { get { return 8; } }
        //public override string Name { get { return "Bone Patrol"; } }
        //public override int HP { get { return 200; } }
        //public override int Dam { get { return 80; } }
        //public override int AC { get { return 68; } }
        //public override int Hit { get { return 97; } }
        //public override int XP { get { return 621; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Undead; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.1, typeof(script.item.Mantle), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.OneEdgedAxe), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.Mask), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.SquareShield), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.Epee), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.IronSword), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.BroadSword), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.HardLeather), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.Cloak), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.TriangleShield), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.Crown), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.HornHelmet), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.BroadAxe), "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d24+300", 40, 1, 1),
                });
            }
        }

        //public BonePatrol()
        //    : base(21)
        //{
        //}
        //public BonePatrol(Serial serial)
        //    : base(serial)
        //{
        //}
        public BonePatrol(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public BonePatrol(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
