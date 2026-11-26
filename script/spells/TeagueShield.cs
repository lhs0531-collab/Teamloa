using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class TeagueShield : Spell
    {
        public override string Name { get { return "TEAGUE SHIELD"; } }
        public override int SpellLearnedIcon { get { return 28; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Casted; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return 66; } }
        public override int ManaCostPl { get { return 0; } }
        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                tbuff.BuffType = BuffCase.ManaAsHP | BuffCase.ReflectDamage;
                //tbuff.Time = 180 * Settings.Minute;
                //tbuff.Dam = 35;
                //tbuff.Dampl = 5;
                tbuff.AC = 8;
                tbuff.ACpl = 4;
                return tbuff;
            }
        }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    6,   //oncast
                    0,  //moving
                    0,   //impact
                    0,   //thickness
                    0,   //type
                    3,   //speed
                    0    //streak
                    );
            }
        }

        public TeagueShield()
            : base(new script.item.TeagueShieldBook(1))
        {
        }
        public override void UseSpell(model.Player player)
        {
            player.AddBuff(this);
        }
    }
}
