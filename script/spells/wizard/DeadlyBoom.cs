using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public class DeadlyBoom : Spell, ISingle
    {
        public override string Name { get { return "DEADLY BOOM"; } }
        public override int SpellLearnedIcon { get { return 69; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 555; } }
        public override int DamPl { get { return 14; } }
        public override int ManaCost { get { return -65; } } //65
        public override int ManaCostPl { get { return 5; } }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    49,  //oncast
                    0,  //moving
                    63,  //impact
                    0,  //thickness
                    3,  //type
                    30,  //speed
                    0  //streak
                    );
            }
        }

        public DeadlyBoom()
            : base(new script.item.DeadlyBoomBook(1))
        {
        }

        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            if (castonid != 0)
            {
                var Target = SerialTarget(player.m_Channel, castonid, player.PK);

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
                World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));
            }
        }
    }
}