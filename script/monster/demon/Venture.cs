using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    #region 동굴
    //public class DeadlyMessenger : Monster
    //{
    //    public override string Name { get { return "Deadly Messenger"; } }
    //    public override int HP { get { return 900; } }
    //    public override int Dam { get { return 500; } }
    //    public override int AC { get { return 285; } }
    //    public override int Hit { get { return 550; } }
    //    public override int XP { get { return 110000; } }
    //    public override int Color { get { return 0; } }
    //    public override int SpawnTime { get { return 30000; } }
    //    public override Race Race { get { return Race.Demon; } }

    //    public override LootPack Loot
    //    {
    //        get
    //        {
    //            return new LootPack(new LootPackEntry[]
    //            {
    //                new LootPackEntry(0.2, typeof(script.item.FlameRoundBook), "15d10+225", 1, 1, 1),
    //           //     new LootPackEntry(1.0, typeof(script.item.DemonDeathBook), "15d10+225", 1, 1, 1),
    //                new LootPackEntry(1.5, typeof(script.item.Saw), "15d10+225", 1, 1, 1),
    //                new LootPackEntry(1.5, typeof(script.item.Buckler), "15d10+225", 1, 1, 1),
    //                new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+80", 40, 1, 1),
    //            });
    //        }
    //    }

    //    public DeadlyMessenger()
    //        : base(203)
    //    {
    //    }

    //    public DeadlyMessenger(Serial temp, int Channel, int x, int y, string map)
    //        : this(temp)
    //    {
    //        m_MonsterID = 203;
    //        m_Loc = new Point2D(x, y);
    //        m_SpawnLoc = new Point2D(m_Loc.X, m_Loc.Y);
    //        m_Map = map;
    //        m_Serial = temp;
    //    }

    //    public DeadlyMessenger(Serial serial)
    //        : base(serial)
    //    {
    //    }
    //}
    //public class WildMonk : Monster
    //{
    //    public override string Name { get { return "Wild Monk"; } }
    //    public override int HP { get { return 900; } }
    //    public override int Dam { get { return 500; } }
    //    public override int AC { get { return 285; } }
    //    public override int Hit { get { return 550; } }
    //    public override int XP { get { return 110000; } }
    //    public override int Color { get { return 0; } }
    //    public override int SpawnTime { get { return 30000; } }
    //    public override Race Race { get { return Race.Demon; } }

    //    public override LootPack Loot
    //    {
    //        get
    //        {
    //            return new LootPack(new LootPackEntry[]
    //            {
    //                new LootPackEntry(0.1, typeof(script.item.GoldPlate), "15d10+225", 1, 1, 1),
    //                new LootPackEntry(0.1, typeof(script.item.DarkPlate), "15d10+225", 1, 1, 1),
    //                new LootPackEntry(0.1, typeof(script.item.Protectoria), "15d10+225", 1, 1, 1),
    //                new LootPackEntry(0.3, typeof(script.item.PowerStrong), "15d10+225", 1, 1, 1),
    //                new LootPackEntry(15.0, typeof(script.item.Gold), "2d1000+3000", 40, 1, 1),
    //            });
    //        }
    //    }

    //    public WildMonk()
    //        : base(202)
    //    {
    //    }

    //    public WildMonk(Serial temp, int Channel, int x, int y, string map)
    //        : this(temp)
    //    {
    //        m_MonsterID = 202;
    //        m_Loc = new Point2D(x, y);
    //        m_SpawnLoc = new Point2D(m_Loc.X, m_Loc.Y);
    //        m_Map = map;
    //        m_Serial = temp;
    //    }

    //    public WildMonk(Serial serial)
    //        : base(serial)
    //    {
    //    }
    //}


    public class Donggul_Medusa : Monster
    {
        public override int INDEX { get { return 60; } }
        //public override string Name { get { return "MEDUSA"; } }
        //public override int HP { get { return 800; } }
        //public override int Dam { get { return 350; } }
        //public override int AC { get { return 250; } }
        //public override int Hit { get { return 450; } }
        //public override int XP { get { return 80000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        //public override int WalkSpeed { get { return 1 * Settings.Second; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.03, typeof(script.item.Protectoria), "15d10+225", 1, 1, 1),      //15.02.12 매찬 0.01 -> 0.03 GM티리엘
                    new LootPackEntry(0.03, typeof(script.item.PythonScale), "15d10+225", 1, 1, 1),      //15.02.12 매찬 0.01 -> 0.03 GM티리엘
                    new LootPackEntry(0.05, typeof(script.item.BlackHandBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.TripleBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.TwisterBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+80", 40, 1, 1),

                    //이전데이터(15.02.01)
                    //new LootPackEntry(0.01, typeof(script.item.GreatLance), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.GreatStaff), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.Protectoria), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.PythonScale), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.BlackHandBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.TripleBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.TwisterBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+80", 40, 1, 1),
                });
            }
        }

        public Donggul_Medusa()
            : base(200)
        {
        }
        public Donggul_Medusa(Serial serial)
            : base(serial)
        {
        }
        public Donggul_Medusa(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Donggul_Medusa(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }


    }
    public class Donggul_TwinCobra : Monster
    {
        public override int INDEX { get { return 105; } }
        //public override string Name { get { return "TWIN COBRA"; } }
        //public override int HP { get { return 800; } }
        //public override int Dam { get { return 350; } }
        //public override int AC { get { return 250; } }
        //public override int Hit { get { return 450; } }
        //public override int XP { get { return 80000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        //public override int WalkSpeed { get { return 1 * Settings.Second; } }
        public override Race Race { get { return Race.Animal; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    //new LootPackEntry(0.01, typeof(script.item.Wand), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.WarPike), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.03, typeof(script.item.Protectoria), "15d10+225", 1, 1, 1),     //15.02.12 매찬 0.01 -> 0.03 GM티리엘
                    new LootPackEntry(0.03, typeof(script.item.PythonScale), "15d10+225", 1, 1, 1),      //15.02.12 매찬 0.01 -> 0.03 GM티리엘
                    new LootPackEntry(0.05, typeof(script.item.BlackHandBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.TripleBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.TwisterBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+80", 40, 1, 1),

                    //이전데이터(15.02.01)
                    //new LootPackEntry(0.01, typeof(script.item.GreatLance), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.GreatStaff), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.Protectoria), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.PythonScale), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.BlackHandBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.TripleBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.TwisterBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+80", 40, 1, 1),
                });
            }
        }

        public Donggul_TwinCobra()
            : base(201)
        {
        }
        public Donggul_TwinCobra(Serial serial)
            : base(serial)
        {
        }
        public Donggul_TwinCobra(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Donggul_TwinCobra(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }

    public class Donggul_Wildmonk : Monster
    {
        public override int INDEX { get { return 117; } }
        //public override string Name { get { return "WILD MONK"; } }
        //public override int HP { get { return 1500; } }
        //public override int Dam { get { return 500; } }
        //public override int AC { get { return 275; } }
        //public override int Hit { get { return 550; } }
        //public override int XP { get { return 90000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        //public override int WalkSpeed { get { return 1 * Settings.Second; } }
        public override Race Race { get { return Race.Animal; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.03, typeof(script.item.Protectoria), "15d10+225", 1, 1, 1),      //15.02.12 매찬 0.01 -> 0.03 GM티리엘
                    new LootPackEntry(0.03, typeof(script.item.PythonScale), "15d10+225", 1, 1, 1),      //15.02.12 매찬 0.01 -> 0.03 GM티리엘
                    new LootPackEntry(0.04, typeof(script.item.Cadeceus), "15d10+225", 1, 1, 1),         //15.02.12 추가 GM티리엘
                    new LootPackEntry(0.04, typeof(script.item.ForkedSpear), "15d10+225", 1, 1, 1),      //15.02.12 추가 GM티리엘
                    new LootPackEntry(0.05, typeof(script.item.BlackHandBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.WildMonkBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+80", 40, 1, 1),

                    //이전데이터(15.02.01)
                    //new LootPackEntry(0.01, typeof(script.item.GreatLance), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.GreatStaff), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.Protectoria), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.PythonScale), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.BlackHandBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.WildMonkBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+80", 40, 1, 1),
                });
            }
        }

        public Donggul_Wildmonk()
            : base(202)
        {
        }
        public Donggul_Wildmonk(Serial serial)
            : base(serial)
        {
        }
        public Donggul_Wildmonk(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Donggul_Wildmonk(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }
    public class Donggul_Deadlymessenger : Monster
    {
        public override int INDEX { get { return 20; } }
        //public override string Name { get { return "DEADLY MESSENGER"; } }
        //public override int HP { get { return 1500; } }
        //public override int Dam { get { return 500; } }
        //public override int AC { get { return 275; } }
        //public override int Hit { get { return 550; } }
        //public override int XP { get { return 90000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        //public override int WalkSpeed { get { return 1 * Settings.Second; } }
        public override Race Race { get { return Race.Animal; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    //new LootPackEntry(0.01, typeof(script.item.Wand), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.WarPike), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.03, typeof(script.item.Protectoria), "15d10+225", 1, 1, 1),      //15.02.12 매찬 0.01 -> 0.03 GM티리엘
                    new LootPackEntry(0.03, typeof(script.item.PythonScale), "15d10+225", 1, 1, 1),      //15.02.12 매찬 0.01 -> 0.03 GM티리엘
                    new LootPackEntry(0.04, typeof(script.item.Cadeceus), "15d10+225", 1, 1, 1),         //15.02.12 추가 GM티리엘
                    new LootPackEntry(0.04, typeof(script.item.ForkedSpear), "15d10+225", 1, 1, 1),      //15.02.12 추가 GM티리엘
                    new LootPackEntry(0.05, typeof(script.item.BlackHandBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.DeadlyMessengerBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+80", 40, 1, 1),

                    //이전데이터
                    //new LootPackEntry(0.01, typeof(script.item.GreatLance), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.GreatStaff), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.Protectoria), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.PythonScale), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.BlackHandBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.DeadlyMessengerBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+80", 40, 1, 1),
                });
            }
        }

        public Donggul_Deadlymessenger()
            : base(203)
        {
        }
        public Donggul_Deadlymessenger(Serial serial)
            : base(serial)
        {
        }
        public Donggul_Deadlymessenger(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Donggul_Deadlymessenger(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }
    #endregion

    #region 바이킹
    public class vViking : Monster
    {
        public override int INDEX { get { return 107; } }
        //public override string Name { get { return "VIKING"; } }
        //public override int HP { get { return 500; } }
        //public override int Dam { get { return 250; } }
        //public override int AC { get { return 160; } }
        //public override int Hit { get { return 230; } }
        //public override int XP { get { return 22500; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.1, typeof(script.item.HealthRing), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.MagicRing), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.SkillRing), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.LifeRing), "15d10+225", 1, 1, 1),

                    new LootPackEntry(0.1, typeof(script.item.HealthAmulet), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.MagicAmulet), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.SkillAmulet), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.LifeAmulet), "15d10+225", 1, 1, 1),

                    new LootPackEntry(0.5, typeof(script.item.MentalSwordBook), "15d10+225", 1, 1, 1), // 15.02.01 매찬 : 0.2 -> 0.5 (GM티리엘)
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+80", 40, 1, 1),
                });
            }
        }

        public vViking()
            : base(32)
        {
        }

        public vViking(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public vViking(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

        public vViking(Serial serial)
            : base(serial)
        {
        }
    }
    public class vButcher : Monster
    {
        public override int INDEX { get { return 14; } }
        //public override string Name { get { return "Butcher"; } }
        //public override int HP { get { return 500; } }
        //public override int Dam { get { return 230; } }
        //public override int AC { get { return 140; } }
        //public override int Hit { get { return 200; } }
        //public override int XP { get { return 16500; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.1, typeof(script.item.HealthRing), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.MagicRing), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.SkillRing), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.LifeRing), "15d10+225", 1, 1, 1),

                    new LootPackEntry(0.1, typeof(script.item.HealthAmulet), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.MagicAmulet), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.SkillAmulet), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.1, typeof(script.item.LifeAmulet), "15d10+225", 1, 1, 1),
                    
                    new LootPackEntry(0.2, typeof(script.item.MentalSwordBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+80", 40, 1, 1),
                });
            }
        }

        public vButcher()
            : base(8)
        {
        }

        public vButcher(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public vButcher(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

        public vButcher(Serial serial)
            : base(serial)
        {
        }
    }
    #endregion

    #region 스콜피온

    public class Venture_MadDog : Monster
    {
        public override int INDEX { get { return 59; } }
        //public override string Name { get { return "MAD DOG"; } }
        //public override int HP { get { return 150; } }
        //public override int Dam { get { return 16; } }
        //public override int AC { get { return 18; } }
        //public override int Hit { get { return 48; } }
        //public override int XP { get { return 1000; } }
        //public override int Color { get { return 0; } }
        //public override int WalkSpeed { get { return 1500; } }
        //public override int AttackSpeed { get { return 1000; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Animal; } }
        
        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.09, typeof(script.item.ThunderStrokeBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.09, typeof(script.item.FlameWaveBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d30+400", 40, 1, 1),
                });
            }
        }

        public Venture_MadDog()
            : base(19)
        {
        }
        public Venture_MadDog(Serial serial)
            : base(serial)
        {
        }
        public Venture_MadDog(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Venture_MadDog(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }
    public class BloodShot : Monster
    {
        public override int INDEX { get { return 3; } }
        //public override string Name { get { return "BLOOD SHOT"; } }
        //public override int HP { get { return 300; } }
        //public override int Dam { get { return 80; } }
        //public override int AC { get { return 75; } }
        //public override int Hit { get { return 100; } }
        //public override int XP { get { return 3000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.05, typeof(script.item.FlameRoundBook), "15d10+225", 1, 1, 1),  
                    new LootPackEntry(0.05, typeof(script.item.FireWallBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.LightningWallBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.ThunderStormBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.075, typeof(script.item.TornadoBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.075, typeof(script.item.StoneCurseBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.09, typeof(script.item.FlameWaveBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.09, typeof(script.item.ThunderStrokeBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d30+400", 40, 1, 1),
                });
            }
        }

        public BloodShot()
            : base(38)
        {
        }
        public BloodShot(Serial serial)
            : base(serial)
        {
        }
        public BloodShot(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public BloodShot(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }
    public class Scorpian : Monster
    {
        public override int INDEX { get { return 86; } }
        //public override string Name { get { return "SCORPIAN"; } }
        //public override int HP { get { return 300; } }
        //public override int Dam { get { return 100; } }
        //public override int AC { get { return 100; } }
        //public override int Hit { get { return 150; } }
        //public override int XP { get { return 4500; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.05, typeof(script.item.ThunderCrossBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.FireWallBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.LightningWallBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.ThunderStormBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.075, typeof(script.item.TornadoBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.075, typeof(script.item.StoneCurseBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.075, typeof(script.item.FlameWaveBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.09, typeof(script.item.ThunderStrokeBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+400", 40, 1, 1),
                });
            }
        }

        public Scorpian()
            : base(39)
        {
        }
        public Scorpian(Serial serial)
            : base(serial)
        {
        }
        public Scorpian(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Scorpian(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }
    #endregion

    public class DevilKid : Monster
    {
        public override int INDEX { get { return 24; } }
        //public override string Name { get { return "Devil Kid"; } }
        //public override int HP { get { return 7000; } }
        //public override int Dam { get { return 650; } }
        //public override int AC { get { return 385; } }
        //public override int Hit { get { return 750; } }
        //public override int XP { get { return 150000; } } //150000
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.03, typeof(script.item.DarkPlate), "15d10+225", 1, 1, 1),        // 15.02.12 추가 GM티리엘
                    new LootPackEntry(0.03, typeof(script.item.GoldPlate), "15d10+225", 1, 1, 1),        // 15.02.12 추가 GM티리엘
                    new LootPackEntry(0.05, LKCamelot.script.Packs.AccessorySet26, "10d22+250", 40, 1, 1), //15.02.10 매찬 0.02 -> 0.05로 변경
                    new LootPackEntry(0.05, typeof(script.item.GrandStaff), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.GreatLance), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.AssassinSpecialBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.GrandBigBangBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.Excalido), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.BarbarianAxe), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.ArmShield), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.BoneMail), "15d10+225", 1, 1, 1), 
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d1000+3000", 40, 1, 1),

                    //이전데이터(15.02.01)
                    //new LootPackEntry(0.005, typeof(script.item.DiamondArmor), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.005, typeof(script.item.GoliathPlate), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, LKCamelot.script.Packs.AccessorySet26, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.AssassinSpecialBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.GrandBigBangBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.Excalido), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.BarbarianAxe), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.ArmShield), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.BoneMail), "15d10+225", 1, 1, 1), 
                    //new LootPackEntry(15.0, typeof(script.item.Gold), "5d1000+3000", 40, 1, 1),
                });
            }
        }

        //public DevilKid()
        //    : base(18)
        //{
        //}
        //public DevilKid(Serial serial)
        //    : base(serial)
        //{
        //}
        public DevilKid(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public DevilKid(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }

    public class vIronGolem : Monster
    {
        public override int INDEX { get { return 44; } }
        //public override string Name { get { return "Iron Golem"; } }
        //public override int HP { get { return 9000; } }
        //public override int Dam { get { return 750; } }
        //public override int AC { get { return 445; } }
        //public override int Hit { get { return 850; } }
        //public override int XP { get { return 210000; } } //210000
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.03, typeof(script.item.DarkPlate), "15d10+225", 1, 1, 1),        // 15.02.12 추가 GM티리엘
                    new LootPackEntry(0.03, typeof(script.item.GoldPlate), "15d10+225", 1, 1, 1),        // 15.02.12 추가 GM티리엘
                    new LootPackEntry(0.05, LKCamelot.script.Packs.AccessorySet26, "10d22+250", 40, 1, 1), //15.02.10 매찬 0.02 -> 0.05로 변경
                    new LootPackEntry(0.05, typeof(script.item.GrandStaff), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.GreatLance), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.08, typeof(script.item.AssassinSpecialBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.08, typeof(script.item.GrandBigBangBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d1000+3000", 40, 1, 1),

                    //이전데이터(15.02.01)
                    //new LootPackEntry(0.008, typeof(script.item.GoliathPlate), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.008, typeof(script.item.DiamondArmor), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, LKCamelot.script.Packs.AccessorySet26, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.08, typeof(script.item.AssassinSpecialBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.08, typeof(script.item.GrandBigBangBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(15.0, typeof(script.item.Gold), "5d1000+3000", 40, 1, 1),
                });
            }
        }

        //public vIronGolem()
        //    : base(34)
        //{
        //}
        //public vIronGolem(Serial serial)
        //    : base(serial)
        //{
        //}
        public vIronGolem(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public vIronGolem(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }

    public class vGolemGuard : Monster
    {
        public override int INDEX { get { return 34; } }
        //public override string Name { get { return "Golem Guard"; } }
        //public override int HP { get { return 8000; } }
        //public override int Dam { get { return 600; } }
        //public override int AC { get { return 360; } }
        //public override int Hit { get { return 700; } }
        //public override int XP { get { return 120000; } } //120000
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.05, typeof(script.item.GrandStaff), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.GreatLance), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.UltraBigBangBook), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.MagmaHandBook), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.AssassinSpecialBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.GrandBigBangBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.ExtraScimitar), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.VikingHelmet), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.BarbarianAxe), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.ArmShield), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.BoneMail), "15d10+225", 1, 1, 1), 
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d1000+3000", 40, 1, 1),

                    //이전데이터(15.02.01)
                    //new LootPackEntry(0.005, typeof(script.item.DiamondArmor), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.005, typeof(script.item.GoliathPlate), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.AssassinSpecialBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.GrandBigBangBook), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.Excalido), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.BarbarianAxe), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.ArmShield), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.BoneMail), "15d10+225", 1, 1, 1), 
                    //new LootPackEntry(15.0, typeof(script.item.Gold), "5d1000+3000", 40, 1, 1),
                });
            }
        }

        //public vGolemGuard()
        //    : base(34)
        //{
        //}
        //public vGolemGuard(Serial serial)
        //    : base(serial)
        //{
        //}

        public vGolemGuard(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            m_Loc = new Point2D(x, y);
            m_SpawnLoc = new Point2D(m_Loc.X, m_Loc.Y);
            m_Map = map;
            m_Serial = temp;
        }
        public vGolemGuard(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }

    public class SuperIronGolem : Monster
    {
        public override int INDEX { get { return 103; } }
        //public override string Name { get { return "Super Iron Golem"; } }
        //public override int HP { get { return 9000; } }
        //public override int Dam { get { return 850; } }
        //public override int AC { get { return 470; } }
        //public override int Hit { get { return 1100; } }
        //public override int XP { get { return 255000; } } //255000
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.0075, typeof(script.item.Cadeceus), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.0075, typeof(script.item.ForkedSpear), "15d10+225", 1, 1, 1),                                    
                    new LootPackEntry(0.009, typeof(script.item.GrandStaff), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.009, typeof(script.item.GreatLance), "15d10+225", 1, 1, 1), 
                    new LootPackEntry(0.04, LKCamelot.script.Packs.AccessorySet26, "10d22+250", 40, 1, 1),  //12.02.12 매찬 0.01 -> 0.04 GM티리엘
                    new LootPackEntry(0.015, typeof(script.item.FamilyShield), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.015, typeof(script.item.RocketMaul), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.015, typeof(script.item.WarriorAngel), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.AssassinSpecialBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.GrandBigBangBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d1000+3000", 40, 1, 1),

                    //new LootPackEntry(0.009, typeof(script.item.GoldVest), "15d10+225", 1, 1, 1),
                });
            }
        }

        //public SuperIronGolem()
        //    : base(34)
        //{
        //}
        //public SuperIronGolem(Serial serial)
        //    : base(serial)
        //{
        //}
        public SuperIronGolem(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public SuperIronGolem(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }

    public class Rioter : Monster
    {
        public override int INDEX { get { return 83; } }
        //public override string Name { get { return "Rioter"; } }
        //public override int HP { get { return 12500; } }
        //public override int Dam { get { return 555; } }
        //public override int AC { get { return 500; } }
        //public override int Hit { get { return 3333; } }
        //public override int XP { get { return 300000; } } //300000
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.05, LKCamelot.script.Packs.AccessorySet26, "10d22+250", 40, 1, 1), //15.02.10 매찬 0.02 -> 0.05로 변경
                    new LootPackEntry(0.05, typeof(script.item.GrandStaff), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.GreatLance), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.ExtraScimitar), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.05, typeof(script.item.VikingHelmet), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.AssassinSpecialBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.GrandBigBangBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d1000+5000", 40, 1, 1),

                    //new LootPackEntry(0.009, typeof(script.item.GoldVest), "15d10+225", 1, 1, 1),
                });
            }
        }

        //public Rioter()
        //    : base(37)
        //{
        //}        
        //public Rioter(Serial serial)
        //    : base(serial)
        //{
        //}
        public Rioter(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Rioter(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
