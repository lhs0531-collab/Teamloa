using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class FireProtector : Spell
    {
        public override string Name { get { return "FIRE PROTECTOR"; } }
        public override int SpellLearnedIcon { get { return 15; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Casted; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return -26; } }
        public override int ManaCostPl { get { return -2; } }
        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                //tbuff.Time = 35 * Settings.Second;
                //tbuff.TimePI = 5 * Settings.Second;

                tbuff.Time = 35 * Settings.Second;
                tbuff.TimePI = 10 * Settings.Second;

                tbuff.Dam = 66;
                tbuff.Dampl = 6;
                tbuff.fAC = 0.26f;
                tbuff.fACpl = 0.01f;
                return tbuff;
            }
        }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    70,   //oncast
                    0,  //moving
                    0,   //impact
                    0,   //thickness
                    0,   //type
                    1,   //speed
                    0    //streak
                    );
            }
        }

        public FireProtector()
            : base(new script.item.FireProtectorBook(1))
        {
        }

        public override void UseSpell(model.Player player)
        {
            player.AddBuff(this);
        }
    }
}
