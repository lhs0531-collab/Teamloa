using LKCamelot.model;
using System;
namespace LKCamelot.script.spells
{
    public class FlameWave : Spell
    {
        public override string Name { get { return "FLAME WAVE"; } } 
        public override int SpellLearnedIcon { get { return 16; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 58; } }
        public override int DamPl { get { return 6; } }
        public override int ManaCost { get { return 38; } }
        public override int ManaCostPl { get { return 6; } }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    40,  //oncast
                    52,  //moving
                    0,  //impact
                    3,  //thickness
                    0,  //type
                    16,  //speed
                    2  //streak
                    );
            }
        }

        public FlameWave()
            : base(new script.item.FlameRoundBook(1))
        {
        }

        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            var Target = TargetDistance(player, 3, World.Dist2d(player.Loc.X, player.Loc.Y, castx, casty), player.PK);

            foreach (var obj in Target)
            {
                if (obj is script.monster.Monster)
                    (obj as script.monster.Monster).TakeDamage(player, this);
                else if (obj is model.Player)
                    (obj as model.Player).TakeDamage(player, this);
            }

            World.SendToSoundRange(new QueDele(player.m_Channel, player.Map, new EffectSound(library.EffectSound.FireBall).Compile()), player);
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new CurveMagic(player.m_Serial, castx, casty, Seq).Compile()));
        }
    }
}