using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
using LKCamelot.library;

namespace LKCamelot.script.item
{
    public enum ItemLoc
    {
        Ground = 0,
        Inventory = 1,
        Equipped = 2,
        Shop = 3
    }
    /*
     * 인벤 0~23
     * 장비 25 ~ 30
     * 보관 40~51
     */
    public class Item : BaseObject
    {
        public int m_ItemID;
        public int m_InvSlot = -1;
        public Player m_Parent;
        public int m_Quantity = 1;
        protected int m_Stage;
        public int ParSer;
        public int Durability = 1;

        public virtual int Stage { get { return m_Stage; } set { m_Stage = value; } }
        public virtual short Limit { get { return 100; } }                           // 최대 갯수

        public virtual float ValidTime { get { return 10 * Settings.Minute; } }     // 바닥템 유효 시간
        public virtual int InitMinHits { get { return 10; } }
        public virtual int InitMaxHits { get { return 10; } }
        public virtual bool IsBreak { get { return false; } }                    // 깨짐 여부
        public virtual bool Stackable { get { return false; } }                  // 겹침 여부


        public virtual ulong BuyPrice { get { return 100000; } }
        public virtual int SellPrice { get { return (int)(BuyPrice * 0.70); } }



        public virtual bool Blessed { get { return false; } }
        public virtual string Name { get { return ""; } }
        public virtual string FlavorText { get { return null; } }
        public virtual string DescText { get { return null; } }
        //   public virtual string ParsedStats { get { return ""; } }
        public virtual int StrReq { get { return 0; } }
        public virtual int MenReq { get { return 0; } }
        public virtual int DexReq { get { return 0; } }
        public virtual int VitReq { get { return 0; } }
        public virtual LKCamelot.library.Class ClassReq { get { return 0; } }
        public virtual int LevelReq { get { return 0; } }
        public virtual int ReduceCast { get { return 0; } }
        public virtual int ReduceSwing { get { return 0; } }

        public virtual int DamBase { get { return 0; } }
        public virtual int ACBase { get { return 0; } }
        public virtual int Level { get { return 0; } }
        public virtual int HPBonus { get { return 0; } }
        public virtual int MPBonus { get { return 0; } }
        public virtual int HitBonus { get { return 0; } }

        public virtual int StrBonus { get { return 0; } }
        public virtual int MenBonus { get { return 0; } }
        public virtual int DexBonus { get { return 0; } }
        public virtual int VitBonus { get { return 0; } }

        public long DropTime = 0;

        public Item(int itemID) : this()
        {
            m_ItemID = itemID;
        }
        public Item(Serial serial) : base(serial)
        {
        }
        public Item() : base(Serial.NewItem)
        {
            Durability = this.InitMaxHits;
        }
        public int Quantity
        {
            get { return m_Quantity; }
            set
            {
                m_Quantity = value;
                if (this is script.item.Gold)
                {
                    if (value < 1000)
                        m_ItemID = 40;
                    else if (value < 100000)
                        m_ItemID = 41;
                    else if (value < 1000000)
                        m_ItemID = 42;
                    else
                        m_ItemID = 43;
                }    
            }
        }
        public byte Stress
        {
            get
            {
                if (!IsBreak)
                    return 0;
                float num = this.InitMaxHits;

                if (this.InitMaxHits != 0)
                    num = Durability / this.InitMaxHits;
                
                if (num < 0.05)
                    return 3;
                else if (num < 0.10)
                    return 2;
                else if (num < 0.50)
                    return 1;
                else
                    return 0;
            }
        }
        public int FullLevel
        {
            get
            {
                int level = this.LevelReq;

                if (this is item.BaseAccessory)
                    return level;

                if (this.Stage > 1)
                    level += (this.Stage * 5);

                return level;
            }
        }
        public int FullDam
        {
            get
            {
                var temp = DamBase + (DamBase * (Stage * 0.1));
                
                //var temp = DamBase;
                //for (int x = 0; x < Stage; x++)
                //{
                //    temp += (int)(temp * 0.1) + 1;
                //}
                return (int) temp;
            }
        }

        public int FullAC
        {
            get
            {
                var temp = ACBase + ( ACBase * (Stage * 0.1));
                
                //var temp = ACBase;
                //for (int x = 0; x < Stage; x++)
                //{
                //    temp += (int)(temp * 0.1) + 1;
                //}
                return (int) temp;
            }
        }

        public string SQLSaveString()
        {
            string temp = "";
            temp += "\'" + Convert.ToInt32(m_Serial) + "\', ";
            temp += "\'" + this.ToString() + "\', ";
            temp += "\'" + m_ItemID + "\', ";
            temp += "\'" + Convert.ToInt32(ParSer) + "\', ";
            temp += "\'" + InvSlot + "\', ";
            temp += "\'" + Stage + "\', ";
            temp += "\'" + Quantity + "\', ";
            temp = temp.Substring(0, (temp.Count() - 2));
            return temp;
        }

        public string ClassReqString
        {
            get
            {
                string ret = "";
                if ((ClassReq & Class.Beginner) != 0)
                    ret += "평민 -";
                if ((ClassReq & Class.Knight) != 0)
                    ret += "기사 - ";
                if ((ClassReq & Class.Swordsman) != 0)
                    ret += "검객 - ";
                if ((ClassReq & Class.Wizard) != 0)
                    ret += "위저드 - ";
                if ((ClassReq & Class.Shaman) != 0)
                    ret += "샤먼 - ";
                if (ret != "")
                    ret = ret.Substring(0, ret.Count() - 3);
                return ret;
            }
        }

        public virtual string ParsedStats
        {
            get
            {
                int strr = StrReq;
                int menr = MenReq;
                int dexr = DexReq;
                int lvlr = FullLevel;

                if (this is BaseSpellbook && m_Parent != null)
                {
                    var spelta = m_Parent.MagicLearned.Where(xe => xe.Name == (this as BaseSpellbook).SpellTaught.Name).FirstOrDefault();
                    if (spelta != null)
                    {
                        strr += (this as BaseSpellbook).StrReqPl * m_Parent.MagicLearned[m_Parent.MagicLearned.IndexOf(spelta)].m_Level;
                        menr += (this as BaseSpellbook).MenReqPl * m_Parent.MagicLearned[m_Parent.MagicLearned.IndexOf(spelta)].m_Level;
                        dexr += (this as BaseSpellbook).DexReqPl * m_Parent.MagicLearned[m_Parent.MagicLearned.IndexOf(spelta)].m_Level;
                        lvlr += (this as BaseSpellbook).LevelReqPl * m_Parent.MagicLearned[m_Parent.MagicLearned.IndexOf(spelta)].m_Level;
                    }
                }

                string ret = "";
                ret += NPrefix() + " " + Name + " : " + Quantity + "\n\t";
                if (ClassReq != 0)
                    ret += "필요직업: " + ClassReqString + "\n\t";
                if (FullLevel != 0)
                    ret += "필요레벨: " + lvlr + "\n\t";
                if (StrReq != 0)
                    ret += "필요힘: " + strr + "\n\t";
                if (MenReq != 0)
                    ret += "필요지력: " + menr + "\n\t";
                if (DexReq != 0)
                    ret += "필요숙련: " + dexr + "\n\t";
                if (this is BaseWeapon)
                {
                    if ( (this as BaseWeapon).WeaponType == WeaponType.Axe ||
                        (this as BaseWeapon).WeaponType == WeaponType.Hammer ||
                        (this as BaseWeapon).WeaponType == WeaponType.Sword)
                        ret += "종류: 한손무기\n\t";
                    else
                        ret += "종류: 양손무기\n\t";

                }

                if (DamBase != 0)
                    ret += "Damage: " + FullDam + "\n\t";
                if (ACBase != 0)
                    ret += "AC: " + this.FullAC + "\n\t";
                if (HitBonus != 0)
                    ret += "Hit: " + this.HitBonus + "\n\t";
                if (HPBonus != 0)
                    ret += "HP: " + this.HPBonus + "\n\t";
                if (MPBonus != 0)
                    ret += "MP: " + this.MPBonus + "\n\t";
                if (ReduceCast != 0)
                    ret += "Cast Speed: " + (float)(ReduceCast * 0.01) + "\n\t";
                if (ReduceSwing != 0)
                    ret += "Swing Speed: " + (float)(ReduceSwing * 0.01) + "\n\t";
                if (Stage != 0)
                    ret += "Stage: " + Stage + "\n\t";
                if (IsBreak)
                    ret += "내구도: " + this.Durability + "/" + this.InitMaxHits + "\n\t";
                if (SellPrice != 0)
                    ret += string.Format("매매가격: {0}\n\t", SellPrice);
                if (DescText != null)
                    ret += "Special: " + DescText + "\n\t";
                if (FlavorText != null)
                    ret += "\n\t  " + FlavorText + "\n\t";
                

                if (ret != "")
                    ret = ret.Substring(0, ret.Count() - 2);
                return ret;
            }
        }

        public string NPrefix()
        {
            if (Stage >= 1)
            {
                if (this is BaseArmor)
                    return ((script.item.Upgrade)Stage).ToString().ToUpper();
                if (this is BaseWeapon)
                    return ((script.item.UpgradeWep)Stage).ToString().ToUpper();
            }
            return "";
        }
        public string NPrefix(int num)
        {
            if (num > 0)
            {
                if (this is BaseArmor)
                    return ((script.item.Upgrade)num).ToString().ToUpper();
                if (this is BaseWeapon)
                    return ((script.item.UpgradeWep)num).ToString().ToUpper();
            }
            return "";
        }

        public int EquipSlot
        {
            get
            {
                int ret = m_InvSlot - 25;
                if (ret < 0)
                    ret = 0;
                return ret;
            }
        }

        public int InvSlot
        {
            get { return m_InvSlot; }
            set
            {
                if (m_InvSlot == value) return;

                var Old_InvSlot = m_InvSlot;
                if (Old_InvSlot >= 0 && Old_InvSlot < 24)
                    m_Parent.client.SendPacket(new DeleteItemSlot((byte)Old_InvSlot).Compile());
                else if (Old_InvSlot > 24 && Old_InvSlot <= 30)
                    m_Parent.client.SendPacket(new DeleteEquipItem2(this).Compile());
                else if (Old_InvSlot >= 40)
                    m_Parent.client.SendPacket(new DeleteEntrustSlot((byte)(Old_InvSlot - 40)).Compile());

                
                m_InvSlot = value;
                if (m_InvSlot >= 0 && m_InvSlot < 24)
                    m_Parent.client.SendPacket(new AddItemToInventory2(this).Compile());
                else if (m_InvSlot > 24 && m_InvSlot <= 30)
                    m_Parent.client.SendPacket(new EquipItem2(this).Compile());
                else if (m_InvSlot >= 40)
                    m_Parent.client.SendPacket(new AddItemToEntrust(this).Compile());
            }
        }

        public Point2D Loc
        {
            get { return m_Loc; }
            set
            {
                var old_Loc = m_Loc;
                m_Loc = value;

                if (m_Loc != null)
                    World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new CreateItemGround2(this, m_Serial).Compile()));
                else
                    World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new DeleteObject(this.m_Serial).Compile()));
            }
        }
        public virtual Player Parent
        {
            get
            {
                return m_Parent;
            }
            set
            {
                if (m_Parent == value)
                    return;

                Player oldParent = m_Parent;
                if (oldParent != null && value != null && oldParent.m_Serial != value.m_Serial)
                    InvSlot = -1;

                if (value != null)
                    ParSer = value.m_Serial;
                if (value == null)
                    ParSer = 0;

                m_Parent = value;

            }
        }

        public virtual void Use(Player player)
        {
        }


        public virtual void Equip(Player player)
        {
            int eslot = -1;
            if (this is BaseWeapon)
            {
                if ((player.Class & (this as BaseWeapon).ClassReq) == 0)
                    return;

                if (((this as BaseWeapon).WeaponType == WeaponType.TwoAxe ||
                    (this as BaseWeapon).WeaponType == WeaponType.TwoHammer ||
                    (this as BaseWeapon).WeaponType == WeaponType.TwoSword)
                    && player.EquippedSlot(29) != null)
                    return;

                /*         if ((this as BaseWeapon).WeaponType == WeaponType.Axe)
                             player.eqFlags |= library.SpriteEquip.Axe;
                         if ((this as BaseWeapon).WeaponType == WeaponType.Sword)
                             player.eqFlags |= library.SpriteEquip.Sword;
                         if ((this as BaseWeapon).WeaponType == WeaponType.Hammer)
                             player.eqFlags |= library.SpriteEquip.Hammer;
                      */
                eslot = 27;
            }
            else if (this is BaseAccessory)
            {
                if ((this as BaseAccessory).Type == AccessoryType.Amulet)
                    eslot = 26;
                if ((this as BaseAccessory).Type == AccessoryType.Ring)
                    eslot = 30;
            }
            else if (this is BaseArmor)
            {
                if ((player.Class & (this as BaseArmor).ClassReq) == 0 && (this as BaseArmor).ClassReq != 0)
                    return;

                if ((this as BaseArmor).ArmorType == ArmorType.Helmet)
                    eslot = 25;
                if ((this as BaseArmor).ArmorType == ArmorType.Amulet)
                    eslot = 26;
                if ((this as BaseArmor).ArmorType == ArmorType.Armor)
                    eslot = 28;
                if ((this as BaseArmor).ArmorType == ArmorType.Shield)
                {
                    script.item.Item it = player.EquippedSlot(27);
                    if (it != null && ( (it as BaseWeapon).WeaponType == WeaponType.TwoSword || (it as BaseWeapon).WeaponType == WeaponType.TwoAxe || (it as BaseWeapon).WeaponType == WeaponType.TwoHammer)) 
                        return;

                    //    if ((this as BaseArmor).ArmorType == ArmorType.Shield)
                    //     player.eqFlags |= library.SpriteEquip.Shield;
                    eslot = 29;
                }
                if ((this as BaseArmor).ArmorType == ArmorType.Ring)
                    eslot = 30;
            }

            if (eslot != -1)
            {
                if (World.NewItems.Where(xe => xe.Value.m_Parent != null && xe.Value.m_Parent == player && xe.Value.InvSlot == eslot)
                    .FirstOrDefault().Value == null)
                {
                    if (Parent.GetStat("str") >= StrReq && Parent.GetStat("men") >= MenReq && Parent.GetStat("dex") >= DexReq && Parent.Level >= FullLevel)
                    {
                        InvSlot = eslot;
                        Parent.Equipped2.TryAdd(m_Serial, this);
                        Parent.client.SendPacket(new UpdateCharStats(Parent).Compile());
                        World.SendToAll(new QueDele(player.m_Channel, player.Map, new ChangeObjectSpritePlayer(Parent).Compile()));
                        World.SendToAll(new QueDele(player.m_Channel, player.Map, new SetObjectEffects(player).Compile()));
                        try
                        {
                            //   World.DBConnect.WriteQue.Enqueue(() => World.DBConnect.UpdateItem(this));
                            //    World.DBConnect.UpdateItem(this);
                        }
                        catch { Console.WriteLine("Update Item on Equip Failed"); }
                    }
                }
            }
        }

        public virtual void Unequip(Player player, int slot)
        {
            if (slot >= 25 && slot <= 30)
            {
                if (World.NewItems.Where(xe => xe.Value.m_Parent != null && xe.Value.m_Parent == player
                    && xe.Value.InvSlot == slot).FirstOrDefault().Value != null)
                {
                    var freeslot = Parent.GetFreeSlot();
                    if (freeslot != -1)
                    {
                        script.item.Item outt;
                        Parent.Equipped2.TryRemove(m_Serial, out outt);
                        InvSlot = freeslot;
                        Parent.client.SendPacket(new UpdateCharStats(Parent).Compile());
                        World.SendToAll(new QueDele(Parent.m_Channel, Parent.Map, new ChangeObjectSpritePlayer(Parent).Compile()));
                        World.SendToAll(new QueDele(Parent.m_Channel, Parent.Map, new SetObjectEffects(player).Compile()));
                        try
                        {
                            //  World.DBConnect.WriteQue.Enqueue(() => World.DBConnect.UpdateItem(this));
                            //   World.DBConnect.UpdateItem(this);
                        }
                        catch { Console.WriteLine("Update Item on Unequip Failed"); }
                    }
                }
            }
        }

        /*
         public const byte Hat = 0x00;
        public const byte Amulet = 0x01;
        public const byte Weapon = 0x02;
        public const byte Armor = 0x03;
        public const byte Shield = 0x04;
        public const byte Ring = 0x05;
         */
        public void Drop(int Channel, Point2D loc, string map)
        {
            m_Channel = Channel;
            m_Map = map;
            Loc = new Point2D(loc.X, loc.Y);
            InvSlot = -1;
            Parent = null;
            ParSer = 0;
            DropTime = LKCamelot.Server.tickcount.ElapsedMilliseconds;


            var player = PlayerHandler.getSingleton().add[Channel].Where(xe => xe.Value.loggedIn && xe.Value.m_Channel == Channel && xe.Value.Map == map).ToList();
            foreach (var p in player)
            {
                p.Value.InstancedObjects.TryAdd(this.m_Serial, this);
            }
            try
            {
                //   World.DBConnect.WriteQue.Enqueue(() => World.DBConnect.DeleteItem(m_Serial));
                //   World.DBConnect.DeleteItem(m_Serial);
            }
            catch
            {
                Console.WriteLine("failed to delete item on drop");
            }
        }
        public void Drop()
        {
            if (this.m_Map == string.Empty)
                return;
            if (this.Loc == null)
                return;

            Drop(this.m_Channel, this.m_Loc, this.m_Map);
        }
        public void Drop(Player player)
        {
            Drop(player.m_Channel, player.Loc, player.Map);
        }
        public void Drop(script.monster.Monster mob, Player player)
        {
            Drop(mob.m_Channel, mob.m_Loc, player.Map);
        }
        public void Drop(Player player, int j)
        {
            if (this.Quantity <= j)
                Drop(player.m_Channel, player.Loc, player.Map);
            
            else
            {
                this.Quantity -= j;
                m_Parent.client.SendPacket(new AddItemToInventory2(this).Compile());
                                
                script.item.Item clone = this.Clone();
                clone.Quantity = j;

                clone.Drop(player.m_Channel, player.Loc, player.Map);
            }
        }

        public void Repair(Player player)
        {
            if (!IsBreak)
                return;


            //Durability      현재내구도
            //MaxDurability   최대 내구도
            int priece = ((int)this.BuyPrice / InitMaxHits) * (InitMaxHits - Durability);

            if (player.Gold >= (ulong)priece)
            {
                try
                {
                    checked
                    {
                        player.Gold -= (ulong)priece;
                        this.Durability = InitMaxHits;
                        player.client.SendPacket(new AddItemToInventory2(this).Compile());
                    }
                }
                catch (OverflowException) { }
            }
            else
                m_Parent.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "돈이 부족합니다.").Compile());
        }

        #region V2
        public virtual void PickUp(Player player)
        {
            var slot = player.GetFreeSlot();

            if (this is Gold)
            {
                try
                {
                    checked
                    {
                        player.Gold += (uint)Quantity;
                        this.DeleteGround();
                    }
                }
                catch (OverflowException) { }

                return;
            }
            else if (slot != -1)
            {
                var item = player.Inventory.Where(xe => xe.Name == this.Name && xe.InvSlot < 24 && xe.Quantity < this.Limit && xe.Stage == this.Stage).FirstOrDefault();

                if (item != null && this.Stackable)
                {
                    if ((item.Quantity + this.Quantity) > item.Limit)            // 바닥 갯수와 인벤 갯수가 24개가 넘어
                    {
                        int Remainder = item.Limit - item.Quantity;               // 소유 가능한 갯수를 구함      
                        if (Remainder == 0) return;

                        this.Quantity -= Remainder;                              // 바닥에 떨어진 아이템 갯수를  삭제
                        item.Quantity += Remainder;                         // 줍는다.

                        DropTime = 0;
                        this.Parent = player;
                        this.Loc = null;
                        this.m_Map = "inventory";
                        this.InvSlot = slot;

                        if (this.Parent != null)
                        {
                            World.NewItems.AddOrUpdate(this.m_Serial, this, (key, value) => value = this);
                            this.Parent.client.SendPacket(new AddItemToInventory2(this).Compile());
                        }
                        item.Parent.client.SendPacket(new AddItemToInventory2(item).Compile());
                    }
                    else
                    {
                        item.Quantity += this.Quantity;                   // 아이템을 주워 넣는다.

                        // 바닥 아이템 삭제
                        this.DeleteGround();
                        item.Parent.client.SendPacket(new AddItemToInventory2(item).Compile());
                    }
                }
                else
                {
                    DropTime = 0;
                    World.RemoveDynamicObj(m_Map, m_Serial);
                    Parent = player;
                    Loc = null;
                    m_Map = "inventory";
                    InvSlot = slot;
                    // World.DBConnect.WriteQue.Enqueue(() => World.DBConnect.UpdateItem(this));
                }
            }
            else
            {

            }
        }
        #endregion

        /// <summary>
        /// 아이템을 생성한 후, 인벤토리에 넣는다.
        /// </summary>
        /// <param name="play">아이템 지급할 케릭터</param>
        /// <returns></returns>
        public virtual Item Inventory(Player play)
        {
            int slot = play.GetFreeSlot();
            if (slot == -1) return null;


            //가지고 있는 아이템
            var item = play.Inventory.Where(xe => xe.Name == this.Name && xe.InvSlot < 24 && xe.Quantity < this.Limit && xe.Stage == this.Stage).FirstOrDefault();             //기존에 있던 템
            if (item != null && this.Stackable)
            {
                if (item.Quantity + this.Quantity > item.Limit)
                {
                    int Amount = item.Limit - item.Quantity;            // 전체에서 가지고 있는 수를 뺌
                    item.Quantity += Amount;
                    this.Quantity -= Amount;

                    DropTime = 0;
                    this.Parent = play;
                    this.Loc = null;
                    this.m_Map = "inventory";
                    this.InvSlot = slot;

                    if (this.Parent != null)
                    {
                        World.NewItems.AddOrUpdate(this.m_Serial, this, (key, value) => value = this);
                        this.Parent.client.SendPacket(new AddItemToInventory2(this).Compile());
                    }
                    item.Parent.client.SendPacket(new AddItemToInventory2(item).Compile());

                    return null;
                }

                else
                {
                    item.Quantity += this.Quantity;
                    play.client.SendPacket(new AddItemToInventory2(item).Compile());

                    this.Delete(play);
                    if (this.Parent != null)
                        this.Parent.client.SendPacket(new DeleteItemSlot((byte)this.InvSlot).Compile());
                    return item; 
                }
            }
            else
            {
                DropTime = 0;
                this.Parent = play;
                this.Loc = null;
                this.m_Map = "inventory";
                this.InvSlot = slot;
                play.client.SendPacket(new AddItemToInventory2(this).Compile());
                World.NewItems.AddOrUpdate(this.m_Serial, this, (key, value) => value = this);

                return this;
            }
        }

        public bool TryUpgrade()
        {
            if (Util.Random(0, 100) >= 100 * (Stage * 0.1))
            {
                Stage++;
             //   World.DBConnect.WriteQue.Enqueue(() => World.DBConnect.UpdateItem(this));
                return true;
            }
            else
            {
                DeleteGround();
                return false;
            }
        }

        public void DeleteGround()
        {
            World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new DeleteObject(m_Serial).Compile()));
            Item temp;
            if (!World.NewItems.TryRemove(m_Serial, out temp))
                Console.WriteLine("Failed to delete item");
            World.RemoveDynamicObj(m_Map, m_Serial);
          //  World.DBConnect.WriteQue.Enqueue(() => World.DBConnect.DeleteItem(m_Serial));
        }
        public void Delete(Player player)
        {
            this.Quantity = 0;
            this.m_Loc = null;
            this.m_Map = null;
            this.InvSlot = -1;
            this.Parent = null;
            this.ParSer = -1;
            Item temp;
            World.NewItems.TryRemove(m_Serial, out temp);
            try
            {
             //   World.DBConnect.WriteQue.Enqueue(() => World.DBConnect.DeleteItem(m_Serial));
                //     World.DBConnect.DeleteItem(m_Serial);
            }
            catch
            {
                Console.WriteLine("failed to delete item on delete");
            }
        }

        public virtual void Event_Give(Player player, Item itemdragdrop)
        {
            string fail = "강화 실패!";
            string succ = "Refining success.";

            if (this is script.item.BaseArmor && itemdragdrop is script.item.Zel
                || this is script.item.BaseWeapon && itemdragdrop is script.item.Dai)
            {
                string log;
                itemdragdrop.Delete(player);
                if (this.TryUpgrade())
                {
                    player.client.castHandler.CreateMagicEffect(Loc, player.m_Channel, m_Map, 0, 42);

                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, succ).Compile());
                    World.SendToAll(new QueDele(player.m_Channel, player.Map, new DeleteObject(m_Serial).Compile()));
                    World.SendToAll(new QueDele(player.m_Channel, player.Map, new CreateItemGround2(this, this.m_Serial).Compile()));
                    log = string.Format("{0}: [{1}] {2},{3},{4},{5},{6},{7},{8},{9}", DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]"),
                                                                                "성공",
                                                                                this.m_Serial,
                                                                                this.Name,
                                                                                this.Stage,
                                                                                this.m_Channel,
                                                                                this.m_Map,
                                                                                this.Loc.X,
                                                                                this.Loc.Y,
                                                                                player.Name);
                }
                else
                {
                    player.client.castHandler.CreateMagicEffect(Loc, player.m_Channel, this.m_Map, 0, 56);
                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, fail).Compile());
                    log = string.Format("{0}: [{1}] {2},{3},{4},{5},{6},{7},{8}", DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]"),
                                                                                "실패",
                                                                                this.m_Serial,
                                                                                this.Name,
                                                                                this.Stage+1,
                                                                                this.m_Channel,
                                                                                this.m_Map,
                                                                                this.Loc.X,
                                                                                this.Loc.Y,
                                                                                player.Name);
                }
                io.IOClient.WriteUpgradeItem(log);
            }
        }

        public Item Clone()
        {
            script.item.Item item = (script.item.Item)Activator.CreateInstance(Type.GetType(this.GetType().ToString()));

            item.Stage = this.Stage;
            item.Quantity = this.Quantity;
            // A clone is temporary until its caller assigns an owner or drops it.
            // Copying Parent makes ownerless, slotless clones reappear on login.
            item.m_Parent = null;
            item.ParSer = 0;
            item.m_InvSlot = -1;
            item.m_Map = null;
            item.m_Loc = null;
            item.DropTime = 0;

            World.NewItems.TryAdd(item.m_Serial, item);
            return item;
        }
    }
}
