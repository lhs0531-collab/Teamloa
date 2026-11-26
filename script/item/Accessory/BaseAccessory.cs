using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.item
{
    public enum AccessoryType
    {
        Ring = 1,
        Amulet = 2,
    }
    public class BaseAccessory : Item
    {
        public virtual AccessoryType Type { get { return 0; } }
        public override bool IsBreak { get { return true; } }                       // 깨짐여부

        public BaseAccessory(int itemID)
            : base(itemID)
        {
            //0~4
            var roll = Util.Random(1, 10000);

            if (roll <= 1)               // 0.01%
                this.Stage = 4;
            else if (roll <= 100)       // 1%
                this.Stage = 3;
            else if (roll <= 500)       // 5%
                this.Stage = 2;
            else if (roll <= 1000)      // 10%
                this.Stage = 1;
            else
                this.Stage = 0;
        }
        public BaseAccessory(LKCamelot.model.Serial serial)
            : base(serial)
        {
        }


        public override string ParsedStats
        {
            get
            {
                string ret = "";

                ret += Name + " : " + Quantity + "\n\t";
                if (this.LevelReq != 0)
                    ret += "필요레벨: " + this.LevelReq + "\n\t";

                if (this.StrBonus != 0)
                    ret += "추가힘: " + this.StrBonus + "\n\t";
                if (this.MenBonus != 0)
                    ret += "추가지력: " + this.MenBonus + "\n\t";
                if (this.DexBonus != 0)
                    ret += "추가숙련: " + this.DexBonus + "\n\t";
                if (this.VitBonus != 0)
                    ret += "추가생명: " + this.VitBonus + "\n\t";

                if (IsBreak)
                    ret += "내구도: " + this.Durability + "/" + this.InitMaxHits + "\n\t";
                if (SellPrice != 0)
                    ret += string.Format("매매가격: {0}\n\t", SellPrice);
                if (DescText != null)
                    ret += "Special: " + DescText + "\n\t";
                if (FlavorText != null)
                    ret += "\n\t  " + FlavorText + "\n\t";


                if (ret != "")
                    ret = ret.Substring(0, ret.Count() - 2);
                return ret;
            }
        }

    }
}
