using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class ThunderCross : Spell
    {
        public override string Name { get { return "THUNDER CROSS"; } }
        public override int SpellLearnedIcon { get { return 24; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Casted; } }

        public override int DamBase { get { return 136; } }
        public override int DamPl { get { return 11; } }
        public override int ManaCost { get { return 105; } }
        public override int ManaCostPl { get { return 0; } }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    46,   //oncast
                    32,  //moving
                    18,   //impact
                    0,   //thickness
                    1,   //type
                    8,   //speed
                    0    //streak
                    );
            }
        }

        public ThunderCross()
            : base(new script.item.ThunderCrossBook(1))
        {
        }

        public override void UseSpell(model.Player player)
        {
            List<BaseObject> Target = this.RangeTarget(player, (short)player.Loc.X, (short)player.Loc.Y);
            if (Target == null) return;

            foreach (var obj in Target)
            {
                if (obj is script.monster.Monster)
                    (obj as script.monster.Monster).TakeDamage(player, this);
                else if (obj is model.Player)
                    (obj as model.Player).TakeDamage(player, this);

            }
            World.SendToSoundRange(new QueDele(player.m_Channel, player.Map, new EffectSound(library.EffectSound.ThunderCross).Compile()), player);
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new CurveMagic(player.m_Serial, (short)player.Loc.X, (short)player.Loc.Y, Seq).Compile()));
        }

    }
}
