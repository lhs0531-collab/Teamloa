using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class Exchange : BaseNPC
    {
        Player Sellplayer;
        string BuyPlayer;
        ulong Price = 0;
        script.item.Item it = null;


        public override string Name { get { return "중매사"; } }
        public override List<string> ChatString
        {
            get
            {
                return new List<string>()
                {
                    "아이템을 교환하려면 저에게 오세요",
                    "아이템 교환을 취소하시려면 \'@취소\'를 입력하세요",
                    "'@가격 [금액] [상대방아이디]'으로 아이템 판매가격을 정하세요",
                    "'@산다 [금액]'으로 아이템을 교환하세요.",
                };
            }
        }

        public override byte Sprite { get { return 2; } }
        public override byte aSpeed { get { return 4; } }
        public override byte aFrames { get { return 4; } }
        public override int MessageGap { get { return 3 * Settings.Second; } }
        public override int EventRange { get { return 6; } }

        public Exchange(Serial serial)
            : base(serial)
        {
        }
        public Exchange(Serial serial, int Channel, string Map, Point2D point)
            : base(serial)
        {
            base.m_Channel = Channel;
            base.m_Map = Map;
            base.m_Loc = new Point2D(point.X, point.Y);
        }
        public override void Event_Give(Player player, item.Item giveitem, int amount)
        {
            if (it == null)
            {
                Sellplayer= player;
                it = giveitem;
                this.Event_Chat(string.Format("아이템:{0}, 판매금액과 상대방아이디를 입력해주세요. '@가격 [가격] [아이디]'", it.Name));
            }
            else
                this.Event_Chat(string.Format("현재 교환이 진행중입니다. 취소를 원하시면 '@취소'를 입력하세요", it.Name));
        }

        public override void Event_Message(Player player, string[] str2, string str)
        {
            if (str2[0] == "@취소")
            {
                if ( Sellplayer == null || Sellplayer.Name == player.Name ||
                     !Sellplayer.loggedIn || this.m_Channel !=  player.m_Channel || this.m_Channel != Sellplayer.m_Channel || 
                    World.Dist2d(this.m_Loc.X, this.m_Loc.Y, Sellplayer.Loc.X, Sellplayer.Loc.Y) > this.EventRange ||
                    this.m_Map != Sellplayer.Map)
                {
                    Price = 0;
                    Sellplayer = null;
                    BuyPlayer = string.Empty;
                    it = null;
                    this.Event_Chat(string.Format("아이템 교환이 취소되었습니다."));
                }
            }
            else if (str2[0] == "@가격" && it != null)
            {
                if (str2.Count() != 3)
                    return;
                if (Sellplayer.Name != player.Name)
                    return;
                if (!System.Text.RegularExpressions.Regex.IsMatch(str2[1], @"[0-9]"))
                    return;
                if (Convert.ToInt64(str2[1]) <= 0)
                    return;

                BuyPlayer = str2[2];
                Price = Convert.ToUInt64(str2[1]);
                this.Event_Chat(string.Format("아이템:{1}, 금액:{2:#,###} GOLD", BuyPlayer, it.Name, Price));
            }

            else if (str2[0] == "@산다" && Price != 0)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(str2[1], @"[0-9]"))
                    return;
                if (Convert.ToUInt64(str2[1]) <= 0 || Convert.ToUInt64(str2[1]) != Price || player.Gold < Price)
                    return;
                if (BuyPlayer != player.Name)
                {
                    this.Event_Chat(string.Format("{0}님만 구매할 수 있습니다. 교환을 초기화 하고 싶으시면 '@취소'를 입력하세요", BuyPlayer));
                    return;
                }
                if (World.Dist2d(this.m_Loc.X, this.m_Loc.Y, player.Loc.X, player.Loc.Y) <= EventRange &&
                    World.Dist2d(this.m_Loc.X, this.m_Loc.Y, Sellplayer.Loc.X, Sellplayer.Loc.Y) <= EventRange &&
                    this.m_Map == player.Map && this.m_Map == Sellplayer.Map && player.loggedIn && Sellplayer.loggedIn &&
                    it.ParSer == Sellplayer.m_Serial && it.InvSlot >= 0 && it.InvSlot < 24 && player.GetFreeSlot() != -1)
                {

                    it.Inventory(player);
                    checked
                    {
                        Sellplayer.Gold += Price;
                        player.Gold -= Price;
                    }
                    this.Event_Chat(string.Format("아이템 교환이 성공하였습니다.", it.Name, Price));

                    Price = 0;
                    Sellplayer = null;
                    it = null;
                }
                else
                {
                    Price = 0;
                    Sellplayer = null;
                    it = null;
                    this.Event_Chat(string.Format("아이템 교환이 취소되었습니다."));
                }
            }
        }

    }
}
