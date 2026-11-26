using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace LKCamelot.model
{
    public static class Spawnere
    {

        static int NPCSerial = 1;
        public static int SignSerial = 100;
        public static void NPCSpawn()
        {
            if (World.NewNpcs == null)
            {
                World.NewNpcs = new System.Collections.Concurrent.ConcurrentDictionary<int, script.npc.BaseNPC>[Settings.VillageName.Count];

                for (int i = 0; i < Settings.VillageName.Count(); i++)
                {
                    World.NewNpcs[i] = new System.Collections.Concurrent.ConcurrentDictionary<int, script.npc.BaseNPC>();

                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.Arnold(NPCSerial++, i, "대장간", new Point2D(16, 10)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.Employee(NPCSerial++, i, Settings.VillageName.ElementAt(i) + ",32", new Point2D(128, 91)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.Loen(NPCSerial++, i, "시약상", new Point2D(13, 8)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.Aron(NPCSerial++, i, Settings.VillageName.ElementAt(i) + ",32", new Point2D(98, 57)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.Boy(NPCSerial++, i, "대장간", new Point2D(13, 11)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.Alias(NPCSerial++, i, Settings.VillageName.ElementAt(i) + ",32", new Point2D(95, 160)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.Employee2(NPCSerial++, i, Settings.VillageName.ElementAt(i) + ",32", new Point2D(74, 94)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.Quester(NPCSerial++, i, "퀘스트", new Point2D(2, 7)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.PKKing(NPCSerial++, i, "대전장", new Point2D(2, 9)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.Bronze(NPCSerial++, i, Settings.VillageName.ElementAt(i) + ",32", new Point2D(99, 114)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.RockPaperScissors(NPCSerial++, i, "대전장", new Point2D(9, 2)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.RestEmployee(NPCSerial++, i, "휴게실", new Point2D(14, 19)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.Quiz(NPCSerial++, i, "대전장", new Point2D(10, 2)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.Exchange(NPCSerial++, i, "교환", new Point2D(46, 17)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.Exchange(NPCSerial++, i, "교환", new Point2D(17, 36)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.Exchange(NPCSerial++, i, "교환", new Point2D(66, 36)));
                    World.NewNpcs[i].TryAdd(NPCSerial, new script.npc.Exchange(NPCSerial++, i, "교환", new Point2D(40, 63)));

                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.Sign(SignSerial++, i, "ItemVillage", new Point2D(97, 106), new Point2D(186, 5), 1, "아이템스켈(185:5)"));
                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.Sign(SignSerial++, i, "ItemVillage", new Point2D(99, 108), new Point2D(193, 30), 1, "아이템부처(193:30)"));
                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.Sign(SignSerial++, i, "ItemVillage", new Point2D(99, 113), new Point2D(177, 161), 3, "아이템스톤(177:161)"));
                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.Sign(SignSerial++, i, "ItemVillage", new Point2D(97, 114), new Point2D(193, 187), 3, "아이템머미(193:187)"));
                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.Sign(SignSerial++, i, "ItemVillage", new Point2D(93, 114), new Point2D(7, 179), 4, "아이템골렘(7:179)"));
                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.Sign(SignSerial++, i, "ItemVillage", new Point2D(90, 114), new Point2D(38, 143), 5, "아이템워더미(38:143)"));
                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.Sign(SignSerial++, i, "ItemVillage", new Point2D(88, 112), new Point2D(23, 136), 5, "아이템더미(23:136)"));
                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.Sign(SignSerial++, i, "ItemVillage", new Point2D(88, 107), new Point2D(13, 45), 7, "아이템하드보일드(13:45)"));
                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.Sign(SignSerial++, i, "ItemVillage", new Point2D(91, 105), new Point2D(2, 19), 7, "아이템피그미(2:19)"));
                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.Sign(SignSerial++, i, "ItemVillage", new Point2D(94, 105), new Point2D(25, 7), 7, "아이템좀비(25:7)"));

                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.Sign(SignSerial++, i, "가보자", new Point2D(24, 31), null, 4, "돌기둥사이로 들어가세요."));
                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.Sign(SignSerial++, i, "가보자", new Point2D(21, 23), null, 6, "바이킹으로 가는길."));
                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.Sign(SignSerial++, i, "가보자", new Point2D(30, 29), null, 2, "아이언 골렘가는길."));
                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.Sign(SignSerial++, i, "가보자", new Point2D(23, 21), null, 0, "스콜피온 골렘가는길."));

                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.BoardSign(SignSerial++, i, Settings.VillageName.ElementAt(i) + ",32", new Point2D(129, 101), 2, "노점상은 간단한 갑옷과 무기를 팝니다. 옆에 놓인 물건을 집으세요"));
                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.BoardSign(SignSerial++, i, Settings.VillageName.ElementAt(i) + ",32", new Point2D(168, 85), 2, "노상방뇨 금지"));
                    World.NewNpcs[i].TryAdd(SignSerial, new script.npc.BoardSign(SignSerial++, i, Settings.VillageName.ElementAt(i) + ",32", new Point2D(141, 158), 2, "아이템보관및시약을 판매하는 곳입니다. @이동 로엔 하세요"));
                }

            }
        }

        public static int temps = 300;
        public static void Spawn()
        {
            LKCamelot.GameData.getInstance().ResetMonster();


            string MapName = string.Empty;
            if (World.NewMonsters == null)
                World.NewMonsters = new System.Collections.Concurrent.ConcurrentDictionary<Serial, script.monster.Monster>[Settings.VillageName.Count];

            for (int i = 0; i < Settings.VillageName.Count(); i++)
                World.NewMonsters[i] = new System.Collections.Concurrent.ConcurrentDictionary<Serial, script.monster.Monster>();


            script.monster.SpawnerMonster.Spawn();

            for (int i = 0; i < Settings.VillageName.Count(); i++)
                foreach (var mob in World.NewMonsters[i].ToList())
                    mob.Value.Alive = true;
        }


        public static void AddMonster(LKCamelot.script.monster.Monster mob)
        {
            if (World.NewMonsters[mob.m_Channel] == null)
                return;

            World.NewMonsters[mob.m_Channel].TryAdd(mob.m_Serial, mob);
        }

        public static void AddMonster(string mob, int channel, string map, List<Point2D> list, bool randomLoc)
        {
            script.monster.Monster Mob = (script.monster.Monster)Activator.CreateInstance(Type.GetType("LKCamelot.script.monster." + mob), (model.Serial)temps++, channel, map, list, randomLoc);

            if (Mob == null)
                return;

            AddMonster(Mob);
        }

        /// <summary>
        /// 몬스터를 해당좌표에 생성한다.
        /// </summary>
        /// <param name="mob">몹 클래스명</param>
        /// <param name="channel">마을 번호</param>
        /// <param name="map">맵 이름</param>
        /// <param name="loc">위치</param>
        /// <param name="randomLoc">랜덤젠 여부</param>
        public static void AddMonster(string mob, int channel, string map, Point2D loc, bool randomLoc)
        {
            List<Point2D> list = new List<Point2D>(){ new Point2D(loc.X, loc.Y) };
            AddMonster(mob, channel, map, list, randomLoc);
        }

        /// <summary>
        /// 기준좌표를 기준으로 해당범위 만큼 위치에 몹을 소환한다.
        /// </summary>
        /// <param name="mob">몹 클래스명</param>
        /// <param name="channel">마을 번호</param>
        /// <param name="map">맵 이름</param>
        /// <param name="loc">기준 위치</param>
        /// <param name="count">스폰 갯수</param>
        /// <param name="range">기준위치에서의 범위</param>
        /// <param name="randomLoc">랜덤젠 여부</param>
        public static void AddMonster(string mob, int channel, string map, Point2D loc, uint count, uint range, bool randomLoc = true)
        {
            for (int i = 0; i < count; i++)
            {
                Point2D tempmob = null;
                while ((tempmob = Map.SpawnPointFixed(channel, map, loc.X, loc.Y, (int)range)) == null) ;

                AddMonster(mob, channel, map, tempmob, randomLoc);
            }
        }

        /// <summary>
        /// 맵 전체에 몹을 소환한다.
        /// </summary>
        /// <param name="mob">몹 클래스명</param>
        /// <param name="channel">마을 번호</param>
        /// <param name="map">맵 이름</param>
        /// <param name="count">스폰 갯수</param>
        /// <param name="randomLoc">랜덤젠 여부</param>
        public static void AddMonster(string mob, int channel, string map, uint count, bool randomLoc = true)
        {
            for (int i = 0; i < count; i++)
            {
                Point2D tempmob = null;
                while ((tempmob = Map.SpawnPoint(channel, map)) == null) ;

                AddMonster(mob, channel, map, tempmob, randomLoc);
            }
        }

        /// <summary>
        /// 해당하는 범위(시작좌표~끝좌표)에 몬스터를 소환한다.
        /// </summary>
        /// <param name="mob">몹 클래스명</param>
        /// <param name="channel">마을 번호</param>
        /// <param name="map">맵 이름</param>
        /// <param name="from">시작 좌표</param>
        /// <param name="to">끝 좌표</param>
        /// <param name="count">몹 갯수</param>
        /// <param name="randomLoc">랜덤젠 여부</param>
        public static void AddMonster(string mob, int channel, string map, Point2D from, Point2D to, uint count, bool randomLoc = true)
        {
            for (int i = 0; i < count; i++)
            {
                Point2D tempmob = null;
                while ((tempmob = Map.SpawnPointFixed(channel, map, from.X, from.Y, to.X, to.Y)) == null);

                AddMonster(mob, channel, map, tempmob, randomLoc);
            }
        }
    }
}
