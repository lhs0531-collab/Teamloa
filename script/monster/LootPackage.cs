using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LKCamelot.script.monster;
namespace LKCamelot.script
{
    
    public static class Packs
    {
        public static Type[] AccessorySet6 = new Type[]{
            typeof(script.item.HealthRing),
            typeof(script.item.HealthAmulet),
            typeof(script.item.MagicRing),
            typeof(script.item.MagicAmulet),
            typeof(script.item.SkillRing),
            typeof(script.item.SkillAmulet),
            typeof(script.item.LifeRing),
            typeof(script.item.LifeAmulet)
        };

        public static Type[] AccessorySet21 = new Type[]{
            typeof(script.item.ForceRing),
            typeof(script.item.ForceAmulet),
            typeof(script.item.SoulRing),
            typeof(script.item.SoulAmulet),
            typeof(script.item.AccuracyRing),
            typeof(script.item.AccuracyAmulet),
            typeof(script.item.StaminaRing),
            typeof(script.item.StaminaAmulet)
        };

        public static Type[] AccessorySet26 = new Type[]{
            typeof(script.item.VictoryRing),
            typeof(script.item.VictoryAmulet),
            typeof(script.item.BlackartRing),
            typeof(script.item.BlackartAmulet),
            typeof(script.item.ConcentrationRing),
            typeof(script.item.ConcentrationAmulet),
            typeof(script.item.VigorRing),
            typeof(script.item.VigorAmulet)
        };

        public static Type[] AccessorySet31 = new Type[]{
            typeof(script.item.MightRing),
            typeof(script.item.MightAmulet),
            typeof(script.item.SpriteRing),
            typeof(script.item.SpriteAmulet),
            typeof(script.item.PerfectionRing),
            typeof(script.item.PerfectionAmulet),
            typeof(script.item.AbundanceRing),
            typeof(script.item.AbundanceAmulet)
        };

        // 15.01.29
        // GM티리엘
        public static Type[] AccessorySet36 = new Type[]{
            typeof(script.item.BarbarrianRing),
            typeof(script.item.BarbarrianAmulet),
            typeof(script.item.MagicianRing),
            typeof(script.item.MagicianAmulet),
            typeof(script.item.CraftsmanRing),
            typeof(script.item.CraftsmanAmulet),
            typeof(script.item.YoungsterRing),
            typeof(script.item.YoungsterAmulet)
        };

        // 15.01.30
        // GM티리엘
        public static Type[] AccessorySet41 = new Type[]{
            typeof(script.item.FighterRing),
            typeof(script.item.FighterAmulet),
            typeof(script.item.SorcererRing),
            typeof(script.item.SorcererAmulet),
            typeof(script.item.VeteranRing),
            typeof(script.item.VeteranAmulet),
            typeof(script.item.SeniroRing),
            typeof(script.item.SeniroAmulet)
        };

        // 15.01.30
        // GM티리엘
        public static Type[] AccessorySet46 = new Type[]{
            typeof(script.item.WarriorRing),
            typeof(script.item.WarriorAmulet),
            typeof(script.item.ExorcistRing),
            typeof(script.item.ExorcistAmulet),
            typeof(script.item.ExpertRing),
            typeof(script.item.ExpertAmulet),
            typeof(script.item.ToughguyRing),
            typeof(script.item.ToughguyAmulet)
        };

        // 15.02.07
        // GM티리엘
        public static Type[] NewVVArmorPack = new Type[]{
            typeof(script.item.SkellGuard),
            typeof(script.item.GagoilPlate),
            typeof(script.item.CrocodileArmor),
            typeof(script.item.DragonSkin),
            typeof(script.item.Gladiator),
            typeof(script.item.PigBasket),
            typeof(script.item.KiteShield),
        };



        public static Type[] CSWSFS = new Type[]{
            typeof(script.item.CurveShockBook),
            typeof(script.item.WindySpiritBook),
            typeof(script.item.FrameStrikeBook)
        };

        public static Type[] VVArmorPack = new Type[]{
            //typeof(script.item.CelestialArmor),
            //typeof(script.item.GoldVest),
            typeof(script.item.DiamondArmor),
            typeof(script.item.GoliathPlate),
            typeof(script.item.HolyDefender),
            typeof(script.item.GrandeurPride)
        };

        public static Type[] GreatArmorPack = new Type[]{
            typeof(script.item.GoliathPlate),
            typeof(script.item.DiamondArmor),
            typeof(script.item.GrandeurPride),
            typeof(script.item.HolyDefender)
            //typeof(script.item.UnicornProtectoria),// 15.01.29 드랍 언락(GM티리엘)
            //typeof(script.item.GoldVest),            // 15.01.29 드랍 추가(GM티리엘)
        };

        public static Type[] GreatWeaponPack = new Type[]{
            typeof(script.item.Excalibur),
            typeof(script.item.DaeungDaegum),
            typeof(script.item.Kassandra),
            typeof(script.item.TaegkFan),
            typeof(script.item.GoldVest)
        };

        public static Type[] WeaponPack110 = new Type[]{
            typeof(script.item.GreatAxe),
            typeof(script.item.GreatMaul),
            typeof(script.item.GreatSword),
            typeof(script.item.GreatLance),
            typeof(script.item.Halberd),
            //typeof(script.item.Troy), // 15.01.26 드랍 언락(GM티리엘)
            typeof(script.item.GiantHammer)
        };

        public static Type[] ArmorPack110 = new Type[]{
            typeof(script.item.FullPlate),
            typeof(script.item.WidePlate),
            typeof(script.item.SamuraiPlate),
            typeof(script.item.GrandShield),
            typeof(script.item.MailPlate),
            typeof(script.item.ShineHelmet),
            typeof(script.item.KiteShield),
            typeof(script.item.PigBasket),
            typeof(script.item.GrandeurHelmet),
            typeof(script.item.FieldCap),
        };
        public static Type[] StatPotions = new Type[]{
            typeof(script.item.StrStatPotion),
            typeof(script.item.MenStatPotion),
            typeof(script.item.DexStatPotion),
            typeof(script.item.VitStatPotion),
        };

        public static Type[] AccessorySet51 = new Type[]{
            typeof(script.item.BraveAmulet1),
			typeof(script.item.BraveRing1),
			typeof(script.item.WitchAmulet),
			typeof(script.item.WitchRing),
			typeof(script.item.RecluseAmulet),
			typeof(script.item.RecluseRing),
			typeof(script.item.SpecialistAmulet),
			typeof(script.item.SpecialistRing),
        };

        public static Type[] AccessorySet56 = new Type[]{
            typeof(script.item.ChampionAmulet),
			typeof(script.item.ChampionRing),
			typeof(script.item.WizardAmulet),
			typeof(script.item.WizardRing),
			typeof(script.item.LeaderAmulet),
			typeof(script.item.LeaderRing),
			typeof(script.item.MaestroAmulet),
			typeof(script.item.MaestroRing),
        };

        public static Type[] AccessorySet61 = new Type[]{
            typeof(script.item.SwordmanAmulet),
			typeof(script.item.SwordmanRing),
			typeof(script.item.NecromancerAmulet),
			typeof(script.item.NecromancerRing),
			typeof(script.item.GuruAmulet),
			typeof(script.item.GuruRing),
			typeof(script.item.professionalAmulet),
			typeof(script.item.professionalRing),
        };

        public static Type[] AccessorySet66 = new Type[]{
            typeof(script.item.KnightAmulet),
			typeof(script.item.KnightRing),
			typeof(script.item.ShamanAmulet),
			typeof(script.item.ShamanRing),
			typeof(script.item.TaoistAmulet),
			typeof(script.item.TaoistRing),
			typeof(script.item.MasterAmulet),
			typeof(script.item.MasterRing),
        };

        public static Type[] AccessorySet71 = new Type[]{
            typeof(script.item.ValorAmulet),
			typeof(script.item.ValorRing),
			typeof(script.item.ProphetAmulet),
			typeof(script.item.ProphetRing),
			typeof(script.item.HighlanderAmulet),
			typeof(script.item.HighlanderRing),
			typeof(script.item.AceAmulet),
			typeof(script.item.AceRing),
        };

        public static Type[] ItemPigmyPark = new Type[]{
			typeof(script.item.Mantle),
			typeof(script.item.Rapier),
			typeof(script.item.Mask),
			typeof(script.item.Saber),
			typeof(script.item.Epee),
			typeof(script.item.SpikedClub),
			typeof(script.item.SquareShield),
			typeof(script.item.TraceBook),
			typeof(script.item.OneEdgedAxe),
			typeof(script.item.SmallAxe),
			typeof(script.item.ComeBackBook),
			typeof(script.item.Cape),
			typeof(script.item.ThunderCrossBook),
			typeof(script.item.FlameRoundBook),
        };

        public static Type[] ItemSkelPark = new Type[]{
			typeof(script.item.Mantle),
            typeof(script.item.Rapier),
            typeof(script.item.Mask),
            typeof(script.item.Saber),
            typeof(script.item.Epee),
            typeof(script.item.SpikedClub),
            typeof(script.item.SquareShield),
            typeof(script.item.TraceBook),
            typeof(script.item.OneEdgedAxe),
            typeof(script.item.SmallAxe),
            typeof(script.item.ComeBackBook),
            typeof(script.item.Cape),
            typeof(script.item.ThunderCrossBook),
            typeof(script.item.FlameRoundBook),
        };

        public static Type[] ItemStonePark = new Type[]{
			typeof(script.item.ChainMail),
			typeof(script.item.LargeHack),
			typeof(script.item.Maul),
			typeof(script.item.Tuck),
			typeof(script.item.BoneMail),
			typeof(script.item.RingMail),
			typeof(script.item.BroadAxe),
			typeof(script.item.ArmShield),
			typeof(script.item.Gladius),
			typeof(script.item.StoneCurseBook),
			typeof(script.item.FreezingBook),
			//샤프아이
			//라디에이션
       };

        public static Type[] ItemWarDummyPark = new Type[]{
			typeof(script.item.Javelin),
			typeof(script.item.WarStaff),
			typeof(script.item.BastardSword),
			typeof(script.item.GreatSword),
			typeof(script.item.SevenStarSword),
			typeof(script.item.DemonDeathBook),
			typeof(script.item.BigBangBook),
			typeof(script.item.RevelationBook),
			typeof(script.item.RainbowArmorBook),
			typeof(script.item.MailPlate),
			typeof(script.item.SamuraiPlate),
			typeof(script.item.WidePlate),
			typeof(script.item.GreatMaul),
			typeof(script.item.FullPlate),
			//은신
       };

        public static Type[] ItemZombiePark = new Type[]{
			typeof(script.item.FlameRoundBook),
			typeof(script.item.ComeBackBook),
			typeof(script.item.MorningStar),
			typeof(script.item.Cloak),
			typeof(script.item.Hanger),
			typeof(script.item.Mantle),
			typeof(script.item.Saber),
			typeof(script.item.ThunderCrossBook),
			typeof(script.item.Simitar),
       };

        public static Type[] ItemButcherPark = new Type[]{
			typeof(script.item.EnamelLeather),
			typeof(script.item.BroadAxe),
			typeof(script.item.FieldCap),
			typeof(script.item.CoatingShield),
			typeof(script.item.ThunderCrossBook),
			typeof(script.item.GuardianSwordBook),
			typeof(script.item.TeleportBook),
			typeof(script.item.FireBallBook),
			typeof(script.item.HardLeather),
			typeof(script.item.LeatherArmor),
			typeof(script.item.TowerShield),
			typeof(script.item.BroadSword),
       };

        public static Type[] ItemDummyPark = new Type[]{
			typeof(script.item.WarStaff),
			typeof(script.item.Javelin),
			typeof(script.item.BreastPlate),
			typeof(script.item.BattleAxe),
			typeof(script.item.GrandShield),
			typeof(script.item.SamuraiSword),
			typeof(script.item.StoneCurseBook),
			typeof(script.item.FreezingBook),
			typeof(script.item.ScaleMail),
			typeof(script.item.WarHammer),
			typeof(script.item.GrandeurHelmet),
			typeof(script.item.FamilyShield),
			//샤프아이
			//라디에이션
			//은신
       };

        public static Type[] ItemGolemPark = new Type[]{
			typeof(script.item.FullPlate),
			typeof(script.item.GrandeurHelmet),
			typeof(script.item.FlyingSwordBook),
			typeof(script.item.MagmaHandBook),
			typeof(script.item.SkillRing),
			typeof(script.item.SkillAmulet),
			typeof(script.item.HealthRing),
			typeof(script.item.HealthAmulet),
       };

        public static Type[] ItemHardBoildPark = new Type[]{
			typeof(script.item.MailPlate),
			typeof(script.item.BastardSword),
			typeof(script.item.GreatSword),
			typeof(script.item.SevenStarSword),
			typeof(script.item.DemonDeathBook),
			typeof(script.item.BigBangBook),
			typeof(script.item.RevelationBook),
			typeof(script.item.SamuraiPlate),
			typeof(script.item.WidePlate),
			typeof(script.item.GreatAxe),
			typeof(script.item.GreatMaul),
			typeof(script.item.FullPlate),
			typeof(script.item.RainbowArmorBook),
			//은신
       };

        public static Type[] ItemMummyPark = new Type[]{
			typeof(script.item.TeleportBook),
			typeof(script.item.BattleArmor),
			typeof(script.item.LargeAxe),
			typeof(script.item.FullHelmet),
			typeof(script.item.Claymore),
			typeof(script.item.PlusHealBook),
			typeof(script.item.TeagueShieldBook),
			typeof(script.item.FlameRoundBook),
       };
    }
}
