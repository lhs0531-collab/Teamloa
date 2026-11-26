namespace LKCamelot.script.spells
{
    public class Twister : Spell
    {
        public override string Name { get { return "TWISTER"; } }
        public override int SpellLearnedIcon { get { return 74; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Casted; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return 102; } }
        public override int ManaCostPl { get { return 6; } }
        public override int Limit { get { return 10; } }

        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                tbuff.BuffType = BuffCase.Twister;
                return tbuff;
            }
        }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    0,  //oncast
                    0,  //moving
                    0,  //impact
                    0,  //thickness
                    3,  //type
                    3,  //speed
                    0  //streak
                    );
            }
        }

        public Twister()
            : base(new script.item.TwisterBook(1))
        {
        }
        public override void UseSpell(model.Player player)
        {
            player.AddBuff(this);
        }
    }
}