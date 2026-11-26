using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class Oblivion : Spell
    {
        public override string Name { get { return "OBLIVION"; } }
        public override int SpellLearnedIcon { get { return 60; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return 46; } }
        public override int ManaCostPl { get { return 6; } }

        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                tbuff.Time = 11 * Settings.Second;
                tbuff.TimePI = 1;
                return tbuff;
            }
        }


        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    100,  //oncast
                    0,  //moving
                    8,  //impact
                    0,  //thickness
                    0,  //type
                    0,  //speed
                    0  //streak
                    );
            }
        }
    
        public Oblivion()
            : base(new script.item.OblivionBook(1))
        {
        }

        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            var Target = SerialTarget(player.m_Channel, castonid, false);
            bool rnd  = Util.Random(0, 100) < 50;
            if (Target != null && rnd)
            {
                if (!Target.AddBuff(this))
                    player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0} 마법이 실패하였습니다", this.Name)).Compile());
                else
                    player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0} 마법이 사용하였습니다.", this.Name)).Compile());
            }
            else
                player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0} 마법이 실패하였습니다", this.Name)).Compile());
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));
        }
    }
}
