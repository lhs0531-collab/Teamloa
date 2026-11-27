
//#define NOSQL
#define PROMOCAP12

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

using LKCamelot.net;
using LKCamelot.util;
using LKCamelot.model;

using System.Diagnostics;
using System.Threading;
using System.Security.Cryptography;
using System.Net.NetworkInformation;


namespace LKCamelot.io
{
    public partial class IOClient
    {
        public Connection connection;
        private long connectedAt;
        public bool CStatus = true;
        public LKCamelot.net.PacketWriter mm_stream;
        private object _bufferLock = new object();
        public CastHandler castHandler;
        public CombatHandler2 combatHandler;
        private long ChatTimeout = 0, lastcmd = 0;
        public  int AliasStage = 0;
        public int AronStage = 0;
        bool firstlogin = false;
        public long keepalive = 0;
        public LKCamelot.model.Modules.NSA nsa;
        
        private readonly Stream incomingStream = new Stream();

        public Player player;
        public PlayerHandler handler;
        public IOClient(Connection s)
        {
            this.connection = s;
            this.connectedAt = Server.CurrentTimeMillis();
            this.mm_stream = new PacketWriter(255);
            this.castHandler = new CastHandler(this, handler);
            this.combatHandler = new CombatHandler2(this, handler);
            //    IOHostList.add(connectedFrom);
        }

        delegate void UpdatePacketInvoker(Byte[] packet);

        public void Parse(ConnectionDataEventArgs e)
        {
            incomingStream.AppendData(e.Data.ToArray());
            while (incomingStream.IsPacketAvailable())
            {
                var packet = incomingStream.PopPacket();

                try
                {
                    //Logger.LogIncomingPacket(message); 
                    //Console.WriteLine(packet.ToFormatedHexString());
                    //여기1
                    // Console.WriteLine(string.Format("RecvData : {0} - {1}",
                    //     packet.Length, packet.ToFormatedHexString()));

                    HandleIncoming(packet);
                }
                catch (NotImplementedException) { }
                catch (Exception ee)
                {
                    Console.WriteLine("[{0}]{1} Unknown process exception: {2}", DateTime.Now, player.Name, ee.StackTrace);
                }
            }
            incomingStream.Flush();
        }

        public void SendPacket(Byte[] packet)
        {
            if (nsa != null)
            {
                nsa.AppendPacketOut(packet);
            }

            lock (this._bufferLock)
                connection.Send(packet);
        }
        
        public short AttackRange(short x, short y, short x2, short y2)
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

            return -1;
        }
     //   System.Collections.Concurrent.ConcurrentDictionary<long, Point2D> walktrace = 
     //       new System.Collections.Concurrent.ConcurrentDictionary<long, Point2D>();
        System.Collections.Generic.List<long> walktrace = new System.Collections.Generic.List<long>();
        public bool Loaded = false;
        public void LoadPlayer(bool first = true)
        {
            if (player == null || (first && Loaded) )
                return;
            Loaded = true;
            player.LastAccessTime = DateTime.Now;
            player.IP = connection.Socket.RemoteEndPoint.ToString().Split(':')[0];

            if (Settings.IPNeighbor)
            {
                var PlayerList = PlayerHandler.getSingleton().Total.Where(xe => xe.Key != player.Name && xe.Value.loggedIn && xe.Value.IP == player.IP).OrderByDescending(xe=>xe.Value.Level).ToList();
                if (PlayerList.Count > 1)
                {
                    for (int i = 0; i < PlayerList.Count-1; i++)
                    {
                        PlayerList[i].Value.IPNeighbor = true;
                    }
                }
            }

            // 아이피 중복 이웃
            //if (Settings.IPNeighbor)
            //{
            //    if( PlayerHandler.getSingleton().Total.Where(xe => xe.Key != player.Name && xe.Value.loggedIn && xe.Value.IP == player.IP).Any())
            //        player.IPNeighbor = true;
            //    else
            //        player.IPNeighbor = false;
            //}

            //초기화
            bool soundFlag = player.SoundFlag;
            player.Clear();
            if (!first)
                player.SoundFlag = soundFlag;

            //마법셋팅
            foreach (var spell in player.MagicLearned)
                SendPacket(new CreateSlotMagic2(spell).Compile());

            //아이템 셋팅
            player.Equipped2.Clear();                                           // 재접속마다 장착 캐시를 실제 슬롯 상태로 재구성
            List<script.item.Item> unknownSlot = new List<script.item.Item>();  // 24, 31~39, 52 이상
            List<script.item.Item> orphan = new List<script.item.Item>();       // InvSlot < 0
            foreach (var item in World.NewItems.Where(xe => (xe.Value.m_Parent == player || xe.Value.ParSer == player.m_Serial) && !Settings.NoSaveItem.Contains(xe.Value.GetType()) ).ToList())
            {
                item.Value.Parent = player;
                int itemSlot = item.Value.InvSlot;

                if (itemSlot >= 0 && itemSlot < 24)
                {
                    SendPacket(new AddItemToInventory2(item.Value).Compile());
                }
                else if (itemSlot >= 25 && itemSlot <= 30)
                {
                    player.Equipped2.TryAdd(item.Value.m_Serial, item.Value);
                    SendPacket(new EquipItem2(item.Value).Compile());
                }
                else if (itemSlot >= 40 && itemSlot <= 51)
                {
                    SendPacket(new AddItemToEntrust(item.Value).Compile());
                }
                else if (itemSlot < 0)
                    orphan.Add(item.Value);
                else
                    unknownSlot.Add(item.Value);
            }

            // 슬롯이 없는 소유 아이템은 Clone 잔재(유령 아이템)다. 빈 칸에 다시 넣지 않고 기록 후 제거한다.
            // 저장 대상(InvSlot >= 0)이 아니므로 파일에는 원래 남지 않는 아이템이다.
            // 제거는 로그인(first) 때만 한다. 마을 티켓 이동(LoadPlayer(false)) 중에는 자동 줍기가
            // 소유자만 넣고 슬롯은 아직 넣지 않은 아이템이 잠깐 있을 수 있어 건드리지 않는다.
            if (!first)
                orphan.Clear();

            foreach (var it in orphan)
            {
                WriteBug(string.Format("{0} [LoadPlayer] orphan removed: player={1} serial={2} name={3} stage={4} qty={5}",
                    DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]"), player.Name, (int)it.m_Serial, it.Name, it.Stage, it.Quantity));
                script.item.Item removed;
                World.NewItems.TryRemove(it.m_Serial, out removed);
            }

            foreach (var it in unknownSlot)
            {
                int slot = player.GetFreeSlot();
                if (slot != -1)
                    it.InvSlot = slot;
                else
                    WriteBug(string.Format("{0} [LoadPlayer] unknown slot item not placed: player={1} serial={2} name={3} slot={4}",
                        DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]"), player.Name, (int)it.m_Serial, it.Name, it.InvSlot));
            }

            if (Settings.GMS.Contains(player.Name))
            {
                player.m_Loc = new Point2D(Settings.StartGMLoc.X, Settings.StartGMLoc.Y);
                player.Map = Settings.VillageName.ElementAt(this.player.m_Channel) + Settings.StartGMMap;
            }
            else
                player.client.HandleGo("마을");

            //알림 셋팅
            SendPacket(new UpdateChatBox(0xff, 0x7e, 15461, Settings.Version).Compile());
            if (Settings.GOLDRate[0] != 1.0 || Settings.XPrate[0] != 1.0 || Settings.ABALRate[0] != 1.0)
                SendPacket(new UpdateChatBox(0xf0, 0x7b, 27296, string.Format("골드배율:x{0:f}, 경험치배율:x{1:f},아발론배율:x{2:f}가 적용중입니다.", Settings.GOLDRate[0], Settings.XPrate[0], Settings.ABALRate[0])).Compile());

            SendPacket(new UpdateChatBox(0xB6, 0x56, 9, Settings.WelComeMessage).Compile());
            foreach (string str in Settings.WelComeNotice)
                SendPacket(new UpdateChatBox(0xB6, 0x56, 9, str).Compile());
            
            if (firstlogin)
            {
                foreach(var str in Settings.BegginerNotice)
                    SendPacket(new UpdateChatBox(0xB6, 0x56, 9, str).Compile());
            }
            if ( player.IPNeighbor || player.BenNeighbor || (Settings.Neighbor && player.Level >= Settings.NeighborLevel && player.neighbor) )
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("이웃 나라 백성으로 접속 되었습니다.")).Compile());


            if (Settings.LoginAlarm && !Settings.GMS.Contains(player.Name))
                World.SendToAll(new QueDele(player.m_Serial, "all", new UpdateChatBox(0x94, 0x52, 1, string.Format("{0}님이 접속하였습니다.", player.Name)).Compile()));
        }

        /// <param name="force">
        /// true면 감옥 수치·얼음 상태여도 이동시킨다. 사망 처리 전용.
        /// (이동이 막히면 죽은 자리에 HP 1로 남아, 맞을 때마다 사망 처리(골드·아이템 드랍)가 반복된다)
        /// </param>
        public void HandleGo(string goloc, bool force = false)
        {
            if (!force && (player.PKCount >= Settings.PrisonCount || player.m_Buffs.Where(xe=>xe.Value.BuffEffect.BuffType == script.spells.BuffCase.Freezing).Any()))
                return;

            var gotoMap = script.map.GotoCmd.Gotocmd[player.m_Channel].Where(xe=>xe.GoName == goloc.ToLower()).FirstOrDefault();

            int X = Util.RandomMinMax(-1, 1);
            int Y = Util.RandomMinMax(-1, 1);
            if (gotoMap != null)
            {
                var GotoMap = model.Map.FullMaps[player.m_Channel].Where(Xe => Xe.Key == gotoMap.Map).FirstOrDefault().Value;

                if ( !GotoMap.isOpen)
                    player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "이동할 수 없는 맵입니다.").Compile());

                else if (GotoMap.LimitLevel > this.player.Level)
                    player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "레벨이 부족합니다.").Compile());

                else
                {
                    player.m_Loc = new Point2D(gotoMap.Loc.X + X, gotoMap.Loc.Y + Y);
                    player.Map = gotoMap.Map;
                }
            }
            else
            {
                var guildmap = World.NewGuilds.Where(xe => xe.Value.Name == goloc.ToUpper() && xe.Value.Board.m_Channel == player.m_Channel && !xe.Value.GuildMapEnabled).FirstOrDefault().Value;
                if (guildmap != null)
                {
                    player.m_Loc = new Point2D(guildmap.Board.m_Loc.X + X, guildmap.Board.m_Loc.Y + Y + 2);
                    player.Map = guildmap.Board.m_Map;
                }
            }
            #region V1
            //int X = Util.RandomMinMax(-1, 1);
            //int Y = Util.RandomMinMax(-1, 1);

            //switch (goloc.ToLower())
            //{
            //    case "village":
            //    case "마을":
            //        player.m_Loc = new Point2D(90 + X, 176 + Y);
            //        player.Map = Settings.VillageName.ElementAt(this.player.m_Channel) + ",32";
            //        break;

            //    case "alias":
            //    case "알리아스":
            //        player.m_Loc = new Point2D(92 + X, 154 + Y);
            //        player.Map = Settings.VillageName.ElementAt(this.player.m_Channel) + ",32";
            //        break;
            //    case "rest":
            //    case "death":
            //    case "휴게실":
            //    case "쉼터":
            //    case "죽음":
            //        player.m_Loc = new Point2D(18 + X, 18 + Y);
            //        player.Map = "휴게실";
            //        break;
            //    case "arnold":
            //    case "아놀드":
            //        player.m_Loc = new Point2D(8 + X, 12 + Y);
            //        player.Map = "대장간";
            //        break;
            //    case "aron":
            //    case "아론":
            //        player.m_Loc = new Point2D(98 + X, 60 + Y);
            //        player.Map = Settings.VillageName.ElementAt(this.player.m_Channel) + ",32";
            //        break;
            //    case "loen":
            //    case "로엔":
            //        player.m_Loc = new Point2D(8 + X, 12 + Y);
            //        player.Map = "시약상";
            //        break;

            //    case "점원":
            //        player.m_Loc = new Point2D(74 + X, 97 + Y);
            //        player.Map = Settings.VillageName.ElementAt(this.player.m_Channel) + ",32";
            //        break;
            //    case "노점상":
            //    case "employee":
            //        player.m_Loc = new Point2D(128 + X, 94 + Y);
            //        player.Map = Settings.VillageName.ElementAt(this.player.m_Channel) + ",32";
            //        break;

            //    case "beginner":
            //    case "초보":
            //        player.m_Loc = new Point2D(31 + X, 20 + Y);
            //        player.Map = "초보";
            //        break;
            //    case "weakly":
            //    case "위클리":
            //        player.m_Loc = new Point2D(66 + X, 123 + Y);
            //        player.Map = Settings.VillageName.ElementAt(this.player.m_Channel) + ",32";
            //        break;
            //    case "skel":
            //    case "스켈":
            //        player.m_Loc = new Point2D(139 + X, 137 + Y);
            //        player.Map = Settings.VillageName.ElementAt(this.player.m_Channel) + ",32";
            //        break;
            //    case "level":
            //    case "터":
            //        player.m_Loc = new Point2D(29, 32);
            //        player.Map = "터";
            //        break;
            //    case "item":
            //    case "아이템":
            //        player.m_Loc = new Point2D(70, 68);
            //        player.Map = Settings.VillageName.ElementAt(this.player.m_Channel) + ",32";
            //        break;

            //    case "가보자":
            //        if (player.Level >= 30)
            //        {
            //            player.m_Loc = new Point2D(25 + X, 25 + Y);
            //            player.Map = "가보자";
            //        }
            //        else
            //            SendPacket(new UpdateChatBox(0x00, 0xff, 1, "레벨이 부족합니다.").Compile());
            //        break;

            //    case "biggun":
            //    case "빅건":
            //        player.m_Loc = new Point2D(167 + X, 119 + Y);
            //        player.Map = Settings.VillageName.ElementAt(this.player.m_Channel) + ",32";
            //        break;

            //    case "퀘스트":
            //    case "오지마세요":
            //        player.m_Loc = new Point2D(8 + X, 11 + Y);
            //        player.Map = "퀘스트";
            //        break;

            //    case "교환":
            //        player.m_Loc = new Point2D(42 + X, 40 + Y);
            //        player.Map = "교환";
            //        break;

            //    case "miro":
            //        player.m_Loc = new Point2D(135 + X, 58 + Y);
            //        player.Map = Settings.VillageName.ElementAt(this.player.m_Channel) + ",32";
            //        break;
            //    case "보물섬":
            //        player.m_Loc = new Point2D(150 + X, 56 + Y);
            //        player.Map = Settings.VillageName.ElementAt(this.player.m_Channel) + ",23";
            //        break;

            //    case "treasureland":
            //        player.m_Loc = new Point2D(25 + X, 25 + Y);
            //        player.Map = "TreasureLand";
            //        break;



            //    case "지구라트":
            //    case "vv1":
            //        player.m_Loc = new Point2D(97 + X, 49 + Y);
            //        player.Map = "지구라트1";
            //        break;
            //    case "그레이트":
            //        player.m_Loc = new Point2D(19 + X, 23 + Y);
            //        player.Map = "Great";
            //        break;

            //    case "가디언":
            //        if (player.Level == Settings.MaxLevel)
            //        {
            //            player.m_Loc = new Point2D(68 + X, 21 + Y);
            //            player.Map = "가디언";
            //        }
            //        else
            //            player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "7승 전용 사냥터입니다.").Compile());

            //        break;


            //    case "광산":
            //    case "miner":
            //        player.m_Loc = new Point2D(17 + X, 39 + Y);
            //        player.Map = "광산0";
            //        break;
            //    case "아발론":
            //        player.m_Loc = new Point2D(15 + X, 38 + Y);
            //        player.Map = "아발론";
            //        break;
            //    case "soccerfield":
            //    case "축구장":
            //        player.m_Loc = new Point2D(23 + X, 18 + Y);
            //        player.Map = "축구장";
            //        break;

            //    case "이기자":
            //    case "대전장":
            //        player.m_Loc = new Point2D(10 + X, 10 + Y);
            //        player.Map = "대전장";
            //        break;
            //    case "큰대전장":
            //        player.m_Loc = new Point2D(18 + X, 18 + Y);
            //        player.Map = "큰대전장";
            //        break;

            //    case "오목":
            //        player.m_Loc = new Point2D(18 + X, 18 + Y);
            //        player.Map = "오목";
            //        break;
            //    case "감옥":
            //    case "독방":
            //        player.m_Loc = new Point2D(13 + X, 12 + Y);
            //        player.Map = "독방";
            //        break;

            //    default:
            //        //길드 맵이동
            //        var guildmap = World.NewGuilds.Where(xe => xe.Value.Name == goloc.ToUpper() && xe.Value.Board.m_Channel == player.m_Channel && !xe.Value.GuildMapEnabled).FirstOrDefault().Value;
            //        if (guildmap != null)
            //        {
            //            player.m_Loc = new Point2D(guildmap.Board.m_Loc.X + X, guildmap.Board.m_Loc.Y + Y + 2);
            //            player.Map = guildmap.Board.m_Map;
            //        }
            //        break;
            //}

            #endregion
        }
        public void parseFace(short x, short y, short face)
        {
            if (face == 0)
            {
                player.Loc = new Point2D(x, --y);
            }
            if (face == 1)
            {
                player.Loc = new Point2D(++x, --y);
            }
            if (face == 2)
            {
                player.Loc = new Point2D(++x, y);
            }
            if (face == 3)
            {
                player.Loc = new Point2D(++x, ++y);
            }
            if (face == 4)
            {
                player.Loc = new Point2D(x, ++y);
            }
            if (face == 5)
            {
                player.Loc = new Point2D(--x, ++y);
            }
            if (face == 6)
            {
                player.Loc = new Point2D(--x, y);
            }
            if (face == 7)
            {
                player.Loc = new Point2D(--x, --y);
            }
        }
        public void parseFace()
        {
            var face = player.Face;
            if (face == 0)
            {
                player.Loc.Y--;
            }
            if (face == 1)
            {
                player.Loc.X++;
                player.Loc.Y--;
            }
            if (face == 2)
            {
                player.Loc.X++;
            }
            if (face == 3)
            {
                player.Loc.X++;
                player.Loc.Y++;
            }
            if (face == 4)
            {
                player.Loc.Y++;
            }
            if (face == 5)
            {
                player.Loc.X--;
                player.Loc.Y++;
            }
            if (face == 6)
            {
                player.Loc.X--;
            }
            if (face == 7)
            {
                player.Loc.X--;
                player.Loc.Y--;
            }
        }
        public long LastAttack, LastCast;
        /*     public int ParseEquipSlot(Item item)
             {
                 var i = item.Sprite;

                 if (i == 0x04 || i == 0xB4 || i == 0xB2)
                 {
                     return LKCamelot.library.EquipSlot.Hat;
                 }
                 if (item.Sprite == 0x52)
                 {
                     return LKCamelot.library.EquipSlot.Amulet;
                 }
                 if ((i >= 9 && i <= 0x10) || (i >= 0x20 && i <= 0x24)
                     || i == 0xB5)
                 {
                     return LKCamelot.library.EquipSlot.Weapon;
                 }
                 if (i == 5 || i == 6 || i == 7 || i == 8 ||
                     i == 0xB1 || i == 0xB6 || i == 0xB8 || i == 0xB9)
                 {
                     return LKCamelot.library.EquipSlot.Armor;
                 }
                 if ((i >= 0x11 && i <= 0x13) || i == 0xB7)
                 {
                     return LKCamelot.library.EquipSlot.Shield;
                 }
                 if (item.Sprite == 1 || item.Sprite == 2 || item.Sprite == 0x53)
                 {
                     return LKCamelot.library.EquipSlot.Ring;
                 }

                 return -1;
             }*/

        public Point2D AdjecentTile(short swingloc)
        {
            if (swingloc == 0)
                return new Point2D(player.Loc.X, player.Loc.Y - 1);
            if (swingloc == 1)
                return new Point2D(player.Loc.X + 1, player.Loc.Y - 1);
            if (swingloc == 2)
                return new Point2D(player.Loc.X + 1, player.Loc.Y);
            if (swingloc == 3)
                return new Point2D(player.Loc.X + 1, player.Loc.Y + 1);
            if (swingloc == 4)
                return new Point2D(player.Loc.X, player.Loc.Y + 1);
            if (swingloc == 5)
                return new Point2D(player.Loc.X - 1, player.Loc.Y + 1);
            if (swingloc == 6)
                return new Point2D(player.Loc.X - 1, player.Loc.Y);
            if (swingloc == 7)
                return new Point2D(player.Loc.X - 1, player.Loc.Y - 1);

            return new Point2D(1, 1);
        }

        public void HandleIncoming(Byte[] data)
        {
            if (nsa != null)
            {
                nsa.AppendPacketIn(data);
            }
            if (Server.tickcount.ElapsedMilliseconds - lastcmd < 300)
                return;

            PacketReader p = null;
            switch (data[0])
            {
                case 0x34: // Keep Alive
                    //if (Server.tickcount.ElapsedMilliseconds - player.lastmove >= 15 * Settings.Minute)
                    //    player.client.connection.Disconnect();

                    keepalive = Server.tickcount.ElapsedMilliseconds;
                    break;
                //Identifiy
                /*     case 0x37:
                         //     for (int x = 0; x < 40; x++)
                         //     {
                         //        SendPacket(new CreateSlotMagic(new MagicSpell((byte)(x+81), "Hii", 1, 1, (byte)x, library.MagicType.Casted)).Compile());
                         //    }
                         int y = 14;
                         byte sprite = 0;
                         for (int x = 0; x < 255; x++)
                         {
                             Thread.Sleep(100);
                             if (x != 0 && x % 19 == 0)
                             {
                                 y += 3;
                                 x -= 19;
                             }
                             //    SendPacket(new CreateMonster(new Monster(3, (short)(x+23),(short)y,"village.map", sprite.ToString(), sprite, 0), Serial.NewMobile).Compile());
                             sprite++;
                         }
                         break;*/
                #region 찾기
                case 0x3A: //find
                    if (player.Map.Contains("시약상"))
                    {
                        var slot = data[1] + 40;
                        var itemtofind = World.NewItems.Where(xe => xe.Value.ParSer == player.m_Serial && xe.Value.InvSlot == slot).FirstOrDefault().Value;
                        if (itemtofind != null)
                        {
                            if (itemtofind is script.item.BasePotion)
                            {
                                // 정책: 같은 물약은 인벤토리에 한 묶음(Limit)까지만. 가득 찬 묶음이 하나라도 있으면 꺼내지 않는다.
                                var samePotions = player.Inventory.Where(xe => xe.Name == itemtofind.Name).ToList();
                                if (samePotions.Any(xe => xe.Quantity >= xe.Limit))
                                    return;

                                var potions = samePotions.FirstOrDefault();

                                if (potions != null)
                                {
                                    // 기존 묶음에 합치는 경우는 빈 칸이 필요 없다.
                                    int Remainder = potions.Limit - potions.Quantity;   // 소유 가능한 갯수를 구함
                                    if (itemtofind.Quantity > Remainder)
                                    {
                                        itemtofind.Quantity -= Remainder;                          // 바닥에 떨어진 아이템 갯수를  삭제
                                        potions.Quantity += Remainder;                  // 줍는다.
                                        SendPacket(new LKCamelot.model.AddItemToEntrust(itemtofind).Compile());       //보관함에 뿌린다.
                                        SendPacket(new LKCamelot.model.AddItemToInventory2(potions).Compile());       //인벤토리에 뿌린다.
                                    }
                                    else
                                    {
                                        potions.Quantity += itemtofind.Quantity;                   // 아이템을 주워 넣는다.
                                        SendPacket(new LKCamelot.model.AddItemToInventory2(potions).Compile());       //인벤토리에 뿌린다.

                                        // 인벤토리 아이템 삭제
                                        itemtofind.Delete(player);
                                    }
                                }
                                else
                                {
                                    int freeSlot = player.GetFreeSlot();
                                    if (freeSlot == -1)
                                        return;
                                    itemtofind.InvSlot = freeSlot;
                                }
                            }
                            else
                            {
                                int freeSlot = player.GetFreeSlot();
                                if (freeSlot == -1)
                                    return;
                                itemtofind.InvSlot = freeSlot;
                            }
                        }
                    }
                    break;
                #endregion

                #region 보관
                case 0x36: //Entrust
                    if (player.Map.Contains("시약상"))
                    {
                        var itemtoentrust = World.NewItems.Where(xe => xe.Value.ParSer == player.m_Serial && xe.Value.InvSlot == data[1]).FirstOrDefault();
                        
                        if (itemtoentrust.Value != null)
                        {
                            int index = (World.NewNpcs[this.player.m_Channel].Count * this.player.m_Channel) + 3;
                            World.NewNpcs[this.player.m_Channel].Where(xe =>xe.Value.Name.Contains("로엔")).FirstOrDefault().Value.Event_Give(this.player, itemtoentrust.Value, itemtoentrust.Value.Quantity);
                        }
                    }
                    break;
                #endregion

                #region 팔기

                case 0x35: //sell
                    if (player.Map.Contains("대장간"))
                    {
                        var itemtosell = World.NewItems.Where(xe => xe.Value.ParSer == player.m_Serial && xe.Value.InvSlot == data[1]).FirstOrDefault();
                        if (itemtosell.Value != null && itemtosell.Value.SellPrice > 0)
                        {
                            try
                            {
                                checked 
                                { 
                                    player.Gold += (uint)(itemtosell.Value.SellPrice * itemtosell.Value.Quantity);
                                }
                                itemtosell.Value.Delete(player);
                            }
                            catch (OverflowException)
                            {
                            }
                        }
                    }
                    break;
                #endregion

                #region 마법

                //Cast
                //  3D-00-00-01-00-00-00-0A-00-09-00
                case 0x1B:
                case 0x3D:
                case 0x19:          // 픽업
                case 0x18:
                    if (LKCamelot.Server.tickcount.ElapsedMilliseconds - player.CastSpeed > LastCast && !player.m_Buffs.Where(xe=>xe.Value.BuffEffect.BuffType == script.spells.BuffCase.Freezing).Any()) 
                    {
                        p = new PacketReader(data, data.Count(), true);
                        int spellslot = p.ReadInt16();
                        if (40 <= spellslot)
                            return;

                        script.spells.Spell castspell = player.MagicLearned.Where(xe => xe.Slot == spellslot && xe.Level > 0).FirstOrDefault();

                        LastCast = Server.tickcount.ElapsedMilliseconds;
                        if (castspell == null)
                        {
                            return;
                        }
                        else if ( Settings.NoSpell.ContainsKey(castspell.Name) && Settings.NoSpell[castspell.Name].Contains(player.Map.Split(',')[0]) )
                        {
                            if (player.SystemBlock)
                                player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("{0} 마법을 실패하였습니다.", castspell.Name)).Compile());
                            return;
                        }

                        if (!(castspell is script.spells.DemonDeath) && player.MPCur >= castspell.RealManaCost(player))
                            player.MPCur -= castspell.RealManaCost(player);
                        else if (castspell is script.spells.DemonDeath && player.HPCur >= player.HP * 0.30)
                        {
                            player.m_HPCur -= castspell.RealManaCost(player);
                            player.client.SendPacket(new SetHP(player).Compile());
                        }
                        else
                            return;


                        //player.spelltrace.TryAdd(Server.tickcount.ElapsedMilliseconds, castspell.Name);
                        short castx, casty;
                        int castonid;
                        switch (data[0])
                        {
                            // 셀프 마법
                            case 0x19:                    // Casted
                                castspell.UseSpell(player);
                                return;

                            // 위치 지정 마법 (텔포, )
                            case 0x1B:                     // Coord
                                if(player.SystemBlock)
                                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("{0} 마법을 사용했습니다.", castspell.Name)).Compile());
                                castx = p.ReadInt16();
                                casty = p.ReadInt16();
                                castspell.UseSpell(player, castx, casty);
                                return;

                            // 타겟 or 좌표
                            case 0x18:
                            case 0x3D:
                                castonid = p.ReadInt32();
                                castx = p.ReadInt16();
                                casty = p.ReadInt16();
                                castspell.UseSpell(player, castonid, castx, casty);
                                return;
                        }
                    }
                    break;
                #endregion

                #region 마법V2

                ////Cast
                ////  3D-00-00-01-00-00-00-0A-00-09-00
                //case 0x1B:
                //case 0x3D:
                //case 0x19:          // 픽업
                //case 0x18:
                //    p = new PacketReader(data, data.Count(), true);
                //    int spellslot = p.ReadInt16();
                //    if (40 <= spellslot)
                //        return;

                //    script.spells.Spell castspell = player.MagicLearned.Where(xe => xe.Slot == spellslot && xe.Level > 0).FirstOrDefault();                    
                //    if (castspell == null)
                //        return;



                //    if(Server.tickcount.ElapsedMilliseconds >= castspell.CastTime + castspell.RecastTime - player.CastSpeed) )
                //    {
                //        castspell.CastTime = Server.tickcount.ElapsedMilliseconds;

                //        if (Settings.NoSpell.ContainsKey(castspell.Name) && Settings.NoSpell[castspell.Name].Contains(player.Map))
                //        {
                //            player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("{0} 마법을 실패하였습니다.", castspell.Name)).Compile());
                //            return;
                //        }


                //        if (!(castspell is script.spells.DemonDeath) && player.MPCur >= castspell.RealManaCost(player))
                //            player.MPCur -= castspell.RealManaCost(player);
                //        else if (castspell is script.spells.DemonDeath && player.HPCur >= player.HP * 0.30)
                //        {
                //            player.m_HPCur -= castspell.RealManaCost(player);
                //            player.client.SendPacket(new SetHP(player).Compile());
                //        }
                //        else
                //            return;


                //        short castx, casty;
                //        int castonid;
                //        switch (data[0])
                //        {
                //            // 셀프 마법
                //            case 0x19:                    // Casted
                //                castspell.UseSpell(player);
                //                return;

                //            // 위치 지정 마법 (텔포, )
                //            case 0x1B:                     // Coord
                //                player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("{0} 마법을 사용했습니다.", castspell.Name)).Compile());
                //                castx = p.ReadInt16();
                //                casty = p.ReadInt16();
                //                castspell.UseSpell(player, castx, casty);
                //                return;

                //            // 타겟 or 좌표
                //            case 0x18:
                //            case 0x3D:
                //                castonid = p.ReadInt32();
                //                castx = p.ReadInt16();
                //                casty = p.ReadInt16();
                //                castspell.UseSpell(player, castonid, castx, casty);
                //                return;
                //        }
                //    }
                //    break;
                #endregion


                #region 공격
                //Attack
                case 0x17:
                    if (LKCamelot.Server.tickcount.ElapsedMilliseconds - player.AttackSpeed > LastAttack)
                    {
                        LastAttack = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                        World.SendToSoundRange(new QueDele(player.m_Channel, player.Map, new EffectSound(library.EffectSound.Attack).Compile()), player);

                        World.SendToRange(new QueDele(player.m_Serial, player.m_Channel, player.Map, new SwingAnimation(player.m_Serial, player.Face).Compile()), player);
                        combatHandler.HandleMelee(player, data[1]);
                    }
                    break;
                #endregion

                #region 엔피씨 샵
                //NPC Shop
                case 0x45:
                    var npclook = World.NewNpcs[this.player.m_Channel].Where(xe => xe.Key == data[1]).FirstOrDefault();
                    if (npclook.Value != null)
                    {
                        if (npclook.Value is script.npc.Sign)
                        {
                            string message = npclook.Value.ChatString[0];
                            if (message != "" && npclook.Value.Move != null)
                                SendPacket(new AutoMove(npclook.Value.Move).Compile());
                            World.SendToRange(new QueDele(player.m_Channel, player.Map, new UpdateCharChatBox(npclook.Key, message).Compile()), player, 12);

                        }
                        else if (npclook.Value is script.npc.BoardSign)
                            World.SendToRange(new QueDele(player.m_Channel, player.Map, new UpdateCharChatBox(npclook.Key, npclook.Value.ChatString[0]).Compile()), player, 12);
                        
                        else
                        {
                            if (npclook.Value.m_Map == player.Map && npclook.Value.Gump != null)
                                SendPacket(new SpawnShopGump2(npclook.Value.Gump).Compile());
                        }
                    }
                    p = new PacketReader(data, data.Count(), true);
                    var auto = World.NewAutoShop.Where(xe => xe.Key == p.ReadInt32()).FirstOrDefault();
                    if (auto.Value != null)
                    {
                        if (auto.Value is script.npc.AutoShopNPC)
                            if (auto.Value.m_Map == player.Map && auto.Value.Gump != null)
                                SendPacket(new SpawnShopGump2(auto.Value.Gump).Compile());

                    }
                    break;

                case 0x2B: //2B-03-00-00-00-01-00-04-00-4D-65-6E-75-00
                    p = new PacketReader(data, data.Count(), true);
                    var npcid = p.ReadInt32();
                    var buyslot = p.ReadByte();

                    var npcitself = World.NewNpcs[this.player.m_Channel].Where(xe => xe.Key == npcid).FirstOrDefault();
                    if (npcitself.Value.m_Map == player.Map && npcitself.Value != null)
                        npcitself.Value.Buy(player, buyslot);

                    break;
                #endregion

                #region 사운드ON OFF
                case 0x44:
                    player.SoundFlag = !player.SoundFlag;
                    break;
                case 0x43:
                    player.SoundFlag = !player.SoundFlag;
                    break;
                #endregion

                #region 사용자 버튼
                case 0x0A:
                    if (data[1] == 0x56)
                    {
                        HandleCommands(new string[] { "@접속자" }, "@접속자");
                    }
                    break;
                #endregion

                #region 길드 버튼
                case 0x3B:
                    if(data[1] == 0x43)
                    {
                        HandleCommands(new string[] { "@길드원보기" }, "@길드원보기");                    
                    }
                    break;
                #endregion

                //Inventory
                #region 인벤토리 사용
                case 0x00: //use
                    if (data[1] == 0)
                        return;
                    var itemu = World.NewItems.Where(xe => xe.Value.m_Parent == player
                      && xe.Value.InvSlot == data[1]).FirstOrDefault();

                    if (itemu.Value != null)
                        itemu.Value.Use(player);

                    break;
                #endregion

                #region 인벤토리 드랍
                case 0x20: //drop
                    var item = World.NewItems.Where(xe => xe.Value.m_Parent == player && xe.Value.InvSlot == data[1]).FirstOrDefault();

                    if (item.Value != null && !item.Value.Stackable)
                        item.Value.Drop(player);
                    else
                        player.client.SendPacket(new AmountBox(item.Value.m_Serial, item.Value).Compile());

                    break;
                #endregion

                #region 아이템 줍기
                case 0x1F: //pickup
                    var item1 = World.NewItems.Where(xe => xe.Value.m_Map != null
                        && xe.Value.m_Channel == player.m_Channel
                        && xe.Value.m_Map == player.Map
                        && xe.Value.Loc.X == player.Loc.X && xe.Value.Loc.Y == player.Loc.Y)
                       .FirstOrDefault();

                    if (item1.Value != null)
                        item1.Value.PickUp(player);

                    break;
                #endregion

                #region 아이템 착용
                case 0x1E: //equip Use
                    // case 0x36:
                    var eitem = World.NewItems.Where(xe => xe.Value.m_Parent == player
                        && xe.Value.InvSlot == data[1]).FirstOrDefault();

                    if (eitem.Value != null)
                    {
                        if (eitem.Value is script.item.BaseArmor || eitem.Value is script.item.BaseWeapon || eitem.Value is script.item.BaseAccessory)
                            eitem.Value.Equip(player);
                        else
                            eitem.Value.Use(player);
                    }
                    break;
                #endregion

                #region 아이템 착용해제
                case 0x23://unequip
                    var uitem = World.NewItems.Where(xe => xe.Value.m_Parent == player
                        && xe.Value.InvSlot == (data[1] + 25)).FirstOrDefault();

                    if (uitem.Value != null)
                        uitem.Value.Unequip(player, data[1] + 25);
                    break;
                #endregion

                #region 아이템 교체
                case 0x25: //swap items
                    var item11 = World.NewItems.Where(xe => xe.Value.ParSer == player.m_Serial && xe.Value.InvSlot == data[1]).FirstOrDefault().Value;
                    int sss = 0;
                    if (data.Count() > 3)
                        sss = data[3];

                    var target1 = World.NewItems.Where(xe => xe.Value.ParSer == player.m_Serial && xe.Value.InvSlot == sss).FirstOrDefault().Value;

                    player.SwapItems(item11, target1, sss);

                    break;
                #endregion

                #region 아이템 건내기
                case 0x24: //drag,drop
                    var itemdragdrop = player.Inventory.Where(xe => xe.ParSer == player.m_Serial && xe.InvSlot == data[1]).FirstOrDefault();
                    if (itemdragdrop != null)
                    {
                        p = new PacketReader(data, data.Count(), false);
                        int targetid = p.ReadInt32();

                        if (itemdragdrop.Stackable)
                        {
                            player.client.SendPacket(new AmountBox(targetid, itemdragdrop).Compile());
                        }
                        else
                        {
                            //엔피씨
                            script.npc.BaseNPC npc;
                            World.NewNpcs[this.player.m_Channel].TryGetValue(targetid, out npc);
                            if (npc != null)
                            {
                                npc.Event_Give(player, itemdragdrop, itemdragdrop.Quantity);
                                return;
                            }
                            script.npc.AutoShopNPC autonpc = World.NewAutoShop.Where(xe => xe.Value.m_Channel == player.m_Channel && xe.Value.m_Serial == targetid).FirstOrDefault().Value;
                            if (autonpc != null)
                            {
                                autonpc.Event_Give(player, itemdragdrop, itemdragdrop.Quantity);
                                return;
                            }

                            //아이템
                            script.item.Item targetitem;
                            World.NewItems.TryGetValue(targetid, out targetitem);
                            if (targetitem != null)
                            {
                                targetitem.Event_Give(player, itemdragdrop);
                                return;
                            }

                            //사람
                            var play = Server.world.handler.add[player.m_Channel].Where(xe => xe.Value != null && xe.Value.m_Serial == targetid).FirstOrDefault().Value;
                            if (play != null)
                            {
                                if (play.Event_Give(player, itemdragdrop, itemdragdrop.Quantity))
                                {
                                    play.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}님이 {1} {2}를 건내주었습니다.", this.player.Name, itemdragdrop.NPrefix(), itemdragdrop.Name)).Compile());
                                    SendPacket(new DeleteItemSlot(data[1]).Compile());
                                }
                                return;
                            }
                        }
                    }
                    break;
                #endregion

                #region 수리
                //착용템 수리
                case 0x3C:
                    break;
                //인벤토리 수리
                case 0x38:
                    if (player.Map.Contains("대장간"))
                    {
                        var items = player.Inventory.Where(xe => xe.ParSer == player.m_Serial && xe.InvSlot == data[1]).FirstOrDefault();

                        if (items != null)
                            items.Repair(player);
                    }
                    break;
                #endregion

                //교환

                //교환
                #region 교환 체크
                case 0x4A:
                    //아이템 추가
                    if (data[1] == 0x07)
                    {
                        //GOLD
                        //player.client.SendPacket(new AmountBox(, itemdragdrop).Compile());
                    }
                    else if (data[1] == 0x02)
                    {
                        //data[7] : Item slot
                        var freeslot = player.Exchange.GetFreeSlot();

                        if (freeslot >= 0)
                        {
                            int Slot = 0;
                            if (((data.Length != 4) && (data.Length != 6)))
                                Slot = data[7];
                            player.Exchange.AddItem(Slot, 1);
                        }
                    }
                    break;
                case 0x4C:
                    player.Exchange.isCheck = !player.Exchange.isCheck;
                    break;
                case 0x4F:
                    player.Exchange.Cancle();
                    break;
                #endregion

                #region 수량
                case 0x4E:
                    if (data.Count() > 10)
                        return;
                    p = new PacketReader(data, data.Count(), true);
                    if (p.ReadByte() == 1)
                    {
                        short invslot = p.ReadInt16();
                        int Target = p.ReadInt32();
                        int amount = p.ReadInt16();
                        if (amount <= 0)
                            return;

                        var invItem = player.Inventory.Where(xe => xe.InvSlot == invslot).FirstOrDefault();
                        if (amount > invItem.Quantity)
                            amount = invItem.Quantity;

                        if (invItem != null)
                        {
                            //엔피씨
                            script.npc.BaseNPC npc;
                            World.NewNpcs[this.player.m_Channel].TryGetValue(Target, out npc);
                            if (npc != null && World.Dist2d(npc.m_Loc, player.Loc) <= 12 && npc.Alive == true)
                            {
                                npc.Event_Give(player, invItem, amount);
                                return;
                            }
                            //사람
                            var play = Server.world.handler.add[player.m_Channel].Where(xe => xe.Value != null && xe.Value.loggedIn == true && xe.Value.m_Serial == Target).FirstOrDefault().Value;
                            if (play != null && World.Dist2d(player.Loc, play.Loc) <= 12)
                            {
                                if (play.Event_Give(player, invItem, amount))
                                {
                                    play.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}님이 {1} {2}를 건내주었습니다.", this.player.Name, invItem.NPrefix(), invItem.Name)).Compile());
                                    return;
                                }
                            }
                            else
                            {
                                invItem.Drop(player, amount);
                            }
                        }
                    }
                    else
                    {
                        //취소
                    }
                    break;
                #endregion              

                #region 채팅
                //Chat message
                case 0x16:
                    p = new PacketReader(data, data.Count(), false);
                    //var str = p.ReadString();
                    var str = p.ReadHanString();            //한글 변환
                    var str2 = str.Split(' ');

                    if (Util.ContainsUnicodeCharacter(str))
                    {
                        var x = 1;
                    }
                    if (str[0] == '@' || str[0] == '#')
                        Server.mainForm.EnqueueCmdChat("[" +player.Name + "] : " +str);
                    HandleChat(str2, str);

                    break;
                #endregion

                #region 이동
                case 0x46:
                    if (player.WalkingMode == 0x00)
                        return;
                    player.WalkingMode = 0x00;
                    World.SendToAll(new QueDele(player.m_Serial, player.m_Channel, player.Map, new SetObjectEffects(player).Compile()));
                    player.client.SendPacket(new SetObjectEffects(player).Compile());
                    break;

                case 0x47:
                    if (player.WalkingMode == 0x01)
                        return;
                    player.WalkingMode = 0x01;
                    World.SendToAll(new QueDele(player.m_Serial, player.m_Channel, player.Map, new SetObjectEffects(player).Compile()));
                    player.client.SendPacket(new SetObjectEffects(player).Compile());
                    break;

                case 0x48:
                    if (player.WalkingMode == 0x02 || !Settings.GMS.Contains(player.Name))
                        return;
                    player.WalkingMode = 0x02;
                    World.SendToAll(new QueDele(player.m_Serial, player.m_Channel, player.Map, new SetObjectEffects(player).Compile()));
                    player.client.SendPacket(new SetObjectEffects(player).Compile());
                    break;

                //Movement
                case 0x14:
                    player.Face = data[1];
                    World.SendToAll(new QueDele(player.m_Serial, player.m_Channel, player.Map, new ChangeFace(player.m_Serial, player.Face).Compile()));
                    break;
                case 0x15:
                    if (player.m_Buffs.Where(xe => xe.Value.BuffEffect.BuffType == script.spells.BuffCase.Freezing).Any())
                    {
                        player.client.SendPacket(new MoveSpriteTele(player.m_Serial, player.Face, (short)player.Loc.X, (short)player.Loc.Y).Compile());
                        return;
                    }
                    p = new PacketReader(data, data.Count(), true);
                    player.Face = p.ReadInt16();
                    
                    short xx = p.ReadInt16();
                    short yy = p.ReadInt16();
                    World.SendToRange(new QueDele(player.m_Serial, player.m_Channel, player.Map, new MoveSpriteWalkChar(player.m_Serial, player.Face, (short)xx, (short)yy).Compile()), player);
                    parseFace(xx, yy, player.Face);



                    if (Settings.VillageName.Contains(player.Map.Split(',')[0]) && (player.Loc.X < 1 || player.Loc.X > 198 || player.Loc.Y < 1 || player.Loc.Y > 198))
                    {
                        int index = Convert.ToInt32(player.Map.Split(',')[1]);
                        string tempMap = Settings.VillageName.ElementAt(player.m_Channel) + ",";
                        Point2D tempLoc = null;
                        if (player.Loc.X < 1)
                        {
                            tempLoc = new Point2D(198, player.Loc.Y);
                            tempMap += (index - 1);
                        }
                        else if (player.Loc.X > 198)
                        {
                            tempLoc = new Point2D(1, player.Loc.Y);
                            tempMap += (index + 1);
                        }
                        else if (player.Loc.Y < 1)
                        {
                            tempLoc = new Point2D(player.Loc.X, 198);
                            tempMap += (index - 10);
                        }
                        else if (player.Loc.Y > 198)
                        {
                            tempLoc = new Point2D(player.Loc.X, 1);
                            tempMap += (index + 10);
                        }
                        if (LKCamelot.model.Map.FullMaps[player.m_Channel].ContainsKey(tempMap))
                        {
                            player.m_Loc = tempLoc;
                            player.Map = tempMap;
                        }
                    }
                    break;
                #endregion

                #region 스텟 추가
                //Stats
                case 0x2C:
                    player.AddStat(ref player.m_Str);
                    SendPacket(new UpdateCharStats(player).Compile());
                    break;
                case 0x2D:
                    player.AddStat(ref player.m_Men);
                    SendPacket(new UpdateCharStats(player).Compile());
                    break;
                case 0x2E:
                    player.AddStat(ref player.m_Dex);
                    SendPacket(new UpdateCharStats(player).Compile());
                    break;
                case 0x2F:
                    player.AddStat(ref player.m_Vit);
                    SendPacket(new UpdateCharStats(player).Compile());
                    break;
                #endregion

                #region 로그인 로그아웃 비번 변경
                case 0x01: //Logout
                    World.SendToAll(new QueDele(player.m_Serial, player.m_Channel, player.Map, new DeleteObject(player.m_Serial).Compile()));
                    player.loggedIn = false;
                    player.SoundFlag = false;
                    player = null;
                    Loaded = false;
                    break;

                case 0x03: //Login
                    var result = TryLogin(data);

                    io.IOClient.WriteLoginLog(string.Format("{0}: {1},{2},{3}", DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]"), result[0], result.Remove(0,1).Split(',')[0], connection.Socket.RemoteEndPoint.ToString().Split(':')[0] ));

                    switch (result[0])
                    {
                        case '1':
                            var puser2 = result.Remove(0, 1).Split(',');
                            player = handler.GetPlayer(puser2[0]);
                            player.client = this;
                            player.loggedIn = true;
                            LoadPlayer();
                            SendPacket(new CloseLogin().Compile());
                            break;
                        case '2':
                            SendPacket(new WrongPass().Compile());
                            break;
                        case '3':
                            SendPacket(new WrongID().Compile());
                            break;
                        case '4': //invalid chars
                            SendPacket(new WrongID().Compile());
                            break;
                        case '5':
                            SendPacket(new AlreadyOnline().Compile());
                            break;
                        case '6':
                            string PName = result.Remove(0, 1);
                            string requestedName = PName.Split(',')[0];
                            // if (requestedName.Contains("GM") || Settings.GMS.Contains(requestedName))// 운영자 케릭터 생성 금지
                            if (requestedName.Contains("GM"))
                            {
                                SendPacket(new WrongID().Compile());
                                return;
                            }

                            player = new Player(this, PName);
                            firstlogin = true;
                            LoadPlayer();
                            player.Pass = Cryption.CreateSaltedSHA256(player.Pass, player.Name);
                            try
                            {
                                handler.add[player.m_Channel].TryAdd(player.Name, player);
                                var touch = handler.add[player.m_Channel].Where(xe => xe.Key == player.Name).FirstOrDefault();
                                touch.Value.client = this;
                                touch.Value.loggedIn = true;
                                //   BinaryIO.SavePlayer(player);
                                // World.DBConnect.InsertPlayer(player);
                            }
                            catch
                            {
                                Console.WriteLine("failed to insert new player");
                            }
                            SendPacket(new CloseLogin().Compile());
                            break;
                    }
                    break;

                case 0x06:
                    var results = TryPass(data);
                    var info = results.Remove(0, 1).Split(',');
                    WritePassLog(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + " " + results[0] + " " + info[0] + " " + connection.Socket.RemoteEndPoint.ToString().Split(':')[0]);
                    switch (results[0])
                    {
                        case '2':
                            SendPacket(new WrongPass().Compile());
                            break;
                        case '4':
                            SendPacket(new WrongID().Compile());
                            break;
                        case '5':
                            SendPacket(new AlreadyOnline().Compile());
                            break;
                        case '9':
                            var pl = handler.GetPlayer(info[0]);
                            pl.Pass = Cryption.CreateSaltedSHA256(info[1], info[0]);
                            break;
                    }
                    break;

                #endregion

                case 0x49: //delete magic
                    var magdelslot = data[1];
                    player.DeleteMagic(magdelslot);
                    break;

                case 0x26: //swap magic
                    if (data.Count() < 4)
                        player.SwapMagic(data[1], 0);
                    else
                        player.SwapMagic(data[1], data[3]);
                    break;

                case 0xff:
                    //SendPacket(new PlayMusic(player.SoundTrack).Compile());
                    if (true) //Login
                    {
                        //         SendPacket(new CloseLogin().Compile());

                        //           SendPacket(new LoadWorld(1, 0x12, 0x12, new byte[] { 00, 04, 00, 00, 00, 00, 00, 00, 00, 03 },
                        //               01, 01, 00, 03, 04, 00, 00,
                        //               new byte[] { 0x69, 0x70, 0x69, 0x67, 0x6d, 0x79, 0x31, 0x2e, 0x6d, 0x61, 0x70, 00 }).Format());
                        break;
                    }
            }
        }

        static object WriteLock = new object();
        static object WriteDeleteLock = new object();
        static object WriteLoginLock = new object();
        public static void WriteBug(string bug)
        {
            lock (WriteLock)
            {
                System.IO.StreamWriter sr = new System.IO.StreamWriter("bugs.txt", true);
                sr.WriteLine(bug);
                sr.Close(); sr.Dispose();
            }        
        }

        #region Log
        public static void WriteDeleteGroundItem(string bug)
        {
            string path = Settings.LogPath + DateTime.Now.ToString("yyyyMMdd") + "\\";
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            lock (WriteDeleteLock)
            {
                using (System.IO.StreamWriter sr = new System.IO.StreamWriter(path + DateTime.Now.ToString("yyyyMMdd") + "_Log_DeleteGroundItem.txt", true))
                {
                    sr.WriteLine(bug);
                    sr.Close();
                    sr.Dispose();
                }
            }
        }
        public static void WriteUpgradeItem(string bug)
        {
            lock (WriteDeleteLock)
            {
                string path = Settings.LogPath + DateTime.Now.ToString("yyyyMMdd") + "\\";
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                using (System.IO.StreamWriter sr = new System.IO.StreamWriter(path + DateTime.Now.ToString("yyyyMMdd") + "_Log_UpgradeItem.txt", true))
                {
                    sr.WriteLine(bug);
                    sr.Close();
                    sr.Dispose();
                }
            }
        }
        public static void WriteLoginLog(string bug)
        {
            lock (WriteLoginLock)
            {
                string path = Settings.LogPath + DateTime.Now.ToString("yyyyMMdd") + "\\";
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                System.IO.StreamWriter sr = new System.IO.StreamWriter(path + DateTime.Now.ToString("yyyyMMdd") + "_Log_Login.txt", true);
                sr.WriteLine(bug);
                sr.Close(); sr.Dispose();
            }
        }
        public static void WritePassLog(string bug)
        {
            lock (WriteLock)
            {
                string path = Settings.LogPath + DateTime.Now.ToString("yyyyMMdd") + "\\";
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                System.IO.StreamWriter sr = new System.IO.StreamWriter(path + DateTime.Now.ToString("yyyyMMdd") + "_Log_ChangePass.txt", true);
                sr.WriteLine(bug);
                sr.Close(); sr.Dispose();
            }
        }
        public static void WriteTurnLog(string bug)
        {
            lock (WriteLock)
            {
                string path = Settings.LogPath + DateTime.Now.ToString("yyyyMMdd") + "\\";
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                System.IO.StreamWriter sr = new System.IO.StreamWriter(path + DateTime.Now.ToString("yyyyMMdd") + "_Log_Turn.txt", true);
                sr.WriteLine(bug);
                sr.Close(); sr.Dispose();
            }
        }
        public static void WriteGMCreateItemLog(string bug)
        {
            lock (WriteLock)
            {
                string path = Settings.LogPath + DateTime.Now.ToString("yyyyMMdd") + "\\";
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                System.IO.StreamWriter sr = new System.IO.StreamWriter(path + DateTime.Now.ToString("yyyyMMdd") + "_Log_CreateItem.txt", true);
                sr.WriteLine(bug);
                sr.Close(); sr.Dispose();
            }
        }
        #endregion

        public string TryPass(byte[] data)
        {
            byte[] user = new byte[10];
            byte[] pass = new byte[10];
            byte[] newpass = new byte[10];

            Buffer.BlockCopy(data, 1, user, 0, 10);
            Buffer.BlockCopy(data, 33, pass, 0, 10);
            Buffer.BlockCopy(data, 65, newpass, 0, 10);

            int usercnt = 0, passcnt = 0, newpasscnt = 0;
            for (int x = 0; x < 10; x++)
            {
                if (user[x] == 00)
                    break;
                usercnt++;
            }
            for (int x = 0; x < 10; x++)
            {
                if (pass[x] == 00)
                    break;
                if ((int)pass[x] > 127)
                    return "4";
                passcnt++;
            }
            for (int x = 0; x < 10; x++)
            {
                if (newpass[x] == 00)
                    break;
                if ((int)newpass[x] > 127)
                    return "4";
                newpasscnt++;
            }
            string test = Encoding.Default.GetString(user, 0, usercnt).ToUpper().Trim();
            var test2 = Encoding.Default.GetString(pass, 0, passcnt);
            var test3 = Encoding.Default.GetString(newpass, 0, newpasscnt);


            //SHA1

            if (!System.Text.RegularExpressions.Regex.IsMatch(test, @"^[a-zA-Z0-9가-힣]+$"))
                return "4";

            if (test2.Count() < 3 || test2.Count() < 5 || test3.Count() < 3 || test3.Count() < 5)
                if (!(Encoding.Default.GetByteCount(test) < 4))
                    return "4";

            if (handler.HasPlayer(test))
                return "5";

#if NOSQL
            return "6" + test + "," + test2;
#endif

            //  var info = BinaryIO.LoadName(test);
            var infos = handler.GetPlayer(test);
            if (infos == null)
                return "4";

            List<string> info = new List<string>();
            info.Add(infos.Name); info.Add(infos.Pass);
            if (info != null && info[0] == test)
            {
                if (info[1].Count() <= 10)
                {
                    if (info[1] != test2)
                        return "2";
                }
                else
                {
                    if (!Cryption.CheckHashPass(info[1], test, test2))
                        return "2";
                }

                return "9" + test + "," + test3;
            }
            else
            {

                return "4";
            }
        }
        public string TryLogin(byte[] data)
        {
            byte[] user = new byte[10];
            byte[] pass = new byte[10];

            Buffer.BlockCopy(data, 1, user, 0, 10);
            Buffer.BlockCopy(data, 33, pass, 0, 10);

            int usercnt = 0, passcnt = 0;
            for (int x = 0; x < 10; x++)
            {
                if (user[x] == 00)
                    break;
                usercnt++;
            }
            for (int x = 0; x < 10; x++)
            {
                if (pass[x] == 00)
                    break;
                if ((int)pass[x] > 127)
                    return "4";
                passcnt++;
            }

            string test = Encoding.Default.GetString(user, 0, usercnt).ToUpper().Trim();
            var test2 = Encoding.Default.GetString(pass, 0, passcnt);

            //SHA1
            if (!System.Text.RegularExpressions.Regex.IsMatch(test, @"^[a-zA-Z0-9가-힣]+$"))
                return "4";

            if ((Encoding.Default.GetByteCount(test) < 3) || test2.Count() < 5)
                    return "4";

            if (handler.HasPlayer(test))
                return "5";

#if NOSQL
            return "6" + test + "," + test2;
#endif

            if (!Settings.OverlapLogin)
            {
                if (PlayerHandler.getSingleton().Total.Where(xe => xe.Value.loggedIn && xe.Value.IP == connection.Socket.RemoteEndPoint.ToString().Split(':')[0]).ToList().Count > 0)
                    return "5" + test + "," + test2;
            }
            //  var info = BinaryIO.LoadName(test);
            var infos = handler.GetPlayer(test);
            if (infos == null)
                return "6" + test + "," + test2;

            List<string> info = new List<string>();
            info.Add(infos.Name); info.Add(infos.Pass);
            if (info != null && info[0] == test)
            {
                if (info[1].Count() <= 10)
                {
                    if (info[1] != test2)
                        return "2";
                }
                else
                {
                    if (!Cryption.CheckHashPass(info[1], test, test2))
                        return "2";
                }

                return "1" + test + "," + test2;
            }
            else
            {

                return "6" + test + "," + test2;
            }
        }
    }
}
