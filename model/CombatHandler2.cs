using LKCamelot.library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.model
{
    public class CombatHandler2
    {
        io.IOClient client;
        PlayerHandler handler;
        public CombatHandler2(io.IOClient client, PlayerHandler handler)
        {
            this.client = client;
            this.handler = handler;
        }

        public void HandleCast(int header, script.spells.Spell spell, Player player, int target = 0, short castx = 0, short casty = 0)
        {
            
        }

        public void HandleMelee(Player play, int swingdir)
        {
            List<BaseObject> target = World.GetTileTarget(play, AdjecentTile(play, swingdir), swingdir);

            if (target == null)
                return;
            var take = play.Dam;

            // 트리플/트위스터의 추가 방향 모션. 시리얼 없는 QueDele로 보내 시전자 본인에게도 전송한다.
            // (시리얼을 넣으면 SendToAll이 본인을 건너뛰어 본인 화면에는 한 방향만 보였다)
            if (play.m_Buffs.Where(Xe => Xe.Value.BuffEffect.BuffType == script.spells.BuffCase.Triple).Any())
            {
                SendExtraSwing(play, (int)play.Face - 1);
                SendExtraSwing(play, (int)play.Face + 1);
            }
            else if (play.m_Buffs.Where(Xe => Xe.Value.BuffEffect.BuffType == script.spells.BuffCase.Twister).Any())
            {
                SendExtraSwing(play, (int)play.Face - 2);
                SendExtraSwing(play, (int)play.Face + 2);
                SendExtraSwing(play, (int)play.Face + 4);
            }

            foreach (var tar in target)
            {
                if (tar is Player)
                {
                    take -= (tar as Player).AC;
                    if (take <= 0)
                        take = 1;
                    (tar as Player).TakeDamage(play, take);
                }


                else if (tar is script.monster.Monster)
                {
                    take -= (tar as script.monster.Monster).AC;
                    if (take <= 0)
                        take = 1;

                    (tar as script.monster.Monster).TakeDamage(play);
                }

                else
                    (tar as script.npc.BaseNPC).TakeDamage(play);
            }
        }

        private static void SendExtraSwing(Player play, int dir)
        {
            Point2D tile = AdjecentTile(play, dir);
            short face = World.AttackRange((short)tile.X, (short)tile.Y, (short)play.Loc.X, (short)play.Loc.Y);
            World.SendToAll(new QueDele(play.m_Channel, play.Map, new SwingAnimation(play.m_Serial, face).Compile()));
        }

        public static Point2D AdjecentTile(Player player, int swingloc)
        {
            if (swingloc == -1)
                swingloc = 7;
            if (swingloc == -2)
                swingloc = 6;
            if (swingloc == -3)
                swingloc = 5;
            if (swingloc == -4)
                swingloc = 4;
            if (swingloc == 8)
                swingloc = 0;
            if (swingloc == 9)
                swingloc = 1;
            if (swingloc == 10)
                swingloc = 2;
            if (swingloc == 11)
                swingloc = 3;

            if (swingloc == 0)
                return new Point2D(player.Loc.X, player.Loc.Y - 1);
            if (swingloc == 1)
                return new Point2D(player.Loc.X + 1, player.Loc.Y - 1);
            if (swingloc == 2)
                return new Point2D(player.Loc.X + 1, player.Loc.Y);
            if (swingloc == 3)
                return new Point2D(player.Loc.X + 1, player.Loc.Y + 1);
            if (swingloc == 4)
                return new Point2D(player.Loc.X, player.Loc.Y + 1);
            if (swingloc == 5)
                return new Point2D(player.Loc.X - 1, player.Loc.Y + 1);
            if (swingloc == 6)
                return new Point2D(player.Loc.X - 1, player.Loc.Y);
            if (swingloc == 7)
                return new Point2D(player.Loc.X - 1, player.Loc.Y - 1);

            return new Point2D(1, 1);
        }
    }
}
