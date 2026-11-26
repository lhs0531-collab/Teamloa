using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class Comeback : Spell
    {
        public override string Name { get { return "COME BACK"; } }  //chance village
        public override int SpellLearnedIcon { get { return 54; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Casted; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return 70; } }
        public override int ManaCostPl { get { return 0; } }
        
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

        public Comeback()
            : base(new script.item.ComeBackBook(1))
        {
        }

        public override void UseSpell(model.Player player)
        {
            player.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, string.Format("{0} 마법을 사용했습니다.", Name)).Compile());
            
            player.CombackLoc = new Point2D(player.Loc.X, player.Loc.Y);
            player.CombackMap = player.Map;

            player.client.HandleGo("마을");
        }
    }
}