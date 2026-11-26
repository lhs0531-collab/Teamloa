//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using System.IO;
//namespace LKCamelot.model
//{
//    public static class Map
//    {
//        public static Dictionary<string, string> FullMaps = new Dictionary<string, string>()
//        {
//            { "휴게실", "erest.map" },
    
//            { "마을1-21", "21.map" },
//            { "마을1-22", "22.map" },
//            { "마을1-23", "23.map" },
//            { "마을1-31", "31.map" },
//            { "마을1-32", "32.map" },        
//            { "마을1-33", "33.map" },
//            { "마을1-41", "41.map" },
//            { "마을1-42", "42.map" },
//            { "마을1-43", "43.map" },
//            { "마을1-51", "51.map" },
//            { "마을1-52", "52.map" },
//            { "마을1-53", "53.map" },

//            { "시약상", "loen.Map" },
//            { "대장간", "anold.Map" },

//            { "초보", "estart2.map" },
//            { "터", "elevel.map" },
//            { "Biggun1", "estart4.map" },
//            { "Biggun2", "estart3.map" },
//            { "Miro1", "miro1.map" },
//            { "TreasureLand", "1000.map" },

//            { "1000.map", "1000.map" },
//            { "bone01.map", "bone01.map" },
//            { "eabinc.map", "eabinc.map" },
//            { "축구장", "esoccer.map" },

//            { "퀘스트", "qweak.map"},
//            { "Qweak1", "qweak1.map" },
//            { "Qweak2", "qweak2.map" },
//            { "Qweak3", "qweak3.map" },
//            { "Qweak4", "qweak4.map" },
//            { "Qweak5", "qweak5.map" },
            

//            { "가보자", "estart1.map" },
//            { "스콜피온", "estart4.map" },
//            { "동굴1층", "cave.map"},
//            { "동굴2층", "cave1.map"},
//            { "동굴3층", "cave2.map"},

//            { "estart2.map", "estart2.map" },
//            { "광산0", "miner0.map" },
//            { "광산1", "miner1.map" },
//            { "광산2", "miner2.map" },
//            { "광산3", "miner3.map" },
            
//            { "아발론", "miner0.map" },


//            { "Great", "great.map" },
//            { "010", "010map.map" },
//            { "020", "020map.map" },
//            { "030", "030map.map" },
//            { "040", "040map.map" },
//            { "050", "050map.map" },
//            { "060", "060map.map" },
//            { "070", "070map.map" },
//            { "080", "080map.map" },
//            { "ItemVillage", "ivillage.map" },
//            { "ItemPigmy1", "ipigmy1.map" },
//            { "ItemPigmy2", "ipigmy2.map" },
//            { "ItemPigmy3", "ipigmy3.map" },
//            { "ItemZomby1", "izomby1.map" },
//            { "ItemZomby2", "izomby2.map" },
//            { "ItemZomby3", "izomby3.map" },
//            { "ItemSkel1", "iskel1.map" },
//            { "ItemSkel2", "iskel2.map" },
//            { "ItemSkel3", "iskel3.map" },
//            { "ItemButcher1", "ibut1.map" },
//            { "ItemButcher2", "ibut2.map" },
//            { "ItemButcher3", "ibut3.map" },
//            { "ItemMummy1", "imummy1.map" },
//            { "ItemMummy2", "imummy2.map" },
//            { "ItemMummy3", "imummy3.map" },
//            { "ItemStone1", "istone1.map" },
//            { "ItemStone2", "istone2.map" },
//            { "ItemStone3", "istone3.map" },
//            { "ItemStone4", "istone4.map" },
//            { "ItemDummy1", "imummy1.map" },
//            { "ItemDummy2", "imummy2.map" },
//            { "ItemDummy3", "imummy3.map" },
//            { "ItemDummy4", "istone3.map" },
//            { "ItemDummy5", "istone4.map" },
//            { "ItemWarDummy1", "imummy1.map" },
//            { "ItemWarDummy2", "imummy2.map" },
//            { "ItemWarDummy3", "imummy3.map" },
//            { "ItemWarDummy4", "istone3.map" },
//            { "ItemWarDummy5", "istone4.map" },
//            { "ItemHardboil1", "imummy1.map" },
//            { "ItemHardboil2", "imummy2.map" },
//            { "ItemHardboil3", "imummy3.map" },
//            { "ItemHardboil4", "istone3.map" },
//            { "ItemHardboil5", "istone4.map" },
//            { "ItemGolem1", "imummy1.map" },
//            { "ItemGolem2", "imummy2.map" },
//            { "ItemGolem3", "imummy3.map" },
//            { "ItemGolem4", "istone3.map" },
//            { "ItemGolem5", "istone4.map" },

//            { "Weakly1", "weakly1.map" },
//            { "Weakly2", "weakly2.map" },
//            { "Weakly3", "weakly3.map" },
//            { "Weakly4", "weakly4.map" },
//            { "Weakly5", "weakly5.map" },

//            { "스켈1", "skel1.map" },
//            { "스켈2", "skel2.map" },
//            { "스켈3", "skel3.map" },
//            { "스켈4", "skel4.map" },
//            { "스켈5", "skel5.map" },

//            { "Cave", "Cave.map" },
//            { "Cave1", "cave1.map" },
//            { "Cave2", "cave2.map" },

//            { "골램", "golem.map" },
//            { "Golem1", "golem1.map" },
//            { "Golem2", "golem2.map" },
//            { "Golem3", "golem3.map" },
//            { "Golem12", "golem12.map" },

//            { "지구라트1", "vv1.map" },
//            { "지구라트2", "vv2.map" },
//            { "지구라트3", "vv3.map" },
//            { "지구라트4", "vv4.map" },
//            { "지구라트5", "vv5.map" },

//            { "대전장", "ebox2x2.map" },       //대전장
//            { "큰대전장", "ebox4x4.map" },       //큰대전장
//            { "독방", "prison.map"},
//        };

//        public enum MapType { LK, VV, PS, Weak };
//        public class TiledMap
//        {
//            string Name;
//            public int sizeX;
//            public int sizeY;
//            int obj;

//            public MyPathNode[,] tiles;

//            public MapAttribute Mapinfo;

//            public TiledMap(string Name, int sizeX, int sizeY, int obj, MapAttribute MapInfo)
//            {
//                this.Name = Name;
//                this.sizeX = sizeX;
//                this.sizeY = sizeY;
//                this.obj = obj;
//                this.Mapinfo = MapInfo;
//                tiles = new MyPathNode[sizeX, sizeY];
//            }
//        }
//        //맵 저장 클레스
//        public class MapAttribute
//        {
//            public MapType Type;
//            public byte Dark;
//            public int SoundTrack = -1;
//            public bool GlobalTime;

//            public MapAttribute() { }
//            public MapAttribute(MapType Type, byte Dark, bool GlobalTime)
//            {
//                this.Type = Type;
//                this.Dark = Dark;
//                this.GlobalTime = GlobalTime;
//            }
//            public MapAttribute(MapType Type, byte Dark, int SoundTrack, bool GlobalTime)
//            {
//                this.Type = Type;
//                this.Dark = Dark;
//                this.SoundTrack = SoundTrack;
//                this.GlobalTime = GlobalTime;
//            }
//        }
//        public static Dictionary<string, MapAttribute> Mapsizes2 = new Dictionary<string, MapAttribute>()
//        {
//            // FullMaps.Value, new MapAttribut( 타일, 안개, 음악, 시간적용여부)    
//            {"erest.map", new MapAttribute(MapType.LK, 0x00, 1002, true) },
//            {"loen.Map", new MapAttribute(MapType.LK, 0x00, 1001,false) },
//            {"anold.Map", new MapAttribute(MapType.LK, 0x00, 1001, false) },
            
//            {"weakly1.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"weakly2.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"weakly3.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"weakly4.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"weakly5.map", new MapAttribute(MapType.LK, 1, 1001, false) },
            
//            {"skel1.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"skel2.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"skel3.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"skel4.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"skel5.map", new MapAttribute(MapType.LK, 1, 1001, false) },

//            {"21.map", new MapAttribute(MapType.LK, 0x00, 1000, true) },
//            {"22.map", new MapAttribute(MapType.LK, 0x00, 1000, true) },
//            {"23.map", new MapAttribute(MapType.LK, 0x00, 1000, true) },
//            {"31.map", new MapAttribute(MapType.LK, 0x00, 1000, true) },
//            {"32.map", new MapAttribute(MapType.LK, 0x00, 1000, true) },
//            {"33.map", new MapAttribute(MapType.LK, 0x00, 1000, true) },
//            {"41.map", new MapAttribute(MapType.LK, 0x00, 1000, true) },
//            {"42.map", new MapAttribute(MapType.LK, 0x00, 1000, true) },
//            {"43.map", new MapAttribute(MapType.LK, 0x00, 1000, true) },
            
//            {"elevel.map", new MapAttribute(MapType.LK, 0x02, 1004, false) },
//            {"010map.map", new MapAttribute(MapType.LK, 0x02, 1001, false) },
//            {"020map.map", new MapAttribute(MapType.LK, 0x02, 1001, false) },
//            {"030map.map", new MapAttribute(MapType.LK, 0x02, 1001, false) },
//            {"040map.map", new MapAttribute(MapType.LK, 0x02, 1001, false) },
//            {"050map.map", new MapAttribute(MapType.LK, 0x02, 1001, false) },
//            {"060map.map", new MapAttribute(MapType.LK, 0x02, 1001, false) },
//            {"061map.map", new MapAttribute(MapType.LK, 0x02, 1001, false) },
//            {"070map.map", new MapAttribute(MapType.LK, 0x02, 1001, false) },
//            {"071map.map", new MapAttribute(MapType.LK, 0x02, 1001, false) },
//            {"080map.map", new MapAttribute(MapType.LK, 0x02, 1001, false) },
//            {"081map.map", new MapAttribute(MapType.LK, 0x02, 1001, false) },

//            {"ivillage.map", new MapAttribute(MapType.LK, 1, 1001, true) },
//            {"ipigmy1.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"ipigmy2.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"ipigmy3.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"izomby1.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"izomby2.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"izomby3.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"iskel1.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"iskel2.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"iskel3.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"ibut1.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"ibut2.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"ibut3.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"imummy1.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"imummy2.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"imummy3.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"istone1.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"istone2.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"istone3.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"istone4.map", new MapAttribute(MapType.LK, 1, 1001, false) },

//            {"estart.map", new MapAttribute(MapType.LK, 1, 1001, true) },
//            {"estart1.map", new MapAttribute(MapType.LK, 1, 1001, true) },
//            {"estart2.map", new MapAttribute(MapType.LK, 1, 1001, true) },
//            {"estart3.map", new MapAttribute(MapType.LK, 1, 1001, true) },
//            {"estart4.map", new MapAttribute(MapType.LK, 1, 1001, true) },

//            {"cave.map", new MapAttribute(MapType.Weak, 1, 1001, false) },
//            {"cave1.map", new MapAttribute(MapType.Weak, 1, 1001, false) },
//            {"cave2.map", new MapAttribute(MapType.Weak, 1, 1001, false) },
//            {"cave3.map", new MapAttribute(MapType.Weak, 1, 1001, false) },

//            {"golem.map", new MapAttribute(MapType.LK, 0x02, 1001, true) },
//            {"golem1.map", new MapAttribute(MapType.LK, 0x02, 1001, true) },
//            {"golem2.map", new MapAttribute(MapType.LK, 0x02, 1001, true) },
//            {"golem3.map", new MapAttribute(MapType.LK, 0x02, 1001, true) },
//            {"golem12.map", new MapAttribute(MapType.LK, 0x02, 1001, true) },
            
//            {"great.map", new MapAttribute(MapType.LK, 1, 1001, true) },
//            {"miro1.map", new MapAttribute(MapType.LK, 1, 1001, true) },
//            {"Viking.map", new MapAttribute(MapType.LK, 1, 1001, true) },
//            {"1000.map", new MapAttribute(MapType.LK, 1, 1001, true) },

//            {"miner0.map", new MapAttribute(MapType.Weak, 0x04, 1001, false) },
//            {"miner1.map", new MapAttribute(MapType.Weak, 0x04, 1001, false) },
//            {"miner2.map", new MapAttribute(MapType.Weak, 0x04, 1001, false) },
//            {"miner3.map", new MapAttribute(MapType.Weak, 0x04, 1001, false) },

//            {"qweak.map", new MapAttribute(MapType.LK, 1, 1001, false) },
//            {"qweak1.map", new MapAttribute(MapType.Weak, 1, 1001, false) },
//            {"qweak2.map", new MapAttribute(MapType.Weak, 1, 1001, false) },
//            {"qweak3.map", new MapAttribute(MapType.Weak, 1, 1001, false) },
//            {"qweak4.map", new MapAttribute(MapType.Weak, 1, 1001, false) },
//            {"qweak5.map", new MapAttribute(MapType.Weak, 1, 1001, false) },

//            {"vv1.map", new MapAttribute(MapType.VV, 0x04, 1001, false) },
//            {"vv2.map", new MapAttribute(MapType.VV, 0x04, 1001, false) },
//            {"vv3.map", new MapAttribute(MapType.VV, 0x04, 1001, false) },
//            {"vv4.map", new MapAttribute(MapType.VV, 0x04, 1001, false) },
//            {"vv5.map", new MapAttribute(MapType.VV, 0x04, 1001, false) },

//            {"ebox2x2.map", new MapAttribute(MapType.LK, 1, false) },
//            {"ebox4x4.map", new MapAttribute(MapType.LK, 1, false) },
//            {"prison.map", new MapAttribute(MapType.PS, 1, 1001, false) },
//            {"esoccer.map", new MapAttribute(MapType.LK, 1, 1001, true) },
//        };

//        public static Dictionary<string, TiledMap> loadedmaps = new Dictionary<string, TiledMap>();             // 맵파일 정보 저장



//        public static Point2D SpawnPoint(string map)
//        {
//            Point2D temp = null;
//            var nmap = model.Map.FullMaps.Where(xe => xe.Key == map).FirstOrDefault().Value;
//            TiledMap curmap;
//            loadedmaps.TryGetValue(nmap, out curmap);

//            if (curmap == null)
//            {
//                Console.WriteLine(string.Format("맵을 찾을수 없습니다.:{0}", map));
//                return null;
//            }

//            var randomtile = curmap.tiles[Util.RandomMinMax(0, loadedmaps[nmap].sizeX - 1), Util.RandomMinMax(0, loadedmaps[nmap].sizeY - 1)];
//            if (randomtile.IsWall == false && randomtile.SpawnP == false)
//            {
//                temp = new Point2D(randomtile.X, randomtile.Y);
//                //temp.X = randomtile.X;
//                //temp.Y = randomtile.Y;
//            }

//            return temp;
//        }
//        public static Point2D SpawnPointFixed(string map)
//        {
//            Point2D temp = null;
//            var nmap = model.Map.FullMaps.Where(xe => xe.Key == map).FirstOrDefault().Value;
//            TiledMap curmap;
//            loadedmaps.TryGetValue(nmap, out curmap);

//            if (curmap == null)
//            {
//                Console.WriteLine(string.Format("맵을 찾을수 없습니다.:{0}", map));
//                return null;
//            }

//            var randomtile = curmap.tiles[Util.RandomMinMax(0, loadedmaps[nmap].sizeX - 1), Util.RandomMinMax(0, loadedmaps[nmap].sizeY - 1)];
//            if (randomtile.IsWall == false)
//            {
//                temp = new Point2D(0, 0);
//                temp.X = randomtile.X;
//                temp.Y = randomtile.Y;
//            }

//            return temp;
//        }
//        public static Point2D SpawnPointFixed(string map, int x, int y, int Range)
//        {
//            Point2D temp = null;
//            var nmap = LKCamelot.model.Map.FullMaps.Where(xee => xee.Key == map).FirstOrDefault().Value;
//            TiledMap curmap;
//            loadedmaps.TryGetValue(nmap, out curmap);

//            if (curmap == null)
//            {
//                Console.WriteLine(string.Format("맵을 찾을수 없습니다.:{0}", map));
//                return null;
//            }

//            int MinX = x < Range ? 0 : x - Range;
//            int MinY = y < Range ? 0 : y - Range;

//            int MaxX = x + Range >= curmap.sizeX ? curmap.sizeX - 1 : x + Range;
//            int MaxY = y + Range >= curmap.sizeY ? curmap.sizeY - 1 : y + Range;

//            var randomtile = curmap.tiles[Util.RandomMinMax(MinX, MaxX), Util.RandomMinMax(MinY, MaxY)];
//            if (randomtile.IsWall == false)
//            {
//                temp = new Point2D(0, 0);
//                temp.X = randomtile.X;
//                temp.Y = randomtile.Y;
//            }

//            return temp;
//        }

//        public static Point2D SpawnPointFixed(string map, int xe, int ye, int xe2, int ye2)
//        {
//            Point2D temp = null;
//            var nmap = LKCamelot.model.Map.FullMaps.Where(xee => xee.Key == map).FirstOrDefault().Value;
//            TiledMap curmap;
//            loadedmaps.TryGetValue(nmap, out curmap);

//            if (curmap == null)
//            {
//                Console.WriteLine(string.Format("맵을 찾을수 없습니다.:{0}", map));
//                return null;
//            }

//            var randomtile = curmap.tiles[Util.RandomMinMax(xe, xe2), Util.RandomMinMax(ye, ye2)];
//            if (randomtile.IsWall == false)
//            {
//                temp = new Point2D(0, 0);
//                temp.X = randomtile.X;
//                temp.Y = randomtile.Y;
//            }

//            return temp;
//        }

//        public static void MapInit()
//        {
//            string[] filePaths = Directory.GetFiles(Settings.rootPath + "map");
//            foreach (var file in filePaths)
//            {
//                string[] mapa = file.Split('\\');
//                var mapn = mapa[mapa.Length - 1];                       // 맵이름 추출
//                if (Mapsizes2.ContainsKey(mapn))
//                {
//                    TiledMap tile;
//                    using (FileStream stream = File.OpenRead(file))
//                    using (BinaryReader reader = new BinaryReader(stream, Encoding.Default))
//                    {
//                        reader.ReadChars(16);
//                        reader.ReadChars(16);

//                        int mWidth = reader.ReadInt32();
//                        int mHeight = reader.ReadInt32();

//                        reader.ReadChars(16);
//                        reader.ReadChars(16);
//                        int obj = reader.ReadInt32();             //오브젝트 갯수

//                        tile = new TiledMap(mapn, mWidth, mHeight, obj, Mapsizes2[mapn]);


//                        for (int i = 0; i < mHeight; i++)
//                            for (int j = 0; j < mWidth; j++)
//                            {
//                                byte[] byteTile = reader.ReadBytes(4);
//                                var tempp = new MyPathNode(j, i);

//                                if (Mapsizes2[mapn].Type == MapType.LK)
//                                {
//                                    if (byteTile[0] == 0x03
//                                        || byteTile[0] == 0x05
//                                        || byteTile[0] == 0x09
//                                        || (byteTile[0] == 0x02 && byteTile[2] == 0x03)
//                                        || byteTile[0] == 0x6A
//                                        || byteTile[0] == 0x6B
//                                        || byteTile[0] == 0x6D
//                                        || byteTile[0] == 0x6E
//                                        || byteTile[0] == 0x6F
//                                        || ((byteTile[0] == 0x6C || byteTile[0] == 0x72) && byteTile[3] != 01)
//                                        || byteTile[0] == 0x66
//                                        || byteTile[0] == 0x67
//                                        || byteTile[0] == 0x68
//                                        )
//                                        tempp.IsWall = true;
//                                }
//                                else if (Mapsizes2[mapn].Type == MapType.VV)
//                                {
//                                    if (byteTile[0] == 0 || (byteTile[0] == 1 && (byteTile[2] == 4 || byteTile[2] == 6 || byteTile[2] == 7 || byteTile[2] == 8)))
//                                        tempp.IsWall = true;
//                                }
//                                else if (Mapsizes2[mapn].Type == MapType.PS)
//                                {
//                                    if (byteTile[0] == 1 && (byteTile[2] == 1 || byteTile[2] == 2 || byteTile[2] == 3 || byteTile[2] == 4 || byteTile[2] == 7 || (byteTile[2] == 9 || byteTile[3] == 0 || byteTile[3] == 1 || byteTile[3] == 5 || byteTile[3] == 8)))
//                                        tempp.IsWall = true;

//                                }
//                                else if (Mapsizes2[mapn].Type == MapType.Weak)
//                                {
//                                    if (byteTile[0] == 1 && (byteTile[2] == 1 || byteTile[2] == 2 || byteTile[2] == 3 || byteTile[2] == 4 || byteTile[2] == 7)
//                                        || byteTile[0] == 2 || byteTile[0] == 3 || byteTile[0] == 4)
//                                        tempp.IsWall = true;
//                                }
//                                tile.tiles[j, i] = tempp;
//                            }
//                    }
//                    loadedmaps.Add(mapn, tile);
//                }
//            }
//        }

//        public static void MapInit1()
//        {
//            string[] filePaths = Directory.GetFiles(Settings.rootPath + "map");
//            foreach (var file in filePaths)
//            {
//                string[] mapa = file.Split('\\');
//                var mapn = mapa[mapa.Length - 1];                       // 맵이름 추출
//                if (Mapsizes2.ContainsKey(mapn))
//                {
//                    TiledMap tile;
//                    using (FileStream stream = File.OpenRead(file))
//                    using (BinaryReader reader = new BinaryReader(stream, Encoding.Default))
//                    {
//                        reader.ReadChars(16);
//                        reader.ReadChars(16);

//                        int mWidth = reader.ReadInt32();
//                        int mHeight = reader.ReadInt32();

//                        reader.ReadChars(16);
//                        reader.ReadChars(16);
//                        int obj = reader.ReadInt32();             //오브젝트 갯수

//                        tile = new TiledMap(mapn, mWidth, mHeight, obj, Mapsizes2[mapn]);


//                        for (int i = 0; i < mHeight; i++)
//                            for (int j = 0; j < mWidth; j++)
//                            {
//                                byte[] byteTile = reader.ReadBytes(4);
//                                var tempp = new MyPathNode(j, i);

//                                if (Mapsizes2[mapn].Type == MapType.LK)
//                                {
//                                    if (byteTile[0] == 0x03
//                                        || byteTile[0] == 0x05
//                                        || byteTile[0] == 0x09
//                                        || (byteTile[0] == 0x02 && byteTile[2] == 0x03)
//                                        || byteTile[0] == 0x6A
//                                        || byteTile[0] == 0x6B
//                                        || byteTile[0] == 0x6D
//                                        || byteTile[0] == 0x6E
//                                        || byteTile[0] == 0x6F
//                                        || ((byteTile[0] == 0x6C || byteTile[0] == 0x72) && byteTile[3] != 01)
//                                        || byteTile[0] == 0x66
//                                        || byteTile[0] == 0x67
//                                        || byteTile[0] == 0x68
//                                        )
//                                        tempp.IsWall = true;
//                                }
//                                else if (Mapsizes2[mapn].Type == MapType.VV)
//                                {
//                                    if (byteTile[0] == 0 || (byteTile[0] == 1 && (byteTile[2] == 4 || byteTile[2] == 6 || byteTile[2] == 7 || byteTile[2] == 8)))
//                                        tempp.IsWall = true;
//                                }
//                                else if (Mapsizes2[mapn].Type == MapType.PS)
//                                {
//                                    if (byteTile[0] == 1 && (byteTile[2] == 1 || byteTile[2] == 2 || byteTile[2] == 3 || byteTile[2] == 4 || byteTile[2] == 7 || (byteTile[2] == 9 || byteTile[3] == 0 || byteTile[3] == 1 || byteTile[3] == 5 || byteTile[3] == 8)))
//                                        tempp.IsWall = true;

//                                }
//                                else if (Mapsizes2[mapn].Type == MapType.Weak)
//                                {
//                                    if (byteTile[0] == 1 && (byteTile[2] == 1 || byteTile[2] == 2 || byteTile[2] == 3 || byteTile[2] == 4 || byteTile[2] == 7)
//                                        || byteTile[0] == 2 || byteTile[0] == 3 || byteTile[0] == 4)
//                                        tempp.IsWall = true;
//                                }
//                                tile.tiles[j, i] = tempp;
//                            }
//                    }
//                    loadedmaps.Add(mapn, tile);
//                }
//            }
//        }
//    }
//}
