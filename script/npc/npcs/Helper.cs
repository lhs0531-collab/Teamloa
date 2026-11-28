using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    /// <summary>
    /// 도우미. 클릭하면 골드를 받고 이웃마을 백성을 해제해 마지막왕국 백성으로 만들어 준다.
    /// 해제는 접속을 끊을 때까지 유지된다(맵·마을 이동과 무관). 비용은 Settings.CitizenPassCost.
    /// </summary>
    public class Helper : BaseNPC
    {
        public override string Name { get { return "도우미"; } }
        public override List<string> ChatString
        {
            get
            {
                return new List<string>()
                {
                    "저를 누르면 마지막왕국 백성으로 만들어 드립니다.",
                };
            }
        }

        public override byte Sprite { get { return 2; } }
        public override byte aSpeed { get { return 4; } }
        public override byte aFrames { get { return 4; } }
        public override int EventRange { get { return 6; } }
        public override int MessageGap { get { return 8 * Settings.Second; } }

        // 동상이므로 다른 NPC처럼 주기적으로 사라졌다 다시 나타나지 않는다.
        public override int AliveTime { get { return 0; } }

        public Helper(Serial serial, int Channel, string Map, Point2D point)
            : base(serial)
        {
            base.m_Channel = Channel;
            base.m_Map = Map;
            base.m_Loc = new Point2D(point.X, point.Y);
        }

        public override bool Event_Click(Player player)
        {
            if (player == null || !player.loggedIn || player.client == null)
                return true;

            // 같은 맵에서 가까이 있을 때만 처리한다.
            if (player.m_Channel != this.m_Channel || player.Map != this.m_Map || World.Dist2d(player.Loc, this.m_Loc) > EventRange)
            {
                Say(player, "도우미에게 가까이 가세요.");
                return true;
            }

            if (player.CitizenPass)
            {
                Say(player, "이미 마지막왕국 백성입니다.");
                return true;
            }

            // 중복 아이피·벤으로 걸린 이웃은 골드로 풀 수 없다.
            if (player.IPNeighbor || player.BenNeighbor)
            {
                Say(player, "해제할 수 없는 이웃 나라 백성입니다.");
                return true;
            }

            if (!player.IsNeighbor)
            {
                Say(player, "이웃 나라 백성이 아닙니다.");
                return true;
            }

            ulong cost = player.Promo > 0 ? Settings.CitizenPassCostPromo : Settings.CitizenPassCost;
            if (player.Gold < cost)
            {
                Say(player, "골드가 부족합니다.");
                return true;
            }

            player.Gold -= cost;
            player.CitizenPass = true;

            // 서비스로 버프 3종(와콘다, 버터플라이, 실버스크린)을 걸어 준다.
            GiveBuff(player, new script.spells.Wakonda());
            GiveBuff(player, new script.spells.Butterfly());
            GiveBuff(player, new script.spells.SilverScreen());

            // 절반으로 적용되던 기본 능력치가 돌아오고 버프가 걸렸으므로 능력치 창과 버프 표시를 다시 보낸다.
            player.client.SendPacket(new UpdateCharStats(player).Compile());
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new SetObjectEffects(player).Compile()));
            if (player.Transparancy >= 100)
                player.client.SendPacket(new SetObjectEffects(player, true).Compile());

            Say(player, "마지막왕국 백성이 되었습니다.");
            Say(player, "접속을 끊으면 다시 이웃 나라 백성이 됩니다.");
            Say(player, "WAKONDA, BUTTERFLY, SILVER SCREEN 마법을 받았습니다.");
            return true;
        }

        /// <summary>
        /// 버프를 Settings.CitizenPassBuffLevel권으로 건다. 같은 버프가 이미 있으면 지우고 다시 건다.
        /// </summary>
        private static void GiveBuff(Player player, script.spells.Spell spell)
        {
            // 시전자가 없는 마법이므로 요구 능력치를 없애 권수를 고정한다.
            // (Spell.Level은 시전자의 능력치가 모자라면 권수를 낮춰 돌려준다)
            spell.Caster = player;
            spell.LevelReq = spell.LevelReqPl = 0;
            spell.StrReq = spell.StrReqPl = 0;
            spell.MenReq = spell.MenReqPl = 0;
            spell.DexReq = spell.DexReqPl = 0;
            spell.m_Level = Settings.CitizenPassBuffLevel;

            foreach (var old in player.m_Buffs.Where(xe => xe.Value.Name == spell.Name).ToList())
            {
                script.spells.Spell removed;
                player.m_Buffs.TryRemove(old.Key, out removed);
            }

            // 버프 목록은 끝나는 시각(ms)이 키라서, 같은 순간에 지속시간이 같은 버프를 여럿 걸면 키가 겹친다.
            // (AddBuff는 겹치면 조용히 버리므로 직접 넣고, 겹치면 1ms씩 미룬다)
            // 지속시간은 마법의 원래 값이 아니라 Settings.CitizenPassBuffTime을 쓴다.
            long key = Server.tickcount.ElapsedMilliseconds + Settings.CitizenPassBuffTime;
            while (!player.m_Buffs.TryAdd(key, spell))
                key++;
        }

        private static void Say(Player player, string message)
        {
            player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, message).Compile());
        }
    }
}
