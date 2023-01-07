using BepInEx.Configuration;
using UnityEngine;

namespace SamusMod.Modules
{
    class Config
    {
        //Config entry variables go here. Use these to get the config settings in Mod Manager.
        public static ConfigEntry<bool> characterEnabled;
        public static ConfigEntry<bool> enableHud;
        private static ConfigEntry<bool> customStats;
        private static ConfigEntry<float> baseMaxHealth;
        private static ConfigEntry<float> baseRegen;
        private static ConfigEntry<float> baseMaxShield;
        private static ConfigEntry<float> baseMoveSpeed;
        private static ConfigEntry<float> baseAcceleration;
        private static ConfigEntry<float> baseDamage;
        private static ConfigEntry<float> baseAttackSpeed;
        private static ConfigEntry<float> baseCrit;
        private static ConfigEntry<float> baseArmor;
        private static ConfigEntry<float> baseJumpPower;
        private static ConfigEntry<float> levelMaxHealth;
        private static ConfigEntry<float> levelRegen;
        private static ConfigEntry<float> levelMaxShield;
        private static ConfigEntry<float> levelJumpPower;
        private static ConfigEntry<float> levelDamage;
        private static ConfigEntry<float> levelAttackSpeed;
        private static ConfigEntry<float> levelCrit;
        private static ConfigEntry<float> levelArmor;
        private static ConfigEntry<bool> customSkillMulti;
        private static ConfigEntry<float> beamMult;
        private static ConfigEntry<float> missileMult;
        private static ConfigEntry<float> sMissileMult;
        private static ConfigEntry<float> rollMult;
        private static ConfigEntry<float> bombMult;
        private static ConfigEntry<float> powerBombMult;

        public static bool customStatsBool => customStats.Value;
        public static bool customSkillMultiBool => customSkillMulti.Value;

        public static float bMaxHealth => baseMaxHealth.Value;
        public static float bMaxShield=>baseMaxShield.Value;
        public static float bRegen=>baseRegen.Value;
        public static float bMoveSpeed => baseMoveSpeed.Value;
        public static float bAttackSpeed => baseAttackSpeed.Value;
        public static float bCrit=>baseCrit.Value;
        public static float bArmor=>baseArmor.Value;
        public static float bAcceleration=>baseAcceleration.Value;
        public static float bDamage=>baseDamage.Value;
        public static float bJumpPower=>baseJumpPower.Value;
        public static float lMaxHealth=>levelMaxHealth.Value;
        public static float lMaxShield=>levelMaxShield.Value;
        public static float lRegen=>levelRegen.Value;
        public static float lDamage=>levelDamage.Value;
        public static float lAttackSpeed=>levelAttackSpeed.Value;
        public static float lCrit=>levelCrit.Value;
        public static float lArmor=>levelArmor.Value;
        public static float lJumpPower=>levelJumpPower.Value;
        public static float pBeamMult;
        public static float pMissileMult;
        public static float pRollMult;
        public static float pBombMult;
        public static float pPowerBombMult;
        public static float pSMissileMult;

        //public static ConfigEntry<bool> AutoFireBind;
        //public static ConfigEntry<bool> forceUnlock;

        public static void ReadConfig()
        {
            //Template
            //ThunderHenryPlugin.instance.Config.Bind<bool>(new ConfigDefinition("section", "name"), false, new ConfigDescription("description"));

            //General
            characterEnabled = SamusPlugin.instance.Config.Bind<bool>(new ConfigDefinition("General", "Character Enabled"), true, new ConfigDescription("Set to false to disable this Survivor."));
            //AutoFireBind = SamusPlugin.instance.Config.Bind<bool>(new ConfigDefinition("General", "AutoFire Toggle Enabled"), false, new ConfigDescription("If this is enabled, it will let you bind a key in the menu to toggle the charge beam to an autofire mode when held down instead of charging."));
            //forceUnlock = SamusPlugin.instance.Config.Bind<bool>(new ConfigDefinition("General", "Force Unlock"), false, new ConfigDescription("Set to true to force this Survivor's content to be unlocked."));

            //VR
            enableHud = SamusPlugin.instance.Config.Bind<bool>("VR Settings", "Enable VR Visor", true, "Enables the Metroid Prime Style Visor When playing in VR.");
            customStats = SamusPlugin.instance.Config.Bind<bool>("General", "Use Custom Character Stats", false, "Whether to use the custom stats set in the config instead of the original ones.");
            baseMaxHealth = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Base Max Health", 200, new ConfigDescription("Base max health.", new AcceptableValueRange<float>(0, float.MaxValue)));
            baseRegen = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Base Regen", 1.5f, new ConfigDescription("Base regen.", new AcceptableValueRange<float>(0, float.MaxValue)));
            baseMaxShield = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Base Max Shield", 0, new ConfigDescription("Base max shield.", new AcceptableValueRange<float>(0, float.MaxValue)));
            baseMoveSpeed = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Base Movement Speed", 7, new ConfigDescription("Base movement speed.", new AcceptableValueRange<float>(0, float.MaxValue)));
            baseAcceleration = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Base Acceleration", 160, new ConfigDescription("Base acceleration.", new AcceptableValueRange<float>(0, float.MaxValue)));
            baseDamage = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Base Damage", 15,new ConfigDescription("Base damage value.",new AcceptableValueRange<float>(0,float.MaxValue)));
            baseAttackSpeed = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Base Attack Speed", 1,new ConfigDescription("Base attack speed.",new AcceptableValueRange<float>(0,float.MaxValue)));
            baseCrit = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Base Crit Percentage", 1, new ConfigDescription("Base crit percentage number between 0-100.",new AcceptableValueRange<float>(0, 100)));
            baseArmor = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Base Armor", 0, new ConfigDescription("Base armor.", new AcceptableValueRange<float>(0, float.MaxValue)));
            baseJumpPower = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Base Jump Power", 15, new ConfigDescription("Base jump power.", new AcceptableValueRange<float>(0, float.MaxValue)));
            levelMaxHealth = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Leveled Max Health", 50, new ConfigDescription("How much max health to gain on level up.", new AcceptableValueRange<float>(0, float.MaxValue)));
            levelRegen = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Leveled Regen", 0.3f, new ConfigDescription("How much regen to gain on level up.", new AcceptableValueRange<float>(0, float.MaxValue)));
            levelMaxShield = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Leveled Max Shield", 0, new ConfigDescription("How much max shield to gain on level up.", new AcceptableValueRange<float>(0, float.MaxValue)));
            levelJumpPower = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Leveled Jump Power", 0, new ConfigDescription("How much jump power to gain on level up.", new AcceptableValueRange<float>(0, float.MaxValue)));
            levelDamage = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Leveled Damage", 2, new ConfigDescription("How much damage to gain on level up", new AcceptableValueRange<float>(0, float.MaxValue)));
            levelAttackSpeed = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Leveled Attack Speed", 0, new ConfigDescription("How much attack speed to gain on level up.", new AcceptableValueRange<float>(0, float.MaxValue)));
            levelCrit = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Leveled Crit", 0, new ConfigDescription("How much crit to gain on level up.", new AcceptableValueRange<float>(0, float.MaxValue)));
            levelArmor = SamusPlugin.instance.Config.Bind<float>("Custom Character Stats", "Leveled Armor", 0, new ConfigDescription("How much armor to gain on level up.", new AcceptableValueRange<float>(0, float.MaxValue)));

            customSkillMulti = SamusPlugin.instance.Config.Bind<bool>("General", "Use Custom Skill Damage Multipliers", false, new ConfigDescription("Whether to use the custom damage multipliers set in the config instead of the original ones."));
            beamMult = SamusPlugin.instance.Config.Bind<float>("Custom Skill Damage Multipliers", "Beam Damage Multiplier", 1, new ConfigDescription("Multiply the normal and charge beam damage by this number.", new AcceptableValueRange<float>(0, float.MaxValue)));
            missileMult = SamusPlugin.instance.Config.Bind<float>("Custom Skill Damage Multipliers", "Missile Damage Multiplier", 1, new ConfigDescription("Multiply the normal and tracking missile damage by this number.", new AcceptableValueRange<float>(0, float.MaxValue)));
            sMissileMult = SamusPlugin.instance.Config.Bind<float>("Custom Skill Damage Multipliers", "Super Missile Damage Multiplier", 1, new ConfigDescription("Multiply the super missile damage by this number.", new AcceptableValueRange<float>(0, float.MaxValue)));
            rollMult = SamusPlugin.instance.Config.Bind<float>("Custom Skill Damage Multipliers", "Rolling Bomb Damage Multiplier", 1, new ConfigDescription("Multiply the roll bomb damage by this number.", new AcceptableValueRange<float>(0, float.MaxValue)));
            bombMult = SamusPlugin.instance.Config.Bind<float>("Custom Skill Damage Multipliers", "Morph Bomb Damage Multiplier", 1, new ConfigDescription("Multiply the morph bomb damage by this number.", new AcceptableValueRange<float>(0, float.MaxValue)));
            powerBombMult = SamusPlugin.instance.Config.Bind<float>("Custom Skill Damage Multipliers", "Power Bomb Damage Multiplier", 1, new ConfigDescription("Multiply the power bomb damage by this number.", new AcceptableValueRange<float>(0, float.MaxValue)));

            if (customSkillMulti.Value)
            {
                pBeamMult = beamMult.Value;
                pBombMult= bombMult.Value;
                pMissileMult= missileMult.Value;
                pSMissileMult=sMissileMult.Value;
                pRollMult= rollMult.Value;
                pPowerBombMult= powerBombMult.Value;
            }
            else
            {
                pBeamMult = 1;
                pBombMult = 1;
                pMissileMult = 1;
                pSMissileMult = 1;
                pRollMult = 1;
                pPowerBombMult = 1;
            }
        }
    }
}
