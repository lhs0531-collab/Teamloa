using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public class FlyingSword : Spell
    {
        public override string Name { get { return "FLYING SWORD"; } }
        public override int SpellLearnedIcon { get { return 49; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 606; } }
        public override int DamPl { get { return 12; } }
        public override int ManaCost { get { return -80; } }
        public override int ManaCostPl { get { return 6; } }
        public override int dexCoff { get { return 6; } }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    50,  //oncast
                    35,  //moving
                    20,  //impact
                    0,  //thickness
                    2,  //type
                    22,  //speed
                    4  //streak
                    );
            }
        }

        public FlyingSword()
            : base(new script.item.FlyingSwordBook(1))
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