using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class Boy : BaseNPC
    {
        public override string Name { get { return "소년"; } }
        public override byte Sprite { get { return 7; } }
        public override byte aSpeed { get { return 2; } }
        public override byte aFrames { get { return 7; } }

        public Boy(Serial serial, int Channel, string Map, Point2D Loc)
            : base(serial)
        {
            base.m_Channel = Channel;
            base.m_Map = Map;
            base.m_Loc = new Point2D(Loc.X, Loc.Y);
            this.RefreshItem();
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
            SellList.AddRange(TotalList.OrderBy(arg => Guid.NewGuid()).Take(13).ToList());
            SellList.AddRange(ticketList.Where(xe => xe.Name.Split(' ')[0].Contains(Settings.VillageName.ElementAt(m_Channel)) == false).ToList().OrderBy(arg => Guid.NewGuid()).Take(2).ToList());
        }

        List<script.item.Item> SellList = new List<script.item.Item>();
        List<script.item.Item> ticketList = new List<item.Item>()
        {
            new script.item.Village0Ticket(1),
            new script.item.Village1Ticket(1),
            new script.item.Village2Ticket(1),
        };
        List<script.item.Item> TotalList = new List<script.item.Item>()
            {
                new script.item.Rod(1),
                new script.item.ShortStaff(1),
                new script.item.Crook(1),
                new script.item.BambooSpear(1),

                new script.item.ShortSpear(1),
                new script.item.Skewer(1),
                new script.item.성냥(1),
                new script.item.장작(1),
                
                new script.item.Brazier(1),
                new script.item.Anvil(1),
                new script.item.MinerSuit(1),
                new script.item.BlackSmithSuit(1),
            };
        List<script.item.Item> StaticList = new List<item.Item>()
        {

        };

        public override void Event_Message(Player player, string[] str2, string str)
        {
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
                return new GUMP(this.m_Serial, 0xff85, 0x03ff, 0x70, "Menu", SellList);
            }
        }
    }
}
