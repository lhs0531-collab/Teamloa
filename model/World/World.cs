using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Timers;
using System.IO;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;
using LKCamelot.library;
using System.Diagnostics;

namespace LKCamelot.model
{
    public class MyPathNode : IPathNode<Object>, IDisposable
    {
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public Boolean IsWall { get; set; }
        public bool SpawnP;

        public bool IsWalkable(Object unused)
        {
            return !IsWall;
        }

        public MyPathNode(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void Dispose()
        {

        }
    }

    public class MySolver<TPathNode, TUserContext> : SpatialAStar<TPathNode, TUserContext> where TPathNode : IPathNode<TUserContext>, IDisposable
    {
        protected override Double Heuristic(PathNode inStart, PathNode inEnd)
        {
            return Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y);
        }

        protected override Double NeighborDistance(PathNode inStart, PathNode inEnd)
        {
            return Heuristic(inStart, inEnd);
        }

        public MySolver(TPathNode[,] inGrid)
            : base(inGrid)
        {
        }

        public void Dispose()
        {
            base.Dispose();
        }
    }

    public class QueDele
    {
        public int Channel;
        public string Map;
        public byte[] Packet;
        public long Serial;
        public int tempser;

        public QueDele(int Channel, string map, byte[] packet)
        {
            this.Channel = Channel;
            this.Map = map;
            this.Packet = packet;
            this.Serial = -1;
        }
        public QueDele(string map, byte[] packet)
        {
            this.Channel = -1;
            this.Map = map;
            this.Packet = packet;
            this.Serial = -1;
        }
        public QueDele(long ser, string map, byte[] packet)
        {
            this.Channel = -1;
            this.Map = map;
            this.Packet = packet;
            this.Serial = ser;
        }
        public QueDele(long ser, int Channel, string map, byte[] packet)
        {
            this.Channel = Channel;
            this.Map = map;
            this.Packet = packet;
            this.Serial = ser;
        }

        public QueDele()
        {
        }
    }

    public class World
    {
        public static long QuestTime { get; set; }
        public static World singleton = new World();
        public static World getSingleton()
        {
            if (singleton == null)
                singleton = new World();

            return singleton;
        }


        public static ConcurrentDictionary<Serial, LKCamelot.script.item.Item> NewItems;
        public static ConcurrentDictionary<int, LKCamelot.script.npc.BaseNPC>[] NewNpcs;
        public static ConcurrentDictionary<int, LKCamelot.script.npc.AutoShopNPC> NewAutoShop;
        public static ConcurrentDictionary<Serial, LKCamelot.script.monster.Monster>[] NewMonsters;
        public static ConcurrentDictionary<Serial, LKCamelot.script.item.AuctionItem> NewAuctions;
        public static ConcurrentDictionary<string, model.BaseGuild> NewGuilds = new ConcurrentDictionary<string, model.BaseGuild>();
        public static List<Serial> DeletePlayer;

        public static ConcurrentDictionary<Serial, Serial> m_MObjects;
        public PlayerHandler handler;
        public static Server w_server;
        public static Task spawner;

        public static Task[] spawner2;

        public static List<QueDele> TickQue = new List<QueDele>();

        public void Tick()
        {
            QueDele tq;
            List<QueDele> TickQued = new List<QueDele>();
            try
            {
                TickQued = TickQue.ToList();
            }
            catch { };

            foreach (var tick in TickQued)
            {
                if (tick == null)
                {
                    TickQue.RemoveAt(0);
                    continue;
                }

                if (tick.Serial < LKCamelot.Server.tickcount.ElapsedMilliseconds)
                {
                    SendToAll(tick);
                    TickQue.Remove(tick);
                    //  var ttt = tick.Packet.Skip(1).Take(4);
                    Serial temp;
                    if (m_MObjects.ContainsKey(tick.tempser))
                        m_MObjects.TryRemove(tick.tempser, out temp);
                }
            }

            foreach (var playe in w_server.OnlineConnections)
            {
                if (playe.Client == null || playe.Client.player == null)
                    continue;

                KeyValuePair<string, Player> play =
                    new KeyValuePair<string, Player>(playe.Client.player.Name, playe.Client.player);
                if (play.Value == null || play.Value.loggedIn == false)
                    continue;

                if ((play.Value.client.keepalive + 15000) < Server.tickcount.ElapsedMilliseconds)
                    w_server.Disconnect(play.Value.client.connection);

                if (play.Value.client.connection == null || play.Value.client.connection.Socket == null)
                    w_server.Disconnect(play.Value.client.connection);

                if (play.Value.AutoHit && LKCamelot.Server.tickcount.ElapsedMilliseconds - play.Value.AttackSpeed > play.Value.autohittick
                    && LKCamelot.Server.tickcount.ElapsedMilliseconds - play.Value.AttackSpeed > play.Value.client.LastAttack)
                {
                    play.Value.autohittick = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                    play.Value.client.LastAttack = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                    World.SendToSoundRange(new QueDele(play.Value.m_Channel, play.Value.Map, new EffectSound(library.EffectSound.Attack).Compile()), play.Value);
                    World.SendToRange(new QueDele(play.Value.m_Channel, play.Value.Map, new SwingAnimation(play.Value.m_Serial, play.Value.Face).Compile()), play.Value);
                    play.Value.client.combatHandler.HandleMelee(play.Value, play.Value.Face);
                }
            }

        }

        public static void spawnrun3(int i)
        {
            while (true)
            {
                if (NewMonsters == null)
                    continue;

                foreach (var mob in NewMonsters[i])
                {
                    try
                    {
                        if (mob.Value.MonsterData == null)
                            continue;

                        #region Buff
                        foreach (var buff in mob.Value.m_Buffs)
                        {
                            long time = (buff.Key - Server.tickcount.ElapsedMilliseconds) / Settings.Second;

                            if (time <= 0)
                            {
                                script.spells.Spell temp;
                                mob.Value.m_Buffs.TryRemove(buff.Key, out temp);
                                World.SendToAll(new QueDele(mob.Value.m_Channel, mob.Value.m_Map, new SetObjectEffects(mob.Value).Compile()));
                            }
                        }
                        #endregion

                        if (mob.Value.Alive == false)
                        {
                            if (Server.tickcount.ElapsedMilliseconds - mob.Value.SpawnTime > mob.Value.LastSpawn)
                            {
                                mob.Value.Target = 0;
                                mob.Value.TargetP = null;
                                mob.Value.m_Buffs.Clear();

                                mob.Value.Alive = true;
                            }
                            else
                                continue;
                        }
                        else
                        {
                            #region 축구공
                            if ((mob.Value is script.monster.Ball))
                            {
                                if ((mob.Value as script.monster.Ball).Loc == null)
                                    continue;
                                mob.Value.LastAction = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                                var newp = mob.Value.AIStep(i, Map.loadedmaps[mob.Value.LoadMap], (mob.Value as script.monster.Ball).Loc, mob.Value.m_Map);

                                if (newp.X == mob.Value.m_Loc.X && newp.Y == mob.Value.m_Loc.Y)
                                    continue;

                                mob.Value.Face = World.GetAngle((short)newp.X, (short)newp.Y, (short)mob.Value.m_Loc.X, (short)mob.Value.m_Loc.Y);

                                //    Point2D newStep = AddMove(mob.Value);

                                World.SendToAll(new QueDele(mob.Value.m_Channel, mob.Value.m_Map, new MoveSpriteWalk(mob.Value.m_Serial, (short)mob.Value.Face, (short)mob.Value.m_Loc.X, (short)mob.Value.m_Loc.Y).Compile()));

                                mob.Value.m_Loc.X = newp.X;
                                mob.Value.m_Loc.Y = newp.Y;

                                newp.Dispose();
                                newp = null;

                                continue;
                            }
                            #endregion
                            
                            var PlayerList = PlayerHandler.getSingleton().add[i].Where(xe => xe.Value.loggedIn && xe.Value.Map == mob.Value.m_Map);
                            if (PlayerList.Count() == 0)
                                continue;

                            if (mob.Value.m_Buffs.Where(xe => xe.Value.GetType() == typeof(script.spells.Oblivion)).Any())
                                continue;

                            // 아발론
                            if (mob.Value is script.monster.BaseNode)
                            {
                                if (mob.Value is script.monster.StrAbalStone || mob.Value is script.monster.MenAbalStone ||
                                    mob.Value is script.monster.DexAbalStone || mob.Value is script.monster.VitAbalStone)
                                {
                                    if (mob.Value.Alive == true && (mob.Value.LastSpawn + mob.Value.SpawnTime + mob.Value.AliveTime < Server.tickcount.ElapsedMilliseconds))
                                        mob.Value.Alive = false;
                                }
                                continue;
                            }

                            if (mob.Value.Target == 0)
                            {
                                var LocalPlayers = PlayerList.Where(xe => xe.Value.client != null && xe.Value.m_Channel == mob.Value.m_Channel && World.Dist2d(xe.Value.Loc, mob.Value.m_Loc) <= mob.Value.MinAggro).OrderBy(xe => xe.Value.Level).FirstOrDefault().Value;

                                if (LocalPlayers == null)
                                {
                                    mob.Value.Target = 0;
                                    mob.Value.TargetP = null;
                                    continue;
                                }
                                else
                                {
                                    mob.Value.Target = (LocalPlayers as Player).m_Serial;
                                    mob.Value.TargetP = (LocalPlayers as Player);
                                }

                            }
                            else
                            {
                                if (!mob.Value.TargetP.loggedIn || mob.Value.TargetP.Map != mob.Value.m_Map || mob.Value.TargetP.m_Channel != mob.Value.m_Channel)
                                {
                                    mob.Value.Target = 0;
                                    mob.Value.TargetP = null;
                                    continue;
                                }
                            }

                            var mobdist = World.Dist2d(mob.Value.m_Loc.X, mob.Value.m_Loc.Y, mob.Value.TargetP.Loc.X, mob.Value.TargetP.Loc.Y);

                            if (mobdist >= 2 && mobdist < mob.Value.MaxAggro && Server.tickcount.ElapsedMilliseconds - mob.Value.WalkSpeed > mob.Value.LastAction)
                            {
                                //이동
                                mob.Value.LastAction = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                                var newp = mob.Value.AIStep(Map.loadedmaps[mob.Value.LoadMap], mob.Value.TargetP);

                                if (newp.X == mob.Value.m_Loc.X && newp.Y == mob.Value.m_Loc.Y)
                                    continue;

                                mob.Value.Face = World.GetAngle((short)newp.X, (short)newp.Y, (short)mob.Value.m_Loc.X, (short)mob.Value.m_Loc.Y);

                                //    Point2D newStep = AddMove(mob.Value);

                                World.SendToAll(new QueDele(mob.Value.m_Channel, mob.Value.m_Map, new MoveSpriteWalk(mob.Value.m_Serial, (short)mob.Value.Face, (short)mob.Value.m_Loc.X, (short)mob.Value.m_Loc.Y).Compile()));

                                mob.Value.m_Loc.X = newp.X;
                                mob.Value.m_Loc.Y = newp.Y;


                                newp.Dispose();
                                newp = null;

                                if (mobdist <= 1)
                                {
                                    mob.Value.LastAction = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                                    mob.Value.Attack(mob.Value.TargetP);

                                    World.SendToAll(new QueDele(mob.Value.m_Channel, mob.Value.m_Map, new SwingAnimation(mob.Value.m_Serial, World.AttackRange((short)mob.Value.TargetP.Loc.X, (short)mob.Value.TargetP.Loc.Y, (short)mob.Value.m_Loc.X, (short)mob.Value.m_Loc.Y)).Compile()));
                                }
                            }
                            else if (mobdist <= 1 && Server.tickcount.ElapsedMilliseconds - mob.Value.AttackSpeed > mob.Value.LastAction)
                            {
                                mob.Value.LastAction = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                                mob.Value.Attack(mob.Value.TargetP);

                                World.SendToAll(new QueDele(mob.Value.m_Channel, mob.Value.m_Map, new SwingAnimation(mob.Value.m_Serial, World.AttackRange((short)mob.Value.TargetP.Loc.X, (short)mob.Value.TargetP.Loc.Y, (short)mob.Value.m_Loc.X, (short)mob.Value.m_Loc.Y)).Compile()));
                            }
                            else if (mobdist > mob.Value.MaxAggro)
                            {
                                mob.Value.Target = 0;
                                mob.Value.TargetP = null;
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(string.Format("spawner fail :{0} {1}/{2}:{3}\n{4}", mob.Value.Name, mob.Value.m_Map, mob.Value.m_Loc.X, mob.Value.m_Loc.Y, e.ToString()));
                        mob.Value.Target = 0;
                        mob.Value.TargetP = null;
                        mob.Value.Alive = false;
                    }
                };
                Thread.Sleep(100);
            }
        }

        public static void spawnrun4(int i)
        {
            while (true)
            {
                Parallel.ForEach(NewMonsters[i], mob =>
                {
                    if (PlayerHandler.getSingleton().add[i].Where(xe => xe.Value.loggedIn && xe.Value.Map == mob.Value.m_Map).Count() == 0)
                        return;

                    if (mob.Value.MonsterData == null)
                        return;
                    try
                    {
                        #region Buff
                        foreach (var buff in mob.Value.m_Buffs)
                        {
                            long time = (buff.Key - Server.tickcount.ElapsedMilliseconds) / Settings.Second;

                            if (time <= 0)
                            {
                                script.spells.Spell temp;
                                mob.Value.m_Buffs.TryRemove(buff.Key, out temp);
                                World.SendToAll(new QueDele(mob.Value.m_Channel, mob.Value.m_Map, new SetObjectEffects(mob.Value).Compile()));
                            }
                        }
                        #endregion


                        #region 축구공
                        if ((mob.Value is script.monster.Ball))
                        {
                            if ((mob.Value as script.monster.Ball).Loc == null)
                                return;
                            mob.Value.LastAction = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                            var newp = mob.Value.AIStep(i, Map.loadedmaps[mob.Value.LoadMap], (mob.Value as script.monster.Ball).Loc, mob.Value.m_Map);

                            if (newp.X == mob.Value.m_Loc.X && newp.Y == mob.Value.m_Loc.Y)
                                return;

                            mob.Value.Face = World.GetAngle((short)newp.X, (short)newp.Y, (short)mob.Value.m_Loc.X, (short)mob.Value.m_Loc.Y);

                            //    Point2D newStep = AddMove(mob.Value);

                            World.SendToAll(new QueDele(mob.Value.m_Channel, mob.Value.m_Map, new MoveSpriteWalk(mob.Value.m_Serial, (short)mob.Value.Face, (short)mob.Value.m_Loc.X, (short)mob.Value.m_Loc.Y).Compile()));

                            mob.Value.m_Loc.X = newp.X;
                            mob.Value.m_Loc.Y = newp.Y;

                            newp.Dispose();
                            newp = null;

                            return;
                        }
                        #endregion

                        if (mob.Value.Alive == false)
                        {
                            if (Server.tickcount.ElapsedMilliseconds - mob.Value.SpawnTime > mob.Value.LastSpawn)
                            {
                                int Rand = Util.RandomMinMax(0, mob.Value.SpawnLoc_List.Count() - 1);
                                if (!mob.Value.RandomSpawn)
                                {
                                    try
                                    {
                                        mob.Value.m_Loc = new Point2D(mob.Value.SpawnLoc_List[Rand].X, mob.Value.SpawnLoc_List[Rand].Y);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(Rand);
                                    }
                                }
                                else
                                {
                                    Point2D temp = null;
                                    int count = 0;
                                    while ((temp = Map.SpawnPointFixed(i, mob.Value.m_Map, mob.Value.SpawnLoc_List[Rand].X, mob.Value.SpawnLoc_List[Rand].Y, 5)) == null)
                                    {
                                        if (++count > 50)
                                        {
                                            temp = new Point2D(mob.Value.SpawnLoc_List[Rand].X, mob.Value.SpawnLoc_List[Rand].Y);
                                            Console.WriteLine(mob.Value.m_Map + " " + temp.X + ":" + temp.Y);
                                            break;
                                        }
                                    };
                                    mob.Value.m_Loc = temp;
                                }

                                mob.Value.Target = 0;
                                mob.Value.TargetP = null;
                                mob.Value.m_Buffs.Clear();

                                mob.Value.Alive = true;
                            }
                            else
                                return;
                        }
                        else
                        {
                            if (mob.Value is script.monster.BaseNode) return;

                            var playerlist = PlayerHandler.getSingleton().add[mob.Value.m_Channel].Where(xe => xe.Value.loggedIn && xe.Value.Map == mob.Value.m_Map).ToList();
                            if (playerlist.Count() == 0)
                                return;

                            if (mob.Value.m_Buffs.Where(xe => xe.Value.GetType() == typeof(script.spells.Oblivion)).Any())
                                return;

                            if (mob.Value.Target == 0)
                            {
                                //var LocalPlayers = w_server.OnlineConnections.Where(xe => xe.Client != null && xe.Client.player != null &&
                                //xe.Client.player.Map != null && xe.Client.player.loggedIn && xe.Client.player.m_Map == mob.Value.m_Map).Select(xe => xe.Client.player);
                                var LocalPlayers = PlayerHandler.getSingleton().add[i].Where(xe => xe.Value.client != null && xe.Value.loggedIn && xe.Value.m_Channel == mob.Value.m_Channel && xe.Value.Map == mob.Value.m_Map && World.Dist2d(xe.Value.Loc, mob.Value.m_Loc) <= mob.Value.MinAggro).OrderBy(xe => xe.Value.Level).FirstOrDefault().Value;

                                if (LocalPlayers == null)
                                {
                                    mob.Value.Target = 0;
                                    mob.Value.TargetP = null;
                                    return;
                                }
                                else
                                {
                                    mob.Value.Target = (LocalPlayers as Player).m_Serial;
                                    mob.Value.TargetP = (LocalPlayers as Player);
                                }

                            }
                            else
                            {
                                if (!mob.Value.TargetP.loggedIn || mob.Value.TargetP.Map != mob.Value.m_Map)
                                {
                                    mob.Value.Target = 0;
                                    mob.Value.TargetP = null;
                                    return;
                                }
                            }

                            var mobdist = World.Dist2d(mob.Value.m_Loc.X, mob.Value.m_Loc.Y, mob.Value.TargetP.Loc.X, mob.Value.TargetP.Loc.Y);

                            if (mobdist >= 2 && mobdist < mob.Value.MaxAggro && Server.tickcount.ElapsedMilliseconds - mob.Value.WalkSpeed > mob.Value.LastAction)
                            {
                                //이동
                                mob.Value.LastAction = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                                var newp = mob.Value.AIStep(Map.loadedmaps[mob.Value.LoadMap], mob.Value.TargetP);

                                if (newp.X == mob.Value.m_Loc.X && newp.Y == mob.Value.m_Loc.Y)
                                    return;

                                mob.Value.Face = World.GetAngle((short)newp.X, (short)newp.Y, (short)mob.Value.m_Loc.X, (short)mob.Value.m_Loc.Y);

                                //    Point2D newStep = AddMove(mob.Value);

                                World.SendToAll(new QueDele(mob.Value.m_Channel, mob.Value.m_Map, new MoveSpriteWalk(mob.Value.m_Serial, (short)mob.Value.Face, (short)mob.Value.m_Loc.X, (short)mob.Value.m_Loc.Y).Compile()));

                                mob.Value.m_Loc.X = newp.X;
                                mob.Value.m_Loc.Y = newp.Y;


                                newp.Dispose();
                                newp = null;

                                if (World.Dist2d(mob.Value.m_Loc.X, mob.Value.m_Loc.Y, mob.Value.TargetP.Loc.X, mob.Value.TargetP.Loc.Y) <= 1)
                                {
                                    mob.Value.LastAction = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                                    mob.Value.Attack(mob.Value.TargetP);

                                    World.SendToAll(new QueDele(mob.Value.m_Channel, mob.Value.m_Map, new SwingAnimation(mob.Value.m_Serial, World.AttackRange((short)mob.Value.TargetP.Loc.X, (short)mob.Value.TargetP.Loc.Y, (short)mob.Value.m_Loc.X, (short)mob.Value.m_Loc.Y)).Compile()));
                                }
                            }
                            else if (mobdist <= 1 && Server.tickcount.ElapsedMilliseconds - mob.Value.AttackSpeed > mob.Value.LastAction)
                            {
                                mob.Value.LastAction = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                                mob.Value.Attack(mob.Value.TargetP);

                                World.SendToAll(new QueDele(mob.Value.m_Channel, mob.Value.m_Map, new SwingAnimation(mob.Value.m_Serial, World.AttackRange((short)mob.Value.TargetP.Loc.X, (short)mob.Value.TargetP.Loc.Y, (short)mob.Value.m_Loc.X, (short)mob.Value.m_Loc.Y)).Compile()));
                            }
                            else if (mobdist > mob.Value.MaxAggro)
                            {
                                mob.Value.Target = 0;
                                mob.Value.TargetP = null;
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(string.Format("spawner fail :{0} {1}/{2}:{3}\n{4}", mob.Value.Name, mob.Value.m_Map, mob.Value.m_Loc.X, mob.Value.m_Loc.Y, e.ToString()));
                        mob.Value.Target = 0;
                        mob.Value.TargetP = null;
                        mob.Value.Alive = false;
                    }
                });
                Thread.Sleep(100);
            }
        }
        public static Point2D AddMove(model.BaseObject mob, int Range = 1)
        {
            int face = mob.Face;
            int x = mob.m_Loc.X;
            int y = mob.m_Loc.Y;
            if (face == 0)
                y -= Range;
            if (face == 1)
            {
                x += Range;
                y -= Range;
            }
            if (face == 2)
                x += Range;
            if (face == 3)
            {
                x += Range;
                y += Range;
            }
            if (face == 4)
                y += Range;
            if (face == 5)
            {
                x -= Range;
                y += Range;
            }
            if (face == 6)
                x -= Range;
            if (face == 7)
            {
                y -= Range;
                x -= Range;
            }
            var p = new Point2D(x, y);

            return p;
        }

        public static short AttackRange(short x, short y, short x2, short y2)
        {
            if (x2 - x == 0 && y2 - y == 0)
                return 0;
            if (x2 - x == -1 && y2 - y == 1)
                return 1;
            if (x2 - x == -1 && y2 - y == 0)
                return 2;
            if (x2 - x == -1 && y2 - y == -1)
                return 3;
            if (x2 - x == 0 && y2 - y == -1)
                return 4;
            if (x2 - x == 1 && y2 - y == -1)
                return 5;
            if (x2 - x == 1 && y2 - y == 0)
                return 6;
            if (x2 - x == 1 && y2 - y == 1)
                return 7;
            if (x2 - x == 0 && y2 - y == 1)
                return 0;

            return 0;
        }
        public static bool ArrayEqual(List<string> list1, List<string> list2)
        {
            if (list1.Count() == list2.Count())
            {
                list1.Sort();
                list2.Sort();
                for (int i = 0; i < list1.Count(); i++)
                {
                    if (!list1[i].Equals(list2[i]))
                        return false;
                }
                return true;
            }
            return false;
        }
        public static short GetAngle(short myx, short myy, short x2, short y2)
        {
            var angle = Math.Atan2(y2 - myy, x2 - myx);
            var test = (angle > 0 ? angle : (2 * Math.PI + angle)) * 360 / (2 * Math.PI);

            if (test == 90)
                return 0;
            if (test > 90 && test < 180)
                return 1;
            if (test == 180)
                return 2;
            if (test > 180 && test < 270)
                return 3;
            if (test == 270)
                return 4;
            if (test > 270 && test < 360)
                return 5;
            if (test == 360)
                return 6;
            if (test > 0 && test < 90)
                return 7;

            return 0;
        }

        public static List<Point2D> FireWalAttackRage(short dir, Point2D Center, int w = 3)
        {
            List<Point2D> temp = new List<Point2D>();

            if (dir == 0 || dir == 4)           // 가로방향 (-)
            {
                for (int i = -w; i <= w; i++)
                    temp.Add(new Point2D(Center.X + i, Center.Y));
            }
            else if (dir == 1 || dir == 5)
            {
                for (int i = -w; i <= w; i++)
                    temp.Add(new Point2D(Center.X + i, Center.Y + i));
            }
            else if (dir == 2 || dir == 6)
            {
                for (int j = -w; j <= w; j++)
                    temp.Add(new Point2D(Center.X, Center.Y + j));
            }
            else if (dir == 3 || dir == 7)
            {
                for (int i = -w; i <= w; i++)
                    temp.Add(new Point2D(Center.X + i, Center.Y - i));
            }

            return temp;
        }

        public static int Dist2d(int x, int y, int x2, int y2)
        {
            int ret = (int)Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
            return ret;
        }
        public static int Dist2d(Point2D one, Point2D two)
        {
            if (one == null || two == null)
                return Int32.MaxValue;

            int ret = (int)Math.Sqrt((one.X - two.X) * (one.X - two.X) + (one.Y - two.Y) * (one.Y - two.Y));
            return ret;
        }


        private interface IEntityEntry
        {
            Serial Serial { get; }
            int TypeID { get; }
            long Position { get; }
            int Length { get; }
        }

        public static Serial FindMobile(Serial serial)
        {
            Serial id;

            if (m_MObjects == null)
                m_MObjects = new ConcurrentDictionary<Serial, Serial>();


            m_MObjects.TryGetValue(serial, out id);

            if (id != 0)
                return Serial.MinusOne;

            return serial;
        }

        public static void RemoveDynamicObj(string map, int m_Serial)
        {
            for (int i = 0; i < Settings.VillageName.Count; i++)
            {
                foreach (var ple in PlayerHandler.getSingleton().add[i])
                {
                    if (ple.Key == null || ple.Value == null || ple.Value.loggedIn == false)
                        continue;
                    if (ple.Value.Map == map)
                    {
                        BaseObject value;
                        ple.Value.InstancedObjects.TryRemove(m_Serial, out value);
                    }
                }
            }
        }

        public static void SendToAll(QueDele p)
        {
            var cons = w_server.OnlineConnections;
            foreach (var conn in cons)
            {
                // 접속 직후에는 Connections에 먼저 들어가고 Client는 나중에 대입되므로 null일 수 있다.
                var client = conn.Client;
                if (client == null)
                    continue;
                var player = client.player;

                if (player == null || !player.loggedIn)
                    continue;

                if (!client.connection.IsConnected || client.connection._server == null)
                    w_server.Disconnect(client.connection);

                if (p.Map != null && p.Map == "all")
                {
                    client.SendPacket(p.Packet);
                }
                else if (p.Map != null && p.Map == "ch" && p.Channel == player.m_Channel)
                {
                    if (p.Serial == player.m_Serial) continue;
                    client.SendPacket(p.Packet);
                }
                else if (p.Map != null && p.Channel == player.m_Channel && p.Map == player.Map)
                {
                    if (p.Serial == player.m_Serial) continue;
                    client.SendPacket(p.Packet);
                }
            }
        }

        #region 외치기
        public static void SendToChatAll(QueDele p)
        {
            var cons = w_server.OnlineConnections;
            foreach (var conn in cons)
            {
                var client = conn.Client;
                if (client == null)
                    continue;
                var player = client.player;

                if (player == null || !player.loggedIn || player.ChatBlack)
                    continue;

                if (!client.connection.IsConnected || client.connection._server == null)
                    w_server.Disconnect(client.connection);

                if (p.Map != null && p.Map == "all")
                {
                    if (p.Serial == player.m_Serial) continue;
                    client.SendPacket(p.Packet);
                }
                else if (p.Map != null && p.Map == "ch" && p.Channel == player.m_Channel)
                {
                    if (p.Serial == player.m_Serial) continue;
                    client.SendPacket(p.Packet);
                }
                else if (p.Map != null && p.Channel == player.m_Channel && p.Map == player.Map)
                {
                    if (p.Serial == player.m_Serial) continue;
                    client.SendPacket(p.Packet);
                }
            }
        }
        #endregion

        #region 사운드
        public static void SendToSoundRange(QueDele p, Player playee, int range = 10)
        {
            var cons = w_server.OnlineConnections.Where(xe => xe.Client != null && xe.Client.player != null
                && xe.Client.player.m_Channel == playee.m_Channel
                && Dist2d(xe.Client.player.Loc, playee.Loc) < range).Select(xe => xe).ToArray();
            foreach (var conn in cons)
            {
                var player = conn.Client.player;
                var client = conn.Client;

                if (player == null || !player.loggedIn)
                    continue;

                if (!client.connection.IsConnected || client.connection._server == null)
                    w_server.Disconnect(client.connection);

                if (p.Map != null && p.Map == player.Map && player.SoundFlag)
                {
                    if (p.Serial == player.m_Serial) continue;
                    client.SendPacket(p.Packet);
                }
            }
        }
        #endregion


        public static void SendToRange(QueDele p, Player playee, int range = 15)
        {
            var cons = w_server.OnlineConnections.Where(xe => xe.Client != null && xe.Client.player != null
                && xe.Client.player.m_Channel == playee.m_Channel
                && Dist2d(xe.Client.player.Loc, playee.Loc) < range).Select(xe => xe).ToArray();
            foreach (var conn in cons)
            {
                var player = conn.Client.player;
                var client = conn.Client;

                if (player == null || !player.loggedIn)
                    continue;

                if (!client.connection.IsConnected || client.connection._server == null)
                    w_server.Disconnect(client.connection);

                if (p.Map != null && p.Map == player.Map && p.Channel == player.m_Channel)
                {
                    if (p.Serial == player.m_Serial) continue;
                    client.SendPacket(p.Packet);
                }
            }
        }
        public static void SendToRange(QueDele p, script.npc.BaseNPC playee, int range = 10)
        {
            var cons = w_server.OnlineConnections.Where(xe => xe.Client != null && xe.Client.player != null
                && xe.Client.player.m_Channel == playee.m_Channel
                && Dist2d(xe.Client.player.Loc, playee.m_Loc) < range).Select(xe => xe).ToArray();
            foreach (var conn in cons)
            {
                var player = conn.Client.player;
                var client = conn.Client;

                if (player == null || !player.loggedIn)
                    continue;

                if (!client.connection.IsConnected || client.connection._server == null)
                    w_server.Disconnect(client.connection);

                if (p.Map != null && p.Map == player.Map && p.Channel == playee.m_Channel)
                {
                    if (p.Serial == player.m_Serial) continue;
                    client.SendPacket(p.Packet);
                }
            }
        }

        public static List<BaseObject> GetTileTarget(Player play, Point2D adjecentTile, int swingdir = 0)
        {
            List<BaseObject> ret = new List<BaseObject>();
            var Targets = World.NewMonsters[play.m_Channel].Where(xe => xe.Value.m_Map != null && xe.Value.m_Loc != null
                && xe.Value.m_Map == play.Map
                && xe.Value.m_Loc.X == adjecentTile.X
                && xe.Value.m_Loc.Y == adjecentTile.Y
                && xe.Value.Alive).Select(xe => xe);
            // .Select(xe => xe);
            foreach (var tttt in Targets)
                ret.Add(tttt.Value);


            var Targets2 = PlayerHandler.getSingleton().add[play.m_Channel].Where(xe => xe.Value != null && xe.Value.Map != null
                && xe.Value != play && xe.Value.Map == play.Map
                && xe.Value.loggedIn
                && xe.Value.Loc.X == adjecentTile.X
                && xe.Value.Loc.Y == adjecentTile.Y)
                .Select(xe => xe);
            foreach (var tttt in Targets2)
                ret.Add(tttt.Value);



            var Targets3 = World.NewNpcs[play.m_Channel].Where(xe => xe.Value != null && xe.Value.m_Map != null
                && xe.Value.m_Map == play.Map
                && xe.Value.m_Loc.X == adjecentTile.X
                && xe.Value.m_Loc.Y == adjecentTile.Y)
                .Select(xe => xe).FirstOrDefault();
            if (Targets3.Value != null)
                ret.Add(Targets3.Value);




            if (play.m_Buffs.Where(xe => xe.Value.BuffEffect.BuffType == script.spells.BuffCase.Triple).FirstOrDefault().Value != null)
            {
                ret.Clear();

                Targets = World.NewMonsters[play.m_Channel].Where(xe => xe.Value.m_Map != null && xe.Value.m_Map == play.Map && xe.Value.Alive &&
                      (
                        (xe.Value.m_Loc.X == adjecentTile.X && xe.Value.m_Loc.Y == adjecentTile.Y) ||
                        (!(xe.Value is script.monster.BaseNode) &&
                            (
                                (xe.Value.m_Loc.X == CombatHandler2.AdjecentTile(play, swingdir - 1).X && xe.Value.m_Loc.Y == CombatHandler2.AdjecentTile(play, swingdir - 1).Y) ||
                                (xe.Value.m_Loc.X == CombatHandler2.AdjecentTile(play, swingdir + 1).X && xe.Value.m_Loc.Y == CombatHandler2.AdjecentTile(play, swingdir + 1).Y)
                            )
                        )
                      )
                      ).Select(xe => xe);


                Targets2 = PlayerHandler.getSingleton().add[play.m_Channel].Where(xe => xe.Key != null && xe.Value != null && xe.Value.loggedIn && xe.Value.Map == play.Map
                    &&
                    (
                   (xe.Value.m_Loc.X == adjecentTile.X && xe.Value.m_Loc.Y == adjecentTile.Y)
                    || (xe.Value.m_Loc.X == CombatHandler2.AdjecentTile(play, swingdir - 1).X && xe.Value.m_Loc.Y == CombatHandler2.AdjecentTile(play, swingdir - 1).Y)
                    || (xe.Value.m_Loc.X == CombatHandler2.AdjecentTile(play, swingdir + 1).X && xe.Value.m_Loc.Y == CombatHandler2.AdjecentTile(play, swingdir + 1).Y)
                    )
                    ).Select(xe => xe);
                foreach (var tttt in Targets)
                    ret.Add(tttt.Value);

                foreach (var tttt in Targets2)
                    ret.Add(tttt.Value);
            }



            if (play.m_Buffs.Where(xe => xe.Value.BuffEffect.BuffType == script.spells.BuffCase.Twister).FirstOrDefault().Value != null)
            {
                ret.Clear();

                #region Monster Target
                Targets = World.NewMonsters[play.m_Channel].Where(xe => xe.Value.m_Map != null && xe.Value.m_Map == play.Map && xe.Value.Alive &&
                        (
                            (xe.Value.m_Loc.X == adjecentTile.X && xe.Value.m_Loc.Y == adjecentTile.Y) ||
                            (
                                !(xe.Value is script.monster.BaseNode) &&
                                (
                                    (xe.Value.m_Loc.X == CombatHandler2.AdjecentTile(play, swingdir - 2).X && xe.Value.m_Loc.Y == CombatHandler2.AdjecentTile(play, swingdir - 2).Y) ||
                                    (xe.Value.m_Loc.X == CombatHandler2.AdjecentTile(play, swingdir + 2).X && xe.Value.m_Loc.Y == CombatHandler2.AdjecentTile(play, swingdir + 2).Y) ||
                                    (xe.Value.m_Loc.X == CombatHandler2.AdjecentTile(play, swingdir + 4).X && xe.Value.m_Loc.Y == CombatHandler2.AdjecentTile(play, swingdir + 4).Y)
                                )
                            )
                        )
                      )
                     .Select(xe => xe);
                #endregion

                #region Player Target
                Targets2 = PlayerHandler.getSingleton().add[play.m_Channel].Where(xe => xe.Key != null && xe.Value != null && xe.Value.loggedIn && xe.Value.Map == play.Map
                    &&
                    (
                   (xe.Value.m_Loc.X == adjecentTile.X && xe.Value.m_Loc.Y == adjecentTile.Y)
                    || (xe.Value.m_Loc.X == CombatHandler2.AdjecentTile(play, swingdir - 2).X && xe.Value.m_Loc.Y == CombatHandler2.AdjecentTile(play, swingdir - 2).Y)
                    || (xe.Value.m_Loc.X == CombatHandler2.AdjecentTile(play, swingdir + 2).X && xe.Value.m_Loc.Y == CombatHandler2.AdjecentTile(play, swingdir + 2).Y)
                    || (xe.Value.m_Loc.X == CombatHandler2.AdjecentTile(play, swingdir + 4).X && xe.Value.m_Loc.Y == CombatHandler2.AdjecentTile(play, swingdir + 4).Y)
                    )
                    ).Select(xe => xe);
                #endregion

                foreach (var tttt in Targets)
                    ret.Add(tttt.Value);

                foreach (var tttt in Targets2)
                    ret.Add(tttt.Value);
            }

            return ret.Count > 0 ? ret : null;
        }

        public static void SaveAll()
        {
                // 저장 파일 로드가 끝나기 전에는 저장하지 않는다. 로드 중 예외로 종료될 때
                // 플레이어 0명인 상태로 저장 파일을 덮어쓰는 것을 막는다.
                if (!Server.WorldLoaded)
                {
                    Console.WriteLine("World Save skipped: world data is not loaded yet.");
                    return;
                }

                // 덮어쓰기 전의 파일을 먼저 백업한다. (기존에는 저장 후에 복사해 직전 상태가 남지 않았다)
                try { BinaryIO.BackUpData(); }
                catch (Exception e) { Console.WriteLine("BackUp Err : " + e.Message); }

                Console.WriteLine("Guild Size: ", NewGuilds.Count());

                Console.WriteLine("Player Size: " + Server.playerHandler.Count);
                var Playss = Server.playerHandler.Total.Where(xe => xe.Key != null && xe.Value != null).Select(xe => xe.Value).ToList();


                var Itemss = NewItems.Values.Where(xe => xe.InvSlot >= 0).Select(xe => xe).ToList();
                Console.WriteLine("Item Size: " + Itemss.Count());
                
                var AuctionItemss = NewAuctions.Values.Where(xe => xe != null).Select(xe => xe).ToList();
                Console.WriteLine("Auction Size: " + AuctionItemss.Count());

                Console.WriteLine("World Save Complete");



                try { BinaryIO.SavePlayers(Playss); }
                catch
                { Console.WriteLine("Player Save Err"); }
            

                try { BinaryIO.SaveGuild(NewGuilds.Values.ToList()); }
                catch
                { Console.WriteLine("Guild Save Err"); }


                try { BinaryIO.SaveItems(Itemss); }
                catch
                { Console.WriteLine("Items Save Err"); }


                try { BinaryIO.SaveAuctionItems(AuctionItemss); }
                catch
                { Console.WriteLine("AuctionItems Save Err"); }



                Console.WriteLine("World Save IO Complete");

                World.DeletePlayer.Clear();
                /*foreach (var item in Itemss)
                {
                    World.DBConnect.WriteQue.Enqueue(() => World.DBConnect.InsertItem(item));
                }*/
        }
    }
}
