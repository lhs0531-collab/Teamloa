using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public class FireHawk : Spell
    {
        public override string Name { get { return "FIRE HAWK"; } }
        public override int SpellLearnedIcon { get { return 14; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 456; } }
        public override int DamPl { get { return 14; } }
        public override int ManaCost { get { return -77; } }
        public override int ManaCostPl { get { return 6; } }
        public override int menCoff { get { return 12; } }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    42,  //oncast
                    27,  //moving
                    17,  //impact
                    0,  //thickness
                    0,  //type
                    9,  //speed
                    1  //streak
                    );
            }
        }

        public FireHawk()
            : base(new script.item.FireHawkBook(1))
        {
        }
        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            if (castonid != 0)
            {
                var Target = SerialTarget(player.m_Channel, castonid, player.PK);
                if (Target == null) return;

                if (Target is script.monster.Monster)
                    (Target as script.monster.Monster).TakeDamage(player, this);
                else if (Target is Player)
                    (Target as Player).TakeDamage(player, this);

            }
            World.SendToSoundRange(new QueDele(player.m_Channel, player.Map, new EffectSound(library.EffectSound.FireHawk).Compile()), player);
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new CurveMagic(player.m_Serial, castx, casty, Seq).Compile()));
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));
            
        }
    }
}