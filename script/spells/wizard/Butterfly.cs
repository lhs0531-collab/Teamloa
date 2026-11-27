using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public class Butterfly : Spell
    {
        public override string Name { get { return "BUTTERFLY"; } }
        public override int SpellLearnedIcon { get { return 71; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return 0; } }
        public override int ManaCostPl { get { return 6; } }

        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                tbuff.Time = 180 * Settings.Second;
                tbuff.TimePI = 10;
                tbuff.Hit = 20;
                tbuff.Hitpl = 2;
                return tbuff;
            }
        }


        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    34,  //oncast
                    0,  //moving
                    0,  //impact
                    0,  //thickness
                    0,  //type
                    0,  //speed
                    0  //streak
                    );
            }
        }

        public Butterfly()
            : base(new script.item.ButterflyBook(1))
        {
        }

        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            var Target = SerialPlayerTarget(player.m_Channel, castonid, castx, casty);

            if (Target != null)
            {
                if (!Target.AddBuff(this))
                    player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0} 마법이 실패하였습니다", this.Name)).Compile());
                else
                    Target.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0} 마법이 사용하였습니다.", this.Name)).Compile());
            }
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));
        }
    }
}