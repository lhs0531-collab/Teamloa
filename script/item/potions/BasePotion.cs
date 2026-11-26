using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.model;
namespace LKCamelot.script.item
{
    public enum PotionEffect
    {
    }

    public class BasePotion : Item
    {
        private PotionEffect m_PotionEffect;
        public PotionEffect PotionEffect
        {
            get
            {
                return m_PotionEffect;
            }
            set
            {
                m_PotionEffect = value;
          //      InvalidateProperties();
            }
        }
        /*

         */
        public override float ValidTime { get { return 1 * Settings.Minute; } }
        public override short Limit { get { return 200; } }
        public override void Event_Give(Player player, Item itemdragdrop){ }

        public override void Use(Player player)
        {
            if (Quantity <= 1)
                Delete(player);
            else
            {
                base.Quantity--;
                player.client.SendPacket(new model.AddItemToInventory2(this).Compile());
            }
        }

        public BasePotion(int itemID) : base(itemID)
        {
        }

        public BasePotion(Serial serial) : base(serial)
        {
        }

        public override string ParsedStats
        {
            get
            {
                string ret = "";
                ret += Name + " : " + Quantity;

                return ret;
            }
        }



        #region V2
        public override void PickUp(Player player)
        {
            var slot = player.GetFreeSlot();

            if (slot != -1)
            {
                var item = player.Inventory.Where(xe => xe.Name == this.Name && xe.InvSlot < 24 && xe.Stage == this.Stage).FirstOrDefault();

                if (item != null && this.Stackable)
                {
                    if ((item.Quantity + this.Quantity) > item.Limit)            // 바닥 갯수와 인벤 갯수가 24개가 넘어
                    {
                        int Remainder = item.Limit - item.Quantity;               // 소유 가능한 갯수를 구함      
                        if (Remainder == 0) return;

                        this.Quantity -= Remainder;                              // 바닥에 떨어진 아이템 갯수를  삭제
                        item.Quantity += Remainder;                         // 줍는다.

                        if (this.Parent != null)
                            this.Parent.client.SendPacket(new AddItemToInventory2(this).Compile());
                        item.Parent.client.SendPacket(new AddItemToInventory2(item).Compile());
                    }
                    else
                    {
                        item.Quantity += this.Quantity;                   // 아이템을 주워 넣는다.

                        // 바닥 아이템 삭제
                        this.DeleteGround();
                        item.Parent.client.SendPacket(new AddItemToInventory2(item).Compile());
                    }
                }
                else
                {
                    DropTime = 0;
                    World.RemoveDynamicObj(m_Map, m_Serial);
                    Parent = player;
                    Loc = null;
                    m_Map = "inventory";
                    InvSlot = slot;
                    // World.DBConnect.WriteQue.Enqueue(() => World.DBConnect.UpdateItem(this));
                }
            }
            else
            {

            }
        }
        #endregion

        /// <summary>
        /// 아이템을 생성한 후, 인벤토리에 넣는다.
        /// </summary>
        /// <param name="play">아이템 지급할 케릭터</param>
        /// <returns></returns>
        public override Item Inventory(Player play)
        {
            int slot = play.GetFreeSlot();
            if (slot == -1) return null;

            var item = play.Inventory.Where(xe => xe.Name == this.Name && xe.InvSlot < 24 && xe.Stage == this.Stage).FirstOrDefault();             //기존에 있던 템
            if (item != null && this.Stackable)
            {
                if (item.Quantity + this.Quantity > item.Limit)
                {
                    int Amount = item.Limit - item.Quantity;            // 전체에서 가지고 있는 수를 뺌
                    item.Quantity += Amount;
                    this.Quantity -= Amount;

                    if (this.Parent != null)
                        this.Parent.client.SendPacket(new AddItemToInventory2(this).Compile());
                    item.Parent.client.SendPacket(new AddItemToInventory2(item).Compile());

                    return null;
                }

                else
                {
                    item.Quantity += this.Quantity;
                    play.client.SendPacket(new AddItemToInventory2(item).Compile());

                    this.Delete(play);
                    if (this.Parent != null)
                        this.Parent.client.SendPacket(new DeleteItemSlot((byte)this.InvSlot).Compile());
                    return item;
                }
            }
            else
            {
                DropTime = 0;
                this.Parent = play;
                this.Loc = null;
                this.m_Map = "inventory";
                this.InvSlot = slot;
                play.client.SendPacket(new AddItemToInventory2(this).Compile());
                World.NewItems.AddOrUpdate(this.m_Serial, this, (key, value) => value = this);

                return this;
            }
        }
    }
}
