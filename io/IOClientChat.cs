 #define PROMOCAP12

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.net;
using LKCamelot.util;
using LKCamelot.model;
using LKCamelot.script;

using MySql.Data.MySqlClient;

using System.Diagnostics;
using System.Threading;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace LKCamelot.io
{
    public partial class IOClient
    {
        public void UpdateChat(string text, byte c1 = 0xff, byte c2 = 0xff, byte cb = 1)
        {
            SendPacket(new UpdateChatBox(c1, c2, 1, text).Compile());
        }

        private static List<Player> RankSort(IEnumerable<Player> players)
        {
            return players
                .OrderByDescending(xe => xe.TotalBounsStat)
                .ThenByDescending(xe => xe.Level)
                .ThenByDescending(xe => xe.XP)
                .ThenBy(xe => xe.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }


        public void HandleChat(string[] str2, string str)
        {
            if (Settings.GMS.Contains(player.Name) && GMCommands.Contains(str2[0]) | str[0] == '~' | str[0] =='`')
                HandleGMChat(str2, str);
            else if (str[0] == '@' || str[0] == '!' || str[0] == '/' || str[0] == '$')
                HandleCommands(str2, str);
            else
            {
                var Fliter = Settings.ChatFilter.Where(xe => Regex.IsMatch(str, xe.Key)).ToList();
                if (Fliter != null)
                {
                    foreach (var temp in Fliter)
                    {
                        Match match = Regex.Match(str, temp.Key);
                        str = str.Replace(match.Value, temp.Value);
                    }

                    str2 = str.Split(' ');
                }

                player.Event_Chat(str);
                EventCommands(str2, str);
            }
        }


        public static List<string> GMCommands = new List<string>()
        {
            "#도움말",
            "#길드해체",
            "#공지",
            "#정보",
            "#얼음",
            "#돈",
            "#경험치배율",
            "#골드배율",
            "#드랍배율",
            "#아발론배율",
            "#소환",
            "#출두",

            "#접속알림",
            "#피케이알림",
            "#뻐꾸기",
            "#배율",
            "#초기화",
            "#시리얼변경",
            "#저장",
            "#길드저장",
            "#아이템저장",
            "#플레이어저장",
            "#패스워드",
            "#맵정보",
            "#이웃",
            "#이웃벤",
            "#이웃레벨",
            "#몹소환",
            "#은신",
            "#강퇴",
            "#아이템",
            "#몹디비갱신",
            "#맵디비갱신",
            "#벤아이피",

            "#광부",
            "#대장장이",
        };



        public void HandleGMChat(string[] str2, string str)
        {
            if (!Settings.GMS.Contains(player.Name))
                return;

            if (str2[0][0] == '~')
            {
                var message = str.Remove(0, 1);
                message = "[" + player.Name + "] : " + message;

                World.SendToAll(new QueDele("all", new UpdateChatBox(0xff, 0x7e, 15461, message).Compile()));
            }
            else if (str2[0][0] == '`')
            {
                var message = str.Remove(0, 1);
                message = "[시스템] " + message;
                World.SendToAll(new QueDele("all", new UpdateChatBox(0x80, 0x03, 384, message).Compile()));

            }
            else
            {
                switch (str2[0])
                {
                    case "#도움말":
                        GMHelp();           break;
                    case "#공지":
                        GMNotice(str2);     break;
                    case "#정보":
                        GMInfo(str2);       break;
                    case "#얼음":
                        GMFreeze();         break;
                    case "#돈":
                        GMGold(str2);       break;
                    case "#경험치배율":
                        GMEXPRate(str2);    break;
                    case "#드랍배율":
                        GMDropRate(str2);   break;
                    case "#골드배율":
                        GMGoldRate(str2);   break;
                    case "#아발론배율":
                        GMAbalRate(str2);   break;
                    case "#시리얼변경":
                        GMSerialChange(str2);   break;
                    case "#출두":
                        GMGO(str2);         break;
                    case "#소환":
                        GMRecall(str2);     break;
                    case "#초기화": 
                        GMReset();          break;
                    case "#저장":
                        GMSave();           break;
                    case "#길드저장":
                        GMGuildSave();      break;
                    case "#길드해체":
                        GMGuildDelete(str2); break;
                    case "#광부":
                        GMSubJobLevel(str2, true);  break;
                    case "#대장장이":
                        GMSubJobLevel(str2, false); break;
                    case "#아이템저장":
                        GMItemSave();       break;
                    case "#플레이어저장":
                        GMPlayerSave();     break;
                    case "#맵정보":
                        GMMapInfo(str2);    break;
                    case "#이웃":
                        GMNeighbor(str2);   break;
                    case "#이웃벤":
                        GMBenNeighbor(str2);   break;
                    case"#이웃레벨":
                        GMNeighborLevel(str2);  break;
                    case "#패스워드":
                        GMPass(str2);           break;
                    case "#몹소환":
                        GMMob(str2);            break;
                    case "#은신":
                        GMTransparancy();       break;
                    case "#강퇴":
                        GMKick(str2);           break;
                    case "#아이템":
                        GMCreateItem(str2);     break;
                    case "#벤아이피":
                        GMBanIP(str2); break;
                    //알림
                    case "#피케이알림":
                        GMPKPKAlarm();          break;
                    case "#접속알림":
                        GMLoginAlarm();         break;
                    case "#배율":
                        GMRate();               break;
                    case "#뻐꾸기":
                        GMClockAlarm();         break;
                    case "#몹디비갱신":
                        GMrefreshMOB();         break;
                    case "#맵디비갱신":
                        GMfefreshMap();         break;
                    default:
                        break;
                }
            }

        }

        private void GMfefreshMap()
        {
            model.Map.FullMapsLoad();
            script.map.Portal.PortalLoad();

            player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, "맵 디비가 갱신되었습니다").Compile());
        }

        private void GMrefreshMOB()
        {
            LKCamelot.GameData.getInstance().ResetMonster(); 
            player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, "몹 디비가 갱신되었습니다").Compile());
        }
        private void GMHelp()
        {
            UpdateChat("~할말, `할말, #공지, #정보, #돈, #소환, #출두");
            UpdateChat("#경험치배율, #골드배율, #배율, #초기화, #얼음");
            UpdateChat("#저장, #접속알림, #피케이알림, #맵정보, #강퇴");
            UpdateChat("#이웃레벨, #이웃, #이웃벤, #뻐꾸기, #몹소환, #은신");
            UpdateChat("#몹디비갱신, #벤아이피, #길드해체");
            UpdateChat("#광부 [레벨], #대장장이 [레벨]");
        }

        #region GM Commands
        private void GMNotice(string[] str2)
        {
            string str = string.Empty;
            foreach (string s in str2)
                str += s + " ";
            Settings.Notice = "<공지> : " + str.Substring(3);
        }
        private void GMBanIP(string[] str2)
        {
            if (str2.Count() <= 1)
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "#벤아이피 ??.??.??.??").Compile());
            else if(str2.Count() == 2)
            {
                string command = "INSERT INTO badip(IP) VALUES('" + str2[1] + "')";

                string strConn = Settings.DB_Conn;
                MySqlConnection con = new MySqlConnection(strConn);
                con.Open();                                            // 디비 오픈
                MySqlCommand com = new MySqlCommand(command, con);     // 쿼리 생성
                com.ExecuteNonQuery();                                 // 쿼리 날리기
                con.Close();                                           // 디비 닫기

                string result = str2[1] + " BadIP 등록 완료";
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, result).Compile());
            }
          
        }
        private void GMInfo(string[] str2)
        {
            if (str2.Count() < 2)
                return;
            var User = PlayerHandler.getSingleton().GetPlayer(str2[1]);

            if (User == null)
                return;
            else
            {
                string str;
                str = string.Format("이름:{0}, 직업:{1}({2}), 길드:{3}", User.Name, User.FullClass, User.Class, (User.Guild==null?"":User.Guild.Name));
                player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, str).Compile());

                str = string.Format("레벨:{0}-{1}({2}), 광부렙:{3}, 대장렙:{4}", User.Promo, User.PromoLevel, User.Level, User.MinerLevel, User.BSLevel);
                player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, str).Compile());

                str = string.Format("마을:{0} 맵:{1}({2}:{3}), PK:{4}", Settings.VillageName.ElementAt(User.m_Channel), User.Map, User.Loc.X, User.Loc.Y, User.PKCount);
                player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, str).Compile());
                
                str = string.Format("돈:{0:#,###}, 보관돈:{1:#,###}, 로그인:{2}", User.Gold, User.BankGold, User.loggedIn);
                player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, str).Compile());

                str = string.Format("힘:{0}, 지력:{1}, 숙련:{2}, 생명:{3} 추가:{4}", User.GetStat("str"), User.GetStat("men"), User.GetStat("dex"), User.GetStat("vit"), User.m_Extra);
                player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, str).Compile());

                str = string.Format("턴힘:{0}, 턴지력:{1}, 턴숙련:{2}, 턴생명:{3}", User.b_Str, User.b_Men, User.b_Dex, User.b_Vit);
                player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, str).Compile());

                str = string.Format("AC:{0}, Hit:{1}, Dam:{2}", User.AC, User.Hit, User.Dam);
                player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, str).Compile());

                str = string.Format("HP:{0:#,###}/{1:#,###}, MP:{2:#,###}/{3:#,###}", User.HPCur, User.HP, User.MPCur, User.MP);
                player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, str).Compile());
                    
                str = string.Format("IP:{0}, 이웃:{1}", User.IP, User.neighbor);
                player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, str).Compile());

                str = string.Format("생성일자:{0}", User.CreateTime);
                player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, str).Compile());
                str = string.Format("접속일자:{0}", User.LastAccessTime);
                player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, str).Compile());
            }
        }
        private void GMFreeze()
        {
            Settings.ChatBlock = !Settings.ChatBlock;
            string message = "";
            message = "";
            World.SendToAll(new QueDele("all", new UpdateChatBox(0xff, 0x7e, 15461, message).Compile()));

            message = "";
            //World.SendToAll(new QueDele(player.Serial, "all", new UpdateChatBox(0xa0, 0x7E, 28128, message).Compile()));
            World.SendToAll(new QueDele("all", new UpdateChatBox(0xa0, 0x7E, 32192, message).Compile()));

            message = "                  운영자님이 외치기를 " + (Settings.ChatBlock ? "얼렸습니다." : "녹였습니다.");
            World.SendToAll(new QueDele("all", new UpdateChatBox(0xf0, 0x7b, 27296, message).Compile()));

            message = "";
            World.SendToAll(new QueDele("all", new UpdateChatBox(0x80, 0x03, 384, message).Compile()));

            message = "";
            World.SendToAll(new QueDele("all", new UpdateChatBox(0x92, 0x29, 3244, message).Compile()));

            message = "";
            World.SendToAll(new QueDele("all", new UpdateChatBox(0x57, 0x3D, 10348, message).Compile()));
        }
        private void GMGold(string[] str2)
        {
            if (str2.Count() < 2)
            {
            }
            else
            {
                this.player.Gold = Convert.ToUInt32(str2[1]);
            }
        }
        private void GMSerialChange(string[] str2)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(str2[1], @"^[0-9]+$"))
                return;
            // ID changeSerial
            if (str2.Count() <= 2)
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "#경험치배율 [수치:기본1.0] [시간(시간:분)]").Compile());
            else
            {
                Player p = PlayerHandler.getSingleton().GetPlayer(str2[1]);

                if (p == null)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "사용자를 찾을 수 없습니다.").Compile());
                else
                {
                    int Serial = 0;
                    try
                    {
                        Serial = Convert.ToInt32(str2[2]);
                    }
                    catch
                    {
                        return;
                    }
                    p.m_Serial = Serial;
                }
            }            
        }
        private void GMCreateItem(string[] str2)
        {
            if (str2.Count() == 1)
            {
                string message = "$아이템 [아이템이름] [업글수치]";
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, message).Compile());
                message = "$아이템 [ItemName] [EnLevel] [Count]";
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, message).Compile());
                message = "$아이템 [유저ID] [ItemName] [EnLevel] [Count]";
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, message).Compile());
            }
            script.item.Item temp;
            try
            {
                if (str2.Count() == 2)
                {
                    temp = (script.item.Item)Activator.CreateInstance(Type.GetType("LKCamelot.script.item." + str2[1]));
                }
                else if (str2.Count() == 4)
                {
                    temp = (script.item.Item)Activator.CreateInstance(Type.GetType("LKCamelot.script.item." + str2[1]));
                    temp.Stage = Convert.ToInt32(str2[2]);
                    temp.Quantity = Convert.ToInt32(str2[3]);
                }
                else if (str2.Count() == 5)
                {
                    Player targetplayer = PlayerHandler.getSingleton().GetPlayer(str2[1]);
                    Player tar;
                    tar = targetplayer;
                    temp = (script.item.Item)Activator.CreateInstance(Type.GetType("LKCamelot.script.item." + str2[2]));
                    temp.Stage = Convert.ToInt32(str2[3]);
                    temp.Quantity = Convert.ToInt32(str2[4]);

                    var newitems = temp.Inventory(tar);
                    World.NewItems.TryAdd(newitems.m_Serial, newitems);
                    tar.client.SendPacket(new AddItemToInventory2(newitems).Compile());
                    return;
                }
                else
                {
                    temp = (script.item.Item)Activator.CreateInstance(Type.GetType("LKCamelot.script.item." + str2[1]));
                    temp.Stage = Convert.ToInt32(str2[2]);
                }
            }
            catch { return; }
            var newitem = temp.Inventory(player);
            World.NewItems.TryAdd(newitem.m_Serial, newitem);
            SendPacket(new AddItemToInventory2(newitem).Compile());
        }
        private bool TryParseRateEvent(string[] args, string command, float maxRate, out float rate, out long duration)
        {
            rate = 0;
            duration = 0;
            string usage = command + " [배율: 0.1~" + maxRate.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + "] [시간(분 또는 시:분)]";

            if (args == null || args.Length != 3)
            {
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, usage).Compile());
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "예: " + command + " 1.5 24:00").Compile());
                return false;
            }

            double parsedRate;
            if (!System.Text.RegularExpressions.Regex.IsMatch(args[1], @"^[0-9]+(\.[0-9]+)?$")
                || !double.TryParse(args[1], System.Globalization.NumberStyles.AllowDecimalPoint,
                    System.Globalization.CultureInfo.InvariantCulture, out parsedRate)
                || double.IsNaN(parsedRate) || double.IsInfinity(parsedRate)
                || parsedRate < 0.1 || parsedRate > maxRate)
            {
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "배율은 0.1~" + maxRate.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " 사이 숫자로 입력하세요.").Compile());
                return false;
            }

            long totalMinutes;
            string[] timeParts = args[2].Split(':');
            long firstPart;
            if (timeParts.Length == 1)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(timeParts[0], @"^[0-9]+$")
                    || !long.TryParse(timeParts[0], System.Globalization.NumberStyles.None,
                        System.Globalization.CultureInfo.InvariantCulture, out totalMinutes))
                {
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "시간은 분 또는 시:분 형식으로 입력하세요.").Compile());
                    return false;
                }
            }
            else if (timeParts.Length == 2)
            {
                long minutes;
                if (!System.Text.RegularExpressions.Regex.IsMatch(timeParts[0], @"^[0-9]+$")
                    || !System.Text.RegularExpressions.Regex.IsMatch(timeParts[1], @"^[0-9]+$")
                    || !long.TryParse(timeParts[0], System.Globalization.NumberStyles.None,
                        System.Globalization.CultureInfo.InvariantCulture, out firstPart)
                    || !long.TryParse(timeParts[1], System.Globalization.NumberStyles.None,
                        System.Globalization.CultureInfo.InvariantCulture, out minutes)
                    || firstPart > 365L * 24 || minutes > 59)
                {
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "시간은 분 또는 시:분 형식으로 입력하세요. 분은 0~59입니다.").Compile());
                    return false;
                }
                totalMinutes = firstPart * 60 + minutes;
            }
            else
            {
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "시간은 분 또는 시:분 형식으로 입력하세요.").Compile());
                return false;
            }

            if (totalMinutes <= 0 || totalMinutes > 365L * 24 * 60)
            {
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "이벤트 시간은 1분부터 365일 사이로 입력하세요.").Compile());
                return false;
            }

            rate = (float)parsedRate;
            duration = totalMinutes * Settings.Minute;
            return true;
        }

        private void SetRateEvent(float[] setting, float rate, long duration, string eventName)
        {
            setting[0] = rate;
            setting[1] = Server.tickcount.ElapsedMilliseconds + duration;
            World.SendToAll(new QueDele("all", new UpdateChatBox(0xff, 0x7e, 15461,
                string.Format("{0} 이벤트(x{1:0.##}배)가 {2}분 적용되었습니다.", eventName, rate, duration / Settings.Minute)).Compile()));
        }

        private void GMEXPRate(string[] str2)
        {
            float rate;
            long duration;
            if (TryParseRateEvent(str2, "#경험치배율", 1000.0f, out rate, out duration))
                SetRateEvent(Settings.XPrate, rate, duration, "경험치");
        }

        private void GMDropRate(string[] str2)
        {
            float rate;
            long duration;
            if (TryParseRateEvent(str2, "#드랍배율", 1000.0f, out rate, out duration))
                SetRateEvent(Settings.DropRate, rate, duration, "드랍");
        }

        private void GMGoldRate(string[] str2)
        {
            float rate;
            long duration;
            if (TryParseRateEvent(str2, "#골드배율", 1000.0f, out rate, out duration))
                SetRateEvent(Settings.GOLDRate, rate, duration, "골드");
        }

        private void GMAbalRate(string[] str2)
        {
            float rate;
            long duration;
            if (TryParseRateEvent(str2, "#아발론배율", 50.0f, out rate, out duration))
                SetRateEvent(Settings.ABALRate, rate, duration, "아발론");
        }
        private void GMGO(string[] str2)
        {
            if (str2.Count() != 2)
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("#출두 [아이디]")).Compile());
            else
            {
                var go = World.w_server.OnlineConnections.Where(xe => xe.Client != null && xe.Client.player != null && xe.Client.player.loggedIn == true && xe.Client.player.Name == str2[1].ToUpper()).FirstOrDefault().Client.player;

                if (go.loggedIn)
                {
                    Player temp;
                    PlayerHandler.getSingleton().add[player.m_Channel].TryRemove(player.Name, out temp);
                    PlayerHandler.getSingleton().add[go.m_Channel].TryAdd(player.Name, player);

                    player.m_Channel = go.m_Channel;
                    player.Loc = new Point2D(go.Loc.X, go.Loc.Y);
                    player.Map = go.Map;
                }
                else
                {
                    SendPacket(new UpdateChatBox(0xf0, 0x7b, 27296, string.Format("접속중이 아닙니다.")).Compile());
                }
            }
        
        }
        private void GMRecall(string[] str2)
        {
            if (str2.Count() != 2)
                SendPacket(new UpdateChatBox(0xf0, 0x7b, 27296, string.Format("#소환 [아이디]")).Compile());
            else
            {
                var go = World.w_server.OnlineConnections.Where(xe => xe.Client != null && xe.Client.player != null && xe.Client.player.loggedIn == true && xe.Client.player.Name == str2[1].ToUpper()).FirstOrDefault().Client.player;
                if (go != null)
                {
                    Player temp;
                    PlayerHandler.getSingleton().add[go.m_Channel].TryRemove(go.Name, out temp);
                    PlayerHandler.getSingleton().add[player.m_Channel].TryAdd(go.Name, go);

                    go.m_Channel = player.m_Channel;
                    go.m_Loc = new Point2D(player.Loc.X, player.Loc.Y);
                    go.Map = player.Map;
                }
                else
                    SendPacket(new UpdateChatBox(0xf0, 0x7b, 27296, string.Format("접속중이 아닙니다.")).Compile());
            }
        }

        private void GMPKPKAlarm()
        {
            Settings.PKAlarm = !Settings.PKAlarm;
            SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("피케이 알림 메세지 설정 : {0}", Settings.PKAlarm)).Compile());
        }
        private void GMClockAlarm()
        {
            Settings.TimeAlarm = !Settings.TimeAlarm;
            SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("뻐꾸기 알림 메세지 설정 : {0}", Settings.TimeAlarm)).Compile());
        }
        private void GMLoginAlarm()
        {
            Settings.LoginAlarm = !Settings.LoginAlarm;
            SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("로그인 알림 메세지 설정 : {0}", Settings.LoginAlarm)).Compile());

        }
        private void GMRate()
        {
            long now = Server.tickcount.ElapsedMilliseconds;
            long goldMinutes = RemainingEventMinutes(Settings.GOLDRate[1], now);
            long xpMinutes = RemainingEventMinutes(Settings.XPrate[1], now);
            long dropMinutes = RemainingEventMinutes(Settings.DropRate[1], now);
            long abalMinutes = RemainingEventMinutes(Settings.ABALRate[1], now);
            SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format(
                "골드 x{0:0.##}({1}분), 경험치 x{2:0.##}({3}분), 드랍 x{4:0.##}({5}분), 아발론 x{6:0.##}({7}분)",
                Settings.GOLDRate[0], goldMinutes, Settings.XPrate[0], xpMinutes,
                Settings.DropRate[0], dropMinutes, Settings.ABALRate[0], abalMinutes)).Compile());
        }

        private static long RemainingEventMinutes(float expiresAt, long now)
        {
            if (expiresAt <= now)
                return 0;
            return (long)Math.Ceiling((expiresAt - now) / Settings.Minute);
        }
        private void GMReset()
        {
            foreach (var item in this.player.Inventory)
            {
                item.Delete(this.player);
            }
        }
        private void GMSave()
        {
            World.SaveAll();
            SendPacket(new UpdateChatBox(0x00, 0xff, 1, "저장되었습니다.").Compile());
        }
        private void GMPlayerSave()
        {
            var Playss = Server.playerHandler.Total.Where(xe => xe.Key != null && xe.Value != null).Select(xe => xe.Value).ToList();
            BinaryIO.SavePlayers(Playss);
            SendPacket(new UpdateChatBox(0x00, 0xff, 1, "플레이어가 저장되었습니다.").Compile());
        }
        private void GMItemSave()
        {
            var Itemss = World.NewItems.Values.Where(xe => xe.InvSlot >= 0).Select(xe => xe).ToList();
            BinaryIO.SaveItems(Itemss);
            SendPacket(new UpdateChatBox(0x00, 0xff, 1, "아이템이 저장되었습니다.").Compile());
        }
        private void GMGuildSave()
        {
            BinaryIO.SaveGuild(World.NewGuilds.Values.ToList());
            SendPacket(new UpdateChatBox(0x00, 0xff, 1, "길드가 저장되었습니다.").Compile());
        }

        /// <summary>
        /// GM이 길드를 강제로 해체한다. 실수 방지를 위해 @길드삭제처럼 길드명을 두 번 입력받는다.
        /// </summary>
        private void GMGuildDelete(string[] str2)
        {
            if (str2.Count() != 3)
            {
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "#길드해체 [길드명] [길드명]").Compile());
                return;
            }
            if (str2[1] != str2[2])
            {
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "길드명 두 개가 서로 다릅니다.").Compile());
                return;
            }

            // 길드 키는 생성 직후에는 입력한 그대로, 재시작 후에는 대문자라서 대소문자 구분 없이 찾는다.
            var entry = World.NewGuilds.Where(xe => string.Equals(xe.Key, str2[1], StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (entry.Value == null)
            {
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "해당 길드를 찾을 수 없습니다.").Compile());
                return;
            }

            BaseGuild guild = entry.Value;
            int memberCount = 0;
            foreach (var p in PlayerHandler.getSingleton().Total.Values.Where(xe => xe != null && xe.Guild == guild).ToList())
            {
                p.Guild = null;
                memberCount++;
                if (p.loggedIn && p.client != null)
                    p.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "운영자에 의해 길드가 해체되었습니다.").Compile());
            }

            BaseGuild removed;
            World.NewGuilds.TryRemove(entry.Key, out removed);

            // 길드맵 표지판이 있으면 함께 제거한다.
            if (guild.Board != null)
            {
                script.npc.BaseNPC board;
                World.NewNpcs[guild.Board.m_Channel].TryRemove(guild.Board.m_Serial, out board);
                World.SendToAll(new QueDele(guild.Board.m_Channel, guild.Board.m_Map, new DeleteObject(guild.Board.m_Serial).Compile()));
            }

            io.IOClient.WriteBug(string.Format("{0} [GuildDelete] GM={1} guild={2} members={3}",
                DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]"), player.Name, entry.Key, memberCount));
            SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}길드를 해체했습니다. (길드원 {1}명)", entry.Key, memberCount)).Compile());
        }

        /// <summary>
        /// 서브직업(광부/대장장이) 레벨을 지정한 값으로 맞춘다.
        /// #광부 [레벨] / #대장장이 [레벨]. 어뷰징 방지를 위해 명령을 친 GM 본인에게만 적용한다(대상 지정 없음).
        /// </summary>
        private void GMSubJobLevel(string[] str2, bool miner)
        {
            const int MaxSubJobLevel = 99;
            string jobName = miner ? "광부" : "대장장이";

            int level;
            if (str2.Count() != 2
                || !int.TryParse(str2[1], System.Globalization.NumberStyles.None, System.Globalization.CultureInfo.InvariantCulture, out level)
                || level < 1 || level > MaxSubJobLevel)
            {
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0} [레벨: 1~{1}]", str2[0], MaxSubJobLevel)).Compile());
                return;
            }

            // 경험치도 레벨에 맞춰 둔다. 레벨만 바꾸면 다음 경험치 획득 때 경험치 기준으로 레벨이 다시 내려간다.
            int xp = level > 1 ? library.NextLevelC.NextLevelTable[level - 1] + 1 : 0;
            if (miner)
            {
                player.MinerLevel = level;
                player._MinerXP = xp;
            }
            else
            {
                player.BSLevel = level;
                player._BSXP = xp;
            }

            SendPacket(new Attrib(player).Compile());

            io.IOClient.WriteBug(string.Format("{0} [SubJobLevel] GM={1} job={2} level={3}",
                DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]"), player.Name, jobName, level));
            SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0} 레벨을 {1}(으)로 설정했습니다.", jobName, level)).Compile());
        }

        private void GMMapInfo(string[] str2)
        {
            if (str2.Count() != 2)
            {
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "#맵정보 [맵이름]").Compile());
                return;
            }


            var Monster = World.NewMonsters[this.player.m_Channel].Where(xe => xe.Value.m_Map == str2[1]).Count();
            var Player = PlayerHandler.getSingleton().add[this.player.m_Channel].Where(xe => xe.Value.loggedIn && xe.Value.Map == str2[1]).Count();
            var Obj = World.NewItems.Where(xe => xe.Value.m_Map == str2[1]).Count();
            var NPC = World.NewNpcs[this.player.m_Channel].Where(xe => xe.Value.m_Map == str2[1]).Count();


            SendPacket(new UpdateChatBox(0x00, 0xff, 1, str2[1]).Compile());
            SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("몬스터,광석: {0}개", Monster)).Compile());
            SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("플레이어   : {0}개", Player)).Compile());
            SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("아이템     : {0}개", Obj)).Compile());
            SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("NPC,표지판 : {0}개", NPC)).Compile());
        }
        private void GMNeighbor (string[] str2)
        {
            if (str2.Count() == 1)
            {
                Settings.Neighbor = !Settings.Neighbor;
                if (Settings.Neighbor)
                    World.SendToAll(new QueDele("all", new UpdateChatBox(0x00, 0xff, 1, string.Format("이웃 나라 백성으로 접속 되었습니다.")).Compile()));
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("서버의 이웃마을 설정:{0}", Settings.Neighbor)).Compile());
            }
            else
            {
                var play = Server.playerHandler.GetPlayer(str2[1]);

                play.neighbor = !play.neighbor;

                if (play.neighbor == false)
                {
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("해당 아이디는 서버 이웃마을 ON일때")).Compile());
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("더 이상 이웃마을에 걸리지 않습니다.")).Compile());
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("해당 아이디를 이웃마을하려면, '#이웃벤' 명령어를 이용하세요")).Compile());
                }

                SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}님의 이웃마을 설정:{1}", str2[1], play.neighbor)).Compile());
            }
        }
        private void GMBenNeighbor(string[] str2)
        {
            if (str2.Count() == 1)
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("#이웃벤 [아이디]", Settings.Neighbor)).Compile());
            else
            {
                var play = Server.playerHandler.GetPlayer(str2[1]);

                play.BenNeighbor = !play.BenNeighbor;
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}님의 이웃마을 벤 설정:{1}", str2[1], play.BenNeighbor)).Compile());
            }
        }
        private void GMNeighborLevel(string[] str2)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(str2[1], @"^[0-9]+$") && str2.Count() != 2)
            {
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "#이웃레벨 [레벨]").Compile());
                return;
            }
            else
            {
                Settings.NeighborLevel = Convert.ToInt32(str2[1]);
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("이웃마을 레벨설정:{0}", Settings.NeighborLevel)).Compile());
            }
        }
        private void GMPass(string[] str2)
        {
            if (str2.Count() != 3)
                return;

            if (!System.Text.RegularExpressions.Regex.IsMatch(str2[2], @"^[0-9a-zA-Z]+$"))
                return;


            var whisp = handler.GetPlayer(str2[1]);
            if (whisp == null)
                return;

            whisp.Pass = Cryption.CreateSaltedSHA256(str2[2], whisp.Name);
            SendPacket(new UpdateChatBox(0x00, 0xff, 1, "변경되었습니다.").Compile());
        }
        private void GMMob(string[] str2)
        {
            if (str2.Count() != 4)
            {
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "#몹소환 [몹이름] [갯수] [스폰범위]").Compile());
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(str2[2], @"^[0-9]+$") ||
                !System.Text.RegularExpressions.Regex.IsMatch(str2[3], @"^[0-9]+$"))
                return;

            try
            {
                for (int i = 0; i < Convert.ToInt32(str2[2]); i++)
                {
                    Point2D tempmob = null;
                    while ((tempmob = Map.SpawnPointFixed(player.m_Channel, player.Map, player.Loc.X, player.Loc.Y, Convert.ToInt32(str2[3]) )) == null);
                    script.monster.Monster mob = (script.monster.Monster)Activator.CreateInstance(Type.GetType("LKCamelot.script.monster." + str2[1]), new object[] { (Serial)Spawnere.temps, this.player.m_Channel, tempmob.X, tempmob.Y, player.Map });
                    mob.m_Loc = tempmob;
                    mob.MobType |= script.monster.MobType.Event;
                    mob.Alive = true;
                    World.NewMonsters[this.player.m_Channel].TryAdd(Spawnere.temps++, mob);
                }
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "몹이 소환되었습니다..").Compile());
            }
            catch {
            }
        }
        private void GMTransparancy()
        {
            if (player.Transparancy == 0)
            {
                player.Transparancy = 100;
            }
            else player.Transparancy = 0;
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new SetObjectEffects(player).Compile()));
        }
        private void GMKick(string[] str2)
        {
            if (str2.Count() != 2)
            {
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "#강퇴 [아이디]").Compile());
                return;
            }

            var plr = handler.GetPlayer(str2[1]);
            if (plr.loggedIn)
            {
                plr.loggedIn = false;
                World.w_server.Disconnect(plr.client.connection);
            }
        }
        #endregion


        public void EventCommands(string[] str2, string str)
        {
            var npcs = World.NewNpcs[this.player.m_Channel].Where(xe => xe.Value.Alive == true && xe.Value.m_Map == player.Map).ToList();

            foreach (var npc in npcs)
            {
                if (World.Dist2d(npc.Value.m_Loc, player.Loc) <= npc.Value.EventRange)
                    npc.Value.Event_Message(player, str2, str);
            }
        }            
        public void HandleCommands(string[] str2, string str)
        {
            if (str2[0] == "@이동")
            {
                if (str2.Count() > 1)
                {
                    HandleGo(str2[1]);
                }
            }
            #region 외치기
            else if (str[0] == '!')
            {
                #region 길드 외치기
                if (str.Count() > 1 && str[1] == '!' && player.Guild != null)
                {
                    var message = str.Remove(0, 2);
                    message = "<<" + player.Name + ">> : " + message;


                    Server.mainForm.EnqueueGuildChat(string.Format("[{0}] {1}", player.Guild.Name, message));
                    var play = PlayerHandler.getSingleton().Total.Where(xe => xe.Value.loggedIn && player.Guild.Member.Keys.Where(ee => ee == xe.Value.m_Serial).Any()).ToList();
                    foreach (var p in play)
                    {
                        p.Value.client.SendPacket(new UpdateChatBox(0x94, 0x52, 1, message).Compile());
                    }
                }
                #endregion

                #region 일반 외치기
                else
                {
                    if (Settings.ChatBlock)
                    {
                        SendPacket(new UpdateChatBox(0xff, 0xff, 1, "외치기가 금지되어 있습니다.").Compile());
                        return;
                    }

                    var message = str.Remove(0, 1);
                    message = "[" + player.Name + "] : " + message;

                    //맵치기
                    if (player.Promo == 0)
                    {
                        Server.mainForm.EnqueueChat("[초록]" + message);
                        //World.SendToChatAll(new QueDele(player.m_Channel, player.Map, new UpdateChatBox(0x20, 0x02, 1, message).Compile()));
                        World.SendToChatAll(new QueDele("all", new UpdateChatBox(0x20, 0x02, 1, message).Compile()));
                    }
                    //흰치기
                    else if (player.Promo >= 1)
                    {
                        Server.mainForm.EnqueueChat("[흰색]" + message);
                        //World.SendToChatAll(new QueDele(player.m_Channel, "ch", new UpdateChatBox(0xB6, 0x5A, 9, message).Compile()));  // 흰색 외치기
                        World.SendToChatAll(new QueDele("all", new UpdateChatBox(0xB6, 0x5A, 9, message).Compile()));  // 흰색 외치기
                    }
                }
                #endregion
            }

            else if (str[0] == '$' && player.Promo >= 7)
            {
                if (Settings.ChatBlock)
                {
                    SendPacket(new UpdateChatBox(0xff, 0xff, 1, "외치기가 금지되어 있습니다.").Compile());
                    return;
                }

                var message = str.Remove(0, 1);
                message = "[" + player.Name + "] : " + message;

                var keys = handler.Total.Values.Where(xe => xe != null && !Settings.GMS.Contains(xe.Name.ToUpper()) && xe.loggedIn).ToList();
                int rankIndex = RankSort(keys).FindIndex(xe => xe.Name == player.Name);
                int rank = rankIndex < 0 ? int.MaxValue : rankIndex + 1;

                if (rank <= Settings.PinkGlobalChat)
                {
                    Server.mainForm.EnqueueChat("[분홍]" + message);
                    World.SendToChatAll(new QueDele("all", new UpdateChatBox(0x86, 0x61, 9, message).Compile()));  // 분홍색 외치기
                }
                else
                {
                    Server.mainForm.EnqueueChat("[똥색]" + message);
                    World.SendToChatAll(new QueDele("all", new UpdateChatBox(0x60, 0x52, 9, message).Compile()));
                }
            }
            #endregion

            else if (str2[0] == "@명령어")
            {
                UpdateChat("@이동 [장소], @대인공격, !할말, $할말, @귓속말, @속성");
                UpdateChat("@입금 [금액], @출금 [금액], @송금 [아이디] [금액]");
                UpdateChat("@잔액조회, @핑, @시간, @서열, @접속자, @자동, @초기화");
                UpdateChat("@피케이, @신고, @채팅, @턴속성, @시스템채팅");

                UpdateChat(", @bug");
            }
            else if (str2[0] == "@길드명령어")
            {
                UpdateChat("@길드생성, @길드삭제, @길드가입, @길드탈퇴, @길드정보");
                UpdateChat("@길드맵설정, @출두");
                UpdateChat("@길드직책명, @길드직책변경, @길드양도");
            }

            #region @핑, @시간
            else if (str2[0] == "@핑")
            {
                long totalTime = 0;
                int timeout = 60;
                System.Net.NetworkInformation.Ping pingSender = new Ping();

                for (int i = 0; i < 1; i++)
                {
                    PingReply reply = pingSender.Send(connection.RemoteEndPoint.Address, timeout);
                    if (reply.Status == IPStatus.Success)
                    {
                        totalTime += reply.RoundtripTime;
                    }
                }
                long res = totalTime / 1;
                string achat = res + " ms";
                SendPacket(new UpdateChatBox(0xff, 0xff, 1, achat).Compile());
            }

            else if (str2[0] == "@시간")
            {
                string achat = DateTime.Now.ToString("yyyy/MM/dd/ tt hh:mm:ss.fff");
                SendPacket(new UpdateChatBox(0xff, 0xff, 1, achat).Compile());
            }
            #endregion

            #region @피케이
            else if (str2[0] == "@피케이")
            {
                string temp = string.Format("피케이카운터:{0}, 피케이시간{1}분", player.PKCount, player.pkhistoryTime / Settings.Minute);
                SendPacket(new UpdateChatBox(0xff, 0xff, 1, temp).Compile());
            }
            #endregion

            #region @대인공격
            else if (str2[0] == "@대인공격")
            {
                player.PK = !player.PK;
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "대인공격 " + (player.PK ? "ON" : "OFF")).Compile());
            }
            #endregion

            #region @신고
            else if (str2[0] == "@신고")
            {
                if (str2.Count() != 2)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "@신고 [아이디]").Compile());

                else
                {
                    var p = this.player.PKhistoryPlayer.Where(xe => xe.Value.Name == str2[1].ToUpper()).FirstOrDefault();

                    if (p.Value != null)
                    {
                        Player play;
                        this.player.PKhistoryPlayer.TryRemove(p.Key, out play);
                        if (play != null)
                        {
                            p.Value.pkhistoryTime += Settings.PKDelay;
                            SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("신고되었습니다.")).Compile());
                        }
                        else
                            SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("대상을 찾을 수 없습니다.")).Compile());
                    }
                    else
                        SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("대상을 찾을 수 없습니다.")).Compile());
                }
            }
            #endregion


            #region @귓속말
            else if (str2[0] == "@귓속말")
            {
                string Message = string.Empty;
                if (str2.Count() == 2)
                {
                    if (player.WhispBlockList.Contains(str2[1].ToUpper()))
                    {
                        player.WhispBlockList.Remove(str2[1].ToUpper());
                        Message = str2[1].ToUpper() + "님의 귓속말을 거부를 해제합니다.";

                    }
                    else
                    {
                        player.WhispBlockList.Add(str2[1].ToUpper());
                        Message = str2[1].ToUpper() + "님의 귓속말을 거부합니다.";
                    }
                }
                else
                {
                    player.WhispBlock = !player.WhispBlock;
                    Message = "전체 귓속말 " + ((player.WhispBlock) ? "OFF" : "ON") + "입니다.";
                }
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, Message).Compile());
            }
            #endregion

            #region @채팅
            else if (str2[0] == "@채팅")
            {
                player.ChatBlack = !player.ChatBlack;
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "채팅거부 " + (player.ChatBlack ? "ON" : "OFF")).Compile());
            }
            #endregion

            #region @시스템채팅
            else if (str2[0] == "@시스템채팅")
            {
                player.SystemBlock = !player.SystemBlock;
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "시스템 채팅거부 " + (player.SystemBlock ? "ON" : "OFF")).Compile());
            }
            #endregion

            #region @길드설정
            else if (str2[0] == "@길드설정")
            {
                player.GuildJoinFlag = !player.GuildJoinFlag;
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("길드설정 {0} ", (player.GuildJoinFlag ? "ON" : "OFF"))).Compile());
            }
            #endregion

            #region @턴속성
            else if (str2[0] == "@턴속성")
            {
                SendPacket(new UpdateChatBox(0x94, 0x52, 1, string.Format("힘 : {0}, 지력 : {1} 숙련 : {2} 생명 : {3}", player.b_Str, player.b_Men, player.b_Dex, player.b_Vit)).Compile());
            }
            #endregion

            #region /귓속말
            else if (str[0] == '/')
            {
                if (str2.Count() <= 1)
                    return;

                string flage = "실패";
                var parsename = str2[0].ToString().Substring(1).ToUpper();
                var whisp = handler.Total.Where(xe => xe.Value != null && xe.Value.loggedIn && xe.Value.Name == parsename).FirstOrDefault().Value;


                if (whisp == null || (whisp != null && !whisp.loggedIn))
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "접속중인 사용자가 아닙니다").Compile());

                else if (whisp.WhispBlockList.Contains(player.Name) || whisp.WhispBlock)
                {
                    flage = "거부";
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "상대방이 귓속말 거부상태입니다.").Compile());
                }
                else if (whisp != null)
                {
                    flage = "성공";
                    var str3 = player.Name + " => " + str.Substring(str2[0].Count());
                    var str1 = "<" + whisp.Name + "> : " + str.Substring(str2[0].Count());

                    SendPacket(new UpdateChatBox(0x94, 0x52, 1, str1).Compile());
                    whisp.client.SendPacket(new UpdateChatBox(0x94, 0x52, 1, str3).Compile());
                }
                Server.mainForm.EnqueueWispChat(string.Format("[{0}] {1} => {2} : {3}", flage, player.Name, parsename, str.Substring(str2[0].Count())).ToString());

            }
            #endregion



            #region  @잔액조회
            else if (str2[0] == "@잔액조회")
            {
                string str1 = "잔액 : " + String.Format("{0:#,###}", player.BankGold) + " Gold";
                SendPacket(new UpdateChatBox(0x00, 0xff, 1, str1).Compile());
            }
            else if (str2[0] == "@입금")
            {
                if (str2.Count() <= 1 || !player.m_Map.Contains("시약상"))
                    return;
                if (World.NewNpcs[player.m_Channel].Where(xe => xe.Value.Name.Contains("로엔")).FirstOrDefault().Value.Alive == false)
                    return;

                if (!System.Text.RegularExpressions.Regex.IsMatch(str2[1], @"^[0-9]+$"))
                    return;

                if (Convert.ToInt64(str2[1]) < 0)
                    return;

                if (player.Gold >= Convert.ToUInt64(str2[1]))
                {
                    try
                    {
                        checked
                        {
                            player.BankGold += Convert.ToInt64(str2[1]);
                            player.Gold -= Convert.ToUInt64(str2[1]);
                        }
                    }
                    catch (OverflowException) { }
                }
                else
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "돈이 부족합니다").Compile());
            }
            #endregion

            #region @출금
            else if (str2[0] == "@출금")
            {
                if (str2.Count() <= 1 || !player.m_Map.Contains("시약상"))
                    return;
                if (World.NewNpcs[this.player.m_Channel].Where(xe => xe.Value.Name.Contains("로엔")).FirstOrDefault().Value.Alive == false)
                    return;
                if (!System.Text.RegularExpressions.Regex.IsMatch(str2[1], @"^[0-9]+$"))
                    return;
                if (Convert.ToInt64(str2[1]) < 0)
                    return;

                if (player.BankGold >= Convert.ToInt64(str2[1]))
                {
                    try
                    {
                        checked
                        {
                            player.BankGold -= Convert.ToInt64(str2[1]);
                            player.Gold += Convert.ToUInt64(str2[1]);
                        }
                    }
                    catch (OverflowException) { }
                }
                else
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "돈이 부족합니다").Compile());
            }
            #endregion

            #region @송금
            else if (str2[0] == "@송금")
            {
                if (str2.Count() <= 1 || !player.m_Map.Contains("시약상"))
                    return;
                if (World.NewNpcs[player.m_Channel].Where(xe => xe.Value.Name.Contains("로엔")).FirstOrDefault().Value.Alive == false)
                    return;
                if (!System.Text.RegularExpressions.Regex.IsMatch(str2[2], @"^[0-9]+$"))
                    return;
                if (Convert.ToInt64(str2[2]) < 0)
                    return;
                ulong Sendgold = Convert.ToUInt64(str2[2]);
                if (player.m_Gold < Sendgold)
                    return;

                var whisp = handler.GetPlayer(str2[1]);

                if (whisp != null && whisp.Name != this.player.Name)
                {
                    try
                    {
                        checked
                        {
                            player.Gold -= Sendgold;
                            whisp.Gold += Sendgold;
                        }
                    }
                    catch (OverflowException)
                    {
                        return;
                    }

                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}님에게 {1:#,###} Gold 송금했습니다.", whisp.Name, Sendgold)).Compile());
                    if (whisp.loggedIn)
                        whisp.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}님에게 {1:#,###} GOLD 받았습니다.", this.player.Name, Sendgold)).Compile());
                }
                else
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "송금할 사용자를 찾을 수 없습니다.").Compile());
            }
            #endregion



            #region @접속자
            else if (str2[0] == "@접속자")
            {
                if (LKCamelot.Server.tickcount.ElapsedMilliseconds - 2000 > ChatTimeout)
                {
                    ChatTimeout = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                    List<Player> keys = null;
                    if (str2.Count() == 1)
                        keys = handler.Total.Values.Where(xe => xe != null && !Settings.GMS.Contains(xe.Name.ToUpper()) && xe.loggedIn).ToList();
                    else
                    {
                        int baseClass = str2[1] == "평민" ? 0 : str2[1] == "기사" ? 1 : str2[1] == "검객" ? 2 : str2[1] == "위저드" ? 3 : 4;
                        keys = handler.Total.Values.Where(xe => xe != null && xe.ClassBase == baseClass && !Settings.GMS.Contains(xe.Name.ToUpper()) && xe.loggedIn).ToList();
                    }

                    var kl = RankSort(keys);
                    int rankIndex = kl.FindIndex(xe => xe.Name == player.Name);
                    if (rankIndex < 0)
                        SendPacket(new UpdateChatBox(0x20, 0x02, 1, string.Format("{0}님은 랭킹 대상에 포함되지 않습니다.[현재{1}명]", player.Name, keys.Count)).Compile());
                    else
                        SendPacket(new UpdateChatBox(0x20, 0x02, 1, string.Format("{0}님은 {1}번째 입니다.[현재{2}명]", player.Name, rankIndex + 1, keys.Count)).Compile());

                    string rankr = string.Empty;

                    if (kl.Count > 20)
                    {
                        int start = rankIndex < 0 ? 0 : Math.Max(0, Math.Min(rankIndex - 10, kl.Count - 20));
                        kl = kl.GetRange(start, 20);
                    }

                    foreach (var rnk in kl)
                    {
                        string name = string.Empty;
                        for (int i = Encoding.Default.GetByteCount(rnk.Name) / 2; i < 5; i++) name += " ";
                        rankr += name + rnk.Name + name + " ";

                        if (Encoding.Default.GetByteCount(rankr) / 55 >= 1)
                        {
                            rankr = rankr.Substring(0, rankr.Count() - 1);
                            SendPacket(new UpdateChatBox(0x94, 0x52, 1, rankr).Compile());
                            rankr = "";
                        }
                    }
                    if (rankr.Count() > 0)
                    {
                        rankr = rankr.Substring(0, rankr.Count() - 1);
                        SendPacket(new UpdateChatBox(0x94, 0x52, 1, rankr).Compile());
                        rankr = "";
                    }
                }
            }
            #endregion

            #region @서열
            else if (str2[0] == "@서열")
            {
                if (LKCamelot.Server.tickcount.ElapsedMilliseconds - 2000 > ChatTimeout)
                {
                    ChatTimeout = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                    string rankr = "랭크: ";

                    List<Player> keys = null;
                    if (str2.Count() == 1)
                        keys = handler.Total.Values.Where(xe => xe != null && !Settings.GMS.Contains(xe.Name.ToUpper())).ToList();
                    else
                    {
                        int baseClass = str2[1] == "평민" ? 0 : str2[1] == "기사" ? 1 : str2[1] == "검객" ? 2 : str2[1] == "위저드" ? 3 : 4;
                        keys = handler.Total.Values.Where(xe => xe != null && xe.ClassBase == baseClass && !Settings.GMS.Contains(xe.Name.ToUpper())).ToList();
                    }
                    var kl = RankSort(keys);

                    if (kl.Count > 25)
                        kl.RemoveRange(25, kl.Count - 25);

                    foreach (var rnk in kl)
                    {
                        rankr += rnk.Name + ", ";
                        if (rankr.Count() / 35 >= 1)
                        {
                            rankr = rankr.Substring(0, rankr.Count() - 2);
                            SendPacket(new UpdateChatBox(0xff, 0xff, 1, rankr).Compile());
                            rankr = "";
                        }

                    }
                    if (rankr.Count() > 0)
                    {
                        rankr = rankr.Substring(0, rankr.Count() - 2);
                        SendPacket(new UpdateChatBox(0xff, 0xff, 1, rankr).Compile());
                        rankr = "";
                    }
                    rankr = string.Format("Players Data: {0}", keys.Count);
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, rankr).Compile());
                }
            }
            #endregion

            #region @자동
            else if (str2[0] == "@자동")
            {
                var strloot = "";
                if (player.AutoHit)
                {
                    player.AutoHit = false;
                    strloot = "수동공격 모드";
                }
                else
                {
                    player.AutoHit = true;
                    strloot = "자동공격 모드";
                }
                SendPacket(new UpdateChatBox(0xff, 0xff, 1, strloot).Compile());
            }
            #endregion

         /*   #region @초기화
            else if (str == "@초기화")
            {
                if (player.GetFreeSlots() > player.Equipped2.Count())
                {
                    foreach (var it in player.Equipped2.Values)
                    {
                        it.Unequip(player, it.InvSlot);
                    }

                    var total = player.m_Str + player.m_Dex + player.m_Men + player.m_Vit + player.Extra;
                    player.Extra = (uint)total;
                    player.m_Str = 0;
                    player.m_Dex = 0;
                    player.m_Men = 0;
                    player.m_Vit = 0;
                    player.m_HPCur = player.HP;
                    player.m_MPCur = player.MP;
                    player.m_Buffs.Clear();
                    SendPacket(new UpdateCharStats(player).Compile());
                }
                else
                {
                    string achat = "빈 아이템슬롯이 부족합니다.";
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, achat).Compile());
                }
            }
            #endregion
            */
            #region @속성
            else if (str2[0] == "@속성")
            {
                try
                {
                    var extras = Convert.ToUInt16(str2[2]);
                    var stat = str2[1];
                    if (player.Extra >= extras)
                    {
                        if (stat == "힘") player.AddStat(ref player.m_Str, extras);
                        if (stat == "지력") player.AddStat(ref player.m_Men, extras);
                        if (stat == "숙련") player.AddStat(ref player.m_Dex, extras);
                        if (stat == "생명") player.AddStat(ref player.m_Vit, extras);
                        SendPacket(new UpdateCharStats(player).Compile());
                    }
                }
                catch
                {
                    string rankr = "@속성 [힘/지력/숙련/생명] [숫자]";
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, rankr).Compile());
                }
            }
            #endregion

            #region @가격, @산다 @취소
            else if (str2[0] == "@가격" || str2[0] == "@산다" || str2[0] == "@취소")
                EventCommands(str2, str);
            #endregion

            #region 자음퀴즈
            else if (str2[0] == "@초성")
            {
                if (str2.Count() < 3)
                {
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "@초성 [답] [설명]").Compile());
                    return;
                }

                var QuizNpc = World.NewNpcs[this.player.m_Channel].Where(xe => xe.Value.Name == "초성퀴즈").FirstOrDefault().Value;
                if ((QuizNpc as script.npc.Quiz).Answer != string.Empty && Server.tickcount.ElapsedMilliseconds - 30 * Settings.Minute < (QuizNpc as script.npc.Quiz).LimiteTime)
                {
                    SendPacket(new UpdateChatBox(0xff, 0xff, 1, "이미 문제가 있습니다.").Compile());
                }
                else if (QuizNpc.m_Map == player.Map && System.Text.RegularExpressions.Regex.IsMatch(str2[1], @"^[가-힣]+$"))
                {
                    string strtemp = string.Empty;
                    foreach (string s in str2.Skip(2))
                        strtemp += s + " ";
                    (QuizNpc as script.npc.Quiz).Answer = str2[1];
                    (QuizNpc as script.npc.Quiz).Question = (QuizNpc as script.npc.Quiz).ChangIC(str2[1]);
                    (QuizNpc as script.npc.Quiz).Explanation = strtemp;
                    (QuizNpc as script.npc.Quiz).LimiteTime = Server.tickcount.ElapsedMilliseconds;
                    SendPacket(new UpdateChatBox(0xff, 0xff, 1, "문제가 제출되었습니다.").Compile());
                }
            }
            #endregion



            #region 길드
            #region @길드생성
            else if (str2[0] == "@길드생성")
            {
                if (str2.Count() != 2)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "@길드생성 [길드명]").Compile());

                else if (player.Guild != null)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "가입된 길드가 있습니다.").Compile());

                else if (!System.Text.RegularExpressions.Regex.IsMatch(str2[1], @"^[a-zA-Z가-힣]+$") || Encoding.Default.GetByteCount(str2[1]) > 8)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "길드명은 한글 4자, 영문은 8자만 가능합니다.").Compile());

                else if (player.Level < Settings.GuildCreateLevel)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "길드 생성 레벨이 부족합니다.").Compile());

                else if (player.Gold < Settings.GuildCreateGold)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "길드 생성 비용이 부족합니다.").Compile());
                else if (World.NewGuilds.Where(xe => xe.Key == str2[1].ToUpper()).Any())
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "동일한 이름의 길드가 있습니다.").Compile());

                else
                {
                    try
                    {
                        player.Gold = checked(player.Gold - Settings.GuildCreateGold);
                    }
                    catch (OverflowException) { }

                    var guild = new model.BaseGuild();
                    guild.Name = str2[1];
                    guild.Member.Add(player.m_Serial, 0);

                    World.NewGuilds.TryAdd(guild.Name, guild);

                    player.Guild = guild;

                    if (Settings.GuildCreateAlarm)
                    {
                        string message = string.Format("[시스템] {0}길드가 창설 되었습니다.", str2[1]);
                        World.SendToAll(new QueDele("all", new UpdateChatBox(0x80, 0x03, 384, message).Compile()));
                    }
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}길드 생성 완료", str2[1])).Compile());
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("\'@길드맵설정 [메세지]\'를 이용하여,", str2[1])).Compile());
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("길드맵을 설정할 수 있습니다.")).Compile());
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("더 많은 길드명령어를 보시려면 \'@길드명령어\'를 입력하세요", str2[1])).Compile());
                }
            }
            #endregion

            #region @길드맵 설정
            else if (str2[0] == "@길드맵설정")
            {
                if (str2.Count() < 2)
                {
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "@길드맵설정 [메세지]").Compile());
                }
                else if (player.Guild == null || !player.Guild.Member.Where(xe => xe.Value < 1 && xe.Key == player.m_Serial).Any() || player.Guild.GuildMapEnabled == false)
                {
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "권한이 없거나 길드맵이 있습니다.").Compile());
                }
                else
                {
                    var aroundguild = World.NewGuilds.Where(xe => xe.Value.Board != null && xe.Value.Board.m_Channel == player.m_Channel && xe.Value.Board.m_Map == player.Map && World.Dist2d(xe.Value.Board.m_Loc, player.Loc) < 12).FirstOrDefault();

                    if (!Settings.VillageName.Contains(player.Map.Split(',')[0]))
                    {
                        SendPacket(new UpdateChatBox(0x00, 0xff, 1, "해당맵에 길드땅을 지정할 수 없습니다.").Compile());
                    }
                    else if (aroundguild.Value == null)
                    {
                        string message = string.Empty;
                        foreach (string s in str2.Skip(1))
                            message += s + " ";

                        if (message.Contains(","))
                        {
                            SendPacket(new UpdateChatBox(0x00, 0xff, 1, "메세지에 쉼표(,)를 사용할 수 없습니다.").Compile());
                        }
                        else
                        {
                            player.Guild.GuildMapEnabled = false;

                            player.Guild.Board = new script.npc.BoardSign(model.Spawnere.SignSerial++, player.m_Channel, player.Map, player.Loc, 2, player.Guild.Name + " : " + message);
                            World.NewNpcs[player.m_Channel].TryAdd(player.Guild.Board.m_Serial, player.Guild.Board);

                            SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}({1},{2})길드 땅이 설정되었습니다.", player.Guild.Name, player.Loc.X, player.Loc.Y)).Compile());
                        }
                    }
                    else
                        SendPacket(new UpdateChatBox(0x00, 0xff, 1, "다른 길드땅 주변에 생성할 수 없습니다.").Compile());

                }
            }
            #endregion

            #region @길드가입
            else if (str2[0] == "@길드가입")
            {
                if (player.Guild == null || !player.Guild.Member.Where(xe => xe.Value < 2 && xe.Key == player.m_Serial).Any())
                {
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "길드가 없거나 권한이 없습니다.").Compile());
                    return;
                }
                else if (str2.Count() != 3)
                {
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "@길드가입 [아이디] [직책]").Compile());
                    return;
                }

                var play = handler.add[player.m_Channel].Where(xe => xe.Value != null && xe.Value.loggedIn && xe.Value.Name == str2[1].ToUpper() && xe.Value.Map == player.Map && World.Dist2d(player.Loc, xe.Value.Loc) <= 5).FirstOrDefault().Value;
                int position = player.Guild.FindPosition(str2[2]);
                if (play == null)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "해당 사용자를 찾을 수 없습니다.").Compile());

                else if (play.Guild != null)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "이미 길드에 가입되어 있습니다.").Compile());

                else if (!play.GuildJoinFlag)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "길드가입이 거부 상태입니다.").Compile());

                else if (position < 0 || position >= 6)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "직책을 찾을 수 없습니다.").Compile());

                // 수장(0) 직책으로는 가입시킬 수 없다. 수장 교체는 @길드양도로만 한다.
                else if (position == 0)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "수장 직책으로는 가입시킬 수 없습니다. (@길드양도)").Compile());

                else if ((position == 1 && play.Level < 100) || play.Level < Settings.GuildJoinLevel)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "해당 레벨이 부족합니다.").Compile());

                else if (player.Guild.Member.Count() > Settings.GuildMemberLimit)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "길드원이 너무 많습니다.").Compile());

                else
                {
                    play.Guild = player.Guild;
                    play.Guild.Member.Add(play.m_Serial, position);
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}님은 {1}직책으로 길드 가입 완료", play.Name, play.Guild.PostionName[position], play.Guild.Name)).Compile());
                    play.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}님은 {1}직책으로 {2}길드에 가입되었습니다.", play.Name, play.Guild.PostionName[position], play.Guild.Name)).Compile());
                }
            }
            #endregion

            #region @출두
            else if (str2[0] == "@출두")
            {
                if (player.Guild == null)
                    return;
                else if (str2.Count() != 2)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "@출두 [아이디]").Compile());
                else
                {
                    var play = handler.add[player.m_Channel].Where(xe => xe.Value != null && xe.Value.loggedIn && xe.Value.Name != player.Name && xe.Value.Name == str2[1].ToUpper()).FirstOrDefault().Value;
                    int position = -1;
                    player.Guild.Member.TryGetValue(player.m_Serial, out position);

                    if (position < 0 || position > 1)
                        SendPacket(new UpdateChatBox(0x00, 0xff, 1, "권한이 없습니다.").Compile());

                    else if (play == null || !Settings.NOPK.Contains(play.Map.Split(',')[0]))
                        SendPacket(new UpdateChatBox(0x00, 0xff, 1, "출두할 수 없습니다.").Compile());

                    else if (!player.Guild.Member.ContainsKey(play.m_Serial))
                        SendPacket(new UpdateChatBox(0x00, 0xff, 1, "길드원이 아닙니다.").Compile());

                    else
                    {
                        player.Loc = new Point2D(play.Loc.X, play.Loc.Y);
                        player.Map = play.Map;
                    }
                }
            }
            #endregion

            #region @길드원보기
            else if (str2[0] == "@길드원보기" && player.Guild != null)
            {
                var keys = handler.Total.Values.Where(xe => xe != null && xe.Guild == player.Guild).ToList();

                string strMenmber = string.Empty;
                for (int i = 0; i < player.Guild.PostionName.Length; i++)
                {
                    SendPacket(new UpdateChatBox(0x20, 0x02, 1, player.Guild.PostionName[i]).Compile());
                    var postitionList = player.Guild.Member.Where(xe => xe.Value == i).Select(xe => xe.Key).ToList();
                    var list = keys.Where(xe => postitionList.Contains(xe.m_Serial)).OrderByDescending(xe => xe.Level).ToList();

                    #region online
                    string strmessage = string.Empty;
                    var online = list.Where(xe => xe.loggedIn).ToList();
                    foreach (var p in online)
                    {
                        strmessage += ", " + p.Name;

                        if (Encoding.Default.GetByteCount(strmessage) > 30)
                        {
                            strmessage = strmessage.Remove(0, 2);
                            SendPacket(new UpdateChatBox(0xff, 0xff, 1, strmessage).Compile());
                            strmessage = string.Empty;
                        }
                    }
                    if (strmessage != string.Empty)
                    {
                        strmessage = strmessage.Remove(0, 2);
                        SendPacket(new UpdateChatBox(0xff, 0xff, 1, strmessage).Compile());
                    }
                    #endregion

                    #region OffLine
                    strmessage = string.Empty;
                    var offline = list.Where(xe => !xe.loggedIn).ToList();
                    foreach (var p in offline)
                    {
                        strmessage += ", " + p.Name;

                        if (Encoding.Default.GetByteCount(strmessage) > 30)
                        {
                            strmessage = strmessage.Remove(0, 2);
                            SendPacket(new UpdateChatBox(0x94, 0x52, 1, strmessage).Compile());
                            strmessage = string.Empty;
                        }
                    }
                    if (strmessage != string.Empty)
                    {
                        strmessage = strmessage.Remove(0, 2);
                        SendPacket(new UpdateChatBox(0x94, 0x52, 1, strmessage).Compile());
                    }
                    #endregion

                }
            }
            #endregion

            #region @길드양도
            else if (str2[0] == "@길드양도")
            {
                int myPosition;
                if (player.Guild == null || !player.Guild.Member.TryGetValue(player.m_Serial, out myPosition) || myPosition != 0)
                {
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "길드가 없거나 권한이 없습니다. (수장만 가능)").Compile());
                    return;
                }
                else if (str2.Count() != 2)
                {
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "@길드양도 [아이디]").Compile());
                    return;
                }

                var guild = player.Guild;
                string targetName = str2[1].ToUpper();
                var play = handler.Total.Where(xe => xe.Value != null && xe.Value.Guild == guild && xe.Value.Name == targetName).FirstOrDefault().Value;

                if (play == null || !guild.Member.ContainsKey(play.m_Serial))
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "해당 길드원을 찾을 수 없습니다.").Compile());

                else if (play == player)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "자기 자신에게는 양도할 수 없습니다.").Compile());

                else
                {
                    // 기존 수장은 가장 낮은 직책(일반 등급)으로, 양도받는 길드원은 수장으로.
                    int memberPosition = guild.PostionName.Length - 1;
                    guild.Member[play.m_Serial] = 0;
                    guild.Member[player.m_Serial] = memberPosition;

                    string notice = string.Format("{0}길드의 {1}이(가) {2}님에서 {3}님으로 변경되었습니다.", guild.Name, guild.PostionName[0], player.Name, play.Name);
                    foreach (var member in handler.Total.Values.Where(xe => xe != null && xe.Guild == guild && xe.loggedIn && xe.client != null).ToList())
                        member.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, notice).Compile());
                }
            }
            #endregion

            #region @길드직책변경
            else if (str2[0] == "@길드직책변경")
            {
                if (str2.Count() != 3)
                {
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "@길드직책변경 [아이디] [변경할직책]").Compile());
                }
                else
                {
                    if (player.Guild.Member.Where(xe => xe.Value < 2 && xe.Key == player.m_Serial).Any())
                    {
                        var play = handler.Total.Where(xe => xe.Value != null && xe.Value.Guild != null && xe.Value.Guild.Name == player.Guild.Name && xe.Value.Name == str2[1].ToUpper()).FirstOrDefault().Value;
                        if (play == null)
                            SendPacket(new UpdateChatBox(0x00, 0xff, 1, "해당 사용자가 없습니다.").Compile());

                        else
                        {
                            int pl, target;
                            play.Guild.Member.TryGetValue(player.m_Serial, out pl);                 //자신
                            play.Guild.Member.TryGetValue(play.m_Serial, out target);               //해당자
                            if (pl > target)
                                return;

                            // 수장이 자신의 직책을 바꾸면 수장 없는 길드가 된다. 수장 교체는 @길드양도로만 한다.
                            if (target == 0 && play.Guild.Member.ContainsKey(play.m_Serial))
                            {
                                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "수장의 직책은 변경할 수 없습니다. (@길드양도)").Compile());
                                return;
                            }

                            if (player.Guild.PostionName.Where(xe => xe.Equals(str2[2])).Any())
                            {
                                int newPosition = player.Guild.FindPosition(str2[2]);
                                if (newPosition == 0)
                                {
                                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "해당 직책으로 변경할 수 없습니다.").Compile());
                                    return;
                                }

                                player.Guild.Member.Remove(play.m_Serial);
                                player.Guild.Member.Add(play.m_Serial, newPosition);


                                SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}님의 직책이 {1}에서 {2}로 변경되었습니다.", play.Name, player.Guild.PostionName[target], player.Guild.PostionName[newPosition])).Compile());
                                play.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("직책이 {0}에서 {1}로 변경되었습니다.", player.Guild.PostionName[target], player.Guild.PostionName[newPosition])).Compile());
                            }
                            else
                                SendPacket(new UpdateChatBox(0x00, 0xff, 1, "해당 직책명이 없습니다.").Compile());
                        }
                    }
                }
            }
            #endregion

            #region @길드직책명
            else if (str2[0] == "@길드직책명" && player.Guild != null)
            {
                if (str2.Count() != 3)
                {
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, player.Guild.ToStringPostionName()).Compile());
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "@길드직책명 [변경할직책] [변경직책]").Compile());
                }
                else
                {
                    if (Encoding.Default.GetByteCount(str2[2]) > 10)
                        SendPacket(new UpdateChatBox(0x00, 0xff, 1, "직책명은 한글 5자이내, 영문 10자이내 입니다.").Compile());

                    // 쉼표 등 특수문자가 들어가면 길드 저장 줄의 칸이 밀려 다음 기동 때 그 길드를 읽지 못한다.
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(str2[2], @"^[a-zA-Z0-9가-힣]+$"))
                        SendPacket(new UpdateChatBox(0x00, 0xff, 1, "직책명은 한글, 영문, 숫자만 사용할 수 있습니다.").Compile());

                    else if (player.Guild.Member.Where(xe => xe.Value == 0 && xe.Key == player.m_Serial).Any())
                    {
                        if (player.Guild.FindPosition(str2[1]) == -1)
                            SendPacket(new UpdateChatBox(0x00, 0xff, 1, "변경할 직책을 찾을 수 없습니다.").Compile());

                        else if (player.Guild.FindPosition(str2[2]) == -1)
                        {
                            player.Guild.PostionName[player.Guild.FindPosition(str2[1])] = str2[2];
                            SendPacket(new UpdateChatBox(0x00, 0xff, 1, "변경완료").Compile());
                        }
                        else
                            SendPacket(new UpdateChatBox(0x00, 0xff, 1, "직책명이 있습니다.").Compile());
                    }
                }
            }
            #endregion

            #region @길드탈퇴
            else if (str2[0] == "@길드탈퇴")
            {
                int pl, target;
                if (player.Guild == null)
                    return;

                else if (str2.Count() == 2)
                {
                    if (player.Guild.Member.Where(xe => xe.Value < 2 && xe.Key == player.m_Serial).Any())
                    {
                        var play = handler.Total.Where(xe => xe.Value != null && xe.Value.Guild != null && xe.Value.Guild.Name == player.Guild.Name && xe.Value.Name == str2[1].ToUpper()).FirstOrDefault().Value;
                        if (play == null)
                            SendPacket(new UpdateChatBox(0x00, 0xff, 1, "해당 사용자가 없습니다.").Compile());

                        else
                        {
                            play.Guild.Member.TryGetValue(player.m_Serial, out pl);
                            play.Guild.Member.TryGetValue(play.m_Serial, out target);
                            if (pl >= target)
                                return;

                            play.Guild.Member.Remove(play.m_Serial);
                            play.Guild = null;
                            SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}님이 길드에서 탈퇴되었습니다.", play.Name)).Compile());
                            if (play.loggedIn)
                                play.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}길드에서 탈퇴되었습니다.", player.Guild.Name)).Compile());

                        }
                    }
                }
                else
                {
                    player.Guild.Member.TryGetValue(player.m_Serial, out pl);
                    if (pl == 0)
                        return;

                    player.Guild.Member.Remove(player.m_Serial);
                    player.Guild = null;
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "길드 탈퇴 성공").Compile());
                }
            }
            #endregion

            #region @길드삭제
            else if (str2[0] == "@길드삭제")
            {
                if (str2.Count() != 3)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "@길드삭제 [길드명] [길드명]").Compile());
                else if (str2[1] != str2[2]) { }
                else
                {
                    int postion;
                    player.Guild.Member.TryGetValue(player.m_Serial, out postion);

                    if (postion != 0)
                        SendPacket(new UpdateChatBox(0x00, 0xff, 1, "권한이 없습니다.").Compile());
                    else if (str2[1] != player.Guild.Name) { }
                    else
                    {
                        var playerList = PlayerHandler.getSingleton().Total.Where(xe => xe.Value.Guild != null && xe.Value.Guild.Name == str2[1]).ToList();

                        foreach (var p in playerList)
                        {
                            p.Value.Guild = null;
                            if (p.Value.loggedIn)
                                p.Value.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "길드가 삭제 되었습니다.").Compile());
                        }
                        BaseGuild guild;
                        script.npc.BaseNPC board;
                        World.NewGuilds.TryRemove(str2[1], out guild);
                        World.NewNpcs[guild.Board.m_Channel].TryRemove(guild.Board.m_Serial, out board);
                    }
                }
            }
            #endregion

            #region @길드정보
            else if (str2[0] == "@길드정보")
            {
                if (player.Guild == null)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("가입된 길드가 없습니다")).Compile());
                else
                {
                    var pl = PlayerHandler.getSingleton().Total.Where(xe => xe.Value.m_Serial == player.Guild.Member.Where(ee => ee.Value == 0).FirstOrDefault().Key).FirstOrDefault();
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("길드명 : {0}", player.Guild.Name)).Compile());
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("수장명 : {0}", pl.Value.Name)).Compile());
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("직책명 : {0}", player.Guild.GetPositionString(player.Name))).Compile());
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("길드땅 : {0}", !player.Guild.GuildMapEnabled)).Compile());
                    if (!player.Guild.GuildMapEnabled)
                        SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("길드맵 : {0}-{1},{2}", Settings.VillageName.ElementAt(player.Guild.Board.m_Channel), player.Guild.Board.m_Loc.X, player.Guild.Board.m_Loc.Y)).Compile());
                }
            }
            #endregion

            #endregion


            #region 도우미지정
            else if (str2[0] == "@도우미지정")
            {
                if (str2.Count() != 2)
                {
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "@도우미지정 [아이디]").Compile());
                    return;
                }
                var play = handler.add[player.m_Channel].Where(xe => xe.Value != null && xe.Value.loggedIn && xe.Value.Name != player.Name && xe.Value.Name == str2[1].ToUpper() && xe.Value.Map == player.Map && World.Dist2d(player.Loc, xe.Value.Loc) <= 5).FirstOrDefault().Value;

                if (play == null)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "사용자를 찾을 수 없습니다.").Compile());
                else if (player.Level > Settings.HelperLevel)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "레벨이 높습니다.").Compile());
                else
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "도우미가 지정되었습니다.").Compile());
            }
            #endregion


            #region 교환
            else if (str2[0] == "@교환")
            {
                var target = handler.add[player.m_Channel].Where(xe => xe.Value != null && xe.Value.loggedIn && xe.Value.Name == str2[1].ToUpper() && xe.Value.Name != player.Name).FirstOrDefault();

                if (target.Value == null || World.Dist2d(target.Value.Loc, player.Loc) > 5 || target.Value.Map != player.Map || target.Value.m_Channel != player.m_Channel)
                {
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "교환할 대상이 없습니다.").Compile());
                    return;
                }
                else if (player.Exchange != null)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "교환중입니다.").Compile());

                else if (target.Value.Exchange != null)
                    SendPacket(new UpdateChatBox(0x00, 0xff, 1, "상대방이 교환중입니다.").Compile());

                else
                {
                    new ExchangeBox(player, target.Value);
                }
            }

            #endregion


            else if (str2[0] == "@tele")
            {
                if (!Settings.GMS.Contains(player.Name))
                    return;
                if (str2.Count() == 2)
                {
                    var teleon = handler.Total.Where(xe => xe.Value != null && xe.Value.Name == str2[1]).FirstOrDefault().Value;
                    if (teleon != null)
                    {
                        player.Loc = new Point2D(teleon.Loc.X, teleon.Loc.Y);
                        player.Map = teleon.Map;
                    }

                }
                else if (str2.Count() == 4)
                {
                    player.Loc = new Point2D(Convert.ToInt16(str2[2]), Convert.ToInt16(str2[3]));
                    player.Map = str2[1];
                }
            }
            else if (str2[0] == "@마법")
            {
                if (!Settings.GMS.Contains(player.Name))
                    return;
                try
                {
                    string activatorstring = "LKCamelot.script.spells.";
                    var tempspell = Activator.CreateInstance(Type.GetType(activatorstring + str2[1]));
                    (tempspell as script.spells.Spell).Caster = player;
                    (tempspell as script.spells.Spell).Slot = player.GetFreeSpellSlot();
                    (tempspell as script.spells.Spell).SLevel2 = 99;
                    (tempspell as script.spells.Spell).Level = 12;
                    player.m_MagicLearned.Add((tempspell as script.spells.Spell));
                    SendPacket(new CreateSlotMagic2((tempspell as script.spells.Spell)).Compile());
                }
                catch { return; }
            }
            else if (str2[0] == "@아이템")
            {
                if (!Settings.GMS.Contains(player.Name))
                    return;
                if (player.GetFreeSlots() == 0)
                    return;

                script.item.Item temp;
                try
                {
                    temp = (script.item.Item)Activator.CreateInstance(Type.GetType("LKCamelot.script.item." + str2[1]));
                }
                catch { return; }

                var item = temp.Inventory(player);
                SendPacket(new AddItemToInventory2(item).Compile());

            }

            else if (str[0] == '~' || str2[0] == "@tele" || str2[0] == "@learn"
    || str2[0] == "@invis" || str2[0] == "@createitem" || str2[0] == "@kick" || str2[0] == "@tap")
            {
                HandleGMChat(str2, str);
            }
        }
    }
}
