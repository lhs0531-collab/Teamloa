using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;

namespace LKCamelot.script.spells
{
    /// <summary>
    /// 단일 대상 얼음 마법. 피해 없이 몬스터의 이동속도를 줄인다. 최대 12권(Limit 기본값).
    /// 유저의 걷기 속도는 서버가 검증하지 않으므로 유저에게는 걸리지 않는다.
    /// </summary>
    public class IceBag : Spell
    {
        public override string Name { get { return "ICE BAG"; } }
        public override int SpellLearnedIcon { get { return 40; } }      // 헤일스톰과 같은 아이콘
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return 40; } }
        public override int ManaCostPl { get { return 0; } }

        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                tbuff.BuffType = BuffCase.Slow;
                tbuff.MoveSlowPercent = Settings.IceBagSlowPercent;
                tbuff.Time = Settings.IceBagSlowTime;
                tbuff.TimePI = Settings.IceBagSlowTimePl;
                return tbuff;
            }
        }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    46,   //oncast  (감속 중인 몬스터 위에 표시되는 효과. 헤일스톰과 동일)
                    79,   //moving  (IceOrLightComet4f)
                    72,   //impact  (RisingIce48)
                    0,    //thickness
                    0,    //type
                    16,   //speed
                    0     //streak
                    );
            }
        }

        public IceBag()
            : base(new script.item.IceBagBook(1))
        {
        }

        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            if (castonid != 0)
            {
                // 몬스터만 대상으로 한다. 피해가 0이라 유저에게는 줄 효과가 없다.
                var mob = SerialTarget(player.m_Channel, castonid, false) as script.monster.Monster;
                if (mob == null || mob.m_Map != player.Map) return;

                // 피해는 0이지만 몬스터가 시전자를 대상으로 잡도록 피해 처리를 거친다.
                mob.TakeDamage(player, this);
                // 다시 맞으면 지속시간이 갱신된다.
                if (mob.Alive)
                    mob.ApplySlow(this);
            }
            World.SendToSoundRange(new QueDele(player.m_Channel, player.Map, new model.EffectSound(library.EffectSound.FrameRound2).Compile()), player);
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new model.CurveMagic(player.m_Serial, castx, casty, Seq).Compile()));
        }
    }
}
