using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public class Pickup : Spell
    {
        public override string Name { get { return "PICK UP"; } } 
        public override int SpellLearnedIcon { get { return 52; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return 116; } }
        public override int ManaCostPl { get { return 6; } }

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
                    0,  //speed
                    0  //streak
                    );
            }
        }

        public Pickup()
            : base(new script.item.PickUpBook(1))
        {
        }
        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            if (castonid != 0)
            {
                var Target = SerialItem(castonid);
                if (Target == null)
                {
                    player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0} 마법이 실패하였습니다.", this.Name)).Compile());
                    return;
                }

                var dist = ((this.Level / 2) * 2);
                if (dist <= 3) dist = 4;
                if (dist > 12) dist = 12;

                if (model.World.Dist2d(castx, casty, (short)player.Loc.X, (short)player.Loc.Y) > dist)
                    return;
                Target.PickUp(player);
                player.Event_Chat("PICK UP!!!");
            }
            else
            {
                player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0} 마법이 실패하였습니다.", this.Name)).Compile());
            }
            World.SendToSoundRange(new QueDele(player.m_Channel, player.Map, new EffectSound(library.EffectSound.Pickup).Compile()), player);
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));        
        }
    }
}