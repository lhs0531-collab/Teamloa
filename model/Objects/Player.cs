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
    public class ExchangeBox
    {
        private Player Owner;
        private Player Target;
        public List<script.item.Item> CellItem = new List<script.item.Item>();
        private int Gold = 0;
        private bool Check = false;


        public ExchangeBox(Player player, Player target)
        {
            Owner = player;
            Owner.Exchange = this;

            Target = target;
            if (Target.Exchange == null)
                Target.Exchange = new ExchangeBox(target, player);

            player.client.SendPacket(new ShowExchangeBox(player, target).Compile());
            player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "서비스 준비 중입니다.").Compile());
        }

        public void AddGold(int Gold)
        {
            if (Check || Target.Exchange.Check)
            {
                Owner.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "체크 상태입니다.").Compile());
                return;
            }



        }
        public void AddItem(int slot, int amount)
        {
            if (Check || Target.Exchange.Check)
            {
                Owner.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "체크 상태입니다.").Compile());
                return;
            }
            var item = Owner.Inventory.Where(xe => xe.InvSlot == slot).FirstOrDefault();

            if (item != null)
            {
                CellItem.Add(item);
            }
            Refresh();
        }

        public int GetFreeSlot()
        {
            return (4 - CellItem.Count());
        }
        public int Price
        {
            get
            {
                return Gold;
            }

            set
            {
                checked
                {

                }
            }
        }
        public bool isCheck
        {
            get
            {
                return Check;
            }
            set
            {
                if (this.Check == value)
                    return;

                this.Check = value;
                Refresh();

                if (Check && Target.Exchange.isCheck)
                {
                    if (Owner.m_Channel == Target.m_Channel &&
                        Owner.Map == Target.Map &&
                        Owner.loggedIn && Target.loggedIn &&
                        World.Dist2d(Owner.Loc, Target.Loc) < 10
                        )
                    {
                        Confirm();
                    }
                    else
                        Cancle();
                }
            }
        }
        public bool ItemCheck()
        {
            foreach (var item in CellItem)
            {
                if (!Owner.Inventory.Where(xe => xe.m_Serial == item.m_Serial && xe.InvSlot < 24).Any())
                    return false;
            }
            return true;
        }
        public void Cancle()
        {
            Owner.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "취소되었습니다.").Compile());
            Target.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "취소되었습니다.").Compile());
            Close();
        }

        private void Confirm()
        {
            if (Owner.Exchange.ItemCheck() && Target.Exchange.ItemCheck())
            {
                Owner.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "교환 성공하였습니다.").Compile());
                Target.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "교환 성공하였습니다.").Compile());

                Close();
            }
            else
                Cancle();
        }
        private void Close()
        {
            Owner.client.SendPacket(new CanselExchangeBox().Compile());
            Target.client.SendPacket(new CanselExchangeBox().Compile());

            Owner.Exchange = null;
            Target.Exchange = null;
        }
        private void Refresh()
        {
            Owner.client.SendPacket(new ShowExchangeBox(Owner, Target).Compile());
            Target.client.SendPacket(new ShowExchangeBox(Target, Owner).Compile());
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


        public List<string> WhispBlockList = new List<string>();


        public LKCamelot.io.IOClient client;
        public string Name, Pass;
        public Class m_Class;
        public byte Stage;
        public ushort m_Str, m_Men, m_Dex, m_Vit, m_AC, m_Hit, m_Dam;
        public ushort b_Str, b_Men, b_Dex, b_Vit;
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
                return b_Str + b_Men + b_Dex + b_Vit;
            }
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

                if (bas == false && (this.IPNeighbor || this.BenNeighbor || (Settings.Neighbor && Level >= Settings.NeighborLevel && neighbor == true)))
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

                if (bas == false && (this.IPNeighbor || this.BenNeighbor || (Settings.Neighbor && Level >= Settings.NeighborLevel && neighbor == true)))
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

                if (bas == false && (this.IPNeighbor || this.BenNeighbor || (Settings.Neighbor && Level >= Settings.NeighborLevel && neighbor == true)))
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

                if (bas == false && (this.IPNeighbor || this.BenNeighbor || (Settings.Neighbor && Level >= Settings.NeighborLevel && neighbor == true)))
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
                client.SendPacket(new LoadWorld(this, filename.Dark).Compile());

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
                            // 죽었을 경우 돈을 10%의 돈을 바닥에 떨어트림
                            // 기존 조건(dropgold >= Gold)은 10%가 전체 이상일 수 없어 한 번도 실행되지 않았다.
                            ulong dropgold = m_Gold / 10;
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

                    client.HandleGo("휴게실");
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
