using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class View : Spell
    {
        public override string Name { get { return "VIEW"; } }
        public override int SpellLearnedIcon { get { return 7; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return 16; } }
        public override int ManaCostPl { get { return 0; } }
        public override int Limit { get { return 1; } }

        public View()
            : base(new script.item.ViewBook(1))
        {

        }

        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            if (castonid != 0)
            {
                var Target = SerialTarget(player.m_Channel, castonid, true);

                if (Target is script.monster.BaseNode)
                {
                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, (Target as script.monster.BaseNode).Name).Compile());
                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("레벨 : {0}", (Target as script.monster.BaseNode).Level)).Compile());
                }
                else if (Target is script.monster.Monster)
                {
                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, (Target as script.monster.Monster).Name).Compile());
                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("방어력 : {0}", (Target as script.monster.Monster).AC)).Compile());
                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("적중률 : {0}", (Target as script.monster.Monster).Hit)).Compile());
                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("파괴력 : {0}", (Target as script.monster.Monster).Dam)).Compile());

                    //기타 속성 (응고 저항력)
                }
                else if (Target is Player)
                {
                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("{0} :{1}", (Target as Player).Name, (Target as Player).Guild == null ? "" : (Target as Player).Guild.Name)).Compile());
                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("직업 : {0}", (Target as Player).FullClass)).Compile());

                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("투구 : {0}", (Target as Player).EquippedSlotString(25) )).Compile());
                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("어뮬 : {0}", (Target as Player).EquippedSlotString(26))).Compile());
                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("무기 : {0}", (Target as Player).EquippedSlotString(27))).Compile());
                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("갑옷 : {0}", (Target as Player).EquippedSlotString(28))).Compile());
                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("방패 : {0}", (Target as Player).EquippedSlotString(29))).Compile());
                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("반지 : {0}", (Target as Player).EquippedSlotString(30))).Compile());
                    player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("PK : {0}", (Target as Player).PKCount)).Compile());
                }
            }
            else
            {
                player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0} 마법이 실패하였습니다.", this.Name)).Compile());
            }
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));
        }

    }

}
