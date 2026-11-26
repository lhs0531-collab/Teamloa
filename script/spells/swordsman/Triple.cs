using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class Triple : Spell
    {
        public override string Name { get { return "TRIPLE"; } }
        public override int SpellLearnedIcon { get { return 74; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Casted; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return -50; } }
        public override int ManaCostPl { get { return 0; } }

        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                tbuff.BuffType = BuffCase.Triple;
                return tbuff;
            }
        }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    0,   //oncast
                    0,  //moving
                    0,   //impact
                    0,   //thickness
                    3,   //type
                    3,   //speed
                    0    //streak
                    );
            }
        }

        public Triple()
            : base(new script.item.TripleBook(1))
        {
        }
        public override void UseSpell(model.Player player)
        {
            player.AddBuff(this);
        }
    }
}
