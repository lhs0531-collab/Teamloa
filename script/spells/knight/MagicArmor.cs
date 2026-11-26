using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class MagicArmor : Spell
    {
        public override string Name { get { return "MAGIC ARMOR"; } }
        public override int SpellLearnedIcon { get { return 35; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Casted; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return -20; } }
        public override int ManaCostPl { get { return 0; } }
        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                //tbuff.Time = 32 * Settings.Second;
                //tbuff.TimePI = 3 * Settings.Second;
                tbuff.fAC = 0.16f;
                tbuff.fACpl = 0.01f;
                return tbuff;
            }
        }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    57,   //oncast
                    0,  //moving
                    0,   //impact
                    0,   //thickness
                    0,   //type
                    1,   //speed
                    0    //streak
                    );
            }
        }

        public MagicArmor()
            : base(new script.item.MagicArmorBook(1))
        {
        }

        public override void UseSpell(model.Player player)
        {
            player.AddBuff(this);
        }
    }
}
