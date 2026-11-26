using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public class UltraBigBang : Spell
    {
        public override string Name { get { return "ULTRA BIG BANG"; } }
        public override int SpellLearnedIcon { get { return 64; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 902; } }
        public override int DamPl { get { return 20; } }
        public override int ManaCost { get { return -65; } }
        public override int ManaCostPl { get { return 5; } }
        public override int menCoff { get { return 12; } }
        public override int Range { get { return 9; } }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    0,  //oncast
                    0,  //moving
                    65,  //impact
                    0,  //thickness
                    3,  //type
                    3,  //speed
                    0  //streak
                    );
            }
        }

        public UltraBigBang()
            : base(new script.item.UltraBigBangBook(1))
        {
        }

        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            var Entity = RangeTarget(player, player.Loc.X, player.Loc.Y);

            foreach (var Target in Entity)
            {
                if (Target is script.monster.Monster)
                {
                    (Target as script.monster.Monster).TakeDamage(player, this, castx, casty);
                    World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicEffect((Target as script.monster.Monster).m_Serial, Seq.OnImpactSprite).Compile()));
                }
                else if (Target is Player)
                {
                    (Target as Player).TakeDamage(player, this, castx, casty);
                    World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicEffect((Target as Player).m_Serial, Seq.OnImpactSprite).Compile()));
                }
            }
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));
        }
    }
}