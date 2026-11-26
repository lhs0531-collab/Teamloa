using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class Loen : BaseNPC
    {
        public override string Name { get { return "로엔"; } }
        public override byte Sprite { get { return 3; } }
        public override byte aSpeed { get { return 4; } }
        public override byte aFrames { get { return 7; } }

        public Loen(Serial serial, int Channel, string Map, Point2D Loc)
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
                    "전 시약전문입니다.",
                    "[@잔액조회] 이렇게 잔액을 확인할 수 있어요.",
                    "전 물건은 안삽니다.",
                    "돈을 송금하고 싶은분은 제게 오세요.",
                    

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
            SellList.AddRange(TotalList);
            SellList.AddRange(ItemList1.OrderBy(arg => Guid.NewGuid()).Take(1).ToList());
            SellList.AddRange(ItemList2.OrderBy(arg => Guid.NewGuid()).Take(2).ToList());
        }

        List<script.item.Item> SellList = new List<script.item.Item>()
            ;
        List<script.item.Item> TotalList = new List<script.item.Item>()
            {
                new script.item.LifeDrug(1),
                new script.item.MagicDrug(1),
                new script.item.FullLifeDrug(1),
                new script.item.FullMagicDrug(1),
                //new script.item.ShopTicket(1),
           //     new ShopItem(3, "50000 Which Book should I be?", 50000),
           //     new ShopItem(0x18, "30000 Poision of Magi", 30000),
            };
        List<script.item.Item> ItemList1 = new List<item.Item>()
        {                
                new script.item.ShamanBook(1),
                new script.item.WizardBook(1),
                new script.item.KnightBook(1),
                new script.item.SwordmanBook(1),
        };
        List<script.item.Item> ItemList2 = new List<item.Item>()
        {
                new script.item.StrStatPotion(1),
                new script.item.MenStatPotion(1),
                new script.item.DexStatPotion(1),
                new script.item.VitStatPotion(1),
        };


        public override void Event_Message(Player player, string[] str2, string str)
        {
            #region 산다
            //if (!System.Text.RegularExpressions.Regex.IsMatch(str, @"^(작은 ?)?(마력|체력)시약 \d{1,2} 산다$"))
            //    return;
            
            //if (Convert.ToInt64(str2[str2.Count() - 2]) <= 0 && Convert.ToInt64(str2[str2.Count() - 2]) > 200)
            //    return;

            if (str2.Count() >= 3 && str2[str2.Count() - 1] == "산다")
            {
                int amount;
                if (!int.TryParse(str2[str2.Count() - 2], System.Globalization.NumberStyles.None,
                    System.Globalization.CultureInfo.InvariantCulture, out amount) || amount <= 0 || amount > 200)
                    return;
                string ItName = string.Empty;

                for (int i = 0; i < str2.Count() - 2; i++)
                    ItName += " " + str2[i];
                ItName = ItName.Remove(0, 1);

                switch (ItName)
                {
                    case "마력시약":
                        ItName = "FULL MAGIC DRUG";
                        break;
                    case "작은 마력시약":
                    case "작은마력시약":
                        ItName = "MAGIC DRUG";
                        break;
                    case "체력시약":
                        ItName = "FULL LIFE DRUG";
                        break;
                    case "작은 체력시약":
                    case "작은체력시약":
                        ItName = "LIFE DRUG";
                        break;
                    default:
                        return;
                }


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
                    if (amount <= 0)
                        return;


                    var inv = player.Inventory.Where(xe => xe.Name == ItName && xe.InvSlot >= 0 && xe.InvSlot < 24).FirstOrDefault();
                    if (inv != null && inv.Quantity + amount > tempitem.Limit)
                    {
                        amount = tempitem.Limit - inv.Quantity;
                        if (amount <= 0)
                            return;
                    }

                    ulong cost;
                    try { cost = checked(tempitem.BuyPrice * (ulong)amount); }
                    catch (OverflowException) { return; }
                    if (player.Gold < cost)
                        return;

                    tempitem.Quantity = (int)amount;
                    var purchasedItem = tempitem.Inventory(player);
                    if (purchasedItem == null)
                    {
                        this.Event_Chat("인벤토리에 빈 공간이 없습니다.", true);
                        return;
                    }
                    player.Gold -= cost;
                    tempitem = purchasedItem;
                    this.Event_Chat(string.Format("{0}를 {1}개 판매했습니다.", tempitem.Name, amount), true);
                }
            }
            #endregion

            #region 산다 V1
            //if (str2[str2.Count() - 1] == "산다")
            //{
            //    if (!System.Text.RegularExpressions.Regex.IsMatch(str2[1], @"[0-9]{1,3}"))
            //        return;
            //    if (Convert.ToInt32(str2[1]) <= 0)
            //        return;

            //    int amonut = Convert.ToInt32(str2[1]);

            //    script.item.Item Item = null;
            //    switch (str2[0])
            //    {
            //        case "마력시약":
            //            Item = new script.item.FullMagicDrug();
            //            break;
            //        case "작은마력시약":
            //            Item = new script.item.MagicDrug();
            //            break;
            //        case "체력시약":
            //            Item = new script.item.FullLifeDrug();
            //            break;
            //        case "작은체력시약":
            //            Item = new script.item.LifeDrug();
            //            break;
            //        default:
            //            return;
            //    }

            //    if (Item.Limit < amonut)
            //        return;
            //    if ((long)player.Gold - ((long)Item.BuyPrice * amonut) < 0)
            //        return;

            //    var ore = player.Inventory.Where(xe => xe.Name == Item.Name && xe.Stage == Item.Stage && xe.InvSlot < 24).FirstOrDefault();

            //    if (ore != null)
            //    {
            //        if (ore.Quantity + amonut > ore.Limit)
            //            amonut = ore.Limit - ore.Quantity;

            //        ore.Quantity += amonut;
            //        player.client.SendPacket(new LKCamelot.model.AddItemToInventory2(ore).Compile());
            //    }
            //    else
            //    {
            //        Item.Quantity = amonut;
            //        Item.Parent = player;
            //        Item.InvSlot = player.GetFreeSlot();
            //        Item.Loc = null;
            //        World.NewItems.TryAdd(Item.m_Serial, Item);
            //        player.client.SendPacket(new LKCamelot.model.AddItemToInventory2(Item).Compile());
            //    }
            //    player.Gold -= Item.BuyPrice * (ulong)amonut;
            //    this.Event_Chat(string.Format("{0}를 {1}개 팔았습니다.", Item.Name, amonut));
            //}
            #endregion
            else
            { }
        }
        public override void Event_Give(Player player, item.Item giveitem, int amount)
        {
            if (player.Map != this.m_Map || World.Dist2d(this.m_Loc.X, this.m_Loc.Y, player.Loc.X, player.Loc.Y) > this.EventRange)
                return;

            if (player.FreeBankSlot != -1)
            {
                giveitem.InvSlot = player.FreeBankSlot;
                this.Event_Chat(string.Format("{0} {1}을 보관하였습니다.", giveitem.NPrefix(), giveitem.Name), true);
            }
            else
            {
                this.Event_Chat("더 이상 보관할 수 없습니다.", true);
            }
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
