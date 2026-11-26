using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class Employee : BaseNPC
    {
        public override string Name { get { return "노점상"; } }
        public override byte Sprite { get { return 6; } }
        public override byte aSpeed { get { return 2; } }
        public override byte aFrames { get { return 7; } }

        public Employee(Serial serial, int Channel, string Map, Point2D Loc)
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
                return new List<string>() {
                    "초보들에게 필요한 법책을 팔고있습니다.",
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
            SellList.AddRange(TotalList.OrderBy(arg => Guid.NewGuid()).Take(8).ToList());
            SellList.AddRange(StaticList.OrderBy(arg => Guid.NewGuid()).Take(2).ToList());    
        }

        List<script.item.Item> SellList = new List<script.item.Item>();
        List<script.item.Item> TotalList = new List<script.item.Item>()
            {
                new script.item.HealBook(1),
                new script.item.PlusHealBook(1),
                new script.item.MagicArmorBook(1),
                new script.item.RainbowArmorBook(1),
                new script.item.GuardianSwordBook(1),

                new script.item.FireProtectorBook(1),
                new script.item.TeagueShieldBook(1),
                new script.item.MagicShieldBook(1),
                //new script.item.MentalSwordBook(1),
                new script.item.SidewinderBook(1),

                new script.item.WakondaBook(1),
                new script.item.SilverscreenBook(1),
                new script.item.ButterflyBook(1),
            };
        List<script.item.Item> StaticList = new List<item.Item>()
        {
                new script.item.Dai(1),
                new script.item.Zel(1),
        };
        public override void Buy(model.Player player, int buyslot)
        {
            if (player.GetFreeSlot() != -1 && player.Gold >= SellList[buyslot].BuyPrice)
            {
                script.item.Item tempitem = (Activator.CreateInstance(SellList.ElementAt(buyslot).GetType()) as script.item.Item).Inventory(player);

                if (tempitem == null)
                    return;
                checked
                {
                    player.Gold -= tempitem.BuyPrice;
                }
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
