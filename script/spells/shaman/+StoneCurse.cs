using LKCamelot.model;
namespace LKCamelot.script.spells
{
    public class StoneCurse : Spell
    {
        public override string Name { get { return "STONE CURSE"; } }
        public override int SpellLearnedIcon { get { return 38; } }
        public override LKCamelot.library.MagicType mType { get { return LKCamelot.library.MagicType.Target2; } }

        public override int DamBase { get { return 0; } }
        public override int DamPl { get { return 0; } }
        public override int ManaCost { get { return -10; } }
        public override int ManaCostPl { get { return -5; } }
        public override Buff BuffEffect
        {
            get
            {
                var tbuff = new Buff();
                tbuff.BuffType = BuffCase.Freezing;
                tbuff.Time = 10 * Settings.Second;
                tbuff.TimePI = 2 * Settings.Second;
                return tbuff;
            }
        }
        public override SpellSequence Seq
        {
            get
            {
                return new SpellSequence(
                    61,  //oncast
                    0,  //moving
                    0,  //impact
                    0,  //thickness
                    0,  //type
                    0,  //speed
                    0  //streak
                    );
            }
        }

        public StoneCurse() : base(new script.item.StoneCurseBook(1))
        {
        }
        public override void UseSpell(model.Player player, int castonid, short castx, short casty)
        {
            var Target = SerialPlayerTarget(player.m_Channel, castonid);

            if (Target != null)
            {
                // 유저 간에는 쌍방이 @대인공격 ON일 때만 걸 수 있다.
                if (!player.PK || !Target.PK)
                {
                    player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "서로 대인공격이 ON인 상태에서만 사용할 수 있습니다.").Compile());
                    return;
                }

                // PK 불가 맵에서는 걸 수 없다. 대상이 다른 맵(안전지대 포함)에 있을 때도 걸 수 없다.
                model.Map.MapAttribute currMap = null;
                if (player.Map != null)
                    model.Map.FullMaps[player.m_Channel].TryGetValue(player.Map, out currMap);
                if (currMap == null || currMap.PKable == model.Map.PKType.ImpossiblePK || Target.Map != player.Map)
                {
                    player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "이곳에서는 사용할 수 없습니다.").Compile());
                    return;
                }

                if (!Target.AddBuff(this))
                    player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "마법이 실패하였습니다").Compile());
            }
            World.SendToAll(new QueDele(player.m_Channel, player.Map, new MagicMotion(player).Compile()));
        }
    }
}