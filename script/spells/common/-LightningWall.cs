using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public class LightningWall : Spell, Wall
    {
        public override string Name { get { return "LIGHTNING WALL"; } }
        public override int SpellLearnedIcon { get { return 57; } }
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
                    45,  //impact
                    3,  //thickness
                    0,  //type
                    2,  //speed
                    0  //streak
                    );
            }
        }

        public LightningWall()
            : base(new script.item.LightningWallBook(1))
        {
        }
        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
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

                player.client.castHandler.CreateMagicEffect(m_buff.Loc, player.m_Channel, player.Map, (short)((player.Face + 2) % 8), 0x2D, time, 3);
                WallTrap.Register(player, m_buff, time);
            }
        }
    }
}