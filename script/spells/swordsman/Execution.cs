using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public class Execution : Spell
    {
        public override string Name { get { return "EXECUTION"; } }
        public override int SpellLearnedIcon { get { return 51; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 390; } }
        public override int DamPl { get { return 12; } }

        public override int ManaCost { get { return -67; } }
        public override int ManaCostPl { get { return 5; } }

        public override int dexCoff { get { return 2; } }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    69,  //oncast
                    69,  //moving
                    68,  //impact
                    0,  //thickness
                    0,  //type
                    26,  //speed
                    10  //streak
                    );
            }
        }

        public Execution()
            : base(new script.item.ExecutionBook(1))
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