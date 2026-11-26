using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKCamelot.script.monster
{
    public static class SpawnerMonster
    {
        public static void Spawn()
        {
            for (int i = 0; i < Settings.VillageName.Count; i++)
            {
                /* public static void AddMonster(LKCamelot.script.monster.Monster mob)
                 * public static void AddMonster(string mob, int channel, string map, List<Point2D> list, bool randomLoc)
                 * public static void AddMonster(string mob, int channel, string map, Point2D loc, bool randomLoc)
                 * public static void AddMonster(string mob, int channel, string map, Point2D loc, uint count, uint range, bool randomLoc = true)
                 * public static void AddMonster(string mob, int channel, string map, uint count, bool randomLoc = true)
                 * public static void AddMonster(string mob, int channel, string map, Point2D from, Point2D to, uint count, bool randomLoc = true)
                 * 
                 * mob : 몬스터의 클래스 이름
                 * channel : 마을 번호
                 * map : 스폰 맵
                 * loc : 스폰 기준 좌표
                 * count : 스폰몹의 갯수
                 * range : 기준좌표를 기준으로 범위만큼 스폰범위를 지정
                 * randomLoc : 몬스터를 랜덤좌표로 생성할지 여부
                 * 
                 * ex> model.Spawnere.AddMonster("Pigmy", 1, "초보", new Point2D(9, 25), 1, 1, true);
                 * Pigmy몹을 마을번호 1번에 초보맵에 9,25좌표에 1개를 스폰하고 범위는 1로 랜덤좌표로 몹을 생성한다.
                 * 
                 * ex> model.Spawnere.AddMonster("Beginn_MadDob", 1, "초보", new Point2D(9, 25), 5, 3, true);
                 * Beginn_MadDob몹을 마을번호 1번 초보맵에 9,25좌표를 기준으로 3범위내에 몹을 5마리를 생성하고 랜덤젠으로 스폰한다..
                 * 
                 * ex> model.Spawnere.AddMonster("Pigmy", i, MapName, 30, true);
                 * Pigmy몹을 마을번호 i번 MapName맵에 30마리를 랜덤젠으로 소환한다.
                 */

                model.Spawnere.AddMonster("Pigmy", i, "초보", new Point2D(9, 25), 1, 1, true);
                model.Spawnere.AddMonster("Pigmy", i, "초보", new Point2D(9, 38), 1, 1, true);
                model.Spawnere.AddMonster("Pigmy", i, "초보", new Point2D(5, 43), 1, 1, true);
                model.Spawnere.AddMonster("Pigmy", i, "초보", new Point2D(4, 45), 1, 1, true);
                model.Spawnere.AddMonster("Pigmy", i, "초보", new Point2D(7, 45), 1, 1, true);

                model.Spawnere.AddMonster("PigmyKing", i, "Weakly3", new Point2D(35, 35), 1, 1, true);

                model.Spawnere.AddMonster("ItemPigmy", i, "ItemPigmy3", new Point2D(59, 19), 1, 1, true);
                model.Spawnere.AddMonster("ItemZombie", i, "ItemZomby3", new Point2D(46, 28), 1, 1, true);
                model.Spawnere.AddMonster("ItemSkel", i, "ItemSkel3", new Point2D(61, 29), 1, 1, true);
                model.Spawnere.AddMonster("ItemButcher", i, "ItemButcher3", new Point2D(63, 36), 1, 1, true);
                model.Spawnere.AddMonster("ItemMummy", i, "ItemMummy3", new Point2D(63, 36), 1, 1, true);

                model.Spawnere.AddMonster("ItemStone", i, "ItemStone4", new Point2D(53, 40), 1, 1, true);

                model.Spawnere.AddMonster("WildDog", i, "초보", new Point2D(12, 8), 1, 1, true);
                model.Spawnere.AddMonster("WildDog", i, "초보", new Point2D(6, 4), 1, 1, true);
                model.Spawnere.AddMonster("WildDog", i, "초보", new Point2D(15, 4), 1, 1, true);
                model.Spawnere.AddMonster("WildDog", i, "초보", new Point2D(12, 10), 1, 1, true);
                model.Spawnere.AddMonster("WildDog", i, "초보", new Point2D(13, 30), 1, 1, true);

                model.Spawnere.AddMonster("WarButcher", i, "010", new Point2D(15, 16), 1, 1, true);

                model.Spawnere.AddMonster("Skel_LargeSkel", i, "스켈3",
                                                                    new List<Point2D> { new Point2D(24, 30), new Point2D(41, 33), new Point2D(38, 25), new Point2D(31, 12) }, 
                                                                    true);

                model.Spawnere.AddMonster("Ball", i, "축구장", new Point2D(28, 16), 1, 1, true);




                string MapName = "초보";
                model.Spawnere.AddMonster("Beginn_MadDog", i, MapName, new Point2D(47, 7), 5, 6, true);
                model.Spawnere.AddMonster("Beginn_MadDog", i, MapName, new Point2D(12, 8), 5, 7, true);
                model.Spawnere.AddMonster("Beginn_MadDog", i, MapName, new Point2D(12, 30), 5, 7, true);
                model.Spawnere.AddMonster("Beginn_MadDog", i, MapName, new Point2D(10, 49), 5, 7, true);
                model.Spawnere.AddMonster("Beginn_MadDog", i, MapName, new Point2D(51, 29), 5, 7, true);
                model.Spawnere.AddMonster("Beginn_MadDog", i, MapName, new Point2D(29, 52), 5, 7, true);


                #region 위클리
                MapName = "Weakly1";
                model.Spawnere.AddMonster("Pigmy", i, MapName, 30, true);

                MapName = "Weakly2";
                model.Spawnere.AddMonster("Pigmy", i, MapName, 20, true);
                model.Spawnere.AddMonster("PigmyRed", i, MapName, 30, true);
                #endregion


                #region 스켈
                MapName = "스켈1";
                model.Spawnere.AddMonster("Skeleton", i, MapName, 30, true);

                MapName = "스켈2";
                model.Spawnere.AddMonster("SkeletonRed", i, MapName, 30, true);
                model.Spawnere.AddMonster("Skeleton", i, MapName, 10, true);
                
                MapName = "스켈3";
                model.Spawnere.AddMonster("Skeleton", i, MapName, 30, true);
                #endregion


                #region Level
                MapName = "010";
                model.Spawnere.AddMonster("Butcher", i, MapName, new Point2D(8, 9), new Point2D(31, 30), 10, true);
                model.Spawnere.AddMonster("WarBadGirl", i, MapName, new Point2D(101, 5), new Point2D(133, 32), 10, true);
                model.Spawnere.AddMonster("HeadCutter", i, MapName, new Point2D(54, 6), new Point2D(100, 27), 10, true);
                model.Spawnere.AddMonster("WarButcher", i, MapName, new Point2D(54, 6), new Point2D(100, 27), 10, true);

                MapName = "020";
                model.Spawnere.AddMonster("BoneDry", i, MapName, new Point2D(52, 41), new Point2D(99, 99), 10, true);
                model.Spawnere.AddMonster("BonePatrol", i, MapName, new Point2D(52, 51), new Point2D(99, 0), 10, true);
                model.Spawnere.AddMonster("Viking", i, MapName, new Point2D(0, 0), new Point2D(52, 41), 15, true);
                model.Spawnere.AddMonster("SkelEscottor", i, MapName, new Point2D(0, 70), new Point2D(50, 99), 10, true);

                MapName = "030";
                model.Spawnere.AddMonster("Viking", i, MapName, new Point2D(52, 41), new Point2D(99, 99), 10, true);
                model.Spawnere.AddMonster("SkelFighter", i, MapName, new Point2D(52, 41), new Point2D(99, 0), 10, true);
                model.Spawnere.AddMonster("WarBone", i, MapName, new Point2D(0, 0), new Point2D(52, 41), 15, true);
                model.Spawnere.AddMonster("SkelEscottor", i, MapName, new Point2D(0, 70), new Point2D(50, 99), 10, true);

                MapName = "040";
                model.Spawnere.AddMonster("SkelEscottor", i, MapName, new Point2D(52, 41), new Point2D(99, 99), 10, true);
                model.Spawnere.AddMonster("LargeSkel", i, MapName, new Point2D(52, 41), new Point2D(99, 0), 10, true);
                model.Spawnere.AddMonster("GeneralSkel", i, MapName, new Point2D(0, 0), new Point2D(52, 41), 15, true);
                model.Spawnere.AddMonster("Stone", i, MapName, new Point2D(0, 70), new Point2D(50, 99), 10, true);

                MapName = "050";
                model.Spawnere.AddMonster("Stone", i, MapName, new Point2D(52, 41), new Point2D(99, 99), 10, true);
                model.Spawnere.AddMonster("GreatStone", i, MapName, new Point2D(52, 41), new Point2D(99, 0), 10, true);
                model.Spawnere.AddMonster("WarDummy", i, MapName, new Point2D(0, 0), new Point2D(52, 41), 15, true);
                model.Spawnere.AddMonster("Dummy", i, MapName, new Point2D(0, 70), new Point2D(50, 99), 10, true);

                MapName = "060";
                model.Spawnere.AddMonster("WarDummy", i, MapName, new Point2D(52, 41), new Point2D(119, 99), 10, true);
                model.Spawnere.AddMonster("Mummy060", i, MapName, new Point2D(52, 41), new Point2D(119, 0), 10, true);
                model.Spawnere.AddMonster("StoneGolem060", i, MapName, new Point2D(0, 0), new Point2D(52, 41), 15, true);
                model.Spawnere.AddMonster("HardBoild060", i, MapName, new Point2D(0, 70), new Point2D(50, 99), 10, true);

                MapName = "070";
                model.Spawnere.AddMonster("Mummy060", i, MapName, 15, true);
                model.Spawnere.AddMonster("HardBoild060", i, MapName, 20, true);
                model.Spawnere.AddMonster("Hag", i, MapName, 20, true);

                MapName = "080";
                model.Spawnere.AddMonster("StoneGolem060", i, MapName, 25, true);
                model.Spawnere.AddMonster("HardBoild060", i, MapName, 20, true);
                model.Spawnere.AddMonster("Hag", i, MapName, 20, true);
                #endregion



                MapName = "ItemPigmy1";
                model.Spawnere.AddMonster("Pigmy", i, MapName, 15, true);

                MapName = "ItemPigmy2";
                model.Spawnere.AddMonster("PigmyRed", i, MapName, 15, true);

                MapName = "ItemPigmy3";
                model.Spawnere.AddMonster("PigmyBlue", i, MapName, 10, true);
                model.Spawnere.AddMonster("PigmyGreen", i, MapName, 10, true);




                MapName = "ItemZomby1";
                model.Spawnere.AddMonster("Zombie", i, MapName, 15, true);

                MapName = "ItemZomby2";
                model.Spawnere.AddMonster("ZombieRed", i, MapName, 15, true);

                MapName = "ItemZomby3";
                model.Spawnere.AddMonster("ZombieGreen", i, MapName, 10, true);
                model.Spawnere.AddMonster("ZombieBlue", i, MapName, 10, true);




                MapName = "ItemSkel1";
                model.Spawnere.AddMonster("Skeleton", i, MapName, 15, true);

                MapName = "ItemSkel2";
                model.Spawnere.AddMonster("BoneDryRed", i, MapName, 10, true);
                model.Spawnere.AddMonster("BoneDryGreen", i, MapName, 10, true);

                MapName = "ItemSkel3";
                model.Spawnere.AddMonster("BoneDryBlue", i, MapName, 15, true);




                MapName = "ItemButcher1";
                model.Spawnere.AddMonster("iButcher", i, MapName, 15, true);

                MapName = "ItemButcher2";
                model.Spawnere.AddMonster("iButcherRed", i, MapName, 15, true);

                MapName = "ItemButcher3";
                model.Spawnere.AddMonster("iButcherBlue", i, MapName, 10, true);
                model.Spawnere.AddMonster("iButcherGreen", i, MapName, 10, true);



                MapName = "ItemMummy1";
                model.Spawnere.AddMonster("iMummy", i, MapName, 30, true);

                MapName = "ItemMummy2";
                model.Spawnere.AddMonster("iMummyRed", i, MapName, 15, true);

                MapName = "ItemMummy3";
                model.Spawnere.AddMonster("iMummyGreen", i, MapName, 10, true);
                model.Spawnere.AddMonster("iMummyBlue", i, MapName, 10, true);



                MapName = "ItemStone1";
                model.Spawnere.AddMonster("iStone", i, MapName, 15, true);

                MapName = "ItemStone2";
                model.Spawnere.AddMonster("iStoneRed", i, MapName, 15, true);

                MapName = "ItemStone3";
                model.Spawnere.AddMonster("iStoneGreen", i, MapName, 15, true);
                model.Spawnere.AddMonster("iStoneBlue", i, MapName, 15, true);



                MapName = "ItemDummy1";
                model.Spawnere.AddMonster("iDummy", i, MapName, 15, true);

                MapName = "ItemDummy2";
                model.Spawnere.AddMonster("iDummyRed", i, MapName, 15, true);

                MapName = "ItemDummy3";
                model.Spawnere.AddMonster("iDummyGreen", i, MapName, 15, true);

                MapName = "ItemDummy4";
                model.Spawnere.AddMonster("iDummyBlue", i, MapName, 10, true);
                
                MapName = "ItemDummy5";
                model.Spawnere.AddMonster("iWarDummy", i, MapName, 10, true);
                model.Spawnere.AddMonster("ItemDummy", i, MapName, 1, true);



                MapName = "ItemWarDummy1";
                model.Spawnere.AddMonster("iWarDummy", i, MapName, 15, true);

                MapName = "ItemWarDummy2";
                model.Spawnere.AddMonster("iWarDummyRed", i, MapName, 15, true);

                MapName = "ItemWarDummy3";
                model.Spawnere.AddMonster("iWarDummyGreen", i, MapName, 10, true);

                MapName = "ItemWarDummy4";
                model.Spawnere.AddMonster("iWarDummyBlue", i, MapName, 10, true);

                MapName = "ItemWarDummy5";
                model.Spawnere.AddMonster("ItemWarDummy", i, MapName, 1, true);



                MapName = "ItemHardboil1";
                model.Spawnere.AddMonster("iHardBoil", i, MapName, 15, true);

                MapName = "ItemHardboil2";
                model.Spawnere.AddMonster("iHardBoilRed", i, MapName, 15, true);

                MapName = "ItemHardboil3";
                model.Spawnere.AddMonster("iHardBoilGreen", i, MapName, 10, true);

                MapName = "ItemHardboil4";
                model.Spawnere.AddMonster("iHardBoilBlue", i, MapName, 10, true);

                MapName = "ItemHardboil5";
                model.Spawnere.AddMonster("iHardBoilBlue", i, MapName, 50, true);
                model.Spawnere.AddMonster("ItemHardBoil", i, MapName, 1, true);



                MapName = "ItemGolem1";
                model.Spawnere.AddMonster("GolemGuard", i, MapName, 15, true);

                MapName = "ItemGolem2";
                model.Spawnere.AddMonster("GolemGuard", i, MapName, 15, true);

                MapName = "ItemGolem3";
                model.Spawnere.AddMonster("GolemGuard", i, MapName, 20, true);

                MapName = "ItemGolem4";
                model.Spawnere.AddMonster("GolemGuard", i, MapName, 20, true);

                MapName = "ItemGolem5";
                model.Spawnere.AddMonster("GolemGuard", i, MapName, 50, true);
                model.Spawnere.AddMonster("ItemGolem", i, MapName, 1, true);



                MapName = "TreasureLand";
                model.Spawnere.AddMonster("HoundsOfDoom", i, MapName, 3, true);
                model.Spawnere.AddMonster("BoneTrio1", i, MapName, 6, true);
                model.Spawnere.AddMonster("BoneTrio2", i, MapName, 6, true);
                model.Spawnere.AddMonster("BoneTrio3", i, MapName, 6, true);
                model.Spawnere.AddMonster("BioRubber", i, MapName, 5, true);
                model.Spawnere.AddMonster("Leo", i, MapName, 5, true);
                model.Spawnere.AddMonster("SmokeGiant", i, MapName, 5, true);
                model.Spawnere.AddMonster("Spot", i, MapName, 5, true);



                MapName = "Biggun1";
                model.Spawnere.AddMonster("Rowedy", i, MapName, 30, true);
                model.Spawnere.AddMonster("Biggun", i, MapName, 25, true);

                MapName = "Biggun2";
                model.Spawnere.AddMonster("Rowedy", i, MapName, 30, true);
                model.Spawnere.AddMonster("Biggun", i, MapName, 35, true);






                #region Miro
                MapName = "Miro1";
                #region 1구역
                for (int x = 0; x < 10; x++)
                {
                    Point2D tempmob = null;
                    while ((tempmob = Map.SpawnPointFixed(i, MapName, 21, 23, 20)) == null) ;
                    model.Spawnere.AddMonster(new script.monster.Cyclops(model.Spawnere.temps++, i, tempmob.X, tempmob.Y, MapName));
                }
                for (int x = 0; x < 10; x++)
                {
                    Point2D tempmob = null;
                    while ((tempmob = Map.SpawnPointFixed(i, MapName, 21, 23, 20)) == null) ;
                    model.Spawnere.AddMonster(new script.monster.OneEye(model.Spawnere.temps++, i, tempmob.X, tempmob.Y, MapName));
                }
                #endregion

                #region 2구역
                for (int x = 0; x < 10; x++)
                {
                    Point2D tempmob = null;
                    while ((tempmob = Map.SpawnPointFixed(i, MapName, 60, 60, 20)) == null) ;
                    model.Spawnere.AddMonster(new script.monster.Cyclops(model.Spawnere.temps++, i, tempmob.X, tempmob.Y, MapName));
                }
                for (int x = 0; x < 10; x++)
                {
                    Point2D tempmob = null;
                    while ((tempmob = Map.SpawnPointFixed(i, MapName, 60, 60, 20)) == null) ;
                    model.Spawnere.AddMonster(new script.monster.OneEye(model.Spawnere.temps++, i, tempmob.X, tempmob.Y, MapName));
                }
                #endregion

                #region 3구역
                for (int x = 0; x < 10; x++)
                {
                    Point2D tempmob = null;
                    while ((tempmob = Map.SpawnPointFixed(i, MapName, 17, 95, 20)) == null) ;
                    model.Spawnere.AddMonster(new script.monster.Cyclops(model.Spawnere.temps++, i, tempmob.X, tempmob.Y, MapName));
                }
                for (int x = 0; x < 10; x++)
                {
                    Point2D tempmob = null;
                    while ((tempmob = Map.SpawnPointFixed(i, MapName, 17, 95, 20)) == null) ;
                    model.Spawnere.AddMonster(new script.monster.OneEye(model.Spawnere.temps++, i, tempmob.X, tempmob.Y, MapName));
                }
                #endregion

                #region 4구역
                for (int x = 0; x < 10; x++)
                {
                    Point2D tempmob = null;
                    while ((tempmob = Map.SpawnPointFixed(i, MapName, 63, 115, 20)) == null) ;
                    model.Spawnere.AddMonster(new script.monster.Cyclops(model.Spawnere.temps++, i, tempmob.X, tempmob.Y, MapName));
                }
                for (int x = 0; x < 10; x++)
                {
                    Point2D tempmob = null;
                    while ((tempmob = Map.SpawnPointFixed(i, MapName, 63, 115, 20)) == null) ;
                    model.Spawnere.AddMonster(new script.monster.OneEye(model.Spawnere.temps++, i, tempmob.X, tempmob.Y, MapName));
                }
                #endregion

                #region 5구역
                for (int x = 0; x < 10; x++)
                {
                    Point2D tempmob = null;
                    while ((tempmob = Map.SpawnPointFixed(i, MapName, 61, 171, 20)) == null) ;
                    model.Spawnere.AddMonster(new script.monster.Cyclops(model.Spawnere.temps++, i, tempmob.X, tempmob.Y, MapName));
                }
                for (int x = 0; x < 10; x++)
                {
                    Point2D tempmob = null;
                    while ((tempmob = Map.SpawnPointFixed(i, MapName, 61, 171, 20)) == null) ;
                    model.Spawnere.AddMonster(new script.monster.OneEye(model.Spawnere.temps++, i, tempmob.X, tempmob.Y, MapName));
                }
                #endregion
                #endregion




                MapName = "골램";
                model.Spawnere.AddMonster("GolemGuard", i, MapName, 15, true);

                MapName = "Golem1";
                model.Spawnere.AddMonster("GolemGuard", i, MapName, 10, true);
                model.Spawnere.AddMonster("vIronGolem", i, MapName, 6, true);

                MapName = "Golem2";
                model.Spawnere.AddMonster("GolemGuard", i, MapName, 5, true);
                model.Spawnere.AddMonster("vIronGolem", i, MapName, 10, true);
                model.Spawnere.AddMonster("SuperIronGolem", i, MapName, 10, true);

                MapName = "Golem3";
                model.Spawnere.AddMonster("DevilKid", i, MapName, 8, true);
                model.Spawnere.AddMonster("vIronGolem", i, MapName, 14, true);
                model.Spawnere.AddMonster("SuperIronGolem", i, MapName, 10, true);

                MapName = "Golem12";
                model.Spawnere.AddMonster("Rioter", i, MapName, 70, true);



                MapName = "Great";
                model.Spawnere.AddMonster("Phantom", i, MapName, 8, true);
                model.Spawnere.AddMonster("Temptress", i, MapName, 8, true);
                model.Spawnere.AddMonster("Solo", i, MapName, 8, true);
                model.Spawnere.AddMonster("FireDragon", i, MapName, 1, true);
                model.Spawnere.AddMonster("Violator", i, MapName, 6, true);
                model.Spawnere.AddMonster("PrincessShea", i, MapName, 3, true);
                model.Spawnere.AddMonster("QueenSpriss", i, MapName, 3, true);
                model.Spawnere.AddMonster("Centeur", i, MapName, 12, true);



                MapName = "지구라트1";
                model.Spawnere.AddMonster("Gargoyle", i, MapName, 30, true);
                model.Spawnere.AddMonster("SKELGIANT", i, MapName, 20, true);
                model.Spawnere.AddMonster("DevilGuard", i, MapName, 8, true);
                
                MapName = "지구라트2";
                model.Spawnere.AddMonster("Gargoyle", i, MapName, 25, true);
                model.Spawnere.AddMonster("SKELGIANT", i, MapName, 20, true);
                model.Spawnere.AddMonster("DevilGuard", i, MapName, 15, true);

                MapName = "지구라트3";
                model.Spawnere.AddMonster("QueenSpriss", i, MapName, 65, true);
                model.Spawnere.AddMonster("Centeur", i, MapName, 20, true);
                
                MapName = "지구라트4";
                model.Spawnere.AddMonster("Bloodwing", i, MapName, 15, true);
                model.Spawnere.AddMonster("Necromancer", i, MapName, 30, true);
                model.Spawnere.AddMonster("LoupGarou", i, MapName, 20, true);
                model.Spawnere.AddMonster("Ninja", i, MapName, 20, true);
                
                MapName = "지구라트5";
                model.Spawnere.AddMonster("GhostKnight", i, MapName, 25, true);
                model.Spawnere.AddMonster("Devil", i, MapName, 25, true);
                model.Spawnere.AddMonster("BoneTrio1", i, MapName, 20, true);




                MapName = "가디언";
                model.Spawnere.AddMonster("Saxon", i, MapName, 20, true);
                model.Spawnere.AddMonster("Minotaur", i, MapName, 10, true);
                model.Spawnere.AddMonster("BoneTrio2", i, MapName, 10, true);
                model.Spawnere.AddMonster("Guardian_Devil", i, MapName, new Point2D(51, 51), 1, 1, true);

                MapName = "가디언1";
                model.Spawnere.AddMonster("Guardian_SmokeGiant", i, MapName, new Point2D(57, 28), 1, 1, true);
                model.Spawnere.AddMonster("Guardian_SmokeGiant", i, MapName, new Point2D(23, 50), 1, 1, true);
                model.Spawnere.AddMonster("Guardian_SmokeGiant", i, MapName, new Point2D(78, 49), 1, 1, true);
                model.Spawnere.AddMonster("Guardian_SmokeGiant", i, MapName, new Point2D(57, 68), 1, 1, true);                
                model.Spawnere.AddMonster("Mercenary", i, MapName, 20, true);
                model.Spawnere.AddMonster("Saxon", i, MapName, 20, true);




                MapName = "광산0";
                model.Spawnere.AddMonster("Iron", i, MapName, 20, true);
                model.Spawnere.AddMonster("Copper", i, MapName, 20, true);
                model.Spawnere.AddMonster("Lead", i, MapName, 20, true);

                MapName = "광산1";
                model.Spawnere.AddMonster("Aluminium", i, MapName, 10, true);
                model.Spawnere.AddMonster("Bronze", i, MapName, 10, true);
                model.Spawnere.AddMonster("Silver", i, MapName, 10, true);
                model.Spawnere.AddMonster("Cadmium", i, MapName, 10, true);
                model.Spawnere.AddMonster("Topaz", i, MapName, 10, true);

                MapName = "광산2";
                model.Spawnere.AddMonster("Quartz", i, MapName, 10, true);
                model.Spawnere.AddMonster("Tungsten", i, MapName, 10, true);
                model.Spawnere.AddMonster("Ruby", i, MapName, 10, true);
                model.Spawnere.AddMonster("Crystal", i, MapName, 10, true);

                MapName = "광산3";
                model.Spawnere.AddMonster("MoonStone", i, MapName, 10, true);
                model.Spawnere.AddMonster("Steel", i, MapName, 10, true);
                model.Spawnere.AddMonster("Garnet", i, MapName, 10, true);
                model.Spawnere.AddMonster("Nickel", i, MapName, 10, true);
                model.Spawnere.AddMonster("SunStone", i, MapName, 10, true);

                MapName = "광산4";
                model.Spawnere.AddMonster("Gold", i, MapName, 10, true);
                model.Spawnere.AddMonster("Zirconium", i, MapName, 10, true);
                model.Spawnere.AddMonster("LeopardJesper", i, MapName, 10, true);
                model.Spawnere.AddMonster("Emerald", i, MapName, 10, true);

                MapName = "광산5";
                model.Spawnere.AddMonster("Cobalt", i, MapName, 10, true);
                model.Spawnere.AddMonster("BloodStone", i, MapName, 10, true);
                model.Spawnere.AddMonster("TigersEye", i, MapName, 10, true);
                model.Spawnere.AddMonster("Jade", i, MapName, 10, true);
                model.Spawnere.AddMonster("Titanium", i, MapName, 10, true);

                MapName = "광산6";
                model.Spawnere.AddMonster("헬리도오", i, MapName, 10, true);
                model.Spawnere.AddMonster("Pearl", i, MapName, 10, true);
                model.Spawnere.AddMonster("DarkStone", i, MapName, 10, true);
                model.Spawnere.AddMonster("Platinum", i, MapName, 10, true);
                
                MapName = "광산7";
                model.Spawnere.AddMonster("Amber", i, MapName, 10, true);
                model.Spawnere.AddMonster("짱돌", i, MapName, 10, true);
                model.Spawnere.AddMonster("AquaMarine", i, MapName, 10, true);
                model.Spawnere.AddMonster("Sapphire", i, MapName, 10, true);
                model.Spawnere.AddMonster("Alloy", i, MapName, 10, true);

                MapName = "광산8";
                model.Spawnere.AddMonster("Amethyst", i, MapName, 10, true);
                model.Spawnere.AddMonster("CatsEye", i, MapName, 10, true);
                model.Spawnere.AddMonster("Coral", i, MapName, 10, true);
                model.Spawnere.AddMonster("BlackPearl", i, MapName, 10, true);

                MapName = "초보광산";
                model.Spawnere.AddMonster("Turquoise", i, MapName, 10, true);
                model.Spawnere.AddMonster("Opal", i, MapName, 10, true);
                model.Spawnere.AddMonster("SuperAlloy", i, MapName, 10, true);
                model.Spawnere.AddMonster("Diamond", i, MapName, 10, true);
                model.Spawnere.AddMonster("GrateSapphire", i, MapName, 10, true);
                model.Spawnere.AddMonster("마왕철", i, MapName, 10, true);
                model.Spawnere.AddMonster("GoldenDiamond", i, MapName, 10, true);
                



                MapName = "아발론";
                model.Spawnere.AddMonster("StrAbalStone", i, MapName, 20, true);
                model.Spawnere.AddMonster("MenAbalStone", i, MapName, 20, true);
                model.Spawnere.AddMonster("DexAbalStone", i, MapName, 20, true);
                model.Spawnere.AddMonster("VitAbalStone", i, MapName, 20, true);
                

                MapName = "Start";
                model.Spawnere.AddMonster("Venture_MadDog", i, MapName, 30, true);



                MapName = "스콜피온";
                model.Spawnere.AddMonster("Scorpian", i, MapName, 30, true);
                model.Spawnere.AddMonster("BloodShot", i, MapName, 30, true);

                MapName = "바이킹";
                model.Spawnere.AddMonster("vViking", i, MapName, 30, true);
                model.Spawnere.AddMonster("vButcher", i, MapName, 30, true);
                

                MapName = "동굴1층";
                model.Spawnere.AddMonster("Donggul_Medusa", i, MapName, 30, true);
                model.Spawnere.AddMonster("Donggul_TwinCobra", i, MapName, 30, true);

                MapName = "동굴2층";
                model.Spawnere.AddMonster("Donggul_Deadlymessenger", i, MapName, 30, true);
                model.Spawnere.AddMonster("Donggul_Wildmonk", i, MapName, 30, true);

                MapName = "동굴3층";
                model.Spawnere.AddMonster("Donggul_Medusa", i, MapName, 25, true);
                model.Spawnere.AddMonster("Donggul_TwinCobra", i, MapName, 25, true);
                model.Spawnere.AddMonster("Donggul_Deadlymessenger", i, MapName, 25, true);
                model.Spawnere.AddMonster("Donggul_Wildmonk", i, MapName, 25, true);



                MapName = "Qweak1";
                model.Spawnere.AddMonster("Qweak_Hag", i, MapName, 80, true);


                MapName = Settings.VillageName.ElementAt(i) + ",23";
                model.Spawnere.AddMonster("Leo", i, MapName, 3, true);
            }
        }
    }
}
