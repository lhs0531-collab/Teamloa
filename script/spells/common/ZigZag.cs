using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class ZigZag : Spell
    {
        public override string Name { get { return "ZIG ZAG"; } }
        public override int SpellLearnedIcon { get { return 21; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 22; } }
        public override int DamPl { get { return 4; } }
        public override int ManaCost { get { return 16; } }
        public override int ManaCostPl { get { return 0; } }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    0,   //oncast
                    54,  //moving
                    31,   //impact
                    0,   //thickness
                    0,   //type
                    13,   //speed
                    0    //streak
                    );
            }
        }

        public ZigZag()
            : base(new script.item.ZigZagBook(1))
        {
        }

        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            if (castonid != 0)
            {
                var Target = SerialTarget(player.m_Channel, castonid, player.PK);
                if (Target == null) return;

                if (Target is script.monster.Monster)
                    (Target as script.monster.Monster).TakeDamage(player, this);
                else if (Target is Player)
                    (Target as Player).TakeDamage(player, this);
            }
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new CurveMagic(player.m_Serial, castx, casty, Seq).Compile()));
        }
    }
}
