using LKCamelot.model;
namespace LKCamelot.script.spells
{
    /// <summary>
    /// 얼음벽. 전기벽과 같은 로직(벽 위 몬스터에게 1초마다 피해, 몬스터 유인/가두기)이며 효과와 아이콘만 다르다.
    /// </summary>
    public class IceWall : Spell, Wall
    {
        public const byte EffectSprite = 72;

        public override string Name { get { return "ICE WALL"; } }
        public override int SpellLearnedIcon { get { return 44; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 152; } }
        public override int DamPl { get { return 8; } }
        public override int ManaCost { get { return 102; } }
        public override int ManaCostPl { get { return 0; } }
        public override int menCoff { get { return 6; } }

        Buff m_buff;
        public override Buff BuffEffect
        {
            get
            {
                return m_buff;
            }
        }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    0,  //oncast
                    0,  //moving
                    EffectSprite,  //impact
                    3,  //thickness
                    0,  //type
                    2,  //speed
                    0  //streak
                    );
            }
        }

        public IceWall()
            : base(new script.item.IceWallBook(1))
        {
        }
        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            // 벽이 남아 있는 동안 다시 쓰면 이전 벽을 지우고 새 위치에 다시 깐다.
            WallTrap.Clear(player, this);

            m_buff = new Buff();
            m_buff.BuffType = BuffCase.Wall;

            m_buff.Time = 30 * Settings.Second;
            m_buff.TimePI = 5 * Settings.Second;
            m_buff.Loc = new Point2D(castx, casty);
            m_buff.Map = player.Map;
            m_buff.Dir = player.Face;

            if (player.AddBuff(this))
            {
                int time = BuffEffect.Time + ((Level - 1) * BuffEffect.TimePI);

                int effect = player.client.castHandler.CreateMagicEffect(m_buff.Loc, player.m_Channel, player.Map, (short)((player.Face + 2) % 8), EffectSprite, time, 3);
                WallTrap.Register(player, this, m_buff, time, effect);
            }
        }
    }
}
