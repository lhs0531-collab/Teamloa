using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class DevilGuard : Monster
    {
        public override int INDEX { get { return 23; } }
        //public override string Name { get { return "CANNIBAL DEMON"; } }
        //public override int HP { get { return 15000; } }
        //public override int Dam { get { return 2000; } }
        //public override int AC { get { return 4000; } }
        //public override int Hit { get { return 4000; } }
        //public override int XP { get { return 500000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.7, typeof(script.item.DiamondStick), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.Calypso), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.AssassinSpecialBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.GrandBigBangBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.UltraBigBangBook), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.7, LKCamelot.script.Packs.NewVVArmorPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d3000+20550", 40, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.CurveShockBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.WindySpiritBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.FrameStrikeBook), "10d12+150", 40, 1, 1),
                });
            }
        }

        //public DevilGuard()
        //    : base(211)
        //{
        //}
        //public DevilGuard(Serial serial)
        //    : base(serial)
        //{
        //}
        public DevilGuard(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public DevilGuard(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }

    public class Bloodwing : Monster
    {
        public override int INDEX { get { return 174; } }
        //public override string Name { get { return "Bloodwing"; } }
        //public override int HP { get { return 32000; } }
        //public override int Dam { get { return 1000; } }
        //public override int AC { get { return 3300; } }
        //public override int Hit { get { return 2500; } }
        //public override int XP { get { return 800000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } } //이벤트 후 적용
        //public override int SpawnTime { get { return 3600000; } }
        //public override int WalkSpeed { get { return 2 * Settings.Second; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    //new LootPackEntry(0.01, LKCamelot.script.Packs.AccessorySet31, "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.DiamondStick), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.Calypso), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.NewVVArmorPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d300+20000", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.CurveShockBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.WindySpiritBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.FrameStrikeBook), "10d12+150", 40, 1, 1),
                });
            }
        }

        //public Ninja()
        //    : base(213)
        //{
        //}
        //public Ninja(Serial serial)
        //    : base(serial)
        //{
        //}
        public Bloodwing(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Bloodwing(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }


    public class Ninja : Monster
    {
        public override int INDEX { get { return 73; } }
        //public override string Name { get { return "NINJA"; } }
        //public override int HP { get { return 32000; } }
        //public override int Dam { get { return 1000; } }
        //public override int AC { get { return 3300; } }
        //public override int Hit { get { return 2500; } }
        //public override int XP { get { return 800000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        //public override int WalkSpeed { get { return 2 * Settings.Second; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(1.0, typeof(script.item.DiamondStick), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.Calypso), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.NewVVArmorPack, "10d22+250", 40, 1, 1),

                    new LootPackEntry(1.0, typeof(script.item.LionHeart), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.DragonBlade), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.Tomahawk), "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.GiantHammer), "10d22+250", 40, 1, 1),

                    new LootPackEntry(1.0, typeof(script.item.CurveShockBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.WindySpiritBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.FrameStrikeBook), "10d12+150", 40, 1, 1),

                    //new LootPackEntry(0.03, typeof(script.item.HolyDefender), "10d22+250", 40, 1, 1),     //15.02.26 삭제 GM티리엘
                    //new LootPackEntry(0.01, typeof(script.item.Kassandra), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.TaegkFan), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.Excalibur), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.DaeungDaegum), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.GrandeurPride), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.GoliathPlate), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.UnicornProtectoria), "10d22+250", 40, 1, 1),//15.01.24 삭제처리 gm티리엘
                    //new LootPackEntry(0.0025, typeof(script.item.SRing), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.0025, typeof(script.item.SAmulet), "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d300+20000", 40, 1, 1),
                });
            }
        }

        //public Ninja()
        //    : base(213)
        //{
        //}
        //public Ninja(Serial serial)
        //    : base(serial)
        //{
        //}
        public Ninja(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Ninja(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }

    public class SKELGIANT : Monster
    {
        public override int INDEX { get { return 89; } }
        //public override string Name { get { return "SKEL GIANT"; } }
        //public override int HP { get { return 18000; } }
        //public override int Dam { get { return 2000; } }
        //public override int AC { get { return 1000; } }
        //public override int Hit { get { return 2500; } }
        //public override int XP { get { return 400000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.7, typeof(script.item.DiamondStick), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.Calypso), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.7, LKCamelot.script.Packs.NewVVArmorPack, "10d22+250", 40, 1, 1),

                    new LootPackEntry(0.7, typeof(script.item.LionHeart), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.DragonBlade), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.Tomahawk), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.GiantHammer), "10d22+250", 40, 1, 1),

                    new LootPackEntry(0.7, typeof(script.item.CurveShockBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.WindySpiritBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.FrameStrikeBook), "10d12+150", 40, 1, 1),

                    //new LootPackEntry(0.03, typeof(script.item.HolyDefender), "10d22+250", 40, 1, 1),     //15.02.26 삭제 GM티리엘
                    //new LootPackEntry(0.01, typeof(script.item.Kassandra), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.TaegkFan), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.Excalibur), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.DaeungDaegum), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.GrandeurPride), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.GoliathPlate), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.UnicornProtectoria), "10d22+250", 40, 1, 1),//15.01.24 삭제처리 gm티리엘
                    //new LootPackEntry(0.0025, typeof(script.item.SRing), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.0025, typeof(script.item.SAmulet), "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d300+20000", 40, 1, 1),
                });
            }
        }

        //public SKELGIANT()
        //    : base(213)
        //{
        //}
        //public SKELGIANT(Serial serial)
        //    : base(serial)
        //{
        //}
        public SKELGIANT(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public SKELGIANT(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }

    public class Gargoyle : Monster
    {
        public override int INDEX { get { return 31; } }
        //public override string Name { get { return "Gargoyle"; } }
        //public override int HP { get { return 20000; } }
        //public override int Dam { get { return 1000; } }
        //public override int AC { get { return 1500; } }
        //public override int Hit { get { return 3000; } }
        //public override int XP { get { return 400000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        //public override int WalkSpeed { get { return 2 * Settings.Second; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.7, typeof(script.item.DiamondStick), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.Calypso), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.7, LKCamelot.script.Packs.NewVVArmorPack, "10d22+250", 40, 1, 1),

                    new LootPackEntry(0.7, typeof(script.item.LionHeart), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.DragonBlade), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.Tomahawk), "10d22+250", 40, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.GiantHammer), "10d22+250", 40, 1, 1),

                    new LootPackEntry(0.7, typeof(script.item.CurveShockBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.WindySpiritBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.7, typeof(script.item.FrameStrikeBook), "10d12+150", 40, 1, 1),

                    //new LootPackEntry(0.03, typeof(script.item.HolyDefender), "10d22+250", 40, 1, 1),     //15.02.26 삭제 GM티리엘
                    //new LootPackEntry(0.01, typeof(script.item.Kassandra), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.TaegkFan), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.Excalibur), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.DaeungDaegum), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.GrandeurPride), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.GoliathPlate), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.UnicornProtectoria), "10d22+250", 40, 1, 1),//15.01.24 삭제처리 gm티리엘
                    //new LootPackEntry(0.0025, typeof(script.item.SRing), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.0025, typeof(script.item.SAmulet), "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d300+20000", 40, 1, 1),
                });
            }
        }

        //public Gargoyle()
        //    : base(204)
        //{
        //}
        //public Gargoyle(Serial serial)
        //    : base(serial)
        //{
        //}
        public Gargoyle(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Gargoyle(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }


    public class Necromancer : Monster
    {
        public override int INDEX { get { return 67; } }
        //public override string Name { get { return "Necromancer"; } }
        //public override int HP { get { return 32000; } }
        //public override int Dam { get { return 1200; } }
        //public override int AC { get { return 3600; } }
        //public override int Hit { get { return 3200; } }
        //public override int XP { get { return 950000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public virtual byte FrameType { get { return 0; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(1.0, typeof(script.item.DiamondStick), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.Calypso), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.NewVVArmorPack, "10d22+250", 40, 1, 1),

                    new LootPackEntry(1.0, typeof(script.item.LionHeart), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.DragonBlade), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.Tomahawk), "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.GiantHammer), "10d22+250", 40, 1, 1),

                    new LootPackEntry(1.0, typeof(script.item.CurveShockBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.WindySpiritBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.FrameStrikeBook), "10d12+150", 40, 1, 1),

                    //new LootPackEntry(0.03, typeof(script.item.HolyDefender), "10d22+250", 40, 1, 1),     //15.02.26 삭제 GM티리엘
                    //new LootPackEntry(0.01, typeof(script.item.Kassandra), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.TaegkFan), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.Excalibur), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.DaeungDaegum), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.GrandeurPride), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.GoliathPlate), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.UnicornProtectoria), "10d22+250", 40, 1, 1),//15.01.24 삭제처리 gm티리엘
                    //new LootPackEntry(0.0025, typeof(script.item.SRing), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.0025, typeof(script.item.SAmulet), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.GrandeurPride), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.GoliathPlate), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.UnicornProtectoria), "10d22+250", 40, 1, 1),//15.01.24 삭제처리 gm티리엘
                    //new LootPackEntry(0.0025, typeof(script.item.SRing), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.0025, typeof(script.item.SAmulet), "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d300+20000", 40, 1, 1),
                });
            }
        }

        //public Necromancer()
        //    : base(205)
        //{
        //}
        //public Necromancer(Serial serial)
        //    : base(serial)
        //{
        //}
        public Necromancer(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Necromancer(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }

    public class LoupGarou : Monster
    {
        public override int INDEX { get { return 57; } }
        //public override string Name { get { return "Loup Garou"; } }
        //public override int HP { get { return 32000; } }
        //public override int Dam { get { return 1800; } }
        //public override int AC { get { return 3700; } }
        //public override int Hit { get { return 3500; } }
        //public override int XP { get { return 1050000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Undead; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(1.0, typeof(script.item.DiamondStick), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.Calypso), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.NewVVArmorPack, "10d22+250", 40, 1, 1),

                    new LootPackEntry(1.0, typeof(script.item.LionHeart), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.DragonBlade), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.Tomahawk), "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.GiantHammer), "10d22+250", 40, 1, 1),

                    new LootPackEntry(1.0, typeof(script.item.CurveShockBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.WindySpiritBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.FrameStrikeBook), "10d12+150", 40, 1, 1),

                    //new LootPackEntry(0.03, typeof(script.item.HolyDefender), "10d22+250", 40, 1, 1),     //15.02.26 삭제 GM티리엘
                    //new LootPackEntry(0.01, typeof(script.item.Kassandra), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.TaegkFan), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.Excalibur), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.DaeungDaegum), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.GrandeurPride), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.GoliathPlate), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.UnicornProtectoria), "10d22+250", 40, 1, 1),//15.01.24 삭제처리 gm티리엘
                    //new LootPackEntry(0.0025, typeof(script.item.SRing), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.0025, typeof(script.item.SAmulet), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.GrandeurPride), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.GoliathPlate), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.03, typeof(script.item.UnicornProtectoria), "10d22+250", 40, 1, 1),//15.01.24 삭제처리 gm티리엘
                    //new LootPackEntry(0.0025, typeof(script.item.SRing), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.0025, typeof(script.item.SAmulet), "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d300+20000", 40, 1, 1),
                });
            }
        }

        //public LoupGarou()
        //    : base(208)
        //{
        //}
        //public LoupGarou(Serial serial)
        //    : base(serial)
        //{
        //}
        public LoupGarou(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public LoupGarou(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }

    public class GhostKnight : Monster
    {
        public override int INDEX { get { return 33; } }
        //public override string Name { get { return "Ghost Knight"; } }
        //public override int HP { get { return 29000; } }
        //public override int Dam { get { return 3000; } }
        //public override int AC { get { return 5500; } }
        //public override int Hit { get { return 6000; } }
        //public override int XP { get { return 2000000; } }
        //public override int Color { get { return 0; } }
        //public override int SpawnTime { get { return 30000; } }
        public override int SpawnTime { get { return 3000000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(1.0, typeof(script.item.LaurelCrown), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.DiamondStick), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.Calypso), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.NewVVArmorPack, "10d22+250", 40, 1, 1),

                    new LootPackEntry(1.0, typeof(script.item.CurveShockBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.WindySpiritBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.FrameStrikeBook), "10d12+150", 40, 1, 1),

                    new LootPackEntry(1.0, typeof(script.item.LionHeart), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.DragonBlade), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.Tomahawk), "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.GiantHammer), "10d22+250", 40, 1, 1),
                });
            }
        }

        //public GhostKnight()
        //    : base(207)
        //{
        //}
        //public GhostKnight(Serial serial)
        //    : base(serial)
        //{
        //}
        public GhostKnight(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public GhostKnight(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }

    public class BoneTrio1 : Monster
    {
        public override int INDEX { get { return 9; } }
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
                    new LootPackEntry(1.0, typeof(script.item.DiamondStick), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.Calypso), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.0, LKCamelot.script.Packs.NewVVArmorPack, "10d22+250", 40, 1, 1),

                    new LootPackEntry(1.0, typeof(script.item.CurveShockBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.WindySpiritBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.FrameStrikeBook), "10d12+150", 40, 1, 1),

                    new LootPackEntry(1.0, typeof(script.item.Tomahawk), "10d22+250", 40, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.GiantHammer), "10d22+250", 40, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d300+22000", 40, 1, 1),
                });
            }
        }

        //public BoneTrio1()
        //    : base(215)
        //{
        //}
        //public BoneTrio1(Serial serial)
        //    : base(serial)
        //{
        //}
        public BoneTrio1(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public BoneTrio1(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }


    //리스폰 변경되면 삭제대상 (15.03.08)
    public class Devil : Monster
    {
        public override int INDEX { get { return 21; } }
        //public override string Name { get { return "Devil"; } }
        //public override int HP { get { return 50000; } }
        //public override int Dam { get { return 3000; } }
        //public override int AC { get { return 7000; } }
        //public override int Hit { get { return 6000; } }
        //public override int XP { get { return 5000000; } }
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 3000000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.01, typeof(script.item.LaurelCrown), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.04, typeof(script.item.DiamondStick), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.04, typeof(script.item.Calypso), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, LKCamelot.script.Packs.NewVVArmorPack, "10d22+250", 40, 1, 1),

                    new LootPackEntry(0.05, typeof(script.item.CurveShockBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.WindySpiritBook), "10d12+150", 40, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.FrameStrikeBook), "10d12+150", 40, 1, 1),

                    //new LootPackEntry(0.1, typeof(script.item.LionHeart), "10d12+150", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.DragonBlade), "10d12+150", 40, 1, 1),
                    //new LootPackEntry(0.2, typeof(script.item.Tomahawk), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.2, typeof(script.item.GiantHammer), "10d22+250", 40, 1, 1),

                    //new LootPackEntry(0.1, typeof(script.item.Kassandra), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.TaegkFan), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.Excalibur), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.DaeungDaegum), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.GrandeurPride), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.GoliathPlate), "10d22+250", 40, 1, 1),// 아머펙에 포함
                    //new LootPackEntry(0.01, typeof(script.item.CelestialArmor), "10d22+250", 40, 1, 1),// 아머펙에 포함
                    //new LootPackEntry(0.1, typeof(script.item.UnicornProtectoria), "10d22+250", 40, 1, 1),//15.01.24 삭제처리 gm티리엘
                    //new LootPackEntry(0.02, typeof(script.item.SRing), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.02, typeof(script.item.SAmulet), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(0.1, typeof(script.item.AngelicArmor), "10d22+250", 40, 1, 1),//15.01.24 삭제처리 gm티리엘
                    //new LootPackEntry(0.1, typeof(script.item.CelestialArmor), "10d22+250", 40, 1, 1),// 아머펙에 포함
                    //new LootPackEntry(0.01, typeof(script.item.AmityPlate), "10d22+250", 40, 1, 1),
                    //new LootPackEntry(15.0, typeof(script.item.Gold), "10d30+55000", 40, 1, 1),
                });
            }
        }

        //public Devil()
        //    : base(212)
        //{
        //}
        //public Devil(Serial serial)
        //    : base(serial)
        //{
        //}
        public Devil(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Devil(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
