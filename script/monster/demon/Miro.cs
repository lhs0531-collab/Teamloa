using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class Cyclops : Monster
    {
        public override int INDEX { get { return 19; } }
        //public override string Name { get { return "Cyclops"; } }
        //public override int HP { get { return 10000; } }
        //public override int Dam { get { return 850; } }
        //public override int AC { get { return 850; } }
        //public override int Hit { get { return 4000; } }
        //public override int XP { get { return 640000; } }           //3000 00
        //public override int Color { get { return 0; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                    new LootPackEntry(0.01, typeof(script.item.PowerStrong), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.01, typeof(script.item.GeneralSword), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.01, typeof(script.item.TitanMace), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.01, typeof(script.item.RocketMaul), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.01, typeof(script.item.StormShield), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.GrandStaff), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.GrandShield), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.GreatLance), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d1000+5000", 40, 1, 1),

                    //new LootPackEntry(0.01, typeof(script.item.DiamondArmor), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.GoliathPlate), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.0001, typeof(script.item.Calypso), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.0001, typeof(script.item.WarriorAngel), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.0001, typeof(script.item.DiamondStick), "15d10+225", 1, 1, 1),
                });
            }
        }

        //public Cyclops()
        //    : base(41)
        //{
        //}
        //public Cyclops(Serial serial)
        //    : base(serial)
        //{
        //}

        public Cyclops(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public Cyclops(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
        }

    }

    public class OneEye : Monster
    {
        public override int INDEX { get { return 74; } }
        //public override string Name { get { return "One Eye"; } }
        //public override int HP { get { return 14000; } }
        //public override int Dam { get { return 1200; } }
        //public override int AC { get { return 1000; } }
        //public override int Hit { get { return 5000; } }
        //public override int XP { get { return 840000; } }           //3000 00
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
                    new LootPackEntry(0.01, typeof(script.item.PowerStrong), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.01, typeof(script.item.GeneralSword), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.01, typeof(script.item.TitanMace), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.01, typeof(script.item.RocketMaul), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.01, typeof(script.item.StormShield), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.GrandStaff), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.GrandShield), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.05, typeof(script.item.GreatLance), "15d10+225", 1, 1, 1),
                    new LootPackEntry(15.0, typeof(script.item.Gold), "10d1000+5000", 40, 1, 1),

                    //new LootPackEntry(0.01, typeof(script.item.DiamondArmor), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.01, typeof(script.item.GoliathPlate), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.0001, typeof(script.item.Calypso), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.0001, typeof(script.item.WarriorAngel), "15d10+225", 1, 1, 1),
                    //new LootPackEntry(0.0001, typeof(script.item.DiamondStick), "15d10+225", 1, 1, 1),
                });
            }
        }

        //public OneEye()
        //    : base(225)
        //{
        //}
        //public OneEye(Serial serial)
        //    : base(serial)
        //{
        //}
        public OneEye(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
        }
        public OneEye(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
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
