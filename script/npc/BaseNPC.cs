using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.npc
{
    public class BaseNPC :BaseObject
    {
        public List<string> Chat;
        public long LastChat = 0;
        public long LastSpawn;
        public virtual bool Alive { get; set; }

        public virtual string Name { get { return ""; } }
        public virtual List<string> ChatString { get { return null; } }
        public virtual Point2D Move { get { return new Point2D(0, 0); } }

        public virtual byte Sprite { get { return 0; } }

        public virtual bool Animation { get { return true; } }
        public virtual byte aSpeed { get { return 0; } }
        public virtual byte aFrames { get { return 0; } }
        public virtual byte aStartFrames { get { return 0; } }
        public virtual byte Type { get { return 0x0A; } }
        public int BuyList = 0;
        public virtual GUMP Gump { get { return null; } }


        public virtual int AliveTime { get { return 30 * Settings.Minute + (Util.Random(0, 5) + Settings.Minute); } }          // 살아있는 시간
        public virtual int reSpawnTime { get { return 30 * Settings.Second + (Util.Random(0, 10) + Settings.Second); } }
        public virtual int ChatTime { get { return 30 * Settings.Second + (Util.Random(0, 10) + Settings.Second); } }
        public virtual int MessageGap { get { return 10 * Settings.Second; } }                                                  // 현재 메시지와 다음 메세지의 간격
        public virtual int EventRange { get { return 10; } }

        public virtual void Buy(Player player, int buyslot)
        {
        }
        public virtual void RefreshItem()
        {}

        /// <summary>
        /// 겹친 아이템
        /// Player가 NPC에게 아이템을 건낸다.
        /// </summary>
        /// <param name="player">건내는 플레이어</param>
        /// <param name="giveitem">건내는 아이템</param>
        public virtual void Event_Give(Player player, item.Item giveitem, int amount)
        {
            Event_Chat("감사합니다.");

            if (giveitem.Quantity <= amount)
            {
                giveitem.Delete(player);
            }
            else
            {
                giveitem.Quantity -= amount;
                player.client.SendPacket(new AddItemToInventory2(giveitem).Compile());
            }
        }

        /// <summary>
        /// 플레이어가 NPC를 눌렀을 때. 직접 처리했으면 true, 기본 동작(상점창 열기)에 맡기려면 false.
        /// </summary>
        public virtual bool Event_Click(Player player)
        {
            return false;
        }

        public virtual void TakeDamage(Player player)
        {
            Event_Chat("때리지마세요. 아픕니다.");
        }
        public virtual void Event_Message(Player player, string[] str2, string str)
        {
        }
        public virtual void Event_Chat(string message, bool flag = false)
        {
            if (flag == false)
            {
                if (Alive == true && Server.tickcount.ElapsedMilliseconds - LastChat > MessageGap)
                {
                    LastChat = Server.tickcount.ElapsedMilliseconds;
                    World.SendToRange(new QueDele(this.m_Channel, this.m_Map, new UpdateCharChatBox(this.m_Serial, string.Format("{0}: {1}", this.Name, message)).Compile()), this);
                }
            }
            else
            {
                World.SendToRange(new QueDele(this.m_Channel, this.m_Map, new UpdateCharChatBox(this.m_Serial, string.Format("{0}: {1}", this.Name, message)).Compile()), this);
            }
        }

        public BaseNPC(Serial serial) : base(serial)
        {
            this.Alive = true;
            LastSpawn = Server.tickcount.ElapsedMilliseconds + Util.Random(Settings.Minute, 3 * Settings.Minute);
        }

    }

    public class GUMP
    {
        public int ID;
        public ushort Titlec;
        public ushort Boxc;
        public byte Time;
        public string ShopName;
        public List<item.Item> SellList;

        public GUMP()
        {
        }

        public GUMP(int ID, ushort titlec, ushort boxc, byte time, string shopname, List<item.Item> SellList)
        {
            this.ID = ID;
            this.Titlec = titlec;
            this.Boxc = boxc;
            this.Time = time;
            this.ShopName = shopname;
            this.SellList = SellList;
        }

        public byte[] ItemIDArray
        {
            get
            {
                byte[] list = new byte[32];
                int itr = 0;
                foreach (var sell in SellList)
                {
                    list[itr + 1] = (byte)(sell.m_ItemID >> 8);
                    list[itr] = (byte)sell.m_ItemID;

                    itr += 2;
                }
                return list;
            }
        }

        public string ItemNameString
        {
            get
            {
                string temp = "";
                foreach (var item in SellList)
                {
                    temp += item.BuyPrice +" "+item.Name + " ,";
                }
                return temp;
            }
        }

        public byte[] ItemNameArray
        {
            get
            {
                byte[] temp = new byte[512];
                int itr = 0;
                foreach (var item in SellList)
                {
                    var stemp = item.Name;
                    if (item != SellList.Last())
                        stemp += ",";
                    System.Buffer.BlockCopy(stemp.ToCharArray(), 0, temp, itr, stemp.Length);
                    itr += stemp.Length;
                }

                Array.Resize(ref temp, itr);
                return temp;
            }
        }
    }
}
