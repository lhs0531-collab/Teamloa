using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Anvil : Item
    {
        public override string Name { get { return "모루"; } }
        public override ulong BuyPrice { get { return 3000; } }


        public Anvil() : base(38)
        {
        }

        public Anvil(Serial serial)
            : base(serial)
        {
            m_ItemID = 38;
        }
        public override void Event_Give(Player player, Item itemdragdrop)
        {
            if (itemdragdrop is script.item.성냥 && this is script.item.Anvil)
            {
                var brazier = new script.npc.Anvil(player, this.Loc);
                World.NewNpcs[this.m_Channel].TryAdd(brazier.m_Serial, brazier);

                this.DeleteGround();
                itemdragdrop.Delete(player);
            }
        }
    }
}
