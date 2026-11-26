using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
	//   Name : Santa Claus
	//  Image : 싼타클로스 (북미서버 ; 템하보 이미지)
	// Chance : 0.025~0.0075 (범위내에서)
	// Status : 용가리 그대로
	
    public class AccEvent : Monster
    {
        public override int INDEX { get { return 176; } }
        //public override string Name { get { return "Santa Claus"; } }
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
					//S링부터 만들어 놓은 모든 스텟 악세 (수치별로 드랍률 차증적용 왠만하면 36셋 나오게)
					//1,000,000 마돈 (확률 25%)
					new LootPackEntry(15, LKCamelot.script.Packs.AccessorySet36, "10d22+250", 40, 1, 1),
                    new LootPackEntry(10, typeof(script.item.BSRing), "10d22+250", 40, 1, 1),
                    new LootPackEntry(10, typeof(script.item.BSAmulet), "10d22+250", 40, 1, 1),					
					new LootPackEntry(10, LKCamelot.script.Packs.AccessorySet41, "10d22+250", 40, 1, 1),
					new LootPackEntry(10, LKCamelot.script.Packs.AccessorySet46, "10d22+250", 40, 1, 1),
					new LootPackEntry(10, LKCamelot.script.Packs.AccessorySet51, "10d22+250", 40, 1, 1),
					new LootPackEntry(10, LKCamelot.script.Packs.AccessorySet56, "10d22+250", 40, 1, 1),
					new LootPackEntry(10, LKCamelot.script.Packs.AccessorySet61, "10d22+250", 40, 1, 1),
					new LootPackEntry(10, LKCamelot.script.Packs.AccessorySet66, "10d22+250", 40, 1, 1),
					new LootPackEntry(10, LKCamelot.script.Packs.AccessorySet71, "10d22+250", 40, 1, 1),
                    new LootPackEntry(25, typeof(script.item.Gold), "d+1000000", 40, 1, 1),
                });
            }
        }

        public AccEvent()
            : base(9)
        {
        }
        public AccEvent(Serial serial)
            : base(serial)
        {
        }
        public AccEvent(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public AccEvent(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }

	//7승들을 위한 이벤트 몬스터
    public class SevenEvent : Monster
    {
        public override int INDEX { get { return 177; } }
        //public override string Name { get { return "Santa Claus"; } }
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
					//새마(기,위,샤)
					//카산, 엑칼, 대웅, 태극펜, 골드베스트, 해모수
					//전직책(기,위,샤,검)
					//올스텟 초기화물약 (있으면) => 없음
					//1,000,000 마돈 (확률 25%)
					new LootPackEntry(10, LKCamelot.script.Packs.CSWSFS, "10d22+250", 40, 1, 1),
					new LootPackEntry(10, LKCamelot.script.Packs.GreatWeaponPack, "10d22+250", 40, 1, 1),
                    new LootPackEntry(10, typeof(script.item.GoldVest), "10d22+250", 40, 1, 1),
					new LootPackEntry(10, typeof(script.item.ShamanBook), "10d22+250", 40, 1, 1),
					new LootPackEntry(10, typeof(script.item.WizardBook), "10d22+250", 40, 1, 1),
					new LootPackEntry(10, typeof(script.item.SwordmanBook), "10d22+250", 40, 1, 1),
					new LootPackEntry(10, typeof(script.item.KnightBook), "10d22+250", 40, 1, 1),
					new LootPackEntry(10, typeof(script.item.Haemosu), "10d22+250", 40, 1, 1),
					new LootPackEntry(25, typeof(script.item.Gold), "d+1000000", 40, 1, 1),
                });
            }
        }

        public SevenEvent()
            : base(9)
        {
        }
        public SevenEvent(Serial serial)
            : base(serial)
        {
        }
        public SevenEvent(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public SevenEvent(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : this(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }
    }	

	//유니크템 드랍되는 이벤트 몬스터
    public class HiddenEvent : Monster
    {
        public override int INDEX { get { return 178; } }
        //public override string Name { get { return "Santa Claus"; } }
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
					//히트셋 (옵션수정필요) <- 아이템 골램드랍이 되있는데  철회해야됨?
					//U셋
					//BS셋
					//숏소드, 에페, 크라운, 칠성검, 바타소드 <- 1순위
					//승갑(기,위,샤,검) <- 2순위
					//골리앗, 가고일, 다갑, 용비  <- 3순위
					//1,000,000 마돈 (확률 25%)
					new LootPackEntry(0.01, typeof(script.item.UGloryRing), "10d22+250", 40, 1, 1),
					new LootPackEntry(0.01, typeof(script.item.UGloryAmulet), "10d22+250", 40, 1, 1),
					new LootPackEntry(10, typeof(script.item.GoldPlate), "10d22+250", 40, 1, 1),
					new LootPackEntry(10, typeof(script.item.DarkPlate), "10d22+250", 40, 1, 1),
					new LootPackEntry(10, typeof(script.item.Protectoria), "10d22+250", 40, 1, 1),
					new LootPackEntry(10, typeof(script.item.PythonScale), "10d22+250", 40, 1, 1),
					new LootPackEntry(15, LKCamelot.script.Packs.StatPotions, "10d22+250", 40, 1, 1),
					new LootPackEntry(10, LKCamelot.script.Packs.NewVVArmorPack, "10d22+250", 40, 1, 1),
					new LootPackEntry(10, LKCamelot.script.Packs.GreatArmorPack, "10d22+250", 40, 1, 1),
					new LootPackEntry(10, LKCamelot.script.Packs.ItemGolemPark, "10d22+250", 40, 1, 1),
					new LootPackEntry(10, LKCamelot.script.Packs.ItemHardBoildPark, "10d22+250", 40, 1, 1),
					new LootPackEntry(50, typeof(script.item.Gold), "d+1000000", 40, 1, 1),
                });
            }
        }

        public HiddenEvent()
            : base(9)
        {
        }
        public HiddenEvent(Serial serial)
            : base(serial)
        {
        }
        public HiddenEvent(Serial temp, int Channel, int x, int y, string map)
            : this(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public HiddenEvent(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
