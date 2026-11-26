using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class Recall : Spell
    {
        public override string Name { get { return "RECALL"; } }
        public override int SpellLearnedIcon { get { return 75; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return -65; } }
        public override int ManaCostPl { get { return 3; } }
        public override int menCoff { get { return 12; } }
        public override int RecastTime { get { return 30000; } }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    42,  //oncast
                    27,  //moving
                    00,  //impact
                    0,  //thickness
                    0,  //type
                    9,  //speed
                    1  //streak
                    );
            }
        }

        public Recall()
            : base(new script.item.RecallBook(1))
        {
        }
        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            if (castonid != 0)
            {
                var Target = SerialPlayerTarget(player.m_Channel, castonid);
                if (Target == null) return;

                var dist = ((this.Level / 4) * 4);

                if (dist <= 4) dist = 4;
                if (dist > 12) dist = 12;

                if (model.World.Dist2d(Target.Loc.X, Target.Loc.Y, (short)player.Loc.X, (short)player.Loc.Y) <= dist)
                {
                    //new model.Point2D(player.Loc.X, player.Loc.Y);
                    Point2D temp = World.AddMove(player);
                    Target.Loc.X = temp.X;
                    Target.Loc.Y = temp.Y;

                    World.SendToAll(new QueDele(Target.m_Channel, Target.Map, new MoveSpriteTele(Target.m_Serial, Target.Face, (short)Target.Loc.X, (short)Target.Loc.Y).Compile()));
                    World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));
                } 
                
            }
        }
    }
}
