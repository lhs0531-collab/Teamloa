using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class RainbowArmor : Spell
    {
        public override string Name { get { return "RAINBOW ARMOR"; } }
        public override int SpellLearnedIcon { get { return 30; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Casted; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return -28; } }
        public override int ManaCostPl { get { return -2; } }

        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                //tbuff.Time = 25 * Settings.Second;
                //tbuff.TimePI = 5 * Settings.Second;

                tbuff.Time = 25 * Settings.Second;
                tbuff.TimePI = 10 * Settings.Second;

                if (Level > 8)
                    tbuff.fAC = 0.44f;
                else if (Level > 4)
                    tbuff.fAC = 0.40f;
                else
                    tbuff.fAC = 0.30f;

                return tbuff;
            }
        }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    73,   //oncast
                    0,  //moving
                    0,   //impact
                    0,   //thickness
                    0,   //type
                    1,   //speed
                    0    //streak
                    );
            }
        }

        public RainbowArmor()
            : base(new script.item.RainbowArmorBook(1))
        {
        }
        public override void UseSpell(model.Player player)
        {
            player.AddBuff(this);
        }
    }
}
