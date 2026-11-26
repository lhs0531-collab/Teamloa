using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class Alias : BaseNPC
    {
        public override string Name { get { return "알리아스"; } }
        public override List<string> ChatString
        {
            get
            {
                return new List<string>()
                {
                    "안녕하세요.",
                    "언제나 길을 깨끗히 청소하고 있습니다.",
                    "청소하다가 체력시약을 주웠는데, 드릴까요?",
                    "아이템을 흘리지 말고 다니게나~",
                };
            }
        }

        public override byte Sprite { get { return (int)5; } }
        public override byte aSpeed { get { return 4; } }
        public override byte aFrames { get { return 7; } }
        
        public Alias(Serial serial, int Channel, string Map, Point2D Loc)
            : base(serial)
        {
            base.m_Channel = Channel;
            base.m_Map = Map;
            base.m_Loc = new Point2D(Loc.X, Loc.Y);
        }

        public override void Event_Message(Player player, string[] str2, string str)
        {
            if ( (str2[0] == "체력시약" || str2[0] == "마력시약") && str2[1] == "주세요")
            {
                if (str2[0] == "체력시약")
                    player.client.AliasStage = 1;
                else
                    player.client.AliasStage = 2;


                Event_Chat("청소하다가 주은게 있는데, 드릴까요?" , true);
            }
            else if (str == "예")
            {
                if ( (player.client.AliasStage == 1 || player.client.AliasStage ==2 ) && player.GetFreeSlots() >= 1)            //체력시약 주세요
                {
                    script.item.Item newitem = null;
                    if (player.client.AliasStage == 1 && player.Inventory.Where(xe => xe.Name == "PROMOTE LIFE DRUG").FirstOrDefault() == null)
                        newitem = new script.item.PromoteLifeDrug().Inventory(player);
                    else if (player.client.AliasStage == 2 && player.Inventory.Where(xe => xe.Name == "PROMOTE LIFE DRUG").FirstOrDefault() == null)
                        newitem = new script.item.PromoteMagicDrug().Inventory(player);
                    else
                    {
                        player.client.AliasStage = 0;
                        Event_Chat("짐이 많아 보이는군.", true);
                        return;
                    }
                    player.client.AliasStage = 0;
                    Event_Chat("유용하게 사용하세요.", true);
                }
            }
        }

    }
}
