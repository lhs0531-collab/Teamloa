using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class Aron : BaseNPC
    {
        public override string Name { get { return "아론"; } }
        public override List<string> ChatString
        {
            get
            {
                return new List<string>()
                {
                    "승급하고 싶으면, 작은체력시약을 나에게 주시오.",
                    "요즘 알리아스는 잘있는지 궁금하구나...",
                    "\"힘속성 올려주세요\"하면 경험치를 힘속성으로 바꿔준다네.",
                    "\"지력속성 올려주세요\"하면 경험치를 지력속성으로 바꿔준다네.",
                    "\"숙련속성 올려주세요\"하면 경험치를 숙련속성으로 바꿔준다네.",
                    "\"생명속성 올려주세요\"하면 경험치를 생명속성으로 바꿔준다네.",
                    "\"기사 시켜주세요\"하면 기사가 됩니다.",
                    "\"검객 시켜주세요\"하면 검객이 됩니다.",
                    "\"샤먼 시켜주세요\"하면 샤먼이 됩니다.",
                    "\"위저드 시켜주세요\"하면 위저드가 됩니다.",
                    
                };
            }
        }
        public override byte Sprite { get { return (int)4; } }
        public override byte aSpeed { get { return 4; } }
        public override byte aFrames { get { return 7; } }

        public Aron(Serial serial, int Channel, string Map, Point2D Loc)
            : base(serial)
        {
            base.m_Channel = Channel;
            base.m_Map = Map;
            base.m_Loc = new Point2D(Loc.X, Loc.Y);
        }

        public override void Event_Message(Player player, string[] str2, string str)
        {
            #region 시켜줘
            if ((str2[0] == "기사" || str2[0] == "검객" || str2[0] == "위저드" || str2[0] == "샤먼" || str2[0] == "승급") && str2[1] == "시켜줘")
            {
                this.Event_Chat("버르장머리가 없구나", true);
            }
            #endregion

            #region 시켜주세요
            else if ((str2[0] == "기사" || str2[0] == "검객" || str2[0] == "위저드" || str2[0] == "샤먼" || str2[0] == "승급") && str2[1] == "시켜주세요")
            {
                if (str2[0] == "승급")
                {
                    if (player.Promo >= 6)
                    {
                        this.Event_Chat("작은 마력시약을 나에게 건내주게나..", true);
                        player.client.AronStage = 3;
                    }
                    else if (player.Level >= 80)
                    {
                        this.Event_Chat("작은 체력시약을 나에게 건내주게나..", true);
                        player.client.AronStage = 4;
                    }
                    else
                        this.Event_Chat("아직 수련이 부족하구나..", true);
                }
                else
                {
                    if (player.Level >= 5 && player.Promo == 0)
                    {
                        if (player.GetFreeSlots() >= player.Equipped2.Count)
                        {
                            foreach (var it in player.Equipped2.Values)
                            {
                                it.Unequip(player, it.InvSlot);
                            }
                            switch (str2[0])
                            {
                                case "기사":
                                    player.Class = LKCamelot.library.Class.Knight;
                                    break;
                                case "검객":
                                    player.Class = LKCamelot.library.Class.Swordsman;
                                    break;
                                case "위저드":
                                    player.Class = LKCamelot.library.Class.Wizard;
                                    break;
                                case "샤먼":
                                    player.Class = LKCamelot.library.Class.Shaman;
                                    break;
                                default:
                                    return;
                            }
                            this.Event_Chat(string.Format("넌 이제 {0} 되었다.", str2[0]), true);
                        }
                        else
                            this.Event_Chat(string.Format("짐이 많아보이는구나."), true);
                    }
                    else
                    {
                        this.Event_Chat("아직 수련이 부족하구나..", true);
                    }

                }
            }
            #endregion

            #region 예
            else if (str == "예")
            {
                if (player.Class == library.Class.BlackSmith || player.Class == library.Class.Miner)
                    return;

                if (
                    (player.Level >= 80 && player.Level <= 99) || 
                    ( (player.Level >= (90 + (20 * player.Promo)) && (player.Level <= (100 + (20 * player.Promo)))) )
                    )
                {
                    if (player.Level >= Settings.MaxLevel)
                        return;

                    if (player.client.AronStage == 1)
                    {
                        player.client.AronStage = 0;
                        if (player.Promo == 0)
                        {
                            player.XP = 0;
                            player.Level = 101;
                            player.Extra += 30;
                        }
                        else if (player.Promo >= 1)
                        {
                            player.XP = 0;
                            if (player.Promo == 1) player.Extra += 50;
                            if (player.Promo == 2) player.Extra += 80;
                            if (player.Promo == 3) player.Extra += 120;
                            if (player.Promo == 4) player.Extra += 180;
                            if (player.Promo == 5) player.Extra += 260;
                            if (player.Promo == 6) player.Extra += 360;

                            if (player.Promo == 7) player.Extra += 480;
                            if (player.Promo == 8) player.Extra += 620;
                            if (player.Promo == 9) player.Extra += 780;
                            if (player.Promo == 10) player.Extra += 960;
                            if (player.Promo == 11) player.Extra += 1160;

                            if (player.Promo == 6 && player.GetFreeSlots() > 0)
                            {
                                script.item.Item prize = null;
                                if (player.Class == LKCamelot.library.Class.Knight)
                                    prize = new script.item.Excalibur().Inventory(player);
                                if (player.Class == LKCamelot.library.Class.Swordsman)
                                    prize = new script.item.DaeungDaegum().Inventory(player);
                                if (player.Class == LKCamelot.library.Class.Wizard)
                                    prize = new script.item.Kassandra().Inventory(player);
                                if (player.Class == LKCamelot.library.Class.Shaman)
                                    prize = new script.item.TaegkFan().Inventory(player);
                                if (player.Class == library.Class.Beginner)
                                    prize = new script.item.GoldVest().Inventory(player);
                                try
                                {
                                    World.NewItems.TryAdd(prize.m_Serial, prize);
                                    player.client.SendPacket(new AddItemToInventory2(prize).Compile());
                                }
                                catch { Console.WriteLine(player.Name + " : failed to add promo item"); }
                            }

                            player.Level = (short)((100 + (20 * player.Promo)) + 1);

                            if (Settings.PromoAlarm)
                            {
                                string message = string.Format("[시스템] {0}님께서 {1}승이 되었습니다.", player.Name, player.Promo);
                                World.SendToAll(new QueDele("all", new UpdateChatBox(0x80, 0x03, 384, message).Compile()));
                            }
                        }

                        this.Event_Chat("넌 이제 승급이 되었다.", true);
                        player.client.SendPacket(new SetLevelGold(player).Compile());
                        player.client.SendPacket(new UpdateCharStats(player).Compile());
                    }
                }

                else
                {
                    this.Event_Chat("아직 수련이 부족하구나..", true);
                    player.client.AronStage = 0;
                }
            }
            #endregion

            #region 올려주세요
            else if ( (str2[0] == "힘" || str2[0] == "지력" || str2[0] == "숙련" || str2[0] == "생명") && str2[1] == "올려주세요")
            {
                if (Settings.MaxLevel != player.Level || (Settings.MaxLevel == player.Level && player.XP < Settings.TrunXP))
                {
                    this.Event_Chat("아직 수련이 부족하구나..", true);
                    return;
                }

                player.m_XP -= Settings.TrunXP;
                io.IOClient.WriteTurnLog(DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss]: ") + "["+str2[0] +"] " + player.Name);
                switch (str2[0])
                {
                    case "힘":
                        player.b_Str += 1;
                        break;
                    case "지력":
                        player.b_Men += 1;
                        break;
                    case "숙련":
                        player.b_Dex += 1;
                        break;
                    case "생명":
                        player.b_Vit += 1;
                        break;
                }

                player.client.SendPacket(new XPChange(player.m_XP).Compile());
                player.client.SendPacket(new UpdateCharStats(player).Compile());
                this.Event_Chat(string.Format("{0}속성이 올랐다네", str2[0]), true);
            }
            #endregion

        }
        public override void Event_Give(Player player, item.Item giveitem, int amount = 0)
        {
            if (giveitem.GetType() == typeof(script.item.PromoteLifeDrug) && player.client.AronStage == 4)
            {
                player.client.AronStage = 1;
                giveitem.Delete(player);
                this.Event_Chat("이제 승급해도 되겠느냐?", true);
            }
            else if (giveitem.GetType() == typeof(script.item.PromoteMagicDrug) && player.client.AronStage == 3)
            {
                player.client.AronStage = 1;
                giveitem.Delete(player);
                this.Event_Chat("이제 승급해도 되겠느냐?", true);
            }
        }


    }
}
