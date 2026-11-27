using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    /// <summary>
    /// 단일 대상 지속 피해 마법. 걸린 몬스터는 지속시간 동안 1초마다 피해를 받는다. 최대 12권(Limit 기본값).
    /// 몬스터와 유저 모두에게 걸린다. 유저는 다른 공격 마법과 같은 PK 조건을 따른다.
    /// </summary>
    public class Shoke :Spell
    {
        public override string Name { get { return "SHOKE"; } }
        public override int SpellLearnedIcon { get { return 26; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 30; } }
        public override int DamPl { get { return 5; } }
        public override int ManaCost { get { return 40; } }
        public override int ManaCostPl { get { return 0; } }

        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                tbuff.TickInterval = Settings.ShokeTickInterval;
                tbuff.Time = Settings.ShokeTime;
                tbuff.TimePI = Settings.ShokeTimePl;
                return tbuff;
            }
        }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    Settings.ShokeEffectSprite,   //oncast (걸린 몬스터에 표시되는 효과)
                    0,  //moving
                    0,  //impact
                    0,  //thickness
                    0,  //type
                    0,  //speed
                    0   //streak
                    );
            }
        }

        public Shoke()
            : base(new script.item.ShokeBook(1))
        {
        }

        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            if (castonid != 0)
            {
                var target = SerialTarget(player.m_Channel, castonid, player.PK);

                if (target is script.monster.Monster)
                {
                    var mob = target as script.monster.Monster;
                    if (mob.m_Map == player.Map)
                    {
                        // 시전 즉시 한 번 피해를 주고(몬스터가 시전자를 대상으로 잡는다), 이후 주기마다 피해가 들어간다.
                        mob.TakeDamage(player, this);
                        if (mob.Alive)
                            mob.ApplyDot(this);
                    }
                }
                else if (target is model.Player)
                {
                    // 유저에게는 다른 공격 마법과 같은 조건(PK 가능 맵, 시전자 대인공격 ON, 보호 레벨 초과, 같은 맵)에서만 걸린다.
                    var victim = target as model.Player;
                    if (victim.loggedIn && victim.CanTakeSpellFrom(player))
                    {
                        victim.TakeDamage(player, this);

                        // 이미 걸려 있으면 지우고 다시 걸어 지속시간을 갱신한다.
                        foreach (var old in victim.m_Buffs.Where(xe => xe.Value.Name == this.Name).ToList())
                        {
                            Spell removed;
                            victim.m_Buffs.TryRemove(old.Key, out removed);
                        }
                        victim.AddBuff(this);
                    }
                }
            }
            model.World.SendToSoundRange(new model.QueDele(player.m_Channel, player.Map, new model.EffectSound(library.EffectSound.FrameRound2).Compile()), player);
            model.World.SendToAll(new model.QueDele(player.m_Channel, player.Map, new model.MagicMotion(player).Compile()));
        }
    }
}
