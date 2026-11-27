using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class PlusHeal : Spell
    {
        public override string Name { get { return "PLUS HEAL"; } }
        public override int SpellLearnedIcon { get { return 2; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return 118; } }
        public override int ManaCostPl { get { return 0; } }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    0,   //oncast
                    1,  //moving
                    0,   //impact
                    0,   //thickness
                    0,   //type
                    6,   //speed
                    3    //streak
                    );
            }
        }

        public PlusHeal()
            : base(new script.item.PlusHealBook(1))
        {
        }
        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            if (castonid != 0)
            {
                var target = this.SerialPlayerTarget(player.m_Channel, castonid);
                if (target == null) return;

                if (target.m_Serial != player.m_Serial)
                {
                    double per = target.HP * ((double)(60 + ((this.Level - 1) * 2)) / 100);

                    if (target.HPCur + per > target.HP)
                        target.HPCur = target.HP;
                    else
                        target.HPCur += (int)per;

                    World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicEffect(target.m_Serial, new Heal().Seq.OnCastSprite).Compile()));
                }
            }
            else
            {
                player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0} 마법이 실패하였습니다.", this.Name)).Compile());
            }
            World.SendToSoundRange(new QueDele(player.m_Channel, player.Map, new EffectSound(library.EffectSound.Heal).Compile()), player);
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));      
        }

    }
}
