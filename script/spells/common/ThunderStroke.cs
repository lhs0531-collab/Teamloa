using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class ThunderStroke : Spell
    {
        public override string Name { get { return "THUNDER STROKE"; } } 
        public override int SpellLearnedIcon { get { return 22; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 206; } }
        public override int DamPl { get { return 8; } }
        public override int ManaCost { get { return 150; } }
        public override int ManaCostPl { get { return 0; } }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    12,  //oncast
                    45,  //moving
                    0,  //impact
                    3,  //thickness
                    0,  //type
                    16,  //speed
                    4  //streak (시전 방향으로 이어지는 줄 수)
                    );
            }
        }

        public ThunderStroke()
            : base(new script.item.ThunderStrokeBook(1))
        {
        }

        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            // 누른 곳에서 끊기지 않고 그 방향으로 끝까지 뻗는다(효과와 피해 범위 모두).
            var end = ExtendCast(player, castx, casty);
            castx = (short)end.X;
            casty = (short)end.Y;

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

