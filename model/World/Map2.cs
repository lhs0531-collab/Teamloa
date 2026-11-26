using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using MySql.Data.MySqlClient;
using System.Data;
namespace LKCamelot.model
{
    public static class Map
    {
        public enum MapType { LK, VV, PS, Weak, Bone };
        public enum PKType { ImpossiblePK = 0, PossiblePK_NoIncrease = 1, PossiblePK_Increase = 2 }
        public class TiledMap
        {
            public int sizeX;
            public int sizeY;
            int obj;

            public MyPathNode[,] tiles;
            MapType Type;

            public TiledMap(int sizeX, int sizeY, MapType Type, int obj)
            {
                this.sizeX = sizeX;
                this.sizeY = sizeY;
                this.obj = obj;
                this.Type = Type;
                tiles = new MyPathNode[sizeX, sizeY];
            }
        }
        public class MapAttribute
        {
            public string Filename;

            public byte Dark;
            public int SoundTrack = -1;
            public bool GlobalTime;
            public int LimitLevel = 0;
            public bool isOpen;
            public PKType PKable;
            public bool isLegend;


            public MapAttribute() { }
            public MapAttribute(string Filename, byte Dark, bool GlobalTime)
            {
                this.Filename = Filename;

                this.Dark = Dark;
                this.GlobalTime = GlobalTime;
            }
            public MapAttribute(string Filename, byte Dark, int SoundTrack, bool GlobalTime)
            {
                this.Filename = Filename;

                this.Dark = Dark;
                this.SoundTrack = SoundTrack;
                this.GlobalTime = GlobalTime;
            }

            public MapAttribute(object Filename, object Dark, object SoundTrack, object GlobalTime, object LimitLevel, object isOpen, object PKable, object islegend)
            {
                this.Filename = Filename.ToString();
                this.Dark = Convert.ToByte(Dark);
                this.SoundTrack = Convert.ToInt32(SoundTrack);
                this.GlobalTime = Convert.ToBoolean(GlobalTime);
                this.isOpen = Convert.ToBoolean(isOpen);
                this.LimitLevel = Convert.ToInt32(LimitLevel);
                this.PKable = (PKType)Convert.ToInt32(PKable.ToString());
                this.isLegend = Convert.ToBoolean(islegend);

            }
        }

        public static void FullMapsLoad()
        {
            FullMaps = new Dictionary<string, MapAttribute>[Settings.VillageName.Count];
            for (int i = 0; i < Settings.VillageName.Count(); i++)
            {
                FullMaps[i] = new Dictionary<string, MapAttribute>();
            }

            string strConn = Settings.DB_Conn;
            DataSet MapData = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                string strQuery = "SELECT * FROM mapmaster where DC_YN='FALSE'";
                conn.Open();
                MySqlDataAdapter adpt = new MySqlDataAdapter(strQuery, conn);
                MapData.Clear();
                adpt.Fill(MapData, "Map");
            }

            foreach (DataRow d in MapData.Tables["Map"].Rows)
            {
                int vilgNum = Convert.ToInt32(d["VilgNum"]);
                if (vilgNum == -1)
                {
                    for (int i = 0; i < Settings.VillageName.Count(); i++)
                        FullMaps[i].Add(d["MAPNAME"].ToString(), new MapAttribute(d["FILENAME"], d["Fog"], d["Sound"], d["WorldTime"], d["LimitLevel"], d["isOpen"], d["isPK"], d["isLegend"]));
                }
                else
                    FullMaps[vilgNum].Add(d["MAPNAME"].ToString(), new MapAttribute(d["FILENAME"], d["Fog"], d["Sound"], d["WorldTime"], d["LimitLevel"], d["isOpen"], d["isPK"], d["isLegend"]));
            }

            script.map.GotoCmd.GotoCmdLoad();

            #region V1
            //for (int i = 0; i < Settings.VillageName.Count(); i++)
            //{
            //FullMaps[i] = new Dictionary<string, MapAttribute>();
            //FullMaps[i].Add("시약상", new MapAttribute("loen.Map", 0x00, 1001, false));
            //FullMaps[i].Add("대장간", new MapAttribute("anold.Map", 0x00, 1001, false));
            //FullMaps[i].Add("휴게실", new MapAttribute("erest.map", 0x00, 1002, true));


            //FullMaps[i].Add(Settings.VillageName.ElementAt(i) + ",21", new MapAttribute("21.map", 0x00, 1001, true));
            //FullMaps[i].Add(Settings.VillageName.ElementAt(i) + ",22", new MapAttribute("22.map", 0x00, 1001, true));
            //FullMaps[i].Add(Settings.VillageName.ElementAt(i) + ",23", new MapAttribute("23.map", 0x00, 1001, true));
            //FullMaps[i].Add(Settings.VillageName.ElementAt(i) + ",31", new MapAttribute("31.map", 0x00, 1001, true));
            //FullMaps[i].Add(Settings.VillageName.ElementAt(i) + ",32", new MapAttribute("32.map", 0x00, 1001, true));
            //FullMaps[i].Add(Settings.VillageName.ElementAt(i) + ",33", new MapAttribute("33.map", 0x00, 1001, true));
            //FullMaps[i].Add(Settings.VillageName.ElementAt(i) + ",41", new MapAttribute("41.map", 0x00, 1001, true));
            //FullMaps[i].Add(Settings.VillageName.ElementAt(i) + ",42", new MapAttribute("42.map", 0x00, 1001, true));
            //FullMaps[i].Add(Settings.VillageName.ElementAt(i) + ",43", new MapAttribute("43.map", 0x00, 1001, true));
            //FullMaps[i].Add(Settings.VillageName.ElementAt(i) + ",51", new MapAttribute("51.map", 0x00, 1001, true));
            //FullMaps[i].Add(Settings.VillageName.ElementAt(i) + ",52", new MapAttribute("52.map", 0x00, 1001, true));
            //FullMaps[i].Add(Settings.VillageName.ElementAt(i) + ",53", new MapAttribute("53.map", 0x00, 1001, true));

            //FullMaps[i].Add("교환", new MapAttribute("sell.map", 0x00, 1002, true));

            //FullMaps[i].Add("초보", new MapAttribute("estart2.map", 1, 1003, false));

            //FullMaps[i].Add("Weakly1", new MapAttribute("weakly1.map", 1, 1001, false));
            //FullMaps[i].Add("Weakly2", new MapAttribute("weakly2.map", 1, 1001, false));
            //FullMaps[i].Add("Weakly3", new MapAttribute("weakly3.map", 1, 1001, false));
            //FullMaps[i].Add("Weakly4", new MapAttribute("weakly4.map", 1, 1001, false));
            //FullMaps[i].Add("Weakly5", new MapAttribute("weakly5.map", 1, 1001, false));


            //FullMaps[i].Add("스켈1", new MapAttribute("skel1.map", 1, 1001, false));
            //FullMaps[i].Add("스켈2", new MapAttribute("skel2.map", 1, 1001, false));
            //FullMaps[i].Add("스켈3", new MapAttribute("skel3.map", 1, 1001, false));
            //FullMaps[i].Add("스켈4", new MapAttribute("skel4.map", 1, 1001, false));
            //FullMaps[i].Add("스켈5", new MapAttribute("skel5.map", 1, 1001, false));


            //FullMaps[i].Add("퀘스트", new MapAttribute("qweak.map", 1, 1001, false));
            //FullMaps[i].Add("Qweak1", new MapAttribute("qweak1.map", 4, 1001, false));
            //FullMaps[i].Add("Qweak2", new MapAttribute("qweak2.map", 4, 1001, false));
            //FullMaps[i].Add("Qweak3", new MapAttribute("qweak3.map", 4, 1001, false));
            //FullMaps[i].Add("Qweak4", new MapAttribute("qweak4.map", 4, 1001, false));
            //FullMaps[i].Add("Qweak5", new MapAttribute("qweak5.map", 4, 1001, false));


            //FullMaps[i].Add("가보자", new MapAttribute("estart1.map", 1, 1001, true));
            //FullMaps[i].Add("Start", new MapAttribute("estart3.map", 1, 1003, true));
            //FullMaps[i].Add("스콜피온", new MapAttribute("estart4.map", 1, 1003, true));
            //FullMaps[i].Add("동굴1층", new MapAttribute("cave.map", 3, 1004, true));
            //FullMaps[i].Add("동굴2층", new MapAttribute("cave1.map", 3, 1005, true));
            //FullMaps[i].Add("동굴3층", new MapAttribute("cave2.map", 3, 1005, true));
            //FullMaps[i].Add("바이킹", new MapAttribute("Viking.map", 1, 1003, true));
            //FullMaps[i].Add("골램", new MapAttribute("golem.map", 2, 1003, true));
            //FullMaps[i].Add("Golem1", new MapAttribute("golem1.map", 2, 1003, true));
            //FullMaps[i].Add("Golem2", new MapAttribute("golem2.map", 2, 1003, true));
            //FullMaps[i].Add("Golem3", new MapAttribute("golem3.map", 2, 1003, true));
            //FullMaps[i].Add("Golem12", new MapAttribute("golem12.map", 2, 1003, true));

            //FullMaps[i].Add("아발론", new MapAttribute("miner0.map", 0x04, 1005, false));
            //FullMaps[i].Add("광산0", new MapAttribute("miner0.map", 0x04, 1005, false));
            //FullMaps[i].Add("광산1", new MapAttribute("miner1.map", 0x04, 1005, false));
            //FullMaps[i].Add("광산2", new MapAttribute("miner2.map", 0x04, 1005, false));
            //FullMaps[i].Add("광산3", new MapAttribute("miner3.map", 0x04, 1005, false));

            //FullMaps[i].Add("광산4", new MapAttribute("miner0.map", 0x05, 1005, false));
            //FullMaps[i].Add("광산5", new MapAttribute("miner1.map", 0x05, 1005, false));
            //FullMaps[i].Add("광산6", new MapAttribute("miner2.map", 0x05, 1005, false));
            //FullMaps[i].Add("광산7", new MapAttribute("miner3.map", 0x05, 1005, false));
            //FullMaps[i].Add("광산8", new MapAttribute("miner0.map", 0x05, 1005, false));

            //FullMaps[i].Add("초보광산", new MapAttribute("miner0.map", 0x05, 1005, false));


            //FullMaps[i].Add("터", new MapAttribute("elevel.map", 2, 1003, false));
            //FullMaps[i].Add("010", new MapAttribute("010map.map", 2, 1003, false));
            //FullMaps[i].Add("020", new MapAttribute("020map.map", 2, 1003, false));
            //FullMaps[i].Add("030", new MapAttribute("030map.map", 2, 1003, false));
            //FullMaps[i].Add("040", new MapAttribute("040map.map", 2, 1003, false));
            //FullMaps[i].Add("050", new MapAttribute("050map.map", 2, 1003, false));
            //FullMaps[i].Add("060", new MapAttribute("060map.map", 2, 1005, false));
            //FullMaps[i].Add("061", new MapAttribute("061map.map", 2, 1005, false));
            //FullMaps[i].Add("070", new MapAttribute("070map.map", 2, 1005, false));
            //FullMaps[i].Add("071", new MapAttribute("071map.map", 2, 1005, false));
            //FullMaps[i].Add("080", new MapAttribute("080map.map", 2, 1005, false));
            //FullMaps[i].Add("081", new MapAttribute("081map.map", 2, 1005, false));

            //FullMaps[i].Add("ItemVillage", new MapAttribute("ivillage.map", 1, 1001, true));
            //FullMaps[i].Add("ItemPigmy1", new MapAttribute("ipigmy1.map", 1, 1001, true));
            //FullMaps[i].Add("ItemPigmy2", new MapAttribute("ipigmy2.map", 1, 1001, true));
            //FullMaps[i].Add("ItemPigmy3", new MapAttribute("ipigmy3.map", 1, 1001, true));
            //FullMaps[i].Add("ItemZomby1", new MapAttribute("izomby1.map", 1, 1001, true));
            //FullMaps[i].Add("ItemZomby2", new MapAttribute("izomby2.map", 1, 1001, true));
            //FullMaps[i].Add("ItemZomby3", new MapAttribute("izomby3.map", 1, 1001, true));
            //FullMaps[i].Add("ItemSkel1", new MapAttribute("iskel1.map", 1, 1001, true));
            //FullMaps[i].Add("ItemSkel2", new MapAttribute("iskel2.map", 1, 1001, true));
            //FullMaps[i].Add("ItemSkel3", new MapAttribute("iskel3.map", 1, 1001, true));
            //FullMaps[i].Add("ItemButcher1", new MapAttribute("ibut1.map", 1, 1001, true));
            //FullMaps[i].Add("ItemButcher2", new MapAttribute("ibut2.map", 1, 1001, true));
            //FullMaps[i].Add("ItemButcher3", new MapAttribute("ibut3.map", 1, 1001, true));
            //FullMaps[i].Add("ItemMummy1", new MapAttribute("imummy1.map", 1, 1001, true));
            //FullMaps[i].Add("ItemMummy2", new MapAttribute("imummy2.map", 1, 1001, true));
            //FullMaps[i].Add("ItemMummy3", new MapAttribute("imummy3.map", 1, 1001, true));
            //FullMaps[i].Add("ItemStone1", new MapAttribute("istone1.map", 1, 1001, true));
            //FullMaps[i].Add("ItemStone2", new MapAttribute("istone2.map", 1, 1001, true));
            //FullMaps[i].Add("ItemStone3", new MapAttribute("istone3.map", 1, 1001, true));
            //FullMaps[i].Add("ItemStone4", new MapAttribute("istone4.map", 1, 1001, true));
            //FullMaps[i].Add("ItemDummy1", new MapAttribute("imummy1.map", 1, 1001, true));
            //FullMaps[i].Add("ItemDummy2", new MapAttribute("imummy2.map", 1, 1001, true));
            //FullMaps[i].Add("ItemDummy3", new MapAttribute("imummy3.map", 1, 1001, true));
            //FullMaps[i].Add("ItemDummy4", new MapAttribute("istone3.map", 1, 1001, true));
            //FullMaps[i].Add("ItemDummy5", new MapAttribute("istone4.map", 1, 1001, true));
            //FullMaps[i].Add("ItemWarDummy1", new MapAttribute("imummy1.map", 1, 1001, true));
            //FullMaps[i].Add("ItemWarDummy2", new MapAttribute("imummy2.map", 1, 1001, true));
            //FullMaps[i].Add("ItemWarDummy3", new MapAttribute("imummy3.map", 1, 1001, true));
            //FullMaps[i].Add("ItemWarDummy4", new MapAttribute("istone3.map", 1, 1001, true));
            //FullMaps[i].Add("ItemWarDummy5", new MapAttribute("istone4.map", 1, 1001, true));
            //FullMaps[i].Add("ItemHardboil1", new MapAttribute("imummy1.map", 1, 1001, true));
            //FullMaps[i].Add("ItemHardboil2", new MapAttribute("imummy2.map", 1, 1001, true));
            //FullMaps[i].Add("ItemHardboil3", new MapAttribute("imummy3.map", 1, 1001, true));
            //FullMaps[i].Add("ItemHardboil4", new MapAttribute("istone3.map", 1, 1001, true));
            //FullMaps[i].Add("ItemHardboil5", new MapAttribute("istone4.map", 1, 1001, true));
            //FullMaps[i].Add("ItemGolem1", new MapAttribute("imummy1.map", 1, 1001, true));
            //FullMaps[i].Add("ItemGolem2", new MapAttribute("imummy2.map", 1, 1001, true));
            //FullMaps[i].Add("ItemGolem3", new MapAttribute("imummy3.map", 1, 1001, true));
            //FullMaps[i].Add("ItemGolem4", new MapAttribute("istone3.map", 1, 1001, true));
            //FullMaps[i].Add("ItemGolem5", new MapAttribute("istone4.map", 1, 1001, true));


            //FullMaps[i].Add("Biggun1", new MapAttribute("estart4.map", 1, 1004, true));
            //FullMaps[i].Add("Biggun2", new MapAttribute("estart3.map", 1, 1004, true));

            //FullMaps[i].Add("Miro1", new MapAttribute("miro1.map", 1, 1001, true));
            //FullMaps[i].Add("TreasureLand", new MapAttribute("1000.map", 1, 1001, true));


            //FullMaps[i].Add("Great", new MapAttribute("great.map", 1, 1001, true));


            //FullMaps[i].Add("지구라트1", new MapAttribute("vv1.map", 4, 1001, false));
            //FullMaps[i].Add("지구라트2", new MapAttribute("vv2.map", 4, 1001, false));
            //FullMaps[i].Add("지구라트3", new MapAttribute("vv3.map", 4, 1001, false));
            //FullMaps[i].Add("지구라트4", new MapAttribute("vv4.map", 4, 1001, false));
            //FullMaps[i].Add("지구라트5", new MapAttribute("vv5.map", 4, 1001, false));

            //FullMaps[i].Add("대전장", new MapAttribute("ebox2x2.map", 1, false));       //대전장
            //FullMaps[i].Add("큰대전장", new MapAttribute("ebox4x4.map", 1, 1004, false));       //큰대전장
            //FullMaps[i].Add("오목", new MapAttribute("ebox4x4.map", 1, false));       //오목

            //FullMaps[i].Add("독방", new MapAttribute("prison.map", 1, false));
            //FullMaps[i].Add("축구장", new MapAttribute("esoccer.map", 1, 1001, true));


            //FullMaps[i].Add("가디언", new MapAttribute("ps01.map", 5, 1001, true));
            //FullMaps[i].Add("가디언1", new MapAttribute("ps02.map", 5, 1001, true));
            //FullMaps[i].Add("가디언2", new MapAttribute("ps03.map", 5, 1001, true));

            //}            
            #endregion

        }
        public static Dictionary<string, MapAttribute>[] FullMaps;
        //public static Dictionary<string, MapAttribute> FullMaps = new Dictionary<string, MapAttribute>()
        //{

        //    { "시약상", new MapAttribute("loen.Map", 0x00, 1001, false) );
        //    { "대장간", new MapAttribute("anold.Map", 0x00, 1001, false) );
        //    { "휴게실", new MapAttribute("erest.map", 0x00, 1002, true) );


        //    { "마을1-21", new MapAttribute("21.map", 0x00, 1002, true) );
        //    { "마을1-22", new MapAttribute("22.map", 0x00, 1002, true) );
        //    { "마을1-23", new MapAttribute("23.map", 0x00, 1002, true) );
        //    { "마을1-31", new MapAttribute("31.map", 0x00, 1002, true) );
        //    { "마을1-32", new MapAttribute("32.map", 0x00, 1002, true) );        
        //    { "마을1-33", new MapAttribute("33.map", 0x00, 1002, true) );
        //    { "마을1-41", new MapAttribute("41.map", 0x00, 1002, true) );
        //    { "마을1-42", new MapAttribute("42.map", 0x00, 1002, true) );
        //    { "마을1-43", new MapAttribute("43.map", 0x00, 1002, true) );
        //    { "마을1-51", new MapAttribute("51.map", 0x00, 1002, true) );
        //    { "마을1-52", new MapAttribute("52.map", 0x00, 1002, true) );
        //    { "마을1-53", new MapAttribute("53.map", 0x00, 1002, true) );

        //    { "교환", new MapAttribute("sell.map", 0x00, 1002, true) );

        //    { "초보", new MapAttribute("estart2.map", 1, 1001, false) );   

        //    { "Weakly1", new MapAttribute("weakly1.map", 1, 1001, false) );
        //    { "Weakly2", new MapAttribute("weakly2.map", 1, 1001, false) );
        //    { "Weakly3", new MapAttribute("weakly3.map", 1, 1001, false) );
        //    { "Weakly4", new MapAttribute("weakly4.map", 1, 1001, false) );
        //    { "Weakly5", new MapAttribute("weakly5.map", 1, 1001, false) );

        //    { "스켈1", new MapAttribute("skel1.map", 1, 1001, false) );
        //    { "스켈2", new MapAttribute("skel2.map", 1, 1001, false) );
        //    { "스켈3", new MapAttribute("skel3.map", 1, 1001, false) );
        //    { "스켈4", new MapAttribute("skel4.map", 1, 1001, false) );
        //    { "스켈5", new MapAttribute("skel5.map", 1, 1001, false) );




        //    { "퀘스트", new MapAttribute("qweak.map", 1, 1001, false) );
        //    { "Qweak1", new MapAttribute("qweak1.map", 4, 1001, false) );
        //    { "Qweak2", new MapAttribute("qweak2.map", 4, 1001, false) );
        //    { "Qweak3", new MapAttribute("qweak3.map", 4, 1001, false) );
        //    { "Qweak4", new MapAttribute("qweak4.map", 4, 1001, false) );
        //    { "Qweak5", new MapAttribute("qweak5.map", 4, 1001, false) );


        //    { "가보자", new MapAttribute("estart1.map", 1, 1001, true) );
        //    { "Start", new MapAttribute("estart3.map", 1, 1001, true) );
        //    { "스콜피온", new MapAttribute("estart4.map", 1, 1001, true) );
        //    { "동굴1층", new MapAttribute("cave.map", 3, 1001, true) );
        //    { "동굴2층", new MapAttribute("cave1.map", 3, 1001, true) );
        //    { "동굴3층", new MapAttribute("cave2.map", 3, 1001, true) );            
        //    { "바이킹", new MapAttribute("Viking.map", 1, 1001, true) );

        //    { "아발론", new MapAttribute("miner0.map", 0x04, 1001, false) );
        //    { "광산0", new MapAttribute("miner0.map", 0x04, 1001, false) );
        //    { "광산1", new MapAttribute("miner1.map", 0x04, 1001, false) );
        //    { "광산2", new MapAttribute("miner2.map", 0x04, 1001, false) );
        //    { "광산3", new MapAttribute("miner3.map", 0x04, 1001, false) );


        //    { "터", new MapAttribute("elevel.map", 2, 1004, false) );
        //    { "010", new MapAttribute("010map.map", 2, 1004, false) );
        //    { "020", new MapAttribute("020map.map", 2, 1004, false) );
        //    { "030", new MapAttribute("030map.map", 2, 1004, false) );
        //    { "040", new MapAttribute("040map.map", 2, 1004, false) );
        //    { "050", new MapAttribute("050map.map", 2, 1004, false) );
        //    { "060", new MapAttribute("060map.map", 2, 1004, false) );
        //    { "070", new MapAttribute("070map.map", 2, 1004, false) );
        //    { "080", new MapAttribute("080map.map", 2, 1004, false) );


        //    { "ItemVillage", new MapAttribute("ivillage.map", 1, 1001, true) );
        //    { "ItemPigmy1", new MapAttribute("ipigmy1.map", 1, 1001, true) );
        //    { "ItemPigmy2", new MapAttribute("ipigmy2.map", 1, 1001, true) );
        //    { "ItemPigmy3", new MapAttribute("ipigmy3.map", 1, 1001, true) );
        //    { "ItemZomby1", new MapAttribute("izomby1.map", 1, 1001, true) );
        //    { "ItemZomby2", new MapAttribute("izomby2.map", 1, 1001, true) );
        //    { "ItemZomby3", new MapAttribute("izomby3.map", 1, 1001, true) );
        //    { "ItemSkel1", new MapAttribute("iskel1.map", 1, 1001, true) );
        //    { "ItemSkel2", new MapAttribute("iskel2.map", 1, 1001, true) );
        //    { "ItemSkel3", new MapAttribute("iskel3.map", 1, 1001, true) );
        //    { "ItemButcher1", new MapAttribute("ibut1.map", 1, 1001, true) );
        //    { "ItemButcher2", new MapAttribute("ibut2.map", 1, 1001, true) );
        //    { "ItemButcher3", new MapAttribute("ibut3.map", 1, 1001, true) );
        //    { "ItemMummy1", new MapAttribute("imummy1.map", 1, 1001, true) );
        //    { "ItemMummy2", new MapAttribute("imummy2.map", 1, 1001, true) );
        //    { "ItemMummy3", new MapAttribute("imummy3.map", 1, 1001, true) );
        //    { "ItemStone1", new MapAttribute("istone1.map", 1, 1001, true) );
        //    { "ItemStone2", new MapAttribute("istone2.map", 1, 1001, true) );
        //    { "ItemStone3", new MapAttribute("istone3.map", 1, 1001, true) );
        //    { "ItemStone4", new MapAttribute("istone4.map", 1, 1001, true) );
        //    { "ItemDummy1", new MapAttribute("imummy1.map", 1, 1001, true) );
        //    { "ItemDummy2", new MapAttribute("imummy2.map", 1, 1001, true) );
        //    { "ItemDummy3", new MapAttribute("imummy3.map", 1, 1001, true) );
        //    { "ItemDummy4", new MapAttribute("istone3.map", 1, 1001, true) );
        //    { "ItemDummy5", new MapAttribute("istone4.map", 1, 1001, true) );
        //    { "ItemWarDummy1", new MapAttribute("imummy1.map", 1, 1001, true) );
        //    { "ItemWarDummy2", new MapAttribute("imummy2.map", 1, 1001, true) );
        //    { "ItemWarDummy3", new MapAttribute("imummy3.map", 1, 1001, true) );
        //    { "ItemWarDummy4", new MapAttribute("istone3.map", 1, 1001, true) );
        //    { "ItemWarDummy5", new MapAttribute("istone4.map", 1, 1001, true) );
        //    { "ItemHardboil1", new MapAttribute("imummy1.map", 1, 1001, true) );
        //    { "ItemHardboil2", new MapAttribute("imummy2.map", 1, 1001, true) );
        //    { "ItemHardboil3", new MapAttribute("imummy3.map", 1, 1001, true) );
        //    { "ItemHardboil4", new MapAttribute("istone3.map", 1, 1001, true) );
        //    { "ItemHardboil5", new MapAttribute("istone4.map", 1, 1001, true) );
        //    { "ItemGolem1", new MapAttribute("imummy1.map", 1, 1001, true) );
        //    { "ItemGolem2", new MapAttribute("imummy2.map", 1, 1001, true) );
        //    { "ItemGolem3", new MapAttribute("imummy3.map", 1, 1001, true) );
        //    { "ItemGolem4", new MapAttribute("istone3.map", 1, 1001, true) );
        //    { "ItemGolem5", new MapAttribute("istone4.map", 1, 1001, true) );


        //    { "Biggun1", new MapAttribute("estart4.map", 1, 1001, true) );
        //    { "Biggun2", new MapAttribute("estart3.map", 1, 1001, true) );

        //    { "Miro1", new MapAttribute("miro1.map", 1, 1001, true) );
        //    { "TreasureLand", new MapAttribute("1000.map", 1, 1001, true) );   


        //    {"Great", new MapAttribute("great.map", 1, 1001, true) );
        //    { "골램", new MapAttribute("golem.map", 2, 1001, true) );
        //    { "Golem1", new MapAttribute("golem1.map", 2, 1001, true) );
        //    { "Golem2", new MapAttribute("golem2.map", 2, 1001, true) );
        //    { "Golem3", new MapAttribute("golem3.map", 2, 1001, true) );
        //    { "Golem12", new MapAttribute("golem12.map", 2, 1001, true) );


        //    { "지구라트1", new MapAttribute("vv1.map", 4, 1001, false) );
        //    { "지구라트2", new MapAttribute("vv2.map", 4, 1001, false) );
        //    { "지구라트3", new MapAttribute("vv3.map", 4, 1001, false) );
        //    { "지구라트4", new MapAttribute("vv4.map", 4, 1001, false) );
        //    { "지구라트5", new MapAttribute("vv5.map", 4, 1001, false) );

        //    { "대전장", new MapAttribute("ebox2x2.map", 1, false) );       //대전장
        //    { "큰대전장", new MapAttribute("ebox4x4.map", 1, false) );       //큰대전장
        //    { "오목", new MapAttribute("ebox4x4.map", 1, false) );       //오목

        //    { "독방", new MapAttribute("prison.map", 1, false) );       
        //    { "축구장", new MapAttribute("esoccer.map", 1, 1001, true) );


        //    { "가디언", new MapAttribute("ps01.map", 1, 1001, true) );
        //    { "가디언1", new MapAttribute("ps02.map", 1, 1001, true) );
        //    { "가디언2", new MapAttribute("ps03.map", 1, 1001, true) );
        //    { "임시", new MapAttribute("temp.Map", 1, 1001, true) );

        //};
        public static Dictionary<string, MapType> Mapsizes1 = new Dictionary<string, MapType>()
        {
            {"bone01.map", MapType.Bone },
            {"erest.map", MapType.LK },
            {"loen.Map", MapType.LK },
            {"anold.Map", MapType.LK },
            
            {"weakly1.map", MapType.LK },
            {"weakly2.map", MapType.LK },
            {"weakly3.map", MapType.LK },
            {"weakly4.map", MapType.LK },
            {"weakly5.map", MapType.LK },
            
            {"skel1.map", MapType.LK },
            {"skel2.map", MapType.LK },
            {"skel3.map", MapType.LK },
            {"skel4.map", MapType.LK },
            {"skel5.map", MapType.LK },

            {"21.map", MapType.LK },
            {"22.map", MapType.LK },
            {"23.map", MapType.LK },
            {"31.map", MapType.LK },
            {"32.map", MapType.LK },
            {"33.map", MapType.LK },
            {"41.map", MapType.LK },
            {"42.map", MapType.LK },
            {"43.map", MapType.LK },
            {"51.map", MapType.LK },
            {"52.map", MapType.LK },
            {"53.map", MapType.LK },
            
            {"elevel.map", MapType.LK },
            {"010map.map", MapType.LK },
            {"020map.map", MapType.LK },
            {"030map.map", MapType.LK },
            {"040map.map", MapType.LK },
            {"050map.map", MapType.LK },
            {"060map.map", MapType.LK },
            {"061map.map", MapType.LK },
            {"070map.map", MapType.LK },
            {"071map.map", MapType.LK },
            {"080map.map", MapType.LK },
            {"081map.map", MapType.LK },

            {"ivillage.map", MapType.LK },
            {"ipigmy1.map", MapType.LK },
            {"ipigmy2.map", MapType.LK },
            {"ipigmy3.map", MapType.LK },
            {"izomby1.map", MapType.LK },
            {"izomby2.map", MapType.LK },
            {"izomby3.map", MapType.LK },
            {"iskel1.map", MapType.LK },
            {"iskel2.map", MapType.LK },
            {"iskel3.map", MapType.LK },
            {"ibut1.map", MapType.LK },
            {"ibut2.map", MapType.LK },
            {"ibut3.map", MapType.LK },
            {"imummy1.map", MapType.LK },
            {"imummy2.map", MapType.LK },
            {"imummy3.map", MapType.LK },
            {"istone1.map", MapType.LK },
            {"istone2.map", MapType.LK },
            {"istone3.map", MapType.LK },
            {"istone4.map", MapType.LK },

            {"estart.map", MapType.LK },
            {"estart1.map", MapType.LK },
            {"estart2.map", MapType.LK },
            {"estart3.map", MapType.LK },
            {"estart4.map", MapType.LK },

            {"cave.map", MapType.Weak },
            {"cave1.map", MapType.Weak },
            {"cave2.map", MapType.Weak },
            {"cave3.map", MapType.Weak },

            {"golem.map", MapType.LK },
            {"golem1.map", MapType.LK },
            {"golem2.map", MapType.LK },
            {"golem3.map", MapType.LK },
            {"golem12.map", MapType.LK },
            
            {"great.map", MapType.LK },
            {"miro1.map", MapType.LK },
            {"Viking.map", MapType.LK },
            {"1000.map", MapType.LK },

            {"miner0.map", MapType.Weak },
            {"miner1.map", MapType.Weak},
            {"miner2.map", MapType.Weak},
            {"miner3.map", MapType.Weak },

            {"qweak.map", MapType.LK },
            {"qweak1.map", MapType.Weak },
            {"qweak2.map", MapType.Weak },
            {"qweak3.map", MapType.Weak },
            {"qweak4.map", MapType.Weak },
            {"qweak5.map", MapType.Weak },

            {"vv1.map", MapType.VV },
            {"vv2.map", MapType.VV },
            {"vv3.map", MapType.VV },
            {"vv4.map", MapType.VV },
            {"vv5.map", MapType.VV },

            {"ps01.map", MapType.PS },
            {"ps02.map", MapType.PS },            
            {"ps03.map", MapType.PS },

            {"ebox2x2.map", MapType.LK },
            {"ebox4x4.map", MapType.LK },
            {"prison.map", MapType.PS },
            {"esoccer.map", MapType.LK },
            {"sell.map", MapType.LK },
        };

        public static Dictionary<string, TiledMap> loadedmaps = new Dictionary<string, TiledMap>();             // 맵파일 정보 저장


        public static Dictionary<string, bool[,]> LKObjectList = new Dictionary<string, bool[,]>();
        public static Dictionary<string, bool[,]> VVObjectList = new Dictionary<string, bool[,]>();
        public static Dictionary<string, bool[,]> PSObjectList = new Dictionary<string, bool[,]>();
        public static Dictionary<string, bool[,]> WeakObjectList = new Dictionary<string, bool[,]>();
        public static Dictionary<string, bool[,]> BoneObjectList = new Dictionary<string, bool[,]>();


        public static Point2D SpawnPoint(int Channel, string map)
        {
            Point2D temp = null;
            var nmap = model.Map.FullMaps[Channel].Where(xe => xe.Key == map).FirstOrDefault().Value;
            TiledMap curmap;
            loadedmaps.TryGetValue(nmap.Filename, out curmap);

            if (curmap == null)
            {
                Console.WriteLine(string.Format("맵을 찾을수 없습니다.:{0}", map));
                return null;
            }

            var randomtile = curmap.tiles[Util.RandomMinMax(0, loadedmaps[nmap.Filename].sizeX - 1), Util.RandomMinMax(0, loadedmaps[nmap.Filename].sizeY - 1)];
            if (randomtile.IsWall == false && randomtile.SpawnP == false)
            {
                temp = new Point2D(randomtile.X, randomtile.Y);
                //temp.X = randomtile.X;
                //temp.Y = randomtile.Y;
            }

            return temp;
        }
        public static Point2D SpawnPointFixed(int Channel, string map)
        {
            Point2D temp = null;
            var nmap = model.Map.FullMaps[Channel].Where(xe => xe.Key == map).FirstOrDefault().Value;
            TiledMap curmap;
            loadedmaps.TryGetValue(nmap.Filename, out curmap);

            if (curmap == null)
            {
                Console.WriteLine(string.Format("맵을 찾을수 없습니다.:{0}", map));
                return null;
            }

            var randomtile = curmap.tiles[Util.RandomMinMax(0, loadedmaps[nmap.Filename].sizeX - 1), Util.RandomMinMax(0, loadedmaps[nmap.Filename].sizeY - 1)];
            if (randomtile.IsWall == false)
            {
                temp = new Point2D(0, 0);
                temp.X = randomtile.X;
                temp.Y = randomtile.Y;
            }

            return temp;
        }
        public static Point2D SpawnPointFixed(int Channel, string map, int x, int y, int Range)
        {
            Point2D temp = null;
            var nmap = LKCamelot.model.Map.FullMaps[Channel].Where(xee => xee.Key == map).FirstOrDefault().Value;
            TiledMap curmap;
            loadedmaps.TryGetValue(nmap.Filename, out curmap);

            if (curmap == null)
            {
                Console.WriteLine(string.Format("맵을 찾을수 없습니다.:{0}", map));
                return null;
            }

            int MinX = x < Range ? 0 : x - Range;
            int MinY = y < Range ? 0 : y - Range;

            int MaxX = x + Range >= curmap.sizeX ? curmap.sizeX - 1 : x + Range;
            int MaxY = y + Range >= curmap.sizeY ? curmap.sizeY - 1 : y + Range;

            var randomtile = curmap.tiles[Util.RandomMinMax(MinX, MaxX), Util.RandomMinMax(MinY, MaxY)];
            if (randomtile.IsWall == false)
            {
                temp = new Point2D(0, 0);
                temp.X = randomtile.X;
                temp.Y = randomtile.Y;
            }

            return temp;
        }
        public static Point2D SpawnPointFixed(int Channel, string map, int xe, int ye, int xe2, int ye2)
        {
            Point2D temp = null;
            var nmap = LKCamelot.model.Map.FullMaps[Channel].Where(xee => xee.Key == map).FirstOrDefault().Value;
            TiledMap curmap;
            loadedmaps.TryGetValue(nmap.Filename, out curmap);

            if (curmap == null)
            {
                Console.WriteLine(string.Format("맵을 찾을수 없습니다.:{0}", map));
                return null;
            }

            var randomtile = curmap.tiles[Util.RandomMinMax(xe, xe2), Util.RandomMinMax(ye, ye2)];
            if (randomtile.IsWall == false)
            {
                temp = new Point2D(0, 0);
                temp.X = randomtile.X;
                temp.Y = randomtile.Y;
            }

            return temp;
        }

        public static void MapInit1()
        {
            string[] filePaths = Directory.GetFiles(Settings.rootPath + "map");
            foreach (var file in filePaths)
            {
                string[] mapa = file.Split('\\');
                var mapn = mapa[mapa.Length - 1];                       // 맵이름 추출
                if (Mapsizes1.ContainsKey(mapn))
                {
                    TiledMap tile;
                    using (FileStream stream = File.OpenRead(file))
                    using (BinaryReader reader = new BinaryReader(stream, Encoding.Default))
                    {
                        reader.ReadChars(16);
                        reader.ReadChars(16);

                        int mWidth = reader.ReadInt32();
                        int mHeight = reader.ReadInt32();

                        reader.ReadChars(16);
                        reader.ReadChars(16);
                        int obj = reader.ReadInt32();             //오브젝트 갯수

                        tile = new TiledMap(mWidth, mHeight, Mapsizes1[mapn], obj);


                        for (int i = 0; i < mHeight; i++)
                        {
                            for (int j = 0; j < mWidth; j++)
                            {
                                byte[] byteTile = reader.ReadBytes(4);
                                var tempp = new MyPathNode(j, i);

                                if (Mapsizes1[mapn] == MapType.LK)
                                {
                                    if (byteTile[0] == 0x03
                                        || byteTile[0] == 0x05
                                        || byteTile[0] == 0x09
                                        || (byteTile[0] == 0x02 && byteTile[2] == 0x03)
                                        || byteTile[0] == 0x6A
                                        || byteTile[0] == 0x6B
                                        || byteTile[0] == 0x6D
                                        || byteTile[0] == 0x6E
                                        || byteTile[0] == 0x6F
                                        || ((byteTile[0] == 0x6C || byteTile[0] == 0x72) && byteTile[3] != 01)
                                        || byteTile[0] == 0x66
                                        || byteTile[0] == 0x67
                                        || byteTile[0] == 0x68
                                        )
                                        tempp.IsWall = true;
                                }
                                else if (Mapsizes1[mapn] == MapType.VV)
                                {
                                    if (byteTile[0] == 0 || (byteTile[0] == 1 && (byteTile[2] == 4 || byteTile[2] == 6 || byteTile[2] == 7 || byteTile[2] == 8)))
                                        tempp.IsWall = true;
                                }
                                else if (Mapsizes1[mapn] == MapType.PS)
                                {
                                    if (byteTile[0] == 0x01 && (byteTile[2] == 0x01 || byteTile[2] == 0x02 || byteTile[2] == 0x03 || byteTile[2] == 0x04 || byteTile[2] == 0x07))
                                        tempp.IsWall = true;

                                }
                                else if (Mapsizes1[mapn] == MapType.Weak)
                                {
                                    if (byteTile[0] == 0x01 && (byteTile[2] == 0x01 || byteTile[2] == 0x02 || byteTile[2] == 0x03 || byteTile[2] == 0x04 || byteTile[2] == 0x07)
                                        || byteTile[0] == 2 || byteTile[0] == 3 || byteTile[0] == 4)
                                        tempp.IsWall = true;
                                }
                                else if (Mapsizes1[mapn] == MapType.Bone)
                                {
                                    if (byteTile[0] == 1 && (byteTile[2] == 0x00 || byteTile[2] == 0x05 || byteTile[2] == 0x06 || byteTile[2] == 0x07 || byteTile[2] == 0x08))
                                        tempp.IsWall = true;
                                }

                                tile.tiles[j, i] = tempp;
                            }
                        }

                        for (int z = 0; z < obj; z++)
                        {

                            int objx = reader.ReadInt32();
                            int objy = reader.ReadInt32();

                            byte[] name = reader.ReadBytes(8);
                            string str = Encoding.Default.GetString(name, 0, name.Length).Split('\0')[0];
                            byte[] objname = reader.ReadBytes(8);
                            string objstr = Encoding.Default.GetString(objname, 0, objname.Length).Split('\0')[0];

                            reader.ReadInt32();
                            reader.ReadInt32();

                            bool[,] node = null;
                            if (Mapsizes1[mapn] == MapType.VV)
                                VVObjectList.TryGetValue(objstr, out node);
                            if (Mapsizes1[mapn] == MapType.LK)
                                LKObjectList.TryGetValue(objstr, out node);
                            if (Mapsizes1[mapn] == MapType.PS)
                                PSObjectList.TryGetValue(objstr, out node);
                            if (Mapsizes1[mapn] == MapType.Weak)
                                WeakObjectList.TryGetValue(objstr, out node);
                            if (Mapsizes1[mapn] == MapType.Bone)
                                BoneObjectList.TryGetValue(objstr, out node);

                            if (node == null)
                            {
                                if (Mapsizes1[mapn] == MapType.VV)
                                    node = VVObjectList.Where(xe => xe.Key.Contains(objstr)).FirstOrDefault().Value;
                                if (Mapsizes1[mapn] == MapType.LK)
                                    node = LKObjectList.Where(xe => xe.Key.Contains(objstr)).FirstOrDefault().Value;
                                if (Mapsizes1[mapn] == MapType.PS)
                                    node = PSObjectList.Where(xe => xe.Key.Contains(objstr)).FirstOrDefault().Value;
                                if (Mapsizes1[mapn] == MapType.Weak)
                                    node = WeakObjectList.Where(xe => xe.Key.Contains(objstr)).FirstOrDefault().Value;
                                if (Mapsizes1[mapn] == MapType.Bone)
                                    node = BoneObjectList.Where(xe => xe.Key.Contains(objstr)).FirstOrDefault().Value;
                            }



                            if (node != null)
                            {
                                for (int i = 0; i < node.GetLength(0); i++)
                                {
                                    for (int j = 0; j < node.GetLength(1); j++)
                                    {
                                        try
                                        {
                                            int posx = objx + j;
                                            int posy = objy - i;
                                            if (posx >= tile.sizeX || posx < 0 || posy >= tile.sizeY || posy < 0)
                                                continue;

                                            if (tile.tiles[posx, posy].IsWall == false)
                                                tile.tiles[posx, posy].IsWall = node[node.GetLength(0) - 1 - i, j];
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(mapn + " " + objstr + " " + objx + " " + objy + " " + e);
                                            continue;
                                        }
                                    }
                                }
                            }
                        }
                        loadedmaps.Add(mapn, tile);
                    }
                }
            }
        }





        public static void ObjInit()
        {
            Load("VV.Obj");
            Load("psobj.obj");
            Load("weakobj.obj");
            Load("boneobj.Obj");
            Load("LkObj.obj");
        }
        public static void Load(string str)
        {
            using (FileStream stream = File.OpenRead(Settings.rootPath + "\\Obj\\" + str))
            using (BinaryReader reader = new BinaryReader(stream, Encoding.Default))
            {
                string Type = new string(reader.ReadChars(8));             // 타입
                int amount = reader.ReadInt32();              // 갯수

                for (int i = 1; i <= amount; i++)
                {
                    //공백 읽기
                    if (i != 1)
                        while (reader.PeekChar() == 0x01 || reader.PeekChar() == 0x00)
                            reader.ReadByte();


                    byte[] ObjName = reader.ReadBytes(16);
                    string FileName = Encoding.Default.GetString(ObjName, 0, ObjName.Length).Split('\0')[0];

                    int TileWidthAmount = reader.ReadInt32();           // 타일 가로 갯수
                    int TileHeightAmount = reader.ReadInt32();          // 타일 세로 갯수

                    int width = reader.ReadInt32();                     // 이미지으 넓이
                    int height = reader.ReadInt32();                    // 이미지의 높이

                    int offsetX = reader.ReadInt32();         //gap
                    int offsetY = reader.ReadInt32();         //gap??

                    reader.ReadBytes(height * width * 2);

                    int x = 1, y = 1;
                    for (x = 1; x * 32 < width; x++) { }
                    for (y = 1; y * 24 < height; y++) { }

                    bool[,] Node = new bool[y, x];
                    if (str == "VV.Obj")
                    {
                        VVBlack(FileName, ref Node);
                        VVObjectList.Add(FileName, Node);
                    }
                    else if (str == "psobj.obj")
                    {
                        PSBlack(FileName, ref Node);
                        PSObjectList.Add(FileName, Node);
                    }
                    else if (str == "weakobj.obj")
                    {
                        WeakBlack(FileName, ref Node);
                        WeakObjectList.Add(FileName, Node);
                    }
                    else if (str == "boneobj.Obj")
                    {
                        BoneBlack(FileName, ref Node);
                        BoneObjectList.Add(FileName, Node);
                    }
                    else if (str == "LkObj.obj")
                    {
                        LKBlack(FileName, ref Node);
                        LKObjectList.Add(FileName, Node);
                    }

                }
            }
        }


        public static void VVBlack(string Name, ref bool[,] Node)
        {
            // [Row, Column]
            Node[Node.GetLength(0) - 1, 0] = true;
            Node[Node.GetLength(0) - 2, 0] = true;

            if (Name.Contains("기둥"))
            {
                Node[Node.GetLength(0) - 1, 1] = true;
                Node[Node.GetLength(0) - 2, 1] = true;
            }
            else if (Name.Contains("석상"))
            {
                Node[Node.GetLength(0) - 1, 1] = true;
            }
            else if (Name.Contains("문오른2"))
            {
            }
            //else if (Name.Contains("계단") || Name.Contains("벽") || Name == "구석위")
            //{
            //    if (Name == "계단올라3")
            //    {

            //    }
            //    for (int i = 0; i < Node.GetLength(0); i++)
            //        for (int j = 0; j < Node.GetLength(1); j++)
            //            Node[i, j] = true;
            //}
            else
            {
                for (int i = 0; i < Node.GetLength(0); i++)
                    for (int j = 0; j < Node.GetLength(1); j++)
                        Node[i, j] = true;
            }
        }
        public static void PSBlack(string Name, ref bool[,] Node)
        {
            Node[Node.GetLength(0) - 1, 0] = true;
            Node[Node.GetLength(0) - 2, 0] = true;
            if (Name.Contains("침대"))
            {
                Node[Node.GetLength(0) - 1, 1] = true;
                Node[Node.GetLength(0) - 2, 1] = true;
                Node[Node.GetLength(0) - 3, 1] = true;
                Node[Node.GetLength(0) - 2, 2] = true;
                Node[Node.GetLength(0) - 3, 2] = true;
            }
        }
        public static void WeakBlack(string Name, ref bool[,] Node)
        {
            for (int i = 0; i < Node.GetLength(0); i++)
                for (int j = 0; j < Node.GetLength(1); j++)
                    Node[i, j] = true;

            #region 첫번째 줄 제외
            if (Name.Contains("석순") ||
                Name == "중왼위끝" || Name == "중왼위2끝" || Name == "중왼위" || Name == "중왼아래끝" || Name == "중왼위" || Name == "중왼아래끝" || Name == "중왼아래2끝" || Name == "중왼아래" ||
                Name == "중오위끝" || Name == "중오위2끝" || Name == "중오위" || Name == "중오아래끝" || Name == "중오아래2끝" || Name == "중오아래" ||

                Name == "왼위담끝" || Name == "왼위담2끝" || Name == "왼아래담" || Name == "왼아래담끝" || Name == "왼위담" || Name == "왼아래담2끝" || Name == "왼아래담" || Name == "왼아래담2끝" || Name == "왼아래담" ||
                Name == "오위담끝" || Name == "오위담2끝" || Name == "오위담" || Name == "오아래담끝" || Name == "오아래담" || Name == "오아래담2끝" || Name == "오아래담" ||

                Name == "중곡선왼" || Name == "중곡선2왼2" || Name == "중곡2왼" || Name == "왼쪽곡선" || Name == "왼곡3" || Name == "왼곡2" ||
                Name == "중곡선오" || Name == "중곡2오" || Name == "오른쪽곡선" || Name == "오른쪽곡선" || Name == "오곡3" || Name == "오곡2" ||

                Name == "높곡선아래" || Name == "높곡선오" || Name == "높곡선왼" || Name == "높곡선위" ||
                Name == "높오곡2" || Name == "높왼곡2" ||
                Name == "높오아래" || Name == "높오아래끝" || Name == "높오아래끝2" ||
                Name == "높오위" || Name == "높오위끝" || Name == "높왼끝2" || Name == "높왼위" || Name == "높왼아래" || Name == "높왼아래끝" ||
                Name == "높왼위" || Name == "높왼위끝" || Name == "높왼위끝2" ||

                Name == "중T1" || Name == "중오위T"

                //옾오아래끝, 높오아래끝2, 높오위끝 , 높왼아래, 높왼아래끝
                )
            {
                for (int j = 0; j < Node.GetLength(1) - 1; j++)
                    Node[0, j] = false;
            }
            #endregion

            #region 두번째 줄 제외
            if (
                Name == "중왼위끝" || Name == "중왼위2끝" || Name == "중왼위" || Name == "중왼아래끝" || Name == "중왼위" || Name == "중왼아래끝" || Name == "중왼아래2끝" || Name == "중왼아래" ||
                Name == "중오위끝" || Name == "중오위2끝" || Name == "중오위" || Name == "중오아래끝" || Name == "중오아래2끝" || Name == "중오아래" ||

                Name == "왼위담끝" || Name == "왼위담2끝" || Name == "왼아래담" || Name == "왼아래담끝" || Name == "왼위담" || Name == "왼아래담2끝" || Name == "왼아래담" || Name == "왼아래담2끝" || Name == "왼아래담" ||
                Name == "오위담끝" || Name == "오위담2끝" || Name == "오위담" || Name == "오아래담끝" || Name == "오아래담" || Name == "오아래담2끝" || Name == "오아래담" ||

                Name == "중곡선왼" || Name == "중곡선2왼2" || Name == "중곡2왼" || Name == "왼쪽곡선" || Name == "왼곡3" || Name == "왼곡2" ||
                Name == "중곡선오" || Name == "중곡2오" || Name == "오른쪽곡선" || Name == "오른쪽곡선" || Name == "오곡3" || Name == "오곡2" ||

                Name == "높곡선아래" || Name == "높곡선위" || Name == "높곡선왼" || Name == "높곡선오" ||

                Name == "높오아래" || Name == "높오아래끝" || Name == "높오아래끝2" ||
                Name == "높오위" || Name == "높오위끝" || Name == "높왼끝2" || Name == "높왼위" || Name == "높왼아래" || Name == "높왼아래끝" ||
                Name == "높왼위" || Name == "높왼위끝" || Name == "높왼위끝2"
                )
            {
                for (int j = 0; j < Node.GetLength(1) - 1; j++)
                    Node[1, j] = false;
            }
            #endregion

            #region 왼쪽 세로
            if (Name == "")
            {
                for (int i = 0; i < Node.GetLength(0); i++)
                    Node[i, 0] = false;
            }
            #endregion

            #region 오른쪽 세로

            if (Name == "동굴이동문" || Name == "동굴이동문출")
            {
                for (int i = 0; i < Node.GetLength(0); i++)
                    Node[i, Node.GetLength(1) - 1] = false;
            }
            #endregion

            #region 기타
            if (Name == "석순3")
            {
                Node[1, Node.GetLength(1) - 1] = true;
                Node[1, Node.GetLength(1) - 2] = true;
            }
            else if (Name == "동굴이동문")
            {
                Node[Node.GetLength(0) - 1, 0] = false;
                Node[Node.GetLength(0) - 2, 1] = false;
            }

            else if (Name == "동굴이동문출")
            {
                Node[Node.GetLength(0) - 1, Node.GetLength(1) - 2] = false;
                Node[Node.GetLength(0) - 2, Node.GetLength(1) - 2] = false;
                Node[Node.GetLength(0) - 1, Node.GetLength(1) - 3] = false;
                Node[Node.GetLength(0) - 2, Node.GetLength(1) - 3] = false;
            }
            else if (Name == "중왼위끝" || Name == "중왼위2끝" || Name == "중왼위" || Name == "중오아래끝" || Name == "중오아래2끝" || Name == "중오아래"
                 || Name == "왼위담끝" || Name == "왼위담2끝" || Name == "왼위담" || Name == "오아래담끝" || Name == "오아래담2끝" || Name == "오아래담")
                Node[1, Node.GetLength(1) - 1] = true;
            else if (Name == "중왼아래끝" || Name == "중왼아래2끝" || Name == "중왼아래" || Name == "중오위끝" || Name == "중오위2끝" || Name == "중오위"
                 || Name == "왼아래담끝" || Name == "왼아래담2끝" || Name == "왼아래담" || Name == "오위담끝" || Name == "오위담2끝" || Name == "오위담")
                Node[1, 0] = true;

            else if (Name == "왼쪽곡선")
                Node[Node.GetLength(0) - 1, 0] = false;
            else if (Name == "오른쪽곡선")
                Node[Node.GetLength(0) - 1, Node.GetLength(1) - 1] = false;

            else if (Name == "높곡선아래")
            {
                Node[2, 1] = false;
                Node[2, 2] = false;
                Node[2, 3] = false;
            }
            else if (Name == "높곡선위")
            {
                Node[2, 0] = false;
                Node[2, Node.GetLength(1) - 1] = false;
            }

            else if (Name == "높곡선왼")
            {
                Node[1, Node.GetLength(1) - 1] = true;
                Node[Node.GetLength(0) - 1, 0] = false;
                Node[Node.GetLength(0) - 1, 1] = false;
                Node[Node.GetLength(0) - 1, 2] = false;
                Node[Node.GetLength(0) - 2, 0] = false;
            }
            else if (Name == "높곡선오")
            {
                Node[1, 0] = true;
                Node[Node.GetLength(0) - 1, 0] = true;
                Node[Node.GetLength(0) - 1, Node.GetLength(1) - 1] = false;
                Node[Node.GetLength(0) - 1, Node.GetLength(1) - 2] = false;
                Node[Node.GetLength(0) - 1, Node.GetLength(1) - 3] = false;
                Node[Node.GetLength(0) - 2, Node.GetLength(1) - 1] = false;
            }
            else if (Name == "높오아래" || Name == "높오아래끝" || Name == "높오아래끝2" || Name == "높왼위" || Name == "높왼위끝" || Name == "높왼위끝2")
            {
                Node[2, 0] = false;
            }
            else if (Name == "높오위" || Name == "높오위끝" || Name == "높오위끝2" || Name == "높왼끝2" || Name == "높왼위" || Name == "높왼아래" || Name == "높왼아래끝")
            {
                Node[2, Node.GetLength(1) - 1] = false;
            }
            else if (Name == "중T1")
            {
                Node[1, 0] = false;
                Node[1, 1] = false;
                Node[2, 0] = false;
            }
            else if (Name == "중오위T")
            {
                Node[1, Node.GetLength(1) - 1] = false;
                Node[1, Node.GetLength(1) - 2] = false;
                Node[2, Node.GetLength(1) - 1] = false;
            }
            #endregion
        }
        public static void BoneBlack(string Name, ref bool[,] Node)
        {
            //Node[Row, Column]
            if (Name == "뼈다01_02" || Name == "뼈다01_03")
            {
                Node[Node.GetLength(0) - 1, Node.GetLength(1) - 1] = true;
                Node[Node.GetLength(0) - 2, Node.GetLength(1) - 1] = true;
                Node[Node.GetLength(0) - 3, Node.GetLength(1) - 1] = true;
                if (Name == "뼈다01_03")
                    Node[Node.GetLength(0) - 4, Node.GetLength(1) - 1] = true;
            }
            else if (Name == "뼈다02_01" || Name == "뼈다02_02")
            {
                Node[0, Node.GetLength(1) - 1] = true;
                Node[0, Node.GetLength(1) - 1] = true;
                Node[0, Node.GetLength(1) - 1] = true;
                if (Name == "뼈다02_01")
                    Node[Node.GetLength(0) - 4, Node.GetLength(1) - 1] = true;
            }



            //벽, 벽낮, 벽낮모, 뼈기둥, 

            else
            {
                for (int i = 0; i < Node.GetLength(0); i++)
                    for (int j = 0; j < Node.GetLength(1); j++)
                        Node[i, j] = true;

            }
        }
        public static void LKBlack(string Name, ref bool[,] Node)
        {
            if (Name == "나무01" || Name == "나무04" || Name == "나무05" || Name == "나무07" || Name == "나무08" || Name == "나무09" || Name == "나무10" || Name == "나무11" || Name == "나무13")
                Node[Node.GetLength(0) - 1, 2] = true;

            else if (Name == "나무02" || Name == "나무12")
                Node[Node.GetLength(0) - 1, 3] = true;
            else if (Name == "나무06")
                Node[Node.GetLength(0) - 1, 2] = true;

            else if (Name == "나무03")
                Node[Node.GetLength(0) - 1, 1] = true;

            else if (Name == "나무14")
            {
                Node[Node.GetLength(0) - 1, 0] = true;
                Node[Node.GetLength(0) - 2, 0] = true;

                Node[Node.GetLength(0) - 2, 1] = true;
                Node[Node.GetLength(0) - 3, 1] = true;

                Node[Node.GetLength(0) - 2, 2] = true;
                Node[Node.GetLength(0) - 3, 2] = true;
                Node[Node.GetLength(0) - 4, 2] = true;

            }
            else
            {
                for (int i = 0; i < Node.GetLength(0); i++)
                    for (int j = 0; j < Node.GetLength(1); j++)
                        Node[i, j] = true;

                #region 첫번째 줄 제외
                if (Name == "담돌01" || Name == "담돌02" || Name == "담돌03" || Name == "담돌04" || Name == "담돌05" || Name == "담돌06" || Name == "담돌07" || Name == "담돌08" ||
                    Name == "담울07" || Name == "담울10" || Name == "담울11" || Name == "담울12" || Name == "담울13" || Name == "담울14" ||
                    Name == "담울15" || Name == "담울16" ||
                    Name == "담울17" || Name == "담울18" || Name == "담울19" ||

                    Name == "집건물" || Name == "집시약상" || Name == "집대장간" ||
                    Name == "돌01" || Name == "돌02" || Name == "돌04" || Name == "돌05" || Name == "돌06" || Name == "돌07" || Name == "돌08" || Name == "돌09" || Name == "돌10" ||
                    Name == "돌11" || Name == "돌12" || Name == "돌13" || Name == "돌14" || Name == "돌15" || Name == "돌16" || Name == "돌17" || Name == "돌18" || Name == "돌19" || Name == "돌20" ||
                    Name == "돌20" || Name == "돌21" || Name == "돌22" || Name == "돌23" ||
                    Name == "로엔06" ||

                    Name == "입구02"
                    )
                {
                    for (int j = 0; j < Node.GetLength(1); j++)
                        Node[0, j] = false;
                }
                #endregion

                #region 두번째 줄 제외
                if (Name == "담돌06" || Name == "담돌08" ||
                    Name == "담울10" || Name == "담울11" || Name == "담울12" || Name == "담울13" || Name == "담울14" ||
                    Name == "담울15" || Name == "담울16" ||
                    Name == "담울17" || Name == "담울18" || Name == "담울19" ||

                    Name == "집건물" || Name == "집시약상" ||
                    Name == "돌07" || Name == "돌09" || Name == "돌10" ||
                    Name == "돌12" || Name == "돌13" || Name == "돌17" || Name == "돌20")
                {
                    for (int j = 0; j < Node.GetLength(1); j++)
                        Node[1, j] = false;
                }
                #endregion

                #region 3번째 줄 제외
                if (Name == "집시약상" ||
                    Name == "담돌06" || Name == "담돌08" ||
                    Name == "담울13" || Name == "담울14" ||
                    Name == "돌12")
                {
                    for (int j = 0; j < Node.GetLength(1); j++)
                        Node[2, j] = false;
                }
                #endregion

                #region 왼쪽 세로
                if (Name == "담울05" ||
                    Name == "집건물" || Name == "집시약상" || Name == "집대장간" || Name == "집아놀드")
                {
                    for (int i = 0; i < Node.GetLength(0); i++)
                        Node[i, 0] = false;
                }
                #endregion

                #region 오른쪽 세로

                if (Name == "집시약상" ||
                    Name == "담돌01" || Name == "담돌03" || Name == "담돌04" || Name == "담돌05" || Name == "담돌07" || Name == "담돌08" ||
                    Name == "담울04" || Name == "담울10" || Name == "담울11" || Name == "담울12" || Name == "담울13" ||
                    Name == "담울17" || Name == "담울18" || Name == "담울19" ||
                    Name == "집건물" || Name == "집대장간" || Name == "집아놀드" ||
                    Name == "돌10" || Name == "돌12" || Name == "돌13" || Name == "돌17" || Name == "돌18" || Name == "돌19" || Name == "돌20" ||
                    Name == "돌22" ||
                    Name == "입구01" || Name == "입구02" || Name == "입구03")
                {
                    for (int i = 0; i < Node.GetLength(0); i++)
                        Node[i, Node.GetLength(1) - 1] = false;
                }
                #endregion

                #region 기타
                if (Name == "집건물")
                {
                    Node[Node.GetLength(0) - 1, 2] = false;
                    Node[Node.GetLength(0) - 2, 2] = false;
                }
                else if (Name == "집대장간")
                {
                    for (int i = 0; i < Node.GetLength(0); i++)
                        Node[i, Node.GetLength(1) - 2] = false;
                    #region 문
                    Node[Node.GetLength(0) - 1, 7] = false;
                    Node[Node.GetLength(0) - 2, 7] = false;
                    Node[Node.GetLength(0) - 3, 7] = false;
                    #endregion

                    #region 창고
                    Node[Node.GetLength(0) - 1, 1] = false;
                    Node[Node.GetLength(0) - 1, 2] = false;
                    Node[Node.GetLength(0) - 1, 3] = false;
                    Node[Node.GetLength(0) - 2, 2] = false;
                    Node[Node.GetLength(0) - 2, 3] = false;
                    Node[Node.GetLength(0) - 3, 2] = false;
                    Node[Node.GetLength(0) - 3, 3] = false;
                    Node[Node.GetLength(0) - 4, 2] = false;
                    Node[Node.GetLength(0) - 4, 3] = false;
                    #endregion

                    #region 왼쪽 지붕
                    Node[1, 1] = false;
                    Node[1, 2] = false;
                    Node[1, 3] = false;
                    Node[1, 4] = false;
                    Node[1, 5] = false;
                    Node[2, 1] = false;
                    Node[2, 2] = false;
                    Node[2, 3] = false;
                    Node[2, 4] = false;
                    Node[3, 1] = false;
                    #endregion

                    #region 오른쪽 지붕
                    Node[1, Node.GetLength(1) - 3] = false;
                    Node[1, Node.GetLength(1) - 4] = false;
                    Node[1, Node.GetLength(1) - 5] = false;
                    Node[1, Node.GetLength(1) - 6] = false;
                    #endregion
                }
                else if (Name == "집아놀드")
                {
                    for (int i = 0; i < Node.GetLength(0); i++)
                        Node[i, Node.GetLength(1) - 2] = false;

                    #region 문
                    Node[Node.GetLength(0) - 1, 2] = false;
                    Node[Node.GetLength(0) - 2, 2] = false;
                    Node[Node.GetLength(0) - 3, 2] = false;
                    #endregion

                }
                else if (Name == "집시약상")
                {
                    for (int i = 0; i < Node.GetLength(1); i++)
                    {
                        Node[3, i] = false;
                    }
                    Node[2, 3] = true;
                    Node[2, 4] = true;
                    Node[3, 3] = true;
                    Node[3, 4] = true;

                    Node[4, 1] = false;
                    Node[4, Node.GetLength(1) - 2] = false;

                    Node[Node.GetLength(0) - 1, 3] = false;
                    Node[Node.GetLength(0) - 2, 3] = false;
                }
                else if (Name == "담울06")
                {
                    Node[0, Node.GetLength(1) - 1] = false;
                }
                else if (Name == "담돌01")
                {
                    Node[Node.GetLength(0) - 1, Node.GetLength(1) - 3] = false;
                    Node[Node.GetLength(0) - 1, Node.GetLength(1) - 2] = false;
                    Node[Node.GetLength(0) - 2, Node.GetLength(1) - 3] = false;
                    Node[Node.GetLength(0) - 2, Node.GetLength(1) - 2] = false;
                }
                else if (Name == "담돌03")
                {
                    Node[Node.GetLength(0) - 1, 0] = false;
                    Node[Node.GetLength(0) - 1, 1] = false;
                }
                else if (Name == "담돌06")
                {
                    Node[0, 0] = true;
                    Node[1, 0] = true;
                    Node[2, 0] = true;
                }
                else if (Name == "담돌08")
                {
                    Node[0, Node.GetLength(1) - 2] = true;
                    Node[1, Node.GetLength(1) - 2] = true;
                    Node[2, Node.GetLength(1) - 2] = true;
                }


                else if (Name == "담울06")
                    Node[0, 0] = true;

                else if (Name == "담울08")
                    Node[0, Node.GetLength(0) - 1] = true;
                else if (Name == "담울13")
                {
                    Node[0, 0] = true;
                    Node[1, 0] = true;
                    Node[2, 0] = true;
                }
                else if (Name == "담울14")
                {
                    Node[0, Node.GetLength(1) - 1] = true;
                    Node[1, Node.GetLength(1) - 1] = true;
                    Node[2, Node.GetLength(1) - 1] = true;
                }
                else if (Name == "담울15" || Name == "담울16")
                {
                    Node[Node.GetLength(0) - 2, 0] = false;
                    Node[Node.GetLength(0) - 1, Node.GetLength(1) - 1] = false;
                }
                else if (Name == "담울19" || Name == "담울18" || Name == "담울17")
                {
                    Node[Node.GetLength(0) - 3, 0] = true;

                    Node[Node.GetLength(0) - 1, 0] = false;
                    Node[Node.GetLength(0) - 2, 2] = false;

                }


                else if (Name == "돌06")
                    Node[0, 1] = true;

                else if (Name == "돌07")
                {
                    Node[1, 0] = true;
                    Node[1, 1] = true;
                }
                else if (Name == "돌09")
                {
                    Node[1, Node.GetLength(1) - 1] = true;
                    Node[1, Node.GetLength(1) - 2] = true;
                }
                else if (Name == "돌23")
                {
                    Node[1, Node.GetLength(1) - 1] = false;
                }

                else if (Name == "입구01")
                {
                    Node[0, 0] = false;
                    Node[0, Node.GetLength(1) - 2] = false;

                    Node[Node.GetLength(0) - 1, 2] = false;
                    Node[Node.GetLength(0) - 2, 2] = false;
                }
                else if (Name == "입구03" || Name == "입구02")
                {
                    Node[1, 0] = false;
                    Node[1, Node.GetLength(1) - 2] = false;

                    Node[Node.GetLength(0) - 1, 2] = false;
                    Node[Node.GetLength(0) - 2, 2] = false;
                }

                else if (Name == "입구04")
                {
                    Node[Node.GetLength(0) - 1, 1] = false;
                    Node[Node.GetLength(0) - 2, 1] = false;
                }
                #endregion
            }
        }
    }
}
