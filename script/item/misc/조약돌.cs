using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public class 조약돌 : Item
    {
        public override string Name { get { return "조약돌"; } }
        public override int SellPrice { get { return 10; } }
        public override short Limit { get { return 200; } }

        public 조약돌() : base(128)
        {

        }

        public 조약돌(Serial serial)
            : base(serial)
        {
            m_ItemID = 128;
        }
        public override void Event_Give(Player player, Item itemdragdrop)
        {
        }
    }
}
