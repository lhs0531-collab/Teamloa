using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.library;

namespace LKCamelot.model
{
    public sealed class ShowExchangeBox : Packet
    {
        public ShowExchangeBox(Player owner, Player target)
            : base(0x47)
        {
            for (int i = 0; i < 4; i++)
            {
                if (owner.Exchange.CellItem.Count() > i)
                    m_Stream.Write((byte)owner.Exchange.CellItem[i].Item.m_ItemID);
                else
                    m_Stream.Write((byte)0);
            }

            for (int i = 0; i < 4; i++)
            {
                if (target.Exchange.CellItem.Count() > i)
                    m_Stream.Write((byte)target.Exchange.CellItem[i].Item.m_ItemID);
                else
                    m_Stream.Write((byte)0);
            }

            m_Stream.Fill(2);       // ??

            m_Stream.Write((uint)owner.Exchange.Price);  //user1 돈
            m_Stream.Write((uint)target.Exchange.Price);  //user2 돈

            m_Stream.Write((byte)(owner.Exchange.isCheck ? 0x01 : 0x00)); //user1 체크
            m_Stream.Write((byte)(target.Exchange.isCheck ? 0x01 : 0x00)); //user2 체크

            m_Stream.Write((short)Encoding.Default.GetByteCount(owner.Name + "," + target.Name));         // 한글 문자 포함 가변 길이 체크;
            m_Stream.WriteEUCKR(owner.Name + "," + target.Name);
        }
    }

    public sealed class CanselExchangeBox : Packet
    {
        public CanselExchangeBox()
            : base(0x48)
        {
            m_Stream.Fill(1);
        }
    }

    public sealed class AddItemToInventory2 : Packet
    {
        public AddItemToInventory2(LKCamelot.script.item.Item item)
            : base(0x17)
        {
            m_Stream.Write((byte)item.InvSlot);
            m_Stream.Write((short)item.m_ItemID);
            m_Stream.Write((byte)02);
            m_Stream.Write((short)Encoding.Default.GetByteCount(item.ParsedStats));
            m_Stream.WriteEUCKR(item.ParsedStats);
        }
    }
    public sealed class MagicMotion : Packet
    {
        public MagicMotion(Player player)
            : base(0x27)
        {
            m_Stream.Write((int)player.m_Serial);
            m_Stream.Write((byte)player.Face);
        }
    }

    //public sealed class Fog : Packet
    //{
    //    // 0x00~ 0x0E
    //    public Fog(byte Range)  : base(0x43)
    //    {
    //        m_Stream.Write((byte)Range);        
    //    }
    //}
    public sealed class Attrib : Packet
    {
        public Attrib(Player player)
            : base(0x41)
        {
            string name = "";
            if (player.HelperLevel != 0)
                name += "도우미:" + player.HelperLevel + ",";
            if (player.MinerXP != 0)
                name += "광부:L" + player.MinerLevel + ":" + player.MinerXP + ",";
            if (player.BSXP != 0)
                name += "대장:L" + player.BSLevel + ":" + player.BSXP + ",";
            if (player.PKCount != 0)
                name += "PK:" + player.PKCount;

            m_Stream.Write((short)Encoding.Default.GetByteCount(name));
            m_Stream.WriteEUCKR(name);
        }
    }

    public sealed class AmountBox : Packet
    {
        public AmountBox(Serial ReturnSeral, script.item.Item item)
            : base(0x46)
        {
            m_Stream.Write((int)ReturnSeral);
            m_Stream.Write((short)item.InvSlot);
            m_Stream.Write((int)item.Quantity);
            m_Stream.Fill(4);
            m_Stream.Write((short)Encoding.Default.GetBytes(item.Name).Length);         // 한글 문자 포함 가변 길이 체크;
            m_Stream.WriteEUCKR(item.Name);
        }
    }


    public sealed class AutoMove : Packet
    {
        public AutoMove(Point2D Loc)
            : base(0x3F)
        {
            m_Stream.Write((short)Loc.X);
            m_Stream.Write((short)Loc.Y);
        }
    }
    
    public sealed class AddItemToEntrust : Packet
    {
        public AddItemToEntrust(LKCamelot.script.item.Item item)
            : base(0x31)
        {
            var invslot = (item.InvSlot - 40);
            while (invslot >= 12)
                invslot -= 12;
            m_Stream.Write((byte)invslot);
            m_Stream.Write((short)item.m_ItemID);
            m_Stream.Write((byte)00);
            m_Stream.Write((short)Encoding.Default.GetByteCount(item.ParsedStats));
            m_Stream.WriteEUCKR(item.ParsedStats);
        }
    }

    public sealed class DeleteEntrustSlot : Packet
    {
        public DeleteEntrustSlot(byte slot)
            : base(0x32)
        {
            m_Stream.Write(slot);
        }
    }

    public sealed class DeleteItemSlot : Packet
    {
        public DeleteItemSlot(byte slot)
            : base(0x18)
        {
            m_Stream.Write(slot);
        }
    }

    public sealed class CreateSlotMagic2 : Packet
    {
        public CreateSlotMagic2(script.spells.Spell spell)
            : base(0x19)
        {
            m_Stream.Write((byte)spell.Slot);
            m_Stream.Write((byte)spell.mType);
            m_Stream.Write((byte)spell.Level);
            m_Stream.Write((short)spell.SpellLearnedIcon);
            m_Stream.Fill(3);
            m_Stream.Write((byte)1);
            m_Stream.WriteAsciiNull(spell.Name + " :S" + spell.m_Level);
        }
    }

    public sealed class DeleteObject : Packet
    {
        public DeleteObject(Serial obj)
            : base(0x1E)
        {
            m_Stream.Write((int)obj);
        }
    }

    public sealed class DeleteSpell : Packet
    {
        public DeleteSpell(int slot)
            : base(0x1A)
        {
            m_Stream.Write((byte)slot);
        }
    }

    public sealed class EquipItem2 : Packet
    {
        public EquipItem2(LKCamelot.script.item.Item item)
            : base(0x15)
        {
            m_Stream.Write((byte)item.EquipSlot);
            m_Stream.WriteEUCKRFixed(item.NPrefix() + " " + item.Name, 32);
            m_Stream.Write((byte)item.Stress);
            m_Stream.Write((short)item.m_ItemID);
        }
    }

    public sealed class DeleteEquipItem2 : Packet
    {
        public DeleteEquipItem2(LKCamelot.script.item.Item item)
            : base(0x16)
        {
            m_Stream.Write((byte)item.EquipSlot);
        }
    }

    public sealed class PlayMusic : Packet
    {
        public PlayMusic(int track)
            : base(0x38)
        {
            m_Stream.Fill(5);
            m_Stream.Write(track);
        }
    }

    public sealed class GoldChange : Packet
    {
        public GoldChange(ulong amount)
            : base(0x12)
        {
            m_Stream.Write(amount > int.MaxValue ? uint.MaxValue : Convert.ToUInt32(amount));
        }
    }

    public sealed class XPChange : Packet
    {
        public XPChange(int amount)
            : base(0x13)
        {
            m_Stream.Write(amount);
        }
    }

    public sealed class HitAnimation : Packet
    {
        public HitAnimation(Serial ser, byte color)
            : base(0x23)
        {
            m_Stream.Write((int)ser);
            m_Stream.Write((byte)color); //0xCB max, 0xCC blue
        }
    }

    public sealed class ChangeFace : Packet
    {
        public ChangeFace(Serial ser, short face)
            : base(0x22)
        {
            m_Stream.Write((int)ser);
            m_Stream.Write(face);
        }
    }

    public sealed class EffectSound : Packet
    {
        public EffectSound(short index)
            : base(0x37)
        {            
            m_Stream.Fill(5);
            m_Stream.Write((short)index);
            m_Stream.Fill(2);
            m_Stream.Write((byte)0x05);
            m_Stream.Write((byte)0x00);
        }
    }
    public sealed class SwingAnimation : Packet
    {
        public SwingAnimation(Serial ser, short face)
            : base(0x21)
        {
            m_Stream.Write((int)ser);
            m_Stream.Write((short)face);
        }
    }

    public sealed class MoveSpriteWalkChar : Packet
    {
        public MoveSpriteWalkChar(Serial ser, short face, short x, short y)
            : base(0x1f)
        {
            m_Stream.Write((int)ser);
            m_Stream.Write(face);
            m_Stream.Write(x);
            m_Stream.Write(y);
        }
    }

    public sealed class MoveSpriteWalk : Packet
    {
        public MoveSpriteWalk(Serial ser, short face, short x, short y)
            : base(0x1f)
        {
            m_Stream.Write((int)ser);
            m_Stream.Write((short)face);
            m_Stream.Write(x);
            m_Stream.Write(y);
        }
    }

    public sealed class MoveSpriteTele : Packet
    {
        public MoveSpriteTele(Serial ser, short face, short x, short y)
            : base(0x1c)
        {
            m_Stream.Write((int)ser);
            m_Stream.Write((short)face);
            m_Stream.Write(x);
            m_Stream.Write(y);
        }
    }

    public sealed class SetLevelGold : Packet
    {
        public SetLevelGold(Player player)
            : base(0x0F)
        {
            m_Stream.WriteEUCKRFixed(player.FullClass, 32);
            m_Stream.Write((byte)player.PromoLevel);
            m_Stream.Write(player.XP);
            m_Stream.Write(player.XPNext);
            m_Stream.Write(player.Gold > int.MaxValue ? int.MaxValue : Convert.ToInt32(player.Gold));
            //  public const byte SetLevelGold = 0x0F; //0x0F 1 32text 1lvl 4exp 4 next 4money
        }
    }

    public sealed class CreateGem : Packet
    {
        public CreateGem(script.npc.BaseNPC gem)
            : base(0x3D)
        {
            m_Stream.Write((int)gem.m_Serial);
            m_Stream.Write((short)1); //facedir
            m_Stream.Write((short)gem.m_Loc.X);
            m_Stream.Write((short)gem.m_Loc.Y);
            m_Stream.Write((byte)0x0B);

            m_Stream.Fill(1);
            m_Stream.Write((byte)1);
            m_Stream.Fill(2);
            m_Stream.Write((int)1);                     //애니메이션(광물때문에 추가)
            m_Stream.Write((byte)gem.Sprite);
            m_Stream.Fill(4);

            m_Stream.Write((byte)gem.aSpeed);           //aSpeed
            m_Stream.Write((byte)0);
            m_Stream.Write((byte)0);
            m_Stream.Write((byte)gem.aStartFrames);     //aStartFrames
            m_Stream.Write((byte)gem.aFrames);          //aFrames5

            m_Stream.Fill(16);

            m_Stream.Write((short)Encoding.Default.GetByteCount(gem.Name));         // 한글 문자 포함 가변 길이 체크;
            m_Stream.WriteEUCKR(gem.Name);
        }
        //광석
        public CreateGem(script.monster.BaseNode gem)
            : base(0x3D)
        {
            m_Stream.Write((int)gem.m_Serial);
            m_Stream.Write((short)1); //facedir
            m_Stream.Write((short)gem.m_Loc.X);
            m_Stream.Write((short)gem.m_Loc.Y);
            m_Stream.Write((byte)0x0B);

            m_Stream.Fill(1);
            m_Stream.Write((byte)1);
            m_Stream.Fill(2);
            m_Stream.Write((int)1);          //애니메이션(광물때문에 추가)
            m_Stream.Write((byte)gem.m_MonsterID);
            m_Stream.Fill(4);

            m_Stream.Write((byte)1);           //aSpeed
            m_Stream.Write((byte)gem.aFrames);  //aFrames
            m_Stream.Fill(1);
            m_Stream.Write((byte)gem.aStartFrames);         //aStartFrames

            m_Stream.Fill(17);

            m_Stream.Write((short)Encoding.Default.GetByteCount(gem.Name));         // 한글 문자 포함 가변 길이 체크;
            m_Stream.WriteEUCKR(gem.Name);
        }
    }
    public sealed class CreateNPC2 : Packet
    {
        public CreateNPC2(script.npc.BaseNPC npc)
            : base(0x3A)
        {
            m_Stream.Write((int)npc.m_Serial);
            m_Stream.Write((short)1); //facedir
            m_Stream.Write((short)npc.m_Loc.X);
            m_Stream.Write((short)npc.m_Loc.Y);
            m_Stream.Write((byte)npc.Type);
            m_Stream.Fill(8);
            m_Stream.Write((byte)npc.Sprite);
            m_Stream.Fill(4);
            m_Stream.Write((byte)npc.aSpeed);
            m_Stream.Write((byte)npc.aStartFrames);         //aStartFrames
            m_Stream.Write((byte)npc.aFrames);
            m_Stream.Fill(1);

            m_Stream.Fill(17);
            m_Stream.Write((short)Encoding.Default.GetByteCount(npc.Name));         // 한글 문자 포함 가변 길이 체크;
            m_Stream.WriteEUCKR(npc.Name);
        }
    }

    public sealed class CreateMonster : Packet
    {
        public CreateMonster(LKCamelot.script.monster.Monster item, Serial key)
            : base(0x1D)
        {
            m_Stream.Write((int)key);
            m_Stream.Write((short)item.Face); //facedir
            m_Stream.Write((short)item.m_Loc.X);
            m_Stream.Write((short)item.m_Loc.Y);

            m_Stream.Write((byte)1); //10 byte sprite 02 00 00 00 00 00 00 00 00 23 (first 2 last # id

            m_Stream.Fill(1);
            m_Stream.Fill(1);
            m_Stream.Fill(1);
            m_Stream.Fill(1);
            m_Stream.Fill(1);
            m_Stream.Fill(1);
            m_Stream.Fill(1);
            m_Stream.Fill(1);

            m_Stream.Write((short)item.m_MonsterID);

            m_Stream.Write((byte)item.Color);//1byte uk
            m_Stream.Write((byte)0);//1byte uk

            m_Stream.Write((byte)0);//1byte uk
            m_Stream.Write((byte)item.Transparancy);//transparency

            m_Stream.Write(item.BuffArray, 0, 4);
            m_Stream.Write((byte)0);
            if (item.m_MonsterID >= 200)
                m_Stream.Write((byte)2);
            else
                m_Stream.Write((byte)1); //sprite frame type);

            m_Stream.Write((byte)0);

            m_Stream.Write((short)Encoding.Default.GetByteCount(item.Name));         // 한글 문자 포함 가변 길이 체크;
            m_Stream.WriteEUCKR(item.Name);
        }
    }
    public sealed class CreateChar : Packet
    {
        public CreateChar(Player play, Serial key)
            : base(0x1D)
        {
            m_Stream.Write((int)key);
            m_Stream.Write((short)play.Face);
            m_Stream.Write((short)play.Loc.X);
            m_Stream.Write((short)play.Loc.Y);
            m_Stream.Write(play.Appearance, 0, 10);
            m_Stream.Write((byte)1);//1byte ukn
            m_Stream.Write((byte)play.Color); // color
            m_Stream.Write((byte)0); //thickness
            m_Stream.Write((byte)0);//uk
            m_Stream.Write((byte)play.Transparancy);//1byte transp

            m_Stream.Write(play.BuffArray, 0, 4); //buffs
            m_Stream.Write((byte)0x00); //uk 
            m_Stream.Write((byte)0x00); //uk
            m_Stream.Write((byte)0x00); //uk
            m_Stream.Write((short)Encoding.Default.GetByteCount(play.Name + ":" + (play.Guild == null ? "" : play.Guild.Name) ));         // 한글 문자 포함 가변 길이 체크;
            m_Stream.WriteEUCKR(play.Name + ":" + (play.Guild==null?"":play.Guild.Name) );
        }
    }

    public sealed class CreateItemGround2 : Packet
    {
        public CreateItemGround2(LKCamelot.script.item.Item item, Serial key)
            : base(0x1D)
        {
            m_Stream.Write((int)key);
            m_Stream.Write((short)1); //facedir
            m_Stream.Write((short)item.Loc.X);
            m_Stream.Write((short)item.Loc.Y);
            m_Stream.Write((byte)2); //10 byte sprite 02 00 00 00 00 00 00 00 00 23 (first 2 last # id
            m_Stream.Fill(8);
            m_Stream.Write((short)item.m_ItemID);
            m_Stream.Write((byte)2);//1byte ukn
            m_Stream.Write((byte)0);//1byte ukn
            m_Stream.Write((byte)0);//1byte ukn
            m_Stream.Write((byte)0);//1byte ukn

            m_Stream.Write((byte)0x0); //1byte ukn
            m_Stream.Write((byte)03); //1byte unclickable, transparency
            m_Stream.Write((byte)0x0); //1byte ukn
            m_Stream.Write((byte)0x2); //1byte ukn

            m_Stream.Write((byte)12);
            m_Stream.Write((byte)12);
            m_Stream.Write((byte)12);

            m_Stream.Write((short)Encoding.Default.GetByteCount(item.NPrefix() + " " + item.Name));         // 한글 문자 포함 가변 길이 체크;
            m_Stream.WriteEUCKR(item.NPrefix() + " " + item.Name);
        }
    }

    public sealed class UpdateCharStats : Packet
    {
        public UpdateCharStats(Player player)
            : base(0x14)
        {
            m_Stream.Write(Util.checkUShort((uint)player.HPCur));
            m_Stream.Write(Util.checkUShort((uint)player.HP));
            m_Stream.Write(Util.checkUShort((uint)player.MPCur));
            m_Stream.Write(Util.checkUShort((uint)player.MP));
            m_Stream.Write(Util.checkUShort(player.GetStat("str", true)));
            m_Stream.Write(Util.checkUShort(player.GetStat("men", true)));
            m_Stream.Write(Util.checkUShort(player.GetStat("dex", true)));
            m_Stream.Write(Util.checkUShort(player.GetStat("vit", true)));
            m_Stream.Write(Util.checkUShort(player.GetStat("str")));
            m_Stream.Write(Util.checkUShort(player.GetStat("men")));
            m_Stream.Write(Util.checkUShort(player.GetStat("dex")));
            m_Stream.Write(Util.checkUShort(player.GetStat("vit")));
            m_Stream.Write(Util.checkUShort((uint)player.AC));
            m_Stream.Write(Util.checkUShort((uint)player.Hit));
            m_Stream.Write(Util.checkUShort((uint)player.Dam));
            m_Stream.Write(Util.checkUShort(player.Extra));
        }
    }


    public sealed class LoadTile : Packet
    {
        public LoadTile(Map.MapType Type):base(0x35)
        {
            string str = string.Empty;
            if (Type == Map.MapType.LK)
                str = "lkTil.til,lkObj.obj";
            else if (Type == Map.MapType.VV)
                str = "VV.Til,VV.Obj";
            else if (Type == Map.MapType.PS)
                str = "pstil.til,psobj.obj";
            else if (Type == Map.MapType.Weak)
                str = "weaktil.til,weakobj.obj";
            else if (Type == Map.MapType.Bone)
                str = "bonetil.Til,boneobj.Obj";

            m_Stream.Write((short)Encoding.Default.GetByteCount(str));         // 한글 문자 포함 가변 길이 체크;
            m_Stream.WriteEUCKR(str);
        }
    }
    public sealed class UpdateChatBox : Packet
    {
        public UpdateChatBox(byte colortext, byte colortext2, short colorbar, string text)
            : base(0x0D)
        {
            m_Stream.Write(colortext); //01 ff orange ff ff white 
            m_Stream.Write(colortext2);
            m_Stream.Write(colorbar);
            m_Stream.Write((short)Encoding.Default.GetByteCount(text));         // 한글 문자 포함 가변 길이 체크;
            m_Stream.WriteEUCKR(text);
        }
    }
    public sealed class UpdateCharChatBox : Packet
    {
        public UpdateCharChatBox(Serial ser, string message)
            : base(0x20)
        {
            m_Stream.Write(ser);
            m_Stream.Write((byte)0x0f);
            m_Stream.Write((short)Encoding.Default.GetByteCount(message));         // 한글 문자 포함 가변 길이 체크;
            m_Stream.WriteEUCKR(message);
        }
    }

    public sealed class SpawnShopGump2 : Packet
    {
        public SpawnShopGump2(script.npc.GUMP shop)
            : base(0x3E)
        {
            m_Stream.Write(shop.ID);
            m_Stream.Write((byte)shop.SellList.Count());
            m_Stream.Write(shop.Titlec);
            m_Stream.Write(shop.Boxc);
            m_Stream.Fill(1);
            m_Stream.Write(shop.Time);
            m_Stream.Write(shop.ItemIDArray, 0, 32);
//            m_Stream.WriteAsciiFixed(shop.ShopName, 32);
            m_Stream.WriteEUCKRFixed(shop.ShopName, 32);

            m_Stream.Write((short)Encoding.Default.GetByteCount(shop.ItemNameString));         // 한글 문자 포함 가변 길이 체크;
            m_Stream.WriteEUCKR(shop.ItemNameString);
        }
    }

    //public const byte ChangeObjectSprite = 0x26; //0x26 1 4id 10 sprite 1uk 1color
    public sealed class ChangeObjectSprite : Packet
    {
        public ChangeObjectSprite(int objectid, byte color, byte[] sprite)
            : base(0x26)
        {
            m_Stream.Write((int)objectid);
            m_Stream.Write(sprite, 0, 10);
            m_Stream.Write((byte)0);
            m_Stream.Write((byte)color);
        }
    }

    public sealed class ChangeObjectSpritePlayer : Packet
    {
        public ChangeObjectSpritePlayer(Player play)
            : base(0x26)
        {
            m_Stream.Write((int)play.m_Serial);
            m_Stream.Write(play.Appearance, 0, 10);
            m_Stream.Write((byte)0);
            m_Stream.Write((byte)play.Color);
            m_Stream.Write((byte)0);
            m_Stream.Write((short)Encoding.Default.GetByteCount(play.Name + ":" + (play.Guild == null ? "" : play.Guild.Name) ));         // 한글 문자 포함 가변 길이 체크;
            m_Stream.WriteEUCKR(play.Name + ":" + (play.Guild == null ? "" : play.Guild.Name));
        }
    }
    public sealed class CreateMagicEffect : Packet
    {
        public CreateMagicEffect(int objectid, short facedir, short x, short y, byte[] sprite, byte width)
            : base(0x1D)
        {
            m_Stream.Write((int)objectid);
            m_Stream.Write(facedir);
            m_Stream.Write(x);
            m_Stream.Write(y);
            m_Stream.Write(sprite, 0, sprite.Count());
            m_Stream.Write((byte)0x0);
            m_Stream.Write((byte)0x0);
            m_Stream.Write((byte)width);
            m_Stream.Write((byte)0x0); ;//1byte ukn
            m_Stream.Write((byte)0x0); ;//1byte transp
        }
    }

    public sealed class CloseLogin : Packet
    {
        public CloseLogin()
            : base(0x04)
        {
            m_Stream.Write((byte)0x02);
            m_Stream.Write((byte)0x00);
        }
    }

    public sealed class WrongPass : Packet
    {
        public WrongPass()
            : base(0x05)
        {
            m_Stream.Write((byte)0xff);
            m_Stream.Write((byte)0xff);
        }
    }

    public sealed class WrongID : Packet
    {
        public WrongID()
            : base(0x0A)
        {
            m_Stream.Write((byte)0xff);
            m_Stream.Write((byte)0xff);
        }
    }

    public sealed class AlreadyOnline : Packet
    {
        public AlreadyOnline()
            : base(0x07)
        {
            m_Stream.Write((byte)0xff);
            m_Stream.Write((byte)0xff);
        }
    }

    public sealed class SetHP : Packet
    {
        public SetHP(LKCamelot.model.Player play)
            : base(0x10)
        {
            m_Stream.Write(Util.checkUShort((uint)play.HPCur));
        }
    }

    public sealed class SetMP : Packet
    {
        public SetMP(LKCamelot.model.Player play)
            : base(0x11)
        {
            m_Stream.Write(Util.checkUShort((uint)play.MPCur));
        }
    }

    public sealed class SetObjectEffects : Packet
    {
        /// <param name="self">대상 본인에게 보내는 패킷이면 true. 완전 투명이어도 본인에게는 원래 빛 반경을 보낸다.</param>
        public SetObjectEffects(BaseObject obj, bool self = false)
            : base(0x34)
        {
            //  SetObjectEffects = 0x34; // 0x34 4id 1light 1trans 4byffs 1staticmagic
            m_Stream.Write(obj.m_Serial);
            // 완전 투명(100)인 유저는 남에게 빛 반경을 0으로 보낸다. 어두운 맵에서 빛 원으로 위치가 드러나는 것을 막는다.
            byte light = obj.LightRad;
            if (!self && obj is model.Player && obj.Transparancy >= 100)
                light = 0;
            m_Stream.Write((byte)light); //lightrad
            m_Stream.Write((byte)obj.Transparancy); //transp
            m_Stream.Write(obj.BuffArray, 0, 4);

            if (obj.m_Buffs.Where(xe => xe.Value.Name == "MENTAL SWORD").FirstOrDefault().Value != null)
                m_Stream.Write((byte)0x63); //move with char  //cast once
            else
                m_Stream.Write((byte)0); //move with char  //cast once
            
            if (obj is script.monster.Monster)
            {
                if ((obj as script.monster.Monster).m_MonsterID >= 200)
                    m_Stream.Write((byte)2);
                else
                    m_Stream.Write((byte)1); //sprite frame type);
            }
            else
            {
                if ((obj as model.Player).Equipped2.Where(xe => xe.Value.GetType() == typeof(script.item.MinerSuit) || xe.Value.GetType() == typeof(script.item.BlackSmithSuit)).Any())
                {
                    m_Stream.Write((byte)2); //sprite frame type
                    m_Stream.Write((byte)0); //sprite frame type
                }
                else
                {
                    m_Stream.Write((byte)0); //sprite frame type
                    m_Stream.Write((byte)obj.WalkingMode); // speed
                }
            }

            m_Stream.Write((byte)0);
        }
    }

    public sealed class MagicEffect : Packet
    {
        public MagicEffect(Serial serial, int number)
            : base(0x25)
        {
            m_Stream.Write((int)serial);
            m_Stream.Write((byte)number);
        }
    }
    public sealed class CurveMagic : Packet
    {
        public CurveMagic(int objectID, short X, short Y, LKCamelot.script.spells.SpellSequence spellani)
            : base(0x28)
        {
            m_Stream.Write(objectID);
            m_Stream.Write(X);
            m_Stream.Write(Y);
            m_Stream.Write((byte)0);
            m_Stream.Write((byte)0);
            m_Stream.Write((byte)spellani.OnCastSprite);
            m_Stream.Write((byte)spellani.MovingSprite);
            m_Stream.Write((byte)spellani.OnImpactSprite);
            //   for (int i = 0; i < 7; i++) msg.Add(0x12);
            m_Stream.Fill(7);
            m_Stream.Write((byte)spellani.Thickness);
            m_Stream.Write((byte)spellani.Type);                // 0:발사 1:프레임, 2:플라잉소드, 4:
            m_Stream.Write((byte)spellani.Speed);
            m_Stream.Write((byte)spellani.Streak);
        }
    }

    public sealed class LoadWorld : Packet
    {
        public LoadWorld(Player player, byte time)
            : base(0x1B)
        {
            m_Stream.Write((byte)time);
            m_Stream.Write((int)player.m_Serial);
            m_Stream.Fill(4);
            m_Stream.Write((short)player.Face);
            m_Stream.Write((short)player.Loc.X);
            m_Stream.Write((short)player.Loc.Y);
            m_Stream.Write(player.Appearance, 0, 10);
            m_Stream.Write((byte)0);
            m_Stream.Write((byte)player.Color); //color
            m_Stream.Write((byte)0);
            m_Stream.Write((byte)player.LightRad);        //m_Stream.Write((byte)player.LightRad);
            m_Stream.Write((byte)player.Transparancy);
            m_Stream.Write(player.BuffArray, 0, 4);

            if (player.m_Buffs.Where(xe => xe.Value.Name == "MENTAL SWORD").FirstOrDefault().Value != null)
                m_Stream.Write((byte)0x63); //move with char
            else
                m_Stream.Write((byte)0); //move with char

            if (player.Equipped2.Where(xe => xe.Value.GetType() == typeof(script.item.MinerSuit) || xe.Value.GetType() == typeof(script.item.BlackSmithSuit)).Any())
            {
                m_Stream.Write((byte)2); //sprite frame type          
                m_Stream.Write((byte)0);
            }
            else
            {
                m_Stream.Write((byte)0); //sprite frame type            
                m_Stream.Write((byte)player.WalkingMode);
            }
            m_Stream.Write((byte)0);

            m_Stream.Write((short)Encoding.Default.GetByteCount(player.LoadMap));
            m_Stream.WriteEUCKR(player.LoadMap);
        }

    }

    public class Packet
    {
        protected LKCamelot.net.PacketWriter m_Stream;
        private int m_PacketID;
        private int m_Length;

        public List<byte> msg = new List<byte>();
        public Packet()
        {
        }

        public Packet(int packetID)
        {
            m_PacketID = packetID;
            m_Length = 32;

            m_Stream = LKCamelot.net.PacketWriter.CreateInstance();
            m_Stream.Write((byte)packetID);
        }

        public Packet(int packetID, int length)
        {
            m_PacketID = packetID;
            m_Length = length;

            m_Stream = LKCamelot.net.PacketWriter.CreateInstance(length);
            m_Stream.Write((byte)packetID);
        }

        private byte[] m_CompiledBuffer;
        private int m_CompiledLength;

        public byte[] Compile()
        {
            System.IO.MemoryStream ms = m_Stream.UnderlyingStream;

            m_CompiledBuffer = ms.GetBuffer();
            m_CompiledLength = (int)ms.Length;

            int length = m_CompiledLength;
            byte[] old = m_CompiledBuffer;
            m_CompiledBuffer = new byte[length];
            Buffer.BlockCopy(old, 0, m_CompiledBuffer, 0, length);

            LKCamelot.net.PacketWriter.ReleaseInstance(m_Stream);
            m_Stream = null;

            return Encrypt(m_CompiledBuffer);
        }

        public byte[] Format()
        {
            return msg.ToArray<byte>();
        }

        public Byte[] Encrypt(Byte[] data)
        {
            Byte[] ret = new Byte[1024];
            int mLoopItr = 0;
            int loop3 = 0;
            byte var_f, var_e, var_d, var_c = 0, var_b = 0;

            mLoopItr = data.Count() / 3;
            if (data.Count() % 3 != 0)
                mLoopItr++;

            for (int x = 0; x < mLoopItr; x++)
            {
                var_d = data[loop3];
                if (loop3 + 1 < data.Count())
                    try { var_c = data[loop3 + 1]; }
                    catch { var_c = 0; }
                if (loop3 + 2 < data.Count())
                    try { var_b = data[loop3 + 2]; }
                    catch { var_b = 0; }

                ret[x * 4] = Convert.ToByte(var_d >> 2);
                var_e = Convert.ToByte((var_d & 3) << 4);
                var_f = Convert.ToByte(var_c >> 4);
                ret[x * 4 + 1] = Convert.ToByte(var_e | var_f);
                var_e = Convert.ToByte((var_c & 0x0F) << 2);
                var_f = Convert.ToByte(var_b >> 6);
                ret[x * 4 + 2] = Convert.ToByte(var_e | var_f);
                ret[x * 4 + 3] = Convert.ToByte(var_b & 0x3F);

                ret[x * 4] += 0x3B;
                ret[x * 4 + 1] += 0x3B;
                ret[x * 4 + 2] += 0x3B;
                ret[x * 4 + 3] += 0x3B;
                loop3 += 3;
            }
            int size = Array.IndexOf<Byte>(ret, 0);
            ret[size] = 0x2E;
            ret[size + 1] = 0x0A;
            //    ret[size + 2] = 0x00;

            Array.Resize(ref ret, size + 2);
            return ret;
        }
    }
}

/*
17 bonetil.Til,boneobj.Obj  62 6f 6e 65 74 69 6c 2e 54 69 6c 2c 62 6f 6e 65 6f 62 6a 2e 4f 62 6a
13 LkTil.Til,LkObj.Obj  4c 6b 54 69 6c 2e 54 69 6c 2c 4c 6b 4f 62 6a 2e 4f 62 6a
13 pstil.til,psobj.obj  70 73 74 69 6c 2e 74 69 6c 2c 70 73 6f 62 6a 2e 6f 62 6a
0D VV.Til,vv.obj        56 56 2e 54 69 6c 2c 76 76 2e 6f 62 6a   //towerish
17 weaktil.til,weakobj.obj  77 65 61 6b 74 69 6c 2e 74 69 6c 2c 77 65 61 6b 6f 62 6a 2e 6f 62 6a   //greenish
*/