using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class GuardianSword : Spell
    {
        public override string Name { get { return "GUARDIAN SWORD"; } }
        public override int SpellLearnedIcon { get { return 31; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Casted; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return -25; } }
        public override int ManaCostPl { get { return 0; } }
        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                tbuff.fAC = 0.20f;
                tbuff.fACpl = 0.01f;
                return tbuff;
            }
        }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    3,   //oncast
                    0,  //moving
                    0,   //impact
                    0,   //thickness
                    0,   //type
                    3,   //speed
                    0    //streak
                    );
            }
        }

        public GuardianSword()
            : base(new script.item.GuardianSwordBook(1))
        {
        }

        public override void UseSpell(model.Player player)
        {
            player.AddBuff(this);
        }
    }
}
