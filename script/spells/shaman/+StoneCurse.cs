using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public class StoneCurse : Spell
    {
        public override string Name { get { return "STONE CURSE"; } }
        public override int SpellLearnedIcon { get { return 38; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return -10; } }
        public override int ManaCostPl { get { return -5; } }
        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                tbuff.BuffType = BuffCase.Freezing;
                tbuff.Time = 10 * Settings.Second;
                tbuff.TimePI = 2 * Settings.Second;
                return tbuff;
            }
        }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    61,  //oncast
                    0,  //moving
                    0,  //impact
                    0,  //thickness
                    0,  //type
                    0,  //speed
                    0  //streak
                    );
            }
        }

        public StoneCurse() : base(new script.item.StoneCurseBook(1))
        {
        }
        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            var Target = SerialPlayerTarget(player.m_Channel, castonid);

            if (Target != null)
            {
                if (!Target.AddBuff(this))
                    player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "마법이 실패하였습니다").Compile());
            }
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));
        }
    }
}