using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    /// <summary>
    /// 도우미(버프 동상). 클릭하면 골드를 받고 버터플라이 마법을 걸어 준다.
    /// 비용과 마법 권수는 Settings.HelperBuffCost / Settings.HelperBuffLevel.
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
                    string.Format("저를 누르면 {0:N0}골드에 버터플라이 마법을 걸어 드립니다.", Settings.HelperBuffCost),
                };
            }
        }

        public override byte Sprite { get { return 2; } }
        public override byte aSpeed { get { return 4; } }
        public override byte aFrames { get { return 4; } }
        public override int EventRange { get { return 6; } }

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

            // 같은 맵에서 가까이 있을 때만 걸어 준다.
            if (player.m_Channel != this.m_Channel || player.Map != this.m_Map || World.Dist2d(player.Loc, this.m_Loc) > EventRange)
            {
                player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "도우미에게 가까이 가세요.").Compile());
                return true;
            }

            ulong cost = Settings.HelperBuffCost;
            if (player.Gold < cost)
            {
                player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("골드가 부족합니다. ({0:N0}골드 필요)", cost)).Compile());
                return true;
            }

            // 시전자가 없는 마법이므로 요구 능력치를 없애고 정해진 권수로 건다.
            // (Spell.Level은 시전자의 능력치가 모자라면 권수를 낮춰 돌려주므로, 요구치를 0으로 둬야 권수가 고정된다)
            var spell = new script.spells.Butterfly();
            spell.Caster = player;
            spell.LevelReq = spell.LevelReqPl = 0;
            spell.StrReq = spell.StrReqPl = 0;
            spell.MenReq = spell.MenReqPl = 0;
            spell.DexReq = spell.DexReqPl = 0;
            spell.m_Level = Settings.HelperBuffLevel;

            // 이미 걸려 있으면 지우고 다시 걸어 지속시간을 처음부터 센다.
            foreach (var old in player.m_Buffs.Where(xe => xe.Value.Name == spell.Name).ToList())
            {
                script.spells.Spell removed;
                player.m_Buffs.TryRemove(old.Key, out removed);
            }

            // 마법이 걸린 것을 확인한 뒤에 골드를 차감한다.
            if (!player.AddBuff(spell))
            {
                player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0} 마법이 실패하였습니다", spell.Name)).Compile());
                return true;
            }

            player.Gold -= cost;
            player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("{0:N0}골드를 내고 {1} 마법을 받았습니다.", cost, spell.Name)).Compile());
            return true;
        }
    }
}
