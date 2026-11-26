using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class Transparency : Spell
    {
        public override string Name { get { return "TRANSPARENCY"; } }
        public override int SpellLearnedIcon { get { return 41; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Casted; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return -68; } }
        public override int ManaCostPl { get { return 4; } }
        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                tbuff.Time = 180 * Settings.Second;
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
                    0,   //type
                    0,   //speed
                    0    //streak
                    );
            }
        }

        public Transparency()
            : base(new script.item.TransparencyBook(1))
        {
        }

        public override void UseSpell(model.Player player)
        {
        }
    }
}
