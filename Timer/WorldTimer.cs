using LKCamelot.library;
using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKCamelot.Timer
{
    public class WorldTimer
    {
        private static System.Threading.Timer timer;
        private ulong Tick;
        private static WorldTimer _instance;
        public static WorldTimer getInstance()
        {
            if (_instance == null)
            {
                _instance = new WorldTimer();
            }
            return _instance;
        }
        public void start()
        {
            timer = new System.Threading.Timer(new System.Threading.TimerCallback(run), null, 0, 1 * Settings.Second);
        }

        private void run(object state)
        {
            Tick++;
            PebbleSpwn();
            TimeAlarm();
            ToNotice();
            Del_GroundItem();
            PlayerTick();
            Messages();
            Quest();
        }

        private void PebbleSpwn()
        {
            if ( (Tick % (ulong)(Settings.PebbleSpwnTime/Settings.Second) ) == 0)
            {
                for (int i = 0; i < Settings.VillageName.Count; i++)
                {
                    var itemlist = World.NewItems.Where(xe => xe.Value.m_Channel == i && xe.Value.m_Map == (Settings.VillageName.ElementAt(i) + ",42") && xe.Value.GetType() == typeof(script.item.조약돌)).ToList();

                    if (itemlist.Count < Settings.PebbleSpwnAmount)
                    {
                        int rndX = Util.RandomMinMax(-Settings.PebbleSpwnRange, Settings.PebbleSpwnRange);
                        int rndY = Util.RandomMinMax(-Settings.PebbleSpwnRange, Settings.PebbleSpwnRange);

                        script.item.Item item = (script.item.Item)Activator.CreateInstance(typeof(script.item.조약돌));
                        item.m_Channel = i;
                        item.m_Map = Settings.VillageName.ElementAt(i) + ",42";
                        item.Quantity = 1;


                        Point2D tempmob = null;
                        while ((tempmob = Map.SpawnPointFixed(i, item.m_Map, 61 - Settings.PebbleSpwnRange, 116 - Settings.PebbleSpwnRange, 61 + Settings.PebbleSpwnRange, 116 + Settings.PebbleSpwnRange)) == null) ;
                        item.Loc = tempmob;
                        

                        item.Drop();
                        World.NewItems.TryAdd(item.m_Serial, item);
                    }
                }
            }
        }
        private void TimeAlarm()
        {
            if (Settings.TimeAlarm)
            {
                if (DateTime.Now.Minute == 00 && DateTime.Now.Second == 00)
                {
                        World.SendToChatAll(new QueDele(1, "all", new UpdateChatBox(0x20, 0x02, 1, string.Format("[뻐꾸기] : {0}시!!!", DateTime.Now.Hour)).Compile()));
                }
            }
        }

        private void Del_GroundItem()
        {
            if ((Tick % 10) != 0)
                return;

            var ItemList = World.NewItems.Where(xe => xe.Value.DropTime != 0 && xe.Value.m_Map != "inventory").OrderByDescending(xe => xe.Value.DropTime).ToList();

            foreach (var item in ItemList)
            {
                if (LKCamelot.Server.tickcount.ElapsedMilliseconds - item.Value.DropTime > item.Value.ValidTime)
                {
                    if (item.Value.m_Map == "오목")
                        continue;

                    try
                    {
                        io.IOClient.WriteDeleteGroundItem(string.Format("{0} {1},{2},{3},{4},{5},{6},{7}", DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]"),
                                                                                                           item.Value.DropTime,
                                                                                                           item.Key,
                                                                                                           item.Value.Name,
                                                                                                           item.Value.Stage,
                                                                                                           item.Value.Quantity,
                                                                                                           item.Value.m_Map,
                                                                                                           item.Value.Loc.X + "-" + item.Value.Loc.Y
                                                                                                           ));
                    }
                    catch
                    {
                        Console.WriteLine("DeleteGroundItem Err");
                    }
                    
                    item.Value.DeleteGround();
                }
            }
        }
        private void ToNotice()
        {
            if ((Tick % 600) == 0)
                World.SendToAll(new QueDele("all", new UpdateChatBox(0x21, 0x67, 1, Settings.Notice).Compile()));

            if ((Tick % 420) == 0)
            {
                for(int i=0; i<Settings.VillageName.Count();i++)
                    World.SendToChatAll(new QueDele(Settings.VillageName.ElementAt(i), new UpdateChatBox(0xB6, 0x5A, 9, string.Format("[아론] : 여기는 {0} 입니다.", Settings.VillageName.ElementAt(i))).Compile()));  // 흰색 외치기
            }

            if ((Tick % (ulong)(Settings.SaveTime / Settings.Second)) == 0)
            {
                World.SaveAll();
                string chat = "[아론] : 마지막 왕국에 오신걸 환영합니다.";
                World.SendToAll(new QueDele("all", new UpdateChatBox(0x20, 0x02, 1, chat).Compile()));
            }

            if (Settings.XPrate[1] != 0 && Settings.XPrate[1] < Server.tickcount.ElapsedMilliseconds)
            {
                Settings.XPrate[0] = 1.0f; 
                Settings.XPrate[1] = 0; 
                World.SendToAll(new QueDele("all", new UpdateChatBox(0xff, 0x7e, 15461, string.Format("경험치 배율 이벤트가 종료되었습니다.")).Compile()));
            }
            if (Settings.GOLDRate[1] != 0 && Settings.GOLDRate[1] < Server.tickcount.ElapsedMilliseconds)
            {
                Settings.GOLDRate[0] = 1.0f; 
                Settings.GOLDRate[1] = 0; 
                World.SendToAll(new QueDele("all", new UpdateChatBox(0xff, 0x7e, 15461, string.Format("골드 배율 이벤트가 종료되었습니다.")).Compile()));
            }
            if (Settings.DropRate[1] != 0 && Settings.DropRate[1] < Server.tickcount.ElapsedMilliseconds)
            {
                Settings.DropRate[0] = 1.0f;
                Settings.DropRate[1] = 0;
                World.SendToAll(new QueDele("all", new UpdateChatBox(0xff, 0x7e, 15461, string.Format("드랍 배율 이벤트가 종료되었습니다.")).Compile()));
            }
            if (Settings.ABALRate[1] != 0 && Settings.ABALRate[1] < Server.tickcount.ElapsedMilliseconds)
            {
                Settings.ABALRate[0] = 1.0f;
                Settings.ABALRate[1] = 0;
                World.SendToAll(new QueDele("all", new UpdateChatBox(0xff, 0x7e, 15461, "아발론 배율 이벤트가 종료되었습니다.").Compile()));
            }
        }
        private void PlayerTick()
        {
            if (World.w_server == null)
                return;

            for (int i = 0; i < Settings.VillageName.Count; i++)
            {
                var loginplayer = Server.world.handler.add[i].Values.Where(xe => xe.loggedIn).ToList();
                foreach (var playe in loginplayer)
                {
                    #region 평민알림
                    if ((Tick % 2400) == 0 && (playe.Class & library.Class.Beginner) != 0 && playe.Level >= 5)
                    {
                        string str = "직업을 가져야 합니다.";
                        playe.client.SendPacket(new UpdateChatBox(0x21, 0x67, 1, str).Compile());
                        str = "직업에는 기사, 검객, 위저드, 샤먼이 있습니다.";
                        playe.client.SendPacket(new UpdateChatBox(0x21, 0x67, 1, str).Compile());
                    }
                    #endregion

                    #region 겹침 처리
                    if ((Tick % 60) == 0)
                    {
                        var play = PlayerHandler.getSingleton().add[i].Values.Where(xe => xe.loggedIn && xe.Name != playe.Name && xe.Loc.X == playe.Loc.X && xe.Loc.Y == playe.Loc.Y).ToList();
                        if (play != null)
                        {
                            foreach (var p in play)
                            {
                                p.m_Loc = new Point2D(Settings.StartLoc.X + Util.Random(5), Settings.StartLoc.Y + Util.Random(5));
                                p.Map = Settings.VillageName.ElementAt(i) + Settings.StartMap;
                            }
                        }
                    }
                    #endregion

                    #region Buff
                    foreach (var buff in playe.m_Buffs)
                    {
                        long time = (buff.Key - Server.tickcount.ElapsedMilliseconds) / Settings.Second;

                        if (buff.Value.BuffEffect.BuffType == script.spells.BuffCase.Wall)
                        {                        
                            List<script.monster.Monster> MonsterList = new List<script.monster.Monster>();
                            List<Point2D> List = World.FireWalAttackRage(buff.Value.BuffEffect.Dir, buff.Value.BuffEffect.Loc);

                            foreach(var mob in List)
                            {
                                MonsterList.AddRange(
                                        World.NewMonsters[playe.m_Channel].Where(xe =>
                                            xe.Value.Alive &&
                                            xe.Value.m_Map == buff.Value.BuffEffect.Map &&
                                            xe.Value.m_Loc.X == mob.X &&
                                            xe.Value.m_Loc.Y == mob.Y
                                    ).Select(xe => xe.Value).ToList());
                            }
                            foreach (var Monster in MonsterList)
                                Monster.TakeDamage(playe, buff.Value);
                        }


                        if (time <= 0)
                        {
                            script.spells.Spell temp;
                            playe.m_Buffs.TryRemove(buff.Key, out temp);
                            World.SendToRange(new QueDele(playe.m_Channel, playe.m_Map, new LKCamelot.model.SetObjectEffects(playe).Compile()), playe);
                            playe.client.SendPacket(new UpdateChatBox(0x21, 0x67, 1, string.Format("{0} 마법이 종료 됐습니다.", buff.Value.Name)).Compile());
                        }
                        else if (time <= 10 && time % 10 == 0)
                        {
                            playe.client.SendPacket(new UpdateChatBox(0x21, 0x67, 1, string.Format("{0} 마법이 {1}초 남았습니다.", buff.Value.Name, time)).Compile());
                        }

                    }
                    #endregion

                    #region HPMP
                    if ((Tick % 10) == 0)
                    {
                        float regamt = 0.1f;
                        if (playe.Map.Contains("휴게실"))
                            regamt = 0.25f;
                        playe.HPCur += (int)((float)playe.HP * regamt);
                        playe.MPCur += (int)((float)playe.MP * regamt);
                    }
                    #endregion

                    #region PK
                    if ((Tick % 10) == 0)
                    {
                        if (playe.pkhistoryTime > 0)
                            playe.pkhistoryTime -= ( 10 * Settings.Second );
                        if (playe.pkhistoryTime < 0)
                            playe.pkhistoryTime = 0;


                        if (playe.PKCount < Settings.PKRedCount)              // 노멀
                        {
                            //if (playe.Color != PlayerColor.None && !playe.Map.Contains("대전장"))
                            //    playe.Color = PlayerColor.None;
                        }
                        else if (playe.PKCount >= Settings.PKRedCount && playe.Color != Color.Red)
                            playe.Color = Color.Red;

                        if (playe.PKCount >= Settings.PrisonCount && !playe.Map.Contains("독방"))
                        {
                            playe.m_Loc = new Point2D(13, 12);
                            playe.Map = "독방" + "," + Settings.VillageName.ElementAt(playe.m_Channel);
                        }

                        if (playe.PKhistoryPlayer.Count != 0)
                        {
                            long time = Server.tickcount.ElapsedMilliseconds - Settings.PKRegesterTime;
                            var p = playe.PKhistoryPlayer.Where(xe => xe.Key < time).ToList();
                            Player tempp;
                            foreach (var list in p)
                            {
                                playe.PKhistoryPlayer.TryRemove(list.Key, out tempp);

                                int xp = (int)(playe.XP * Settings.MacroKillXP);
                                
                            }
                        }
                    }
                    
                    #endregion

                    #region 장비
                    if ((Tick % 60) == 0)
                    {
                        foreach (var item in playe.Equipped2)
                        {
                            if (playe.GetStat("str") < item.Value.StrReq || playe.GetStat("men") < item.Value.MenReq || playe.GetStat("dex") < item.Value.DexReq || playe.Level < item.Value.FullLevel)
                            {
                                if (playe.GetFreeSlots() >= 1)
                                    item.Value.Unequip(playe, item.Value.InvSlot);
                            }
                        }
                    }
                    #endregion

                    #region 자동 줍기
                    if (Settings.AutoAttackPickup && playe.AutoHit)
                    {
                        if ((Tick % 10) == 0)
                        {
                            var item1 = World.NewItems.Where(xe => xe.Value.m_Map != null
                                && xe.Value.m_Channel == playe.m_Channel
                                && xe.Value.m_Map == playe.Map
                                && xe.Value.Loc.X == playe.Loc.X && xe.Value.Loc.Y == playe.Loc.Y)
                               .FirstOrDefault();

                            if (item1.Value != null)
                                item1.Value.PickUp(playe);
                        }
                    }
                    #endregion
                }
            }
        }


        private void Messages()
        {
            if (World.NewNpcs == null)
                return;
            if ((Tick % 10) != 0)  return;

            for (int i = 0; i < Settings.VillageName.Count; i++)
            {
                foreach (var n in World.NewNpcs[i])
                {
                    if (n.Value is script.npc.Sign || n.Value is script.npc.BoardSign)
                        continue;

                    // 대사 치기
                    if (n.Value.ChatString != null && n.Value.ChatString.Count() > 0 && n.Value.Alive == true && Server.tickcount.ElapsedMilliseconds - n.Value.ChatTime > n.Value.LastChat)
                        n.Value.Event_Chat(n.Value.ChatString[Util.Random((n.Value.ChatString.Count() - 1))]);



                    if (n.Value.AliveTime != 0)
                    {
                        // 살만큼 살았으닌 죽어.
                        if (n.Value.Alive == true && n.Value.LastSpawn < Server.tickcount.ElapsedMilliseconds - n.Value.AliveTime)
                        {
                            n.Value.Alive = false;
                            World.SendToAll(new QueDele(n.Value.m_Channel, n.Value.m_Map, new DeleteObject(n.Value.m_Serial).Compile()));

                            if (n.Value is script.npc.BaseInstance)
                            {
                                (n.Value as script.npc.BaseInstance).Dispose();
                            }
                        }

                        //살아날 시간임.
                        if (n.Value.Alive == false && n.Value.LastSpawn + n.Value.AliveTime + n.Value.reSpawnTime < Server.tickcount.ElapsedMilliseconds)
                        {
                            if (n.Value is script.npc.BaseInstance)
                                continue;
                            n.Value.Alive = true;
                            n.Value.LastSpawn = Server.tickcount.ElapsedMilliseconds;
                            World.SendToAll(new QueDele(n.Value.m_Channel, n.Value.m_Map, new CreateNPC2(n.Value).Compile()));
                        }
                    }
                }
            }
        }
        private void Quest()
        {
            if ( (Server.tickcount.ElapsedMilliseconds - World.QuestTime) >= Settings.QuestTime)
            {
                for (int i = 0; i < Settings.VillageName.Count; i++)
                {
                    World.QuestTime = Server.tickcount.ElapsedMilliseconds;
                    var inplayer = Server.world.handler.add[i].Where(xe => xe.Value.loggedIn == true && xe.Value.Map.Contains("퀘스트")).ToList();
                    var outplayer = Server.world.handler.add[i].Where(xe => xe.Value.loggedIn == true && (xe.Value.CombackMap.Contains("Qweak") || xe.Value.Map.Contains("Qweak")) ).ToList();

                    foreach (var p in outplayer)
                    {
                        if (p.Value.CombackMap.Contains("Qweak"))
                        {
                            p.Value.CombackLoc = null;
                            p.Value.CombackMap = string.Empty;
                        }
                        else 
                            p.Value.client.HandleGo("마을");
                    }
                    foreach (var p in inplayer)
                    {
                        int X = Util.RandomMinMax(-2, 2);
                        int Y = Util.RandomMinMax(-2, 2);
                        p.Value.m_Loc = new Point2D(86 + X, 10 + Y);
                        p.Value.Map = "Qweak1";
                    }
                }
            }
        }
    }
}
