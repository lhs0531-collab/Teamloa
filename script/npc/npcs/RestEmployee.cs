using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class RestEmployee : BaseNPC
    {
        public override string Name { get { return "점원"; } }
        public override byte Sprite { get { return 6; } }
        public override byte aSpeed { get { return 2; } }
        public override byte aFrames { get { return 7; } }

        public RestEmployee(Serial serial, int Channel, string Map, Point2D Loc)
            : base(serial)
        {
            base.m_Channel = Channel;
            m_Map = Map;
            base.m_Loc = new Point2D(Loc.X, Loc.Y);
        }
        public override List<string> ChatString
        {
            get
            {
                return new List<string>() {
                    "대장장이에 필요한 아이템을 판매하고 있습니다.",
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
        }

        public override void Event_Message(Player player, string[] str2, string str)
        {
            #region 산다
            if (str2.Count() >= 3 && str2[str2.Count() - 1] == "산다")
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
            else
            { }
        }

        List<script.item.Item> SellList = new List<script.item.Item>();
        List<script.item.Item> TotalList = new List<script.item.Item>()
            {
                new script.item.고무(1),
                new script.item.각목(1),
                new script.item.나무쪼가리(1),
                new script.item.톱밥(1),
                new script.item.가죽(1),
                new script.item.종이(1),
                new script.item.화강석(1),
                new script.item.자갈(1),
                new script.item.조가비(1),
            };
        List<script.item.Item> StaticList = new List<item.Item>()
        {

        };

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
