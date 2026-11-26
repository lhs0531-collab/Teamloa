using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class FlameRound : Spell
    {
        public override string Name { get { return "FLAME ROUND"; } }
        public override int SpellLearnedIcon { get { return 18; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Casted; } }

        public override int DamBase { get { return 108; } }
        public override int DamPl { get { return 11; } }
        public override int ManaCost { get { return 54; } }
        public override int ManaCostPl { get { return 0; } }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    40,   //oncast
                    25,  //moving
                    15,   //impact
                    0,   //thickness
                    1,   //type
                    8,   //speed
                    0    //streak
                    );
            }
        }

        public FlameRound()
            : base(new script.item.FlameRoundBook(1))
        {
        }

        public override void UseSpell(model.Player player)
        {
            IEnumerable<Object> Target = this.RangeTarget(player, (short) player.Loc.X, (short)player.Loc.Y);
            if (Target == null) return;

            foreach (var obj in Target)
            {
                if (obj is script.monster.Monster)
                    (obj as script.monster.Monster).TakeDamage(player, this);
                else if (obj is model.Player)
                    (obj as model.Player).TakeDamage(player, this);

            }
            World.SendToSoundRange(new QueDele(player.m_Channel, player.Map, new EffectSound(library.EffectSound.FrameRound2).Compile()), player);
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new CurveMagic(player.m_Serial, (short)player.Loc.X, (short)player.Loc.Y, Seq).Compile()));
        }

    }
}
