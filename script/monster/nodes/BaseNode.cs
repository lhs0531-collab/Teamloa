
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.library;
using LKCamelot.model;
namespace LKCamelot.script.monster
{
    public class BaseNode : monster.Monster
    {
        public int Hits = 0;

        public virtual int aFrames { get { return 0; } }
        public virtual int aStartFrames { get { return 0; } }

        public override void TakeDamage(Player player, spells.Spell spell) { }
        public override void TakeDamage(Player player)
        {
            if (!player.Equipped2.Where(xe=>xe.Value.GetType() == typeof(script.item.MinerSuit)).Any())
                return;
            
            Hits++;
            if (Hits % 10 == 0)
            {
                if (player.MinerLevel < Level - 5)
                {
                    try
                    {
                        player.MinerXP = checked(player.MinerXP - this.XP);
                    }
                    catch (OverflowException)
                    {
                        player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, string.Format("레벨이 부족합니다.")).Compile());
                    }
                }
                else
                {
                    try
                    {
                        var item = DropLoot(player);

                        if (item != null)
                        {
                            if (item.Stage == 1)
                                player.MinerXP = checked(player.MinerXP + this.XP);
                            else if (item.Stage == 2)
                                player.MinerXP = checked(player.MinerXP + (int)(0.80 * this.XP) );
                            else
                                player.MinerXP = checked(player.MinerXP + (int)(0.60 * this.XP ) );
                        }

                        if (Hits >= 100)
                        {
                            Alive = false;
                            Hits = 0;
                        }
                    }
                    catch (OverflowException)
                    {
                        player.client.SendPacket(new UpdateChatBox(0x00, 0xff, 1, "더 이상 경험치를 누적할 수 없습니다.").Compile());
                    }
                }
            }
        }


        public script.item.Item DropLoot(Player play)
        {
            if (Loot != null)
            {
                foreach (var loot in Loot.m_Entries)
                {
                    if (loot.TryDrop())
                    {
                        var inst = Activator.CreateInstance(Type.GetType(loot.t_item.ToString()));

                        if (play.AutoLoot)
                        {
                            if (play.GetFreeSlots() > 0)
                            {
                                inst = (inst as script.item.Item).Inventory(play);
                                string itdrop = this.Name + " gave you " + (inst as script.item.Item).Name + ".";
                                play.client.SendPacket(new UpdateChatBox(0xff, 0xff, 1, itdrop).Compile());
                                play.client.SendPacket(new LKCamelot.model.AddItemToInventory2((inst as script.item.Item)).Compile());
                            }
                            else
                            {
                                (inst as script.item.Item).Drop(play);
                            }
                            World.NewItems.TryAdd((inst as script.item.Item).m_Serial, (inst as script.item.Item));
                        }
                        else
                        {
                            if (!(inst as script.item.BaseOre).IsJewelry)
                            {
                                if (play.MinerLevel >= this.Level)                                                      // 최상
                                    (inst as script.item.BaseOre).Stage = 1;
                                else if (play.MinerLevel >= this.Level - 2 && play.MinerLevel >= this.Level - 1)        // 중간
                                    (inst as script.item.BaseOre).Stage = 2;
                                else                                                                                    // 하위
                                    (inst as script.item.BaseOre).Stage = 3;
                            }
                            else
                                (inst as script.item.BaseOre).Stage = 1;

                            (inst as script.item.Item).Drop(play);
                            World.NewItems.TryAdd((inst as script.item.Item).m_Serial, (inst as script.item.Item));
                            World.SendToAll(new QueDele(play.m_Channel, play.Map, new CreateItemGround2((inst as script.item.Item), (inst as script.item.Item).m_Serial).Compile()));
                        }
                        return (inst as script.item.Item);
                    }
                }
            }
            return null;
        }

        public BaseNode(Serial serial)
        {
            this.m_Serial = serial;
            HPCur = HP;
        }

        public BaseNode()
        {
            if (m_Serial == 0)
                m_Serial = Serial.NewMobile;
            HPCur = HP;
        }
    }
}
