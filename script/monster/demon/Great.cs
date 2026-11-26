using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class Phantom : Monster
    {
        public override int INDEX { get { return 75; } }
        //public override string Name { get { return "Phantom"; } }
        //public override int HP { get { return 25000; } }
        //public override int Dam { get { return 6500; } }
        //public override int AC { get { return 10000; } }
        //public override int Hit { get { return 6500; } }
        //public override int XP { get { return 2000000; } }
        //public override int Color { get { return 0; } }
        public override byte FrameType { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(1.0, LKCamelot.script.Packs.GreatArmorPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.CSWSFS, "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.AccessorySet36, "10d22+250", 40, 1, 1),  // 15.02.12 매찬 0.07 -> 0.09 GM티리엘
                    //new LootPackEntry(0.1, LKCamelot.script.Packs.GreatWeaponPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d3000+25550", 40, 1, 1),

                    //삭제리스트(15.02.01)
                    //new LootPackEntry(0.3, LKCamelot.script.Packs.ArmorPack110, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.3, LKCamelot.script.Packs.WeaponPack110, "10d22+250", 40, 1, 1),
                });
            }
        }

        //public Phantom()
        //    : base(227)
        //{
        //}
        //public Phantom(Serial serial)
        //    : base(serial)
        //{
        //}
        public Phantom(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Phantom(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }

    public class Temptress : Monster
    {
        public override int INDEX { get { return 104; } }
        //public override string Name { get { return "Temptress"; } }
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
                    new LootPackEntry(1.0, LKCamelot.script.Packs.GreatArmorPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.CSWSFS, "10d22+250", 40, 1, 1),
					new LootPackEntry(1.0, LKCamelot.script.Packs.AccessorySet36, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, LKCamelot.script.Packs.GreatWeaponPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d3000+25550", 40, 1, 1),

                    //삭제리스트(15.02.01)
                    //new LootPackEntry(0.3, LKCamelot.script.Packs.ArmorPack110, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.3, LKCamelot.script.Packs.WeaponPack110, "10d22+250", 40, 1, 1),
                });
            }
        }

        public Temptress()
            : base(223)
        {
        }
        public Temptress(Serial serial)
            : base(serial)
        {
        }
        public Temptress(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Temptress(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }

    public class Solo : Monster
    {
        public override int INDEX { get { return 95; } }
        //public override string Name { get { return "Solo"; } }
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
                    new LootPackEntry(1.0, LKCamelot.script.Packs.GreatArmorPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.CSWSFS, "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.AccessorySet36, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, LKCamelot.script.Packs.GreatWeaponPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d3000+25550", 40, 1, 1),

                    //삭제리스트(15.02.01)
                    //new LootPackEntry(0.3, LKCamelot.script.Packs.ArmorPack110, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.3, LKCamelot.script.Packs.WeaponPack110, "10d22+250", 40, 1, 1),
                });
            }
        }

        public Solo()
            : base(225)
        {
        }
        public Solo(Serial serial)
            : base(serial)
        {
        }
        public Solo(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Solo(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }

    public class Violator : Monster
    {
        public override int INDEX { get { return 173; } }
        //public override string Name { get { return "Violator"; } }
        //public override int HP { get { return 1000; } }
        //public override int Dam { get { return 154; } }
        //public override int AC { get { return 110; } }
        //public override int Hit { get { return 155; } }
        //public override int XP { get { return 5250; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 1800000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d3000+25550", 40, 1, 1),
                });
            }
        }

        //public Stone()
        //    : base(6)
        //{
        //}
        //public Stone(Serial serial)
        //    : base(serial)
        //{
        //}
        public Violator(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Violator(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }

    public class FireDragon : Monster
    {
        public override int INDEX { get { return 30; } }
        //public override string Name { get { return "Fire Dragon"; } }
        //public override int HP { get { return 1500000; } }
        //public override int Dam { get { return 6500; } }
        //public override int AC { get { return 10000; } }
        //public override int Hit { get { return 6500; } }
        //public override int XP { get { return 0; } }          //2000000
        //public override int Color { get { return 0; } }
        public override byte FrameType { get { return 0; } }
        public override int SpawnTime { get { return 30 * Settings.Minute; } } //이벤끝나면
        //public override int SpawnTime { get { return 3600000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
					//카산, 엑칼, 대웅, 태극펜, 골드베스트, 해모수
					//글라디, 월계, 그프, 홀디, 골럇, 위저드옷, 샤먼옷
					//새마(기,위,샤)
					//36셋
					//전직책(기,위,샤,검)
					//1,000,000 마돈(확률 100%)
					new LootPackEntry(1.0, LKCamelot.script.Packs.CSWSFS, "10d22+250", 40, 1, 1),
					new LootPackEntry(1.0, LKCamelot.script.Packs.GreatWeaponPack, "10d22+250", 40, 1, 1),
					new LootPackEntry(1.0, LKCamelot.script.Packs.GreatArmorPack, "10d22+250", 40, 1, 1),
					new LootPackEntry(1.0, LKCamelot.script.Packs.NewVVArmorPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.AccessorySet36, "10d22+250", 40, 1, 1),
					new LootPackEntry(1.0, typeof(script.item.Haemosu), "10d22+250", 40, 1, 1),
					new LootPackEntry(1.0, typeof(script.item.GoldVest), "10d22+250", 40, 1, 1),
					new LootPackEntry(1.0, typeof(script.item.ShamanBook), "10d22+250", 40, 1, 1),
					new LootPackEntry(1.0, typeof(script.item.WizardBook), "10d22+250", 40, 1, 1),
					new LootPackEntry(1.0, typeof(script.item.SwordmanBook), "10d22+250", 40, 1, 1),
					new LootPackEntry(1.0, typeof(script.item.KnightBook), "10d22+250", 40, 1, 1),
					new LootPackEntry(100, typeof(script.item.Gold), "d+1000000", 40, 1, 1),

                    //new LootPackEntry(0.01, typeof(script.item.Haemosu), "10d30+550", 40, 1, 1),              // 15.01.23 GM티리엘
                    //new LootPackEntry(0.08, LKCamelot.script.Packs.CSWSFS, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.09, LKCamelot.script.Packs.AccessorySet36, "10d22+250", 40, 1, 1),  // 15.02.12 매찬 0.07 -> 0.09 GM티리엘
                    //new LootPackEntry(0.2, LKCamelot.script.Packs.GreatArmorPack, "10d22+250", 40, 1, 1),
                    ////new LootPackEntry(0.2, LKCamelot.script.Packs.GreatWeaponPack, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(1, LKCamelot.script.Packs.ArmorPack110, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(1, LKCamelot.script.Packs.WeaponPack110, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(15.0, typeof(script.item.Gold), "10d3000+100000", 40, 1, 1),

                    //삭제리스트(15.02.01)
                    //new LootPackEntry(0.01, typeof(script.item.FSofdk), "10d22+250", 40, 1, 1),
                });
            }
        }

        public FireDragon()
            : base(216)
        {
        }
        public FireDragon(Serial serial)
            : base(serial)
        {
        }
        public FireDragon(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public FireDragon(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }

    public class PrincessShea : Monster
    {
        public override int INDEX { get { return 81; } }
        //public override string Name { get { return "Princess Shea"; } }
        //public override int HP { get { return 30000; } }
        //public override int Dam { get { return 6500; } }
        //public override int AC { get { return 10000; } }
        //public override int Hit { get { return 6500; } }
        //public override int XP { get { return 1000000; } }
        //public override int Color { get { return 0; } }
        public override byte FrameType { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(1.0, LKCamelot.script.Packs.GreatArmorPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.CSWSFS, "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.AccessorySet36, "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.GreatWeaponPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d3000+25550", 40, 1, 1),

                    //삭제리스트(15.02.01)
                    //new LootPackEntry(0.3, LKCamelot.script.Packs.ArmorPack110, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.3, LKCamelot.script.Packs.WeaponPack110, "10d22+250", 40, 1, 1),
                });
            }
        }

        public PrincessShea()
            : base(11)
        {
        }
        public PrincessShea(Serial serial)
            : base(serial)
        {
        }
        public PrincessShea(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public PrincessShea(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }

    public class QueenSpriss : Monster
    {
        public override int INDEX { get { return 82; } }
        //public override string Name { get { return "Queen Spriss"; } }
        //public override int HP { get { return 30000; } }
        //public override int Dam { get { return 6500; } }
        //public override int AC { get { return 10000; } }
        //public override int Hit { get { return 6500; } }
        //public override int XP { get { return 1000000; } }
        //public override int Color { get { return 0; } }
        public override byte FrameType { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(1.0, LKCamelot.script.Packs.GreatArmorPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.AccessorySet36, "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.GreatWeaponPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d3000+25550", 40, 1, 1),

                    //삭제리스트(15.02.01)
                    //new LootPackEntry(0.6, LKCamelot.script.Packs.ArmorPack110, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.6, LKCamelot.script.Packs.WeaponPack110, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.KVSword), "10d3000+25550", 40, 1, 1),
                });
            }
        }

        public QueenSpriss()
            : base(226)
        {
        }
        public QueenSpriss(Serial serial)
            : base(serial)
        {
        }
        public QueenSpriss(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public QueenSpriss(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }

    public class Centeur : Monster
    {
        public override int INDEX { get { return 18; } }
        //public override string Name { get { return "Centeur"; } }
        //public override int HP { get { return 15000; } }
        //public override int Dam { get { return 5900; } }
        //public override int AC { get { return 6900; } }
        //public override int Hit { get { return 5900; } }
        //public override int XP { get { return 500000; } }
        //public override int Color { get { return 0; } }
        public override byte FrameType { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(1.0, LKCamelot.script.Packs.GreatArmorPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.CSWSFS, "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.AccessorySet36, "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.GreatWeaponPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d3000+25550", 40, 1, 1),

                    //삭제리스트(15.02.01)
                    //new LootPackEntry(0.3, LKCamelot.script.Packs.ArmorPack110, "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.3, LKCamelot.script.Packs.WeaponPack110, "10d22+250", 40, 1, 1),
                });
            }
        }

        public Centeur()
            : base(222)
        {
        }
        public Centeur(Serial serial)
            : base(serial)
        {
        }
        public Centeur(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Centeur(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
