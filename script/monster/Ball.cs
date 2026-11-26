using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.monster
{
    public class Ball : Monster
    {
        public Point2D Loc = null;
        public override int INDEX { get { return 158; } }
        //public override string Name { get { return "축구공"; } }
        //public override int HP { get { return int.MaxValue; } }
        //public override int Dam { get { return int.MaxValue; } }
        //public override int AC { get { return int.MaxValue; } }
        //public override int Hit { get { return int.MaxValue; } }
        //public override int XP { get { return 0; } }
        //public override int Color { get { return 0; } }
        //public override int WalkSpeed { get { return 1200; } }
        public override int SpawnTime { get { return 30000; } }
        public override Race Race { get { return Race.Demon; } }

        public override LootPack Loot
        {
            get
            {
                return new LootPack(new LootPackEntry[]
                {
                });
            }
        }

        //public Ball()
        //    : base(1)
        //{
        //}
        //public Ball(Serial serial)
        //    : base(serial)
        //{
        //}
        public Ball(Serial temp, int Channel, int x, int y, string map)
            : base(temp)
        {
            this.m_Channel = Channel;
            this.SpawnLoc_List.Add(new Point2D(x, y));
            m_Map = map;
            m_Serial = temp;
            this.m_Loc = new Point2D(x, y);
            this.Alive = true;
        }
        public Ball(Serial temp, int Channel, string map, List<Point2D> list, bool RandomGan = true)
            : base(temp)
        {
            this.m_Channel = Channel;
            m_Serial = temp;
            this.m_Map = map;
            this.SpawnLoc_List.AddRange(list);
            this.RandomSpawn = RandomGan;
            this.m_Loc = new Point2D(list[0].X, list[0].Y);
            this.Alive = true;
        }


        public override void TakeDamage(Player player)
        {
            this.Face = player.Face;
            Point2D temp = World.AddMove(this, 5);
            Loc = new Point2D(temp.X, temp.Y);
        }
        public override void TakeDamage(Player player, spells.Spell spell)
        {
        }
    }
}
