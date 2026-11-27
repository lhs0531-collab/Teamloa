using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    /// <summary>
    /// 은신. 자신을 권당 10%씩 투명하게 만든다(10권 = 운영자 #은신과 같은 완전 투명).
    /// 지속시간은 1권 30초, 권당 +10초. 시간이 지나면 자동으로 풀린다.
    /// 남은 시간·종료 안내는 버프 공통 처리(WorldTimer)가 보낸다.
    /// </summary>
    public class Transparency : Spell
    {
        public override string Name { get { return "TRANSPARENCY"; } }
        public override int SpellLearnedIcon { get { return 10; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Casted; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return -68; } }
        public override int ManaCostPl { get { return 4; } }
        public override int Limit { get { return 10; } }

        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                tbuff.Time = Settings.HideTime;
                tbuff.TimePI = Settings.HideTimePl;
                return tbuff;
            }
        }
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

        public Transparency()
            : base(new script.item.TransparencyBook(1))
        {
        }

        public override void UseSpell(model.Player player)
        {
            // 이미 걸려 있으면 지우고 다시 걸어 지속시간과 투명도를 새로 적용한다.
            foreach (var old in player.m_Buffs.Where(xe => xe.Value is Transparency).ToList())
            {
                Spell removed;
                player.m_Buffs.TryRemove(old.Key, out removed);
            }

            int percent = Settings.HidePercentPl * this.Level;
            if (percent > 100) percent = 100;
            if (percent < 0) percent = 0;

            // AddBuff가 표시 패킷을 보내므로 투명도를 먼저 정해 둔다.
            player.SpellHide = (byte)percent;
            if (!player.GMHidden)
                player.Transparancy = player.SpellHide;

            if (!player.AddBuff(this))
            {
                player.RefreshTransparency();
                player.client.SendPacket(new UpdateChatBox(0x21, 0x67, 1, string.Format("{0} 마법이 실패하였습니다", this.Name)).Compile());
                return;
            }

            player.RefreshTransparency();
            player.SendEffects();
            player.client.SendPacket(new UpdateChatBox(0x21, 0x67, 1, string.Format("{0} 마법을 사용했습니다.", this.Name)).Compile());
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));
        }
    }
}
