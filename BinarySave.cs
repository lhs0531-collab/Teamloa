    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using LKCamelot.model;

namespace LKCamelot
{
    public static class BinaryIO
    {
        public static void GetMobDBFile()
        {
            var list = World.NewMonsters[0].Values.ToList();

            using (StreamWriter sw = new StreamWriter(Settings.rootPath + "MobDB.txt"))
            {
                int i = 1;
                foreach (script.monster.Monster m in list)
                {
                    sw.WriteLine( string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                        i++,
                        m.Name,
                        m.m_MonsterID,
                        m.Color,
                        ((script.monster.MobType)m.MobType).ToString(),
                        m.SpawnTime,
                        m.WalkSpeed,
                        m.AttackSpeed,
                        m.XP,
                        m.AC,
                        m.Hit,
                        m.Dam,
                        m.HP
                        ));
                }
            }
        }

        public static void SaveGuild(List<LKCamelot.model.BaseGuild> Guilds)
        {
            lock (readPlayerlock)
            {
                using (StreamWriter sw = new StreamWriter(Settings.rootPath + "worldsaveGuild.txt"))
                {
                    foreach (var guild in Guilds)
                    {
                        try
                        {
                            //if ( World.DeletePlayer.Contains(guild.Member.Where(xe=>xe.Value == 0).FirstOrDefault().Key) )
                            int channel = 0;
                            string map = string.Empty;
                            string message = string.Empty;
                            Point2D loc = new Point2D(0, 0);
                            if (!guild.GuildMapEnabled)
                            {
                                channel = guild.Board.m_Channel;
                                map = guild.Board.m_Map.Split(',')[1];
                                message = guild.Board.Message[0];
                                loc = guild.Board.m_Loc;
                            }

                            sw.WriteLine(
                                string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                                guild.Name, guild.GuildMapEnabled, channel, map, loc.X, loc.Y, message, guild.ToStringPostionName(), guild.ToStringNember(World.DeletePlayer)
                                ));
                        }
                        catch
                        {
                            Console.WriteLine("Guild Save Err : " + guild.Name);                        
                        }
                    }
                }
            }
            Console.WriteLine("Guild Save Complete");
        }

        public static List<LKCamelot.model.BaseGuild> LoadGuilds()
        {
            lock (readPlayerlock)
            {
                List<LKCamelot.model.BaseGuild> temp = new List<LKCamelot.model.BaseGuild>();
                if (File.Exists(Settings.rootPath + "worldsaveGuild.txt"))
                {
                    using (StreamReader sr = new StreamReader(Settings.rootPath + "worldsaveGuild.txt"))
                    {
                        int lineNo = 0;
                        while (!sr.EndOfStream)
                        {
                            string raw = sr.ReadLine();
                            lineNo++;
                            if (string.IsNullOrWhiteSpace(raw))
                                continue;

                            // 깨진 줄 하나 때문에 기동 전체가 실패하지 않도록 줄 단위로 보호한다.
                            try
                            {
                                var ret = new LKCamelot.model.BaseGuild(raw.Split(','));
                                temp.Add(ret);
                            }
                            catch (Exception e) { LogLoadError("worldsaveGuild.txt", lineNo, raw, e); }
                        }
                    }
                }
                return temp;
            }
        }





        public static void SavePlayers(List<LKCamelot.model.Player> players)
        {
            lock (readPlayerlock)
            {
                using (StreamWriter sw = new StreamWriter(Settings.rootPath + "worldsavePl.txt"))
                {
                    foreach (var play in players)
                    {
                        int autoDeleteDays = play.Promo > 0
                            ? Settings.AutoDeleteDaysPromo
                            : Settings.AutoDeleteDaysNormal;
                        if (!Settings.GMS.Contains(play.Name)
                            && autoDeleteDays > 0
                            && play.LastAccessTime.AddDays(autoDeleteDays) < DateTime.Now)
                        {
                            World.DeletePlayer.Add(play.m_Serial);
                            continue;
                        }

                        sw.WriteLine(
                            string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39}",
                            Convert.ToInt32(play.m_Serial), play.Name, play.Pass, (int)play.Class, play.Stage, play.pkhistoryTime, play.BenNeighbor, play.neighbor, play.Map.Split(',')[0], play.Loc.X, play.Loc.Y,
                            play.Face, play.m_Str, play.m_Men, play.m_Dex, play.m_Vit, play.Extra, play.b_Str, play.b_Men, play.b_Dex, play.b_Vit, play.HP, play.HPCur, play.MP, play.MPCur, 
                            play.Level, play.XP, play.MinerLevel, play.MinerXP, play.BSLevel, play.BSXP, play.HelperLevel, play.Gold, play.BankGold, play.MagicLearnedString(),
                            play.CreateTime, play.LastAccessTime, play.IP, play.Ben, play.b_Extra
                            
                            ));
                    }
                }
            }
            Console.WriteLine("Player Save Complete");
        }

        public static List<LKCamelot.model.Player> LoadPlayers()
        {
            lock (readPlayerlock)
            {
                List<LKCamelot.model.Player> temp = new List<LKCamelot.model.Player>();
                if (File.Exists(Settings.rootPath + "worldsavePl.txt"))
                {
                    using (StreamReader sr = new StreamReader(Settings.rootPath + "worldsavePl.txt"))
                    {
                        int lineNo = 0;
                        while (!sr.EndOfStream)
                        {
                            string raw = sr.ReadLine();
                            lineNo++;
                            if (string.IsNullOrWhiteSpace(raw))
                                continue;

                            try
                            {
                                string[] line = raw.Split(',');
                                var ret = new LKCamelot.model.Player(Convert.ToInt32(line[0]), line);
                                temp.Add(ret);
                            }
                            catch (Exception e) { LogLoadError("worldsavePl.txt", lineNo, raw, e); }
                        }
                    }
                }
                return temp;
            }
        }

        /// <summary>
        /// 저장 파일에서 읽지 못한 줄을 콘솔과 loaderrors.txt(실행 폴더)에 남긴다.
        /// 건너뛴 줄의 데이터는 메모리에 올라오지 않으므로 다음 저장 때 파일에서 빠진다.
        /// 원본은 저장 직전 백업(LKDB\backup)에 남아 있으니 로그의 줄 내용을 보고 복구한다.
        /// </summary>
        private static void LogLoadError(string file, int lineNo, string raw, Exception e)
        {
            string msg = string.Format("{0} [LoadError] {1} line {2}: {3} | {4}",
                DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]"), file, lineNo, e.Message, raw);
            Console.WriteLine(msg);
            try
            {
                using (StreamWriter sw = new StreamWriter("loaderrors.txt", true))
                    sw.WriteLine(msg);
            }
            catch { }
        }

        public static void SavePlayer(LKCamelot.model.Player play)
        {
            lock (readPlayerlock)
            {
                using (StreamWriter sw = new StreamWriter(Settings.rootPath + "worldsavePl.txt", true))
                {
                    sw.WriteLine(
                        string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39}",
                        Convert.ToInt32(play.m_Serial), play.Name, play.Pass, (int)play.Class, play.Stage, play.pkhistoryTime, play.BenNeighbor, play.neighbor, play.Map.Split(',')[0], play.Loc.X, play.Loc.Y,
                        play.Face, play.m_Str, play.m_Men, play.m_Dex, play.m_Vit, play.Extra, play.b_Str, play.b_Men, play.b_Dex, play.b_Vit, play.HP, play.HPCur, play.MP, play.MPCur,
                        play.Level, play.XP, play.MinerLevel, play.MinerXP, play.BSLevel, play.BSXP, play.HelperLevel, play.Gold, play.BankGold, play.MagicLearnedString(),
                        play.CreateTime, play.LastAccessTime, play.IP, play.Ben, play.b_Extra
                        ));
                }
            }
            Console.WriteLine("Player Save Complete");
        }

        public static void UpdatePlayer(LKCamelot.model.Player play)
        {
            lock (readPlayerlock)
            {
                using (StreamReader sr = new StreamReader(Settings.rootPath + "worldsavePl.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split('$');
                        if (line[1] != play.Name)
                            continue;

                        using (StreamWriter sw = new StreamWriter(Settings.rootPath + "worldsavePl.txt", true))
                        {
                            sw.WriteLine(
                                string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39}",
                                Convert.ToInt32(play.m_Serial), play.Name, play.Pass, (int)play.Class, play.Stage, play.pkhistoryTime, play.BenNeighbor, play.neighbor, play.Map.Split(',')[0], play.Loc.X, play.Loc.Y,
                                play.Face, play.m_Str, play.m_Men, play.m_Dex, play.m_Vit, play.Extra, play.b_Str, play.b_Men, play.b_Dex, play.b_Vit, play.HP, play.HPCur, play.MP, play.MPCur,
                                play.Level, play.XP, play.MinerLevel, play.MinerXP, play.BSLevel, play.BSXP, play.HelperLevel, play.Gold, play.BankGold, play.MagicLearnedString(),
                                play.CreateTime, play.LastAccessTime, play.IP, play.Ben, play.b_Extra

                                ));
                        }
                    }
                }
            }
        }

        public static List<string> LoadName(string name)
        {
            List<string> ret = null;
            lock (readPlayerlock)
            {
                if (File.Exists(Settings.rootPath + "worldsavePl.txt"))
                {
                    using (StreamReader sr = new StreamReader(Settings.rootPath + "worldsavePl.txt"))
                    {
                        while (!sr.EndOfStream)
                        {
                            string[] line = sr.ReadLine().Split(',');
                            if (line[1] != name)
                                continue;

                            ret.Add(line[1]);
                            ret.Add(line[2]);
                        }
                    }
                }
            }
            return ret;
        }

        public static object readPlayerlock = new object();
        public static LKCamelot.model.Player LoadPlayer(io.IOClient client, string name)
        {
            lock (readPlayerlock)
            {
                LKCamelot.model.Player ret = null;
                if (File.Exists(Settings.rootPath + "worldsavePl.txt"))
                {
                    using (StreamReader sr = new StreamReader(Settings.rootPath + "worldsavePl.txt"))
                    {
                        while (!sr.EndOfStream)
                        {
                            string[] line = sr.ReadLine().Split(',');
                            if (line[1] != name)
                                continue;

                            ret = new model.Player(client, Convert.ToInt32(line[0]), line);

                        }
                    }
                }
                return ret;
            }
        }


        public static object readGuildMaplock = new object();
        public static List<LKCamelot.script.npc.BoardSign> LoadGuildMap()
        {
            lock (readGuildMaplock)
            {
                List<LKCamelot.script.npc.BoardSign> temp = new List<LKCamelot.script.npc.BoardSign>();
                if (File.Exists(Settings.rootPath + "worldgildMap.txt"))
                {
                    using (StreamReader sr = new StreamReader(Settings.rootPath + "worldgildMap.txt"))
                    {
                        while (!sr.EndOfStream)
                        {
                            string[] line = sr.ReadLine().Split('_');

                            var ret = new LKCamelot.script.npc.BoardSign(Convert.ToInt32(line[0]), Convert.ToInt32(line[1]), line[2], new Point2D(Convert.ToInt32(line[3]), Convert.ToInt32(line[4])), 2, line[5]);
                            temp.Add(ret);
                        }
                    }
                }
                return temp;
            }
        }

        public static void SaveGuildMap(List<script.npc.BoardSign> Sings)
        {
            lock (readPlayerlock)
            {
                using (StreamWriter sw = new StreamWriter(Settings.rootPath + "worldgildMap.txt", true))
                {
                    foreach (var sing in Sings)
                    {
                        sw.WriteLine(
                            string.Format("{0}_{1}_{2}_{3}_{4}_{5}_{6}_{7}", Convert.ToInt32(sing.m_Serial), sing.m_Channel, sing.m_Map, sing.m_Loc.X, sing.m_Loc.Y, sing.Message
                            ));
                    }
                }
            }
            Console.WriteLine("Player Save Complete");
        }











        public static void SaveItems(List<script.item.Item> items)
        {
            using (StreamWriter sw = new StreamWriter(Settings.rootPath + "worldsaveIt.txt"))
            {
                foreach (var item in items.OrderBy(xe => xe.Name))
                {
                    if (item.InvSlot >= 0)
                    {
                        if (!World.DeletePlayer.Contains(item.ParSer) && !Settings.NoSaveItem.Contains(item.GetType()) )
                        {
                            string parsedType = item.GetType().Name.ToString();
                            sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6}",
                                Convert.ToInt32(item.m_Serial), 
                                parsedType, 
                                item.Durability, 
                                item.ParSer, 
                                item.InvSlot, 
                                item.Stage, 
                                (item.Quantity > item.Limit? 1: item.Quantity))
                                );
                        }
                    }
                }
            }

            Console.WriteLine("Items Save Complete");
        }
        public static void SaveAuctionItems(List<script.item.AuctionItem> items)
        {
            using (StreamWriter sw = new StreamWriter(Settings.rootPath + "worldsaveAi.txt"))
            {
                foreach (var item in items.OrderBy(xe => xe.item.Name))
                {
                    if (item.item.InvSlot >= 0)
                    {
                        string parsedType = item.item.GetType().Name.ToString();
                        sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", Convert.ToInt32(item.item.m_Serial),
                            parsedType, item.item.Durability, item.item.ParSer, item.item.InvSlot,
                            item.item.Stage, item.item.Quantity, item.goldprice, item.agoldprice, item.flags,
                            item.sellerSerial, item.buyerSerial, (int)item.state));
                    }
                }
            }
        }

        public static List<script.item.Item> LoadItems()
        {
            var ret = new List<script.item.Item>();
            if (File.Exists(Settings.rootPath + "worldsaveIt.txt"))
            {
                using (StreamReader sr = new StreamReader(Settings.rootPath + "worldsaveIt.txt"))
                {
                    int lineNo = 0;
                    while (!sr.EndOfStream)
                    {
                        string raw = sr.ReadLine();
                        lineNo++;
                        if (string.IsNullOrWhiteSpace(raw))
                            continue;

                        try
                        {
                            string[] line = raw.Split(',');

                            script.item.Item temp = null;
                            try
                            {
                                temp = (script.item.Item)Activator.CreateInstance(Type.GetType("LKCamelot.script.item." + line[1]), (LKCamelot.model.Serial)Convert.ToInt32(line[0]));
                            }
                            catch { Console.WriteLine(line[1] + " - ParSer:" + line[3]); continue; }

                            //내구도
                            if ((temp.Durability = Convert.ToInt32(line[2])) > temp.InitMaxHits)
                                temp.Durability = temp.InitMaxHits;
                            temp.ParSer = Convert.ToInt32(line[3]);         //소유주
                            try
                            {
                                temp.m_InvSlot = Convert.ToInt32(line[4]);      //슬롯번호
                            }
                            catch
                            {
                                temp.m_InvSlot = 0;
                            }
                            temp.Stage = Convert.ToInt32(line[5]);              //단수
                            if (temp.Stackable)
                                temp.Quantity = Convert.ToInt32(line[6]) > temp.Limit ? 1 : Convert.ToInt32(line[6]);           //갯수
                            else
                                temp.Quantity = 1;
                            ret.Add(temp);
                        }
                        catch (Exception e) { LogLoadError("worldsaveIt.txt", lineNo, raw, e); }
                    }
                }
            }
            return ret;
        }

        public static void BackUpData()
        {
            //string path = Settings.rootPath + "backup" + Path.DirectorySeparatorChar + DateTime.Now.Year;


            string path = Settings.rootPath + "backup" + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyyMMdd");
           
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!Directory.Exists(path + "\\Player"))
                Directory.CreateDirectory(path + "\\Player");
            if (!Directory.Exists(path + "\\Items"))
                Directory.CreateDirectory(path + "\\Items");
            if (!Directory.Exists(path + "\\Ai"))
                Directory.CreateDirectory(path + "\\Ai");
            if (!Directory.Exists(path + "\\Guild"))
                Directory.CreateDirectory(path + "\\Guild");


            try
            {
                File.Copy(Settings.rootPath + "worldsavePl.txt", path + "\\Player" + Path.DirectorySeparatorChar + "worldsavePl-" + DateTime.Now.ToString("HHmmss") + ".txt");
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            try
            {
                File.Copy(Settings.rootPath + "worldsaveIt.txt", path + "\\Items" + Path.DirectorySeparatorChar + "worldsaveIt-" + DateTime.Now.ToString("HHmmss") + ".txt");
            }
            catch { }
            try
            {
                File.Copy(Settings.rootPath + "worldsaveAi.txt", path + "\\Ai" + Path.DirectorySeparatorChar + "worldsaveAi-" + DateTime.Now.ToString("HHmmss") + ".txt");
            }
            catch { }
            try
            {
                File.Copy(Settings.rootPath + "worldsaveGuild.txt", path + "\\Guild" + Path.DirectorySeparatorChar + "worldsaveGuild-" + DateTime.Now.ToString("HHmmss") + ".txt");
            }
            catch { }
        }

        public static List<script.item.AuctionItem> LoadAuctions()
        {
            var ret = new List<script.item.AuctionItem>();
            // 다른 로드 함수와 달리 파일 존재 검사가 없어, 파일이 없으면 기동 중 예외가 났다.
            if (!File.Exists(Settings.rootPath + "worldsaveAi.txt"))
                return ret;

            using (StreamReader sr = new StreamReader(Settings.rootPath + "worldsaveAi.txt"))
            {
                int lineNo = 0;
                while (!sr.EndOfStream)
                {
                    string raw = sr.ReadLine();
                    lineNo++;
                    if (string.IsNullOrWhiteSpace(raw))
                        continue;

                    try
                    {
                        string[] line = raw.Split(',');
                        script.item.Item temp = null;
                        script.item.AuctionItem temp2 = null;
                        try
                        {
                            temp = (script.item.Item)Activator.CreateInstance(Type.GetType("LKCamelot.script.item." + line[1]), (LKCamelot.model.Serial)Convert.ToInt32(line[0]));
                        }
                        catch { Console.WriteLine(line[1]); continue; }

                        temp.m_ItemID = Convert.ToInt32(line[2]);
                        temp.ParSer = Convert.ToInt32(line[3]);
                        temp.InvSlot = Convert.ToInt32(line[4]);
                        temp.Stage = Convert.ToInt32(line[5]);
                        temp.Quantity = Convert.ToInt32(line[6]);
                        temp2 = new script.item.AuctionItem(temp, Convert.ToUInt64(line[7]), Convert.ToInt32(line[9]));
                        temp2.item = temp;
                        temp2.sellerSerial = Convert.ToInt32(line[10]);
                        temp2.buyerSerial = Convert.ToInt32(line[11]);
                        temp2.state = (script.item.aucState)Convert.ToInt32(line[12]);
                        ret.Add(temp2);
                    }
                    catch (Exception e) { LogLoadError("worldsaveAi.txt", lineNo, raw, e); }
                }
            }
            return ret;
        }

    }
}
