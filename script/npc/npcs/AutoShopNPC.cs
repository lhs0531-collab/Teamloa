using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class AutoShopNPC : BaseNPC
    {
        public Player owner;
        public long createtime = 0;
        public override string Name { get { return this.ToString(); } }
        public override byte Sprite { get { return 6; } }
        public override byte aSpeed { get { return 2; } }
        public override byte aFrames { get { return 7; } }
        
        public AutoShopNPC(Player player) : base(Serial.NewMobile)
        {
            this.m_Channel = player.m_Channel;
            this.m_Map = player.Map;
            this.m_Loc = new Point2D(player.Loc.X, player.Loc.Y);

            owner = player;
            createtime = Server.tickcount.ElapsedMilliseconds;
        }

        public override void Event_Give(Player player, item.Item giveitem, int amount = 1)
        {
            if (player != owner ||
                (giveitem is script.item.LifeDrug) ||
                (giveitem is script.item.MagicDrug) ||
                (giveitem is script.item.FullLifeDrug) ||
                (giveitem is script.item.FullMagicDrug))
                return;

            if (SellList.Count >= 12)
            {
                player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "더 이상 등록할 수 없습니다.").Compile());
                return;
            }

            this.Event_Chat("등록되었습니다.", true);
            script.item.Item clone = giveitem.Clone();
            clone.Parent = null;
            clone.ParSer = 0;
            SellList.Add(clone);
            giveitem.Delete(player);
        }


        public override void Buy(model.Player player, int buyslot)
        {
            //if (player.GetFreeSlot() != -1 && player.Gold >= SellList[buyslot].BuyPrice)
            //{
            //    script.item.Item tempitem = (Activator.CreateInstance(SellList.ElementAt(buyslot).GetType()) as script.item.Item).Inventory(player);

            //    if (tempitem == null)
            //        return;
            //    checked
            //    {
            //        player.Gold -= tempitem.BuyPrice;
            //    }
            //    this.Event_Chat(string.Format("{0}를 판매했습니다.", tempitem.Name), true);
            //}
        }

        List<script.item.Item> SellList = new List<script.item.Item>();
        public override GUMP Gump
        {
            get
            {
                return new GUMP((int)this.m_Serial, 0xff85, 0x03ff, 0x70, this.ToString(), SellList);
            }
        }

        public override string ToString()
        {
            return (owner == null) ? "자동상점" : (owner.Name + " 상점");
        }
    }
}
