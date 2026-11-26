using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public class WindySpirit : Spell
    {
        public override string Name { get { return "WINDY SPIRIT"; } }
        public override int SpellLearnedIcon { get { return 29; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 3000; } }
        public override int DamPl { get { return 300; } }
        public override int ManaCost { get { return -40; } }
        public override int ManaCostPl { get { return 3; } }
        public override int RecastTime { get { return 5000; } }
        public override int menCoff { get { return 2; } }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    0,  //oncast
                    29,  //moving
                    56,  //impact
                    0,  //thickness
                    2,  //type
                    30,  //speed
                    4  //streak
                    );
            }
        }

        public WindySpirit()
            : base(new script.item.WindySpiritBook(1))
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
                }
                else if (Target is Player)
                {
                    (Target as Player).TakeDamage(player, this);
                }
            }
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new CurveMagic(player.m_Serial, castx, casty, Seq).Compile()));
        }

    }
}