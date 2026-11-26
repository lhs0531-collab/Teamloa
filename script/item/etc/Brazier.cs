using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class Brazier : Item
    {
        public override string Name { get { return "화로"; } }
        public override ulong BuyPrice { get { return 3000; } }

        public Brazier() : base(37)
        {

        }

        public Brazier(Serial serial)
            : base(serial)
        {
            m_ItemID = 37;
        }

        public override void Event_Give(Player player, Item itemdragdrop)
        {
            if (itemdragdrop is script.item.성냥 && this is script.item.Brazier)
            {
                var brazier = new script.npc.Brazier(player, this.Loc);
                World.NewNpcs[this.m_Channel].TryAdd(brazier.m_Serial, brazier);

                this.DeleteGround();
                itemdragdrop.Delete(player);
            }
        }
    }
}
