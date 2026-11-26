using LKCamelot.model;
using System;
using System.Linq;

namespace LKCamelot.script.spells
{
    public class Teleport : Spell
    {
        public override string Name { get { return "TELEPORT"; } }
        public override int SpellLearnedIcon { get { return 53; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Coord; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return 125; } }
        public override int ManaCostPl { get { return 0; } }
        public override int RecastTime { get { return 1000; } }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    0,  //oncast
                    0,  //moving
                    0,  //impact
                    0,  //thickness
                    0xff,  //type
                    0,  //speed
                    0  //streak
                    );
            }
        }

        public Teleport()
            : base(new script.item.TeleportBook(1))
        {
        }
        public override void UseSpell(model.Player player, short X, short Y)
        {
            var dist = ((this.Level / 2) * 2);

            if (dist <= 3) dist = 4;
            if (dist > 12) dist = 12;

            if (model.World.Dist2d(X, Y, (short)player.Loc.X, (short)player.Loc.Y) <= dist)
            {
                var play = PlayerHandler.getSingleton().add[player.m_Channel].Where(xe=> xe.Value.loggedIn && xe.Value.Loc.X == X && xe.Value.Loc.Y == Y).FirstOrDefault();
                if (play.Value != null)
                    return;

                var nmap = model.Map.FullMaps[player.m_Channel][player.Map];
                Map.TiledMap curmap = null;
                try
                {
                    curmap = LKCamelot.model.Map.loadedmaps[nmap.Filename];
                }
                catch
                {
                    Console.WriteLine(string.Format("Failed to nmap at {0} - {1}", nmap, player.Name));
                }

                LKCamelot.model.MyPathNode randomtile;

                try
                {
                    randomtile = curmap.tiles[X, Y];
                }
                catch
                {
                    return;
                }
                if (randomtile.IsWall)
                    return;

                player.Loc = new Point2D(X, Y);

                World.SendToSoundRange(new QueDele(player.m_Channel, player.Map, new EffectSound(library.EffectSound.Teleport).Compile()), player);
                World.SendToAll(new QueDele(player.m_Channel, player.Map, new MoveSpriteTele(player.m_Serial, player.Face, (short)player.Loc.X, (short)player.Loc.Y).Compile()));
                World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));
                
            }
        }
    }
}