using LKCamelot.library;
using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    class MinerSuit : BaseArmor
    {
        public Class TempClass { get; set; }

        public override string Name { get { return "Miner"; } }

        public override int InitMinHits { get { return 80; } }
        public override int InitMaxHits { get { return 80; } }
		public override int DamBase { get { return 0; } }
        public override ulong BuyPrice { get { return 3000; } }

		public override Class ClassReq { get { return 0; } }
		public override ArmorType ArmorType { get { return ArmorType.Armor; } }

        public override void Equip(Player player)
        {
            if (World.NewItems.Where(xe => xe.Value.m_Parent != null && xe.Value.m_Parent == player && xe.Value.InvSlot == 28)
                .FirstOrDefault().Value == null)
            {
                if (Parent.GetStat("str") >= StrReq && Parent.GetStat("men") >= MenReq && Parent.GetStat("dex") >= DexReq && Parent.Level >= LevelReq)
                {
                    InvSlot = 28;
                    Parent.Equipped2.TryAdd(this.m_Serial, this);
                    Parent.client.SendPacket(new UpdateCharStats(Parent).Compile());
                    World.SendToAll(new QueDele(player.m_Channel, player.Map, new ChangeObjectSpritePlayer(Parent).Compile()));
                    World.SendToAll(new QueDele(player.m_Channel, player.Map, new SetObjectEffects(player).Compile()));
                    try
                    {
                        //   World.DBConnect.WriteQue.Enqueue(() => World.DBConnect.UpdateItem(this));
                        //    World.DBConnect.UpdateItem(this);
                    }
                    catch { Console.WriteLine("Update Item on Equip Failed"); }
                }
            }
        }
        public override void Unequip(Player player, int slot)
        {
            base.Unequip(player, slot);
        }

		public MinerSuit() : base (5)
		{
		}

        public MinerSuit(Serial serial)
            : base(serial)
		{
            m_ItemID = 5;
		}

    }
}
