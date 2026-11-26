using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
using MySql.Data.MySqlClient;
using System.Data;
namespace LKCamelot.script.map
{
    public class InnerMap
    {
        public string Map;
        public string Dest;
        //public List<Point2D> Locs;
        public Point2D Locs;
        public Point2D ArriveLoc;

        //public InnerMap(string Vilage, string Map, string Dest, Point2D ArriveLoc, List<Point2D> Locs)
        //{
        //    this.Map = Map;
        //    this.Dest = Dest;
        //    this.Locs = Locs;
        //    this.ArriveLoc = ArriveLoc;
        //}
        public InnerMap(string Vilage, string Map, string Dest, Point2D ArriveLoc, Point2D Locs)
        {
            this.Map = Map;
            this.Dest = Dest;
            this.Locs = Locs;
            this.ArriveLoc = ArriveLoc;
        }
        public InnerMap(string Map, string Dest)
        {
            this.Map = Map;
            this.Dest = Dest;
        }
    }

    public class Portal
    {
        public static List<InnerMap>[] Portals;

        public static void PortalLoad()
        {
            Portals = new List<InnerMap>[Settings.VillageName.Count];

            for (int i = 0; i < Settings.VillageName.Count(); i++)
            {
                Portals[i] = new List<InnerMap>();
            }

            string strConn = Settings.DB_Conn;
            DataSet PortalData = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                string strQuery ="SELECT * FROM portalMaster where DC_YN='FALSE'";
                conn.Open();
                MySqlDataAdapter adpt = new MySqlDataAdapter(strQuery, conn);
                PortalData.Clear();
                adpt.Fill(PortalData, "Portal");
            }

            foreach (DataRow d in PortalData.Tables["Portal"].Rows)
            {
                Point2D FromLoc = new Point2D(Convert.ToInt32(d["From_LocX"]), Convert.ToInt32(d["From_LocY"]));
                Point2D ToLoc = new Point2D(Convert.ToInt32(d["To_LocX"]), Convert.ToInt32(d["To_LocY"]));
                
                int Vlig = Convert.ToInt32(d["VilgNum"]);
                if (Vlig == -1)
                {
                    for (int j = 0; j < Settings.VillageName.Count(); j++)
                        Portals[j].Add(new InnerMap(Settings.VillageName.ElementAt(j), d["From_MAP"].ToString(), d["To_MAP"].ToString(), FromLoc, ToLoc));
                }
                else
                    Portals[Vlig].Add(new InnerMap(Settings.VillageName.ElementAt(Vlig), d["From_MAP"].ToString(), d["To_MAP"].ToString(), FromLoc, ToLoc));
            }



            #region V1

            //Portals = new List<InnerMap>[Settings.VillageName.Count];
            //for (int i = 0; i < Settings.VillageName.Count(); i++)
            //{
            //Portals[i] = new List<InnerMap>();
            //                                                            //출발맵      도착맵          도착지점                             출발지점
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "대장간", "광산0", new Point2D(17, 39), new List<Point2D>() { new Point2D(12, 3) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), Settings.VillageName.ElementAt(i) + ",32", "시약상", new Point2D(6, 14), new List<Point2D>() { new Point2D(139, 155) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "시약상", Settings.VillageName.ElementAt(i) + ",32", new Point2D(139, 157), new List<Point2D>() { new Point2D(5, 15) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), Settings.VillageName.ElementAt(i) + ",32", "대장간", new Point2D(5, 17), new List<Point2D>() { new Point2D(65, 158) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "대장간", Settings.VillageName.ElementAt(i) + ",32", new Point2D(65, 160), new List<Point2D>() { new Point2D(4, 17) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), Settings.VillageName.ElementAt(i) + ",42", "휴게실", new Point2D(19, 14), new List<Point2D>() { new Point2D(61, 114) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "휴게실", Settings.VillageName.ElementAt(i) + ",42", new Point2D(61, 116), new List<Point2D>() { new Point2D(19, 12) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), Settings.VillageName.ElementAt(i) + ",41", "퀘스트", new Point2D(14, 5), new List<Point2D>() { new Point2D(96, 124) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "퀘스트", Settings.VillageName.ElementAt(i) + ",41", new Point2D(96, 126), new List<Point2D>() { new Point2D(14, 3) }));


            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), Settings.VillageName.ElementAt(i) + ",32", "Biggun1", new Point2D(98, 78), new List<Point2D>() { new Point2D(167, 122) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Biggun1", Settings.VillageName.ElementAt(i) + ",32", new Point2D(164, 122), new List<Point2D>() { new Point2D(99, 79) }));


            //#region 가디언
            //                                                              //출발맵      도착맵          도착지점                             출발지점
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "가디언", "가디언1", new Point2D(47, 13), new List<Point2D>() { new Point2D(41, 40) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "가디언1", "가디언", new Point2D(43, 41), new List<Point2D>() { new Point2D(49, 12) }));

            //#endregion


            //#region 빅건
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Biggun1", "Biggun1", new Point2D(5, 5), new List<Point2D>() { new Point2D(14, 17) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Biggun1", "Biggun1", new Point2D(14, 19), new List<Point2D>() { new Point2D(6, 7) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Biggun1", "Biggun2", new Point2D(98, 78), new List<Point2D>() { new Point2D(0, 0) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Biggun2", "Biggun1", new Point2D(1, 1), new List<Point2D>() { new Point2D(99, 79) }));
            //#endregion


            //#region 가보자
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "가보자", "Miro1", new Point2D(1, 1), new List<Point2D>() { new Point2D(30, 21) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Miro1", "가보자", new Point2D(28, 22), new List<Point2D>() { new Point2D(0, 0) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Miro1", "Miro1", new Point2D(52, 156), new List<Point2D>() { new Point2D(74, 96) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Miro1", "Miro1", new Point2D(74, 97), new List<Point2D>() { new Point2D(52, 155) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "가보자", "Start", new Point2D(2, 2), new List<Point2D>() { new Point2D(2, 2) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Start", "가보자", new Point2D(6, 4), new List<Point2D>() { new Point2D(1, 0) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Start", "스콜피온", new Point2D(2, 2), new List<Point2D>() { new Point2D(99, 79) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "스콜피온", "Start", new Point2D(98, 78), new List<Point2D>() { new Point2D(0, 1) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "스콜피온", "스콜피온", new Point2D(5, 5), new List<Point2D>() { new Point2D(14, 17) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "스콜피온", "스콜피온", new Point2D(14, 19), new List<Point2D>() { new Point2D(6, 7) }));



            ////출발맵      도착맵          도착지점                             출발지점            
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "가보자", "골램", new Point2D(15, 1), new List<Point2D>() { new Point2D(49, 0) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "골램", "가보자", new Point2D(43, 5), new List<Point2D>() { new Point2D(15, 0) }));

            //#region 동굴
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "가보자", "동굴1층", new Point2D(10, 8), new List<Point2D>() { new Point2D(44, 49) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "동굴1층", "가보자", new Point2D(41, 42), new List<Point2D>() { new Point2D(9, 6) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "동굴1층", "동굴2층", new Point2D(8, 48), new List<Point2D>() { new Point2D(62, 61) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "동굴2층", "동굴1층", new Point2D(64, 62), new List<Point2D>() { new Point2D(6, 47) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "동굴2층", "동굴3층", new Point2D(91, 42), new List<Point2D>() { new Point2D(88, 29) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "동굴3층", "동굴2층", new Point2D(90, 30), new List<Point2D>() { new Point2D(89, 41) }));
            //#endregion

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "가보자", "바이킹", new Point2D(10, 10), new List<Point2D>() { new Point2D(2, 44) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "바이킹", "가보자", new Point2D(4, 44), new List<Point2D>() { new Point2D(7, 7) }));
            //#endregion

            //#region 지구라트
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "지구라트1", "지구라트2", new Point2D(158, 71), new List<Point2D>() { new Point2D(146, 101) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "지구라트2", "지구라트1", new Point2D(144, 103), new List<Point2D>() { new Point2D(160, 69) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "지구라트2", "지구라트3", new Point2D(175, 56), new List<Point2D>() { new Point2D(169, 134) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "지구라트3", "지구라트2", new Point2D(167, 136), new List<Point2D>() { new Point2D(176, 55) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "지구라트3", "지구라트4", new Point2D(171, 32), new List<Point2D>() { new Point2D(55, 21) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "지구라트4", "지구라트3", new Point2D(54, 22), new List<Point2D>() { new Point2D(172, 31) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "지구라트4", "지구라트5", new Point2D(179, 157), new List<Point2D>() { new Point2D(46, 97) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "지구라트5", "지구라트4", new Point2D(45, 98), new List<Point2D>() { new Point2D(180, 156) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "지구라트5", Settings.VillageName.ElementAt(i) + ",32", new Point2D(Settings.StartLoc.X, Settings.StartLoc.Y), new List<Point2D>() { new Point2D(150, 134) }));
            //#endregion

            //#region 광산
            //                                                            //출발맵      도착맵          도착지점                             출발지점            
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "광산0", "광산1", new Point2D(76, 12), new List<Point2D>() { new Point2D(139, 39) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "광산1", "광산0", new Point2D(137, 40), new List<Point2D>() { new Point2D(76, 8) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "광산1", "광산2", new Point2D(75, 9), new List<Point2D>() { new Point2D(85, 66) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "광산2", "광산1", new Point2D(82, 67), new List<Point2D>() { new Point2D(76, 7) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "광산2", "광산3", new Point2D(75, 10), new List<Point2D>() { new Point2D(15, 38) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "광산3", "광산2", new Point2D(17, 39), new List<Point2D>() { new Point2D(76, 8) }));


            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "광산3", "광산4", new Point2D(17, 39), new List<Point2D>() { new Point2D(77, 21) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "광산4", "광산3", new Point2D(78, 23), new List<Point2D>() { new Point2D(15, 38) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "광산4", "광산5", new Point2D(76, 12), new List<Point2D>() { new Point2D(139, 39) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "광산5", "광산4", new Point2D(137, 40), new List<Point2D>() { new Point2D(76, 8) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "광산5", "광산6", new Point2D(75, 9), new List<Point2D>() { new Point2D(85, 66) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "광산6", "광산5", new Point2D(82, 67), new List<Point2D>() { new Point2D(76, 7) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "광산6", "광산7", new Point2D(75, 10), new List<Point2D>() { new Point2D(15, 38) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "광산7", "광산6", new Point2D(17, 39), new List<Point2D>() { new Point2D(76, 8) }));



            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "초보", "초보광산", new Point2D(18, 38), new List<Point2D>() { new Point2D(52, 43) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "초보광산", "초보", new Point2D(52, 45), new List<Point2D>() { new Point2D(15, 38) }));
            //#endregion

            //#region 퀘스트
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Qweak1", "Qweak2", new Point2D(185, 35), new List<Point2D>() { new Point2D(211, 85) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Qweak2", "Qweak1", new Point2D(209, 87), new List<Point2D>() { new Point2D(187, 33) }));
            //#endregion



            ////                                                            출발맵 도착맵          도착지점                             출발지점            
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), Settings.VillageName.ElementAt(i) + ",32", "터", new Point2D(29, 32),
            //    new List<Point2D>() { new Point2D(99, 128) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "터", Settings.VillageName.ElementAt(i) + ",32", new Point2D(99, 130),
            //    new List<Point2D>() { new Point2D(29, 30) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "터", "010", new Point2D(73, 8),
            //    new List<Point2D>() { new Point2D(11, 42) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "010", "터", new Point2D(11, 44),
            //    new List<Point2D>() { new Point2D(73, 6) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "터", "020", new Point2D(7, 8),
            //    new List<Point2D>() { new Point2D(11, 30) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "020", "터", new Point2D(11, 32),
            //    new List<Point2D>() { new Point2D(7, 6) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "터", "030", new Point2D(14, 11), new List<Point2D>() { new Point2D(11, 19) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "030", "터", new Point2D(11, 21), new List<Point2D>() { new Point2D(14, 10) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "터", "040", new Point2D(16, 18),
            //    new List<Point2D>() { new Point2D(22, 12) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "040", "터", new Point2D(22, 14),
            //    new List<Point2D>() { new Point2D(16, 16) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "터", "050", new Point2D(87, 46),
            //    new List<Point2D>() { new Point2D(35, 12) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "050", "터", new Point2D(35, 14),
            //    new List<Point2D>() { new Point2D(87, 44) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "터", "060", new Point2D(105, 78), new List<Point2D>() { new Point2D(47, 18) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "060", "터", new Point2D(47, 20), new List<Point2D>() { new Point2D(105, 76) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "060", "061", new Point2D(12, 12), new List<Point2D>() { new Point2D(12, 7) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "061", "060", new Point2D(12, 9), new List<Point2D>() { new Point2D(12, 10) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "터", "070", new Point2D(12, 10), new List<Point2D>() { new Point2D(47, 30) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "070", "터", new Point2D(47, 32), new List<Point2D>() { new Point2D(12, 8) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "070", "071", new Point2D(12, 10), new List<Point2D>() { new Point2D(92, 89) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "071", "070", new Point2D(92, 91), new List<Point2D>() { new Point2D(12, 8) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "터", "080", new Point2D(17, 9), new List<Point2D>() { new Point2D(47, 42) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "080", "터", new Point2D(47, 44), new List<Point2D>() { new Point2D(17, 7) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "080", "081", new Point2D(20, 17), new List<Point2D>() { new Point2D(87, 63) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "081", "080", new Point2D(87, 65), new List<Point2D>() { new Point2D(20, 15) }));


            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), Settings.VillageName.ElementAt(i) + ",32", "ItemVillage", new Point2D(93, 110),
            //    new List<Point2D>() { new Point2D(70, 66) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemVillage", Settings.VillageName.ElementAt(i) + ",32", new Point2D(70, 68),
            //    new List<Point2D>() { new Point2D(93, 108) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemVillage", "ItemPigmy1", new Point2D(16, 31),
            //    new List<Point2D>() { new Point2D(2, 18) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemPigmy1", "ItemVillage", new Point2D(2, 20),
            //    new List<Point2D>() { new Point2D(16, 29) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemPigmy1", "ItemPigmy2", new Point2D(10, 12),
            //    new List<Point2D>() { new Point2D(41, 27) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemPigmy2", "ItemPigmy1", new Point2D(41, 29),
            //    new List<Point2D>() { new Point2D(10, 10) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemPigmy2", "ItemPigmy3", new Point2D(7, 27),
            //    new List<Point2D>() { new Point2D(67, 8) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemPigmy3", "ItemPigmy2", new Point2D(67, 10),
            //    new List<Point2D>() { new Point2D(7, 25) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemVillage", "ItemZomby1", new Point2D(14, 39),
            //    new List<Point2D>() { new Point2D(25, 5) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemZomby1", "ItemVillage", new Point2D(25, 7),
            //    new List<Point2D>() { new Point2D(12, 39) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemZomby1", "ItemZomby2", new Point2D(13, 14),
            //    new List<Point2D>() { new Point2D(68, 42) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemZomby2", "ItemZomby1", new Point2D(68, 44),
            //    new List<Point2D>() { new Point2D(13, 12) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemZomby2", "ItemZomby3", new Point2D(13, 12),
            //    new List<Point2D>() { new Point2D(64, 20) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemZomby3", "ItemZomby2", new Point2D(64, 22),
            //    new List<Point2D>() { new Point2D(13, 10) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemVillage", "ItemSkel1", new Point2D(20, 31),
            //    new List<Point2D>() { new Point2D(186, 4) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemSkel1", "ItemVillage", new Point2D(186, 6),
            //    new List<Point2D>() { new Point2D(20, 29) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemSkel1", "ItemSkel2", new Point2D(12, 13),
            //    new List<Point2D>() { new Point2D(42, 21) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemSkel2", "ItemSkel1", new Point2D(42, 23),
            //    new List<Point2D>() { new Point2D(11, 12) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemSkel2", "ItemSkel3", new Point2D(15, 18),
            //    new List<Point2D>() { new Point2D(59, 12) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemSkel3", "ItemSkel2", new Point2D(58, 12),
            //    new List<Point2D>() { new Point2D(15, 17) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemVillage", "ItemButcher1", new Point2D(19, 25),
            //    new List<Point2D>() { new Point2D(190, 29) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemButcher1", "ItemVillage", new Point2D(190, 31),
            //    new List<Point2D>() { new Point2D(19, 23) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemButcher1", "ItemButcher2", new Point2D(19, 13),
            //    new List<Point2D>() { new Point2D(43, 27) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemButcher2", "ItemButcher1", new Point2D(43, 29),
            //    new List<Point2D>() { new Point2D(19, 11) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemButcher2", "ItemButcher3", new Point2D(20, 15),
            //    new List<Point2D>() { new Point2D(57, 29) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemButcher3", "ItemButcher2", new Point2D(57, 31),
            //    new List<Point2D>() { new Point2D(20, 13) }));

            ////출발맵      도착맵          도착지점                             출발지점       
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemVillage", "ItemMummy1", new Point2D(18, 19),
            //    new List<Point2D>() { new Point2D(193, 186) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemMummy1", "ItemVillage", new Point2D(193, 188),
            //    new List<Point2D>() { new Point2D(18, 17) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemMummy1", "ItemMummy2", new Point2D(13, 11), new List<Point2D>() { new Point2D(47, 34) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemMummy2", "ItemMummy1", new Point2D(47, 36), new List<Point2D>() { new Point2D(13, 9) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemMummy2", "ItemMummy3", new Point2D(7, 9),
            //    new List<Point2D>() { new Point2D(62, 36) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemMummy3", "ItemMummy2", new Point2D(62, 38),
            //    new List<Point2D>() { new Point2D(7, 7) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemVillage", "ItemStone1", new Point2D(16, 14),
            //    new List<Point2D>() { new Point2D(177, 160) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemStone1", "ItemVillage", new Point2D(177, 162),
            //    new List<Point2D>() { new Point2D(16, 12) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemStone1", "ItemStone2", new Point2D(22, 15),
            //    new List<Point2D>() { new Point2D(31, 30) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemStone2", "ItemStone1", new Point2D(31, 32),
            //    new List<Point2D>() { new Point2D(22, 13) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemStone2", "ItemStone3", new Point2D(16, 18),
            //    new List<Point2D>() { new Point2D(44, 34) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemStone3", "ItemStone2", new Point2D(44, 36),
            //    new List<Point2D>() { new Point2D(16, 16) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemStone3", "ItemStone4", new Point2D(6, 23),
            //    new List<Point2D>() { new Point2D(37, 32) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemStone4", "ItemStone3", new Point2D(37, 34),
            //    new List<Point2D>() { new Point2D(6, 21) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemVillage", "ItemDummy1", new Point2D(18, 19),
            //    new List<Point2D>() { new Point2D(23, 135) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemDummy1", "ItemVillage", new Point2D(23, 136),
            //    new List<Point2D>() { new Point2D(18, 17) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemDummy1", "ItemDummy2", new Point2D(13, 11),
            //    new List<Point2D>() { new Point2D(47, 34) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemDummy2", "ItemDummy1", new Point2D(13, 9),
            //    new List<Point2D>() { new Point2D(18, 17) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemDummy2", "ItemDummy3", new Point2D(7, 9),
            //    new List<Point2D>() { new Point2D(62, 36) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemDummy3", "ItemDummy2", new Point2D(62, 38),
            //    new List<Point2D>() { new Point2D(7, 7) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemDummy3", "ItemDummy4", new Point2D(16, 18),
            //    new List<Point2D>() { new Point2D(66, 12) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemDummy4", "ItemDummy3", new Point2D(66, 14),
            //    new List<Point2D>() { new Point2D(16, 16) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemDummy4", "ItemDummy5", new Point2D(6, 23),
            //    new List<Point2D>() { new Point2D(37, 32) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemDummy5", "ItemDummy4", new Point2D(37, 34),
            //    new List<Point2D>() { new Point2D(6, 21) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemVillage", "ItemWarDummy1", new Point2D(18, 19),
            //    new List<Point2D>() { new Point2D(38, 142) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemWarDummy1", "ItemVillage", new Point2D(38, 143),
            //    new List<Point2D>() { new Point2D(18, 17) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemWarDummy1", "ItemWarDummy2", new Point2D(13, 11),
            //    new List<Point2D>() { new Point2D(47, 34) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemWarDummy2", "ItemWarDummy1", new Point2D(13, 9),
            //    new List<Point2D>() { new Point2D(18, 17) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemWarDummy2", "ItemWarDummy3", new Point2D(7, 9),
            //    new List<Point2D>() { new Point2D(62, 36) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemWarDummy3", "ItemWarDummy2", new Point2D(62, 38),
            //    new List<Point2D>() { new Point2D(7, 7) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemWarDummy3", "ItemWarDummy4", new Point2D(16, 18),
            //    new List<Point2D>() { new Point2D(66, 12) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemWarDummy4", "ItemWarDummy3", new Point2D(66, 14),
            //    new List<Point2D>() { new Point2D(16, 16) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemWarDummy4", "ItemWarDummy5", new Point2D(6, 23),
            //    new List<Point2D>() { new Point2D(37, 32) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemWarDummy5", "ItemWarDummy4", new Point2D(37, 34),
            //    new List<Point2D>() { new Point2D(6, 21) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemVillage", "ItemHardboil1", new Point2D(18, 19),
            //    new List<Point2D>() { new Point2D(13, 45) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemHardboil1", "ItemVillage", new Point2D(13, 46),
            //    new List<Point2D>() { new Point2D(18, 17) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemHardboil1", "ItemHardboil2", new Point2D(13, 11), new List<Point2D>() { new Point2D(47, 34) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemHardboil2", "ItemHardboil1", new Point2D(47, 35), new List<Point2D>() { new Point2D(13, 9) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemHardboil2", "ItemHardboil3", new Point2D(7, 9),
            //    new List<Point2D>() { new Point2D(62, 36) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemHardboil3", "ItemHardboil2", new Point2D(62, 38),
            //    new List<Point2D>() { new Point2D(7, 7) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemHardboil3", "ItemHardboil4", new Point2D(16, 18),
            //    new List<Point2D>() { new Point2D(66, 12) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemHardboil4", "ItemHardboil3", new Point2D(66, 14),
            //    new List<Point2D>() { new Point2D(16, 16) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemHardboil4", "ItemHardboil5", new Point2D(6, 23),
            //    new List<Point2D>() { new Point2D(37, 32) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemHardboil5", "ItemHardboil4", new Point2D(37, 34),
            //    new List<Point2D>() { new Point2D(6, 21) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemVillage", "ItemGolem1", new Point2D(18, 19),
            //    new List<Point2D>() { new Point2D(7, 178) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemGolem1", "ItemVillage", new Point2D(7, 179),
            //    new List<Point2D>() { new Point2D(18, 17) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemGolem1", "ItemGolem2", new Point2D(13, 11),
            //    new List<Point2D>() { new Point2D(47, 34) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemGolem2", "ItemGolem1", new Point2D(13, 9),
            //    new List<Point2D>() { new Point2D(18, 17) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemGolem2", "ItemGolem3", new Point2D(7, 9),
            //    new List<Point2D>() { new Point2D(62, 36) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemGolem3", "ItemGolem2", new Point2D(62, 38),
            //    new List<Point2D>() { new Point2D(7, 7) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemGolem3", "ItemGolem4", new Point2D(16, 18),
            //    new List<Point2D>() { new Point2D(66, 12) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemGolem4", "ItemGolem3", new Point2D(66, 14),
            //    new List<Point2D>() { new Point2D(16, 16) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemGolem4", "ItemGolem5", new Point2D(6, 23),
            //    new List<Point2D>() { new Point2D(37, 32) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "ItemGolem5", "ItemGolem4", new Point2D(37, 34),
            //    new List<Point2D>() { new Point2D(6, 21) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), Settings.VillageName.ElementAt(i) + ",32", "스켈1", new Point2D(50, 13),
            //    new List<Point2D>() { new Point2D(139, 134) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "스켈1", Settings.VillageName.ElementAt(i) + ",32", new Point2D(138, 136),
            //    new List<Point2D>() { new Point2D(50, 11) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "스켈1", "스켈2", new Point2D(12, 14),
            //    new List<Point2D>() { new Point2D(13, 47) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "스켈2", "스켈1", new Point2D(13, 49),
            //    new List<Point2D>() { new Point2D(12, 12) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "스켈2", "스켈3", new Point2D(34, 14),
            //    new List<Point2D>() { new Point2D(48, 42) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "스켈3", "스켈2", new Point2D(48, 44),
            //    new List<Point2D>() { new Point2D(34, 12) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "스켈3", "스켈4", new Point2D(33, 24),
            //    new List<Point2D>() { new Point2D(33, 49) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "스켈4", "스켈3", new Point2D(33, 51),
            //    new List<Point2D>() { new Point2D(33, 22) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "스켈4", "스켈5", new Point2D(14, 21),
            //    new List<Point2D>() { new Point2D(32, 35) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "스켈5", "스켈4", new Point2D(32, 37),
            //    new List<Point2D>() { new Point2D(14, 19) }));   //스켈5 out 52:54


            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), Settings.VillageName.ElementAt(i) + ",32", "Weakly1", new Point2D(11, 19),
            //    new List<Point2D>() { new Point2D(65, 122) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Weakly1", Settings.VillageName.ElementAt(i) + ",32", new Point2D(66, 123),
            //    new List<Point2D>() { new Point2D(11, 17) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Weakly1", "Weakly2", new Point2D(51, 14),
            //    new List<Point2D>() { new Point2D(50, 44) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Weakly2", "Weakly1", new Point2D(50, 46),
            //    new List<Point2D>() { new Point2D(51, 12) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Weakly2", "Weakly3", new Point2D(53, 15),
            //    new List<Point2D>() { new Point2D(38, 53) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Weakly3", "Weakly2", new Point2D(38, 55),
            //    new List<Point2D>() { new Point2D(53, 13) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Weakly3", "Weakly4", new Point2D(9, 54),
            //    new List<Point2D>() { new Point2D(9, 34) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Weakly4", "Weakly3", new Point2D(9, 36),
            //    new List<Point2D>() { new Point2D(9, 52) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Weakly4", "Weakly5", new Point2D(9, 11),
            //    new List<Point2D>() { new Point2D(52, 51) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Weakly5", "Weakly4", new Point2D(52, 53),
            //    new List<Point2D>() { new Point2D(9, 9) }));



            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "골램", "Golem1", new Point2D(1, 1),
            //    new List<Point2D>() { new Point2D(41, 49) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Golem1", "골램", new Point2D(41, 48),
            //    new List<Point2D>() { new Point2D(0, 0) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "골램", "Golem3", new Point2D(27, 1),
            //    new List<Point2D>() { new Point2D(49, 5) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Golem3", "골램", new Point2D(48, 5),
            //    new List<Point2D>() { new Point2D(27, 0) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Golem1", "Golem2", new Point2D(33, 27),
            //    new List<Point2D>() { new Point2D(35, 49) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Golem2", "Golem1", new Point2D(35, 48),
            //    new List<Point2D>() { new Point2D(33, 26) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Golem1", "Golem12", new Point2D(1, 2),
            //    new List<Point2D>() { new Point2D(40, 0) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Golem12", "Golem1", new Point2D(40, 1),
            //    new List<Point2D>() { new Point2D(0, 2) }));

            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "TreasureLand", "Great", new Point2D(19, 22),
            //    new List<Point2D>() { new Point2D(45, 71) }));
            //Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Great", "TreasureLand", new Point2D(45, 72),
            //    new List<Point2D>() { new Point2D(19, 21) }));
            #endregion


            /*        Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Golem2", "Golem3", new Point2D(27,1), 
                            new List<Point2D>(){ new Point2D(18,49)}));
                        Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Golem3", "Golem2", new Point2D(18,48), 
                            new List<Point2D>(){ new Point2D(27,0)}));
                        Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Golem3", "Golem12", new Point2D(1,2), 
                            new List<Point2D>(){ new Point2D(25,29)}));
                        Portals[i].Add(new InnerMap(Settings.VillageName.ElementAt(i), "Golem12", "Golem3", new Point2D(26,29), 
                            new List<Point2D>(){ new Point2D(0,2)}));*/
            //}

            //using (System.IO.StreamWriter sw = new System.IO.StreamWriter(Settings.rootPath + "aaaaaa.txt"))
            //{
            //    for (int i = 0; i < Settings.VillageName.Count; i++)
            //    {
            //        foreach (InnerMap p in Portals[i])
            //        {
            //            string strQuery
            //                = string.Format("INSERT INTO PortalMaster(VilgNum, From_MAP, From_LocX, From_LocY, To_MAP, To_LocX, To_LocY, LimitLevel, MAXLimitLevel) VALUES ({0}, ('{1}'), {2}, {3}, ('{4}'), {5}, {6}, {7}, {8});", i, p.Map, p.ArriveLoc.X, p.ArriveLoc.Y, p.Dest, p.Locs[0].X, p.Locs[0].Y, 0, 0);

            //            sw.WriteLine(strQuery);
            //        }
            //    }
            //}
        }
    }


    public class GotoInfo
    {
        public int vilgNum;
        public string GoName;
        public string Map;
        public Point2D Loc;

        public GotoInfo(object vilg, object goName, object map, object locx, object locy)
        {
            vilgNum = Convert.ToInt32(vilg);
            GoName = goName.ToString();
            Map = map.ToString();
            Loc = new Point2D(Convert.ToInt32(locx), Convert.ToInt32(locy));
        }
    }

    public class GotoCmd
    {
        public static List<GotoInfo>[] Gotocmd;

        public static void GotoCmdLoad()
        {
            Gotocmd = new List<GotoInfo>[Settings.VillageName.Count];

            for (int i = 0; i < Settings.VillageName.Count; i++)
                Gotocmd[i] = new List<GotoInfo>();

            string strConn = Settings.DB_Conn;
            DataSet GotoData = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                string strQuery = "SELECT * FROM GotoCmd";
                conn.Open();
                MySqlDataAdapter adpt = new MySqlDataAdapter(strQuery, conn);
                GotoData.Clear();
                adpt.Fill(GotoData, "Goto");
            }

            foreach (DataRow d in GotoData.Tables["Goto"].Rows)
            {
                int vilgNum = Convert.ToInt32(d["VilgNum"]);
                if (vilgNum == -1)
                {
                    for (int i = 0; i < Settings.VillageName.Count(); i++)
                    {
                        GotoInfo temp = new GotoInfo(i, d["GoName"], d["Map"], d["LocX"], d["LocY"]);
                        Gotocmd[i].Add(temp);
                    }
                }
                else
                {
                    GotoInfo temp = new GotoInfo(vilgNum, d["GoName"], d["Map"], d["LocX"], d["LocY"]);
                    Gotocmd[vilgNum].Add(temp);
                }
            }
        }
    }
}
