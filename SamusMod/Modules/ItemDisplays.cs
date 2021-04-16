using R2API;
using RoR2;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SamusMod.Modules
{
    public static class ItemDisplays
    {
        internal static ItemDisplayRuleSet ItemDisplayRuleSet;
        internal static List<ItemDisplayRuleSet.KeyAssetRuleGroup> itemDisplayRules;

        

        private static Dictionary<string, GameObject> itemDisplayPrefabs = new Dictionary<string, GameObject>();
        #region old
        //public static void RegisterDisplays()
        //{
        //    PopulateDisplays();

        //    GameObject bodyPrefab = Prefabs.samusPrefab;

        //    GameObject model = bodyPrefab.GetComponent<ModelLocator>().modelTransform.gameObject;
        //    CharacterModel characterModel = model.GetComponent<CharacterModel>();

        //    ItemDisplayRuleSet itemDisplayRuleSet = ScriptableObject.CreateInstance<ItemDisplayRuleSet>();

        //    itemRules = new List<ItemDisplayRuleSet.NamedRuleGroup>();
        //    equipmentRules = new List<ItemDisplayRuleSet.NamedRuleGroup>();

        //    equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Jetpack", "DisplayBugWings", "A_Bust", new Vector3(2.395F, 1.2027F, 0F), new Vector3(19.6332F, 270F, 0F), new Vector3(1F, 1F, 1F)));

        //    equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("GoldGat", "DisplayGoldGat", "A_ClavivleL", new Vector3(0F, -0.2019F, 4.1597F), new Vector3(68.0647F, 180F, 180F), new Vector3(1F, 1F, 1F)));

        //    equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("BFG", "DisplayBFG", "A_ArmR", new Vector3(-0.8662F, 1.8556F, 0.1537F), new Vector3(270F, 90F, 0F), new Vector3(1F, 1F, 1F)));


        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("CritGlasses", "DisplayGlasses", "A_Head", new Vector3(0, 0.0012f, 0.0015f), new Vector3(0, 0, 90), new Vector3(0.0025f, 0.002f, 0.0025f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Syringe", "DisplaySyringeCluster", "A_Bust", new Vector3(-0.001f, 0.003f, 0), new Vector3(25, 315, 0), new Vector3(0.002f, 0.002f, 0.002f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("NearbyDamageBonus", "DisplayDiamond", "Gun", new Vector3(0, -0.001f, 0), new Vector3(0, 0, 0), new Vector3(0.001f, 0.001f, 0.001f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ArmorReductionOnHit", "DisplayWarhammer", "A_HandL", new Vector3(-0.0005f, 0.001f, 0.006f), new Vector3(0, 0, 90), new Vector3(0.0035f, 0.0035f, 0.0035f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("SecondarySkillMagazine", "DisplayDoubleMag", "Gun", new Vector3(0.0008f, 0.001f, -0.0008f), new Vector3(15, 325, 0), new Vector3(0.001f, 0.001f, 0.001f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Bear", "DisplayBear", "A_Bust", new Vector3(-0.002f, -0.0025f, 0), new Vector3(0, 270, 0), new Vector3(0.0025f, 0.0025f, 0.0025f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("SprintOutOfCombat", "DisplayWhip", "A_Waist", new Vector3(0.003f, 0, 0), new Vector3(0, 200, 335), new Vector3(0.005f, 0.005f, 0.005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("PersonalShield", "DisplayShieldGenerator", "A_Bust", new Vector3(-0.0015f, 0, 0.0015f), new Vector3(45, 270, 90), new Vector3(0.002f, 0.002f, 0.002f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("RegenOnKill", "DisplaySteakCurved", "A_Head", new Vector3(0, 0.0022f, 0.0015f), new Vector3(335, 0, 180), new Vector3(0.001f, 0.001f, 0.001f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("FireballsOnHit", "DisplayFireballsOnHit", "Gun", new Vector3(0.003f, 0.016f, -0.003f), new Vector3(0, 120, 0), new Vector3(0.002f, 0.002f, 0.002f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Hoof", "DisplayHoof", "A_KneeR", new Vector3(0, 0.0034f, -0.0006f), new Vector3(80, 0, 0), new Vector3(0.0012f, 0.0012f, 0.001f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("WardOnLevel", "DisplayWarbanner", "A_Waist", new Vector3(0, 0.001f, -0.0012f), new Vector3(0, 0, 90), new Vector3(0.005f, 0.005f, 0.005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BarrierOnOverHeal", "DisplayAegis", "A_ArmL", new Vector3(0.0008f, 0, 0), new Vector3(90, 270, 0), new Vector3(0.0035f, 0.0035f, 0.0035f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("WarCryOnMultiKill", "DisplayPauldron", "A_ShoulderL", new Vector3(0.0015f, 0.0005f, 0), new Vector3(60, 90, 0), new Vector3(0.01f, 0.01f, 0.01f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("SprintArmor", "DisplayBuckler", "A_ArmR", new Vector3(0, 0.0025f, 0), new Vector3(0, 270, 90), new Vector3(0.003f, 0.003f, 0.003f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("IceRing", "DisplayIceRing", "Gun", new Vector3(0, 0.0018f, 0), new Vector3(270, 90, 0), new Vector3(0.005f, 0.005f, 0.005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("FireRing", "DisplayFireRing", "Gun", new Vector3(0, 0.002f, 0), new Vector3(270, 90, 0), new Vector3(0.005f, 0.005f, 0.005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Behemoth", "DisplayBehemoth", "Gun", new Vector3(-0.002f, 0.008f, 0), new Vector3(0, 280, 0), new Vector3(0.001f, 0.001f, 0.001f)));
        //    itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Missile", "DisplayMissileLauncher", "A_Bust", new Vector3(0.9197F, 6.1691F, 1.4894F), new Vector3(0F, 270F, 0F), new Vector3(1F, 1F, 1F)));

        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Dagger", "DisplayDagger", "A_Bust", new Vector3(0, 0.002f, 0), new Vector3(270, 45, 0), new Vector3(0.01f, 0.01f, 0.01f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ChainLightning", "DisplayUkulele", "Gun", new Vector3(0.3194F, -2.3098F, -0.4187F), new Vector3(36.7132F, 89.7111F, 10.8468F), new Vector3(1F, 1F, 1F)));

        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("GhostOnKill", "DisplayMask", "A_Head", new Vector3(0, 0.001f, 0.0012f), new Vector3(0, 0, 0), new Vector3(0.005f, 0.005f, 0.005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Mushroom", "DisplayMushroom", "A_Bust", new Vector3(0.0012f, 0.003f, 0), new Vector3(45, 90, 0), new Vector3(0.001f, 0.001f, 0.001f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("AttackSpeedOnCrit", "DisplayWolfPelt", "A_Head", new Vector3(0, 0.0024f, 0), new Vector3(340, 0, 0), new Vector3(0.005f, 0.005f, 0.005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BleedOnHit", "DisplayTriTip", "A_HandL", new Vector3(-0.0004f, 0.001f, -0.0005f), new Vector3(0, 180, 0), new Vector3(0.004f, 0.004f, 0.004f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("HealOnCrit", "DisplayScythe", "A_Bust", new Vector3(0, 0.002f, -0.002f), new Vector3(270, 0, 0), new Vector3(0.002f, 0.002f, 0.002f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("HealWhileSafe", "DisplaySnail", "A_Bust", new Vector3(0, 0, 0), new Vector3(270, 180, 0), new Vector3(0.0015f, 0.0015f, 0.0015f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Clover", "DisplayClover", "A_Head", new Vector3(-0.0008f, 0.003f, -0.0006f), new Vector3(270, 90, 0), new Vector3(0.005f, 0.005f, 0.005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("GoldOnHit", "DisplayBoneCrown", "A_Head", new Vector3(0, 0.002f, 0.0002f), new Vector3(10, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("JumpBoost", "DisplayWaxBird", "A_Head", new Vector3(0, -0.0036f, 0), new Vector3(0, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
        //    itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ArmorPlate", "DisplayRepulsionArmorPlate", "A_KneeL", new Vector3(-0.5912F, -0.7741F, -0.5183F), new Vector3(40.4F, 258.8467F, 357.4842F), new Vector3(3.2643F, 3F, 2F)));

        //    itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Feather", "DisplayFeather", "A_ArmL", new Vector3(0.0004F, 1.0197F, 0.7426F), new Vector3(75F, 0F, 0F), new Vector3(0.1F, 0.1F, 0.1F)));


        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Crowbar", "DisplayCrowbar", "Gun", new Vector3(0.00035f, 0.012f, 0.00035f), new Vector3(0, 110, 0), new Vector3(0.005f, 0.005f, 0.005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ExecuteLowHealthElite", "DisplayGuillotine", "A_LegR", new Vector3(0.0015f, 0, -0.0012f), new Vector3(60, 315, 0), new Vector3(0.0025f, 0.0025f, 0.0025f)));
        //    itemRules.Add(ItemDisplays.CreateGenericDisplayRule("EquipmentMagazine", "DisplayBattery", "A_Waist", new Vector3(1.1365F, -0.1514F, -0.2399F), new Vector3(0F, 0F, 0F), new Vector3(1F, 1F, 1F)));

        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Infusion", "DisplayInfusion", "A_Waist", new Vector3(-0.002f, 0.0015f, 0.001f), new Vector3(0, 300, 0), new Vector3(0.0075f, 0.0075f, 0.0075f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Medkit", "DisplayMedkit", "A_Waist", new Vector3(0, 0.0015f, -0.0008f), new Vector3(90, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Bandolier", "DisplayBandolier", "A_Bust", new Vector3(0, 0, 0), new Vector3(330, 270, 90), new Vector3(0.008f, 0.008f, 0.008f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BounceNearby", "DisplayHook", "A_Bust", new Vector3(0, 0.003f, -0.0012f), new Vector3(0, 0, 0), new Vector3(0.002f, 0.002f, 0.002f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("StunChanceOnHit", "DisplayStunGrenade", "A_KneeR", new Vector3(0, 0.002f, -0.0016f), new Vector3(90, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("IgniteOnKill", "DisplayGasoline", "A_LegR", new Vector3(0.0015f, 0.002f, -0.002f), new Vector3(90, 60, 0), new Vector3(0.005f, 0.005f, 0.005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Firework", "DisplayFirework", "A_Waist", new Vector3(-0.0015f, 0.002f, -0.001f), new Vector3(270, 5, 0), new Vector3(0.004f, 0.004f, 0.004f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("LunarDagger", "DisplayLunarDagger", "A_Bust", new Vector3(-0.002f, -0.002f, -0.0018f), new Vector3(45, 90, 270), new Vector3(0.0075f, 0.0075f, 0.0075f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Knurl", "DisplayKnurl", "A_Bust", new Vector3(-0.003f, 0.0015f, 0), new Vector3(90, 0, 0), new Vector3(0.00125f, 0.00125f, 0.00125f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BeetleGland", "DisplayBeetleGland", "A_Bust", new Vector3(0.0025f, 0.002f, -0.001f), new Vector3(0, 270, 0), new Vector3(0.0015f, 0.0015f, 0.0015f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("SprintBonus", "DisplaySoda", "A_Waist", new Vector3(0.002f, 0.001f, 0), new Vector3(270, 90, 0), new Vector3(0.004f, 0.004f, 0.004f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("StickyBomb", "DisplayStickyBomb", "A_Waist", new Vector3(0.0012f, 0.002f, -0.0014f), new Vector3(345, 15, 0), new Vector3(0.002f, 0.002f, 0.002f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("TreasureCache", "DisplayKey", "A_Waist", new Vector3(0, 0.0008f, -0.0012f), new Vector3(90, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BossDamageBonus", "DisplayAPRound", "A_Waist", new Vector3(-0.0012f, 0, -0.001f), new Vector3(90, 45, 0), new Vector3(0.008f, 0.008f, 0.008f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ExtraLife", "DisplayHippo", "A_Bust", new Vector3(-0.002f, 0, -0.001f), new Vector3(0, 220, 0), new Vector3(0.0025f, 0.0025f, 0.0025f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("KillEliteFrenzy", "DisplayBrainstalk", "A_Head", new Vector3(0, 0.002f, 0.0005f), new Vector3(0, 0, 0), new Vector3(0.002f, 0.002f, 0.002f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("RepeatHeal", "DisplayCorpseFlower", "A_Bust", new Vector3(0.0012f, 0.003f, 0), new Vector3(0, 25, 300), new Vector3(0.004f, 0.004f, 0.004f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("AutoCastEquipment", "DisplayFossil", "A_Waist", new Vector3(0.002f, 0.002f, 0.0012f), new Vector3(0, 315, 0), new Vector3(0.005f, 0.005f, 0.005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("TitanGoldDuringTP", "DisplayGoldHeart", "A_Bust", new Vector3(-0.002f, 0, 0.0012f), new Vector3(0, 235, 0), new Vector3(0.0025f, 0.0025f, 0.0025f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("SprintWisp", "DisplayBrokenMask", "A_ShoulderL", new Vector3(0.0015f, 0, 0), new Vector3(0, 90, 180), new Vector3(0.002f, 0.002f, 0.002f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BarrierOnKill", "DisplayBrooch", "A_Bust", new Vector3(0, 0.002f, 0.0012f), new Vector3(90, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("TPHealingNova", "DisplayGlowFlower", "A_Bust", new Vector3(0.0012f, 0.002f, 0.0018f), new Vector3(340, 30, 0), new Vector3(0.004f, 0.004f, 0.004f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("LunarUtilityReplacement", "DisplayBirdFoot", "A_Head", new Vector3(0, 0.002f, -0.0012f), new Vector3(0, 270, 0), new Vector3(0.005f, 0.005f, 0.005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Thorns", "DisplayRazorwireLeft", "Gun", new Vector3(0, 0.006f, -0.001f), new Vector3(270, 300, 0), new Vector3(0.006f, 0.009f, 0.012f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("LunarPrimaryReplacement", "DisplayBirdEye", "A_Head", new Vector3(0, 0.001f, 0.0012f), new Vector3(270, 0, 0), new Vector3(0.003f, 0.003f, 0.003f)));
        //    itemRules.Add(ItemDisplays.CreateGenericDisplayRule("NovaOnLowHealth", "DisplayJellyGuts", "A_Waist", new Vector3(-0.5173F, 0.5587F, -0.6604F), new Vector3(328.9744F, 13.2779F, 335.4454F), new Vector3(1F, 1F, 1F)));

        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("LunarTrinket", "DisplayBeads", "A_ArmL", new Vector3(0, 0.0008f, 0), new Vector3(0, 90, 90), new Vector3(0.01f, 0.01f, 0.01f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Plant", "DisplayInterstellarDeskPlant", "A_Bust", new Vector3(0, 0.002f, 0), new Vector3(270, 90, 0), new Vector3(0.02f, 0.0175f, 0.0175f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("DeathMark", "DisplayDeathMark", "A_HandL", new Vector3(0, 0, 0), new Vector3(90, 270, 0), new Vector3(0.0004f, 0.0004f, 0.0004f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("CooldownOnCrit", "DisplaySkull", "A_Bust", new Vector3(0, 0.0012f, 0.0024f), new Vector3(270, 0, 0), new Vector3(0.0025f, 0.0025f, 0.0025f)));

        //    itemRules.Add(ItemDisplays.CreateGenericDisplayRule("UtilitySkillMagazine", "DisplayAfterburnerShoulderRing", "A_Bust", new Vector3(2.5049F, 0.3424F, -0.0001F), new Vector3(0F, 0F, 41.3981F), new Vector3(2F, 2F, 2F)));

        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ExplodeOnDeath", "DisplayWilloWisp", "A_Waist", new Vector3(-0.002f, 0.0012f, 0), new Vector3(0, 0, 0), new Vector3(0.0005f, 0.0005f, 0.0005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Seed", "DisplaySeed", "A_ArmR", new Vector3(0, 0, -0.0004f), new Vector3(270, 0, 0), new Vector3(0.0005f, 0.0005f, 0.0005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Phasing", "DisplayStealthkit", "A_KneeL", new Vector3(0, 0.002f, -0.0012f), new Vector3(90, 0, 0), new Vector3(0.004f, 0.005f, 0.005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ShockNearby", "DisplayTeslaCoil", "A_Bust", new Vector3(0, 0.0025f, -0.0015f), new Vector3(290, 0, 0), new Vector3(0.005f, 0.005f, 0.005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("AlienA_Head", "DisplayAlienHead", "Gun", new Vector3(0, 0, 0), new Vector3(315, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("A_HeadHunter", "DisplaySkullCrown", "A_Head", new Vector3(0, 0.003f, 0), new Vector3(0, 0, 0), new Vector3(0.005f, 0.002f, 0.002f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("EnergizedOnEquipmentUse", "DisplayWarHorn", "A_Waist", new Vector3(-0.0028f, 0.002f, 0), new Vector3(0, 190, 270), new Vector3(0.005f, 0.005f, 0.005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Tooth", "DisplayToothMeshLarge", "A_Bust", new Vector3(0, 0.003f, 0), new Vector3(290, 0, 0), new Vector3(0.08f, 0.08f, 0.08f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Pearl", "DisplayPearl", "A_HandL", new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0.002f, 0.002f, 0.002f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ShinyPearl", "DisplayShinyPearl", "A_HaveR", new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0.002f, 0.002f, 0.002f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BonusGoldPackOnKill", "DisplayTome", "A_LegL", new Vector3(-0.0008f, 0.0012f, -0.0024f), new Vector3(20, 200, 0), new Vector3(0.0008f, 0.0008f, 0.0008f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Squid", "DisplaySquidTurret", "root", new Vector3(0, 0.001f, -0.0012f), new Vector3(270, 0, 0), new Vector3(0.001f, 0.001f, 0.001f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("LaserTurbine", "DisplayLaserTurbine", "A_LegR", new Vector3(0.012f, 0.0032f, 0), new Vector3(0, 90, 0), new Vector3(0.005f, 0.005f, 0.005f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Incubator", "DisplayAncestralIncubator", "A_Bust", new Vector3(0, 0, 0), new Vector3(330, 0, 0), new Vector3(0.0006f, 0.0006f, 0.0006f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("SiphonOnLowHealth", "DisplaySiphonOnLowHealth", "A_Waist", new Vector3(0.0006f, 0, -0.0006f), new Vector3(0, 315, 0), new Vector3(0.001f, 0.001f, 0.001f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BleedOnHitAndExplode", "DisplayBleedOnHitAndExplode", "A_LegR", new Vector3(0, 0.0025f, 0.002f), new Vector3(0, 0, 0), new Vector3(0.001f, 0.001f, 0.001f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("MonstersOnShrineUse", "DisplayMonstersOnShrineUse", "A_LegL", new Vector3(0, 0.003f, -0.0024f), new Vector3(90, 90, 0), new Vector3(0.001f, 0.001f, 0.001f)));
        //    //itemRules.Add(ItemDisplays.CreateGenericDisplayRule("RandomDamageZone", "DisplayRandomDamageZone", "A_HaveR", new Vector3(-0.001f, 0.0006f, -0.0005f), new Vector3(0, 90, 270), new Vector3(0.0008f, 0.0006f, 0.0008f)));

        //    equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("QuestVolatileBattery", "DisplayBatteryArray", "A_Bust", new Vector3(2.7872F, 1.7367F, -0.0556F), new Vector3(27.6515F, 270F, 0F), new Vector3(1F, 1F, 1F)));


        //    equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("CommandMissile", "DisplayMissileRack", "A_ClavivleL", new Vector3(-0.7502F, 0.4576F, 2.1268F), new Vector3(0F, 75.1807F, 118.4881F), new Vector3(1F, 1F, 1F)));

        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Fruit", "DisplayFruit", "A_Bust", new Vector3(0, -0.005f, 0.004f), new Vector3(0, 150, 0), new Vector3(0.005f, 0.005f, 0.005f)));
        //   // equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("AffixWhite", "DisplayEliteIceCrown", "A_Head", new Vector3(0, 0.003f, 0), new Vector3(270, 0, 0), new Vector3(0.0003f, 0.0003f, 0.0003f)));
        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("AffixPoison", "DisplayEliteUrchinCrown", "A_Head", new Vector3(0, 0.0025f, 0), new Vector3(270, 0, 0), new Vector3(0.0005f, 0.0005f, 0.0005f)));
        //   // equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("AffixHaunted", "DisplayEliteStealthCrown", "A_Head", new Vector3(0, 0.002f, 0), new Vector3(270, 0, 0), new Vector3(0.0008f, 0.0008f, 0.0008f)));
        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("CritOnUse", "DisplayNeuralImplant", "A_Head", new Vector3(0, 0.0012f, 0.002f), new Vector3(0, 0, 0), new Vector3(0.004f, 0.004f, 0.004f)));
        //    equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("DroneBackup", "DisplayRadio", "A_Waist", new Vector3(0.059F, 0.48F, 1.4496F), new Vector3(341.817F, 0F, 3.258F), new Vector3(2F, 2F, 2F)));

        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Lightning", "DisplayLightning", "A_ShoulderL", new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("BurnNearby", "DisplayPotion", "A_Waist", new Vector3(0.0025f, 0.0012f, 0), new Vector3(0, 0, 330), new Vector3(0.0005f, 0.0005f, 0.0005f)));
        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("CrippleWard", "DisplayEffigy", "A_Waist", new Vector3(0.0025f, 0.0012f, 0), new Vector3(0, 270, 0), new Vector3(0.004f, 0.004f, 0.004f)));
        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("GainArmor", "DisplayElephantFigure", "A_KneeR", new Vector3(0, 0.003f, 0.0012f), new Vector3(90, 0, 0), new Vector3(0.008f, 0.008f, 0.008f)));
        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Recycle", "DisplayRecycler", "A_Bust", new Vector3(0, 0.002f, -0.002f), new Vector3(0, 90, 0), new Vector3(0.001f, 0.001f, 0.001f)));
        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("FireBallDash", "DisplayEgg", "A_Waist", new Vector3(0.0025f, 0.0012f, 0), new Vector3(270, 0, 0), new Vector3(0.003f, 0.003f, 0.003f)));
        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Cleanse", "DisplayWaterPack", "A_Bust", new Vector3(0, 0, -0.0018f), new Vector3(0, 180, 0), new Vector3(0.0014f, 0.0014f, 0.0014f)));
        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Tonic", "DisplayTonic", "A_Waist", new Vector3(0.0025f, 0.0012f, 0), new Vector3(0, 90, 0), new Vector3(0.003f, 0.003f, 0.003f)));
        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Gateway", "DisplayVase", "A_Waist", new Vector3(0.0025f, 0.0012f, 0), new Vector3(0, 0, 0), new Vector3(0.003f, 0.003f, 0.003f)));
        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Scanner", "DisplayScanner", "A_Waist", new Vector3(0.0025f, 0.0012f, 0), new Vector3(270, 90, 0), new Vector3(0.0015f, 0.0015f, 0.0015f)));
        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("DeathProjectile", "DisplayDeathProjectile", "A_Waist", new Vector3(-0.0024f, 0, -0.001f), new Vector3(0, 240, 0), new Vector3(0.0015f, 0.0015f, 0.0015f)));
        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("LifestealOnHit", "DisplayLifestealOnHit", "A_Head", new Vector3(-0.0015f, 0.004f, 0), new Vector3(45, 90, 0), new Vector3(0.001f, 0.001f, 0.001f)));
        //    //equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("TeamWarCry", "DisplayTeamWarCry", "A_Waist", new Vector3(0, 0, 0.003f), new Vector3(0, 0, 0), new Vector3(0.0015f, 0.0015f, 0.0015f)));

        //    //itemRules.Add(ItemDisplays.CreateFollowerDisplayRule("Icicle", "DisplayFrostRelic", new Vector3(0.013f, 0.01f, -0.006f), new Vector3(90, 0, 0), new Vector3(2, 2, 2)));
        //    //itemRules.Add(ItemDisplays.CreateFollowerDisplayRule("Talisman", "DisplayTalisman", new Vector3(-0.013f, 0.01f, -0.006f), new Vector3(0, 0, 0), new Vector3(1, 1, 1)));
        //    //itemRules.Add(ItemDisplays.CreateFollowerDisplayRule("FocusConvergence", "DisplayFocusedConvergence", new Vector3(-0.01f, 0.005f, -0.01f), new Vector3(0, 0, 0), new Vector3(0.2f, 0.2f, 0.2f)));

        //    //equipmentRules.Add(ItemDisplays.CreateFollowerDisplayRule("Saw", "DisplaySawmerang", new Vector3(0, 0.01f, -0.015f), new Vector3(90, 0, 0), new Vector3(0.25f, 0.25f, 0.25f)));
        //    //equipmentRules.Add(ItemDisplays.CreateFollowerDisplayRule("Meteor", "DisplayMeteor", new Vector3(0, 0.01f, -0.015f), new Vector3(90, 0, 0), new Vector3(1, 1, 1)));
        //    //equipmentRules.Add(ItemDisplays.CreateFollowerDisplayRule("Blackhole", "DisplayGravCube", new Vector3(0, 0.01f, -0.015f), new Vector3(90, 0, 0), new Vector3(1, 1, 1)));
        //    itemRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
        //    {
        //        name = "IncreaseHealing",
        //        displayRuleGroup = new DisplayRuleGroup
        //        {
        //            rules = new ItemDisplayRule[]
        //               {
        //                new ItemDisplayRule
        //                {
        //                    ruleType = ItemDisplayRuleType.ParentedPrefab,
        //                    followerPrefab = ItemDisplays.LoadDisplay("DisplayAntler"),
        //                    childName = "A_Head",
        //                    localPos = new Vector3(0.1102F, 1.0538F, 0.8495F),
        //                    localAngles = new Vector3(0F, 0F, 0F),
        //                    localScale = new Vector3(1F, 1F, 1F),
        //                    limbMask = LimbFlags.None
        //                },
        //                new ItemDisplayRule
        //                {
        //                    ruleType = ItemDisplayRuleType.ParentedPrefab,
        //                    followerPrefab = ItemDisplays.LoadDisplay("DisplayAntler"),
        //                    childName = "A_Head",
        //                    localPos = new Vector3(0.1102F, 1.0538F, -0.8495F),
        //                    localAngles = new Vector3(0F, 180F, 0F),
        //                    localScale = new Vector3(1F, 1F, 1F),
        //                    limbMask = LimbFlags.None
        //                }
        //               }
        //        }
        //    });

        //    //equipmentRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
        //    //{
        //    //    name = "AffixRed",
        //    //    displayRuleGroup = new DisplayRuleGroup
        //    //    {
        //    //        rules = new ItemDisplayRule[]
        //    //        {
        //    //            new ItemDisplayRule
        //    //            {
        //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
        //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteHorn"),
        //    //                childName = "A_Head",
        //    //                localPos = new Vector3(0, 0.002f, 0),
        //    //                localAngles = new Vector3(0, 0, 0),
        //    //                localScale = new Vector3(0.001f, 0.001f, 0.001f),
        //    //                limbMask = LimbFlags.None
        //    //            },
        //    //            new ItemDisplayRule
        //    //            {
        //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
        //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteHorn"),
        //    //                childName = "A_Head",
        //    //                localPos = new Vector3(0, 0.002f, 0),
        //    //                localAngles = new Vector3(0, 180, 0),
        //    //                localScale = new Vector3(0.001f, 0.001f, -0.001f),
        //    //                limbMask = LimbFlags.None
        //    //            }
        //    //        }
        //    //    }
        //    //});

        //    //equipmentRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
        //    //{
        //    //    name = "AffixBlue",
        //    //    displayRuleGroup = new DisplayRuleGroup
        //    //    {
        //    //        rules = new ItemDisplayRule[]
        //    //        {
        //    //            new ItemDisplayRule
        //    //            {
        //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
        //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteRhinoHorn"),
        //    //                childName = "A_Head",
        //    //                localPos = new Vector3(0, 0.002f, 0.002f),
        //    //                localAngles = new Vector3(315, 0, 0),
        //    //                localScale = new Vector3(0.005f, 0.005f, 0.005f),
        //    //                limbMask = LimbFlags.None
        //    //            },
        //    //            new ItemDisplayRule
        //    //            {
        //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
        //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteRhinoHorn"),
        //    //                childName = "A_Head",
        //    //                localPos = new Vector3(0, 0.002f, 0),
        //    //                localAngles = new Vector3(290, 0, 0),
        //    //                localScale = new Vector3(0.0025f, 0.0025f, 0.0025f),
        //    //                limbMask = LimbFlags.None
        //    //            }
        //    //        }
        //    //    }
        //    //});

        //    //itemRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
        //    //{
        //    //    name = "ShieldOnly",
        //    //    displayRuleGroup = new DisplayRuleGroup
        //    //    {
        //    //        rules = new ItemDisplayRule[]
        //    //        {
        //    //            new ItemDisplayRule
        //    //            {
        //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
        //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayShieldBug"),
        //    //                childName = "A_Head",
        //    //                localPos = new Vector3(0, 0.002f, 0.001f),
        //    //                localAngles = new Vector3(0, 0, 0),
        //    //                localScale = new Vector3(0.004f, 0.004f, 0.004f),
        //    //                limbMask = LimbFlags.None
        //    //            },
        //    //            new ItemDisplayRule
        //    //            {
        //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
        //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayShieldBug"),
        //    //                childName = "A_Head",
        //    //                localPos = new Vector3(0, 0.002f, 0.001f),
        //    //                localAngles = new Vector3(0, 0, 0),
        //    //                localScale = new Vector3(-0.004f, 0.004f, 0.004f),
        //    //                limbMask = LimbFlags.None
        //    //            }
        //    //        }
        //    //    }
        //    //});


        //    itemRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
        //    {
        //        name = "FallBoots",
        //        displayRuleGroup = new DisplayRuleGroup
        //        {
        //            rules = new ItemDisplayRule[]
        //            {
        //                new ItemDisplayRule
        //                {
        //                    ruleType = ItemDisplayRuleType.ParentedPrefab,
        //                    followerPrefab = ItemDisplays.LoadDisplay("DisplayGravBoots"),
        //                    childName = "A_KneeR",
        //                    localPos = new Vector3(0.0406F, 5.2503F, -0.0161F),
        //                    localAngles = new Vector3(0F, 90F, 0F),
        //                    localScale = new Vector3(1.75F, 1.75F, 1.8F),
        //                    limbMask = LimbFlags.None
        //                },
        //                new ItemDisplayRule
        //                {
        //                    ruleType = ItemDisplayRuleType.ParentedPrefab,
        //                    followerPrefab = ItemDisplays.LoadDisplay("DisplayGravBoots"),
        //                    childName = "A_KneeL",
        //                    localPos = new Vector3(0.044F, 5.4006F, -0.0128F),
        //                    localAngles = new Vector3(0F, 90F, 0F),
        //                    localScale = new Vector3(1.75F, 1.75F, 1.8F),
        //                    limbMask = LimbFlags.None
        //                }
        //            }
        //        }
        //    });
        //    //itemRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
        //    //{
        //    //    name = "NovaOnHeal",
        //    //    displayRuleGroup = new DisplayRuleGroup
        //    //    {
        //    //        rules = new ItemDisplayRule[]
        //    //           {
        //    //            new ItemDisplayRule
        //    //            {
        //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
        //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayDevilHorns"),
        //    //                childName = "A_Head",
        //    //                localPos = new Vector3(0, 0, 0),
        //    //                localAngles = new Vector3(0, 0, 20),
        //    //                localScale = new Vector3(0.01f, 0.01f, 0.01f),
        //    //                limbMask = LimbFlags.None
        //    //            },
        //    //            new ItemDisplayRule
        //    //            {
        //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
        //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayDevilHorns"),
        //    //                childName = "A_Head",
        //    //                localPos = new Vector3(0, 0, 0),
        //    //                localAngles = new Vector3(0, 0, 340),
        //    //                localScale = new Vector3(-0.01f, 0.01f, 0.01f),
        //    //                limbMask = LimbFlags.None
        //    //            }
        //    //           }
        //    //    }
        //    //});

        //    BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        //    ItemDisplayRuleSet.NamedRuleGroup[] item = itemRules.ToArray();
        //    ItemDisplayRuleSet.NamedRuleGroup[] equip = equipmentRules.ToArray();
        //    typeof(ItemDisplayRuleSet).GetField("namedItemRuleGroups", bindingAttr).SetValue(itemDisplayRuleSet, item);
        //    typeof(ItemDisplayRuleSet).GetField("namedEquipmentRuleGroups", bindingAttr).SetValue(itemDisplayRuleSet, equip);

        //    characterModel.itemDisplayRuleSet = itemDisplayRuleSet;
        //}

        //public static ItemDisplayRuleSet.NamedRuleGroup CreateGenericDisplayRule(string itemName, string prefabName,string childName, Vector3 position, Vector3 rotation, Vector3 scale)
        //{
        //    ItemDisplayRuleSet.NamedRuleGroup displayRule = new ItemDisplayRuleSet.NamedRuleGroup
        //    {
        //        name = itemName,
        //        displayRuleGroup = new DisplayRuleGroup
        //        {
        //            rules = new ItemDisplayRule[]
        //{
        //                new ItemDisplayRule
        //                {
        //                    ruleType = ItemDisplayRuleType.ParentedPrefab,
        //                    childName = childName,
        //                    followerPrefab = ItemDisplays.LoadDisplay(prefabName),
        //                    limbMask = LimbFlags.None,
        //                    localPos = position,
        //                    localAngles = rotation,
        //                    localScale = scale
        //                }
        //}
        //        }
        //    };

        //    return displayRule;
        //}

        //public static ItemDisplayRuleSet.NamedRuleGroup CreateGenericDisplayRule(string itemName, GameObject itemPrefab, string childName, Vector3 position, Vector3 rotation, Vector3 scale)
        //{
        //    ItemDisplayRuleSet.NamedRuleGroup displayRule = new ItemDisplayRuleSet.NamedRuleGroup
        //    {
        //        name = itemName,
        //        displayRuleGroup = new DisplayRuleGroup
        //        {
        //            rules = new ItemDisplayRule[]
        //            {
        //                new ItemDisplayRule
        //                {
        //                    ruleType = ItemDisplayRuleType.ParentedPrefab,
        //                    childName = childName,
        //                    followerPrefab = itemPrefab,
        //                    limbMask = LimbFlags.None,
        //                    localPos = position,
        //                    localAngles = rotation,
        //                    localScale = scale
        //                }
        //            }
        //        }
        //    };

        //    return displayRule;
        //}

        //public static ItemDisplayRuleSet.NamedRuleGroup CreateFollowerDisplayRule(string itemName, string prefabName, Vector3 position, Vector3 rotation, Vector3 scale)
        //{
        //    ItemDisplayRuleSet.NamedRuleGroup displayRule = new ItemDisplayRuleSet.NamedRuleGroup
        //    {
        //        name = itemName,
        //        displayRuleGroup = new DisplayRuleGroup
        //        {
        //            rules = new ItemDisplayRule[]
        //            {
        //                new ItemDisplayRule
        //                {
        //                    ruleType = ItemDisplayRuleType.ParentedPrefab,
        //                    childName = "Base",
        //                    followerPrefab = ItemDisplays.LoadDisplay(prefabName),
        //                    limbMask = LimbFlags.None,
        //                    localPos = position,
        //                    localAngles = rotation,
        //                    localScale = scale
        //                }
        //            }
        //        }
        //    };

        //    return displayRule;
        //}

        //public static ItemDisplayRuleSet.NamedRuleGroup CreateFollowerDisplayRule(string itemName, GameObject itemPrefab, Vector3 position, Vector3 rotation, Vector3 scale)
        //{
        //    ItemDisplayRuleSet.NamedRuleGroup displayRule = new ItemDisplayRuleSet.NamedRuleGroup
        //    {
        //        name = itemName,
        //        displayRuleGroup = new DisplayRuleGroup
        //        {
        //            rules = new ItemDisplayRule[]
        //            {
        //                new ItemDisplayRule
        //                {
        //                    ruleType = ItemDisplayRuleType.ParentedPrefab,
        //                    childName = "Base",
        //                    followerPrefab = itemPrefab,
        //                    limbMask = LimbFlags.None,
        //                    localPos = position,
        //                    localAngles = rotation,
        //                    localScale = scale
        //                }
        //            }
        //        }
        //    };

        //    return displayRule;
        //}

        //public static GameObject LoadDisplay(string name)
        //{
        //    if (itemDisplayPrefabs.ContainsKey(name.ToLower()))
        //    {
        //        if (itemDisplayPrefabs[name.ToLower()]) return itemDisplayPrefabs[name.ToLower()];
        //    }
        //    return null;
        //}

        //private static void PopulateDisplays()
        //{
        //    ItemDisplayRuleSet itemDisplayRuleSet = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").GetComponent<ModelLocator>().modelTransform.GetComponent<CharacterModel>().itemDisplayRuleSet;

        //    BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        //    ItemDisplayRuleSet.NamedRuleGroup[] array = typeof(ItemDisplayRuleSet).GetField("namedItemRuleGroups", bindingAttr).GetValue(itemDisplayRuleSet) as ItemDisplayRuleSet.NamedRuleGroup[];
        //    ItemDisplayRuleSet.NamedRuleGroup[] array2 = typeof(ItemDisplayRuleSet).GetField("namedEquipmentRuleGroups", bindingAttr).GetValue(itemDisplayRuleSet) as ItemDisplayRuleSet.NamedRuleGroup[];
        //    ItemDisplayRuleSet.NamedRuleGroup[] array3 = array;

        //    for (int i = 0; i < array3.Length; i++)
        //    {
        //        ItemDisplayRule[] rules = array3[i].displayRuleGroup.rules;
        //        for(int j = 0; j < rules.Length; j++)
        //        {
        //            GameObject followerPrefab = rules[j].followerPrefab;
        //            if (!(followerPrefab == null))
        //            {
        //                string name = followerPrefab.name;
        //                string key = (name != null) ? name.ToLower() : null;
        //                if (!itemDisplayPrefabs.ContainsKey(key))
        //                {
        //                    itemDisplayPrefabs[key] = followerPrefab;
        //                }
        //            }
        //        }
        //    }
        //    array3 = array2;
        //    for(int i = 0; i < array3.Length; i++)
        //    {
        //        ItemDisplayRule[] rules = array3[i].displayRuleGroup.rules;
        //        for(int j = 0; j < rules.Length; j++)
        //        {
        //            GameObject followerPrefab2 = rules[j].followerPrefab;
        //            if (!(followerPrefab2 == null))
        //            {
        //                string name2 = followerPrefab2.name;
        //                string key2 = (name2 != null) ? name2.ToLower() : null;
        //                if (!itemDisplayPrefabs.ContainsKey(key2))
        //                {
        //                    itemDisplayPrefabs[key2] = followerPrefab2;
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion
        internal static void InitializeItemDisplays()
        {
            PopulateDisplays();

            CharacterModel characterModel = Prefabs.samusPrefab.GetComponentInChildren<CharacterModel>();

            ItemDisplayRuleSet = ScriptableObject.CreateInstance<ItemDisplayRuleSet>();
            ItemDisplayRuleSet.name = "idrsSamus";

            characterModel.itemDisplayRuleSet = ItemDisplayRuleSet;
        }

        internal static void SetItemDisplays()
        {
            itemDisplayRules = new List<ItemDisplayRuleSet.KeyAssetRuleGroup>();

            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            {
                keyAsset = RoR2Content.Equipment.Jetpack,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBugWings"),
                            childName = "A_Bust",
                            localPos = new Vector3(2.395F, 1.2027F, 0F),
                            localAngles = new Vector3(19.6332F, 270F, 0F),
                            localScale = Vector3.one,
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            {
                keyAsset = RoR2Content.Equipment.BFG,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
        {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBFG"),
                            childName = "A_ArmR",
                            localPos = new Vector3(-0.8662F, 1.8556F, 0.1537F),
                            localAngles = new Vector3(270F, 90F, 0F),
                            localScale = Vector3.one,
                            limbMask = LimbFlags.None
                        }
        }
                }
            });

            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            {
                keyAsset = RoR2Content.Items.Missile,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayMissileLauncher"),
                            childName = "A_Bust",
                            localPos = new Vector3(0.9197F, 6.1691F, 1.4894F),
                            localAngles = new Vector3(0F, 270F, 0F),
                            localScale = Vector3.one,
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            {
                keyAsset = RoR2Content.Items.ArmorPlate,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayRepulsionArmorPlate"),
                            childName = "A_KneeL",
                            localPos = new Vector3(-0.5912F, -0.7741F, -0.5183F),
                            localAngles = new Vector3(40.4F, 258.8467F, 357.4842F),
                            localScale = new Vector3(3.2643F, 3F, 2F),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            }); 
            #region WIP
            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Feather,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //{
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFeather"),
            //                            childName = "A_ArmL",
            //                            localPos = new Vector3(0.0004F, 1.0197F, 0.7426F),
            //                            localAngles = new Vector3(75F, 0F, 0F),
            //                            localScale = new Vector3(0.1F, 0.1F, 0.1F),
            //                            limbMask = LimbFlags.None
            //                        }
            //}
            //                });
            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Jetpack,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //{
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBugWings"),
            //                            childName = "A_Bust",
            //                            localPos = new Vector3(2.395F, 1.2027F, 0F),
            //                            localAngles = new Vector3(19.6332F, 270F, 0F),
            //                            localScale = Vector3.one,
            //                            limbMask = LimbFlags.None
            //                        }
            //}
            //                });
            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Jetpack,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //{
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBugWings"),
            //                            childName = "A_Bust",
            //                            localPos = new Vector3(2.395F, 1.2027F, 0F),
            //                            localAngles = new Vector3(19.6332F, 270F, 0F),
            //                            localScale = Vector3.one,
            //                            limbMask = LimbFlags.None
            //                        }
            //}
            //                });
            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Jetpack,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //{
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBugWings"),
            //                            childName = "A_Bust",
            //                            localPos = new Vector3(2.395F, 1.2027F, 0F),
            //                            localAngles = new Vector3(19.6332F, 270F, 0F),
            //                            localScale = Vector3.one,
            //                            limbMask = LimbFlags.None
            //                        }
            //}
            //                });
            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Jetpack,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //{
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBugWings"),
            //                            childName = "A_Bust",
            //                            localPos = new Vector3(2.395F, 1.2027F, 0F),
            //                            localAngles = new Vector3(19.6332F, 270F, 0F),
            //                            localScale = Vector3.one,
            //                            limbMask = LimbFlags.None
            //                        }
            //}
            //                });
            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Jetpack,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //{
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBugWings"),
            //                            childName = "A_Bust",
            //                            localPos = new Vector3(2.395F, 1.2027F, 0F),
            //                            localAngles = new Vector3(19.6332F, 270F, 0F),
            //                            localScale = Vector3.one,
            //                            limbMask = LimbFlags.None
            //                        }
            //}
            //                });
            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Jetpack,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //{
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBugWings"),
            //                            childName = "A_Bust",
            //                            localPos = new Vector3(2.395F, 1.2027F, 0F),
            //                            localAngles = new Vector3(19.6332F, 270F, 0F),
            //                            localScale = Vector3.one,
            //                            limbMask = LimbFlags.None
            //                        }
            //}
            //                });
            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Jetpack,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //{
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBugWings"),
            //                            childName = "A_Bust",
            //                            localPos = new Vector3(2.395F, 1.2027F, 0F),
            //                            localAngles = new Vector3(19.6332F, 270F, 0F),
            //                            localScale = Vector3.one,
            //                            limbMask = LimbFlags.None
            //                        }
            //}
            //                });
            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Jetpack,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //{
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBugWings"),
            //                            childName = "A_Bust",
            //                            localPos = new Vector3(2.395F, 1.2027F, 0F),
            //                            localAngles = new Vector3(19.6332F, 270F, 0F),
            //                            localScale = Vector3.one,
            //                            limbMask = LimbFlags.None
            //                        }
            //}
            //                });
            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Jetpack,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //{
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBugWings"),
            //                            childName = "A_Bust",
            //                            localPos = new Vector3(2.395F, 1.2027F, 0F),
            //                            localAngles = new Vector3(19.6332F, 270F, 0F),
            //                            localScale = Vector3.one,
            //                            limbMask = LimbFlags.None
            //                        }
            //}
            //                });
            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Jetpack,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //{
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBugWings"),
            //                            childName = "A_Bust",
            //                            localPos = new Vector3(2.395F, 1.2027F, 0F),
            //                            localAngles = new Vector3(19.6332F, 270F, 0F),
            //                            localScale = Vector3.one,
            //                            limbMask = LimbFlags.None
            //                        }
            //}
            //                });
            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Jetpack,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //{
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBugWings"),
            //                            childName = "A_Bust",
            //                            localPos = new Vector3(2.395F, 1.2027F, 0F),
            //                            localAngles = new Vector3(19.6332F, 270F, 0F),
            //                            localScale = Vector3.one,
            //                            limbMask = LimbFlags.None
            //                        }
            //}
            //                });
            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Jetpack,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //{
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBugWings"),
            //                            childName = "A_Bust",
            //                            localPos = new Vector3(2.395F, 1.2027F, 0F),
            //                            localAngles = new Vector3(19.6332F, 270F, 0F),
            //                            localScale = Vector3.one,
            //                            limbMask = LimbFlags.None
            //                        }
            //}
            //                });
            //("Feather", "DisplayFeather", "A_ArmL", new Vector3(0.0004F, 1.0197F, 0.7426F), new Vector3(75F, 0F, 0F), new Vector3(0.1F, 0.1F, 0.1F)));
            //("EquipmentMagazine", "DisplayBattery", "A_Waist", new Vector3(1.1365F, -0.1514F, -0.2399F), new Vector3(0F, 0F, 0F), new Vector3(1F, 1F, 1F)));
            //("NovaOnLowHealth", "DisplayJellyGuts", "A_Waist", new Vector3(-0.5173F, 0.5587F, -0.6604F), new Vector3(328.9744F, 13.2779F, 335.4454F), new Vector3(1F, 1F, 1F)));
            //("UtilitySkillMagazine", "DisplayAfterburnerShoulderRing", "A_Bust", new Vector3(2.5049F, 0.3424F, -0.0001F), new Vector3(0F, 0F, 41.3981F), new Vector3(2F, 2F, 2F)));
            //("QuestVolatileBattery", "DisplayBatteryArray", "A_Bust", new Vector3(2.7872F, 1.7367F, -0.0556F), new Vector3(27.6515F, 270F, 0F), new Vector3(1F, 1F, 1F)));
            //("CommandMissile", "DisplayMissileRack", "A_ClavivleL", new Vector3(-0.7502F, 0.4576F, 2.1268F), new Vector3(0F, 75.1807F, 118.4881F), new Vector3(1F, 1F, 1F)));
            //("DroneBackup", "DisplayRadio", "A_Waist", new Vector3(0.059F, 0.48F, 1.4496F), new Vector3(341.817F, 0F, 3.258F), new Vector3(2F, 2F, 2F)));
            //        name = "IncreaseHealing",
            //        displayRuleGroup = new DisplayRuleGroup
            //        {
            //            rules = new ItemDisplayRule[]
            //               {
            //                new ItemDisplayRule
            //                {
            //                    ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                    followerPrefab = ItemDisplays.LoadDisplay("DisplayAntler"),
            //                    childName = "A_Head",
            //                    localPos = new Vector3(0.1102F, 1.0538F, 0.8495F),
            //                    localAngles = new Vector3(0F, 0F, 0F),
            //                    localScale = new Vector3(1F, 1F, 1F),
            //                    limbMask = LimbFlags.None
            //                },
            //                new ItemDisplayRule
            //                {
            //                    ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                    followerPrefab = ItemDisplays.LoadDisplay("DisplayAntler"),
            //                    childName = "A_Head",
            //                    localPos = new Vector3(0.1102F, 1.0538F, -0.8495F),
            //                    localAngles = new Vector3(0F, 180F, 0F),
            //                    localScale = new Vector3(1F, 1F, 1F),
            //                    limbMask = LimbFlags.None
            //                }
            //               }
            //        }
            //    });

            //    //equipmentRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
            //    //{
            //    //    name = "AffixRed",
            //    //    displayRuleGroup = new DisplayRuleGroup
            //    //    {
            //    //        rules = new ItemDisplayRule[]
            //    //        {
            //    //            new ItemDisplayRule
            //    //            {
            //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
            //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteHorn"),
            //    //                childName = "A_Head",
            //    //                localPos = new Vector3(0, 0.002f, 0),
            //    //                localAngles = new Vector3(0, 0, 0),
            //    //                localScale = new Vector3(0.001f, 0.001f, 0.001f),
            //    //                limbMask = LimbFlags.None
            //    //            },
            //    //            new ItemDisplayRule
            //    //            {
            //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
            //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteHorn"),
            //    //                childName = "A_Head",
            //    //                localPos = new Vector3(0, 0.002f, 0),
            //    //                localAngles = new Vector3(0, 180, 0),
            //    //                localScale = new Vector3(0.001f, 0.001f, -0.001f),
            //    //                limbMask = LimbFlags.None
            //    //            }
            //    //        }
            //    //    }
            //    //});

            //    //equipmentRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
            //    //{
            //    //    name = "AffixBlue",
            //    //    displayRuleGroup = new DisplayRuleGroup
            //    //    {
            //    //        rules = new ItemDisplayRule[]
            //    //        {
            //    //            new ItemDisplayRule
            //    //            {
            //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
            //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteRhinoHorn"),
            //    //                childName = "A_Head",
            //    //                localPos = new Vector3(0, 0.002f, 0.002f),
            //    //                localAngles = new Vector3(315, 0, 0),
            //    //                localScale = new Vector3(0.005f, 0.005f, 0.005f),
            //    //                limbMask = LimbFlags.None
            //    //            },
            //    //            new ItemDisplayRule
            //    //            {
            //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
            //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteRhinoHorn"),
            //    //                childName = "A_Head",
            //    //                localPos = new Vector3(0, 0.002f, 0),
            //    //                localAngles = new Vector3(290, 0, 0),
            //    //                localScale = new Vector3(0.0025f, 0.0025f, 0.0025f),
            //    //                limbMask = LimbFlags.None
            //    //            }
            //    //        }
            //    //    }
            //    //});

            //    //itemRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
            //    //{
            //    //    name = "ShieldOnly",
            //    //    displayRuleGroup = new DisplayRuleGroup
            //    //    {
            //    //        rules = new ItemDisplayRule[]
            //    //        {
            //    //            new ItemDisplayRule
            //    //            {
            //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
            //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayShieldBug"),
            //    //                childName = "A_Head",
            //    //                localPos = new Vector3(0, 0.002f, 0.001f),
            //    //                localAngles = new Vector3(0, 0, 0),
            //    //                localScale = new Vector3(0.004f, 0.004f, 0.004f),
            //    //                limbMask = LimbFlags.None
            //    //            },
            //    //            new ItemDisplayRule
            //    //            {
            //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
            //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayShieldBug"),
            //    //                childName = "A_Head",
            //    //                localPos = new Vector3(0, 0.002f, 0.001f),
            //    //                localAngles = new Vector3(0, 0, 0),
            //    //                localScale = new Vector3(-0.004f, 0.004f, 0.004f),
            //    //                limbMask = LimbFlags.None
            //    //            }
            //    //        }
            //    //    }
            //    //});


            //    itemRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
            //    {
            //        name = "FallBoots",
            //        displayRuleGroup = new DisplayRuleGroup
            //        {
            //            rules = new ItemDisplayRule[]
            //            {
            //                new ItemDisplayRule
            //                {
            //                    ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                    followerPrefab = ItemDisplays.LoadDisplay("DisplayGravBoots"),
            //                    childName = "A_KneeR",
            //                    localPos = new Vector3(0.0406F, 5.2503F, -0.0161F),
            //                    localAngles = new Vector3(0F, 90F, 0F),
            //                    localScale = new Vector3(1.75F, 1.75F, 1.8F),
            //                    limbMask = LimbFlags.None
            //                },
            //                new ItemDisplayRule
            //                {
            //                    ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                    followerPrefab = ItemDisplays.LoadDisplay("DisplayGravBoots"),
            //                    childName = "A_KneeL",
            //                    localPos = new Vector3(0.044F, 5.4006F, -0.0128F),
            //                    localAngles = new Vector3(0F, 90F, 0F),
            //                    localScale = new Vector3(1.75F, 1.75F, 1.8F),
            //                    limbMask = LimbFlags.None
            //                }
            //            }
            //        }
            //    });
            //    //itemRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
            //    //{
            //    //    name = "NovaOnHeal",
            //    //    displayRuleGroup = new DisplayRuleGroup
            //    //    {
            //    //        rules = new ItemDisplayRule[]
            //    //           {
            //    //            new ItemDisplayRule
            //    //            {
            //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
            //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayDevilHorns"),
            //    //                childName = "A_Head",
            //    //                localPos = new Vector3(0, 0, 0),
            //    //                localAngles = new Vector3(0, 0, 20),
            //    //                localScale = new Vector3(0.01f, 0.01f, 0.01f),
            //    //                limbMask = LimbFlags.None
            //    //            },
            //    //            new ItemDisplayRule
            //    //            {
            //    //                ruleType = ItemDisplayRuleType.ParentedPrefab,
            //    //                followerPrefab = ItemDisplays.LoadDisplay("DisplayDevilHorns"),
            //    //                childName = "A_Head",
            //    //                localPos = new Vector3(0, 0, 0),
            //    //                localAngles = new Vector3(0, 0, 340),
            //    //                localScale = new Vector3(-0.01f, 0.01f, 0.01f),
            //    //                limbMask = LimbFlags.None
            //    //            }
            //    //           }
            //    //    }
            #endregion
            ItemDisplayRuleSet.keyAssetRuleGroups = itemDisplayRules.ToArray();
            ItemDisplayRuleSet.GenerateRuntimeValues();
        }
        internal static void PopulateDisplays()
        {
            ItemDisplayRuleSet itemDisplayRuleSet = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").GetComponent<ModelLocator>().modelTransform.GetComponent<CharacterModel>().itemDisplayRuleSet;

            ItemDisplayRuleSet.KeyAssetRuleGroup[] item = itemDisplayRuleSet.keyAssetRuleGroups;

            for (int i = 0; i < item.Length; i++)
            {
                ItemDisplayRule[] rules = item[i].displayRuleGroup.rules;

                for(int j = 0; j < rules.Length; j++)
                {
                    GameObject followerPrefab = rules[j].followerPrefab;
                    if (followerPrefab)
                    {
                        string name = followerPrefab.name;
                        string key = (name != null) ? name.ToLower() : null;
                        if (!itemDisplayPrefabs.ContainsKey(key))
                        {
                            itemDisplayPrefabs[key] = followerPrefab;
                        }
                    }
                }
            }
        }

        internal static GameObject LoadDisplay(string name)
        {
            if (itemDisplayPrefabs.ContainsKey(name.ToLower()))
            {
                if (itemDisplayPrefabs[name.ToLower()]) return itemDisplayPrefabs[name.ToLower()];
            }

            return null;
        }
        

        
    }
}
