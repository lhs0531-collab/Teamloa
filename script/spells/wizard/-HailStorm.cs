using LKCamelot.model;
using System.Collections.Generic;
namespace LKCamelot.script.spells
{
    public class HailStorm : Spell
    {
        public override string Name { get { return "HAIL STORM"; } }
        public override int SpellLearnedIcon { get { return 40; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Casted; } }

        public override int DamBase { get { return 76; } }
        public override int DamPl { get { return 9; } }
        public override int ManaCost { get { return 54; } }
        public override int ManaCostPl { get { return 0; } }

        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                tbuff.BuffType = BuffCase.Slow;
                tbuff.MoveSlowPercent = Settings.HailStormSlowPercent;
                tbuff.Time = 180 * Settings.Second;
                tbuff.TimePI = 10;
                tbuff.Hit = 20;
                tbuff.Hitpl = 2;
                return tbuff;
            }
        }


        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    46 ,   //oncast
                    11,  //moving
                    11,   //impact
                    0,   //thickness
                    1,   //type
                    6,   //speed
                    0    //streak
                    );
            }
        }

        public HailStorm()
            : base(new script.item.HailStormBook(1))
        {

        }

        public override void UseSpell(model.Player player)
        {
            IEnumerable<BaseObject> Target = this.RangeTarget(player, (short)player.Loc.X, (short)player.Loc.Y);
            if (Target == null) return;

            foreach (var obj in Target)
            {
                if (obj is script.monster.Monster)
                {
                    var mob = obj as script.monster.Monster;
                    mob.TakeDamage(player, this);
                    // 피해로 죽지 않은 몬스터에게만 감속을 건다. 다시 맞으면 지속시간이 갱신된다.
                    if (mob.Alive)
                        mob.ApplySlow(this);
                }
                //else if (obj is model.Player)
                //{
                //    (obj as model.Player).AddBuff(this);
                //    (obj as model.Player).TakeDamage(player, this);
                //}

            }
            World.SendToSoundRange(new QueDele(player.m_Channel, player.Map, new EffectSound(library.EffectSound.FrameRound2).Compile()), player);
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new CurveMagic(player.m_Serial, (short)player.Loc.X, (short)player.Loc.Y, Seq).Compile()));
        }
    }
}