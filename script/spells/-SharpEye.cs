using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;

namespace LKCamelot.script.spells
{
    /// <summary>
    /// 샤프아이. 대상의 은신(TRANSPARENCY) 투명도를 권당 10%씩 벗긴다. 0%가 되면 은신이 풀린다.
    /// 운영자 #은신에는 통하지 않는다.
    /// </summary>
    public class SharpEye : Spell
    {
        public override string Name { get { return "SHARP EYE"; } }
        public override int SpellLearnedIcon { get { return 5; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return -68; } }
        public override int ManaCostPl { get { return 4; } }
        public override int Limit { get { return 10; } }

        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    0,   //oncast
                    0,  //moving
                    0,   //impact
                    0,   //thickness
                    0,   //type
                    0,   //speed
                    0    //streak
                    );
            }
        }

        public SharpEye()
            : base(new script.item.SharpEyeBook(1))
        {
        }

        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));

            // 같은 맵에서 은신 마법이 걸려 있는 유저만 대상이다. 운영자 은신은 제외.
            var hidden = PlayerHandler.getSingleton().add[player.m_Channel].Values
                .Where(xe => xe != player && xe.loggedIn && xe.Map == player.Map && xe.Loc != null
                    && !xe.GMHidden && xe.SpellHide > 0
                    && xe.m_Buffs.Values.Any(b => b is Transparency))
                .ToList();

            // 시리얼로 찍힌 유저가 우선. 완전 투명한 유저는 클릭이 안 될 수 있으므로,
            // 없으면 누른 좌표 주변(Settings.SharpEyeRange)에서 가장 가까운 은신 유저를 찾는다.
            Player target = null;
            if (castonid != 0)
                target = hidden.FirstOrDefault(xe => xe.m_Serial == castonid);
            if (target == null)
                target = hidden
                    .Where(xe => World.Dist2d(xe.Loc.X, xe.Loc.Y, castx, casty) <= Settings.SharpEyeRange)
                    .OrderBy(xe => World.Dist2d(xe.Loc.X, xe.Loc.Y, castx, casty))
                    .FirstOrDefault();

            if (target == null)
            {
                player.client.SendPacket(new UpdateChatBox(0x21, 0x67, 1, string.Format("{0} 마법이 실패하였습니다", this.Name)).Compile());
                return;
            }

            int remain = target.SpellHide - (Settings.SharpEyePercentPl * this.Level);
            if (remain <= 0)
            {
                // 전부 벗겨졌으면 은신을 끝낸다.
                foreach (var old in target.m_Buffs.Where(xe => xe.Value is Transparency).ToList())
                {
                    Spell removed;
                    target.m_Buffs.TryRemove(old.Key, out removed);
                }
                target.RefreshTransparency();
                target.SendEffects();
            }
            else
            {
                target.SpellHide = (byte)remain;
                target.RefreshTransparency();
                target.SendEffects();
            }

            player.client.SendPacket(new UpdateChatBox(0x21, 0x67, 1, string.Format("{0} 마법을 사용했습니다.", this.Name)).Compile());
        }
    }
}
