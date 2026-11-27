using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class Arnold : BaseNPC
    {
        public override string Name { get { return "아놀드"; } }
        public override byte Sprite { get { return 1; } }
        public override byte aSpeed { get { return 4; } }
        public override byte aFrames { get { return 7; } }

        public Arnold(Serial serial, int Channel, string Map, Point2D Loc)
            : base(serial)
        {
            base.m_Channel = Channel;
            base.m_Map = Map;
            base.m_Loc = new Point2D(Loc.X, Loc.Y);
        }

        public override List<string> ChatString
        {
            get
            {
                return new List<string>()
                {
                    "초보들에게 필요한 장비를 팔고 있습니다.",
                    "사고싶은 물건을 보려면 날 클릭하세요",
                    "팔고 싶은 물건이 있으면 저한테 주세요.",
                    
                };
            }
        }
        public override bool Alive
        {
            get { return base.Alive; }
            set
            {
                if (base.Alive == false && value == true)
                    this.RefreshItem();
                base.Alive = value;
            }
        }

        public override void RefreshItem()
        {
            SellList.Clear();
            SellList.AddRange(TotalList.OrderBy(arg => Guid.NewGuid()).Take(12).ToList());
            SellList.AddRange(StaticList.OrderBy(arg => Guid.NewGuid()).Take(2).ToList());
        }
        List<script.item.Item> SellList = new List<script.item.Item>();
        List<script.item.Item> TotalList = new List<script.item.Item>()
            {
                new script.item.Suit((LKCamelot.model.Serial)1),
                new script.item.Surplice(1),
                new script.item.Cape(1),
                new script.item.Robe(1),

                new script.item.BambooKnife(1),
                new script.item.WoodenSword(1),
                new script.item.ShortSword(1),
                new script.item.Saw(1),

                new script.item.SpikedClub(1),
                new script.item.MorningStar(1),
                new script.item.SmallShield(1),
                new script.item.Hood(1),

                new script.item.Crown(1),
                new script.item.Mask(1),
                new script.item.FullDress(1),
                new script.item.Mace(1),

            };
        List<script.item.Item> StaticList = new List<item.Item>()
        {

        };

        public override void Event_Message(Player player, string[] str2, string str)
        {
            #region 판다
            if (str2[str2.Count() - 1] == "판다")
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(str2[str2.Count() - 2], @"[0-9]{1,3}"))
                    return;
                if (Convert.ToInt32(str2[str2.Count() - 2]) <= 0)
                    return;

                int amonut = Convert.ToInt32(str2[str2.Count() - 2]);
                string Name = string.Empty;

                for (int i = 0; i < str2.Count() - 2; i++)
                {
                    Name += " " + str2[i];
                }
                Name = Name.Substring(1, Name.Length - 1);

                var item = player.Inventory.Where(xe => xe.Name == Name && xe.Quantity >= amonut).FirstOrDefault();

                if (item != null)
                {
                    checked
                    {
                        if (item.Quantity > amonut)
                        {
                            item.Quantity -= amonut;
                            player.Gold += (ulong)(amonut * item.SellPrice);
                            player.client.SendPacket(new AddItemToInventory2(item).Compile());
                        }
                        else if (item.Quantity == amonut)
                        {
                            script.item.Item it;
                            World.NewItems.TryRemove(item.m_Serial, out it);

                            player.Gold += (ulong)(amonut * it.SellPrice);
                            player.client.SendPacket(new DeleteItemSlot((byte)it.InvSlot).Compile());
                        }
                    }
                }
            }
            #endregion



            #region 산다
            if (str2[str2.Count() - 1] == "산다")
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(str2[str2.Count() - 2], @"^[0-9]{1,3}"))
                    return;
                if (Convert.ToInt64(str2[str2.Count() - 2]) <= 0 && Convert.ToInt64(str2[str2.Count() - 2]) > 200)
                    return;


                int amount = Convert.ToInt32(str2[str2.Count() - 2]);
                string ItName = string.Empty;

                for (int i = 0; i < str2.Count() - 2; i++)
                    ItName += " " + str2[i];
                ItName = ItName.Remove(0, 1);

                var sellitem = SellList.Where(xe => xe.Name == ItName).FirstOrDefault();
                if (sellitem != null)
                {
                    script.item.Item tempitem = (script.item.Item)Activator.CreateInstance(sellitem.GetType());
                    if (tempitem == null)
                        return;
                    if (!tempitem.Stackable)
                        amount = 1;
                    if (sellitem.Limit < amount)
                        amount = sellitem.Limit;
                    if (player.Gold < (tempitem.BuyPrice * (ulong)amount))
                        return;
                    if (amount <= 0)
                        return;


                    var inv = player.Inventory.Where(xe => xe.Name == ItName).FirstOrDefault();
                    if (inv != null && inv.Quantity + amount > tempitem.Limit)
                    {
                        amount = tempitem.Limit - inv.Quantity;
                        if (amount <= 0)
                            return;
                    }

                    // 골드는 지급이 가능한지 확인한 뒤에 차감한다.
                    // (기존: 먼저 차감해서 인벤토리가 가득 차 있으면 아이템 없이 골드만 빠져나갔다)
                    if (player.GetFreeSlot() == -1)
                    {
                        this.Event_Chat("인벤토리에 빈 공간이 없습니다.", true);
                        return;
                    }

                    ulong cost;
                    try { cost = checked(tempitem.BuyPrice * (ulong)amount); }
                    catch (OverflowException) { return; }
                    if (player.Gold < cost)
                        return;

                    // Inventory()는 묶음에 일부만 합쳐지고 나머지가 새 칸으로 갈 때 null을 돌려주지만 지급은 된 것이므로
                    // 반환값 대신 미리 받아 둔 이름을 쓴다.
                    string itemName = tempitem.Name;
                    tempitem.Quantity = (int)amount;
                    tempitem.Inventory(player);
                    player.Gold -= cost;
                    this.Event_Chat(string.Format("{0}를 {1}개 판매했습니다.", itemName, amount), true);
                }
            }
            #endregion
        }
        public override void Event_Give(Player player, item.Item giveitem, int amount = 1)
        {
            if (giveitem.Quantity <= amount)
            {
                script.item.Item it;

                player.Gold += (ulong)(giveitem.SellPrice * giveitem.Quantity);
                World.NewItems.TryRemove(giveitem.m_Serial, out it);
                it.Delete(player);
            }
            else
            {
                giveitem.Quantity -= amount;
                player.Gold += (ulong)(giveitem.SellPrice * amount);

                giveitem.m_Parent.client.SendPacket(new AddItemToInventory2(giveitem).Compile());
            }
            this.Event_Chat(string.Format("{0} {1}를 {2}개 매입했습니다.", giveitem.NPrefix(), giveitem.Name, amount), true);
            
        }
        public override void Buy(model.Player player, int buyslot)
        {

            if (player.GetFreeSlot() != -1 && player.Gold >= SellList[buyslot].BuyPrice)
            {
                script.item.Item tempitem = (Activator.CreateInstance(SellList.ElementAt(buyslot).GetType()) as script.item.Item).Inventory(player);

                if (tempitem == null)
                    return;

                player.Gold -= tempitem.BuyPrice;
                this.Event_Chat(string.Format("{0}를 판매했습니다.", tempitem.Name), true);
            }
        }


        public override GUMP Gump
        {
            get
            {
                return new GUMP((int)this.m_Serial, 0xff85, 0x03ff, 0x70, "Menu", SellList);
            }
        }
    }
}
