using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class Minotaur : Monster
    {
        public override int INDEX { get { return 175; } }
        //public override string Name { get { return "MINO"; } }
        //public override int HP { get { return 25000; } }
        //public override int Dam { get { return 6500; } }
        //public override int AC { get { return 10000; } }
        //public override int Hit { get { return 6500; } }
        //public override int XP { get { return 2000000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override byte FrameType { get { return 0; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.02, LKCamelot.script.Packs.CSWSFS, "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d3000+25550", 40, 1, 1),

                    //삭제리스트(15.02.01)
                    //new LootPackEntry(0.3, LKCamelot.script.Packs.ArmorPack110, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.3, LKCamelot.script.Packs.WeaponPack110, "10d22+250", 40, 1, 1),
                });
            }
        }

        public Minotaur()
            : base(223)
        {
        }
        public Minotaur(Serial serial)
            : base(serial)
        {
        }
        public Minotaur(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Minotaur(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }
    //15.03.08
    //스킬 추가된다면 이몹은 썬더크로스입니다.
    public class BoneTrio2 : Monster
    {
        public override int INDEX { get { return 10; } }
        //public override string Name { get { return "Bone Trio"; } }
        //public override int HP { get { return 15500; } }
        //public override int Dam { get { return 2000; } }
        //public override int AC { get { return 4000; } }
        //public override int Hit { get { return 2500; } }
        //public override int XP { get { return 1000000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override byte FrameType { get { return 0; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.02, typeof(script.item.CurveShockBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.02, typeof(script.item.WindySpiritBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.02, typeof(script.item.FrameStrikeBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d12+15000", 40, 1, 1),
                });
            }
        }

        //public BoneTrio2()
        //    : base(220)
        //{
        //}
        //public BoneTrio2(Serial serial)
        //    : base(serial)
        //{
        //}

        public BoneTrio2(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public BoneTrio2(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }

    public class Mercenary : Monster
    {
        public override int INDEX { get { return 61; } }
        //public override string Name { get { return "Mercenary"; } }
        //public override int HP { get { return 15500; } }
        //public override int Dam { get { return 2000; } }
        //public override int AC { get { return 4000; } }
        //public override int Hit { get { return 3000; } }
        //public override int XP { get { return 750000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 1800000; } } //30분으로 
        public override byte FrameType { get { return 0; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d12+15000", 40, 1, 1),
                });
            }
        }

        //public Mercenary()
        //    : base(209)
        //{
        //}
        //public Mercenary(Serial serial)
        //    : base(serial)
        //{
        //}
        public Mercenary(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }

        public Mercenary(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }

    public class Saxon : Monster
    {
        public override int INDEX { get { return 85; } }
        //public override string Name { get { return "SAXON"; } }
        //public override int HP { get { return 250000; } }
        //public override int Dam { get { return 2000; } }
        //public override int AC { get { return 4000; } }
        //public override int Hit { get { return 4000; } }
        //public override int XP { get { return 1000000; } }
        //public override int Color { get { return 0; } }
        public override byte FrameType { get { return 0; } }
        //public override int SpawnTime { get { return 20 * Settings.Minute; } }
        public override int SpawnTime { get { return 1200000; } } // 15.01.31 20분 조정 (GM티리엘) 
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.01, typeof(script.item.GoliathPlate), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.01, typeof(script.item.DiamondArmor), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.01, typeof(script.item.GrandeurPride), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.01, typeof(script.item.HolyDefender), "10d22+250", 40, 1, 1),

                    //new LootPackEntry(0.05, typeof(script.item.Excalibur), "10d22+250", 40, 1, 1),       // 15.01.30 추가 GM티리엘
                    //new LootPackEntry(0.05, typeof(script.item.DaeungDaegum), "10d22+250", 40, 1, 1),    // 15.01.30 추가 GM티리엘
                    //new LootPackEntry(0.05, typeof(script.item.Kassandra), "10d22+250", 40, 1, 1),       // 15.01.30 추가 GM티리엘
                    //new LootPackEntry(0.05, typeof(script.item.TaegkFan), "10d22+250", 40, 1, 1),        // 15.01.30 추가 GM티리엘
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d30+100000", 40, 1, 1),
                });
            }
        }

        //public Saxon()
        //    : base(217)
        //{
        //}
        //public Saxon(Serial serial)
        //    : base(serial)
        //{
        //}
        public Saxon(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Saxon(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }

    public class Guardian_Devil : Monster
    {
        public override int INDEX { get { return 22; } }
        //public override string Name { get { return "DEVIL"; } }
        //public override int HP { get { return 1000000; } }
        //public override int Dam { get { return 3000; } }
        //public override int AC { get { return 7000; } }
        //public override int Hit { get { return 6000; } }
        //public override int XP { get { return 5000000; } }
        //public override int Color { get { return 0; } }
        //public override int SpawnTime { get { return 3000000; } }
        public override int SpawnTime { get { return 300000; } } // 15.01.29 50분 -> 5분 조정 (GM티리엘) 
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.01, typeof(script.item.GrandeurPride), "10d22+250", 40, 1, 1),
                    new LootPackEntry(3.0, typeof(script.item.VikingHelmet), "15d10+225", 1, 1, 1),
                    new LootPackEntry(6.0, LKCamelot.script.Packs.AccessorySet31, "10d22+250", 40, 1, 1), // 15.02.08 매찬 : 3.0 -> 6.0 (GM티리엘) 
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d30+100000", 40, 1, 1),
                });
            }
        }

        //public Guardian_Devil()
        //    : base(212)
        //{
        //}
        //public Guardian_Devil(Serial serial)
        //    : base(serial)
        //{
        //}
        public Guardian_Devil(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Guardian_Devil(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }

    public class Guardian_SmokeGiant : Monster
    {
        public override int INDEX { get { return 94; } }
        //public override string Name { get { return "SMOKE GIANT"; } }
        //public override int HP { get { return 20500; } }
        //public override int Dam { get { return 5900; } }
        //public override int AC { get { return 6900; } }
        //public override int Hit { get { return 5900; } }
        //public override int XP { get { return 2000000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override byte FrameType { get { return 0; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.01, typeof(script.item.LaurelCrown), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.CurveShockBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.WindySpiritBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.FrameStrikeBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.06, LKCamelot.script.Packs.AccessorySet36, "10d22+250", 40, 1, 1),  // 15.02.12 매찬 0.04 -> 0.06 GM티리엘
                    new LootPackEntry(0.1, typeof(script.item.DragonBlade), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.PigBasket), "10d12+150", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d12+15000", 40, 1, 1),
                });
            }
        }

        //public Guardian_SmokeGiant()
        //    : base(224)
        //{
        //}
        //public Guardian_SmokeGiant(Serial serial)
        //    : base(serial)
        //{
        //}
        public Guardian_SmokeGiant(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Guardian_SmokeGiant(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
