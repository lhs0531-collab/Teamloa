using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class FireBall : Spell
    {
        public override string Name { get { return "FIRE BALL"; } }
        public override int SpellLearnedIcon { get { return 12; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 33; } }
        public override int DamPl { get { return 8; } }
        public override int ManaCost { get { return 32; } }
        public override int ManaCostPl { get { return 0; } }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    40,   //oncast
                    23,  //moving
                    15,   //impact
                    0,   //thickness
                    0,   //type
                    16,   //speed
                    0    //streak
                    );
            }
        }

        public FireBall()
            : base(new script.item.FireBallBook(1))
        {
        }
        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            if (castonid != 0)
            {
                var Target = SerialTarget(player.m_Channel, castonid, player.PK);
                if (Target == null) return;

                if (Target is script.monster.Monster)
                    (Target as script.monster.Monster).TakeDamage(player, this);
                else if (Target is Player)
                    (Target as Player).TakeDamage(player, this);
            }
            World.SendToSoundRange(new QueDele(player.m_Channel, player.Map, new EffectSound(library.EffectSound.FireBall).Compile()), player);
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new CurveMagic(player.m_Serial, castx, casty, Seq).Compile()));
        }
    }
}
