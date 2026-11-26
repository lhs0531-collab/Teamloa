using LKCamelot.model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class BaseInstance : BaseNPC
    {
        public Player parSer;

        public int Hits = 0;                        //얼마나 때렸느냐
        public int Time = 10 * Settings.Second;

        public override string Name { get { return ""; } }

        byte sprite;
        public override int AliveTime {
            get
            {
                if (aStartFrames != sprite)
                {
                    sprite = aStartFrames;
                    World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new DeleteObject(this.m_Serial).Compile()));
                    World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new CreateGem(this).Compile()));
                }

                return Time; 
            }        
        }
        public override bool Alive
        {
            get { return base.Alive; }
            set {
                if (base.Alive == value) return;

                base.Alive = value;
                if (value == false)
                    World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new DeleteObject(this.m_Serial).Compile()));

            }
        }
        
        public override byte aStartFrames
        {
            get
            {
                long gap = (LastSpawn + Time) - Server.tickcount.ElapsedMilliseconds;
                if (gap > (2 * Settings.Minute))
                    return 8;
                else if (gap > (30 * Settings.Second))
                    return 4;
                else
                    return 0;

            }
        }           // 시작 프레임수

        public virtual void SetTime(int time)
        {
            Time += time;
            if (aStartFrames != sprite)
            {
                sprite = aStartFrames;
                World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new DeleteObject(this.m_Serial).Compile()));
                World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new CreateGem(this).Compile()));
            }
        }

        public BaseInstance(Player player, Point2D Loc, Serial serial) : base(serial)
        {
            this.m_Channel = player.m_Channel;
            this.LastSpawn = Server.tickcount.ElapsedMilliseconds;
            this.sprite = aStartFrames;
            this.parSer = player;
            this.m_Loc = new Point2D(Loc.X, Loc.Y);
            this.m_Map = player.Map;
        }

        public override void TakeDamage(Player player)
        {

        }
        public virtual void Dispose()
        {

        }
    }
}
