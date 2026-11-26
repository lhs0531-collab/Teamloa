using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public class BlackHand : Spell, ISingle
    {
        public override string Name { get { return "BLACK HAND"; } }
        public override int SpellLearnedIcon { get { return 70; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 500; } }
        public override int DamPl { get { return 00; } }
        public override int ManaCost { get { return -78; } }
        public override int ManaCostPl { get { return 6; } }
        public override int Range { get { return 1; } }
        public override int menCoff { get { return 10; } }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    0,  //oncast
                    0,  //moving
                    51,  //impact
                    0,  //thickness
                    3,  //type
                    0,  //speed
                    0  //streak
                    );
            }
        }

        public BlackHand()
            : base(new script.item.BlackHandBook(1))
        {
        }

        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            var Entity = RangeTarget(player, castx, casty);

            foreach (var Target in Entity)
            {
                if (Target is script.monster.Monster)
                {
                    (Target as script.monster.Monster).TakeDamage(player, this);
                    World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicEffect((Target as script.monster.Monster).m_Serial, Seq.OnImpactSprite).Compile()));
                }
                else if (Target is Player)
                {
                    (Target as Player).TakeDamage(player, this);
                    World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicEffect((Target as Player).m_Serial, Seq.OnImpactSprite).Compile()));
                }
            }
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));

        }
    }
}