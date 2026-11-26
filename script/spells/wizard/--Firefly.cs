using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class Firefly : Spell
    {
        public override string Name { get { return "FIREFLY"; } }
        public override int SpellLearnedIcon { get { return 71; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return -77; } }
        public override int ManaCostPl { get { return 6; } }
        public override int menCoff { get { return 10; } }

        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                return tbuff;
            }
        }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    0,  //oncast
                    0,  //moving
                    8,  //impact
                    0,  //thickness
                    3,  //type
                    30,  //speed
                    0  //streak
                    );
            }
        }

        public Firefly() : base(new script.item.FireflyBook(1))
        {
        }
        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            var Target = SerialPlayerTarget(player.m_Channel, castonid, castx, casty);

            if (Target != null)
            {
                Target.AddBuff(this);
                World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicEffect((Target as Player).m_Serial, Seq.OnImpactSprite).Compile()));
            }
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));
        }
    }
}