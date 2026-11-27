using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using LKCamelot.script.monster;
namespace LKCamelot.model
{
    /// <summary>
    /// 불벽·전기벽의 몬스터 유인/가두기.
    /// 어그로가 끌린 몬스터는 벽 가운데로 들어가려 하고, 가운데가 차 있으면 벽 위에서 멈춰 선다.
    /// 가운데 몬스터가 죽으면 다른 몬스터가 그 자리를 채우고, 가끔은 벽 위의 몬스터가 가운데 몬스터를 공격한다.
    /// </summary>
    public class WallTrap
    {
        public enum Result
        {
            None,   // 벽과 무관. 평소대로 행동
            Hold,   // 벽 위에 자리 잡음. 이동하지 않고 붙어 있는 대상만 공격
            Acted,  // 이번 차례에 벽 쪽으로 이동했거나 가운데 몬스터를 공격함
        }

        public int Channel;
        public string Map;
        public Point2D Center;
        public List<Point2D> Tiles;
        public long Expire;
        public Player Owner;
        public script.spells.Buff Effect;

        static ConcurrentDictionary<int, WallTrap> Walls = new ConcurrentDictionary<int, WallTrap>();
        static int NextId = 0;

        /// <summary>벽을 등록한다. time은 지속시간(ms).</summary>
        public static void Register(Player owner, script.spells.Buff effect, int time)
        {
            if (owner == null || effect == null || effect.Loc == null)
                return;

            var wall = new WallTrap();
            wall.Channel = owner.m_Channel;
            wall.Map = effect.Map;
            wall.Center = new Point2D(effect.Loc.X, effect.Loc.Y);
            wall.Tiles = World.FireWalAttackRage(effect.Dir, effect.Loc);
            wall.Expire = Server.tickcount.ElapsedMilliseconds + time;
            wall.Owner = owner;
            wall.Effect = effect;

            Walls.TryAdd(System.Threading.Interlocked.Increment(ref NextId), wall);
        }

        // 시전자가 벽 버프를 아직 들고 있는지. 접속 종료나 사망으로 버프가 지워지면 벽 피해도 멈추므로 유인도 멈춘다.
        bool OwnerHolds()
        {
            if (Owner == null || !Owner.loggedIn)
                return false;
            foreach (var buff in Owner.m_Buffs.Values)
            {
                if (buff is script.spells.Wall && buff.BuffEffect == Effect)
                    return true;
            }
            return false;
        }

        static bool Same(Point2D a, int x, int y)
        {
            return a != null && a.X == x && a.Y == y;
        }

        /// <summary>
        /// 몬스터 AI 루프가 대상을 잡은 몬스터마다 호출한다.
        /// </summary>
        public static Result Process(Monster mob)
        {
            if (Walls.IsEmpty || mob.m_Loc == null)
                return Result.None;

            long now = Server.tickcount.ElapsedMilliseconds;

            // 유인 범위 안에서 가장 가까운 벽을 고른다.
            WallTrap wall = null;
            int best = int.MaxValue;
            foreach (var entry in Walls)
            {
                var w = entry.Value;
                WallTrap removed;
                if (now >= w.Expire)
                {
                    Walls.TryRemove(entry.Key, out removed);
                    continue;
                }
                if (w.Channel != mob.m_Channel || w.Map != mob.m_Map)
                    continue;

                int dist = World.Dist2d(mob.m_Loc, w.Center);
                if (dist > Settings.WallLureRange || dist >= best)
                    continue;

                if (!w.OwnerHolds())
                {
                    Walls.TryRemove(entry.Key, out removed);
                    continue;
                }
                wall = w;
                best = dist;
            }
            if (wall == null)
                return Result.None;

            int mx = mob.m_Loc.X, my = mob.m_Loc.Y;

            // 가운데에 선 몬스터는 벽이 끝날 때까지 그 자리를 지킨다.
            if (Same(wall.Center, mx, my))
                return Result.Hold;

            // 벽 주변의 다른 몬스터와 유저 위치
            var mobsNear = World.NewMonsters[mob.m_Channel].Values
                .Where(xe => xe != mob && xe.Alive && xe.m_Map == mob.m_Map && xe.m_Loc != null && World.Dist2d(xe.m_Loc, wall.Center) <= 4)
                .ToList();
            var playersNear = PlayerHandler.getSingleton().add[mob.m_Channel].Values
                .Where(xe => xe.loggedIn && xe.Map == mob.m_Map && xe.m_Loc != null && World.Dist2d(xe.m_Loc, wall.Center) <= 4)
                .ToList();

            Func<Point2D, bool> isFree = tile =>
                !mobsNear.Any(xe => Same(xe.m_Loc, tile.X, tile.Y)) &&
                !playersNear.Any(xe => Same(xe.m_Loc, tile.X, tile.Y));

            Monster centerMob = mobsNear.FirstOrDefault(xe => Same(xe.m_Loc, wall.Center.X, wall.Center.Y));
            bool centerFree = isFree(wall.Center);
            bool onTile = wall.Tiles.Any(xe => Same(xe, mx, my));

            if (onTile && !centerFree)
            {
                // 가운데가 차 있으면 벽 위에서 멈춰 선다. 가운데 몬스터와 붙어 있으면 가끔 그 몬스터를 공격한다.
                if (centerMob != null && now >= mob.NextInfightRoll &&
                    Math.Abs(centerMob.m_Loc.X - mx) <= 1 && Math.Abs(centerMob.m_Loc.Y - my) <= 1)
                {
                    mob.NextInfightRoll = now + mob.AttackSpeed;
                    if (Util.Random(100) < Settings.WallInfightChance)
                    {
                        mob.LastAction = now;
                        mob.AttackMonster(centerMob);
                        return Result.Acted;
                    }
                }
                return Result.Hold;
            }

            // 목적지: 가운데가 비었으면 가운데, 아니면 가장 가까운 빈 벽 칸
            Point2D dest = null;
            if (centerFree)
                dest = wall.Center;
            else
                dest = wall.Tiles.Where(isFree).OrderBy(xe => World.Dist2d(xe.X, xe.Y, mx, my)).FirstOrDefault();

            // 들어갈 칸이 없으면 평소대로 행동한다.
            if (dest == null)
                return Result.None;

            // 걷기 대기 중에는 다른 행동을 하지 않고 기다린다.
            if (now - mob.EffectiveWalkSpeed <= mob.LastAction)
                return Result.Acted;

            // AIStep이 좌표를 보정하며 값을 바꾸므로 복사본을 넘긴다.
            var newp = mob.AIStep(mob.m_Channel, model.Map.loadedmaps[mob.LoadMap], new Point2D(dest.X, dest.Y), mob.m_Map);
            if (newp.X == mx && newp.Y == my)
            {
                // 길이 막혔다. 벽 위에 있으면 그대로 서 있고, 밖이면 평소대로 행동한다.
                newp.Dispose();
                return onTile ? Result.Hold : Result.None;
            }

            mob.LastAction = now;
            mob.Face = World.GetAngle((short)newp.X, (short)newp.Y, (short)mx, (short)my);
            World.SendToAll(new QueDele(mob.m_Channel, mob.m_Map, new MoveSpriteWalk(mob.m_Serial, (short)mob.Face, (short)mx, (short)my).Compile()));
            mob.m_Loc.X = newp.X;
            mob.m_Loc.Y = newp.Y;
            newp.Dispose();

            return Result.Acted;
        }
    }
}
