
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Data;
namespace LKCamelot.script.monster
{
    public enum Race
    {
        Magical = 1,
        Animal = 2,
        Undead = 4,
        Demon = 8,
        Human = 16,

        Gem = 128,
    }
    public enum MobType
    {
        ShortDistMob = 1,
        LongDistMob = 2,
        ShortDistSummonmob = 4,
        LongDistSummonmob = 8,
        Event = 16,
    }

    public class Monster : BaseObject
    {
        public DataRow MonsterData
        {
            get
            { 
                return LKCamelot.GameData.getInstance().GetMob(this.INDEX);
            }
        }
        public MobType MobType;
        public bool RandomSpawn = true;

        public int m_MonsterID { get { return Convert.ToInt32(MonsterData["SPRITE_ID"]); } }
        public string LoadMap { get { return LKCamelot.model.Map.FullMaps[this.m_Channel].Where(xe => xe.Key == m_Map).FirstOrDefault().Value.Filename; } }


        public Point2D m_SpawnLoc;
        public List<Point2D> SpawnLoc_List = new List<Point2D>();


        private int m_HPCur;
        public bool SpawnReady, m_Alive = false;
        public Serial Target;
        public Player TargetP;
        public long LastAction = 0;
        public long LastSpawn = 0;

        //public virtual string Name { get { return ""; } }
        public virtual int INDEX { get { return -1; } }
        public virtual byte FrameType { get { return 1; } }
        //public virtual int MinAggro { get { return 7; } }
        //public virtual int MaxAggro { get { return 12; } }

        //public virtual int Level { get { return 0; } }
        //public virtual int HP { get { return 0; } }
        //public virtual int Dam { get { return 0; } }
        //public virtual int AC { get { return 0; } }
        //public virtual int Hit { get { return 0; } }
        //public virtual int XP { get { return 0; } }
        //public virtual int Color { get { return 0; } } 

        public string Name { get { return MonsterData["ENG_NAME"].ToString(); } }
        public int Level { get { return Convert.ToInt32(MonsterData["LEVEL"]); }} 
        public int HP { get { return Convert.ToInt32(MonsterData["HP"]); } }
        public int Dam { get { return Convert.ToInt32(MonsterData["DAM"]); } }
        public int AC { get { return Convert.ToInt32(MonsterData["AC"]); } }
        public int Hit { get { return Convert.ToInt32(MonsterData["HIT"]); } }
        public int XP { get { return Convert.ToInt32(MonsterData["XP"]); } }
        public library.Color Color { get { return (library.Color)Convert.ToInt32(MonsterData["COLOR"]); } }
        public int MinAggro { get { return Convert.ToInt32(MonsterData["MIN_AGGRO"]); } }
        public int MaxAggro { get { return Convert.ToInt32(MonsterData["MAX_AGGRO"]); } }
        public int WalkSpeed { get { return Convert.ToInt32(MonsterData["WALKSPEED"]); } }
        public int AttackSpeed { get { return Convert.ToInt32(MonsterData["ATTACKSPEED"]); } }

        public virtual int SpawnTime { get { return 30000; } }
        public virtual int AliveTime { get { return 0; } }
        public virtual Race Race { get { return 0; } }

        //public virtual int WalkSpeed { get { return 500; } }
        //public virtual int AttackSpeed { get { return 1000; } }
        public virtual LootPack Loot { get { return null; } }

        MySolver<MyPathNode, Object> aStar;
        LKCamelot.model.SpatialAStar<MyPathNode, object>.PathNode path;


        public ConcurrentDictionary<Player, int> Exp = new ConcurrentDictionary<Player, int>();


        public Monster(Serial serial)
            : base(serial)
        {
            HPCur = HP;

        }
        public Monster(int MonsterID)
            : this()
        {
            m_HPCur = HP;
        }
        public Monster()
            : base(Serial.NewMobile)
        {
            HPCur = HP;
        }





















        public virtual void GenerateLoot()
        {
        }

        public MyPathNode AIStep(int Channel, Map.TiledMap map, Point2D Loc, string Map)
        {
            if (map.sizeX <= Loc.X)
                Loc.X = map.sizeX-1;
            else if (map.sizeY <= Loc.Y)
                Loc.Y = map.sizeX-1;
            else if (0 >= Loc.X)
                Loc.X = 0;
            else if (0 >= Loc.X)
                Loc.X = 0;


            var mobs = World.NewMonsters[Channel].Where(xe => xe.Value != this && xe.Value.Alive && xe.Value.m_Map == Map);
            var play = PlayerHandler.getSingleton().add[Channel].Where(xe => xe.Value.loggedIn && xe.Value.m_Map == Map);

            List<MyPathNode> tempsquares = new List<MyPathNode>();

            foreach (var pl in play)
            {
                var tile = map.tiles[pl.Value.m_Loc.X, pl.Value.m_Loc.Y];
                tile.IsWall = true;
                tempsquares.Add(tile);
            }
            foreach (var mob in mobs)
            {
                var tile = map.tiles[mob.Value.m_Loc.X, mob.Value.m_Loc.Y];
                tile.IsWall = true;
                tempsquares.Add(tile);
            }

            aStar = new MySolver<MyPathNode, Object>(map.tiles);
            path = aStar.SearchOnce(new Point(m_Loc.X, m_Loc.Y), new Point(Loc.X, Loc.Y), null);

            foreach (var tile in tempsquares)
                map.tiles[tile.X, tile.Y].IsWall = false;
            //   foreach (var mob in mobs)
            //      map.tiles[mob.Value.m_Loc.X, mob.Value.m_Loc.Y].IsWall = false;

            MyPathNode p = new MyPathNode(m_Loc.X, m_Loc.Y);
            if (path != null)
            {
                p.X = path.X;
                p.Y = path.Y;
            }
            path = null;
            aStar.Dispose();
            aStar = null;
            return p;
        }
        public MyPathNode AIStep(Map.TiledMap map, Player target)
        {
            List<MyPathNode> tempsquares = new List<MyPathNode>();

            var play = PlayerHandler.getSingleton().add[this.m_Channel].Where(xe => xe.Value.loggedIn && xe.Value.m_Map == target.Map);
            foreach (var pl in play)
            {
                var tile = map.tiles[pl.Value.m_Loc.X, pl.Value.m_Loc.Y];
                tile.IsWall = true;
                tempsquares.Add(tile);
            }

            var mobs = World.NewMonsters[this.m_Channel].Where(xe => xe.Value != this && xe.Value.Alive && xe.Value.m_Map == target.Map);
            foreach (var mob in mobs)
            {
                var tile = map.tiles[mob.Value.m_Loc.X, mob.Value.m_Loc.Y];
                tile.IsWall = true;
                tempsquares.Add(tile);
            }

            aStar = new MySolver<MyPathNode, Object>(map.tiles);
            try
            {
                path = aStar.SearchOnce(new Point(m_Loc.X, m_Loc.Y), new Point(target.m_Loc.X, target.m_Loc.Y), null);
            }
            catch {
                Console.WriteLine("SearchOnec " + map + " " + m_Loc.X + " " + m_Loc.Y + " " + target.m_Loc.X + " " + target.m_Loc.Y);
            }
            foreach (var tile in tempsquares)
                map.tiles[tile.X, tile.Y].IsWall = false;
         //   foreach (var mob in mobs)
          //      map.tiles[mob.Value.m_Loc.X, mob.Value.m_Loc.Y].IsWall = false;

            MyPathNode p = new MyPathNode(m_Loc.X, m_Loc.Y);
            if (path != null)
            {
                p.X = path.X;
                p.Y = path.Y;
            }
            path = null;
            aStar.Dispose();
            aStar = null;
            return p;
        }

        public virtual void DropLoot(Player play)
        {
            if (Loot != null)
            {
                foreach (var loot in Loot.m_Entries)
                {
                    if (loot.TryDrop())
                    {
                        var inst = Activator.CreateInstance(Type.GetType(loot.t_item.ToString()));
                        if (inst is script.item.Gold)
                        {
                            var amount = ScaleGoldQuantity(loot.m_Quantity.Roll(), Settings.GOLDRate[0]);
                            (inst as script.item.Gold).Quantity = amount;
                        }
                        if (play.AutoLoot)
                        {
                            if (inst is script.item.Gold)
                                AddGoldSafely(play, (inst as script.item.Item).Quantity);
                            else
                            {
                                if (play.GetFreeSlots() > 0)
                                {
                                    inst = (inst as script.item.Item).Inventory(play);
                                    string itdrop = this.Name +" gave you " + (inst as script.item.Item).Name+".";
                                    play.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, itdrop).Compile());
                                    play.client.SendPacket(new LKCamelot.model.AddItemToInventory2((inst as script.item.Item)).Compile());
                                }
                                else
                                {
                                    (inst as script.item.Item).Drop(play, 0);
                                }
                                World.NewItems.TryAdd((inst as script.item.Item).m_Serial, (inst as script.item.Item));
                            }  
                        }
                        else
                        {
                            (inst as script.item.Item).Drop(this, play);
                            (inst as script.item.Item).DropTime = Server.tickcount.ElapsedMilliseconds;
                            World.NewItems.TryAdd((inst as script.item.Item).m_Serial, (inst as script.item.Item));
                            World.SendToAll(new QueDele(play.m_Channel, play.Map, new CreateItemGround2((inst as script.item.Item), (inst as script.item.Item).m_Serial).Compile()));
                        }
                        return;
                    }
                }
            }
        }

        private static int ScaleGoldQuantity(int amount, float rate)
        {
            double scaled = (double)amount * rate;
            if (double.IsNaN(scaled) || scaled <= 0)
                return 0;
            if (double.IsInfinity(scaled) || scaled >= int.MaxValue)
                return int.MaxValue;
            return (int)scaled;
        }

        private static void AddGoldSafely(Player player, int amount)
        {
            if (amount <= 0)
                return;

            ulong current = player.Gold;
            ulong addition = (uint)amount;
            player.Gold = ulong.MaxValue - current < addition ? ulong.MaxValue : current + addition;
        }

        private static void AwardExperience(Player recipient, float amount)
        {
            if (recipient == null || amount <= 0 || float.IsNaN(amount) || float.IsInfinity(amount))
                return;

            try
            {
                // Preserve the existing promotion reduction before applying the event multiplier.
                if (recipient.Promo > 0)
                    amount = (int)(amount * 0.01f);

                double scaled = (double)amount * Settings.XPrate[0];
                if (double.IsNaN(scaled) || scaled <= 0)
                    return;

                long current = recipient.XP;
                long room = int.MaxValue - current;
                if (room <= 0)
                    return;

                long gain = scaled >= room ? room : (long)scaled;
                if (gain > 0)
                    recipient.XP = (int)(current + gain);
            }
            catch (Exception e)
            {
                Console.WriteLine("Experience award failed for {0}: {1}", recipient.Name, e);
            }
        }

        public bool Alive
        {
            get { return m_Alive; }
            set
            {
                if (value == false)
                {
                    LastSpawn = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                    World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new DeleteObject(m_Serial).Compile()));
                    if ( (this.MobType & script.monster.MobType.Event) != 0 )
                    {
                        script.monster.Monster mob;
                        World.NewMonsters[this.m_Channel].TryRemove(this.m_Serial, out mob);
                    }
                }

                if (m_Alive == false && value == true)
                {
                    HPCur = HP;
                    Target = 0;
                    
                    #region Random
                    int Rand = Util.RandomMinMax(0, this.SpawnLoc_List.Count() - 1);
                    if (!this.RandomSpawn)
                    {
                        try
                        {
                            this.m_Loc = new Point2D(this.SpawnLoc_List[Rand].X, this.SpawnLoc_List[Rand].Y);
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
                        while ((temp = Map.SpawnPointFixed(this.m_Channel, this.m_Map, this.SpawnLoc_List[Rand].X, this.SpawnLoc_List[Rand].Y, 5)) == null)
                        {
                            if (++count > 50)
                            {
                                temp = new Point2D(this.SpawnLoc_List[Rand].X, this.SpawnLoc_List[Rand].Y);
                                Console.WriteLine(this.m_Map + " " + temp.X + ":" + temp.Y);
                                break;
                            }
                        };
                        this.m_Loc = temp;
                    }
                    #endregion

                    //        play.Value.InstancedObjects.Add(m_Serial);
                    if (this is script.monster.BaseNode)
                        World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new CreateGem((this as script.monster.BaseNode)).Compile()));
                    else
                    {
                        World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new CreateMonster(this, m_Serial).Compile()));
                        //World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new SetObjectEffects(this).Compile()));
                    }
                }
                m_Alive = value;
            }
        }

        public int HPCur
        {
            get { return m_HPCur; }
            set
            {
                if (value > HP)
                    value = HP;
                if (value <= 0)
                {
                    value = 0;
                    Alive = false;
                    World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new DeleteObject(m_Serial).Compile()));
                }

                if (value < m_HPCur)
                {

                }

                m_HPCur = value;
            }
        }




        public virtual void Attack(Player player)
        {
            float h = ((float)Hit / ((float)player.Hit + (float)Hit)) * 200;

            if (h >= 100 || new Random().Next(0, 100) < (int)h)
            {
                var take = Dam - player.AC;
                if (take <= 0)
                    take = 1;
                player.HPCur -= take;
            }
        }

        public virtual void TakeDamage(Player player)
        {
            if (Target == 0)
            {
                Target = player.m_Serial;
                TargetP = player;
            }


            float h = ((float)player.Hit / ((float)player.Hit + (float)Hit)) * 200;

            if (h >= 100 || new Random().Next(0, 100) < (int)h)
            {
                var take = (player.Dam - AC);
                if (take <= 0)
                    take = 1;

                var swingbuff = player.m_Buffs.Where(xe => xe.Value.BuffEffect.BuffType == script.spells.BuffCase.Triple || xe.Value.BuffEffect.BuffType == script.spells.BuffCase.Twister).FirstOrDefault().Value;
                if (swingbuff != null)
                {
                    var ttt = swingbuff.Level * 0.05;
                    take = (int)(take * (ttt + 0.40d));
                }

                if (HPCur - take <= 0)
                    take = HPCur;
                
                Exp.AddOrUpdate(player, take, (key, oldValue) => oldValue + take);


                int total = 0;
                Exp.TryGetValue(player, out total);
                if (total > (HP / 5) && Target != player.m_Serial)
                {
                    Target = player.m_Serial;
                    TargetP = player;
                }


                HPCur -= take;
                World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new HitAnimation(m_Serial,
                    Convert.ToByte(((((float)m_HPCur / (float)HP) * 100) * 1))).Compile()));

                if (HPCur <= 0)
                {
                    foreach (var p in Exp)
                    {
                        if (this.m_Channel != p.Key.m_Channel)
                            continue;
                        if (this.m_Map != p.Key.Map)
                            continue;
                        if (World.Dist2d(this.m_Loc.X, this.m_Loc.Y, p.Key.Loc.X, p.Key.Loc.Y) > 12)
                            continue;
                        
                        //if (p.Key.Level == Settings.MaxLevel && )


                        float temp = (float)p.Value / this.HP;      // 피해입힌 체력 / 총 체력
                        temp = XP * temp;                           // 총경험치를 나눔


                        var currMap = model.Map.FullMaps[this.m_Channel].Where(Xe => Xe.Key == this.m_Map).FirstOrDefault().Value;
                        if (p.Key.Level == Settings.MaxLevel && !currMap.isLegend)
                            temp = 0;

                        AwardExperience(p.Key, temp);
                    }
                    Exp.Clear();
                    DropLoot(player);
                    //m_Loc.X = m_SpawnLoc.X;
                    //m_Loc.Y = m_SpawnLoc.Y;
                }
            }
        }
        public virtual void TakeDamage(Player player, script.spells.Spell spell)
        {
            if (Target == 0)
            {
                Target = player.m_Serial;
                TargetP = player;
            }

            float h = ((float)player.Hit / ((float)player.Hit + (float)Hit)) * 200;

            if (h >= 100 || new Random().Next(0, 100) < (int)h)
            {
                int take = spell.DamBase + (spell.DamPl * spell.Level-1);// + (spell.DamPl * spell.SLevel2);
                if (spell.ManaCostPl != 0)
                {
                    take += (player.GetStat("men") / spell.menCoff);
                    take += (player.GetStat("str") / spell.strCoff);
                    take += (player.GetStat("dex") / spell.dexCoff);
                }
                if (spell is script.spells.DemonDeath)
                    take = Convert.ToInt32( (player.m_HPCur + (player.HP * 0.30)) * 0.5) + player.GetStat("dex");

                take = (take - AC);
                if (take < 0)
                    take = 0;
                if (HPCur - take <= 0)
                    take = HPCur;
                Exp.AddOrUpdate(player, take, (key, oldValue) => oldValue + take);


                int total = 0;
                Exp.TryGetValue(player, out total);
                if (total > (HP / 5) && Target != player.m_Serial)
                {
                    Target = player.m_Serial;
                    TargetP = player;
                }

                HPCur -= take;
                World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new HitAnimation(m_Serial, Convert.ToByte(((((float)m_HPCur / (float)HP) * 100) * 1))).Compile()));
                if (HPCur <= 0)
                {
                    foreach (var p in Exp)
                    {
                        if (this.m_Map != p.Key.Map)
                            continue;
                        if (World.Dist2d(this.m_Loc.X, this.m_Loc.Y, p.Key.Loc.X, p.Key.Loc.Y) > 12)
                            continue;

                        float temp = (float)p.Value / this.HP;      // 피해입힌 체력 / 총 체력
                        temp = XP * temp;                           // 총경험치를 나눔

                        var currMap = model.Map.FullMaps[this.m_Channel].Where(Xe => Xe.Key == this.m_Map).FirstOrDefault().Value;
                        if (p.Key.Level == Settings.MaxLevel && !currMap.isLegend)
                            temp = 0;

                        AwardExperience(p.Key, temp);
                    }
                    Exp.Clear();
                    DropLoot(player);

                }
            }
        }
        public virtual void TakeDamage(Player player, script.spells.Spell spell, short castx, short casty)
        {
            if (Target == 0)
            {
                Target = player.m_Serial;
                TargetP = player;
            }

            float h = ((float)player.Hit / ((float)player.Hit + (float)Hit)) * 200;

            if (h >= 100 || new Random().Next(0, 100) < (int)h)
            {
                int take = spell.DamBase + (spell.DamPl * spell.Level - 1);// + (spell.DamPl * spell.SLevel2);
                if (spell.ManaCostPl != 0)
                {
                    take += (player.GetStat("men") / spell.menCoff);
                    take += (player.GetStat("str") / spell.strCoff);
                    take += (player.GetStat("dex") / spell.dexCoff);
                }
                if (spell is script.spells.DemonDeath)
                    take = Convert.ToInt32((player.m_HPCur + (player.HP * 0.30)) * 0.5) + player.GetStat("dex");

                take = (take - AC);
                if (take < 0)
                    take = 0;
                if (HPCur - take <= 0)
                    take = HPCur;
                Exp.AddOrUpdate(player, take, (key, oldValue) => oldValue + take);

                int total = 0;
                Exp.TryGetValue(player, out total);
                if (total > (HP / 5) && Target != player.m_Serial)
                {
                    Target = player.m_Serial;
                    TargetP = player;
                }

                HPCur -= take;
                World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new HitAnimation(m_Serial, Convert.ToByte(((((float)m_HPCur / (float)HP) * 100) * 1))).Compile()));
                if (HPCur <= 0)
                {
                    foreach (var p in Exp)
                    {
                        if (this.m_Map != p.Key.Map)
                            continue;
                        if (World.Dist2d(this.m_Loc.X, this.m_Loc.Y, p.Key.Loc.X, p.Key.Loc.Y) > 12)
                            continue;

                        float temp = (float)p.Value / this.HP;      // 피해입힌 체력 / 총 체력
                        temp = XP * temp;                           // 총경험치를 나눔

                        var currMap = model.Map.FullMaps[this.m_Channel].Where(Xe => Xe.Key == this.m_Map).FirstOrDefault().Value;
                        if (p.Key.Level == Settings.MaxLevel && !currMap.isLegend)
                            temp = 0;

                        AwardExperience(p.Key, temp);
                    }
                    Exp.Clear();
                    DropLoot(player);

                }
            }
        }
    }
}
