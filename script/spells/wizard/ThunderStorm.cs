using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public class ThunderStorm : Spell
    {
        public override string Name { get { return "THUNDER STORM"; } }
        public override int SpellLearnedIcon { get { return 25; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 290; } }
        public override int DamPl { get { return 10; } }
        public override int menCoff { get { return 12; } }
        public override int ManaCost { get { return -67; } }
        public override int ManaCostPl { get { return 5; } }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    0,  //oncast
                    0,  //moving
                    10,  //impact
                    0,  //thickness
                    3,  //type
                    0,  //speed
                    0  //streak
                    );
            }
        }

        public ThunderStorm()
            : base(new script.item.ThunderStormBook(1))
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