using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public interface ISingle
    {
    }
    public interface Wall
    {
        
    }
    public enum BuffCase
    {
        ManaAsHP = 1,
        ReflectDamage = 2,
        Triple = 4,
        Twister = 8,
        Freezing = 16,
        Wall = 32,
        Slow = 64,
    }
    
    public class Buff
    {
        public float fAC = 0f, fDam = 0f, fHit = 0f, fACpl = 0f, fDampl = 0f, fHitpl = 0f;
        public int AC = 0, Dam = 0, Hit = 0, ACpl = 0, Dampl = 0, Hitpl = 0;

        //public int Time = 180 * Settings.Second;
        //public int TimePI = 0;

        public Point2D Loc;
        public string Map;
        public short Dir;

        public int Time = Int32.MaxValue;
        public int TimePI = 0;
        public BuffCase BuffType;

        // 이동속도 감소율(%). BuffType이 Slow일 때 몬스터의 걷기 간격에 반영된다. (Monster.EffectiveWalkSpeed)
        public int MoveSlowPercent = 0;

        // 지속 피해 주기(ms). 0보다 크면 이 버프가 걸린 몬스터는 주기마다 마법 피해를 받는다. (Monster.ProcessDot)
        public int TickInterval = 0;
    }
    
	public class Spell
	{
        public long CastTime;
        public virtual string Name { get { return ""; } }

        public Player Caster;
        public int m_Level = 1, SLevel2 = 1, Slot;
        public long Cooldown;
        public library.Class ClassReq = 0;
        public int LevelReq, LevelReqPl, StrReq, StrReqPl, MenReq, MenReqPl, DexReq, DexReqPl;

        public virtual LKCamelot.library.MagicType mType { get { return 0; } }
        public virtual int SpellLearnedIcon { get { return 0; } }
        public virtual SpellSequence Seq { get { return null; } }
        public virtual Buff BuffEffect { get { return null; } }
        //public virtual LKCamelot.library.Class ClassReq { get { return 0; } }
        public virtual int Range { get { return 7; } }
        public virtual int RecastTime { get { return 300; } }
        public virtual script.item.Item SpellBook { get { return null; } }

        //데미지
        public virtual int DamBase { get { return 0; } }
        public virtual int DamPl { get { return 0; } }
        
        //마나 소모
        public virtual int ManaCost { get { return 0; } }
        public virtual int ManaCostPl { get { return 0; } }

        //가중치
        public virtual int menCoff { get { return 16; } }
        public virtual int strCoff { get { return 5000; } }
        public virtual int dexCoff { get { return 5000; } }

        //최대 권수
        public virtual int Limit { get { return 12; } }

        public Spell(script.item.BaseSpellbook item)
        {
            ClassReq = item.ClassReq;

            LevelReq = item.LevelReq;
            LevelReqPl = item.LevelReqPl;
            StrReq = item.StrReq;
            StrReqPl = item.StrReqPl;
            MenReq = item.MenReq;
            MenReqPl = item.MenReqPl;
            DexReq = item.DexReq;
            DexReqPl = item.DexReqPl;
        }
        public int Level
        {
            get 
            {
                int LowLevel;
                if (Settings.GMS.Contains(Caster.Name))
                    return m_Level;

                for (LowLevel = 0; LowLevel <= m_Level; LowLevel++)
                {
                    if (Caster.GetStat("men") < MenReq + (MenReqPl * LowLevel)
                    || Caster.GetStat("dex") < DexReq + (DexReqPl * LowLevel)
                    || Caster.GetStat("str") < StrReq + (StrReqPl * LowLevel)
                    || Caster.Level < LevelReq + (LevelReqPl * LowLevel))
                    {
                        return LowLevel;
                    }
                }
                return m_Level;
            }
            set
            {
                if (m_Level == value) return;
                m_Level = value;
            }
        }
        public int RealManaCost(Player play)
        {
                int temp = 0;
                if (ManaCost >= 0)
                    temp = ManaCost + (ManaCostPl * (Level-1));
                if (ManaCost < 0)                               // 0보다 작을 경우 퍼센트 적용
                {
                    double tt = (ManaCost*-0.01);               
                    tt = tt + ((ManaCostPl * (Level-1)) * -0.01);
                    if (this.Name == "DEMON DEATH")
                    {
                        temp = (int)(tt * play.HP);
                    }
                    else
                    {
                        temp = (int)(tt * play.MP);
                    }
                }
                return temp;
        }

        public virtual void CheckLevelUp(Player player)
        {
            if (Util.Random(0, (50 * SLevel2)) == 5)
            {
                SLevel2++;
                player.client.SendPacket(new CreateSlotMagic2(this).Compile());
                int mobile = Serial.NewMobile;
                World.SendToAll(new QueDele(player.m_Channel, player.m_Map, new CreateMagicEffect(mobile, 1, (short)player.m_Loc.X, (short)player.m_Loc.Y, new byte[] { 4, 0, 0, 0, 0, 0, 0, 0, 0, 85 }, 0).Compile()));
                var tmp = new QueDele(LKCamelot.Server.tickcount.ElapsedMilliseconds + 2000, player.m_Channel, player.m_Map, new DeleteObject(mobile).Compile());
                tmp.tempser = mobile;
                World.TickQue.Add(tmp);
            }
        }



        /// <summary>
        /// 범위 마법
        /// </summary>
        /// <param name="Serial">시리얼</param>
        /// <param name="PK">피케이사용 여부</param>
        /// <returns></returns>
        protected List<Object> TargetDistance(Player play, int width, int Dist, bool PK)
        {
            List<Point2D> List = World.FireWalAttackRage(play.Face, play.Loc, width);
            List<Object> Result = new List<Object>();
            foreach (var point in List)
            {
                Result.AddRange(
                    World.NewMonsters[play.m_Channel].Values.Where(xe => xe.Alive && xe.m_Map == play.Map &&
                            World.GetAngle((short)xe.m_Loc.X, (short)xe.m_Loc.Y, (short)point.X, (short)point.Y) == play.Face &&
                            World.Dist2d(point.X, point.Y, xe.m_Loc.X, xe.m_Loc.Y) <= Dist).ToList()
                    );
            }
            return Result;
        }
        /// <summary>
        /// 누른 좌표를 시전자에서 그 방향으로 Settings.WaveReach칸까지 늘린 좌표를 돌려준다.
        /// 뻗어 나가는 마법(프레임웨이브, 썬더스트록)이 누른 곳에서 끊기지 않고 화면 끝까지 가게 한다.
        /// </summary>
        protected Point2D ExtendCast(Player player, short castx, short casty)
        {
            int dx = castx - player.Loc.X;
            int dy = casty - player.Loc.Y;
            int longest = Math.Max(Math.Abs(dx), Math.Abs(dy));
            if (longest == 0)
                return new Point2D(castx, casty);

            int x = player.Loc.X + dx * Settings.WaveReach / longest;
            int y = player.Loc.Y + dy * Settings.WaveReach / longest;

            // 맵 밖으로 나가지 않게 자른다.
            int maxX = short.MaxValue, maxY = short.MaxValue;
            try
            {
                var full = model.Map.FullMaps[player.m_Channel].Where(xe => xe.Key == player.Map).FirstOrDefault().Value;
                if (full != null && model.Map.loadedmaps.ContainsKey(full.Filename))
                {
                    maxX = model.Map.loadedmaps[full.Filename].sizeX - 1;
                    maxY = model.Map.loadedmaps[full.Filename].sizeY - 1;
                }
            }
            catch { }

            x = Math.Max(0, Math.Min(maxX, x));
            y = Math.Max(0, Math.Min(maxY, y));
            return new Point2D(x, y);
        }

        /// <summary>
        /// 단일 타켓팅을 위한 함수
        /// </summary>
        /// <param name="Serial">시리얼</param>
        /// <param name="PK">피케이사용 여부</param>
        /// <returns></returns>
        protected BaseObject SerialTarget(int Channel, int Serial, bool PK)
        {
            BaseObject target = null;
            target = World.NewMonsters[Channel].Where(xe => xe.Value.Alive && xe.Value.m_Serial == Serial).FirstOrDefault().Value;
            
            if (PK == true && target == null)
                target = PlayerHandler.getSingleton().add[Channel].Where(xe => xe.Value.m_Serial == Serial).FirstOrDefault().Value;

            if (target == null)
                return null;
            return target;
        }

        protected Object DirTarget(int Channel, Point2D Loc, string Map, int Dir = 12, bool PK = false) 
        {            
            Object target = null;
            target = World.NewMonsters[Channel].Where(xe => xe.Value.Alive && 
                                             xe.Value.m_Map == Map &&
                                             xe.Value.m_Loc.X == Loc.X && xe.Value.m_Loc.Y == Loc.Y &&
                                             World.Dist2d(xe.Value.m_Loc, Loc) <= Dir
                                             ).FirstOrDefault().Value;

            if (PK == true && target == null)
                target = PlayerHandler.getSingleton().add[Channel].Where(xe => 
                                                                xe.Value.loggedIn &&
                                                                xe.Value.m_Map == Map &&
                                                                xe.Value.Loc.X == Loc.X && xe.Value.Loc.Y == Loc.Y &&
                                                                World.Dist2d(xe.Value.Loc, Loc) <= Dir).FirstOrDefault().Value;

            if (target == null)
                return null;
            return target;
        }
        protected List<BaseObject> RangeTarget(Player player, int x, int y)
        {
            List<BaseObject> Target = new List<BaseObject>();
            Target.AddRange(World.NewMonsters[player.m_Channel].Where(xe => xe.Value.m_Map != null
                                                    && xe.Value.m_Loc != null
                                                    && xe.Value.m_Map == player.Map
                                                    && World.Dist2d(xe.Value.m_Loc.X, xe.Value.m_Loc.Y, x, y) <= this.Range
                                                    && xe.Value.Alive).Select(xe=>xe.Value).ToList());
            if (player.PK)
            {
                Target.AddRange(PlayerHandler.getSingleton().add[player.m_Channel].Where(xe => xe.Value.loggedIn 
                                                                        && xe.Value.Map != null
                                                                        && xe.Value.Map == player.Map
                                                                        && xe.Value.m_Serial != player.m_Serial
                                                                        && World.Dist2d(xe.Value.Loc.X, xe.Value.Loc.Y, x, y) <= this.Range).Select(xe => xe.Value));
            }

            return Target;
        }

        protected script.item.Item SerialItem(int castonid)
        {
            item.Item item;
            World.NewItems.TryGetValue(castonid, out item);
            return item;
        }
        protected Player SerialPlayerTarget(int Channel, int castonid)
        {
            return PlayerHandler.getSingleton().add[Channel].Where(xe => xe.Value.loggedIn && xe.Value.m_Serial == castonid).FirstOrDefault().Value;
        }
        /// <summary>
        /// 버프 대상 찾기. 시전자와 같은 맵의 접속자만 대상으로 한다.
        /// 1) 시리얼이 맞는 유저가 있으면 그 유저.
        /// 2) 없으면 누른 좌표가 다른 유저의 몸통 범위(Settings.BuffTarget*) 안일 때 가장 가까운 유저.
        /// 3) 그 외(빈 땅 등)에는 시전자 자신.
        /// </summary>
        protected Player FindBuffTarget(Player caster, int castonid, int x, int y)
        {
            var candidates = PlayerHandler.getSingleton().add[caster.m_Channel].Values
                .Where(xe => xe.loggedIn && xe.Map == caster.Map && xe.Loc != null)
                .ToList();

            if (castonid != 0)
            {
                var bySerial = candidates.FirstOrDefault(xe => xe.m_Serial == castonid);
                if (bySerial != null)
                    return bySerial;
            }

            // 캐릭터 그림은 서 있는 칸보다 위쪽으로 그려지므로, 몸통을 누르면 발밑 칸보다 위(Y가 작은 쪽) 좌표가 온다.
            var near = candidates
                .Where(xe => xe != caster
                    && Math.Abs(xe.Loc.X - x) <= Settings.BuffTargetRangeX
                    && (xe.Loc.Y - y) <= Settings.BuffTargetRangeUp
                    && (y - xe.Loc.Y) <= Settings.BuffTargetRangeDown)
                .OrderBy(xe => World.Dist2d(xe.Loc.X, xe.Loc.Y, x, y))
                .FirstOrDefault();

            return near ?? caster;
        }

        protected Player SerialPlayerTarget(int Channel, int castonid, int x, int y)
        {
            return PlayerHandler.getSingleton().add[Channel].Where(xe => xe.Value.loggedIn && 
                                                                        (xe.Value.m_Serial == castonid || (xe.Value.Loc.X == x && xe.Value.Loc.Y == y))).FirstOrDefault().Value;
        }




        /// <summary>
        /// 마법 사용 함수
        /// </summary>
        /// <param name="player">시전자</param>
        /// <param name="Target">타겟팅</param>
        public virtual void UseSpell(Player player)
        {
        }
        public virtual void UseSpell(Player player, short X, short Y)
        {

        }
        public virtual void UseSpell(Player player, int castonid, short castx, short casty)
        {
        }
    }
}