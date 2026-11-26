using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class Nosebleed : Spell
    {
        public override string Name { get { return "NOSEBLEED"; } }
        public override int SpellLearnedIcon { get { return 15; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Casted; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return -26; } }
        public override int ManaCostPl { get { return -2; } }

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






        public Nosebleed()
            : base(new script.item.NosebleedBook(1))
        {
        }
        public override void UseSpell(model.Player player)
        {
            player.m_Buffs.Clear();
        }

    }
}