using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.script.spells;
using LKCamelot.library;
namespace LKCamelot.model
{
    public class CastHandler
    {
        io.IOClient client;
        PlayerHandler handler;
        public CastHandler(io.IOClient client, PlayerHandler handler)
        {
            this.client = client;
            this.handler = handler;
        }

        /// <returns>만든 효과 오브젝트의 시리얼. 만들지 못했으면 0.</returns>
        public int CreateMagicEffect(Point2D target, int channel, string map, short dir, byte sprite, int time = 1500, byte width = 0)
        {
            if (target == null) return 0;

            int mobile = Serial.NewMobile;
            World.SendToAll(new QueDele(channel, map, new CreateMagicEffect(mobile, dir, (short)target.X, (short)target.Y, new byte[] { 4, 0, 0, 0, 0, 0, 0, 0, 0, sprite }, width).Compile()));
            var tmp = new QueDele(LKCamelot.Server.tickcount.ElapsedMilliseconds + time, channel, map, new DeleteObject(mobile).Compile());
            tmp.tempser = mobile;
            World.TickQue.Add(tmp);
            return mobile;
        }

        public void TakeDamage(Player caster, Player target, script.spells.Spell spell)
        {
            //if (Settings.NOPK.Contains(caster.Map.Split(',')[0]))
            //    return;
            //if (Settings.PKProtectLevel >= (target as Player).Level || caster.PK == false)
            //    return;

            var currMap = model.Map.FullMaps[caster.m_Channel].Where(Xe => Xe.Key == caster.Map).FirstOrDefault().Value;
            if (currMap.PKable == Map.PKType.ImpossiblePK)
                return;
            if (Settings.PKProtectLevel >= (target as Player).Level || caster.PK == false)
                return;

            float h = ((float)caster.Hit / ((float)caster.Hit + (float)target.Hit)) * 200;

            if (h >= 100 || new Random().Next(0, 100) < (int)h)
            {
                int take = spell.DamBase + (spell.DamPl * spell.Level);// +(spell.DamPl * spell.SLevel2);
                if (spell.ManaCostPl != 0)
                {
                    take += (caster.GetStat("men") / spell.menCoff);
                    take += (caster.GetStat("str") / spell.strCoff);
                    take += (caster.GetStat("dex") / spell.dexCoff);
                }
                if (spell is script.spells.DemonDeath)
                    take = Convert.ToInt32(caster.HP * 0.5) + caster.GetStat("dex");

                
                target.HPCur -= (take - target.AC);
                if ( Settings.RestMap.Contains(target.Map) && target.Color != Color.Red)
                {
                    //if (Settings.NotIncreasePKCountMap.Contains(caster.Map))
                    //    return;
                    if (currMap.PKable == Map.PKType.PossiblePK_NoIncrease)
                        return;

                    caster.pkhistoryTime += Settings.PKDelay;
                    caster.client.SendPacket(new Attrib(caster).Compile());
                    if (Settings.PKAlarm)
                    {                        
                            string chat = string.Format("{0}님(PK:{1}) 이 {2}님을 죽임. 맵:{3}({4}:{5}", caster.Name, caster.PKCount,  (target as Player).Name, caster.Map, caster.Loc.X ,caster.Loc.Y);
                            World.SendToAll(new QueDele(caster.m_Channel, caster.Map, new UpdateChatBox(0x25, 0x65, 0, chat).Compile()));
                    }
                }
                World.SendToAll(new QueDele(caster.m_Channel, caster.m_Map, new HitAnimation(target.m_Serial, Convert.ToByte(((((float)target.m_HPCur / (float)target.HP) * 100) * 1))).Compile()));
            }
        }

        public void HandleCast(int header, script.spells.Spell castspell, Player player, int target = 0, short castx = 0, short casty = 0)
        {
            var caston = World.NewMonsters[player.m_Channel].Where(xe => xe.Value.m_Serial == target
                                && World.Dist2d(xe.Value.m_Loc.X, xe.Value.m_Loc.Y, player.Loc.X, player.Loc.Y) <= castspell.Range
                                && xe.Value.Alive
                && xe.Value.m_Map != null && xe.Value.m_Map == player.m_Map
                ).Select(xe => xe.Value);

            var playcaston = PlayerHandler.getSingleton().Total.Where(xe => xe.Value != null && xe.Value != player && xe.Value.loggedIn
                && World.Dist2d(xe.Value.m_Loc.X, xe.Value.m_Loc.Y, player.m_Loc.X, player.m_Loc.Y) <= castspell.Range
                && xe.Value.m_Serial == (Serial)target
                && xe.Value.m_Map != null && xe.Value.m_Map == player.m_Map).FirstOrDefault();

            if (castspell.mType == LKCamelot.library.MagicType.Casted || castspell.mType == LKCamelot.library.MagicType.Target)
            {
                caston = World.NewMonsters[player.m_Channel].Where(xe => xe.Value.m_Map != null
                       && xe.Value.m_Map == player.Map
                       && World.Dist2d(xe.Value.m_Loc.X, xe.Value.m_Loc.Y, player.Loc.X, player.Loc.Y) <= castspell.Range
                       && xe.Value.Alive)
                      .Select(xe => xe.Value);
            }


            if (playcaston.Key != null && !(Settings.NOPK.Contains(player.Map.Split(',')[0])) )
            {

                if (castspell is ISingle)
                {
                    if (player.MPCur < castspell.RealManaCost(player))
                        return;
                    player.MPCur -= castspell.RealManaCost(player);
                    castspell.CheckLevelUp(player);

                    CreateMagicEffect(playcaston.Value.Loc, playcaston.Value.m_Channel, playcaston.Value.Map, playcaston.Value.Face, (byte)castspell.Seq.OnImpactSprite, 1500);

                    TakeDamage(player, playcaston.Value, castspell);
                    return;
                }

                if (castspell.Name == "DEMON DEATH")
                {
                    if (player.HPCur < (int)(player.HP * 0.70))
                        return;

                    var miyamo = player.Equipped.Where(xe => xe.GetType() == typeof(script.item.MiyamotosStick)).FirstOrDefault();
                    var recast = castspell.RecastTime;
                    if (miyamo != null)
                    {
                        recast -= 1000;
                        recast -= miyamo.Stage * 300;
                    }
                    
                    if (LKCamelot.Server.tickcount.ElapsedMilliseconds - recast > castspell.Cooldown)
                    {
                        castspell.Cooldown = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                    }
                    else
                        return;

                    player.HPCur -= castspell.RealManaCost(player);
                    castspell.CheckLevelUp(player);

                    int mobile = Serial.NewMobile;
                    World.SendToAll(new QueDele(player.m_Channel, player.Map, new CreateMagicEffect(mobile, 1, (short)playcaston.Value.m_Loc.X, (short)playcaston.Value.m_Loc.Y, new byte[] { 4, 0, 0, 0, 0, 0, 0, 0, 0, (byte)castspell.Seq.OnImpactSprite }, 0).Compile()));
                    var tmp = new QueDele(LKCamelot.Server.tickcount.ElapsedMilliseconds + 2000, player.m_Map, new DeleteObject(mobile).Compile());
                    tmp.tempser = mobile;
                    World.TickQue.Add(tmp);

                    TakeDamage(player, playcaston.Value, castspell);

                    return;
                }

                if (player.MPCur < castspell.RealManaCost(player))
                    return;
                player.MPCur -= castspell.RealManaCost(player);
                castspell.CheckLevelUp(player);
                TakeDamage(player, playcaston.Value, castspell);
                World.SendToAll(new QueDele(player.m_Channel, player.Map, new CurveMagic(player.m_Serial, castx, casty, castspell.Seq).Compile()));
            }


            switch (castspell.mType)
            {
                case (LKCamelot.library.MagicType.Target2):
                    foreach (var targete in caston)
                    {
                        if (castspell is ISingle)
                        {
                            if (player.MPCur < castspell.RealManaCost(player))
                                return;
                            player.MPCur -= castspell.RealManaCost(player);
                            castspell.CheckLevelUp(player);

                            CreateMagicEffect(targete.m_Loc, targete.m_Channel, targete.m_Map, targete.Face, (byte)castspell.Seq.OnImpactSprite, 1500);

                            targete.TakeDamage(player, castspell);
                            return;
                        }

                        if (castspell.Name == "DEMON DEATH")
                        {
                            if (player.HPCur < (int)(player.HP * 0.70))
                                return;

                            var miyamo = player.Equipped.Where(xe => xe.GetType() == typeof(script.item.MiyamotosStick)).FirstOrDefault();
                            var recast = castspell.RecastTime;
                            if (miyamo != null)
                            {
                                recast -= 1000;
                                recast -= miyamo.Stage * 300;
                            }

                            if (LKCamelot.Server.tickcount.ElapsedMilliseconds - recast > castspell.Cooldown)
                            {
                                castspell.Cooldown = LKCamelot.Server.tickcount.ElapsedMilliseconds;
                            }
                            else
                                return;

                            player.HPCur -= castspell.RealManaCost(player);
                            castspell.CheckLevelUp(player);

                            int mobile = Serial.NewMobile;
                            World.SendToAll(new QueDele(player.m_Channel, player.Map, new CreateMagicEffect(mobile, 1, (short)targete.m_Loc.X, (short)targete.m_Loc.Y, new byte[] { 4, 0, 0, 0, 0, 0, 0, 0, 0, (byte)castspell.Seq.OnImpactSprite }, 0).Compile()));
                            var tmp = new QueDele(LKCamelot.Server.tickcount.ElapsedMilliseconds + 2000, player.m_Map, new DeleteObject(mobile).Compile());
                            tmp.tempser = mobile;
                            World.TickQue.Add(tmp);

                            targete.TakeDamage(player, castspell);

                            return;
                        }

                        if (player.MPCur < castspell.RealManaCost(player))
                            return;
                        player.MPCur -= castspell.RealManaCost(player);
                        castspell.CheckLevelUp(player);
                        targete.TakeDamage(player, castspell);
                        World.SendToAll(new QueDele(player.m_Channel, player.Map, new CurveMagic(player.m_Serial, castx, casty, castspell.Seq).Compile()));
                    }

                    break;

                case (LKCamelot.library.MagicType.Casted):
                    if (player.MPCur < castspell.RealManaCost(player))
                        return;
                    player.MPCur -= castspell.RealManaCost(player);

                    foreach (var targete in caston)
                        targete.TakeDamage(player, castspell);

                    World.SendToAll(new QueDele(player.m_Channel, player.Map, new CurveMagic(player.m_Serial, 1, 1, castspell.Seq).Compile()));

                    break;
                case (LKCamelot.library.MagicType.Target):
                    if (player.MPCur < castspell.RealManaCost(player))
                        return;
                    player.MPCur -= castspell.RealManaCost(player);

                    World.SendToAll(new QueDele(player.m_Channel, player.Map, new CurveMagic(player.m_Serial, 1, 1, castspell.Seq).Compile()));
                    foreach (var targetee in caston)
                    {
                        int mobile = Serial.NewMobile;
                        World.SendToAll(new QueDele(player.m_Channel, player.Map, new CreateMagicEffect(mobile, 1, (short)targetee.m_Loc.X, (short)targetee.m_Loc.Y, new byte[] { 4, 0, 0, 0, 0, 0, 0, 0, 0, (byte)castspell.Seq.OnImpactSprite }, 0).Compile()));
                        // World.SendToAll(new QueDele(player.Map, new SetObjectEffectsMonsterSpell(targetee, castspell.Seq.OnImpactSprite).Compile()));
                        targetee.TakeDamage(player, castspell);
                        var tmp = new QueDele(LKCamelot.Server.tickcount.ElapsedMilliseconds + 1000, player.m_Map, new DeleteObject(mobile).Compile());
                        tmp.tempser = mobile;
                        World.TickQue.Add(tmp);
                    }
                    break;
            }
        }
    }
}
