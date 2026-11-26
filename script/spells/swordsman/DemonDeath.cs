using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public class DemonDeath : Spell
    {
        public override string Name { get { return "DEMON DEATH"; } }
        public override int SpellLearnedIcon { get { return 68; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return -30; } }
        public override int ManaCostPl { get { return 0; } }
        public override int Range { get { return 2; } }
        public override int RecastTime { get { return 5000; } }
        public override int Limit { get { return 5; } }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    0,  //oncast
                    0,  //moving
                    4,  //impact
                    0,  //thickness
                    0,  //type
                    0,  //speed
                    0  //streak
                    );
            }
        }

        public DemonDeath()
            : base(new script.item.DemonDeathBook(1))
        {
        }
        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            Point2D Loc = CombatHandler2.AdjecentTile(player, player.Face);
            var Target = DirTarget(player.m_Channel, Loc, player.Map, 1, player.PK);

            if (Target == null)     return;
            
            if (Target is script.monster.Monster)
                (Target as script.monster.Monster).TakeDamage(player, this);
            else if (Target is Player)
                (Target as Player).TakeDamage(player, this);
            player.client.castHandler.CreateMagicEffect(Loc, player.m_Channel, player.Map, player.Face, 4, 2000);
        }
    }
}