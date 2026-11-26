using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class Rowedy : Monster
    {
        public override int INDEX { get { return 84; } }
        //public override string Name { get { return "Rowedy"; } }
        //public override int HP { get { return 3000; } }
        //public override int Dam { get { return 350; } }
        //public override int AC { get { return 195; } }
        //public override int Hit { get { return 270; } }
        //public override int XP { get { return 65000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.1, typeof(script.item.DarkPlate), "15d10+225", 1, 1, 1),           // 15.02.12 매찬 0.1 -> 0.05 (GM티리엘)
                    new LootPackEntry(0.1, typeof(script.item.Protectoria), "15d10+225", 1, 1, 1),         // 15.02.12 매찬 0.1 -> 0.05 (GM티리엘)
                    new LootPackEntry(0.1, typeof(script.item.PythonScale), "15d10+225", 1, 1, 1),         // 15.02.12 매찬 0.1 -> 0.05 (GM티리엘)
                    new LootPackEntry(0.1, typeof(script.item.GoldPlate), "15d10+225", 1, 1, 1),           // 15.02.12 매찬 0.1 -> 0.05 (GM티리엘)
                    new LootPackEntry(0.06, LKCamelot.script.Packs.AccessorySet21, "10d22+250", 40, 1, 1),  // 15.02.12 매찬 0.04 -> 0.06 (GM티리엘)
                    new LootPackEntry(0.1, typeof(script.item.Wand), "15d10+225", 1, 1, 1),             // 15.01.25 매찬 0.001 -> 0.1 (GM티리엘)
                    new LootPackEntry(0.1, typeof(script.item.WarPike), "15d10+225", 1, 1, 1),          // 15.01.25 매찬 0.001 -> 0.1 (GM티리엘)
                    new LootPackEntry(0.2, typeof(script.item.WarStaff), "15d10+225", 1, 1, 1),         // 15.01.25 매찬 0.009 -> 0.1 (GM티리엘)
                    new LootPackEntry(0.2, typeof(script.item.Javelin), "15d10+225", 1, 1, 1),          // 15.01.25 매찬 0.009 -> 0.1 (GM티리엘)
                    new LootPackEntry(0.1, typeof(script.item.ThunderStormBook), "5d500+8000", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.BigBangBook), "5d500+8000", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.AssassinBook), "5d500+8000", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.RevelationBook), "5d500+8000", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.ExecutionBook), "5d500+8000", 40, 1, 1),  //15.01.27 추가 (GM티리엘)
                    new LootPackEntry(0.2, typeof(script.item.SidewinderBook), "5d500+8000", 40, 1, 1),  //15.01.31 추가 (GM티리엘)
                    new LootPackEntry(20.0, typeof(script.item.Gold), "10d20+3000", 40, 1, 1),

                    //new LootPackEntry(0.009, typeof(script.item.BraveRing), "5d500+8000", 40, 1, 1),    // 15.01.25 매찬 0.01 -> 0.009 (GM티리엘)
                });
            }
        }

        //public Rowedy()
        //    : base(35)
        //{
        //}
        //public Rowedy(Serial serial)
        //    : base(serial)
        //{
        //}

        public Rowedy(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Rowedy(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }

    public class Biggun : Monster
    {
        public override int INDEX { get { return 1; } }
        //public override string Name { get { return "Biggun"; } }
        //public override int HP { get { return 5000; } }
        //public override int Dam { get { return 300; } }
        //public override int AC { get { return 235; } }
        //public override int Hit { get { return 550; } }
        //public override int XP { get { return 75000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.1, typeof(script.item.Wand), "15d10+225", 1, 1, 1),         // 15.01.25 매찬 0.001 -> 0.1 (GM티리엘)
                    new LootPackEntry(0.1, typeof(script.item.WarPike), "15d10+225", 1, 1, 1),      // 15.01.25 매찬 0.001 -> 0.1 (GM티리엘)
                    new LootPackEntry(0.2, typeof(script.item.WarStaff), "15d10+225", 1, 1, 1),     // 15.01.25 매찬 0.009 -> 0.1 (GM티리엘)
                    new LootPackEntry(0.2, typeof(script.item.Javelin), "15d10+225", 1, 1, 1),      // 15.01.25 매찬 0.009 -> 0.1 (GM티리엘)
                    new LootPackEntry(0.1, typeof(script.item.GoldPlate), "15d10+225", 1, 1, 1),    // 15.01.25 매찬 0.02 -> 0.1 (GM티리엘)
                    new LootPackEntry(0.1, typeof(script.item.DarkPlate), "15d10+225", 1, 1, 1),    // 15.01.25 매찬 0.02 -> 0.1 (GM티리엘)
                    new LootPackEntry(0.1, typeof(script.item.Protectoria), "15d10+225", 1, 1, 1),  // 15.01.25 매찬 0.02 -> 0.1 (GM티리엘)
                    new LootPackEntry(0.1, typeof(script.item.PythonScale), "15d10+225", 1, 1, 1),  // 15.01.25 매찬 0.02 -> 0.1 (GM티리엘)
                    new LootPackEntry(0.04, LKCamelot.script.Packs.AccessorySet21, "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.ThunderStormBook), "5d500+8000", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.BigBangBook), "5d500+8000", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.AssassinBook), "5d500+8000", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.RevelationBook), "5d500+8000", 40, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.ExecutionBook), "5d500+8000", 40, 1, 1),  //15.01.27 추가 (GM티리엘)
                    new LootPackEntry(0.2, typeof(script.item.SidewinderBook), "5d500+8000", 40, 1, 1),  //15.01.31 추가 (GM티리엘)
                    new LootPackEntry(20.0, typeof(script.item.Gold), "10d20+3500", 40, 1, 1),

                    //new LootPackEntry(0.009, typeof(script.item.BraveRing), "5d500+8000", 40, 1, 1),// 15.01.25 매찬 0.01 -> 0.009 (GM티리엘)
                });
            }
        }

        public Biggun()
            : base(36)
        {
        }
        public Biggun(Serial serial)
            : base(serial)
        {
        }
        public Biggun(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Biggun(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }
}
