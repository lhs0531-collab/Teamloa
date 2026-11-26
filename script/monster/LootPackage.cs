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
            typeof(script.item.TaegkFan)
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
    }
}
