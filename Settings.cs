using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;




namespace LKCamelot
{
    public static class Settings
    {

        public const int Day = 24 * Hour, Hour = 60 * Minute, Minute = 60 * Second, Second = 1000;
        public const int 억 = 10000 * 만, 만 = 10000;

        public static string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\LKDB\\";
        public static string LogPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\LKDB\\Log\\";

        private static string DB_TableName = "lastking";
        private static string DB_UserName = "root"; //"admin";
        private static string DB_Password = "owner"; //"!fhdkxlaqb1"; !로아팀뷰1
        public static string DB_Conn = string.Format("Server=127.0.0.1;Database={0};Uid={1};Pwd={2};", DB_TableName, DB_UserName, DB_Password);

        public static string ServerName = "The Last Kingdom";
        public static string Version = "v2015-10-11-0001";
        public static string WelComeMessage = "마지막왕국에 오신걸 환영합니다.";
        public static string ServerIPAddress = "0.0.0.0";
        public static int CycleTime = 500;
#if DEBUG
        public static int ServerPort = 43333;
#else
        public static int ServerPort = 41380;
#endif
        public static int ChatServerPort = 9000;

        public static int WebPort = 43331;

        public static Point2D StartLoc = new Point2D(90, 176);
        public static string StartMap = ",32";
        public static Point2D StartGMLoc = new Point2D(168, 86);
        public static string StartGMMap = ",32";


        public static HashSet<string> WelComeNotice = new HashSet<string>()
        {
            "구마왕에 대해서 기억나는 부분은 카페에 올려주시면 반영합니다.",
            "<<<<<<< 테스트 서버 >>>>>>>",
            "1. 현거래 유저 압류조치",
            "2. 관련공지 [laking.tk ] 에서 확인",
            "3. 불량사용자 IP 벤 기능 업데이트",
        };
        public static HashSet<string> BegginerNotice = new HashSet<string>() 
        {
            "본 서버는 마왕복원을 초점으로 하고있습니다",
            "승급 캐릭터는 100일, 일반 캐릭터는 50일간 미접속 시 자동삭제되니 주의하세요.",
            "초보 사냥터는 \"@이동 초보\", \"@이동 위클리\" 입니다.",
        };

        // 타인에게 걸 수 있는 버프(와콘다, 버터플라이, 실버스크린)의 대상 판정 범위(칸).
        // 누른 좌표가 다른 유저의 발밑 칸 기준으로 이 범위 안이면 그 유저에게, 아니면 시전자 자신에게 건다.
        public static int BuffTargetRangeX = 1;         // 좌우
        public static int BuffTargetRangeUp = 2;        // 위쪽(몸통·머리)
        public static int BuffTargetRangeDown = 1;      // 아래쪽

        // 감속 마법(몬스터 대상). 이동속도 감소율(%)과 아이스백의 지속시간.
        // 유저의 걷기 속도는 서버가 검증하지 않으므로 유저에게는 감속이 걸리지 않는다.
        public static int HailStormSlowPercent = 30;
        public static int IceBagSlowPercent = 50;
        public static int IceBagSlowTime = 8 * Second;      // 1권 기준
        public static int IceBagSlowTimePl = 1 * Second;    // 권당 증가
        public static int SlowPercentMax = 90;              // 감속 상한(%)

        // 은신(TRANSPARENCY): 권당 투명도(%)와 지속시간. 샤프아이(SHARP EYE): 권당 벗기는 투명도(%)와 대상 판정 범위(칸).
        public static int HidePercentPl = 10;               // 1권 10% ~ 10권 100%
        public static int HideTime = 30 * Second;           // 1권 기준
        public static int HideTimePl = 10 * Second;         // 권당 증가 (10권 = 120초)
        public static int SharpEyePercentPl = 10;
        public static int SharpEyeRange = 2;                // 누른 좌표에서 이 범위 안의 은신 유저를 찾는다

        // 쇼크(몬스터 대상 지속 피해). 주기마다 마법 피해(30 + 권당 5)를 준다.
        public static int ShokeTickInterval = 1 * Second;   // 피해 주기
        public static int ShokeTime = 5 * Second;           // 지속시간, 1권 기준
        public static int ShokeTimePl = 1 * Second;         // 권당 증가 (12권 = 16초)
        public static int ShokeEffectSprite = 53;           // 걸린 몬스터에 표시되는 효과 번호

        // 불벽·전기벽의 몬스터 유인. 어그로가 끌린 몬스터가 벽 가운데에서 이 범위(칸) 안에 있으면 벽으로 들어간다.
        public static int WallLureRange = 6;
        public static int WallInfightChance = 10;           // 벽 위 몬스터가 공격 주기마다 가운데 몬스터를 공격할 확률(%)

        public static int AutoDeleteDaysPromo = 100;
        public static int AutoDeleteDaysNormal = 50;

        //도우미관련
        public static int HelperLevel = 15;             // 도우미 설정 레벨

        //길드관련
        public static int GuildJoinLevel = 50;          // 길드 가입 최소 레벨
        public static ulong GuildCreateGold = 30000000; // 길드 창설 비용
        public static int GuildCreateLevel = 0;         // 길드 창설 레벨
        public static bool GuildCreateAlarm = true;     // 길드 창설 알림
        public static int GuildMemberLimit = 25;        // 길드 인원 제한 설정


        //일반 설정
        public static int ShowRange = 18;               // 보여지는 범위 설정   
        public static bool LoginAlarm = true;           // 접속 알림
        public static bool GuildLoginAlarm = true;      // 길드 접속 알림
        public static bool TimeAlarm = true;            // 정각 시간 알림
        public static bool PromoAlarm = false;          // 승급 알림
        public static bool AutoAttackPickup = true;     // @자동 일때 자동 줍기 설정

        // 피케이
        public static int PKProtectLevel = 20;          // 피케이 보호 레벨
        public static bool PKAlarm = true;              // 피케이 알림
        public static long PKDelay = 60 * Minute;       // 빨갱이 딜레이
        public static long PKRedCount = 3;              // 빨갱이 피케이 수치
        public static long PrisonCount = 5;            // 빨갱이 감옥 수치
        public static int PKLevelRegester = 1;              // @신고 명령어 사용 불가능한 피케이수치 
        public static long PKRegesterTime = 10 * Minute;    // @신고 명령어 제한 시간
        public static float MacroKillXP = 0.0f;             // 매크로 사냥시 보상 피케이

        //게임 설정
        public static int SaveTime = 30 * Minute;       // 저장 시간
        public static short MaxLevel = 231;             // 만렙 설정 (7승11->231, 12승20-> 340)
        // 미분배 턴 안내 메시지 색상(UpdateChatBox의 앞 두 값)과 반복 주기(초).
        // 색상 값은 기존 시스템 안내에 쓰이던 (0x00, 0xff)를 그대로 썼다. 클라이언트에서 노란색이 아니면 이 두 값을 바꾼다.
        public static byte TurnNoticeColor1 = 0x00;
        public static byte TurnNoticeColor2 = 0xff;
        public static int TurnNoticeInterval = 180;

        // 전직 책(기사·검객·위저드·샤먼) 가격. 아론의 턴 초기화 비용도 이 값을 쓴다.
        public static ulong ClassBookPrice = 85000000;

        public static int TrunXP = 1000000;           // 턴 경험치 설정 (승급기준 : 100만)
        public static bool DieDropGold = true;          // 죽을 경우 골드 떨굼 여부 (10%)
        public static bool DieDropItem = false;         // 죽을 경우 아이템 떨굼 여부

        public static float[] XPrate = new float[2] { 1.0f, 0 };              // 경험치 배율
        public static float[] GOLDRate = new float[2] { 1.0f, 0 };            // 골드 배율
        public static float[] DropRate = new float[2] { 1.0f, 0 };            // 골드 배율
        public static float[] ABALRate = new float[2] { 1.0f, 0 };            // 아발론 배율
        public static long QuestTime = 30 * Minute;                         //퀘스트 입장 시간


        public static int PebbleSpwnTime = 1 * Minute;      // 조약돌 생성 시간
        public static int PebbleSpwnAmount = 10;            // 조약돌 생성 갯수
        public static int PebbleSpwnRange = 5;              // 조약돌 생성 범위 (반지름)
#if DEBUG
        public static bool OverlapLogin = true;            // 아이피 중복 접속 여부 ( False : 중복접속 방지, True : 중복접속허용)
#else
        public static bool OverlapLogin = true;            // 아이피 중복 접속 여부 ( False : 중복접속 방지, True : 중복접속허용)

#endif


        //이웃관련 설정
        public static int NeighborLevel = 30;           // 이웃마을 레벨 설정
        public static bool Neighbor = false;            // 이웃마을 설정
        public static bool IPNeighbor = false;          // 중복 아이피 이웃 설정

        //외치기 설정
        public static int PinkGlobalChat = 5;           // 빨치기 인원수
        public static int GrayGlobalChat = 20;          // 똥치기 인원수


        public static HashSet<string> GMS = new HashSet<string>()
        {
            "서버관리자",
            "박물관"
        };

        public static HashSet<string> VillageName = new HashSet<string>()
        {
            "영혼의마을",
            "정령의마을",
            //"새벽의마을",
            //"창공의마을",
            //"신비의땅",
            //"약속의땅",
            //"모험의땅",
            //"어둠의땅",
            //"미지의땅",
            //"시간마을",
            //"이웃마을",
            //"초보마을",
        };

        public static HashSet<string> NOPK = new HashSet<string>()
        {
            "휴게실",
            "영혼의마을",
            "정령의마을",
            //"새벽의마을",
            //"창공의마을",
            //"신비의땅",
            //"약속의땅",
            //"모험의땅",
            //"어둠의땅",
            //"미지의땅",
            //"시간마을",
            //"이웃마을",
            //"초보마을",
            "대장간",
            "시약상",
            "초보",
            "교환",
        };
        public static Dictionary<string, List<string>> NoSpell = new Dictionary<string, List<string>>()
        {
            { "TELEPORT", new List<string>{ "동굴 1층", "동굴 2층", "동굴 3층", 
                                            "아발론", "교환",
                                            "Qweak1", "Qweak2"} },  
        };
        public static HashSet<string> RestMap = new HashSet<string>()
        {
            "휴게실",
        };

        #region 욕 필터링
        public static Dictionary<string, string> ChatFilter = new Dictionary<string, string>()
        {
            {@"창고", "창.고"},
            {@"나눔", "나.눔"},
            {@"졋[\p{P}\d\s]{0,}밥", "약자"},
            {@"새[\p{P}\d\s]{0,}끼", "착한아이"},
            {@"슈[\p{P}\d\s]{0,}발", "신발"},
            {@"씨[\p{P}\d\s]{0,}발", "신발"},
            {@"쓔[\p{P}\d\s]{0,}발", "신발"},
            {@"님[\p{P}\d\s]{0,}아", "님"},
            {@"님[\p{P}\d\s]{0,}들[\p{P}\d\s]{0,}아", "님들"},
            {@"미[\p{P}\d\s]{0,}친[\p{P}\d\s]{0,}놈", "사랑이"},
            {@"병[\p{P}\d\s]{0,}신", "신"},
            {@"미[\p{P}\d\s]{0,}친", "열정"},
            {@"개[\p{P}\d\s]{0,}쉐[\p{P}\d\s]{0,}키", "멍멍이"},
            {@"존[\p{P}\d\s]{0,}나", "많이"},
            {@"또[\p{P}\d\s]{0,}라[\p{P}\d\s]{0,}이", "빙글이"},
        };
        #endregion

        public static bool ChatBlock = false;
        public static string Notice = "<공지> : 운영진은 절대로 비밀번호을 묻지않습니다.";




        public static List<Type> NoSaveItem = new List<Type>()
        {
            typeof(script.item.StrStatPotion),
            typeof(script.item.MenStatPotion),
            typeof(script.item.DexStatPotion),
            typeof(script.item.VitStatPotion),
        };


        public static List<string> BSArmor = new List<string>() { "토파즈", "석영", "크리스탈", "금", "레오파드제스퍼", "에메랄드", "캣츠아이", "흑진주", "터키석", "그레이트사파이어" };
        public static List<string> BSWeapons = new List<string>() { "은", "루비", "문스톤", "썬스톤", "블러드스톤", "타이거스아이", "산호", "오팔", "다이아몬드", "골드 다이아몬드" };
        public static List<int> BSUpgradeLevelTable = new List<int>() { 1, 10, 20, 30, 40, 50, 70, 80, 90, 99 };

        //public static Dictionary<string, Dictionary<string, int>> BSItemsTable = new Dictionary<string, Dictionary<string, int>>()
        //{   
        //    //Lv,XP,확률, Item, ItemList
        //    {"0,5,10000,Buckler", new Dictionary<string, int>(){            {"철", 1},        {"알루미늄", 1}}    },
        //    {"0,5,10000,Hatchet", new Dictionary<string, int>(){            {"철", 3}}   },
        //    {"0,5,10000,Hood", new Dictionary<string, int>(){               {"구리", 1},      {"고무", 1}}   },
        //    {"0,5,10000,Club", new Dictionary<string, int>(){               {"철", 1},        {"각목", 1}}  },
        //    {"0,5,10000,Knife", new Dictionary<string, int>(){              {"철", 1},        {"구리", 2}}  },
        //    {"0,5,10000,Rag", new Dictionary<string, int>(){                {"납", 1},        {"고무", 1}}  },                     
        //    {"1,7,10000,BambooKnife", new Dictionary<string, int>(){        {"각목", 2},      {"고무", 1}}  }, 
        //    {"1,7,10000,Suit", new Dictionary<string, int>(){               {"구리", 2},      {"알루미늄", 1}}  }, 
        //    {"2,10,10000,RoundShield", new Dictionary<string, int>(){       {"구리", 1},      {"납", 2},           {"나무쪼가리", 1}}  }, 
        //    {"2,10,10000,Sickle", new Dictionary<string, int>(){            {"철", 2},        {"납", 2}}  },     
        //    {"3,11,10000,WoodenSword", new Dictionary<string, int>(){       {"각목", 3},      {"나무쪼가리", 2}}  }, 
        //    {"4,12,10000,Cap", new Dictionary<string, int>(){               {"청동", 3}}  }, 
        //    {"4,12,10000,FullDress", new Dictionary<string, int>(){         {"구리", 1},      {"청동", 2},         {"고무", 2}} }, 
        //    {"5,13,10000,Dagger", new Dictionary<string, int>(){            {"철", 3},        {"구리", 2},         {"알루미늄",1}}  }, 
        //    {"6,15,10000,SmallAxe", new Dictionary<string, int>(){          {"철", 3},        {"납", 1},           {"청동", 2}}  }, 
        //    {"6,15,10000,Surplice", new Dictionary<string, int>(){          {"알루미늄", 1},  {"톱밥", 3},         {"고무", 2}}  },                     
        //    {"7,16,10000,Saw", new Dictionary<string, int>(){               {"철", 2},        {"납", 4}}  }, 
        //    {"8,18,10000,ShortSword", new Dictionary<string, int>(){        {"철", 4},        {"구리", 2}}  },             
        //    {"8,18,10000,SmallShield", new Dictionary<string, int>(){       {"납", 3},        {"알루미늄", 2},     {"청동", 2}}  }, 
        //    {"9,21,10000,Cape", new Dictionary<string, int>(){              {"구리", 3},      {"고무", 2},         {"종이", 1},          {"가죽", 1}}}, 
        //    {"10,24,10000,Fleuret", new Dictionary<string, int>(){          {"철", 2},        {"구리", 2},         {"알루미늄", 3}}  }, 
        //    {"10,24,10000,Headpiece", new Dictionary<string, int>(){        {"철", 2},        {"납", 1},           {"청동", 4}}  }, 
        //    {"11,27,10000,Mantle", new Dictionary<string, int>(){           {"철", 1},        {"청동", 2},         {"고무", 3},          {"톱밥", 1},    {"가죽", 1}}  }, 
        //    {"12,31,10000,Rapier", new Dictionary<string, int>(){           {"구리", 2},      {"납", 1},           {"알루미늄", 5}}  }, 
        //    {"12,31,10000,SpikedClub", new Dictionary<string, int>(){       {"철", 4},        {"납", 2},           {"각목", 2}}  },
        //    {"13,36,10000,OneEdgedAxe", new Dictionary<string, int>(){      {"철", 5},        {"구리", 1},         {"납", 2}}  }, 
        //    {"14,42,10000,SquareShield", new Dictionary<string, int>(){     {"철", 3},        {"알루미늄", 1},     {"청동", 4}}  }, 
        //    {"14,42,10000,Saber", new Dictionary<string, int>(){            {"철", 2},        {"알루미늄", 1},     {"납", 1}}  }, 
        //    {"15,46,10000,Cloak", new Dictionary<string, int>(){            {"알루미늄", 1},  {"청동", 3},         {"종이", 1},          {"톱밥", 1},    {"고무", 1}}  }, 
        //    {"16,51,10000,Epee", new Dictionary<string, int>(){             {"구리", 5},      {"청동", 2}}  }, 
        //    {"16,51,10000,Mask", new Dictionary<string, int>(){             {"철", 6},        {"구리", 1},         {"알루미늄", 1}}  }, 
        //    {"17,57,10000,MorningStar", new Dictionary<string, int>(){      {"청동", 6},      {"각목", 2}}  }, 
        //    {"18,63,10000,Cope", new Dictionary<string, int>(){             {"구리", 1},      {"납", 3},           {"톱밥", 1},          {"종이", 1},    {"고무", 2}}  }, 
        //    {"18,63,10000,Blade", new Dictionary<string, int>(){            {"철", 8},        {"납", 2},           {"카드뮴", 2},        {"텅스텐", 1}}}, 
        //    {"19,69,10000,TriangleShield", new Dictionary<string, int>(){   {"철", 6},        {"알루미늄", 2},     {"청동", 5},          {"고무", 2}}}, 
        //    {"20,73,10000,QuiltedArmor", new Dictionary<string, int>(){     {"구리", 3},      {"납", 5},           {"톱밥", 7}}  }, 
        //    {"20,73,10000,IceAxe", new Dictionary<string, int>(){           {"철", 6},        {"납", 4},           {"카드뮴", 3},        {"화강석", 2}}}, 
        //    {"21,79,10000,LongSword", new Dictionary<string, int>(){        {"철", 5},        {"청동", 5},         {"카드뮴", 7},        {"텅스텐", 2},   {"강철", 1}}  }, 
        //    {"21,79,10000,Mace", new Dictionary<string, int>(){             {"구리", 6},      {"납", 6},           {"청동", 6},          {"강철", 2}}}, 
        //    {"22,82,10000,Headgear", new Dictionary<string, int>(){         {"철", 2},        {"청동", 8},         {"텅스텐", 3},        {"뼈다귀", 5}}}, 
        //    {"23,90,10000,Robe", new Dictionary<string, int>(){             {"알루미늄", 2},  {"강철", 1},         {"니켈", 2},          {"가죽", 5},     {"고무", 2}}  }, 
        //    {"23,90,10000,Hanger", new Dictionary<string, int>(){           {"청동", 5},      {"카드뮴", 5},       {"텅스텐", 2}}  }, 
        //    {"24,97,10000,LargeShield", new Dictionary<string, int>(){      {"철", 3},        {"구리", 1},         {"텅스텐", 4},        {"강철", 4}}}, 
        //    {"25,105,10000,LeatherArmor", new Dictionary<string, int>(){    {"납", 1},        {"니켈", 3},         {"가죽", 6},          {"고무", 4}}}, 
        //    {"25,105,10000,Saver", new Dictionary<string, int>(){           {"철", 4},        {"청동", 4},         {"카드뮴", 7}}  }, 
        //    {"26,112,10000,Flail", new Dictionary<string, int>(){           {"철", 3},        {"청동", 5},         {"강철", 7}}  }, 
        //    {"27,124,10000,Simitar", new Dictionary<string, int>(){         {"청동", 2},      {"카드뮴", 13}}  }, 
        //    {"27,124,10000,HardLeather", new Dictionary<string, int>(){     {"납", 1},        {"카드뮴", 4},       {"가죽", 7},          {"고무", 3},     {"톱밥", 1}}  }, 
        //    {"28,135,10000,Helmet", new Dictionary<string, int>(){          {"철", 2},        {"알루미늄", 2},     {"청동", 5},          {"텅스텐", 2},   {"니켈", 2}}  }, 
        //    {"29,150,10000,TwoBladedAxe", new Dictionary<string, int>(){    {"철", 7},        {"구리", 3},         {"납", 1},            {"알루미늄", 2}, {"청동", 1}}  }, 
        //    {"29,150,10000,TowerShield", new Dictionary<string, int>(){     {"철", 5},        {"구리", 2},         {"납", 1},            {"청동", 1},     {"텅스텐", 5},     {"고무", 1}}  }, 
        //    {"30,165,10000,Cutlass", new Dictionary<string, int>(){         {"철", 4},        {"청동", 4},         {"니켈", 7}}  }, 
        //    {"31,175,10000,Hammer", new Dictionary<string, int>(){          {"철", 3},        {"청동", 2},         {"텅스텐", 4},        {"강철", 6},     {"코발트", 1}}  }, 
        //    {"31,175,10000,EnamelLeather", new Dictionary<string, int>(){   {"알루미늄", 1},  {"텅스텐", 8},       {"조가비", 7}}  }, 
        //    {"32,190,10000,IronSword", new Dictionary<string, int>(){       {"철", 7},        {"청동", 2},         {"강철", 6},          {"니켈", 1}}}, 
        //    {"33,190,10000,ToughLeather", new Dictionary<string, int>(){    {"카드뮴", 1},    {"강철", 1},         {"니켈", 2},          {"자갈", 3},     {"가죽", 8}}  }, 
        //    {"33,190,10000,CoatingShield", new Dictionary<string, int>(){   {"철", 1},        {"구리", 1},         {"알루미늄", 2},      {"텅스텐", 5},   {"지르코늄", 6},    {"대리석", 2}}  }, 
        //    {"34,220,10000,BroadSword", new Dictionary<string, int>(){      {"철", 4},        {"청동", 3},         {"카드뮴", 5},        {"니켈", 4}}}, 
        //    {"35,240,10000,Crown", new Dictionary<string, int>(){           {"구리", 2},      {"납", 1},           {"강철", 2},          {"지르코늄", 8}, {"코발트", 5}}  }, 
        //    {"35,240,10000,Hack", new Dictionary<string, int>(){            {"강철", 9},      {"티타늄", 6},       {"화강석", 3}}  }, 
        //    {"36,260,10000,ChestGuard", new Dictionary<string, int>(){      {"철", 1},        {"알루미늄", 3},     {"텅스텐", 5},        {"강철", 4},     {"코발트", 2}}  }, 
        //    {"37,275,10000,BroadAxe", new Dictionary<string, int>(){        {"니켈", 8},      {"티타늄", 7}}  }, 
        //    {"37,275,10000,Falchion", new Dictionary<string, int>(){        {"텅스텐", 10},   {"코발트", 5}}  },             

        //    {"38,300,10000,BattleArmor", new Dictionary<string, int>(){     {"알루미늄", 3},  {"니켈", 2},         {"코발트", 5},        {"고무", 8}}}, 
        //    {"39,320,10000,Claymore", new Dictionary<string, int>(){        {"철", 4},        {"강철", 10},        {"지르코늄", 2},      {"코발트", 2},   {"티타늄", 2}}  }, 
        //    {"39,320,10000,BattleShield", new Dictionary<string, int>(){    {"철", 3},        {"니켈", 3},         {"지르코늄", 8},      {"티타늄", 4},   {"뼈다귀", 2}}  }, 
        //    {"40,350,10000,WarHammer", new Dictionary<string, int>(){       {"철", 2},        {"코발트", 8},       {"티타늄", 6},        {"화강석", 4}}}, 
        //    {"40,350,10000,Gladius", new Dictionary<string, int>(){         {"철", 2},        {"카드뮴", 6},       {"강철", 12},         {"지르코늄", 2}}}, 
        //    {"41,620,10000,BoneMail", new Dictionary<string, int>(){        {"코발트", 8},    {"뼈다귀", 12}}  }, 
        //    {"42,640,10000,FieldCap", new Dictionary<string, int>(){        {"철", 2},        {"청동", 6},         {"강철", 14},         {"합금K", 1}}}, 
        //    {"42,640,10000,Tuck", new Dictionary<string, int>(){            {"강철", 1},      {"니켈", 5},         {"지르코늄", 1},      {"코발트", 3},    {"합금K", 1}}  }, 
        //    {"43,680,10000,RingMail", new Dictionary<string, int>(){        {"카드뮴", 1},    {"텅스텐", 2},       {"강철", 1},          {"니켈", 2},      {"지르코늄", 3},      {"코발트", 2}}  }, 
        //    {"44,720,10000,LargeAxe", new Dictionary<string, int>(){        {"강철", 4},      {"지르코늄", 1},     {"코발트", 4},        {"티타늄", 2}}}, 
        //    {"44,720,10000,LargeHack", new Dictionary<string, int>(){       {"강철", 1},      {"니켈", 2},         {"코발트", 4},        {"티타늄", 4}}},             
        //    {"45,760,10000,ArmShield", new Dictionary<string, int>(){       {"알루미늄", 1},  {"텅스텐", 1},       {"지르코늄", 2},      {"코발트", 5},    {"티타늄", 5}}  }, 
        //    {"46,800,10000,BastardSword", new Dictionary<string, int>(){    {"니켈", 3},      {"지르코늄", 1},     {"코발트", 8}}  }, 
        //    {"46,800,10000,ChainMail", new Dictionary<string, int>(){       {"청동", 1},      {"강철", 2},         {"니켈", 1},          {"코발트", 7},    {"합금K", 1} }  },             
        //    {"47,880,10000,FullHelmet", new Dictionary<string, int>(){      {"철", 2},        {"강철", 5},         {"지르코늄", 1},      {"코발트", 1},    {"티타늄", 3}}  }, 
        //    {"48,940,10000,SamuraiSword", new Dictionary<string, int>(){    {"청동", 2},      {"텅스텐", 2},       {"강철", 3},          {"지르코늄", 2},  {"티타늄", 5}}  }, 
        //    {"48,940,10000,Maul", new Dictionary<string, int>(){            {"텅스텐", 2},    {"강철", 3},         {"지르코늄", 3},      {"티타늄", 7}}  }, 
        //    {"49,1050,10000,ScaleMail", new Dictionary<string, int>(){       {"카드뮴", 1},    {"텅스텐", 1},       {"강철", 3},          {"니켈", 2},      {"지르코늄", 2},      {"티타늄", 6}}  },             
        //    {"50,1150,10000,FamilyShield", new Dictionary<string, int>(){    {"알루미늄", 2},  {"텅스텐", 1},       {"강철", 2},          {"지르코늄", 1},  {"코발트", 2},        {"티타늄", 12}}  }, 
        //    {"50,1150,10000,SevenStarSword", new Dictionary<string, int>(){  {"강철", 5},      {"지르코늄", 2},     {"티타늄", 2},        {"텅스텐", 1}}  }, 
        //    {"51,1300,9000,BattleAxe", new Dictionary<string, int>(){        {"강철", 3},      {"코발트", 2},       {"티타늄", 7}}  }, 
        //    {"52,1500,9000,BreastPlate", new Dictionary<string, int>(){      {"텅스텐", 1},    {"강철", 3},         {"코발트", 3},        {"티타늄", 4},     {"합금K", 1}}  }, 
        //    {"52,1500,9000,CoreaSword", new Dictionary<string, int>(){       {"강철", 4},      {"지르코늄", 1},     {"티타늄", 3},        {"백금", 1},       {"합금K", 3}}  }, 
        //    {"53,1800,9000,Halberd", new Dictionary<string, int>(){          {"지르코늄", 5},  {"코발트", 4},       {"티타늄", 2},        {"합금K", 1}}  }, 
        //    {"54,1800,9000,GrandeurHelmet", new Dictionary<string, int>(){   {"납", 1},        {"강철", 5},         {"지르코늄", 2},      {"티타늄", 2},     {"합금K", 3}}  }, 
        //    {"54,1800,9000,TwoHandSword", new Dictionary<string, int>(){     {"강철", 6},      {"지르코늄", 4},     {"티타늄", 2},        {"합금K", 1}}  },             
        //    {"55,2220,9000,MailPlate", new Dictionary<string, int>(){        {"강철", 4},      {"니켈", 1},         {"코발트", 2},        {"티타늄", 6}}  }, 
        //    {"56,2670,9000,GrandShield", new Dictionary<string, int>(){      {"텅스텐", 2},    {"강철", 4},         {"지르코늄", 3},      {"티타늄", 3},     {"합금K", 7}}  }, 
        //    {"56,2670,9000,GreatSword", new Dictionary<string, int>(){       {"강철", 6},      {"지르코늄", 1},     {"티타늄", 4},        {"백금", 1},       {"합금K", 4}}  }, 
        //    {"57,3020,9000,SamuraiPlate", new Dictionary<string, int>(){     {"강철", 5},      {"니켈", 3},         {"코발트", 2},        {"티타늄", 6}}  }, 
        //    {"58,3620,9000,GreatMaul", new Dictionary<string, int>(){        {"구리", 1},      {"강철", 7},         {"지르코늄", 3},      {"함금K", 7}}  }, 
        //    {"58,3620,9000,GreatAxe", new Dictionary<string, int>(){         {"강철", 6},      {"지르코늄", 4},     {"티타늄", 3},        {"합금K", 5}}  },
        //    {"59,4180,9000,WidePlate", new Dictionary<string, int>(){        {"강철", 6},      {"지르코늄", 6},     {"티타늄", 4},        {"합금K", 4} }  }, 
        //    {"60,4520,9000,FullPlate", new Dictionary<string, int>(){        {"강철", 5},      {"지르코늄", 4},     {"티타늄", 5},        {"백금", 2},        {"합금K", 7} }  },     
        //    {"61,5000,8000,VikingHelmet", new Dictionary<string, int>(){     {"강철", 1},      {"코발트", 6},       {"티타늄", 6},        {"백금", 3},        {"합금K", 6}}  }, 
        //    {"62,5550,8000,ExtraScimitar", new Dictionary<string, int>(){    {"강철", 3},      {"니켈", 6},         {"문스톤", 4},        {"헬리도오", 5},    {"아쿠아머린", 3}}  }, 
        //    {"63,6100,8000,SkellGuard", new Dictionary<string, int>(){       {"강철", 3},      {"니켈", 4},         {"썬스톤", 8},        {"지르코늄", 6},    {"백금", 6},         {"뼈다귀", 1}}  }, 
        //    {"64,6660,8000,CrocodileArmor", new Dictionary<string, int>(){   {"블러드스톤", 6},{"티타늄", 4},       {"헬리도오", 6},      {"다크스톤", 8},    {"고무", 1}}  }, 
        //    {"65,7300,8000,Excalido", new Dictionary<string, int>(){         {"크리스탈", 3},  {"코발트", 5},       {"블러드스톤", 6},    {"짱돌", 4},        {"합금", 7}}  },                   
        //    {"66,7810,8000,PigBasket", new Dictionary<string, int>(){        {"강철", 5},      {"에메랄드", 6},     {"진주", 5},          {"사파이어", 7},    {"조가비", 2}}  }, 
        //    {"67,8200,8000,BarbarianAxe", new Dictionary<string, int>(){     {"지르코늄", 2},  {"다크스톤", 6},     {"호박", 2},          {"짱돌", 7},        {"아메시스트", 5}}  }, 
        //    {"68,8900,8000,GagoilPlate", new Dictionary<string, int>(){      {"토파즈", 4},    {"티타늄", 8},       {"헬리도오", 6},      {"캣츠아이", 6}}  }, 
        //    {"70,9540,8000,RocketMaul", new Dictionary<string, int>(){       {"코발트", 2},    {"티타늄", 2},       {"다크스톤", 4},      {"백금", 4},        {"합금", 10},         {"강철", 3}}  }, 
        //    {"72,9540,7000,WarriorAngel", new Dictionary<string, int>(){     {"헬리도오", 3},  {"짱돌", 3},         {"합금", 8},          {"아메시스트", 6}}  }, 
        //    {"74,9540,7000,GeneralSword", new Dictionary<string, int>(){     {"텅스텐", 6},    {"제이드", 6},       {"진주", 1},          {"산호", 3},        {"통뼈", 4},          {"오팔", 2}}  }, 
        //    {"76,10900,7000,DiamondStick", new Dictionary<string, int>(){     {"강철", 8},      {"문스톤", 3},       {"아쿠아머린", 6},    {"다이아몬드", 1}}  }, 
        //    {"78,10900,7000,Calypso", new Dictionary<string, int>(){          {"에메랄드", 6},  {"블러드스톤", 6},   {"백금", 3},          {"오팔", 1}}  }, 
        //    {"80,12050,7000,DiamondArmor", new Dictionary<string, int>(){     {"강철", 5},      {"크리스탈", 3},     {"에메랄드", 5},      {"백금", 5}}  }, 
        //    {"82,12550,6000,PowerStrong", new Dictionary<string, int>(){      {"합금", 5},      {"타이거스아이", 3}, {"다크스톤", 5},      {"캣츠아이", 2},     {"니켈", 5}}  }, 
        //    {"84,12950,6000,TitanMace", new Dictionary<string, int>(){        {"코발트", 5},    {"티타늄", 6},       {"짱돌", 9},          {"합금", 4},         {"초합금", 1}}  }, 
        //    {"86,14200,6000,Gladiator", new Dictionary<string, int>(){        {"크리스탈", 6},  {"티타늄", 6},       {"백금", 5},          {"짱돌", 4},         {"합금", 4}}  }, 
        //    {"88,17750,6000,StormShield", new Dictionary<string, int>(){      {"코발트", 9},    {"다크스톤", 3},     {"합금", 5},          {"아메시스트", 5}}  }, 
        //    {"90,19570,6000,Tomahawk", new Dictionary<string, int>(){         {"헬리도오", 8},  {"다크스톤", 5},     {"합금", 6},          {"초합금", 1}}  }, 
        //    {"92,23700,5000,LionHeart", new Dictionary<string, int>(){        {"썬스톤", 6},    {"레오파드제스퍼", 4},{"합금", 4},         {"아메시스트", 5},   {"터키석", 1}}  }, 
        //    {"94,28870,5000,LaurelCrown", new Dictionary<string, int>(){      {"합금", 5},      {"사파이어", 4},     {"백금", 4},          {"흑진주", 2},       {"아메시스트", 3}}  },

        //    {"96,33500,5000,DragonSkin", new Dictionary<string, int>(){       {"텅스텐", 6},    {"다크스톤", 4},     {"합금", 2},          {"산호", 4},         {"오팔", 3}}  }, 
        //    {"96,33500,5000,DragonBlade", new Dictionary<string, int>(){      {"합금", 4},      {"티타늄", 4},       {"호박", 4},          {"산호", 2},         {"캣츠아이", 2},      {"터키석", 2}}  }, 
        //    {"98,48870,5000,GiantHammer", new Dictionary<string, int>(){      {"티타늄", 4},    {"헬리도오", 5},     {"합금", 5},          {"아메시스트", 3},   {"초합금", 1},        {"마왕철", 1}}  }, 
        //};
        public static Dictionary<string, Dictionary<string, int>> BSItemsTable = new Dictionary<string, Dictionary<string, int>>()
        {   
            //Lv,XP,확률, Item, ItemList
            {"0,5,10000,Buckler", new Dictionary<string, int>(){            {"철", 1},        {"알루미늄", 1}}    },
            {"0,5,10000,Hatchet", new Dictionary<string, int>(){            {"철", 3}}   },
            {"0,5,10000,Hood", new Dictionary<string, int>(){               {"구리", 1},      {"고무", 1}}   },
            {"0,5,10000,Club", new Dictionary<string, int>(){               {"철", 1},        {"각목", 1}}  },
            {"0,5,10000,Knife", new Dictionary<string, int>(){              {"철", 1},        {"구리", 2}}  },
            {"0,5,10000,Rag", new Dictionary<string, int>(){                {"납", 1},        {"고무", 1}}  },                     
            {"1,7,10000,BambooKnife", new Dictionary<string, int>(){        {"각목", 2},      {"고무", 1}}  }, 
            {"1,7,10000,Suit", new Dictionary<string, int>(){               {"구리", 2},      {"알루미늄", 1}}  }, 
            {"2,10,10000,RoundShield", new Dictionary<string, int>(){       {"구리", 1},      {"납", 2},           {"나무쪼가리", 1}}  }, 
            {"2,10,10000,Sickle", new Dictionary<string, int>(){            {"철", 2},        {"납", 2}}  },     
            {"3,11,10000,WoodenSword", new Dictionary<string, int>(){       {"각목", 3},      {"나무쪼가리", 2}}  }, 
            {"4,12,10000,Cap", new Dictionary<string, int>(){               {"청동", 3}}  }, 
            {"4,12,10000,FullDress", new Dictionary<string, int>(){         {"구리", 1},      {"청동", 2},         {"고무", 2}} }, 
            {"5,13,10000,Dagger", new Dictionary<string, int>(){            {"철", 3},        {"구리", 2},         {"알루미늄",1}}  }, 
            {"6,15,10000,SmallAxe", new Dictionary<string, int>(){          {"철", 3},        {"납", 1},           {"청동", 2}}  }, 
            {"6,15,10000,Surplice", new Dictionary<string, int>(){          {"알루미늄", 1},  {"톱밥", 3},         {"고무", 2}}  },                     
            {"7,16,10000,Saw", new Dictionary<string, int>(){               {"철", 2},        {"납", 4}}  }, 
            {"8,18,10000,ShortSword", new Dictionary<string, int>(){        {"철", 4},        {"구리", 2}}  },             
            {"8,18,10000,SmallShield", new Dictionary<string, int>(){       {"납", 3},        {"알루미늄", 2},     {"청동", 2}}  }, 
            {"9,21,10000,Cape", new Dictionary<string, int>(){              {"구리", 3},      {"고무", 2},         {"종이", 1},          {"가죽", 1}}}, 
            {"10,24,10000,Fleuret", new Dictionary<string, int>(){          {"철", 2},        {"구리", 2},         {"알루미늄", 3}}  }, 
            {"10,24,10000,Headpiece", new Dictionary<string, int>(){        {"철", 2},        {"납", 1},           {"청동", 4}}  }, 
            {"11,27,10000,Mantle", new Dictionary<string, int>(){           {"철", 1},        {"청동", 2},         {"고무", 3},          {"톱밥", 1},    {"가죽", 1}}  }, 
            {"12,31,10000,Rapier", new Dictionary<string, int>(){           {"구리", 2},      {"납", 1},           {"알루미늄", 5}}  }, 
            {"12,31,10000,SpikedClub", new Dictionary<string, int>(){       {"철", 4},        {"납", 2},           {"각목", 2}}  },
            {"13,36,10000,OneEdgedAxe", new Dictionary<string, int>(){      {"철", 5},        {"구리", 1},         {"납", 2}}  }, 
            {"14,42,10000,SquareShield", new Dictionary<string, int>(){     {"철", 3},        {"알루미늄", 1},     {"청동", 4}}  }, 
            {"14,42,10000,Saber", new Dictionary<string, int>(){            {"철", 2},        {"알루미늄", 1},     {"납", 1}}  }, 
            {"15,46,10000,Cloak", new Dictionary<string, int>(){            {"알루미늄", 1},  {"청동", 3},         {"종이", 1},          {"톱밥", 1},    {"고무", 1}}  }, 
            {"16,51,10000,Epee", new Dictionary<string, int>(){             {"구리", 5},      {"청동", 2}}  }, 
            {"16,51,10000,Mask", new Dictionary<string, int>(){             {"철", 6},        {"구리", 1},         {"알루미늄", 1}}  }, 
            {"17,57,10000,MorningStar", new Dictionary<string, int>(){      {"청동", 6},      {"각목", 2}}  }, 
            {"18,63,10000,Cope", new Dictionary<string, int>(){             {"구리", 1},      {"납", 3},           {"톱밥", 1},          {"종이", 1},    {"고무", 2}}  }, 
            {"18,63,10000,Blade", new Dictionary<string, int>(){            {"철", 8},        {"납", 2},           {"카드뮴", 2},        {"텅스텐", 1}}}, 
            {"19,69,10000,TriangleShield", new Dictionary<string, int>(){   {"철", 6},        {"알루미늄", 2},     {"청동", 5},          {"고무", 2}}}, 
            {"20,73,10000,QuiltedArmor", new Dictionary<string, int>(){     {"구리", 3},      {"납", 5},           {"톱밥", 7}}  }, 
            {"20,73,10000,IceAxe", new Dictionary<string, int>(){           {"철", 6},        {"납", 4},           {"카드뮴", 3},        {"화강석", 2}}}, 
            {"21,79,10000,LongSword", new Dictionary<string, int>(){        {"철", 5},        {"청동", 5},         {"카드뮴", 7},        {"텅스텐", 2},   {"강철", 1}}  }, 
            {"21,79,10000,Mace", new Dictionary<string, int>(){             {"구리", 6},      {"납", 6},           {"청동", 6},          {"강철", 2}}}, 
            {"22,82,10000,Headgear", new Dictionary<string, int>(){         {"철", 2},        {"청동", 8},         {"텅스텐", 3},        {"뼈다귀", 5}}}, 
            {"23,90,10000,Robe", new Dictionary<string, int>(){             {"알루미늄", 2},  {"강철", 1},         {"니켈", 2},          {"가죽", 5},     {"고무", 2}}  }, 
            {"23,90,10000,Hanger", new Dictionary<string, int>(){           {"청동", 5},      {"카드뮴", 5},       {"텅스텐", 2}}  }, 
            {"24,97,10000,LargeShield", new Dictionary<string, int>(){      {"철", 3},        {"구리", 1},         {"텅스텐", 4},        {"강철", 4}}}, 
            {"25,105,10000,LeatherArmor", new Dictionary<string, int>(){    {"납", 1},        {"니켈", 3},         {"가죽", 6},          {"고무", 4}}}, 
            {"25,105,10000,Saver", new Dictionary<string, int>(){           {"철", 4},        {"청동", 4},         {"카드뮴", 7}}  }, 
            {"26,112,10000,Flail", new Dictionary<string, int>(){           {"철", 3},        {"청동", 5},         {"강철", 7}}  }, 
            {"27,124,10000,Simitar", new Dictionary<string, int>(){         {"청동", 2},      {"카드뮴", 13}}  }, 
            {"27,124,10000,HardLeather", new Dictionary<string, int>(){     {"납", 1},        {"카드뮴", 4},       {"가죽", 7},          {"고무", 3},     {"톱밥", 1}}  }, 
            {"28,135,10000,Helmet", new Dictionary<string, int>(){          {"철", 2},        {"알루미늄", 2},     {"청동", 5},          {"텅스텐", 2},   {"니켈", 2}}  }, 
            {"29,150,10000,TwoBladedAxe", new Dictionary<string, int>(){    {"철", 7},        {"구리", 3},         {"납", 1},            {"알루미늄", 2}, {"청동", 1}}  }, 
            {"29,150,10000,TowerShield", new Dictionary<string, int>(){     {"철", 5},        {"구리", 2},         {"납", 1},            {"청동", 1},     {"텅스텐", 5},     {"고무", 1}}  }, 
            {"30,165,10000,Cutlass", new Dictionary<string, int>(){         {"철", 4},        {"청동", 4},         {"니켈", 7}}  }, 
            {"31,175,10000,Hammer", new Dictionary<string, int>(){          {"철", 3},        {"청동", 2},         {"텅스텐", 4},        {"강철", 6},     {"코발트", 1}}  }, 
            {"31,175,10000,EnamelLeather", new Dictionary<string, int>(){   {"알루미늄", 1},  {"텅스텐", 8},       {"조가비", 7}}  }, 
            {"32,190,10000,IronSword", new Dictionary<string, int>(){       {"철", 7},        {"청동", 2},         {"강철", 6},          {"니켈", 1}}}, 
            {"33,190,10000,ToughLeather", new Dictionary<string, int>(){    {"카드뮴", 1},    {"강철", 1},         {"니켈", 2},          {"자갈", 3},     {"가죽", 8}}  }, 
            {"33,190,10000,CoatingShield", new Dictionary<string, int>(){   {"철", 1},        {"구리", 1},         {"알루미늄", 2},      {"텅스텐", 5},   {"지르코늄", 6},    {"대리석", 2}}  }, 
            {"34,220,10000,BroadSword", new Dictionary<string, int>(){      {"철", 4},        {"청동", 3},         {"카드뮴", 5},        {"니켈", 4}}}, 
            {"35,240,10000,Crown", new Dictionary<string, int>(){           {"구리", 2},      {"납", 1},           {"강철", 2},          {"지르코늄", 8}, {"코발트", 5}}  }, 
            {"35,240,10000,Hack", new Dictionary<string, int>(){            {"강철", 9},      {"티타늄", 6},       {"화강석", 3}}  }, 
            {"36,260,10000,ChestGuard", new Dictionary<string, int>(){      {"철", 1},        {"알루미늄", 3},     {"텅스텐", 5},        {"강철", 4},     {"코발트", 2}}  }, 
            {"37,275,10000,BroadAxe", new Dictionary<string, int>(){        {"니켈", 8},      {"티타늄", 7}}  }, 
            {"37,275,10000,Falchion", new Dictionary<string, int>(){        {"텅스텐", 10},   {"코발트", 5}}  },             
            
            {"38,300,10000,BattleArmor", new Dictionary<string, int>(){     {"알루미늄", 3},  {"니켈", 2},         {"코발트", 5},        {"고무", 8}}}, 
            {"39,320,10000,Claymore", new Dictionary<string, int>(){        {"철", 4},        {"강철", 10},        {"지르코늄", 2},      {"코발트", 2},   {"티타늄", 2}}  }, 
            {"39,320,10000,BattleShield", new Dictionary<string, int>(){    {"철", 3},        {"니켈", 3},         {"지르코늄", 8},      {"티타늄", 4},   {"뼈다귀", 2}}  }, 
            {"40,350,10000,WarHammer", new Dictionary<string, int>(){       {"철", 2},        {"코발트", 8},       {"티타늄", 6},        {"화강석", 4}}}, 
            {"40,350,10000,Gladius", new Dictionary<string, int>(){         {"철", 2},        {"카드뮴", 6},       {"강철", 12},         {"지르코늄", 2}}}, 
            {"41,620,10000,BoneMail", new Dictionary<string, int>(){        {"코발트", 8},    {"뼈다귀", 12}}  }, 
            {"42,640,10000,FieldCap", new Dictionary<string, int>(){        {"철", 2},        {"청동", 6},         {"강철", 14},         {"합금", 1}}}, 
            {"42,640,10000,Tuck", new Dictionary<string, int>(){            {"강철", 1},      {"니켈", 5},         {"지르코늄", 1},      {"코발트", 3},    {"합금", 1}}  }, 
            {"43,680,10000,RingMail", new Dictionary<string, int>(){        {"카드뮴", 1},    {"텅스텐", 2},       {"강철", 1},          {"니켈", 2},      {"지르코늄", 3},      {"코발트", 2}}  }, 
            {"44,720,10000,LargeAxe", new Dictionary<string, int>(){        {"강철", 4},      {"지르코늄", 1},     {"코발트", 4},        {"티타늄", 2}}}, 
            {"44,720,10000,LargeHack", new Dictionary<string, int>(){       {"강철", 1},      {"니켈", 2},         {"코발트", 4},        {"티타늄", 4}}},             
            {"45,760,10000,ArmShield", new Dictionary<string, int>(){       {"알루미늄", 1},  {"텅스텐", 1},       {"지르코늄", 2},      {"코발트", 5},    {"티타늄", 5}}  }, 
            {"46,800,10000,BastardSword", new Dictionary<string, int>(){    {"니켈", 3},      {"지르코늄", 1},     {"코발트", 8}}  }, 
            {"46,800,10000,ChainMail", new Dictionary<string, int>(){       {"청동", 1},      {"강철", 2},         {"니켈", 1},          {"코발트", 7},    {"합금", 1} }  },             
            {"47,880,10000,FullHelmet", new Dictionary<string, int>(){      {"철", 2},        {"강철", 5},         {"지르코늄", 1},      {"코발트", 1},    {"티타늄", 3}}  }, 
            {"48,940,10000,SamuraiSword", new Dictionary<string, int>(){    {"청동", 2},      {"텅스텐", 2},       {"강철", 3},          {"지르코늄", 2},  {"티타늄", 5}}  }, 
            {"48,940,10000,Maul", new Dictionary<string, int>(){            {"텅스텐", 2},    {"강철", 3},         {"지르코늄", 3},      {"티타늄", 7}}  }, 
            {"49,1050,10000,ScaleMail", new Dictionary<string, int>(){       {"카드뮴", 1},    {"텅스텐", 1},       {"강철", 3},          {"니켈", 2},      {"지르코늄", 2},      {"티타늄", 6}}  },             
            {"50,1150,10000,FamilyShield", new Dictionary<string, int>(){    {"알루미늄", 2},  {"텅스텐", 1},       {"강철", 2},          {"지르코늄", 1},  {"코발트", 2},        {"티타늄", 12}}  }, 
            {"50,1150,10000,SevenStarSword", new Dictionary<string, int>(){  {"강철", 5},      {"지르코늄", 2},     {"티타늄", 2},        {"합금", 1}}  }, 
            {"51,1300,9000,BattleAxe", new Dictionary<string, int>(){        {"강철", 3},      {"코발트", 2},       {"티타늄", 7}}  }, 
            {"52,1500,9000,BreastPlate", new Dictionary<string, int>(){      {"텅스텐", 1},    {"강철", 3},         {"코발트", 3},        {"티타늄", 4},     {"합금", 1}}  }, 
            {"52,1500,9000,CoreaSword", new Dictionary<string, int>(){       {"강철", 4},      {"지르코늄", 1},     {"티타늄", 3},        {"백금", 1},       {"합금", 3}}  }, 
            {"53,1800,9000,Halberd", new Dictionary<string, int>(){          {"지르코늄", 5},  {"코발트", 4},       {"티타늄", 2},        {"합금", 1}}  }, 
            {"54,1800,9000,GrandeurHelmet", new Dictionary<string, int>(){   {"납", 1},        {"강철", 5},         {"지르코늄", 2},      {"티타늄", 2},     {"합금", 3}}  }, 
            {"54,1800,9000,TwoHandSword", new Dictionary<string, int>(){     {"강철", 6},      {"지르코늄", 4},     {"티타늄", 2},        {"합금", 1}}  },            
            {"55,2220,9000,MailPlate", new Dictionary<string, int>(){        {"강철", 4},      {"니켈", 1},         {"코발트", 2},        {"티타늄", 6}}  }, 
            {"56,2670,9000,GrandShield", new Dictionary<string, int>(){      {"텅스텐", 2},    {"강철", 4},         {"지르코늄", 3},      {"티타늄", 3},     {"합금", 7}}  }, 
            {"56,2670,9000,GreatSword", new Dictionary<string, int>(){       {"강철", 6},      {"지르코늄", 1},     {"티타늄", 4},        {"백금", 1},       {"합금", 4}}  }, 
            {"57,3020,9000,SamuraiPlate", new Dictionary<string, int>(){     {"강철", 5},      {"니켈", 3},         {"코발트", 2},        {"티타늄", 6}}  }, 
            {"58,3620,9000,GreatMaul", new Dictionary<string, int>(){        {"구리", 1},      {"강철", 7},         {"지르코늄", 3},      {"합금", 7}}  }, 
            {"58,3620,9000,GreatAxe", new Dictionary<string, int>(){         {"강철", 6},      {"지르코늄", 4},     {"티타늄", 3},        {"합금", 5}}  },
            {"59,4180,9000,WidePlate", new Dictionary<string, int>(){        {"강철", 6},      {"지르코늄", 6},     {"티타늄", 4},        {"합금", 4} }  }, 
            {"60,4520,9000,FullPlate", new Dictionary<string, int>(){        {"강철", 5},      {"지르코늄", 4},     {"티타늄", 5},        {"백금", 2},        {"합금", 7} }  },     
            {"61,5000,8000,VikingHelmet", new Dictionary<string, int>(){     {"강철", 1},      {"코발트", 6},       {"티타늄", 6},        {"백금", 3},        {"합금", 6}}  }, 
            {"62,5550,8000,ExtraScimitar", new Dictionary<string, int>(){    {"강철", 3},      {"니켈", 6},         {"문스톤", 4},        {"헬리도오", 5},    {"아쿠아머린", 3}}  }, 
            {"63,6100,8000,SkellGuard", new Dictionary<string, int>(){       {"강철", 3},      {"니켈", 4},         {"썬스톤", 8},        {"지르코늄", 6},    {"백금", 6},         {"뼈다귀", 1}}  }, 
            {"64,6660,8000,CrocodileArmor", new Dictionary<string, int>(){   {"블러드스톤", 6},{"티타늄", 4},       {"헬리도오", 6},      {"다크스톤", 8},    {"고무", 1}}  }, 
            {"65,7300,8000,Excalido", new Dictionary<string, int>(){         {"크리스탈", 3},  {"코발트", 5},       {"블러드스톤", 6},    {"짱돌", 4},        {"합금", 7}}  },                   
            {"66,7810,8000,PigBasket", new Dictionary<string, int>(){        {"강철", 5},      {"에메랄드", 6},     {"진주", 5},          {"사파이어", 7},    {"조가비", 2}}  }, 
            {"67,8200,8000,BarbarianAxe", new Dictionary<string, int>(){     {"지르코늄", 2},  {"다크스톤", 6},     {"호박", 2},          {"짱돌", 7},        {"아메시스트", 5}}  }, 
            {"68,8900,8000,GagoilPlate", new Dictionary<string, int>(){      {"토파즈", 4},    {"티타늄", 8},       {"헬리도오", 6},      {"캣츠아이", 6}}  }, 
            {"70,9540,8000,RocketMaul", new Dictionary<string, int>(){       {"코발트", 2},    {"티타늄", 2},       {"다크스톤", 4},      {"백금", 4},        {"합금", 10},         {"강철", 3}}  }, 
            {"72,9540,7000,WarriorAngel", new Dictionary<string, int>(){     {"헬리도오", 3},  {"짱돌", 3},         {"합금", 8},          {"아메시스트", 6}}  }, 
            {"74,9540,7000,GeneralSword", new Dictionary<string, int>(){     {"텅스텐", 6},    {"제이드", 6},       {"진주", 1},          {"산호", 3},        {"통뼈", 4},          {"오팔", 2}}  }, 
            {"76,10900,7000,DiamondStick", new Dictionary<string, int>(){     {"강철", 8},      {"문스톤", 3},       {"아쿠아머린", 6},    {"다이아몬드", 1}}  }, 
            {"78,10900,7000,Calypso", new Dictionary<string, int>(){          {"에메랄드", 6},  {"블러드스톤", 6},   {"백금", 3},          {"오팔", 1}}  }, 
            {"80,12050,7000,DiamondArmor", new Dictionary<string, int>(){     {"강철", 5},      {"크리스탈", 3},     {"에메랄드", 5},      {"백금", 5}}  }, 
            {"82,12550,6000,PowerStrong", new Dictionary<string, int>(){      {"합금", 5},      {"타이거스아이", 3}, {"다크스톤", 5},      {"캣츠아이", 2},     {"니켈", 5}}  }, 
            {"84,12950,6000,TitanMace", new Dictionary<string, int>(){        {"코발트", 5},    {"티타늄", 6},       {"짱돌", 9},          {"합금", 4},         {"초합금", 1}}  }, 
            {"86,14200,6000,Gladiator", new Dictionary<string, int>(){        {"크리스탈", 6},  {"티타늄", 6},       {"백금", 5},          {"짱돌", 4},         {"합금", 4}}  }, 
            {"88,17750,6000,StormShield", new Dictionary<string, int>(){      {"코발트", 9},    {"다크스톤", 3},     {"합금", 5},          {"아메시스트", 5}}  }, 
            {"90,19570,6000,Tomahawk", new Dictionary<string, int>(){         {"헬리도오", 8},  {"다크스톤", 5},     {"합금", 6},          {"초합금", 1}}  }, 
            {"92,23700,5000,LionHeart", new Dictionary<string, int>(){        {"썬스톤", 6},    {"레오파드제스퍼", 4},{"합금", 4},         {"아메시스트", 5},   {"터키석", 1}}  }, 
            {"94,28870,5000,LaurelCrown", new Dictionary<string, int>(){      {"합금", 5},      {"사파이어", 4},     {"백금", 4},          {"흑진주", 2},       {"아메시스트", 3}}  },
 
            {"96,33500,5000,DragonSkin", new Dictionary<string, int>(){       {"텅스텐", 6},    {"다크스톤", 4},     {"합금", 2},          {"산호", 4},         {"오팔", 3}}  }, 
            {"96,33500,5000,DragonBlade", new Dictionary<string, int>(){      {"합금", 4},      {"티타늄", 4},       {"호박", 4},          {"산호", 2},         {"캣츠아이", 2},      {"터키석", 2}}  }, 
            {"98,48870,5000,GiantHammer", new Dictionary<string, int>(){      {"티타늄", 4},    {"헬리도오", 5},     {"합금", 5},          {"아메시스트", 3},   {"초합금", 1},        {"마왕철", 1}}  }, 
        };
        public static Dictionary<string, Dictionary<Type, int>> BSItemsTable2 = new Dictionary<string, Dictionary<Type, int>>()
        {   
            //Lv,XP,확률, Item, ItemList
            {"0,5,10000,Buckler", new Dictionary<Type, int>(){            {typeof(script.item.IronGem), 1},         {typeof(script.item.AluminiumGem), 1}}    },
            {"0,5,10000,Hatchet", new Dictionary<Type, int>(){            {typeof(script.item.IronGem), 3}}   },
            {"0,5,10000,Hood", new Dictionary<Type, int>(){               {typeof(script.item.CopperGem), 1},       {typeof(script.item.고무), 1}}   },
            {"0,5,10000,Club", new Dictionary<Type, int>(){               {typeof(script.item.IronGem), 1},         {typeof(script.item.각목), 1}}  },
            {"0,5,10000,Knife", new Dictionary<Type, int>(){              {typeof(script.item.IronGem), 1},         {typeof(script.item.CopperGem), 2}}  },
            {"0,5,10000,Rag", new Dictionary<Type, int>(){                {typeof(script.item.LeadGem), 1},         {typeof(script.item.고무), 1}}  },                     
            {"1,7,10000,BambooKnife", new Dictionary<Type, int>(){        {typeof(script.item.각목), 2},            {typeof(script.item.고무), 1}}  }, 
            {"1,7,10000,Suit", new Dictionary<Type, int>(){               {typeof(script.item.CopperGem), 2},       {typeof(script.item.AluminiumGem), 1}}  }, 
            {"2,10,10000,RoundShield", new Dictionary<Type, int>(){       {typeof(script.item.CopperGem), 1},       {typeof(script.item.LeadGem), 2},           {typeof(script.item.나무쪼가리), 1}}  }, 
            {"2,10,10000,Sickle", new Dictionary<Type, int>(){            {typeof(script.item.IronGem), 2},         {typeof(script.item.LeadGem), 2}}  },     
            {"3,11,10000,WoodenSword", new Dictionary<Type, int>(){       {typeof(script.item.각목), 3},            {typeof(script.item.나무쪼가리), 2}}  }, 
            {"4,12,10000,Cap", new Dictionary<Type, int>(){               {typeof(script.item.Bronze), 3}}  }, 
            {"4,12,10000,FullDress", new Dictionary<Type, int>(){         {typeof(script.item.CopperGem), 1},       {typeof(script.item.Bronze), 2},            {typeof(script.item.고무), 2}} }, 
            {"5,13,10000,Dagger", new Dictionary<Type, int>(){            {typeof(script.item.IronGem), 3},         {typeof(script.item.CopperGem), 2},         {typeof(script.item.AluminiumGem),1}}  }, 
            {"6,15,10000,SmallAxe", new Dictionary<Type, int>(){          {typeof(script.item.IronGem), 3},         {typeof(script.item.LeadGem), 1},           {typeof(script.item.Bronze), 2}}  }, 
            {"6,15,10000,Surplice", new Dictionary<Type, int>(){          {typeof(script.item.AluminiumGem), 1},    {typeof(script.item.톱밥), 3},              {typeof(script.item.고무), 2}}  },                     
            {"7,16,10000,Saw", new Dictionary<Type, int>(){               {typeof(script.item.IronGem), 2},         {typeof(script.item.LeadGem), 4}}  }, 
            {"8,18,10000,ShortSword", new Dictionary<Type, int>(){        {typeof(script.item.IronGem), 4},         {typeof(script.item.CopperGem), 2}}  },             
            {"8,18,10000,SmallShield", new Dictionary<Type, int>(){       {typeof(script.item.LeadGem), 3},         {typeof(script.item.AluminiumGem), 2},      {typeof(script.item.Bronze), 2}}  }, 
            {"9,21,10000,Cape", new Dictionary<Type, int>(){              {typeof(script.item.CopperGem), 3},       {typeof(script.item.고무), 2},              {typeof(script.item.종이), 1},              {typeof(script.item.가죽), 1}}}, 
            {"10,24,10000,Fleuret", new Dictionary<Type, int>(){          {typeof(script.item.IronGem), 2},         {typeof(script.item.CopperGem), 2},         {typeof(script.item.AluminiumGem), 3}}  }, 
            {"10,24,10000,Headpiece", new Dictionary<Type, int>(){        {typeof(script.item.IronGem), 2},         {typeof(script.item.LeadGem), 1},           {typeof(script.item.Bronze), 4}}  }, 
            {"11,27,10000,Mantle", new Dictionary<Type, int>(){           {typeof(script.item.IronGem), 1},         {typeof(script.item.Bronze), 2},            {typeof(script.item.고무), 3},              {typeof(script.item.톱밥), 1},              {typeof(script.item.가죽), 1}}  }, 
            {"12,31,10000,Rapier", new Dictionary<Type, int>(){           {typeof(script.item.CopperGem), 2},       {typeof(script.item.LeadGem), 1},           {typeof(script.item.AluminiumGem), 5}}  }, 
            {"12,31,10000,SpikedClub", new Dictionary<Type, int>(){       {typeof(script.item.IronGem), 4},         {typeof(script.item.LeadGem), 2},           {typeof(script.item.각목), 2}}  },
            {"13,36,10000,OneEdgedAxe", new Dictionary<Type, int>(){      {typeof(script.item.IronGem), 5},         {typeof(script.item.CopperGem), 1},         {typeof(script.item.LeadGem), 2}}  }, 
            {"14,42,10000,SquareShield", new Dictionary<Type, int>(){     {typeof(script.item.IronGem), 3},         {typeof(script.item.AluminiumGem), 1},      {typeof(script.item.Bronze), 4}}  }, 
            {"14,42,10000,Saber", new Dictionary<Type, int>(){            {typeof(script.item.IronGem), 2},         {typeof(script.item.AluminiumGem), 1},      {typeof(script.item.LeadGem), 1}}  }, 
            {"15,46,10000,Cloak", new Dictionary<Type, int>(){            {typeof(script.item.AluminiumGem), 1},    {typeof(script.item.Bronze), 3},            {typeof(script.item.종이), 1},              {typeof(script.item.톱밥), 1},              {typeof(script.item.고무), 1}}  }, 
            {"16,51,10000,Epee", new Dictionary<Type, int>(){             {typeof(script.item.CopperGem), 5},       {typeof(script.item.Bronze), 2}}  }, 
            {"16,51,10000,Mask", new Dictionary<Type, int>(){             {typeof(script.item.IronGem), 6},         {typeof(script.item.CopperGem), 1},         {typeof(script.item.AluminiumGem), 1}}  }, 
            {"17,57,10000,MorningStar", new Dictionary<Type, int>(){      {typeof(script.item.Bronze), 6},          {typeof(script.item.각목), 2}}  }, 
            {"18,63,10000,Cope", new Dictionary<Type, int>(){             {typeof(script.item.CopperGem), 1},       {typeof(script.item.LeadGem), 3},           {typeof(script.item.톱밥), 1},              {typeof(script.item.종이), 1},              {typeof(script.item.고무), 2}}  }, 
            {"18,63,10000,Blade", new Dictionary<Type, int>(){            {typeof(script.item.IronGem), 8},         {typeof(script.item.LeadGem), 2},           {typeof(script.item.CadmiumGem), 2},        {typeof(script.item.TungstenGem), 1}}}, 
            {"19,69,10000,TriangleShield", new Dictionary<Type, int>(){   {typeof(script.item.IronGem), 6},         {typeof(script.item.AluminiumGem), 2},      {typeof(script.item.Bronze), 5},            {typeof(script.item.고무), 2}}}, 
            {"20,73,10000,QuiltedArmor", new Dictionary<Type, int>(){     {typeof(script.item.CopperGem), 3},       {typeof(script.item.LeadGem), 5},           {typeof(script.item.톱밥), 7}}  }, 
            {"20,73,10000,IceAxe", new Dictionary<Type, int>(){           {typeof(script.item.IronGem), 6},         {typeof(script.item.LeadGem), 4},           {typeof(script.item.CadmiumGem), 3},        {typeof(script.item.화강석), 2}}}, 
            {"21,79,10000,LongSword", new Dictionary<Type, int>(){        {typeof(script.item.IronGem), 5},         {typeof(script.item.Bronze), 5},            {typeof(script.item.CadmiumGem), 7},        {typeof(script.item.TungstenGem), 2},       {typeof(script.item.SteelGem), 1}}  }, 
            {"21,79,10000,Mace", new Dictionary<Type, int>(){             {typeof(script.item.CopperGem), 6},       {typeof(script.item.LeadGem), 6},           {typeof(script.item.Bronze), 6},            {typeof(script.item.SteelGem), 2}}}, 
            {"22,82,10000,Headgear", new Dictionary<Type, int>(){         {typeof(script.item.IronGem), 2},         {typeof(script.item.Bronze), 8},            {typeof(script.item.TungstenGem), 3},       {typeof(script.item.DogBone), 5}}}, 
            {"23,90,10000,Robe", new Dictionary<Type, int>(){             {typeof(script.item.AluminiumGem), 2},    {typeof(script.item.SteelGem), 1},          {typeof(script.item.NickelGem), 2},         {typeof(script.item.가죽), 5},              {typeof(script.item.고무), 2}}  }, 
            {"23,90,10000,Hanger", new Dictionary<Type, int>(){           {typeof(script.item.Bronze), 5},          {typeof(script.item.CadmiumGem), 5},        {typeof(script.item.TungstenGem), 2}}  }, 
            {"24,97,10000,LargeShield", new Dictionary<Type, int>(){      {typeof(script.item.IronGem), 3},         {typeof(script.item.CopperGem), 1},         {typeof(script.item.TungstenGem), 4},       {typeof(script.item.SteelGem), 4}}}, 
            {"25,105,10000,LeatherArmor", new Dictionary<Type, int>(){    {typeof(script.item.LeadGem), 1},         {typeof(script.item.NickelGem), 3},         {typeof(script.item.가죽), 6},              {typeof(script.item.고무), 4}}}, 
            {"25,105,10000,Saver", new Dictionary<Type, int>(){           {typeof(script.item.IronGem), 4},         {typeof(script.item.Bronze), 4},            {typeof(script.item.CadmiumGem), 7}}  }, 
            {"26,112,10000,Flail", new Dictionary<Type, int>(){           {typeof(script.item.IronGem), 3},         {typeof(script.item.Bronze), 5},            {typeof(script.item.SteelGem), 7}}  }, 
            {"27,124,10000,Simitar", new Dictionary<Type, int>(){         {typeof(script.item.Bronze), 2},          {typeof(script.item.CadmiumGem), 13}}  }, 
            {"27,124,10000,HardLeather", new Dictionary<Type, int>(){     {typeof(script.item.LeadGem), 1},         {typeof(script.item.CadmiumGem), 4},        {typeof(script.item.가죽), 7},              {typeof(script.item.고무), 3},              {typeof(script.item.톱밥), 1}}  }, 
            {"28,135,10000,Helmet", new Dictionary<Type, int>(){          {typeof(script.item.IronGem), 2},         {typeof(script.item.AluminiumGem), 2},      {typeof(script.item.Bronze), 5},            {typeof(script.item.TungstenGem), 2},       {typeof(script.item.NickelGem), 2}}  }, 
            {"29,150,10000,TwoBladedAxe", new Dictionary<Type, int>(){    {typeof(script.item.IronGem), 7},         {typeof(script.item.CopperGem), 3},         {typeof(script.item.LeadGem), 1},           {typeof(script.item.AluminiumGem), 2},      {typeof(script.item.Bronze), 1}}  }, 
            {"29,150,10000,TowerShield", new Dictionary<Type, int>(){     {typeof(script.item.IronGem), 5},         {typeof(script.item.CopperGem), 2},         {typeof(script.item.LeadGem), 1},           {typeof(script.item.Bronze), 1},            {typeof(script.item.TungstenGem), 5},           {typeof(script.item.고무), 1}}  }, 
            
            {"30,18,10000,Cutlass", new Dictionary<Type, int>(){          {typeof(script.item.IronGem), 4},        {typeof(script.item.Bronze), 4},             {typeof(script.item.NickelGem), 7}}  }, 
            {"31,18,10000,Hammer", new Dictionary<Type, int>(){           {typeof(script.item.IronGem), 3},        {typeof(script.item.Bronze), 2},             {typeof(script.item.TungstenGem), 4},       {typeof(script.item.SteelGem), 6},          {typeof(script.item.CobaltGem), 1}}  }, 
            {"31,18,10000,EnamelLeather", new Dictionary<Type, int>(){    {typeof(script.item.AluminiumGem), 1},   {typeof(script.item.TungstenGem), 8},        {typeof(script.item.조가비), 7}}  }, 
            {"32,18,10000,IronSword", new Dictionary<Type, int>(){        {typeof(script.item.IronGem), 7},        {typeof(script.item.Bronze), 2},             {typeof(script.item.SteelGem), 6},          {typeof(script.item.NickelGem), 1}}}, 
            {"33,18,10000,ToughLeather", new Dictionary<Type, int>(){     {typeof(script.item.CadmiumGem), 1},     {typeof(script.item.SteelGem), 1},           {typeof(script.item.NickelGem), 2},         {typeof(script.item.자갈), 3},              {typeof(script.item.가죽), 8}}  }, 
            {"33,18,10000,CoatingShield", new Dictionary<Type, int>(){    {typeof(script.item.IronGem), 1},        {typeof(script.item.CopperGem), 1},          {typeof(script.item.AluminiumGem), 2},      {typeof(script.item.TungstenGem), 5},       {typeof(script.item.ZirconiumGem), 6},          {typeof(script.item.대리석), 2}}  }, 
            {"34,18,10000,BroadSword", new Dictionary<Type, int>(){       {typeof(script.item.IronGem), 4},        {typeof(script.item.Bronze), 3},             {typeof(script.item.CadmiumGem), 5},        {typeof(script.item.NickelGem), 4}}}, 
            {"35,18,10000,Crown", new Dictionary<Type, int>(){            {typeof(script.item.CopperGem), 2},      {typeof(script.item.LeadGem), 1},            {typeof(script.item.SteelGem), 2},          {typeof(script.item.ZirconiumGem), 8},      {typeof(script.item.CobaltGem), 5}}  }, 
            {"35,18,10000,Hack", new Dictionary<Type, int>(){             {typeof(script.item.SteelGem), 9},       {typeof(script.item.TitaniumGem), 6},        {typeof(script.item.화강석), 3}}  }, 
            {"36,18,10000,ChestGuard", new Dictionary<Type, int>(){       {typeof(script.item.IronGem), 1},        {typeof(script.item.AluminiumGem), 3},       {typeof(script.item.TungstenGem), 5},       {typeof(script.item.SteelGem), 4},          {typeof(script.item.CobaltGem), 2}}  }, 
            {"37,18,10000,BroadAxe", new Dictionary<Type, int>(){         {typeof(script.item.NickelGem), 8},      {typeof(script.item.TitaniumGem), 7}}  }, 
            {"37,18,10000,Falchion", new Dictionary<Type, int>(){         {typeof(script.item.TungstenGem), 10},   {typeof(script.item.CobaltGem), 5}}  },             
            {"38,18,10000,BattleArmor", new Dictionary<Type, int>(){      {typeof(script.item.AluminiumGem), 3},   {typeof(script.item.NickelGem), 2},          {typeof(script.item.CobaltGem), 5},         {typeof(script.item.고무), 8}}}, 
            {"39,18,10000,Claymore", new Dictionary<Type, int>(){         {typeof(script.item.IronGem), 4},        {typeof(script.item.SteelGem), 10},          {typeof(script.item.ZirconiumGem), 2},      {typeof(script.item.CobaltGem), 2},         {typeof(script.item.TitaniumGem), 2}}  }, 
            {"39,18,10000,BattleShield", new Dictionary<Type, int>(){     {typeof(script.item.IronGem), 3},        {typeof(script.item.NickelGem), 3},          {typeof(script.item.ZirconiumGem), 8},      {typeof(script.item.TitaniumGem), 4},       {typeof(script.item.DogBone), 2}}  }, 
            {"40,18,10000,WarHammer", new Dictionary<Type, int>(){        {typeof(script.item.IronGem), 2},        {typeof(script.item.CobaltGem), 8},          {typeof(script.item.TitaniumGem), 6},       {typeof(script.item.화강석), 4}}}, 
            {"40,18,10000,Gladius", new Dictionary<Type, int>(){          {typeof(script.item.IronGem), 2},        {typeof(script.item.CadmiumGem), 6},         {typeof(script.item.SteelGem), 12},         {typeof(script.item.ZirconiumGem), 2}}}, 
            {"41,18,10000,BoneMail", new Dictionary<Type, int>(){         {typeof(script.item.CobaltGem), 8},      {typeof(script.item.DogBone), 12}}  }, 
            {"42,18,10000,FieldCap", new Dictionary<Type, int>(){         {typeof(script.item.IronGem), 2},        {typeof(script.item.Bronze), 6},             {typeof(script.item.SteelGem), 14},         {typeof(script.item.AlloyGem), 1}}}, 
            {"42,18,10000,Tuck", new Dictionary<Type, int>(){             {typeof(script.item.SteelGem), 1},       {typeof(script.item.NickelGem), 5},          {typeof(script.item.ZirconiumGem), 1},      {typeof(script.item.CobaltGem), 3},         {typeof(script.item.AlloyGem), 1}}  }, 
            {"43,18,10000,RingMail", new Dictionary<Type, int>(){         {typeof(script.item.CadmiumGem), 1},     {typeof(script.item.TungstenGem), 2},        {typeof(script.item.SteelGem), 1},          {typeof(script.item.NickelGem), 2},         {typeof(script.item.ZirconiumGem), 3},          {typeof(script.item.CobaltGem), 2}}  }, 
            {"44,18,10000,LargeAxe", new Dictionary<Type, int>(){         {typeof(script.item.SteelGem), 4},       {typeof(script.item.ZirconiumGem), 1},       {typeof(script.item.CobaltGem), 4},         {typeof(script.item.TitaniumGem), 2}}}, 
            {"44,18,10000,LargeHack", new Dictionary<Type, int>(){        {typeof(script.item.SteelGem), 1},       {typeof(script.item.NickelGem), 2},          {typeof(script.item.CobaltGem), 4},         {typeof(script.item.TitaniumGem), 4}}},             
            {"45,18,10000,ArmShield", new Dictionary<Type, int>(){        {typeof(script.item.AluminiumGem), 1},   {typeof(script.item.TungstenGem), 1},        {typeof(script.item.ZirconiumGem), 2},      {typeof(script.item.CobaltGem), 5},         {typeof(script.item.TitaniumGem), 5}}  }, 
            {"46,18,10000,BastardSword", new Dictionary<Type, int>(){     {typeof(script.item.NickelGem), 3},      {typeof(script.item.ZirconiumGem), 1},       {typeof(script.item.CobaltGem), 8}}  }, 
            {"46,18,10000,ChainMail", new Dictionary<Type, int>(){        {typeof(script.item.Bronze), 1},         {typeof(script.item.SteelGem), 2},           {typeof(script.item.NickelGem), 1},         {typeof(script.item.CobaltGem), 7},         {typeof(script.item.AlloyGem), 1} }  },             
            {"47,18,10000,FullHelmet", new Dictionary<Type, int>(){       {typeof(script.item.IronGem), 2},        {typeof(script.item.SteelGem), 5},           {typeof(script.item.ZirconiumGem), 1},      {typeof(script.item.CobaltGem), 1},         {typeof(script.item.TitaniumGem), 3}}  }, 
            {"48,18,10000,SamuraiSword", new Dictionary<Type, int>(){     {typeof(script.item.Bronze), 2},         {typeof(script.item.TungstenGem), 2},        {typeof(script.item.SteelGem), 3},          {typeof(script.item.ZirconiumGem), 2},      {typeof(script.item.TitaniumGem), 5}}  }, 
            {"48,18,10000,Maul", new Dictionary<Type, int>(){             {typeof(script.item.TungstenGem), 2},    {typeof(script.item.SteelGem), 3},           {typeof(script.item.ZirconiumGem), 3},      {typeof(script.item.TitaniumGem), 7}}  }, 

            {"49,18,10000,ScaleMail", new Dictionary<Type, int>(){        {typeof(script.item.CadmiumGem), 1},     {typeof(script.item.TungstenGem), 1},        {typeof(script.item.SteelGem), 3},          {typeof(script.item.NickelGem), 2},         {typeof(script.item.ZirconiumGem), 2},          {typeof(script.item.TitaniumGem), 6}}  },             
            {"50,18,10000,FamilyShield", new Dictionary<Type, int>(){     {typeof(script.item.AluminiumGem), 2},   {typeof(script.item.TungstenGem), 1},        {typeof(script.item.SteelGem), 2},          {typeof(script.item.ZirconiumGem), 1},      {typeof(script.item.CobaltGem), 2},             {typeof(script.item.TitaniumGem), 12}}  }, 
            {"50,18,10000,SevenStarSword", new Dictionary<Type, int>(){   {typeof(script.item.SteelGem), 5},       {typeof(script.item.ZirconiumGem), 2},       {typeof(script.item.TitaniumGem), 2},       {typeof(script.item.AlloyGem), 1}}  }, 
            {"51,18,9000,BattleAxe", new Dictionary<Type, int>(){         {typeof(script.item.SteelGem), 3},       {typeof(script.item.CobaltGem), 2},          {typeof(script.item.TitaniumGem), 7}}  }, 
            {"52,18,9000,BreastPlate", new Dictionary<Type, int>(){       {typeof(script.item.TungstenGem), 1},    {typeof(script.item.SteelGem), 3},           {typeof(script.item.CobaltGem), 3},         {typeof(script.item.TitaniumGem), 4},       {typeof(script.item.AlloyGem), 1}}  }, 
            {"52,18,9000,CoreaSword", new Dictionary<Type, int>(){        {typeof(script.item.SteelGem), 4},       {typeof(script.item.ZirconiumGem), 1},       {typeof(script.item.TitaniumGem), 3},       {typeof(script.item.PlatinumGem), 1},       {typeof(script.item.AlloyGem), 3}}  }, 
            {"53,18,9000,Halberd", new Dictionary<Type, int>(){           {typeof(script.item.ZirconiumGem), 5},   {typeof(script.item.CobaltGem), 4},          {typeof(script.item.TitaniumGem), 2},       {typeof(script.item.AlloyGem), 1}}  }, 
            {"54,18,9000,GrandeurHelmet", new Dictionary<Type, int>(){    {typeof(script.item.LeadGem), 1},        {typeof(script.item.SteelGem), 5},           {typeof(script.item.ZirconiumGem), 2},      {typeof(script.item.TitaniumGem), 2},       {typeof(script.item.AlloyGem), 3}}  }, 
            {"54,18,9000,TwoHandSword", new Dictionary<Type, int>(){      {typeof(script.item.SteelGem), 6},       {typeof(script.item.ZirconiumGem), 4},       {typeof(script.item.TitaniumGem), 2},       {typeof(script.item.AlloyGem), 1}}  },             
            {"55,18,9000,MailPlate", new Dictionary<Type, int>(){         {typeof(script.item.SteelGem), 4},       {typeof(script.item.NickelGem), 1},          {typeof(script.item.CobaltGem), 2},         {typeof(script.item.TitaniumGem), 6}}  }, 
            {"56,18,9000,GrandShield", new Dictionary<Type, int>(){       {typeof(script.item.TungstenGem), 2},    {typeof(script.item.SteelGem), 4},           {typeof(script.item.ZirconiumGem), 3},      {typeof(script.item.TitaniumGem), 3},       {typeof(script.item.AlloyGem), 7}}  }, 
            {"56,18,9000,GreatSword", new Dictionary<Type, int>(){        {typeof(script.item.SteelGem), 6},       {typeof(script.item.ZirconiumGem), 1},       {typeof(script.item.TitaniumGem), 4},       {typeof(script.item.PlatinumGem), 1},       {typeof(script.item.AlloyGem), 4}}  }, 
            {"57,18,9000,SamuraiPlate", new Dictionary<Type, int>(){      {typeof(script.item.SteelGem), 5},       {typeof(script.item.NickelGem), 3},          {typeof(script.item.CobaltGem), 2},         {typeof(script.item.TitaniumGem), 6}}  }, 
            {"58,18,9000,GreatMaul", new Dictionary<Type, int>(){         {typeof(script.item.CopperGem), 1},      {typeof(script.item.SteelGem), 7},           {typeof(script.item.ZirconiumGem), 3},      {typeof(script.item.AlloyGem), 7}}  }, 
            {"58,18,9000,GreatAxe", new Dictionary<Type, int>(){          {typeof(script.item.SteelGem), 6},       {typeof(script.item.ZirconiumGem), 4},       {typeof(script.item.TitaniumGem), 3},       {typeof(script.item.AlloyGem), 5}}  },
            {"59,18,9000,WidePlate", new Dictionary<Type, int>(){         {typeof(script.item.SteelGem), 6},       {typeof(script.item.ZirconiumGem), 6},       {typeof(script.item.TitaniumGem), 4},       {typeof(script.item.AlloyGem), 4} }  }, 
            {"60,18,9000,FullPlate", new Dictionary<Type, int>(){         {typeof(script.item.SteelGem), 5},       {typeof(script.item.ZirconiumGem), 4},       {typeof(script.item.TitaniumGem), 5},       {typeof(script.item.PlatinumGem), 2},       {typeof(script.item.AlloyGem), 7} }  },     
            {"61,18,8000,VikingHelmet", new Dictionary<Type, int>(){      {typeof(script.item.SteelGem), 1},       {typeof(script.item.CobaltGem), 6},          {typeof(script.item.TitaniumGem), 6},       {typeof(script.item.PlatinumGem), 3},       {typeof(script.item.AlloyGem), 6}}  }, 
            {"62,18,8000,ExtraScimitar", new Dictionary<Type, int>(){     {typeof(script.item.SteelGem), 3},       {typeof(script.item.NickelGem), 6},          {typeof(script.item.MoonStone), 4},         {typeof(script.item.헬리도오), 5},          {typeof(script.item.AquaMarine), 3}}  }, 
            {"63,18,8000,SkellGuard", new Dictionary<Type, int>(){        {typeof(script.item.SteelGem), 3},       {typeof(script.item.NickelGem), 4},          {typeof(script.item.SunStone), 8},          {typeof(script.item.ZirconiumGem), 6},      {typeof(script.item.PlatinumGem), 6},               {typeof(script.item.DogBone), 1}}  }, 
            {"64,18,8000,CrocodileArmor", new Dictionary<Type, int>(){    {typeof(script.item.BloodStone), 6},     {typeof(script.item.TitaniumGem), 4},        {typeof(script.item.헬리도오), 6},          {typeof(script.item.DarkStone), 8},         {typeof(script.item.고무), 1}}  }, 
            {"65,18,8000,Excalido", new Dictionary<Type, int>(){          {typeof(script.item.Crystal), 3},        {typeof(script.item.CobaltGem), 5},          {typeof(script.item.BloodStone), 6},        {typeof(script.item.짱돌), 4},              {typeof(script.item.AlloyGem), 7}}  },                   
            {"66,18,8000,PigBasket", new Dictionary<Type, int>(){         {typeof(script.item.SteelGem), 5},       {typeof(script.item.Emerald), 6},            {typeof(script.item.Pearl), 5},             {typeof(script.item.Sapphire), 7},          {typeof(script.item.조가비), 2}}  }, 
            {"67,18,8000,BarbarianAxe", new Dictionary<Type, int>(){      {typeof(script.item.ZirconiumGem), 2},   {typeof(script.item.DarkStone), 6},          {typeof(script.item.Amber), 2},             {typeof(script.item.짱돌), 7},              {typeof(script.item.Amethyst), 5}}  }, 
            {"68,18,8000,GagoilPlate", new Dictionary<Type, int>(){       {typeof(script.item.Topaz), 4},          {typeof(script.item.TitaniumGem), 8},        {typeof(script.item.헬리도오), 6},          {typeof(script.item.CatsEye), 6}}  }, 
            {"70,18,8000,RocketMaul", new Dictionary<Type, int>(){        {typeof(script.item.CobaltGem), 2},      {typeof(script.item.TitaniumGem), 2},        {typeof(script.item.DarkStone), 4},         {typeof(script.item.PlatinumGem), 4},       {typeof(script.item.AlloyGem), 10},                 {typeof(script.item.SteelGem), 3}}  }, 
            {"72,18,7000,WarriorAngel", new Dictionary<Type, int>(){      {typeof(script.item.헬리도오), 3},       {typeof(script.item.짱돌), 3},               {typeof(script.item.AlloyGem), 8},          {typeof(script.item.Amethyst), 6}}  }, 
            {"74,18,7000,GeneralSword", new Dictionary<Type, int>(){      {typeof(script.item.TungstenGem), 6},    {typeof(script.item.Jade), 6},               {typeof(script.item.Pearl), 1},             {typeof(script.item.Coral), 3},             {typeof(script.item.통뼈), 4},                       {typeof(script.item.Opal), 2}}  }, 
            {"76,18,7000,DiamondStick", new Dictionary<Type, int>(){      {typeof(script.item.SteelGem), 8},       {typeof(script.item.MoonStone), 3},          {typeof(script.item.AquaMarine), 6},        {typeof(script.item.Diamond), 1}}  }, 
            {"78,18,7000,Calypso", new Dictionary<Type, int>(){           {typeof(script.item.Emerald), 6},        {typeof(script.item.BloodStone), 6},         {typeof(script.item.PlatinumGem), 3},       {typeof(script.item.Opal), 1}}  }, 
            {"80,18,7000,DiamondArmor", new Dictionary<Type, int>(){      {typeof(script.item.SteelGem), 5},       {typeof(script.item.Crystal), 3},            {typeof(script.item.Emerald), 5},           {typeof(script.item.PlatinumGem), 5}}  }, 
            {"82,18,6000,PowerStrong", new Dictionary<Type, int>(){       {typeof(script.item.AlloyGem), 5},       {typeof(script.item.TigersEye), 3},          {typeof(script.item.DarkStone), 5},         {typeof(script.item.CatsEye), 2},           {typeof(script.item.NickelGem), 5}}  }, 
            {"84,18,6000,TitanMace", new Dictionary<Type, int>(){         {typeof(script.item.CobaltGem), 5},      {typeof(script.item.TitaniumGem), 6},        {typeof(script.item.짱돌), 9},              {typeof(script.item.AlloyGem), 4},          {typeof(script.item.SuperAlloyGem), 1}}  }, 
            {"86,18,6000,Gladiator", new Dictionary<Type, int>(){         {typeof(script.item.Crystal), 6},        {typeof(script.item.TitaniumGem), 6},        {typeof(script.item.PlatinumGem), 5},       {typeof(script.item.짱돌), 4},              {typeof(script.item.AlloyGem), 4}}  }, 
            {"88,18,6000,StormShield", new Dictionary<Type, int>(){       {typeof(script.item.CobaltGem), 9},      {typeof(script.item.DarkStone), 3},          {typeof(script.item.AlloyGem), 5},          {typeof(script.item.Amethyst), 5}}  }, 
            {"90,18,6000,Tomahawk", new Dictionary<Type, int>(){          {typeof(script.item.헬리도오), 8},       {typeof(script.item.DarkStone), 5},          {typeof(script.item.AlloyGem), 6},          {typeof(script.item.SuperAlloyGem), 1}}  }, 
            {"92,18,5000,LionHeart", new Dictionary<Type, int>(){         {typeof(script.item.SunStone), 6},       {typeof(script.item.LeopardJesper), 4},      {typeof(script.item.AlloyGem), 4},          {typeof(script.item.Amethyst), 5},          {typeof(script.item.Turquoise), 1}}  }, 
            {"94,18,5000,LaurelCrown", new Dictionary<Type, int>(){       {typeof(script.item.AlloyGem), 5},       {typeof(script.item.Sapphire), 4},           {typeof(script.item.PlatinumGem), 4},       {typeof(script.item.BlackPearl), 2},        {typeof(script.item.Amethyst), 3}}  },
 
            {"96,18,5000,DragonSkin", new Dictionary<Type, int>(){        {typeof(script.item.TungstenGem), 6},    {typeof(script.item.DarkStone), 4},          {typeof(script.item.AlloyGem), 2},          {typeof(script.item.Coral), 4},             {typeof(script.item.Opal), 3}}  }, 
            {"98,18,5000,DragonBlade", new Dictionary<Type, int>(){       {typeof(script.item.AlloyGem), 4},       {typeof(script.item.TitaniumGem), 4},        {typeof(script.item.Amber), 4},             {typeof(script.item.Coral), 2},             {typeof(script.item.CatsEye), 2},                   {typeof(script.item.Turquoise), 2}}  }, 
            {"98,18,5000,GiantHammer", new Dictionary<Type, int>(){       {typeof(script.item.TitaniumGem), 4},    {typeof(script.item.헬리도오), 5},           {typeof(script.item.AlloyGem), 5},          {typeof(script.item.Amethyst), 3},          {typeof(script.item.SuperAlloyGem), 1},             {typeof(script.item.마왕철), 1}}  }, 
        };
    }
}
