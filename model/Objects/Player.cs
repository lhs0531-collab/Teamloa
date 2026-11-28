using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.library;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace LKCamelot.model
{
    public class ExchangeEntry
    {
        public script.item.Item Item;
        public int Amount;
    }

    /// <summary>
    /// 교환창(@교환). 양쪽이 아이템(최대 4칸)과 골드를 올리고 둘 다 체크하면 한 번에 맞바꾼다.
    /// 플레이어마다 자기 쪽 ExchangeBox를 하나씩 갖고, Target.Exchange가 상대 쪽이다.
    /// </summary>
    public class ExchangeBox
    {
        public readonly Player Owner;
        public readonly Player Target;
        public List<ExchangeEntry> CellItem = new List<ExchangeEntry>();
        private ulong m_Gold = 0;
        private bool Check = false;

        public const int MaxCell = 4;

        // 교환 확정은 한 번에 하나씩만 처리한다. (두 사람의 아이템·골드를 함께 바꾸므로)
        private static readonly object ConfirmLock = new object();

        public ExchangeBox(Player player, Player target)
        {
            Owner = player;
            Owner.Exchange = this;

            Target = target;
            if (Target.Exchange == null)
                Target.Exchange = new ExchangeBox(target, player);

            player.client.SendPacket(new ShowExchangeBox(player, target).Compile());
        }

        private ExchangeBox Other { get { return Target.Exchange; } }

        /// <summary>이 아이템이 교환창에 올라가 있는지. 올라간 아이템은 사용·버리기·판매 등이 막힌다.</summary>
        public bool Contains(script.item.Item item)
        {
            return item != null && CellItem.Any(xe => xe.Item.m_Serial == item.m_Serial);
        }

        public ulong Price
        {
            get { return m_Gold; }
        }

        /// <summary>골드 제시. 클라이언트의 골드 입력 패킷 형식이 확정되면 핸들러가 호출한다.</summary>
        public bool SetGold(ulong gold)
        {
            if (Check || Other == null || Other.Check)
            {
                SendIfOnline(Owner, new UpdateChatBox(0x00, 0xff, 1, "체크 상태입니다.").Compile());
                return false;
            }
            if (gold > Owner.Gold)
            {
                SendIfOnline(Owner, new UpdateChatBox(0x00, 0xff, 1, "골드가 부족합니다.").Compile());
                return false;
            }
            if (gold > uint.MaxValue)               // ShowExchangeBox가 uint로 전송
                return false;

            m_Gold = gold;
            Refresh();
            return true;
        }

        public bool AddItem(int slot, int amount)
        {
            if (Check || Other == null || Other.Check)
            {
                SendIfOnline(Owner, new UpdateChatBox(0x00, 0xff, 1, "체크 상태입니다.").Compile());
                return false;
            }
            if (slot < 0 || slot >= 24)
                return false;
            if (CellItem.Count >= MaxCell)
            {
                SendIfOnline(Owner, new UpdateChatBox(0x00, 0xff, 1, "더 이상 올릴 수 없습니다.").Compile());
                return false;
            }

            var item = Owner.Inventory.Where(xe => xe.InvSlot == slot).FirstOrDefault();
            if (item == null)
                return false;
            if (Contains(item))
                return false;

            // 수량 지정 패킷이 확정되지 않아 겹치는 아이템은 전량을 올린다.
            if (!item.Stackable)
                amount = 1;
            else if (amount <= 0 || amount > item.Quantity)
                amount = item.Quantity;

            CellItem.Add(new ExchangeEntry { Item = item, Amount = amount });
            Refresh();

            // 교환창에는 아이콘만 보이므로 무엇을 올렸는지 양쪽에 글로 알린다.
            Notify(string.Format("[교환] {0}님이 {1}번 칸에 {2} 올렸습니다.", Owner.Name, CellItem.Count, Describe(item, amount)));
            return true;
        }

        /// <summary>교환창에서 아이템 내리기. 클라이언트의 내리기 패킷 형식이 확정되면 핸들러가 호출한다.</summary>
        public bool RemoveItem(int cellIndex)
        {
            if (Check || Other == null || Other.Check)
                return false;
            if (cellIndex < 0 || cellIndex >= CellItem.Count)
                return false;

            var removed = CellItem[cellIndex];
            CellItem.RemoveAt(cellIndex);
            Refresh();

            Notify(string.Format("[교환] {0}님이 {1} 내렸습니다.", Owner.Name, Describe(removed.Item, removed.Amount)));
            return true;
        }

        private static string Describe(script.item.Item item, int amount)
        {
            string name = (item.NPrefix() + " " + item.Name).Trim();
            return item.Stackable ? string.Format("{0} {1}개를", name, amount) : name + "(을)를";
        }

        private void Notify(string message)
        {
            SendIfOnline(Owner, new UpdateChatBox(0x00, 0xff, 1, message).Compile());
            SendIfOnline(Target, new UpdateChatBox(0x00, 0xff, 1, message).Compile());
        }

        public bool isCheck
        {
            get { return Check; }
            set
            {
                if (Check == value)
                    return;
                if (Other == null)
                {
                    Cancle();
                    return;
                }

                Check = value;
                Refresh();

                if (Check && Other.Check)
                    Confirm();
            }
        }

        /// <summary>올린 아이템이 아직 내 인벤토리(0~23)에 있고 수량이 충분한지</summary>
        public bool ItemCheck()
        {
            var inv = Owner.Inventory;
            foreach (var e in CellItem)
            {
                var cur = inv.Where(xe => xe.m_Serial == e.Item.m_Serial).FirstOrDefault();
                if (cur == null)
                    return false;
                if (cur.Quantity < e.Amount)
                    return false;
            }
            return true;
        }

        private bool Preconditions(out string reason)
        {
            reason = null;
            var theirs = Other;
            if (theirs == null) { reason = "상대방이 없습니다."; return false; }
            if (!Owner.loggedIn || !Target.loggedIn || Owner.client == null || Target.client == null)
            { reason = "상대방이 접속 중이 아닙니다."; return false; }
            if (Owner.m_Channel != Target.m_Channel || Owner.Map != Target.Map || World.Dist2d(Owner.Loc, Target.Loc) >= 10)
            { reason = "거리가 너무 멉니다."; return false; }
            if (!ItemCheck() || !theirs.ItemCheck()) { reason = "아이템이 변경되었습니다."; return false; }
            if (Owner.Gold < m_Gold || Target.Gold < theirs.m_Gold) { reason = "골드가 부족합니다."; return false; }
            // 보수적 검사: 상대가 내주어 비는 칸은 계산하지 않는다. 이 조건이면 이동 도중 실패가 없어 되돌릴 일이 없다.
            if (Target.GetFreeSlots() < CellItem.Count) { reason = Target.Name + "의 빈 칸이 부족합니다."; return false; }
            if (Owner.GetFreeSlots() < theirs.CellItem.Count) { reason = Owner.Name + "의 빈 칸이 부족합니다."; return false; }
            return true;
        }

        private void Confirm()
        {
            lock (ConfirmLock)
            {
                var theirs = Other;
                string reason;

                if (!Preconditions(out reason))
                {
                    SendIfOnline(Owner, new UpdateChatBox(0x00, 0xff, 1, reason).Compile());
                    SendIfOnline(Target, new UpdateChatBox(0x00, 0xff, 1, reason).Compile());
                    Cancle();
                    return;
                }

                // 골드: 대입 전에 결과값을 모두 계산한다. 넘치면 아무것도 바꾸지 않고 취소한다.
                ulong ownerGold, targetGold;
                try
                {
                    checked
                    {
                        ownerGold = Owner.Gold - m_Gold + theirs.m_Gold;
                        targetGold = Target.Gold - theirs.m_Gold + m_Gold;
                    }
                }
                catch (OverflowException)
                {
                    Cancle();
                    return;
                }

                var mineSnapshot = CellItem.ToList();
                var theirsSnapshot = theirs.CellItem.ToList();

                // 교환창을 먼저 비워야 올린 아이템의 잠금이 풀려 이동할 수 있다.
                CellItem.Clear();
                theirs.CellItem.Clear();

                foreach (var e in mineSnapshot)
                    Target.Event_Give(Owner, e.Item, e.Amount);
                foreach (var e in theirsSnapshot)
                    Owner.Event_Give(Target, e.Item, e.Amount);

                if (ownerGold != Owner.Gold)
                    Owner.Gold = ownerGold;
                if (targetGold != Target.Gold)
                    Target.Gold = targetGold;

                WriteLog(mineSnapshot, theirsSnapshot, m_Gold, theirs.m_Gold);

                SendIfOnline(Owner, new UpdateChatBox(0x00, 0xff, 1, "교환 성공하였습니다.").Compile());
                SendIfOnline(Target, new UpdateChatBox(0x00, 0xff, 1, "교환 성공하였습니다.").Compile());
                Close();
            }
        }

        private void WriteLog(List<ExchangeEntry> mine, List<ExchangeEntry> theirs, ulong myGold, ulong theirGold)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendFormat("{0} {1}->{2} gold={3} items=", DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]"), Owner.Name, Target.Name, myGold);
                foreach (var e in mine)
                    sb.AppendFormat("[{0}:{1}:{2}:{3}]", (int)e.Item.m_Serial, e.Item.Name, e.Item.Stage, e.Amount);
                sb.AppendFormat(" | {0}->{1} gold={2} items=", Target.Name, Owner.Name, theirGold);
                foreach (var e in theirs)
                    sb.AppendFormat("[{0}:{1}:{2}:{3}]", (int)e.Item.m_Serial, e.Item.Name, e.Item.Stage, e.Amount);
                io.IOClient.WriteExchangeLog(sb.ToString());
            }
            catch (Exception ex)
            {
                // 로그 실패가 이미 끝난 교환을 되돌리면 안 된다.
                Console.WriteLine("Exchange log failed: " + ex.Message);
            }
        }

        public void Cancle()
        {
            SendIfOnline(Owner, new UpdateChatBox(0x00, 0xff, 1, "취소되었습니다.").Compile());
            SendIfOnline(Target, new UpdateChatBox(0x00, 0xff, 1, "취소되었습니다.").Compile());
            Close();
        }

        private void Close()
        {
            SendIfOnline(Owner, new CanselExchangeBox().Compile());
            SendIfOnline(Target, new CanselExchangeBox().Compile());

            if (Owner.Exchange == this)
                Owner.Exchange = null;
            if (Target.Exchange != null && Target.Exchange.Target == Owner)
                Target.Exchange = null;
        }

        // 양쪽 교환창 화면을 다시 그린다.
        private void Refresh()
        {
            var theirs = Other;
            if (theirs == null)
                return;
            SendIfOnline(Owner, new ShowExchangeBox(Owner, Target).Compile());
            SendIfOnline(Target, new ShowExchangeBox(Target, Owner).Compile());
        }

        private static void SendIfOnline(Player p, byte[] packet)
        {
            if (p != null && p.loggedIn && p.client != null)
                p.client.SendPacket(packet);
        }
    }

    public class Player : BaseObject
    {
        public ExchangeBox Exchange = null;

        public ulong StaticTime;
        public model.BaseGuild Guild = null;

        public bool SoundFlag = false, ChatBlack = false, PK = false, GuildJoinFlag = false, WhispBlock = false, SystemBlock  = false;

        public long pkhistory = 0;
        public long pkhistoryTime
        {
            get{ return pkhistory;}
            set 
            {
                if( pkhistory == value)
                    return;
                int tempPKCount = PKCount;
                pkhistory = value;

                if (PKCount != tempPKCount && this.client != null)
                    client.SendPacket(new Attrib(this).Compile());
            }
        }                    // 피케이 시간
        
        public DateTime CreateTime;                     // 케릭터 생성일자
        public DateTime LastAccessTime;                 // 마지막 접근 일자    
        public string IP { get; set; }                  // IP 주소
        public Point2D CombackLoc;                      // 컴백 위치
        public string CombackMap;                       // 컴백 맵
        public bool Ben;
        public bool neighbor = true;                    // 서버 이웃
        public bool BenNeighbor = false;                // 벤 이웃
        public bool IPNeighbor = false;                 // 아이피 중복 이웃

        // 이번 접속 동안만 유효한 이웃마을 백성 해제. 도우미에게 골드를 내면 켜지고, 접속을 끊으면 꺼진다. (저장하지 않는다)
        public bool CitizenPass = false;

        /// <summary>
        /// 이웃마을 백성 여부. 이웃마을 백성은 기본 능력치가 절반으로 적용된다.
        /// 서버 이웃마을 설정이 켜져 있으면 Settings.NeighborLevel 이상인 캐릭터는 자동으로 이웃마을 백성이 되고,
        /// 도우미에게 해제를 받으면(CitizenPass) 접속을 끊을 때까지 마지막왕국 백성이 된다.
        /// 중복 아이피·벤으로 걸린 이웃은 해제 대상이 아니다.
        /// </summary>
        public bool IsNeighbor
        {
            get
            {
                // 운영자는 이웃마을 백성이 되지 않는다.
                if (Settings.GMS.Contains(Name))
                    return false;
                if (IPNeighbor || BenNeighbor)
                    return true;
                return Settings.Neighbor && Level >= Settings.NeighborLevel && neighbor && !CitizenPass;
            }
        }


        public List<string> WhispBlockList = new List<string>();


        public LKCamelot.io.IOClient client;
        public string Name, Pass;
        public Class m_Class;
        public byte Stage;
        public ushort m_Str, m_Men, m_Dex, m_Vit, m_AC, m_Hit, m_Dam;
        public ushort b_Str, b_Men, b_Dex, b_Vit;
        public uint b_Extra;            // 미분배 턴. 전직·턴 초기화로 회수된 턴이 여기 쌓인다. 저장 컬럼 40번째
        public uint m_Extra;
        public short pStr, pMen, pDex, pVit;
        public ushort apStr, apMen, apDex, apVit;
        public int m_HP, m_MP, m_HPCur, m_MPCur;
        public short m_Level;
        public int m_XP, m_XPNext;
        public ulong m_Gold;
        private SpriteEquip m_eqFlags;
        public byte[] Buffs = new byte[] { 0, 0, 0, 0 };
        public List<script.spells.Spell> m_MagicLearned = new List<script.spells.Spell>();
        //public List<Serial> InstancedObjects = new List<Serial>();
        public int m_AttackSpeed = 450;
        public int m_CastSpeed = 1000;
        public int m_MoveSpeed = 450;
        public int RegenTick = 15000;
        public long LastRegen = 0, autohittick = 0, lastmove = 0;
        public bool AutoHit, AutoLoot = false;
        public int m_Promo;
        public bool loggedIn = false;
        public int apistate = 0;
        public long keepalive = Server.tickcount.ElapsedMilliseconds;

        public ConcurrentDictionary<Serial, BaseObject> InstancedObjects = new ConcurrentDictionary<Serial, BaseObject>();
        public ConcurrentDictionary<Serial, BaseObject> InstancedRangeObjects = new ConcurrentDictionary<Serial, BaseObject>();
        public ConcurrentDictionary<int, script.item.Item> Equipped2 = new ConcurrentDictionary<int, script.item.Item>();

        public List<Point2D> Move = new List<Point2D>();
        public ConcurrentDictionary<long, string> spelltrace = new ConcurrentDictionary<long, string>();
        public ConcurrentDictionary<long, Player> PKhistoryPlayer = new ConcurrentDictionary<long, Player>();

        public Color color = Color.None;
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                if (color == value)
                    return;
                color = value;
                World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new ChangeObjectSpritePlayer(this).Compile()));
            }
        }

        //public Player Clone()
        //{
        //    var temp = new Player(Serial.NewMobile);
        //    temp.Name = Name;
        //    temp.m_Class = Class;
        //    temp.Stage = Stage;
        //    temp.LightRad = LightRad;
        //    temp.Transparancy = Transparancy;
        //    temp.m_Map = Map;
        //    temp.m_Loc = new Point2D(m_Loc.X, m_Loc.Y);
        //    temp.Face = Face;
        //    temp.m_Str = m_Str;
        //    temp.m_Men = m_Men;
        //    temp.m_Dex = m_Dex;
        //    temp.m_Vit = m_Vit;
        //    temp.m_Extra = Extra;
        //    temp.m_HPCur = m_HPCur;
        //    temp.m_MPCur = m_MPCur;
        //    temp.m_Level = m_Level;
        //    temp.m_XP = XP;
        //    temp.m_Gold = Gold;
        //    temp.m_MagicLearned = m_MagicLearned.ConvertAll<script.spells.Spell>(item => item);
        //    temp.m_AttackSpeed = m_AttackSpeed;
        //    temp.m_CastSpeed = m_CastSpeed;

        //    return temp;
        //}

        public int AttackSpeed
        {
            get
            {
                var temp = m_AttackSpeed;
                foreach (var item in Equipped2.Values)
                    temp -= item.ReduceSwing;

                return temp;
            }
            set { m_AttackSpeed = value; }
        }
        public int CastSpeed
        {
            get
            {
                var temp = m_CastSpeed;
                foreach (var item in Equipped2.Values)
                    temp -= item.ReduceCast;

                if (Class == Class.Swordsman || Class == Class.Knight)
                    temp -= GetStat("men") / 10;


                if (temp <= 600)
                    temp = 600;

                return temp;
            }
            set { m_CastSpeed = value; }
        }

        public List<script.item.Item> Inventory
        {
            get
            {
                return World.NewItems.Where(xe =>
                    (xe.Value.Parent == this || xe.Value.ParSer == this.m_Serial) &&
                    xe.Value.InvSlot >= 0 && xe.Value.InvSlot < 24).Select(xe => xe.Value).ToList();
            }
        }

        public int Promo
        {
            get
            {
                if (m_Level < 101)
                    return 0;
                else
                    return ((m_Level - 101) / 20) + 1;
            }
        }


        public string FullClass
        {
            get
            {
                if (Class == Class.Knight)
                {
                    if (Promo == 0) return "기사";
                    if (Promo == 1) return "DRAGON KNIGHT";
                    if (Promo == 2) return "NOVA";
                    if (Promo == 3) return "THE LORD";
                    if (Promo == 4) return "ROYAL KNIGHT";
                    if (Promo == 5) return "SAINT KNIGHT";
                    if (Promo == 6) return "CRUX";
                    if (Promo == 7) return "GUARDIANSHIP";
                    if (Promo == 8) return "Paladin";
                    if (Promo == 9) return "Black Swordsman";
                    if (Promo == 10) return "Guardian Deity";
                    if (Promo == 11) return "Skull Knight";
                    if (Promo == 12) return "Berserker";
                }
                if (Class == Class.Swordsman)
                {
                    if (Promo == 0) return "검객";
                    if (Promo == 1) return "SWORD MASTER";
                    if (Promo == 2) return "ELEMENTALICA";
                    if (Promo == 3) return "DRAGON CLOUD";
                    if (Promo == 4) return "ANGEL HEART";
                    if (Promo == 5) return "HAE-DONG SAMURANG";
                    if (Promo == 6) return "BACK-DU GUMSUNG";
                    if (Promo == 7) return "DAEUNG DAEGUM";
                    if (Promo == 8) return "Kensei";
                    if (Promo == 9) return "Royal Assassin";
                    if (Promo == 10) return "Fist of the South Star";
                    if (Promo == 11) return "Fist of the North Star";
                    if (Promo == 12) return "Transcended";
                }
                if (Class == Class.Shaman)
                {
                    if (Promo == 0) return "샤먼";
                    if (Promo == 1) return "BODHI-SATTVA";
                    if (Promo == 2) return "YOGI";
                    if (Promo == 3) return "DIABOLIC TOIST";
                    if (Promo == 4) return "ZODIAC MONK";
                    if (Promo == 5) return "POONG-BACK";
                    if (Promo == 6) return "SAINT PURGATORY";
                    if (Promo == 7) return "TAEGK SON";
                    if (Promo == 8) return "Saint";
                    if (Promo == 9) return "Wild Mage";
                    if (Promo == 10) return "Blooming Lotus";
                    if (Promo == 11) return "Seeded One";
                    if (Promo == 12) return "Eternal";
                }
                if (Class == Class.Wizard)
                {
                    if (Promo == 0) return "위저드";
                    if (Promo == 1) return "PRIEST";
                    if (Promo == 2) return "BISHOP";
                    if (Promo == 3) return "MARTYR";
                    if (Promo == 4) return "HOLY WIZARD";
                    if (Promo == 5) return "GODLY MESSENGER";
                    if (Promo == 6) return "SACROSANT";
                    if (Promo == 7) return "GLORIANSHIP";
                    if (Promo == 8) return "Grand Master";
                    if (Promo == 9) return "Archangel";
                    if (Promo == 10) return "Prophet";
                    if (Promo == 11) return "The Fallen";
                    if (Promo == 12) return "Godhand";
                }
                if (Class == Class.Beginner)
                {
                    if (Promo >= 1) return "평민";
                }

                return "평민";

            }
        }

        public script.item.Item Weapon
        {
            get
            {
                return World.NewItems.Where(xe => xe.Value.m_Parent != null && xe.Value.m_Parent == this
                    && xe.Value.InvSlot == 27).FirstOrDefault().Value;
            }
        }

        public short PromoLevel
        {
            get
            {
                if (Promo == 0)
                    return Level;
                if (Promo > 0)
                {
                    return (short)(((Level - 100) - (Promo * 20 - 20)));
                }

                return 1;
            }
        }

        public short Level
        {
            get { return m_Level; }
            set
            {
                if (value > Settings.MaxLevel)
                    value = Settings.MaxLevel;

                if (firstlevel && m_Level != value)
                {
                    m_Level = value;
                }
                m_Level = value;
                firstlevel = true;

            }
        }
        bool firstlevel = false;

        public byte[] Appearance
        {
            get
            {
                var temp = new byte[10];
                temp[1] = ClassBase;

                if (temp[1] == 5 || temp[1] == 6)
                    return temp;

                else
                {
                    if ((Class & Class.Beginner) != 0)
                    {
                        if ((eqFlags & LKCamelot.library.SpriteEquip.Shield) != 0)
                            temp[3] = 1;
                        if ((eqFlags & LKCamelot.library.SpriteEquip.Sword) != 0)
                            temp[4] = 1;
                        if ((eqFlags & LKCamelot.library.SpriteEquip.Hammer) != 0
                            || (eqFlags & LKCamelot.library.SpriteEquip.Axe) != 0)
                            temp[6] = 1;
                    }
                    else if ((Class & Class.Knight) != 0)
                    {
                        if ((eqFlags & LKCamelot.library.SpriteEquip.Shield) != 0)
                            temp[3] = 1;
                        if ((eqFlags & LKCamelot.library.SpriteEquip.Sword) != 0)
                            temp[4] = 1;
                        if ((eqFlags & LKCamelot.library.SpriteEquip.Hammer) != 0)
                            temp[5] = 1;
                        if ((eqFlags & LKCamelot.library.SpriteEquip.Axe) != 0)
                            temp[6] = 1;
                    }
                    else if ((Class & Class.Swordsman) != 0)
                    {
                        if ((eqFlags & LKCamelot.library.SpriteEquip.Sword) != 0
                            || (eqFlags & LKCamelot.library.SpriteEquip.Hammer) != 0
                            || (eqFlags & LKCamelot.library.SpriteEquip.Axe) != 0)
                            temp[4] = 1;
                    }
                    else if ((Class & Class.Shaman) != 0 || (Class & Class.Wizard) != 0)
                    {
                        if ((eqFlags & LKCamelot.library.SpriteEquip.Cane) != 0)
                            temp[7] = 1;
                        if ((eqFlags & LKCamelot.library.SpriteEquip.Cane2) != 0)
                            temp[7] = 2;
                        if ((eqFlags & LKCamelot.library.SpriteEquip.Spear) != 0)
                            temp[7] = 1;
                        if ((eqFlags & LKCamelot.library.SpriteEquip.Spear2) != 0)
                            temp[7] = 2;
                    }
                }
                var armor = World.NewItems.Where(xe => xe.Value.m_Parent != null && xe.Value.m_Parent == this
                    && xe.Value.InvSlot == 28)
                    .FirstOrDefault();
                if (armor.Value != null)
                {
                    var tempee = (armor.Value as script.item.BaseArmor).APStage;
                    temp[2] = (byte)tempee;
                }
                return temp;
            }
        }

        public List<script.item.Item> Equipped
        {
            get
            {
                return World.NewItems.Where(xe => (xe.Value.ParSer == m_Serial || xe.Value.Parent == this) && xe.Value.InvSlot >= 25 && xe.Value.InvSlot <= 30).Select(xe => xe.Value).ToList();
            }
        }
        public string EquippedSlotString(int slot)
        {
            
                script.item.Item aa = Equipped.Find(xe=>xe.InvSlot == slot);

                return (aa == null)?"":(aa.NPrefix()+ " " + aa.Name);   
        }
        public script.item.Item EquippedSlot(int slot)
        {
            return Equipped.Find(xe => xe.InvSlot == slot);
        }

        public uint Extra
        {
            get { return m_Extra; }
            set
            {
                if (value <= 0)
                    value = 0;
                m_Extra = value;
            }
        }

        public void AddStat(ref ushort stat)
        {
            if (m_Extra == 0 || stat >= ushort.MaxValue)
                return;
            Extra--;

            stat++;
        }

        public void AddStat(ref ushort stat, ushort amount)
        {
            if (m_Extra == 0 || amount + stat >= ushort.MaxValue)
                return;

            Extra -= amount;

            stat += amount;
        }
        public int TotalBounsStat
        {
            get
            {
                // 미분배 턴도 합계에 넣는다. 전직·초기화로 재분배하는 동안 서열이 떨어지지 않게 한다.
                return (int)(b_Str + b_Men + b_Dex + b_Vit + b_Extra);
            }
        }

        /// <summary>
        /// 턴으로 올린 속성(b_Str/b_Men/b_Dex/b_Vit)을 모두 회수해 미분배 턴(b_Extra)으로 돌린다.
        /// 합계는 변하지 않는다. 이미 모두 미분배 상태면 아무것도 하지 않는다(연속 전직해도 수치 불변).
        /// </summary>
        /// <returns>이번에 회수한 턴 수</returns>
        public uint ResetTurnStats()
        {
            uint moved = (uint)b_Str + (uint)b_Men + (uint)b_Dex + (uint)b_Vit;
            if (moved == 0)
                return 0;

            b_Extra += moved;
            b_Str = 0;
            b_Men = 0;
            b_Dex = 0;
            b_Vit = 0;

            // 생명·지력 턴이 빠지면 최대치가 줄어든다. 현재치가 최대치를 넘지 않게 맞춘다.
            // (HPCur setter를 쓰면 매직실드가 감소분을 마나로 흡수하므로 필드에 직접 넣는다)
            if (m_HPCur > HP)
                m_HPCur = HP;
            if (m_MPCur > MP)
                m_MPCur = MP;

            if (client != null)
            {
                client.SendPacket(new UpdateCharStats(this).Compile());
                client.SendPacket(new SetHP(this).Compile());
                client.SendPacket(new SetMP(this).Compile());
            }
            return moved;
        }

        /// <summary>미분배 턴이 남아 있으면 분배 방법을 안내한다.</summary>
        public void NotifyUnspentTurn()
        {
            if (b_Extra == 0 || client == null)
                return;

            client.SendPacket(new UpdateChatBox(Settings.TurnNoticeColor1, Settings.TurnNoticeColor2, 1,
                string.Format("분배하지 않은 턴 속성이 {0} 남아 있습니다. 아론에게 \"힘 [수량] 올려주세요\"라고 말하세요.", b_Extra)).Compile());
        }

        /// <summary>전직 책으로 직업을 바꾼다. 전직 시 턴 속성은 유지한다.</summary>
        public void ChangeClassByBook(Class newClass)
        {
            Class = newClass;
        }
        public ushort GetStat(string stat, bool bas = false)
        {
            uint temp = 0;
            if (stat == "str")
            {
                temp += m_Str;
                temp += b_Str;          // 턴
                if (m_Class == Class.Beginner)
                    temp += 5;
                else if (m_Class == Class.Knight)
                    temp += 30;
                else if (m_Class == Class.Swordsman)
                    temp += 20;
                else if (m_Class == Class.Wizard)
                    temp += 15;
                else if (m_Class == Class.Shaman)
                    temp += 15;

                if (bas == false && IsNeighbor)
                    temp = (uint)Math.Round((double)(temp / 2));

                if (bas == false)
                {
                    foreach (var item in Equipped2.Values)
                        temp += (uint)item.StrBonus;
                }
                return temp < ushort.MaxValue ? (ushort)temp : ushort.MaxValue;
            }
            if (stat == "men")
            {
                temp += m_Men;
                temp += b_Men;          // 턴
                if (m_Class == Class.Beginner)
                    temp += 5;
                else if (m_Class == Class.Knight)
                    temp += 10;
                else if (m_Class == Class.Swordsman)
                    temp += 5;
                else if (m_Class == Class.Wizard)
                    temp += 30;
                else if (m_Class == Class.Shaman)
                    temp += 25;

                if (bas == false && IsNeighbor)
                    temp = (uint)Math.Round((double)(temp / 2));

                if (bas == false)
                {
                    foreach (var item in Equipped2.Values)
                        temp += (uint)item.MenBonus;
                }
                return temp < ushort.MaxValue ? (ushort)temp : ushort.MaxValue;
            }
            if (stat == "dex")
            {
                temp += m_Dex;
                temp += b_Dex;          // 턴
                if (m_Class == Class.Beginner)
                    temp += 5;
                else if (m_Class == Class.Knight)
                    temp += 15;
                else if (m_Class == Class.Swordsman)
                    temp += 30;
                else if (m_Class == Class.Wizard)
                    temp += 15;
                else if (m_Class == Class.Shaman)
                    temp += 20;

                if (bas == false && IsNeighbor)
                    temp = (uint)Math.Round((double)(temp / 2));

                if (bas == false)
                {
                    foreach (var item in Equipped2.Values)
                        temp += (uint)item.DexBonus;
                }
                return temp < ushort.MaxValue ? (ushort)temp : ushort.MaxValue;
            }
            if (stat == "vit")
            {
                temp += m_Vit;
                temp += b_Vit;          // 턴
                if (m_Class == Class.Beginner)
                    temp += 5;
                else
                    temp += 10;

                if (bas == false && IsNeighbor)
                    temp = (uint)Math.Round( (double)(temp / 2));

                if (bas == false)
                {
                    foreach (var item in Equipped2.Values)
                        temp += (uint)item.VitBonus;
                }
                return temp < ushort.MaxValue ? (ushort)temp : ushort.MaxValue;
            }
            return 1;
        }

        public string SQLSaveString()
        {
            string temp = "";
            temp += "\'" + Convert.ToInt32(m_Serial) + "\', ";
            temp += "\'" + Name.ToString() + "\', ";
            temp += "\'" + Pass.ToString() + "\', ";
            temp += "\'" + Convert.ToInt32(Class) + "\', ";
            temp += "\'" + Stage + "\', ";
            temp += "\'" + LightRad + "\', ";
            temp += "\'" + Transparancy + "\', ";
            temp += "\'" + Map + "\', ";
            temp += "\'" + m_Loc.X + "\', ";
            temp += "\'" + m_Loc.Y + "\', ";
            temp += "\'" + Face + "\', ";
            temp += "\'" + apStr + "\', ";
            temp += "\'" + apMen + "\', ";
            temp += "\'" + apDex + "\', ";
            temp += "\'" + apVit + "\', ";
            temp += "\'" + Extra + "\', ";
            temp += "\'" + HP + "\', ";
            temp += "\'" + HPCur + "\', ";
            temp += "\'" + MP + "\', ";
            temp += "\'" + MPCur + "\', ";
            temp += "\'" + Level + "\', ";
            temp += "\'" + XP + "\', ";
            temp += "\'" + Gold + "\', ";
            temp += "\'" + MagicLearnedString() + "\', "; //Magic Learned
            //        temp += "\'" + BuffString() + "\', "; //buff string
            temp += "\'450\', ";
            temp += "\'1500\', ";
            //   temp += "\'" + AttackSpeed + "\', ";
            //   temp += "\'" + CastSpeed + "\', ";
            temp = temp.Substring(0, (temp.Count() - 2));
            return temp;
        }

        public string MagicLearnedString()
        {
            if (m_MagicLearned.Count == 0)
                return "0";

            var temp = "";
            foreach (var spell in m_MagicLearned)
            {
                temp += spell.GetType().Name.ToString() + " " + spell.Slot + " " + spell.SLevel2 + " " + spell.m_Level + "-";
            }

            return temp;
        }

        public void LoadMagic(string mag)
        {
            if (mag.Count() > 3)
            {
                var temp = mag.Split('-');
                foreach (var spell in temp)
                {
                    if (spell == "")
                        continue;
                    var temp2 = spell.Split(' ');
                    var activatorstring = "";
                    if (temp2[0].IndexOf("LKCamelot.script.spells.") != -1)
                        activatorstring = null;
                    else
                        activatorstring = "LKCamelot.script.spells.";
                    object tempspell = null;
                    try
                    {
                        tempspell = Activator.CreateInstance(Type.GetType(activatorstring + temp2[0]));
                    }
                    catch { continue; }
                    (tempspell as script.spells.Spell).Caster = this;
                    (tempspell as script.spells.Spell).Slot = Convert.ToInt32(temp2[1]);
                    (tempspell as script.spells.Spell).SLevel2 = Convert.ToInt32(temp2[2]);
                    (tempspell as script.spells.Spell).m_Level = Convert.ToInt32(temp2[3]);

                    if (!Settings.GMS.Contains(this.Name) && (tempspell as script.spells.Spell).Limit < Convert.ToInt32(temp2[3]))
                        (tempspell as script.spells.Spell).m_Level = (tempspell as script.spells.Spell).Limit;
                    else
                        (tempspell as script.spells.Spell).m_Level = Convert.ToInt32(temp2[3]);

                    m_MagicLearned.Add((tempspell as script.spells.Spell));
                    //    client.SendPacket(new CreateSlotMagic2((tempspell as script.spells.Spell)).Compile());
                }
            }
        }

        public string BuffString()
        {
            var temp = "6";
            return temp;
        }

        public ulong Gold
        {
            get { return m_Gold; }
            set
            {
                m_Gold = value;
                if (loggedIn)
                    client.SendPacket(new LKCamelot.model.GoldChange(this.m_Gold).Compile());
            }
        }

        public SpriteEquip eqFlags
        {
            get
            {
                SpriteEquip flags = 0;
                var equiped = World.NewItems.Where(xe => xe.Value.m_Parent != null && xe.Value.m_Parent == this
                    && xe.Value.InvSlot >= 25 && xe.Value.InvSlot <= 30)
                    .Select(xe => xe.Value);

                foreach (var gear in equiped)
                {
                    if (gear is script.item.BaseWeapon)
                    {
                        if ((gear as script.item.BaseWeapon).WeaponType == script.item.WeaponType.Axe ||
                            (gear as script.item.BaseWeapon).WeaponType == script.item.WeaponType.TwoAxe)
                            flags |= library.SpriteEquip.Axe;
                        if ((gear as script.item.BaseWeapon).WeaponType == script.item.WeaponType.Sword ||
                            (gear as script.item.BaseWeapon).WeaponType == script.item.WeaponType.TwoSword)
                            flags |= library.SpriteEquip.Sword;
                        if ((gear as script.item.BaseWeapon).WeaponType == script.item.WeaponType.Hammer ||
                            (gear as script.item.BaseWeapon).WeaponType == script.item.WeaponType.TwoHammer)
                            flags |= library.SpriteEquip.Hammer;
                        if ((gear as script.item.BaseWeapon).WeaponType == script.item.WeaponType.Cane)
                            flags |= library.SpriteEquip.Cane;
                        if ((gear as script.item.BaseWeapon).WeaponType == script.item.WeaponType.Cane2)
                            flags |= library.SpriteEquip.Cane2;
                        if ((gear as script.item.BaseWeapon).WeaponType == script.item.WeaponType.Spear)
                            flags |= library.SpriteEquip.Spear;
                        if ((gear as script.item.BaseWeapon).WeaponType == script.item.WeaponType.Spear2)
                            flags |= library.SpriteEquip.Spear2;
                    }
                    else if (gear is script.item.BaseArmor)
                    {
                        if ((gear as script.item.BaseArmor).ArmorType == script.item.ArmorType.Shield &&
                            (Class & Class.Swordsman) == 0 && 
                            (Class & Class.Shaman) == 0 && (Class & Class.Wizard) == 0)
                            flags |= library.SpriteEquip.Shield;
                    }
                }

                return flags;
            }
            set
            {
                m_eqFlags = value;
                World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new ChangeObjectSpritePlayer(this).Compile()));
            }
        }

        public byte ClassBase
        {
            get
            {
                if (Equipped2.Where(xe => xe.Value.GetType() == typeof(script.item.MinerSuit)).Any())
                    return 5;

                if (Equipped2.Where(xe => xe.Value.GetType() == typeof(script.item.BlackSmithSuit)).Any())
                    return 6;

                if (Class == Class.Beginner)
                    return 0;
                if (Class == Class.Knight)
                    return 1;
                if (Class == Class.Swordsman)
                    return 2;
                if (Class == Class.Wizard)
                    return 3;
                if (Class == Class.Shaman)
                    return 4;

                return 0;
            }
        }

        public Class Class
        {
            get { return m_Class; }
            set
            {
                if (value != m_Class)
                {
                    m_Class = value;
                    var removes = m_MagicLearned.Where(xe => xe.ClassReq != 0 && (xe.ClassReq & Class) == 0).Select(xe => xe).ToList();
                    foreach (var sp in removes)
                    {
                        m_MagicLearned.Remove(sp);
                        client.SendPacket(new DeleteSpell(sp.Slot).Compile());
                    }
                    var buffremoves = m_Buffs.Where(xe => xe.Value.ClassReq != 0 && (xe.Value.ClassReq & Class) == 0).Select(xe => xe).ToList();
                    foreach (var sp in buffremoves)
                    {
                        script.spells.Spell temp;
                        m_Buffs.TryRemove(sp.Key, out temp);
                    }
                    if (!(value == Class.Beginner))
                    {
                        RemoveChangeGear();
                        World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new ChangeObjectSprite(m_Serial, 0, Appearance).Compile()));
                        client.SendPacket(new SetLevelGold(this).Compile());
                        client.SendPacket(new UpdateCharStats(this).Compile());
                        World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new SetObjectEffects(this).Compile()));
                    }
                }
            }
        }

        public void RemoveChangeGear()
        {
            var equiped = World.NewItems.Where(xe => xe.Value.Parent == this
    && xe.Value.InvSlot >= 25 && xe.Value.InvSlot <= 30).Select(xe => xe.Value);

            foreach (var item in equiped)
            {
                if (item.ClassReq != 0 && (item.ClassReq & Class) == 0)
                    item.Unequip(this, item.InvSlot);
            }
        }

        public List<script.spells.Spell> MagicLearned
        {
            get { return m_MagicLearned; }
        }

        public virtual void SwapItems(LKCamelot.script.item.Item initem, LKCamelot.script.item.Item outitem, int outslot)
        {
            if (initem != null)
            {
                if (outitem == null)
                {
                    initem.InvSlot = (byte)outslot;
                    try
                    {
                        //    World.DBConnect.WriteQue.Enqueue(() => World.DBConnect.UpdateItem(initem));
                        //  World.DBConnect.UpdateItem(initem);
                    }
                    catch { Console.WriteLine("Update Item on Swap1 Failed"); }
                }
                else
                {
                    client.SendPacket(new DeleteItemSlot((byte)initem.InvSlot).Compile());
                    client.SendPacket(new DeleteItemSlot((byte)outitem.InvSlot).Compile());
                    outitem.m_InvSlot = (byte)initem.InvSlot;
                    initem.m_InvSlot = (byte)outslot;
                    client.SendPacket(new AddItemToInventory2(initem).Compile());
                    client.SendPacket(new AddItemToInventory2(outitem).Compile());
                    try
                    {
                        //     World.DBConnect.WriteQue.Enqueue(() => World.DBConnect.UpdateItem(initem));
                        //     World.DBConnect.WriteQue.Enqueue(() => World.DBConnect.UpdateItem(outitem));
                        //  World.DBConnect.UpdateItem(initem);
                        // World.DBConnect.UpdateItem(outitem);
                    }
                    catch { Console.WriteLine("Update Item on Swap2 Failed"); }
                }

            }
        }

        public void DeleteMagic(int slot)
        {
            if (slot < 40)
            {
                var spell = m_MagicLearned.Where(xe => xe.Slot == slot).FirstOrDefault();
                if (spell != null)
                {
                    m_MagicLearned.Remove(spell);
                    client.SendPacket(new DeleteSpell(slot).Compile());
                }
            }
        }

        public void SwapMagic(int inslot, int outslot)
        {
            var spell1 = m_MagicLearned.Where(xe => xe.Slot == inslot).FirstOrDefault();
            var target1 = m_MagicLearned.Where(xe => xe.Slot == outslot).FirstOrDefault();

            if (spell1 != null)
            {
                if (target1 == null)
                {
                    m_MagicLearned[m_MagicLearned.IndexOf(spell1)].Slot = outslot;
                    client.SendPacket(new DeleteSpell(inslot).Compile());
                    client.SendPacket(new CreateSlotMagic2(m_MagicLearned[m_MagicLearned.IndexOf(spell1)]).Compile());
                }
                else
                {
                    m_MagicLearned[m_MagicLearned.IndexOf(spell1)].Slot = outslot;
                    m_MagicLearned[m_MagicLearned.IndexOf(target1)].Slot = inslot;
                    client.SendPacket(new DeleteSpell(inslot).Compile());
                    client.SendPacket(new DeleteSpell(outslot).Compile());
                    client.SendPacket(new CreateSlotMagic2(m_MagicLearned[m_MagicLearned.IndexOf(spell1)]).Compile());
                    client.SendPacket(new CreateSlotMagic2(m_MagicLearned[m_MagicLearned.IndexOf(target1)]).Compile());
                }
            }
        }

        public bool LearnSpell(script.item.BaseSpellbook spellb)
        {
            var learned = m_MagicLearned.Find(xe => xe.Name.ToLower() == spellb.SpellTaught.Name.ToLower());
            if (learned != null)
            {
                if (GetStat("men") >= spellb.MenReq + (spellb.MenReqPl * learned.m_Level)
                    && GetStat("dex") >= spellb.DexReq + (spellb.DexReqPl * learned.m_Level)
                    && GetStat("str") >= spellb.StrReq + (spellb.StrReqPl * learned.m_Level)
                    && Level >= spellb.LevelReq + (spellb.LevelReqPl * learned.m_Level)
                    && (spellb.ClassReq == 0 || ((Class & spellb.ClassReq) != 0))
                    && learned.m_Level < learned.Limit)
                {
                    learned.m_Level++;
                    client.SendPacket(new CreateSlotMagic2(learned).Compile());
                    return true;
                }
            }
            else if (m_MagicLearned.Count() < 40)
            {
                if (GetStat("men") >= spellb.MenReq && GetStat("dex") >= spellb.DexReq && Level >= spellb.LevelReq
                    && (spellb.ClassReq == 0 || ((Class & spellb.ClassReq) != 0)))
                {
                    var spell2l = spellb.SpellTaught;
                    spell2l.Caster = this;
                    spell2l.Slot = GetFreeSpellSlot();
                    m_MagicLearned.Add(spell2l);
                    client.SendPacket(new CreateSlotMagic2(spell2l).Compile());
                    return true;
                }
            }
            return false;
        }

        public string LoadMap
        {
            get
            {
                var temp = LKCamelot.model.Map.FullMaps[this.m_Channel].Where(xe => xe.Key == m_Map).FirstOrDefault();
                return temp.Value.Filename + "," + temp.Key;
            }
        }

        public List<script.item.Item> LocalItems
        {
            get
            {
                return World.NewItems.Values.Where(xe => xe.m_Map != null && xe.m_Channel == this.m_Channel && xe.m_Map == Map).OrderByDescending(xe=>xe.DropTime).Select(xe => xe).ToList();
            }
        }
        public List<script.monster.Monster> LocalMonsters
        {
            get
            {
                //return World.NewMonsters.Values.Where(xe => xe.m_Map != null && xe.m_Map == Map && xe.m_Alive).Select(xe => xe).ToList();
                return World.NewMonsters[this.m_Channel].Values.Where(xe => xe.m_Map != null && xe.m_Map == Map).Select(xe => xe).ToList();
            }
        }

        public List<script.npc.BaseNPC> LocalNpcs
        {
            get
            {
                return World.NewNpcs[this.m_Channel].Where(xe => xe.Value.m_Map == Map && xe.Value.Alive == true).Select(xe => xe.Value).ToList();
            }
        }
        public List<script.npc.AutoShopNPC> LocalAutoNpcs
        {
            get
            {
                return World.NewAutoShop.Where(xe => xe.Value.m_Map == Map && xe.Value.m_Channel == this.m_Channel).Select(xe => xe.Value).ToList();
            }
        }
        public List<Player> LocalPlayers
        {
            get
            {
                var LocalPlayers = Server.world.handler.add[this.m_Channel].Where(xe =>xe.Value.client != null && xe.Value.loggedIn == true && xe.Value.m_Channel == this.m_Channel && xe.Value.Map == m_Map && xe.Key != this.Name).Select(xe=>xe.Value).ToList();
                return LocalPlayers;
            }
        }

        public string Map
        {
            get { return m_Map; }
            set
            {
                BaseObject Value;

                // 맵을 옮기면 진행 중인 교환을 취소한다. (취소하면 양쪽 Exchange가 null이 되어 다시 호출되지 않는다)
                if (Exchange != null)
                    Exchange.Cancle();

                var oldMap = m_Map;
                m_Map = value;

                var filename = model.Map.FullMaps[this.m_Channel].Where(xe => xe.Key == value).FirstOrDefault().Value;

                if (filename == null)
                {
                    // 새 맵이 목록에 없으면 이전 맵을 다시 로드한다(기존 동작 유지: 바뀐 좌표를 클라이언트에 반영).
                    // setter를 다시 부르지 않는다. 이전 맵도 목록에 없을 때 무한 재귀(StackOverflow)로 서버가 즉시 종료된다.
                    m_Map = oldMap;
                    if (oldMap == null)
                        return;

                    filename = model.Map.FullMaps[this.m_Channel].Where(xe => xe.Key == oldMap).FirstOrDefault().Value;
                    if (filename == null)
                        return;
                }
                
                client.SendPacket(new PlayMusic(filename.SoundTrack).Compile());
                client.SendPacket(new LoadTile(model.Map.Mapsizes1[filename.Filename]).Compile());
                // 은신 중인 운영자에게는 맵을 밝게(안개 0) 보여 준다. 순찰용. 다른 유저의 화면에는 영향이 없다.
                client.SendPacket(new LoadWorld(this, GMHidden ? (byte)0 : filename.Dark).Compile());

                InstancedObjects.Clear();
                InstancedRangeObjects.Clear();
                Move.Clear();
                
                #region Item
                foreach (var item in LocalItems)
                {
                    if (item != null && !InstancedObjects.ContainsKey(item.m_Serial))
                    {
                        InstancedObjects.TryAdd(item.m_Serial, item);

                        if (World.Dist2d(item.m_Loc, this.Loc) <= Settings.ShowRange && !(InstancedRangeObjects.ContainsKey(item.m_Serial)))
                        {
                            InstancedRangeObjects.TryAdd(item.m_Serial, item);
                            client.SendPacket(new CreateItemGround2(item, item.m_Serial).Compile());
                        }
                    }
                }
                #endregion

                #region Monster
                foreach (var monster in LocalMonsters)
                {
                    if (monster != null && !InstancedObjects.ContainsKey(monster.m_Serial))
                    {
                        InstancedObjects.TryAdd(monster.m_Serial, monster);

                        if (monster.Alive && World.Dist2d(monster.m_Loc, this.Loc) <= Settings.ShowRange && !(InstancedRangeObjects.ContainsKey(monster.m_Serial)))
                        {
                            InstancedRangeObjects.TryAdd(monster.m_Serial, monster);
                            if (monster is script.monster.BaseNode)
                                client.SendPacket(new CreateGem(monster as script.monster.BaseNode).Compile());
                            else
                            {
                                client.SendPacket(new CreateMonster(monster, monster.m_Serial).Compile());
                                //client.SendPacket(new SetObjectEffects(monster).Compile());
                            }
                        }
                    }
                }
                #endregion
                
                #region Player
                var OldPlayers = PlayerHandler.getSingleton().add[this.m_Channel].Where(xe => xe.Value.loggedIn && xe.Value.Map == oldMap && xe.Value.Name != Name).Select(xe =>xe.Value).ToList();

                foreach (var oldplayer in OldPlayers)
                {
                    if (oldplayer == null)
                        continue;
                    //이전의 맵 플레이어가 나를 알고 있을때.
                    if (oldplayer.InstancedObjects.ContainsKey(m_Serial))
                    {
                        oldplayer.InstancedObjects.TryRemove(m_Serial, out Value);
                        oldplayer.InstancedRangeObjects.TryRemove(m_Serial, out Value);
                        oldplayer.client.SendPacket(new DeleteObject(m_Serial).Compile());                        
                    }
                }
                foreach (var player in LocalPlayers)
                {
                    if (player == null)
                        continue;

                    //상대방이 나를 모를경우
                    if (!player.InstancedObjects.ContainsKey(m_Serial))
                    {
                        player.InstancedObjects.TryAdd(m_Serial, this);
                        if (World.Dist2d(player.m_Loc, this.Loc) <= Settings.ShowRange && !(player.InstancedRangeObjects.ContainsKey(m_Serial)))
                        {
                            player.InstancedRangeObjects.TryAdd(m_Serial, this);

                            player.client.SendPacket(new CreateChar(this, m_Serial).Compile());
                            player.client.SendPacket(new SetObjectEffects(this).Compile());
                            player.client.SendPacket(new ChangeObjectSpritePlayer(this).Compile());
                        }
                    }

                    //내가 상대방을 모를경우
                    if (!InstancedObjects.ContainsKey(player.m_Serial))
                    {
                        InstancedObjects.TryAdd(player.m_Serial, player);
                        if (World.Dist2d(player.m_Loc, this.Loc) <= Settings.ShowRange && !(InstancedRangeObjects.ContainsKey(player.m_Serial)))
                        {
                            InstancedRangeObjects.TryAdd(player.m_Serial, player);

                            client.SendPacket(new CreateChar(player, player.m_Serial).Compile());
                            client.SendPacket(new SetObjectEffects(player).Compile());
                            client.SendPacket(new ChangeObjectSpritePlayer(player).Compile());
                        }
                    }

                }
                #endregion

                #region NPCs
                foreach (var npc in LocalNpcs)
                {
                    if (npc != null && !InstancedObjects.ContainsKey(npc.m_Serial))
                    {
                        InstancedObjects.TryAdd(npc.m_Serial, npc);

                        if (World.Dist2d(npc.m_Loc, this.Loc) <= Settings.ShowRange && !(InstancedRangeObjects.ContainsKey(npc.m_Serial)))
                        {
                            InstancedRangeObjects.TryAdd(npc.m_Serial, npc);
                            if (npc is script.npc.BaseInstance)
                                client.SendPacket(new CreateGem(npc).Compile());
                            else
                                client.SendPacket(new CreateNPC2(npc).Compile());
                        }
                    }
                }
                foreach (var autonpc in LocalAutoNpcs)
                {
                    if (autonpc != null && !InstancedObjects.ContainsKey(autonpc.m_Serial))
                    {
                        InstancedObjects.TryAdd(autonpc.m_Serial, autonpc);

                        if (World.Dist2d(autonpc.m_Loc, this.Loc) <= Settings.ShowRange && !(InstancedRangeObjects.ContainsKey(autonpc.m_Serial)))
                        {
                            InstancedRangeObjects.TryAdd(autonpc.m_Serial, autonpc);
                            client.SendPacket(new CreateNPC2(autonpc).Compile());
                        }
                    }
                }
                #endregion

                                
                this.client.SendPacket(new Attrib(this).Compile());
                this.client.SendPacket(new UpdateCharStats(this).Compile());
                this.client.SendPacket(new SetLevelGold(this).Compile());
            }
        }

        public Point2D Loc
        {
            get { return m_Loc; }
            set
            {
                m_Loc = value;
                var CheckStep = script.map.Portal.Portals[this.m_Channel].Where(xe => xe.Map == Map
                    && (xe.ArriveLoc.X == m_Loc.X && xe.ArriveLoc.Y == m_Loc.Y)).FirstOrDefault();

                if (CheckStep != null)
                {
                    var GotoMap = model.Map.FullMaps[this.m_Channel].Where(xe => xe.Key == CheckStep.Dest).FirstOrDefault().Value;
                    if ( !GotoMap.isOpen)
                        client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "이동할 수 없는 맵입니다.").Compile());

                    else if ( GotoMap.LimitLevel > this.Level)
                        client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "레벨이 부족합니다.").Compile());

                    else
                    {
                        m_Loc = new Point2D(CheckStep.Locs.X, CheckStep.Locs.Y);
                        World.SendToAll(new QueDele(this.m_Serial, this.m_Channel, this.m_Map, new DeleteObject(m_Serial).Compile()));
                        Map = CheckStep.Dest;
                    }
                }

                lock (Move)
                {
                    if (MacroCheacker(value) && Move.Where(xe => xe.X == value.X && xe.Y == value.Y).ToList().Count() > 3)
                    {
                        Console.WriteLine(string.Format("[매크로] {0}, {1}({2},{3})", this.Name, this.Map, this.Loc.X, this.Loc.Y));
                        loggedIn = false;
                        World.w_server.Disconnect(client.connection);
                        return;
                    }

                    Move.Add(Loc);
                    if (Move.Count() > 100)
                        Move.RemoveAt(0);
                }
                LoadInstance();
            }
        }

        public void LoadInstance()
        {
            BaseObject value;
            Parallel.ForEach(InstancedObjects, Obj =>
                {
                    if (Obj.Value.m_Channel == this.m_Channel && Obj.Value.m_Map == this.Map && World.Dist2d(Obj.Value.m_Loc, this.Loc) <= Settings.ShowRange)
                    {
                        if (InstancedRangeObjects.ContainsKey(Obj.Value.m_Serial))
                        {
                            if (Obj.Value is script.monster.Monster && (Obj.Value as script.monster.Monster).Alive == false)
                            {
                                InstancedRangeObjects.TryRemove(Obj.Value.m_Serial, out value);
                                this.client.SendPacket(new DeleteObject(Obj.Value.m_Serial).Compile());
                            }

                            return;
                        }

                        if (Obj.Value is script.item.Item)
                        {
                            InstancedRangeObjects.TryAdd(Obj.Value.m_Serial, Obj.Value);
                            client.SendPacket(new CreateItemGround2((Obj.Value as script.item.Item), (Obj.Value as script.item.Item).m_Serial).Compile());
                        }
                        else if (Obj.Value is script.monster.Monster)
                        {
                            if (!(Obj.Value as script.monster.Monster).Alive)
                            {
                                InstancedRangeObjects.TryRemove(Obj.Value.m_Serial, out value);
                                return;
                            }

                            InstancedRangeObjects.TryAdd(Obj.Value.m_Serial, Obj.Value);

                            if (Obj.Value is script.monster.BaseNode)
                                client.SendPacket(new CreateGem(Obj.Value as script.monster.BaseNode).Compile());
                            else
                            {
                                client.SendPacket(new CreateMonster((Obj.Value as script.monster.Monster), (Obj.Value as script.monster.Monster).m_Serial).Compile());
                                //client.SendPacket(new SetObjectEffects((Obj.Value as script.monster.Monster)).Compile());
                            }

                        }
                        else if (Obj.Value is Player)
                        {

                            if (!(Obj.Value as Player).loggedIn)
                                return;

                            InstancedRangeObjects.TryAdd(Obj.Value.m_Serial, Obj.Value);

                            client.SendPacket(new CreateChar((Obj.Value as Player), (Obj.Value as Player).m_Serial).Compile());
                            client.SendPacket(new SetObjectEffects((Obj.Value as Player)).Compile());
                            client.SendPacket(new ChangeObjectSpritePlayer((Obj.Value as Player)).Compile());

                            if ((Obj.Value as Player).client != null)
                            {
                                (Obj.Value as Player).InstancedRangeObjects.TryAdd(this.m_Serial, this);
                                (Obj.Value as Player).client.SendPacket(new CreateChar(this, this.m_Serial).Compile());
                                (Obj.Value as Player).client.SendPacket(new SetObjectEffects(this).Compile());
                                (Obj.Value as Player).client.SendPacket(new ChangeObjectSpritePlayer(this).Compile());
                            }
                        }
                        else if (Obj.Value is script.npc.BaseNPC)
                        {
                            if (!(Obj.Value as script.npc.BaseNPC).Alive)
                                return;

                            InstancedRangeObjects.TryAdd(Obj.Value.m_Serial, Obj.Value);

                            if (Obj.Value is script.npc.BaseInstance)
                                client.SendPacket(new CreateGem((Obj.Value as script.npc.BaseInstance)).Compile());
                            else
                                client.SendPacket(new CreateNPC2((Obj.Value as script.npc.BaseNPC)).Compile());
                        }
                    }
                    else
                    {
                        if (InstancedRangeObjects.ContainsKey(Obj.Value.m_Serial))
                        {
                            InstancedRangeObjects.TryRemove(Obj.Value.m_Serial, out value);
                            this.client.SendPacket(new DeleteObject(Obj.Value.m_Serial).Compile());

                            if (Obj.Value is Player && (Obj.Value as Player).InstancedRangeObjects.ContainsKey(this.m_Serial))
                            {
                                (Obj.Value as Player).InstancedRangeObjects.TryRemove(this.m_Serial, out value);
                                (Obj.Value as Player).client.SendPacket(new DeleteObject(this.m_Serial).Compile());
                            }
                        }
                    }
                });
        }
        public bool MacroCheacker(Point2D Loc)
        {
            if (Move.Count() < 10)
                return false;

            int last = Move.FindLastIndex(xe => xe.X == Loc.X && xe.Y == Loc.Y); // 마지막 리스트와 같은 전 리스트 <>

            if (last >= 3 &&
                Move[Move.Count()-1].X == Move[last-1].X && Move[Move.Count()-1].Y == Move[last-1].Y &&
                Move[Move.Count()-2].X == Move[last-2].X && Move[Move.Count()-2].Y == Move[last-2].Y &&
                Move[Move.Count()-3].X == Move[last-3].X && Move[Move.Count()-3].Y == Move[last-3].Y)
            {
                return true;
            }
            return false;
        }
        
        public byte[] ByteeqFlags
        {
            get
            {
                var eqArr = new byte[7];
                if ((eqFlags & SpriteEquip.Shield) != 0)
                    eqArr[0] = 1;
                if ((eqFlags & SpriteEquip.Sword) != 0)
                    eqArr[1] = 1;
                if ((eqFlags & SpriteEquip.Hammer) != 0)
                    eqArr[2] = 1;
                if ((eqFlags & SpriteEquip.Axe) != 0)
                    eqArr[3] = 1;

                return eqArr;
            }
        }

        public int AC
        {
            get
            {
                int basac = 0;//GetStat("dex") / 10;


                #region V2
                //int basac = 0;
                //basac += (int)(0.06f * GetStat("str"));
                //basac += (int)(0.06f * GetStat("men"));
                //basac += (int)(0.06f * GetStat("dex"));
                //basac += (int)(0.06f * GetStat("vit"));
                #endregion


                foreach (var eq in Equipped2.Values)
                {
                    if (eq is script.item.BaseArmor)
                    {
                        basac += (eq as script.item.BaseArmor).FullAC;
                    }
                    if (eq is script.item.BaseWeapon)
                    {
                        basac += (eq as script.item.BaseWeapon).FullAC;
                    }
                }
                foreach (var buff in m_Buffs.Values)
                {
                    if (buff.BuffEffect != null)
                    {
                        basac += buff.BuffEffect.AC + (buff.BuffEffect.ACpl * (buff.Level-1));
                        var tfloat = buff.BuffEffect.fAC + (buff.BuffEffect.fACpl * (buff.Level-1));
                        tfloat = tfloat * basac;
                        basac += (int)tfloat;
                    }
                }
                return basac;
            }
        }
        public int Hit
        {
            get
            {
                var temp = GetStat("dex") + Level;


                #region V2
                //int temp = 0;
                //temp += (int)(0.10f * GetStat("str"));

                //if ((m_Class & library.Class.Knight) != 0 && (m_Class & library.Class.Wizard) != 0)
                //    temp += (int)(1.00f * GetStat("dex"));
                //else if ((m_Class & library.Class.Swordsman) != 0 && (m_Class & library.Class.Shaman) != 0)
                //    temp += (int)(1.30f * GetStat("dex"));
                //else
                //    temp += (int)(1.00f * GetStat("dex"));
                #endregion




                foreach (var item in Equipped2.Values)
                    temp += item.HitBonus;

                foreach (var buff in m_Buffs.Values)
                {
                    if (buff.BuffEffect != null)
                    {
                        temp += buff.BuffEffect.Hit + (buff.BuffEffect.Hitpl * (buff.Level-1));
                        
                        var tfloat = buff.BuffEffect.fHit + (buff.BuffEffect.fHitpl * (buff.Level-1));
                        tfloat = tfloat * temp;
                        temp += (int)tfloat;
                    }
                }

                if (temp > ushort.MaxValue)
                    temp = ushort.MaxValue;

                return temp;
            }
        }
        public int Dam
        {
            get
            {
                int basdam = GetStat("str") / 5;

                #region V2
                //int basdam = 0;
                //if ((m_Class & library.Class.Knight) != 0)
                //    basdam += (int)(0.46f * GetStat("str"));
                //else if ((m_Class & library.Class.Swordsman) != 0 && (m_Class & library.Class.Wizard) != 0 && (m_Class & library.Class.Shaman) != 0)
                //    basdam += (int)(0.40f * GetStat("str"));
                //else
                //    basdam += (int)(0.40f * GetStat("str"));
                #endregion


                foreach (var eq in Equipped2.Values)
                {
                    if (eq is script.item.BaseWeapon)
                    {
                        basdam += (eq as script.item.BaseWeapon).FullDam;
                    }
                    if (eq is script.item.BaseArmor)
                    {
                        basdam += (eq as script.item.BaseArmor).FullDam;
                    }
                }
                foreach (var buff in m_Buffs.Values)
                {
                    if (buff.BuffEffect != null)
                    {
                        basdam += buff.BuffEffect.Dam + (buff.BuffEffect.Dampl * (buff.Level-1));
                        var tfloat = buff.BuffEffect.fDam + (buff.BuffEffect.fDampl * (buff.Level-1));
                        tfloat = tfloat * basdam;
                        basdam += (int)tfloat;
                    }
                }
                return basdam;
            }
        }

        public int HP
        {
            get
            {
                var temp = 30 + (GetStat("vit") * 3) + (Level * 4);

                #region V2
                //int temp = 0;
                //temp += (int)(0.06f * GetStat("str"));
                //temp += (int)(0.06f * GetStat("men"));
                //temp += (int)(0.06f * GetStat("dex"));

                //if ((m_Class & library.Class.Knight) != 0)
                //    temp += (int)(3.06f * GetStat("vit"));
                //else if ((m_Class & library.Class.Swordsman) != 0)
                //    temp += (int)(2.06f * GetStat("vit"));
                //else if ((m_Class & library.Class.Wizard) != 0 && (m_Class & library.Class.Shaman) != 0)
                //    temp += (int)(1.56f * GetStat("vit"));
                //else
                //    temp += (int)(1.56f * GetStat("vit"));
                #endregion

                foreach (var item in Equipped2.Values)
                    temp += item.HPBonus;

                return temp;
            }
        }
        public int MP
        {
            get
            {
                var temp = 30 + (GetStat("men") * 3) + (Level * 4);

                #region V2
                //int temp = 0;
                //temp += (int)(0.06f * GetStat("str"));

                //if ((m_Class & library.Class.Knight) != 0)
                //    temp += (int)(1.56f * GetStat("men"));
                //else if ((m_Class & library.Class.Swordsman) != 0 && (m_Class & library.Class.Shaman) != 0)
                //    temp += (int)(1.86f * GetStat("men"));
                //else if ((m_Class & library.Class.Wizard) != 0)
                //    temp += (int)(2.06f * GetStat("men"));
                //else
                //    temp += (int)(1.56f * GetStat("men"));      //평민


                //temp += (int)(0.06f * GetStat("dex"));
                //temp += (int)(0.06f * GetStat("vit"));
                #endregion

                foreach (var item in Equipped2.Values)
                    temp += item.MPBonus;

                return temp;
            }
        }

        public int MPCur
        {
            get { return m_MPCur; }
            set
            {
                if (value > MP)
                    value = MP;
                if (value != m_MPCur)
                {
                    m_MPCur = value;
                    client.SendPacket(new SetMP(this).Compile());
                }
            }
        }
        public int HPCur
        {
            get { return m_HPCur; }
            set
            {
                if (value > HP)
                    value = HP;

                if (m_Buffs.Where(xe => xe.Value.Name == "MAGIC SHIELD" || xe.Value.Name == "TEAGUE SHIELD").FirstOrDefault().Value != null
                    && value < m_HPCur)
                {
                    if (MPCur > (m_HPCur - value))
                    {
                        MPCur -= (m_HPCur - value);
                        return;
                    }
                }

                if (value <= 0)
                {

                    #region 레드 상태일 경우
                    if (PKCount >= Settings.PKRedCount)
                    {
                        script.item.Item[] eqds = new script.item.Item[Equipped2.Count];
                        Equipped2.Values.CopyTo(eqds, 0);
                        if (eqds.Length == 0)
                        {
                            foreach (var item in Inventory)
                            {
                                if (item.InvSlot > 24)
                                    continue;
                                if (item.Blessed)
                                    continue;
                                if (Util.Random(0, 100) <= 10)
                                {
                                    item.Drop(this);
                                }
                            }
                        }
                        else
                        {
                            foreach (var item in eqds)
                            {
                                if (item.Blessed)
                                    continue;

                                var chance = 0;
                                if (PKCount >= 2)
                                    chance += 20;
                                for (int x = 0; x < PKCount - 2; x++)
                                    chance += 10;

                                if (Util.Random(0, 100) <= chance)
                                {
                                    script.item.Item outi;
                                    Equipped2.TryRemove(item.m_Serial, out outi);
                                    item.Drop(this);
                                    break;
                                }
                            }
                        }
                    }
                    #endregion

                    #region PK증가 맵이 아닐 경우
                    var currMap = model.Map.FullMaps[this.m_Channel].Where(Xe => Xe.Key == this.Map).FirstOrDefault().Value;

                    //if (  !Settings.NotIncreasePKCountMap.Contains(this.Map)))
                    if (currMap.PKable != model.Map.PKType.PossiblePK_NoIncrease)
                    {
                        if (Settings.DieDropGold)
                        {
                            // 죽었을 경우 가진 돈의 일부를 바닥에 떨어트림
                            // 기존 조건(dropgold >= Gold)은 10%가 전체 이상일 수 없어 한 번도 실행되지 않았다.
                            // 비율과 상한은 Settings.DieDropGoldPercent / Settings.DieDropGoldMax
                            ulong dropgold = m_Gold / 100 * (ulong)Math.Max(0, Math.Min(100, Settings.DieDropGoldPercent));
                            if (Settings.DieDropGoldMax > 0 && dropgold > Settings.DieDropGoldMax)
                                dropgold = Settings.DieDropGoldMax;
                            if (dropgold > int.MaxValue)        // 골드 아이템 수량은 int
                                dropgold = int.MaxValue;
                            if (dropgold > 1)
                            {
                                script.item.Gold inst = new script.item.Gold((int)dropgold);
                                World.NewItems.TryAdd(inst.m_Serial, inst);     // 바닥에 보이기 전에 먼저 등록해야 바로 주울 수 있다
                                Gold -= dropgold;
                                inst.Drop(this);
                                World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new CreateItemGround2(inst, inst.m_Serial).Compile()));  // 땅에떨어진 아이템 그려줌
                            }
                        }
                        if (Settings.DieDropItem)
                        {
                            foreach (var item in this.Inventory)
                            {
                                item.Drop(this);
                            }
                        }
                    }
                    #endregion

                    client.HandleGo("휴게실", true);
                    value = 1;
                    //  World.SendToAll(new QueDele(Serial, Map, new DeleteObject(Serial).Compile()));
                }

                if (value < m_HPCur)
                {
                    byte val = 0;
                    try
                    {
                        val = Convert.ToByte(((((float)m_HPCur / (float)HP) * 100) * 1));
                    }
                    catch
                    {
                        Console.WriteLine(string.Format("HP Bug {0} {1}", m_HPCur, HP));
                    }

                    World.SendToAll(new QueDele(this.m_Channel, this.Map, new HitAnimation(m_Serial,
                        val).Compile()));
                }

                //   m_HPCur = (ushort)value > ushort.MaxValue ? ushort.MaxValue : (ushort)value;
                m_HPCur = value;
                try
                {
                    client.SendPacket(new SetHP(this).Compile());
                }
                catch
                {
                }
            }
        }

        public int XPNext { 
            get {
                if (Level == Settings.MaxLevel)
                    return 0;
                else
                    return LKCamelot.library.NextLevelC.NextLevelTable[Level]; 
            }
        }
        public int XP
        {
            get { return m_XP; }
            set
            {
                if (value != m_XP)
                    client.SendPacket(new XPChange(value).Compile());
                if (value == m_XP)
                    return;
                m_XP = value;

                if (value > XPNext && Level < Settings.MaxLevel)
                {
                    if (Level <= 98 && Promo == 0)
                    {
                        Level++;
                        Extra += (ushort)(5 + (Level / 10));
                    }
                    else if (Promo == 1 && Level < 120)
                    {
                        Level++;
                        Extra += 30;
                    }
                    else if (Promo == 2 && Level < 140)
                    {
                        Level++;
                        Extra += 50;
                    }
                    else if (Promo == 3 && Level < 160)
                    {
                        Level++;
                        Extra += 80;
                    }
                    else if (Promo == 4 && Level < 180)
                    {
                        Level++;
                        Extra += 120;
                    }
                    else if (Promo == 5 && Level < 200)
                    {
                        Level++;
                        Extra += 180;
                    }
                    else if (Promo == 6 && Level < 220)             // 220 => 6-20
                    {
                        Level++;
                        Extra += 260;
                    }
                    else if (Promo == 7 && Level < Settings.MaxLevel)
                    {
                        Level++;
                    }
                    // 이웃마을 레벨에 도달하면 자동으로 이웃마을 백성이 된다.
                    if (Level == Settings.NeighborLevel && IsNeighbor)
                    {
                        client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "이웃 나라 백성이 되었습니다.").Compile());
                        client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "도우미에게 가면 마지막왕국 백성이 될 수 있습니다.").Compile());
                    }
                    client.SendPacket(new SetLevelGold(this).Compile());
                    client.SendPacket(new UpdateCharStats(this).Compile());
                }
            }
        }

        public int GetFreeSlots()
        {
            return Math.Max(0, 24 - GetOccupiedInventorySlots().Count);
        }
        public int GetFreeSlot()
        {
            var occupied = GetOccupiedInventorySlots();
            for (int slot = 0; slot < 24; slot++)
                if (!occupied.Contains(slot))
                    return slot;
            return -1;
        }

        private HashSet<int> GetOccupiedInventorySlots()
        {
            return new HashSet<int>(World.NewItems.Values
                .Where(item => (item.Parent == this || item.ParSer == this.m_Serial) &&
                    item.InvSlot >= 0 && item.InvSlot < 24)
                .Select(item => item.InvSlot));
        }
        public int FreeBankSlot
        {
            get
            {
                var content = BankContent;
                if (content.Count == 0)
                    return 40;

                if (content.Count == 12)
                    return -1;

                int itr = 40;
                foreach (var item in content.OrderBy(xe => xe.InvSlot).ToList())
                {
                    if (item.InvSlot != itr)
                        return itr;
                    itr++;
                }

                return content.Count() + 40;
            }
        }

        public List<script.item.Item> BankContent
        {
            get
            {
                //var bank = World.NewItems.Values.Where(xe => xe.m_Parent != null
                //&& (xe.m_Parent == this || xe.ParSer == this.Serial)
                //&& xe.InvSlot >= 40 + (BankTab * 12) && xe.InvSlot < 52 + (BankTab * 12))
                //.OrderBy(xe => xe.InvSlot).ToList();
                var bank =
                    World.NewItems.Values.Where(xe => xe.m_Parent != null && (xe.m_Parent == this || xe.ParSer == this.m_Serial)
                    && xe.InvSlot >= 40 && xe.InvSlot < 52)
                    .OrderBy(xe => xe.InvSlot).ToList();
                return bank;
            }
        }

        public int GetFreeSpellSlot()
        {
            if (m_MagicLearned.Count() == 0)
                return 0;

            if (m_MagicLearned != null)
            {
                int itr = 0;
                foreach (var item in m_MagicLearned.OrderBy(xe => xe.Slot).ToList())
                {
                    if (item.Slot != itr)
                        return itr;
                    itr++;
                }
            }
            return m_MagicLearned.Count();
        }

        /// <summary>
        /// 상대로부터 데미지를 입음
        /// </summary>
        /// <param name="player">공격한 대상</param>
        /// <param name="take">대미지</param>
        public bool TakeDamage(Player player, int take)
        {
            //if (Settings.NOPK.Contains(player.Map.Split(',')[0]))
            //    return false;
            //if ((Settings.PKProtectLevel >= Level && !Settings.NotIncreasePKCountMap.Contains(player.Map)) || player.PK == false)
            //    return false;
            var currMap = model.Map.FullMaps[player.m_Channel].Where(Xe => Xe.Key == player.Map).FirstOrDefault().Value;
            if (currMap.PKable == model.Map.PKType.ImpossiblePK)
                return false;
            if ((Settings.PKProtectLevel >= Level && currMap.PKable != model.Map.PKType.PossiblePK_NoIncrease) || player.PK == false)
                return false;


            float h = ((float)player.Hit / ((float)player.Hit + (float)Hit)) * 200;

            if (h >= 100 || new Random().Next(0, 100) < (int)h)
            {
                var swingbuff = player.m_Buffs.Where(xe => xe.Value.BuffEffect.BuffType == script.spells.BuffCase.Triple || xe.Value.BuffEffect.BuffType == script.spells.BuffCase.Twister).FirstOrDefault().Value;
                if (swingbuff != null)
                {
                    var ttt = swingbuff.Level * 0.05;
                    take = (int)(take * (ttt + 0.40d));
                }
                HPCur -= take;

                if (Settings.RestMap.Contains(this.Map) && this.PKCount < Settings.PKRedCount)
                {
                    //if (Settings.NotIncreasePKCountMap.Contains(player.Map.Split(',')[0]))
                    //    return false;
                    
                    if (currMap.PKable == model.Map.PKType.PossiblePK_NoIncrease)
                        return false;

                    if (Settings.PKAlarm)
                        World.SendToAll(new QueDele("all", new UpdateChatBox(0x25, 0x65, 0, string.Format("{0}님(PK:{1})이 {2}님을 죽임. 맵:{3}({4},{5})", player.Name, player.PKCount, this.Name, player.Map.Split(',')[0], player.Loc.X, player.Loc.Y)).Compile()));

                    if (this.PKCount < Settings.PKLevelRegester)
                    {
                        this.PKhistoryPlayer.TryAdd(Server.tickcount.ElapsedMilliseconds, player);
                        this.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}님에게 죽임을 당했습니다.", player.Name)).Compile());
                        this.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("\'@신고\'를 통해서 신고할 수 있습니다.")).Compile());
                    }
                }

                World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new HitAnimation(m_Serial, Convert.ToByte(((((float)HPCur / (float)HP) * 100) * 1))).Compile()));
                return true;
            }
            return false;
        }

        /// <summary>
        /// 이 유저가 caster의 공격 마법을 맞을 수 있는 상태인지 확인한다.
        /// 아래 TakeDamage(Player, Spell)의 조건과 같다: 같은 맵, PK 가능 맵, 시전자 대인공격 ON, 보호 레벨 초과.
        /// </summary>
        public bool CanTakeSpellFrom(Player caster)
        {
            if (caster == null || caster == this || !caster.loggedIn || !caster.PK)
                return false;
            if (caster.m_Channel != this.m_Channel || caster.Map == null || caster.Map != this.Map)
                return false;

            model.Map.MapAttribute currMap;
            if (!model.Map.FullMaps[caster.m_Channel].TryGetValue(caster.Map, out currMap) || currMap == null)
                return false;
            if (currMap.PKable == model.Map.PKType.ImpossiblePK)
                return false;
            if (Settings.PKProtectLevel >= Level && currMap.PKable != model.Map.PKType.PossiblePK_NoIncrease)
                return false;
            return true;
        }

        // 운영자 #은신 상태. 완전 투명(100)이며 몬스터가 대상으로 잡지 않고 샤프아이로 벗겨지지 않는다.
        public bool GMHidden;
        // 은신 마법(TRANSPARENCY)으로 얻은 현재 투명도(%). 샤프아이를 맞으면 줄어든다.
        public byte SpellHide;

        /// <summary>
        /// 투명도(Transparancy)를 현재 상태에 맞게 다시 계산한다.
        /// 운영자 은신이면 100, 은신 마법이 걸려 있으면 SpellHide, 둘 다 아니면 0.
        /// </summary>
        public void RefreshTransparency()
        {
            if (GMHidden)
                Transparancy = 100;
            else if (m_Buffs.Values.Any(xe => xe is script.spells.Transparency))
                Transparancy = SpellHide;
            else
            {
                SpellHide = 0;
                Transparancy = 0;
            }
        }

        /// <summary>
        /// 이 유저의 표시 상태(투명도, 빛, 버프 효과)를 주변과 본인에게 보낸다.
        /// 완전 투명일 때 주변에는 빛 반경을 0으로 보내 어두운 맵에서 위치가 드러나지 않게 하고,
        /// 본인에게는 원래 빛 반경을 보내 자기 화면은 그대로 보이게 한다.
        /// </summary>
        public void SendEffects()
        {
            World.SendToAll(new QueDele(m_Serial, m_Channel, m_Map, new SetObjectEffects(this).Compile()));
            if (client != null)
                client.SendPacket(new SetObjectEffects(this, true).Compile());
        }

        /// <summary>
        /// 지속 피해 마법의 한 주기 피해를 받는다. 시전자의 현재 상태(접속, 맵, 대인공격)는 보지 않는다.
        /// 이 피해로 죽었으면 true. (죽으면 HPCur 처리에서 휴게실로 이동하므로 맵이 바뀐 것으로 판단한다)
        /// </summary>
        public bool TakeDotDamage(script.spells.Spell spell)
        {
            int take = spell.DamBase + (spell.DamPl * spell.Level - 1) - this.AC;
            if (take <= 0)
                return false;

            string mapBefore = this.Map;
            this.HPCur -= take;
            World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new HitAnimation(m_Serial, Convert.ToByte(((((float)HPCur / (float)HP) * 100) * 1))).Compile()));
            return this.Map != mapBefore;
        }

        /// <summary>
        /// 사용자로부터 마법 경격을 당함
        /// </summary>
        /// <param name="caster">마법 시전자</param>
        /// <param name="spell">마법</param>
        public void TakeDamage(Player player, script.spells.Spell spell)
        {
            //if (Settings.NOPK.Contains(player.Map.Split(',')[0]))
            //    return;
            //if ( (Settings.PKProtectLevel >= Level && !Settings.NotIncreasePKCountMap.Contains(player.Map)) || player.PK == false)
            //    return;
            var currMap = model.Map.FullMaps[player.m_Channel].Where(Xe => Xe.Key == player.Map).FirstOrDefault().Value;
            if (currMap.PKable == model.Map.PKType.ImpossiblePK)
                return;
            if ((Settings.PKProtectLevel >= Level && currMap.PKable != model.Map.PKType.PossiblePK_NoIncrease) || player.PK == false)
                return;

            float h = ((float)player.Hit / ((float)player.Hit + (float)this.Hit)) * 200;

            if (h >= 100 || new Random().Next(0, 100) < (int)h)
            {
                int take = spell.DamBase + (spell.DamPl * spell.Level-1);
                if (spell.ManaCostPl != 0)
                {
                    take += (player.GetStat("men") / spell.menCoff);
                    take += (player.GetStat("str") / spell.strCoff);
                    take += (player.GetStat("dex") / spell.dexCoff);
                }
                if (spell is script.spells.DemonDeath)
                    take += spell.DamBase + (spell.DamPl * spell.Level) + Convert.ToInt32((player.m_HPCur + (player.HP * 0.30)) * 0.5) + player.GetStat("dex");

                // 방어력이 피해보다 높으면 0으로 처리한다. (보정이 없으면 피해가 음수가 되어 맞은 쪽이 회복된다)
                take -= this.AC;
                if (take < 0)
                    take = 0;
                this.HPCur -= take;
                if (Settings.RestMap.Contains(this.Map) && this.PKCount < Settings.PKRedCount)
                {
                    //if (Settings.NotIncreasePKCountMap.Contains(player.Map.Split(',')[0]))
                    //    return;
                    if (currMap.PKable == model.Map.PKType.PossiblePK_NoIncrease)
                        return;

                    if (Settings.PKAlarm)
                        World.SendToAll(new QueDele("all", new UpdateChatBox(0x25, 0x65, 0, string.Format("{0}님(PK:{1})이 {2}님을 죽임. 맵:{3}({4},{5})", player.Name, player.PKCount, this.Name, player.Map.Split(',')[0], player.Loc.X, player.Loc.Y)).Compile()));

                    if (this.PKCount < Settings.PKLevelRegester)
                    {
                        this.PKhistoryPlayer.TryAdd(Server.tickcount.ElapsedMilliseconds, player);
                        this.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}님에게 죽임을 당했습니다.", player.Name)).Compile());
                        this.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("\'@신고\'를 통해서 신고할 수 있습니다.")).Compile());
                    }
                }

                World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new HitAnimation(m_Serial, Convert.ToByte(((((float)HPCur / (float)HP) * 100) * 1))).Compile()));
            }
        }
        public void TakeDamage(Player player, script.spells.Spell spell, int castx, int casty)
        {
            //if (Settings.NOPK.Contains(player.Map.Split(',')[0]))
            //    return;
            //if ((Settings.PKProtectLevel >= Level && !Settings.NotIncreasePKCountMap.Contains(player.Map)) || player.PK == false)
            //    return;
            var currMap = model.Map.FullMaps[player.m_Channel].Where(Xe => Xe.Key == player.Map).FirstOrDefault().Value;
            if (currMap.PKable == model.Map.PKType.ImpossiblePK)
                return;
            if ((Settings.PKProtectLevel >= Level && currMap.PKable != model.Map.PKType.PossiblePK_NoIncrease) || player.PK == false)
                return;

            float h = ((float)player.Hit / ((float)player.Hit + (float)this.Hit)) * 200;

            if (h >= 100 || new Random().Next(0, 100) < (int)h)
            {
                int take = spell.DamBase + (spell.DamPl * spell.Level - 1);
                if (spell.ManaCostPl != 0)
                {
                    take += (player.GetStat("men") / spell.menCoff);
                    take += (player.GetStat("str") / spell.strCoff);
                    take += (player.GetStat("dex") / spell.dexCoff);
                }
                if (spell is script.spells.DemonDeath)
                    take += spell.DamBase + (spell.DamPl * spell.Level) + Convert.ToInt32((player.m_HPCur + (player.HP * 0.30)) * 0.5) + player.GetStat("dex");

                this.HPCur -= (take - this.AC);
                if (Settings.RestMap.Contains(this.Map) && this.PKCount < Settings.PKRedCount)
                {
                    //if (Settings.NotIncreasePKCountMap.Contains(player.Map.Split(',')[0]))
                    //    return;
                    if (currMap.PKable == model.Map.PKType.PossiblePK_NoIncrease)
                        return;


                    if (Settings.PKAlarm)
                        World.SendToAll(new QueDele("all", new UpdateChatBox(0x25, 0x65, 0, string.Format("{0}님(PK:{1})이 {2}님을 죽임. 맵:{3}({4},{5})", player.Name, player.PKCount, this.Name, player.Map.Split(',')[0], player.Loc.X, player.Loc.Y)).Compile()));

                    if (this.PKCount < Settings.PKLevelRegester)
                    {
                        this.PKhistoryPlayer.TryAdd(Server.tickcount.ElapsedMilliseconds, player);
                        this.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0}님에게 죽임을 당했습니다.", player.Name)).Compile());
                        this.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("\'@신고\'를 통해서 신고할 수 있습니다.")).Compile());
                    }
                }

                World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new HitAnimation(m_Serial, Convert.ToByte(((((float)HPCur / (float)HP) * 100) * 1))).Compile()));
            }
        }

        public Player(Serial serial)
            : base(serial)
        {

        }
        public Player(LKCamelot.io.IOClient iocli, string Name)
            : this (Serial.NewMobile)
        {
            this.client = iocli;
            CreateBeginner(Name);
        }
        public Player(LKCamelot.io.IOClient iocli, Serial serial, string[] line)
            : this(serial, line)
        {
            this.client = iocli;
        }

        public Player(Serial serial, string[] line) : this(serial)
        {
            Load(line);
        }

        public void Load(string[] line)
        {
            m_Channel = Util.Random(Settings.VillageName.Count);
            Guild = World.NewGuilds.Values.Where(xe => xe.Member.Keys.Contains(this.m_Serial)).FirstOrDefault();

            if (line.Count() == 38)
            {
                Name = line[1];
                Pass = line[2];
                m_Class = (LKCamelot.library.Class)Convert.ToInt32(line[3]);
                Stage = (byte)Convert.ToInt32(line[4]);
                pkhistory = Convert.ToInt64(line[5]);
                neighbor = true;
                m_Map = line[7];
                m_Loc = new model.Point2D(Convert.ToInt32(line[8]), Convert.ToInt32(line[9]));
                Face = (byte)Convert.ToInt32(line[10]);

                m_Str = Settings.GMS.Contains(Name) ? ushort.MaxValue : (ushort)Convert.ToInt32(line[11]);
                m_Men = Settings.GMS.Contains(Name) ? ushort.MaxValue : (ushort)Convert.ToInt32(line[12]);
                m_Dex = Settings.GMS.Contains(Name) ? ushort.MaxValue : (ushort)Convert.ToInt32(line[13]);
                m_Vit = Settings.GMS.Contains(Name) ? ushort.MaxValue : (ushort)Convert.ToInt32(line[14]);
                m_Extra = Convert.ToUInt32(line[15]);

                b_Str = Convert.ToUInt16(line[16]);
                b_Men = Convert.ToUInt16(line[17]);
                b_Dex = Convert.ToUInt16(line[18]);
                b_Vit = Convert.ToUInt16(line[19]);

                m_HP = (ushort)Convert.ToInt32(line[20]);
                m_HPCur = (ushort)Convert.ToInt32(line[21]);
                m_MP = (ushort)Convert.ToInt32(line[22]);
                m_MPCur = Convert.ToInt32(line[23]);

                m_Level = (short)Convert.ToInt32(line[24]);
                m_XP = Convert.ToInt32(line[25]);
                MinerLevel = Convert.ToInt32(line[26]);
                _MinerXP = Convert.ToInt32(line[27]);
                BSLevel = Convert.ToInt32(line[28]);
                _BSXP = Convert.ToInt32(line[29]);
                _HelperLevel = Convert.ToInt32(line[30]);

                m_Gold = Convert.ToUInt64(line[31]);
                BankGold = Convert.ToInt64(line[32]);

                LoadMagic(line[33]);
                DateTime.TryParse(line[34], out CreateTime);
                DateTime.TryParse(line[35], out LastAccessTime);
                IP = line[36];
                Ben = Convert.ToBoolean(line[37]);
            }
            else
            {
                Name = line[1];
                Pass = line[2];
                m_Class = (LKCamelot.library.Class)Convert.ToInt32(line[3]);
                Stage = (byte)Convert.ToInt32(line[4]);
                pkhistory = Convert.ToInt64(line[5]);
                BenNeighbor = Convert.ToBoolean(line[6]);
                neighbor = Convert.ToBoolean(line[7]);
                m_Map = line[8];
                m_Loc = new model.Point2D(Convert.ToInt32(line[9]), Convert.ToInt32(line[10]));
                Face = (byte)Convert.ToInt32(line[11]);

                m_Str = Settings.GMS.Contains(Name) ? ushort.MaxValue : (ushort)Convert.ToInt32(line[12]);
                m_Men = Settings.GMS.Contains(Name) ? ushort.MaxValue : (ushort)Convert.ToInt32(line[13]);
                m_Dex = Settings.GMS.Contains(Name) ? ushort.MaxValue : (ushort)Convert.ToInt32(line[14]);
                m_Vit = Settings.GMS.Contains(Name) ? ushort.MaxValue : (ushort)Convert.ToInt32(line[15]);
                m_Extra = Convert.ToUInt32(line[16]);

                b_Str = Convert.ToUInt16(line[17]);
                b_Men = Convert.ToUInt16(line[18]);
                b_Dex = Convert.ToUInt16(line[19]);
                b_Vit = Convert.ToUInt16(line[20]);

                m_HP = (ushort)Convert.ToInt32(line[21]);
                m_HPCur = (ushort)Convert.ToInt32(line[22]);
                m_MP = (ushort)Convert.ToInt32(line[23]);
                m_MPCur = Convert.ToInt32(line[24]);

                m_Level = (short)Convert.ToInt32(line[25]);
                m_XP = Convert.ToInt32(line[26]);
                MinerLevel = Convert.ToInt32(line[27]);
                _MinerXP = Convert.ToInt32(line[28]);
                BSLevel = Convert.ToInt32(line[29]);
                _BSXP = Convert.ToInt32(line[30]);
                _HelperLevel = Convert.ToInt32(line[31]);

                m_Gold = Convert.ToUInt64(line[32]);
                BankGold = Convert.ToInt64(line[33]);

                LoadMagic(line[34]);
                DateTime.TryParse(line[35], out CreateTime);
                DateTime.TryParse(line[36], out LastAccessTime);
                IP = line[37];
                Ben = Convert.ToBoolean(line[38]);

                // 40번째 컬럼(미분배 턴)은 나중에 추가됐다. 없는 줄(기존 39컬럼 파일)은 0으로 읽는다.
                b_Extra = line.Count() >= 40 ? Convert.ToUInt32(line[39]) : 0;
            }
        }

        public void CreateBeginner(string Name)
        {
            this.m_Channel = Util.Random(Settings.VillageName.Count);
            this.CreateTime = DateTime.Now;
            this.Name = Name.Split(',')[0];
            this.Pass = Name.Split(',')[1];
            this.m_Class = Class.Beginner;
            this.m_Str = 0;
            this.m_Men = 0;
            this.m_Dex = 0;
            this.m_Vit = 0;
            this.m_Extra = 0;
            /*
            this.m_Str = 55550;
            this.m_Men = 55550;
            this.m_Dex = 55550;
            this.m_Vit = 55550;
            */
            this.HPCur = HP;
            this.MPCur = MP;
            this.Face = 4;

            //    this.m_Promo = 0;
            this.m_Level = 1;
            this.m_XP = 0;
            this.m_Gold = 0;
            this.AttackSpeed = 450;
            this.CastSpeed = 1500;

            new script.item.Knife().Inventory(this);

            /*      var newitem1 = new script.item.Dai().Inventory(this);
                  World.NewItems.TryAdd(newitem1.m_Serial, newitem1);
                  var newitem2 = new script.item.Dai().Inventory(this);
                  World.NewItems.TryAdd(newitem2.m_Serial, newitem2);
                  var newitem3 = new script.item.Dai().Inventory(this);
                  World.NewItems.TryAdd(newitem3.m_Serial, newitem3);
                  var newitem4 = new script.item.Dai().Inventory(this);
                  World.NewItems.TryAdd(newitem4.m_Serial, newitem4);*/


        }

        public long BankGold { get; set; }
        public int MinerLevel
        {
            get;
            set;
        }
        public int _MinerXP;
        public int MinerXP
        {
            get { return _MinerXP; }
            set
            {
                if (_MinerXP == value) return;
                if (_MinerXP < 0)
                {
                    _MinerXP = 0;
                    return;
                }
                _MinerXP = value;

                if (MinerLevel < 99 && _MinerXP >= library.NextLevelC.NextLevelTable[MinerLevel])
                    MinerLevel++;
                if (MinerLevel > 1 && _MinerXP <= library.NextLevelC.NextLevelTable[MinerLevel-1])
                    MinerLevel--;
                client.SendPacket(new Attrib(this).Compile());
                
            }
        }
        public int BSLevel { get; set; }
        public int _BSXP;
        public int BSXP
        {
            get { return _BSXP; }
            set
            {
                if (_BSXP == value || BSLevel >= 99) return;
                _BSXP = value;

                if (BSLevel < 99 && _BSXP >= library.NextLevelC.NextLevelTable[BSLevel])
                    BSLevel++;
                if (BSLevel > 1 && _BSXP <= library.NextLevelC.NextLevelTable[BSLevel -1])
                    BSLevel--;
                client.SendPacket(new Attrib(this).Compile());
            }
        }

        public int _HelperLevel;
        public int HelperLevel
        {
            get { return _HelperLevel; }
            set
            {
                _HelperLevel = value;
                client.SendPacket(new Attrib(this).Compile());
            }
        }
        public int PKCount
        {
            get { return (int)Math.Ceiling((double)this.pkhistoryTime / Settings.PKDelay); }
        }




        /// <summary>
        /// player가 this에게 아이템을 건내준다.
        /// </summary>
        /// <param name="player">건내는 플레이어</param>
        /// <param name="giveItem">건내는 아이템</param>
        /// <param name="amount">수량</param>
        public bool Event_Give(Player player, script.item.Item giveItem, int amount)
        {
            int slot = this.GetFreeSlot();
            if (slot == -1)
                return false;

            //겹치는 아이템 처리
            var item = this.Inventory.Where(xe => xe.Name == giveItem.Name && xe.InvSlot< 24 && xe.Stage == giveItem.Stage).FirstOrDefault();

            if (item != null && giveItem.Stackable)            //아이템 있음
            {
                if (item.Quantity + amount > item.Limit)
                {
                    int Amount = item.Limit - item.Quantity;
                    item.Quantity += Amount;
                    giveItem.Quantity -= Amount;

                    if (giveItem.Parent != null)
                        giveItem.Parent.client.SendPacket(new AddItemToInventory2(giveItem).Compile());
                    this.client.SendPacket(new AddItemToInventory2(item).Compile());

                    return true;
                }
                else
                {
                    if (giveItem.Quantity <= amount)
                        giveItem.Delete(player);
                    else 
                        giveItem.Quantity -= amount;
                    item.Quantity += amount;

                    if (giveItem.Parent != null)
                        giveItem.Parent.client.SendPacket(new AddItemToInventory2(giveItem).Compile());
                    this.client.SendPacket(new AddItemToInventory2(item).Compile());

                    return true;
                }
            }
            //아이템 없음
            else
            {
                if (giveItem.Quantity <= amount)            //가지고 있는 모든 템을 줌
                {
                    giveItem.m_Parent.client.SendPacket(new DeleteItemSlot((byte)giveItem.m_InvSlot).Compile());

                    giveItem.Parent = this;
                    giveItem.m_InvSlot = slot;
                    this.client.SendPacket(new AddItemToInventory2(giveItem).Compile());

                    return true;
                }
                else
                {
                    giveItem.Quantity -= amount;
                    giveItem.m_Parent.client.SendPacket(new AddItemToInventory2(giveItem).Compile());

                    script.item.Item Cloneitem = (script.item.Item)giveItem.Clone();
                    Cloneitem.Quantity = amount;
                    Cloneitem.Parent = this;
                    Cloneitem.InvSlot = slot;
                    Cloneitem.m_Parent.client.SendPacket(new AddItemToInventory2(Cloneitem).Compile());

                    return true;
                }
            }
        }
        public bool Event_DragDrop(Player player, script.item.Item itemdragdrop)
        {
            player.client.SendPacket(new DeleteItemSlot((byte)itemdragdrop.InvSlot).Compile());


            itemdragdrop.Parent = this;
            itemdragdrop.InvSlot = GetFreeSlot();
            this.client.SendPacket(new AddItemToInventory2(itemdragdrop).Compile());

            return true;
        }
        public void Event_Chat(string str)
        {
            StringBuilder message = new StringBuilder(Name + ": " + str);
            if (str != "PICK UP!!!" && !System.Text.RegularExpressions.Regex.IsMatch(str, @"^(작은 ?)?(마력|체력)시약 \d{1,2} 산다$"))
                Server.mainForm.EnqueueBubbleChat(message.ToString());
            World.SendToRange(new QueDele(this.m_Channel, this.m_Map, new UpdateCharChatBox(m_Serial, message.ToString()).Compile()), this);
        }

        public void Clear()
        {
            Move.Clear();
            InstancedObjects.Clear();
            InstancedRangeObjects.Clear();
            m_Buffs.Clear();
            RefreshTransparency();      // 은신 마법이 지워졌으므로 투명도를 되돌린다 (운영자 은신은 유지)
            CombackLoc = null;
            CombackMap = string.Empty;
            GuildJoinFlag = false;
            WhispBlock = false;
            SoundFlag = false;
            PK = false;
            ChatBlack = false;
        }
    }
}
