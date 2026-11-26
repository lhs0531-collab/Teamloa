using LKCamelot.library;
using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class Brazier : BaseInstance, IDisposable
    {
        public script.item.Item it;

        public override string Name { get { return "화덕"; } }
        public override byte Sprite { get { return 9; } }
        public override byte aSpeed { get { return 1; } }                 // 프레임 속도
        public override byte aFrames { get { return 4; } }                // 프레임수

        public Brazier(Player player, Point2D loc)
            : base(player, loc, Serial.NewMobile)
        {
            World.SendToAll(new QueDele(player.m_Channel, this.m_Map, new CreateGem(this).Compile()));
        }


        public override bool Alive
        {
            get { return base.Alive; }
            set {
                if (base.Alive == value) return;

                if (value == false)
                {
                    script.item.Item item = new script.item.Brazier();
                    script.npc.BaseNPC npc;

                    World.NewItems.TryAdd(item.m_Serial, item);
                    World.NewNpcs[this.m_Channel].TryRemove(this.m_Serial, out npc);

                    item.Drop(this.m_Channel, this.m_Loc, this.m_Map);

                    World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new CreateItemGround2(item, item.m_Serial).Compile()));
                    World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new DeleteObject(this.m_Serial).Compile()));
                }
                    
                base.Alive = value; 
            
            }

        }

        public override void Event_Give(Player player, item.Item giveitem, int amount = 1)
        {
            if (giveitem is script.item.장작)
            {
                SetTime(20 * Settings.Second * amount);
                if (giveitem.Quantity <= amount)
                    giveitem.Delete(player);
                else
                {
                    giveitem.Quantity -= amount;
                    player.client.SendPacket(new AddItemToInventory2(giveitem).Compile());
                }
            }
            else if (giveitem is script.item.BasePotion)
            {
                Alive = false;
                World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new DeleteObject(this.m_Serial).Compile()));
                this.Dispose();
            }
            else if (giveitem is script.item.BaseOre && !(giveitem as script.item.BaseOre).IsJewelry)
            {
                if (it == null)
                {
                    it = giveitem.Clone();
                    it.Parent = null;
                    it.ParSer = 0;

                    if (giveitem.Quantity <= amount)
                    {
                        giveitem.Delete(player);
                    }
                    else
                    {
                        it.Quantity = amount;
                        giveitem.Quantity -= amount;
                        player.client.SendPacket(new AddItemToInventory2(giveitem).Compile());
                    }
                }
                else
                {
                    if (it.Name == giveitem.Name && it.Stage == giveitem.Stage)
                    {
                        if (it.Quantity + amount > it.Limit)
                            return;

                        else if (giveitem.Quantity <= amount)
                        {
                            it.Quantity += amount;
                            giveitem.Delete(player);
                        }
                        else
                        {
                            it.Quantity += amount;
                            giveitem.Quantity -= amount;
                            player.client.SendPacket(new AddItemToInventory2(giveitem).Compile());
                        }
                    }
                }
            }
        }

        public override void TakeDamage(Player player)
        {
            if (!player.Equipped2.Where(xe=>xe.Value.GetType() == typeof(script.item.MinerSuit)).Any())
                return;

            
            if (it != null &&
                ((it.Stage == (int)LKCamelot.script.item.BaseOre.OreTypeE.PB && it.Quantity >= 3) ||
                 (it.Stage == (int)LKCamelot.script.item.BaseOre.OreTypeE.PN && it.Quantity >= 2) ||
                 (it.Stage == (int)LKCamelot.script.item.BaseOre.OreTypeE.PG && it.Quantity >= 1)
                ))
                Hits++;
            else
            {
                player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "재료가 없습니다.").Compile());
            }
            
            if (Hits != 0 && Hits % 10 == 0)
            {                
                var drop = (it as script.item.BaseOre).OreTaught;
                World.NewItems.TryAdd(drop.m_Serial, drop);
                try
                {
                    player.MinerXP = checked(player.MinerXP + (drop as script.item.BaseOre).XP);
                    drop.Drop(player);

                    World.SendToAll(new QueDele(player.m_Channel, player.Map, new CreateItemGround2(drop, drop.m_Serial).Compile()));
                    Hits = 0;
                }
                catch (OverflowException)
                {
                    player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "경험치를 누적할 수 없습니다.").Compile());
                }
                
                it.Quantity -= it.Stage;
                if (it.Quantity < 1)
                    it = null;
            }
        }

        public override void Dispose()
        {
            if (it == null)
                return;

            it.Drop(this.m_Channel, this.m_Loc, this.m_Map);
        }


    }
}
