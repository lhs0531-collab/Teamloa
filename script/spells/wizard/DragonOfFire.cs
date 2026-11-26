using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public class DragonOfFire : Spell
    {
        public override string Name { get { return "DRAGON OF FIRE"; } }
        public override int SpellLearnedIcon { get { return 63; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 575; } }
        public override int DamPl { get { return 12; } }
        public override int ManaCost { get { return -78; } }
        public override int ManaCostPl { get { return 6; } }
        public override int menCoff { get { return 6; } }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    42,  //oncast
                    28,  //moving
                    17,  //impact
                    0,  //thickness
                    0,  //type
                    9,  //speed
                    0  //streak
                    );
            }
        }

        public DragonOfFire()
            : base(new script.item.DragonOfFireBook(1))
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
            World.SendToSoundRange(new QueDele(player.m_Channel, player.Map, new EffectSound(library.EffectSound.DragonOfFire).Compile()), player);
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new CurveMagic(player.m_Serial, castx, casty, Seq).Compile()));
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));
        }
    }
}