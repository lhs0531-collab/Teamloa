using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class Sidewinder : Spell
    {
        public override string Name { get { return "SIDEWINDER"; } }
        public override int SpellLearnedIcon { get { return 41; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 300; } }
        public override int DamPl { get { return 12; } }
        public override int ManaCost { get { return -68; } }
        public override int ManaCostPl { get { return 4; } }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    8,   //oncast
                    37,  //moving
                    0,   //impact
                    0,   //thickness
                    0,   //type
                    9,   //speed
                    0    //streak
                    );
            }
        }

        public Sidewinder()
            : base(new script.item.SidewinderBook(1))
        {
        }

        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            if (castonid != 0)
            {
                var Target = SerialTarget(player.m_Channel, castonid, player.PK);

                if (Target is script.monster.Monster)
                    (Target as script.monster.Monster).TakeDamage(player, this);
                else if (Target is Player)
                    (Target as Player).TakeDamage(player, this);
            }
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new CurveMagic(player.m_Serial, castx, casty, Seq).Compile()));
        }
    }
}
