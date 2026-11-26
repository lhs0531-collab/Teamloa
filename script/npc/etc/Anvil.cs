using LKCamelot.library;
using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class Anvil : BaseInstance, IDisposable
    {
        public script.item.Item Upgrade;

        int reqLevel = 0, GiveXP = 0;
        string Enable = string.Empty;
        script.item.Item Result;
        public Dictionary<string, script.item.Item> it = new Dictionary<string, item.Item>();

        public override string Name { get { return "모루"; } }
        public override byte Sprite { get { return 10; } }
        public override byte aSpeed { get { return 1; } }                 // 프레임 속도
        public override byte aFrames { get { return 4; } }                // 프레임수

        public Anvil(Player player, Point2D loc)
            : base(player, loc, Serial.NewMobile)
        {
            World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new CreateGem(this).Compile()));
        }

        public override bool Alive
        {
            get { return base.Alive; }
            set
            {
                if (base.Alive == value) return;

                if (value == false)
                {
                    script.item.Item item = new script.item.Anvil();
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


        public override void Event_Give(Player player, item.Item giveitem, int amount)
        {
            Hits = 0;

            if ((giveitem is script.item.BaseArmor) || (giveitem is script.item.BaseWeapon))
            {
                if (Upgrade != null)
                    return;
                Upgrade = giveitem.Clone();
                giveitem.Delete(player);
            }
            else if (giveitem is script.item.BasePotion)
            {
                Alive = false;
                World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new DeleteObject(this.m_Serial).Compile()));
                this.Dispose();
            }
            //장작일경우
            else if (giveitem is script.item.장작)
            {
                if (giveitem.Quantity <= amount)
                {
                    giveitem.Delete(player);
                    SetTime(20 * Settings.Second * amount);
                }
                else
                {
                    giveitem.Quantity -= amount;
                    SetTime(20 * Settings.Second * amount);
                    player.client.SendPacket(new AddItemToInventory2(giveitem).Compile());
                }
            }
            // 보석일 경우
            else if (!(giveitem is script.item.BaseOre && !(giveitem as script.item.BaseOre).IsJewelry))
            {
                if (giveitem.Quantity <= amount)
                {
                    var item = it.Where(xe => xe.Key == giveitem.Name).FirstOrDefault().Value;
                    if (item == null)
                    {
                        script.item.Item clone = giveitem.Clone();
                        clone.Parent = null;
                        clone.ParSer = 0;
                        it.Add(giveitem.Name, clone);
                    }
                    else if (item != null && item.Quantity + amount <= item.Limit)
                        item.Quantity += amount;
                    else
                        return;
                    giveitem.Delete(player);
                }
                else
                {
                    int gap = giveitem.Quantity - amount;

                    var item = it.Where(xe => xe.Key == giveitem.Name).FirstOrDefault().Value;
                    if (item == null)
                    {
                        var its = giveitem.Clone();
                        its.Quantity = amount;
                        its.Parent = null;
                        its.ParSer = 0;
                        giveitem.Quantity = gap;

                        it.Add(its.Name, its);
                    }

                    else if (item != null && item.Quantity + amount <= item.Limit)
                    {
                        item.Quantity += amount;
                        giveitem.Quantity = giveitem.Quantity - amount;
                    }
                    else
                        return;
                    player.client.SendPacket(new AddItemToInventory2(giveitem).Compile());
                }
            }
        }

        public override void TakeDamage(Player player)
        {
            if (!player.Equipped2.Where(xe=>xe.Value.GetType() == typeof(script.item.BlackSmithSuit)).Any())
                return;



            if (Enable == string.Empty)
                Enable = Process(player, ref Result, ref reqLevel, ref GiveXP);
            else if (Enable != string.Empty && reqLevel <= player.BSLevel)
                Hits++;
            else
                return;

            if (Enable != string.Empty)
            {
                #region 멘트
                if (Hits == 1)
                {
                    Event_Chat(Enable + " 아이템 제작을 시작합니다.", true);
                }
                else if (Hits == 10)
                {
                    Event_Chat("영차~! 영차~!", true);
                }
                else if (Hits == 20)
                {
                    Event_Chat("거의 완성됬습니다.", true);
                }
                #endregion
                else if (Hits != 0 && Hits % 30 == 0)
                {
                    if (Result != null)
                    {
                        try
                        {
                            if (Upgrade != null)
                                Upgrade.Delete(player);

                            player.BSXP = checked(player.BSXP + GiveXP);
                            Result.Drop(player);
                            Event_Chat(Enable + " 제작에 성공하였습니다.", true);
                        }
                        catch (OverflowException)
                        {
                            Event_Chat(Enable + "경험치를 누적할 수 없습니다.", true);
                        }
                    }
                    else
                    {
                        if (Upgrade != null && Upgrade.Stage <= 3)
                        {
                            Upgrade.Stage = 0;
                            Upgrade.Drop(player);
                        }
                        else if (Upgrade != null)
                            Upgrade.Delete(player);
                        Event_Chat("아이템 제작에 실패하였습니다.", true);
                    }
                    Upgrade = Result = null;
                    Enable = string.Empty;
                    GiveXP = 0;
                    Hits = 0;
                    reqLevel = 0;
                }
            }
            else
                Event_Chat("재료가 부족합니다.");
        }

        private string Process(Player player, ref item.Item Result, ref int reqLevel, ref int GiveXP)
        {

            if (Upgrade != null)
            {
                if (Upgrade.Stage >= 10)
                    return string.Empty;

                #region 방어구
                if (Upgrade is script.item.BaseArmor)
                {
                    var item = it.Where(xe=> xe.Value is script.item.BaseOre && (xe.Value as script.item.BaseOre).IsJewelry && xe.Value.Name == Settings.BSArmor[Upgrade.Stage] ).FirstOrDefault();

                    if (item.Value == null || Settings.BSUpgradeLevelTable[Upgrade.Stage] > player.BSLevel)
                        return string.Empty;

                    if (Upgrade.Stage >= 9)
                        item.Value.Quantity -= 4;
                    else if (Upgrade.Stage >= 8)
                        item.Value.Quantity -= 3;
                    else if (Upgrade.Stage >= 7)
                        item.Value.Quantity -= 2;
                    else
                        item.Value.Quantity -= 1;


                    if (item.Value.Quantity < 1)
                        it.Remove(item.Value.Name);

                    Result = Upgrade.Clone();
                    if (!Result.TryUpgrade())
                    {
                        Result.Delete(player);
                        Result = null;
                    }
                    return string.Format("{0} {1}", Upgrade.NPrefix(Upgrade.Stage + 1), Upgrade.Name);
                }
                #endregion

                #region 무기
                else if (Upgrade is script.item.BaseWeapon)
                {
                    var item = it.Where(xe => xe.Value is script.item.BaseOre && (xe.Value as script.item.BaseOre).IsJewelry && xe.Value.Name == Settings.BSWeapons[Upgrade.Stage]).FirstOrDefault();

                    if (item.Value == null || Settings.BSUpgradeLevelTable[Upgrade.Stage] > player.BSLevel)
                        return string.Empty;

                    if (Upgrade.Stage >= 9)
                        item.Value.Quantity -= 4;
                    else if (Upgrade.Stage >= 8)
                        item.Value.Quantity -= 3;
                    else if (Upgrade.Stage >= 7)
                        item.Value.Quantity -= 2;
                    else
                        item.Value.Quantity -= 1;


                    if (item.Value.Quantity < 1)
                        it.Remove(item.Value.Name);

                    Result = Upgrade.Clone();
                    if (!Result.TryUpgrade())
                    {
                        Result.Delete(player);
                        Result = null;
                    }
                    return string.Format("{0} {1}", Upgrade.NPrefix(Upgrade.Stage + 1), Upgrade.Name);
                }
                #endregion

                else
                    return string.Empty;
            }
            else if (Upgrade == null && it.Count() != 0)
            {
                #region 아이템 만들기 V2
                //foreach (var table in Settings.BSItemsTable2.Reverse())
                //{
                //    bool check = false;

                //    //bool t = table.Value.Keys.ToList().Contains(it.Values);
                //}
                #endregion


                // 아이템 만들기
                #region 아이템 만들기 V1
                foreach (var table in Settings.BSItemsTable.Reverse())
                {
                    bool check = false;
                    // 리스트가 같은지 비교
                    if ( World.ArrayEqual(table.Value.Keys.ToList(), it.Keys.ToList()) )
                    {
                        check = true;
                        var key = table.Key.Split(',');

                        if (Convert.ToInt32(key[0]) > player.BSLevel)
                        {
                            check = false;
                            continue;
                        }

                        Dictionary<string, script.item.Item> copy = new Dictionary<string, item.Item>(it);
                        foreach (var ii in copy)
                        {
                            int decrease;
                            table.Value.TryGetValue(ii.Key, out decrease);
                            if (ii.Value.Quantity < decrease)
                            {
                                check = false;
                                break;
                            }
                        }
                        if (check == false)
                            continue;


                        reqLevel = Convert.ToInt32(key[0]);
                        foreach (var ii in copy)
                        {
                            int decrease;
                            table.Value.TryGetValue(ii.Key, out decrease);

                            it[ii.Key].Quantity -= decrease;
                            if (it[ii.Key].Quantity < 1)
                                it.Remove(ii.Key);
                        }

                        Result = (script.item.Item)Activator.CreateInstance(System.Type.GetType("LKCamelot.script.item." + key[3]));

                        var roll = Util.Random(1, 10000);
                        if (roll > Convert.ToInt32(key[2]))
                        {
                            string re = Result.NPrefix() + " " + Result.Name;
                            Result = null;
                            return re;
                        }   
                        
                        GiveXP = Convert.ToInt32(key[1]);
                        World.NewItems.TryAdd(Result.m_Serial, Result);
                        return Result.NPrefix() + " " + Result.Name;
                    }
                }
                return string.Empty;
                #endregion
            }
            return string.Empty;
        }

        public override void Dispose()
        {
            if (it == null)
                return;

            foreach (var i in it.Values)
                i.Drop(this.m_Channel, this.m_Loc, this.m_Map);
            it.Clear();


            if (Upgrade != null)
                Upgrade.Drop(this.m_Channel, this.m_Loc, this.m_Map);
        }
    }
}
