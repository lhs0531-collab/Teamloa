using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.library;
using LKCamelot.model;
namespace LKCamelot.script.item
{
    public class BaseOre : Item
    {
        public enum OreTypeE
        {
            PB = 3,
            PN = 2,
            PG = 1,
        }

        public int Hits = 0;
        public virtual int XP { get { return 0; } }
        public override bool Stackable { get { return true; } }
        public virtual bool IsJewelry { get { return false; } }
        public override void Event_Give(Player player, Item itemdragdrop){ }
        public virtual script.item.Item OreTaught { get { return null; } }

        public BaseOre(int itemID)
            : base(itemID)
        {
            m_ItemID = SetSprite(itemID);
        }

        public BaseOre(Serial serial)
            : base(serial)
        {
        }

        public override int Stage { 
            get { return m_Stage; }
            set {
                m_Stage = value;
                if(!IsJewelry)
                    m_ItemID = SetSprite(m_ItemID);
            }
        }

        public override string ParsedStats
        {
            get
            {
                string ret = "";
                ret += Name + " : " + Quantity + "\n\t";
                if (!IsJewelry)
                    ret += "등급: " + Stage + "\n\t";
                ret += "종류: " + (IsJewelry ? "보석\n\t" : "광물\n\t");
                ret += "판매가격 : " + SellPrice;


                return ret;
            }
        }

        public int SetSprite(int ItemID)
        {
            if (IsJewelry) return ItemID;

            if (Stage == (int)OreTypeE.PG)
                return ItemID + 2;
            else if (Stage == (int)OreTypeE.PN)
                return ItemID + 1;
            else
                return ItemID;
        }
        public void RandomQuantity()
        {            
            if (!IsJewelry)
            {
                var roll = Util.Random(1, 100);
                if (roll <= 20) Stage = (int)OreTypeE.PG;
                if (roll <= 50 && roll > 20) Stage = (int)OreTypeE.PN;
                if (roll > 50) Stage = (int)OreTypeE.PB;
            }
        }
    }
}
