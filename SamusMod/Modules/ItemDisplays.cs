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
        public static List<ItemDisplayRuleSet.NamedRuleGroup> itemRules;
        public static List<ItemDisplayRuleSet.NamedRuleGroup> equipmentRules;

        public static GameObject capacitorPrefab;

        private static Dictionary<string, GameObject> itemDisplayPrefabs = new Dictionary<string, GameObject>();

        public static void RegisterDisplays()
        {
            PopulateDisplays();

            GameObject bodyPrefab = Prefabs.samusPrefab;

            GameObject model = bodyPrefab.GetComponent<ModelLocator>().modelTransform.gameObject;
            CharacterModel characterModel = model.GetComponent<CharacterModel>();

            ItemDisplayRuleSet itemDisplayRuleSet = ScriptableObject.CreateInstance<ItemDisplayRuleSet>();

            itemRules = new List<ItemDisplayRuleSet.NamedRuleGroup>();
            equipmentRules = new List<ItemDisplayRuleSet.NamedRuleGroup>();

            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Jetpack", "DisplayBugWings", "A_Bust", new Vector3(0, 0, 0), Vector3.zero, new Vector3(1f, 1f, 1f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("GoldGat", "DisplayGoldGat", "A_Bust", new Vector3(0.002f, 0.005f, -0.002f), new Vector3(0, 90, 290), new Vector3(0.002f, 0.002f, 0.002f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("BFG", "DisplayBFG", "A_Bust", new Vector3(-0.001f, 0.002f, 0), new Vector3(330, 0, 45), new Vector3(0.004f, 0.004f, 0.004f)));

            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("CritGlasses", "DisplayGlasses", "A_Head", new Vector3(0, 0.0012f, 0.0015f), new Vector3(0, 0, 90), new Vector3(0.0025f, 0.002f, 0.0025f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Syringe", "DisplaySyringeCluster", "A_Bust", new Vector3(-0.001f, 0.003f, 0), new Vector3(25, 315, 0), new Vector3(0.002f, 0.002f, 0.002f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("NearbyDamageBonus", "DisplayDiamond", "A_Gun", new Vector3(0, -0.001f, 0), new Vector3(0, 0, 0), new Vector3(0.001f, 0.001f, 0.001f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ArmorReductionOnHit", "DisplayWarhammer", "A_HandL", new Vector3(-0.0005f, 0.001f, 0.006f), new Vector3(0, 0, 90), new Vector3(0.0035f, 0.0035f, 0.0035f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("SecondarySkillMagazine", "DisplayDoubleMag", "A_Gun", new Vector3(0.0008f, 0.001f, -0.0008f), new Vector3(15, 325, 0), new Vector3(0.001f, 0.001f, 0.001f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Bear", "DisplayBear", "A_Bust", new Vector3(-0.002f, -0.0025f, 0), new Vector3(0, 270, 0), new Vector3(0.0025f, 0.0025f, 0.0025f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("SprintOutOfCombat", "DisplayWhip", "A_Waist", new Vector3(0.003f, 0, 0), new Vector3(0, 200, 335), new Vector3(0.005f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("PersonalShield", "DisplayShieldGenerator", "A_Bust", new Vector3(-0.0015f, 0, 0.0015f), new Vector3(45, 270, 90), new Vector3(0.002f, 0.002f, 0.002f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("RegenOnKill", "DisplaySteakCurved", "A_Head", new Vector3(0, 0.0022f, 0.0015f), new Vector3(335, 0, 180), new Vector3(0.001f, 0.001f, 0.001f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("FireballsOnHit", "DisplayFireballsOnHit", "A_Gun", new Vector3(0.003f, 0.016f, -0.003f), new Vector3(0, 120, 0), new Vector3(0.002f, 0.002f, 0.002f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Hoof", "DisplayHoof", "A_KneeR", new Vector3(0, 0.0034f, -0.0006f), new Vector3(80, 0, 0), new Vector3(0.0012f, 0.0012f, 0.001f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("WardOnLevel", "DisplayWarbanner", "A_Waist", new Vector3(0, 0.001f, -0.0012f), new Vector3(0, 0, 90), new Vector3(0.005f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BarrierOnOverHeal", "DisplayAegis", "A_ArmL", new Vector3(0.0008f, 0, 0), new Vector3(90, 270, 0), new Vector3(0.0035f, 0.0035f, 0.0035f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("WarCryOnMultiKill", "DisplayPauldron", "A_ShoulderL", new Vector3(0.0015f, 0.0005f, 0), new Vector3(60, 90, 0), new Vector3(0.01f, 0.01f, 0.01f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("SprintArmor", "DisplayBuckler", "A_ArmR", new Vector3(0, 0.0025f, 0), new Vector3(0, 270, 90), new Vector3(0.003f, 0.003f, 0.003f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("IceRing", "DisplayIceRing", "A_Gun", new Vector3(0, 0.0018f, 0), new Vector3(270, 90, 0), new Vector3(0.005f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("FireRing", "DisplayFireRing", "A_Gun", new Vector3(0, 0.002f, 0), new Vector3(270, 90, 0), new Vector3(0.005f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Behemoth", "DisplayBehemoth", "A_Gun", new Vector3(-0.002f, 0.008f, 0), new Vector3(0, 280, 0), new Vector3(0.001f, 0.001f, 0.001f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Missile", "DisplayMissileLauncher", "A_Bust", new Vector3(0.0025f, 0.0055f, 0), new Vector3(0, 0, 335), new Vector3(0.001f, 0.001f, 0.001f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Dagger", "DisplayDagger", "A_Bust", new Vector3(0, 0.002f, 0), new Vector3(270, 45, 0), new Vector3(0.01f, 0.01f, 0.01f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ChainLightning", "DisplayUkulele", "A_Gun", new Vector3(0.00035f, 0.014f, 0.00035f), new Vector3(0, 40, 180), new Vector3(0.01f, 0.01f, 0.01f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("GhostOnKill", "DisplayMask", "A_Head", new Vector3(0, 0.001f, 0.0012f), new Vector3(0, 0, 0), new Vector3(0.005f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Mushroom", "DisplayMushroom", "A_Bust", new Vector3(0.0012f, 0.003f, 0), new Vector3(45, 90, 0), new Vector3(0.001f, 0.001f, 0.001f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("AttackSpeedOnCrit", "DisplayWolfPelt", "A_Head", new Vector3(0, 0.0024f, 0), new Vector3(340, 0, 0), new Vector3(0.005f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BleedOnHit", "DisplayTriTip", "A_HandL", new Vector3(-0.0004f, 0.001f, -0.0005f), new Vector3(0, 180, 0), new Vector3(0.004f, 0.004f, 0.004f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("HealOnCrit", "DisplayScythe", "A_Bust", new Vector3(0, 0.002f, -0.002f), new Vector3(270, 0, 0), new Vector3(0.002f, 0.002f, 0.002f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("HealWhileSafe", "DisplaySnail", "A_Bust", new Vector3(0, 0, 0), new Vector3(270, 180, 0), new Vector3(0.0015f, 0.0015f, 0.0015f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Clover", "DisplayClover", "A_Head", new Vector3(-0.0008f, 0.003f, -0.0006f), new Vector3(270, 90, 0), new Vector3(0.005f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("GoldOnHit", "DisplayBoneCrown", "A_Head", new Vector3(0, 0.002f, 0.0002f), new Vector3(10, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("JumpBoost", "DisplayWaxBird", "A_Head", new Vector3(0, -0.0036f, 0), new Vector3(0, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ArmorPlate", "DisplayRepulsionArmorPlate", "A_KneeL", new Vector3(-0.0004f, 0.0024f, 0.0004f), new Vector3(90, 0, 0), new Vector3(0.004f, 0.004f, 0.004f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Feather", "DisplayFeather", "A_ArmL", new Vector3(0, 0.0004f, 0.0006f), new Vector3(90, 90, 0), new Vector3(0.00035f, 0.00035f, 0.00035f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Crowbar", "DisplayCrowbar", "A_Gun", new Vector3(0.00035f, 0.012f, 0.00035f), new Vector3(0, 110, 0), new Vector3(0.005f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ExecuteLowHealthElite", "DisplayGuillotine", "A_LegR", new Vector3(0.0015f, 0, -0.0012f), new Vector3(60, 315, 0), new Vector3(0.0025f, 0.0025f, 0.0025f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("EquipmentMagazine", "DisplayBattery", "A_Bust", new Vector3(0.001f, 0, 0.0025f), new Vector3(0, 270, 45), new Vector3(0.003f, 0.003f, 0.003f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Infusion", "DisplayInfusion", "A_Waist", new Vector3(-0.002f, 0.0015f, 0.001f), new Vector3(0, 300, 0), new Vector3(0.0075f, 0.0075f, 0.0075f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Medkit", "DisplayMedkit", "A_Waist", new Vector3(0, 0.0015f, -0.0008f), new Vector3(90, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Bandolier", "DisplayBandolier", "A_Bust", new Vector3(0, 0, 0), new Vector3(330, 270, 90), new Vector3(0.008f, 0.008f, 0.008f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BounceNearby", "DisplayHook", "A_Bust", new Vector3(0, 0.003f, -0.0012f), new Vector3(0, 0, 0), new Vector3(0.002f, 0.002f, 0.002f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("StunChanceOnHit", "DisplayStunGrenade", "A_KneeR", new Vector3(0, 0.002f, -0.0016f), new Vector3(90, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("IgniteOnKill", "DisplayGasoline", "A_LegR", new Vector3(0.0015f, 0.002f, -0.002f), new Vector3(90, 60, 0), new Vector3(0.005f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Firework", "DisplayFirework", "A_Waist", new Vector3(-0.0015f, 0.002f, -0.001f), new Vector3(270, 5, 0), new Vector3(0.004f, 0.004f, 0.004f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("LunarDagger", "DisplayLunarDagger", "A_Bust", new Vector3(-0.002f, -0.002f, -0.0018f), new Vector3(45, 90, 270), new Vector3(0.0075f, 0.0075f, 0.0075f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Knurl", "DisplayKnurl", "A_Bust", new Vector3(-0.003f, 0.0015f, 0), new Vector3(90, 0, 0), new Vector3(0.00125f, 0.00125f, 0.00125f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BeetleGland", "DisplayBeetleGland", "A_Bust", new Vector3(0.0025f, 0.002f, -0.001f), new Vector3(0, 270, 0), new Vector3(0.0015f, 0.0015f, 0.0015f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("SprintBonus", "DisplaySoda", "A_Waist", new Vector3(0.002f, 0.001f, 0), new Vector3(270, 90, 0), new Vector3(0.004f, 0.004f, 0.004f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("StickyBomb", "DisplayStickyBomb", "A_Waist", new Vector3(0.0012f, 0.002f, -0.0014f), new Vector3(345, 15, 0), new Vector3(0.002f, 0.002f, 0.002f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("TreasureCache", "DisplayKey", "A_Waist", new Vector3(0, 0.0008f, -0.0012f), new Vector3(90, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BossDamageBonus", "DisplayAPRound", "A_Waist", new Vector3(-0.0012f, 0, -0.001f), new Vector3(90, 45, 0), new Vector3(0.008f, 0.008f, 0.008f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ExtraLife", "DisplayHippo", "A_Bust", new Vector3(-0.002f, 0, -0.001f), new Vector3(0, 220, 0), new Vector3(0.0025f, 0.0025f, 0.0025f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("KillEliteFrenzy", "DisplayBrainstalk", "A_Head", new Vector3(0, 0.002f, 0.0005f), new Vector3(0, 0, 0), new Vector3(0.002f, 0.002f, 0.002f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("RepeatHeal", "DisplayCorpseFlower", "A_Bust", new Vector3(0.0012f, 0.003f, 0), new Vector3(0, 25, 300), new Vector3(0.004f, 0.004f, 0.004f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("AutoCastEquipment", "DisplayFossil", "A_Waist", new Vector3(0.002f, 0.002f, 0.0012f), new Vector3(0, 315, 0), new Vector3(0.005f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("TitanGoldDuringTP", "DisplayGoldHeart", "A_Bust", new Vector3(-0.002f, 0, 0.0012f), new Vector3(0, 235, 0), new Vector3(0.0025f, 0.0025f, 0.0025f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("SprintWisp", "DisplayBrokenMask", "A_ShoulderL", new Vector3(0.0015f, 0, 0), new Vector3(0, 90, 180), new Vector3(0.002f, 0.002f, 0.002f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BarrierOnKill", "DisplayBrooch", "A_Bust", new Vector3(0, 0.002f, 0.0012f), new Vector3(90, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("TPHealingNova", "DisplayGlowFlower", "A_Bust", new Vector3(0.0012f, 0.002f, 0.0018f), new Vector3(340, 30, 0), new Vector3(0.004f, 0.004f, 0.004f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("LunarUtilityReplacement", "DisplayBirdFoot", "A_Head", new Vector3(0, 0.002f, -0.0012f), new Vector3(0, 270, 0), new Vector3(0.005f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Thorns", "DisplayRazorwireLeft", "A_Gun", new Vector3(0, 0.006f, -0.001f), new Vector3(270, 300, 0), new Vector3(0.006f, 0.009f, 0.012f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("LunarPrimaryReplacement", "DisplayBirdEye", "A_Head", new Vector3(0, 0.001f, 0.0012f), new Vector3(270, 0, 0), new Vector3(0.003f, 0.003f, 0.003f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("NovaOnLowHealth", "DisplayJellyGuts", "A_Bust", new Vector3(0.0008156907f, -0.01147854f, -0.01344202f), new Vector3(0, 0, 0), new Vector3(.005f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("LunarTrinket", "DisplayBeads", "A_ArmL", new Vector3(0, 0.0008f, 0), new Vector3(0, 90, 90), new Vector3(0.01f, 0.01f, 0.01f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Plant", "DisplayInterstellarDeskPlant", "A_Bust", new Vector3(0, 0.002f, 0), new Vector3(270, 90, 0), new Vector3(0.02f, 0.0175f, 0.0175f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("DeathMark", "DisplayDeathMark", "A_HandL", new Vector3(0, 0, 0), new Vector3(90, 270, 0), new Vector3(0.0004f, 0.0004f, 0.0004f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("CooldownOnCrit", "DisplaySkull", "A_Bust", new Vector3(0, 0.0012f, 0.0024f), new Vector3(270, 0, 0), new Vector3(0.0025f, 0.0025f, 0.0025f)));
           
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("UtilitySkillMagazine", "DisplayAfterburnerShoulderRing", "A_ShoulderL", new Vector3(-0.0014f, 0, 0), new Vector3(0, 0, 90), new Vector3(0.01f, 0.01f, 0.01f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ExplodeOnDeath", "DisplayWilloWisp", "A_Waist", new Vector3(-0.002f, 0.0012f, 0), new Vector3(0, 0, 0), new Vector3(0.0005f, 0.0005f, 0.0005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Seed", "DisplaySeed", "A_ArmR", new Vector3(0, 0, -0.0004f), new Vector3(270, 0, 0), new Vector3(0.0005f, 0.0005f, 0.0005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Phasing", "DisplayStealthkit", "A_KneeL", new Vector3(0, 0.002f, -0.0012f), new Vector3(90, 0, 0), new Vector3(0.004f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ShockNearby", "DisplayTeslaCoil", "A_Bust", new Vector3(0, 0.0025f, -0.0015f), new Vector3(290, 0, 0), new Vector3(0.005f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("AlienA_Head", "DisplayAlienHead", "A_Gun", new Vector3(0, 0, 0), new Vector3(315, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("A_HeadHunter", "DisplaySkullCrown", "A_Head", new Vector3(0, 0.003f, 0), new Vector3(0, 0, 0), new Vector3(0.005f, 0.002f, 0.002f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("EnergizedOnEquipmentUse", "DisplayWarHorn", "A_Waist", new Vector3(-0.0028f, 0.002f, 0), new Vector3(0, 190, 270), new Vector3(0.005f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Tooth", "DisplayToothMeshLarge", "A_Bust", new Vector3(0, 0.003f, 0), new Vector3(290, 0, 0), new Vector3(0.08f, 0.08f, 0.08f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Pearl", "DisplayPearl", "A_HandL", new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0.002f, 0.002f, 0.002f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("ShinyPearl", "DisplayShinyPearl", "A_HandR", new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0.002f, 0.002f, 0.002f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BonusGoldPackOnKill", "DisplayTome", "A_LegL", new Vector3(-0.0008f, 0.0012f, -0.0024f), new Vector3(20, 200, 0), new Vector3(0.0008f, 0.0008f, 0.0008f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Squid", "DisplaySquidTurret", "Root", new Vector3(0, 0.001f, -0.0012f), new Vector3(270, 0, 0), new Vector3(0.001f, 0.001f, 0.001f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("LaserTurbine", "DisplayLaserTurbine", "A_LegR", new Vector3(0.012f, 0.0032f, 0), new Vector3(0, 90, 0), new Vector3(0.005f, 0.005f, 0.005f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("Incubator", "DisplayAncestralIncubator", "A_Bust", new Vector3(0, 0, 0), new Vector3(330, 0, 0), new Vector3(0.0006f, 0.0006f, 0.0006f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("SiphonOnLowHealth", "DisplaySiphonOnLowHealth", "A_Waist", new Vector3(0.0006f, 0, -0.0006f), new Vector3(0, 315, 0), new Vector3(0.001f, 0.001f, 0.001f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("BleedOnHitAndExplode", "DisplayBleedOnHitAndExplode", "A_LegR", new Vector3(0, 0.0025f, 0.002f), new Vector3(0, 0, 0), new Vector3(0.001f, 0.001f, 0.001f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("MonstersOnShrineUse", "DisplayMonstersOnShrineUse", "A_LegL", new Vector3(0, 0.003f, -0.0024f), new Vector3(90, 90, 0), new Vector3(0.001f, 0.001f, 0.001f)));
            itemRules.Add(ItemDisplays.CreateGenericDisplayRule("RandomDamageZone", "DisplayRandomDamageZone", "A_HandR", new Vector3(-0.001f, 0.0006f, -0.0005f), new Vector3(0, 90, 270), new Vector3(0.0008f, 0.0006f, 0.0008f)));

            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("QuestVolatileBattery", "DisplayBatteryArray", "A_Bust", new Vector3(0, 0, -0.0028f), new Vector3(0, 0, 0), new Vector3(0.003f, 0.003f, 0.003f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("CommandMissile", "DisplayMissileRack", "A_Bust", new Vector3(0, 0.002f, 0), new Vector3(90, 180, 0), new Vector3(0.006f, 0.006f, 0.006f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Fruit", "DisplayFruit", "A_Bust", new Vector3(0, -0.005f, 0.004f), new Vector3(0, 150, 0), new Vector3(0.005f, 0.005f, 0.005f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("AffixWhite", "DisplayEliteIceCrown", "A_Head", new Vector3(0, 0.003f, 0), new Vector3(270, 0, 0), new Vector3(0.0003f, 0.0003f, 0.0003f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("AffixPoison", "DisplayEliteUrchinCrown", "A_Head", new Vector3(0, 0.0025f, 0), new Vector3(270, 0, 0), new Vector3(0.0005f, 0.0005f, 0.0005f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("AffixHaunted", "DisplayEliteStealthCrown", "A_Head", new Vector3(0, 0.002f, 0), new Vector3(270, 0, 0), new Vector3(0.0008f, 0.0008f, 0.0008f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("CritOnUse", "DisplayNeuralImplant", "A_Head", new Vector3(0, 0.0012f, 0.002f), new Vector3(0, 0, 0), new Vector3(0.004f, 0.004f, 0.004f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("DroneBackup", "DisplayRadio", "A_Waist", new Vector3(0.002f, 0.0016f, 0), new Vector3(0, 90, 0), new Vector3(0.01f, 0.01f, 0.01f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Lightning", "DisplayLightning", "A_ShoulderL", new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("BurnNearby", "DisplayPotion", "A_Waist", new Vector3(0.0025f, 0.0012f, 0), new Vector3(0, 0, 330), new Vector3(0.0005f, 0.0005f, 0.0005f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("CrippleWard", "DisplayEffigy", "A_Waist", new Vector3(0.0025f, 0.0012f, 0), new Vector3(0, 270, 0), new Vector3(0.004f, 0.004f, 0.004f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("GainArmor", "DisplayElephantFigure", "A_KneeR", new Vector3(0, 0.003f, 0.0012f), new Vector3(90, 0, 0), new Vector3(0.008f, 0.008f, 0.008f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Recycle", "DisplayRecycler", "A_Bust", new Vector3(0, 0.002f, -0.002f), new Vector3(0, 90, 0), new Vector3(0.001f, 0.001f, 0.001f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("FireBallDash", "DisplayEgg", "A_Waist", new Vector3(0.0025f, 0.0012f, 0), new Vector3(270, 0, 0), new Vector3(0.003f, 0.003f, 0.003f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Cleanse", "DisplayWaterPack", "A_Bust", new Vector3(0, 0, -0.0018f), new Vector3(0, 180, 0), new Vector3(0.0014f, 0.0014f, 0.0014f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Tonic", "DisplayTonic", "A_Waist", new Vector3(0.0025f, 0.0012f, 0), new Vector3(0, 90, 0), new Vector3(0.003f, 0.003f, 0.003f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Gateway", "DisplayVase", "A_Waist", new Vector3(0.0025f, 0.0012f, 0), new Vector3(0, 0, 0), new Vector3(0.003f, 0.003f, 0.003f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("Scanner", "DisplayScanner", "A_Waist", new Vector3(0.0025f, 0.0012f, 0), new Vector3(270, 90, 0), new Vector3(0.0015f, 0.0015f, 0.0015f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("DeathProjectile", "DisplayDeathProjectile", "A_Waist", new Vector3(-0.0024f, 0, -0.001f), new Vector3(0, 240, 0), new Vector3(0.0015f, 0.0015f, 0.0015f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("LifestealOnHit", "DisplayLifestealOnHit", "A_Head", new Vector3(-0.0015f, 0.004f, 0), new Vector3(45, 90, 0), new Vector3(0.001f, 0.001f, 0.001f)));
            equipmentRules.Add(ItemDisplays.CreateGenericDisplayRule("TeamWarCry", "DisplayTeamWarCry", "A_Waist", new Vector3(0, 0, 0.003f), new Vector3(0, 0, 0), new Vector3(0.0015f, 0.0015f, 0.0015f)));

            itemRules.Add(ItemDisplays.CreateFollowerDisplayRule("Icicle", "DisplayFrostRelic", new Vector3(0.013f, 0.01f, -0.006f), new Vector3(90, 0, 0), new Vector3(2, 2, 2)));
            itemRules.Add(ItemDisplays.CreateFollowerDisplayRule("Talisman", "DisplayTalisman", new Vector3(-0.013f, 0.01f, -0.006f), new Vector3(0, 0, 0), new Vector3(1, 1, 1)));
            itemRules.Add(ItemDisplays.CreateFollowerDisplayRule("FocusConvergence", "DisplayFocusedConvergence", new Vector3(-0.01f, 0.005f, -0.01f), new Vector3(0, 0, 0), new Vector3(0.2f, 0.2f, 0.2f)));

            equipmentRules.Add(ItemDisplays.CreateFollowerDisplayRule("Saw", "DisplaySawmerang", new Vector3(0, 0.01f, -0.015f), new Vector3(90, 0, 0), new Vector3(0.25f, 0.25f, 0.25f)));
            equipmentRules.Add(ItemDisplays.CreateFollowerDisplayRule("Meteor", "DisplayMeteor", new Vector3(0, 0.01f, -0.015f), new Vector3(90, 0, 0), new Vector3(1, 1, 1)));
            equipmentRules.Add(ItemDisplays.CreateFollowerDisplayRule("Blackhole", "DisplayGravCube", new Vector3(0, 0.01f, -0.015f), new Vector3(90, 0, 0), new Vector3(1, 1, 1)));
            itemRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "IncreaseHealing",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                       {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAntler"),
                            childName = "A_Head",
                            localPos = new Vector3(0, 0.002f, 0.0005f),
                            localAngles = new Vector3(0, 90, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAntler"),
                            childName = "A_Head",
                            localPos = new Vector3(0, 0.002f, 0.0005f),
                            localAngles = new Vector3(0, 90, 0),
                            localScale = new Vector3(0.005f, 0.005f, -0.005f),
                            limbMask = LimbFlags.None
                        }
                       }
                }
            });

            equipmentRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "AffixRed",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteHorn"),
                            childName = "A_Head",
                            localPos = new Vector3(0, 0.002f, 0),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteHorn"),
                            childName = "A_Head",
                            localPos = new Vector3(0, 0.002f, 0),
                            localAngles = new Vector3(0, 180, 0),
                            localScale = new Vector3(0.001f, 0.001f, -0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            equipmentRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "AffixBlue",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteRhinoHorn"),
                            childName = "A_Head",
                            localPos = new Vector3(0, 0.002f, 0.002f),
                            localAngles = new Vector3(315, 0, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteRhinoHorn"),
                            childName = "A_Head",
                            localPos = new Vector3(0, 0.002f, 0),
                            localAngles = new Vector3(290, 0, 0),
                            localScale = new Vector3(0.0025f, 0.0025f, 0.0025f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            itemRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "ShieldOnly",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayShieldBug"),
                            childName = "A_Head",
                            localPos = new Vector3(0, 0.002f, 0.001f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.004f, 0.004f, 0.004f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayShieldBug"),
                            childName = "A_Head",
                            localPos = new Vector3(0, 0.002f, 0.001f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(-0.004f, 0.004f, 0.004f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });


            itemRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "FallBoots",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGravBoots"),
                            childName = "A_FootR",
                            localPos = new Vector3(0, 0, -0.0006f),
                            localAngles = new Vector3(90, 0, 0),
                            localScale = new Vector3(0.0025f, 0.0025f, 0.0025f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGravBoots"),
                            childName = "A_FootL",
                            localPos = new Vector3(0, 0, -0.0006f),
                            localAngles = new Vector3(90, 0, 0),
                            localScale = new Vector3(0.0025f, 0.0025f, 0.0025f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            itemRules.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "NovaOnHeal",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                       {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDevilHorns"),
                            childName = "A_Head",
                            localPos = new Vector3(0, 0, 0),
                            localAngles = new Vector3(0, 0, 20),
                            localScale = new Vector3(0.01f, 0.01f, 0.01f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDevilHorns"),
                            childName = "A_Head",
                            localPos = new Vector3(0, 0, 0),
                            localAngles = new Vector3(0, 0, 340),
                            localScale = new Vector3(-0.01f, 0.01f, 0.01f),
                            limbMask = LimbFlags.None
                        }
                       }
                }
            });

            BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            ItemDisplayRuleSet.NamedRuleGroup[] item = itemRules.ToArray();
            ItemDisplayRuleSet.NamedRuleGroup[] equip = equipmentRules.ToArray();
            typeof(ItemDisplayRuleSet).GetField("namedItemRuleGroups", bindingAttr).SetValue(itemDisplayRuleSet, item);
            typeof(ItemDisplayRuleSet).GetField("namedEquipmentRuleGroups", bindingAttr).SetValue(itemDisplayRuleSet, equip);

            characterModel.itemDisplayRuleSet = itemDisplayRuleSet;
        }

        public static ItemDisplayRuleSet.NamedRuleGroup CreateGenericDisplayRule(string itemName, string prefabName,string childName, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            ItemDisplayRuleSet.NamedRuleGroup displayRule = new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = itemName,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
        {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            childName = childName,
                            followerPrefab = ItemDisplays.LoadDisplay(prefabName),
                            limbMask = LimbFlags.None,
                            localPos = position,
                            localAngles = rotation,
                            localScale = scale
                        }
        }
                }
            };

            return displayRule;
        }

        public static ItemDisplayRuleSet.NamedRuleGroup CreateGenericDisplayRule(string itemName, GameObject itemPrefab, string childName, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            ItemDisplayRuleSet.NamedRuleGroup displayRule = new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = itemName,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            childName = childName,
                            followerPrefab = itemPrefab,
                            limbMask = LimbFlags.None,
                            localPos = position,
                            localAngles = rotation,
                            localScale = scale
                        }
                    }
                }
            };

            return displayRule;
        }

        public static ItemDisplayRuleSet.NamedRuleGroup CreateFollowerDisplayRule(string itemName, string prefabName, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            ItemDisplayRuleSet.NamedRuleGroup displayRule = new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = itemName,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            childName = "Base",
                            followerPrefab = ItemDisplays.LoadDisplay(prefabName),
                            limbMask = LimbFlags.None,
                            localPos = position,
                            localAngles = rotation,
                            localScale = scale
                        }
                    }
                }
            };

            return displayRule;
        }

        public static ItemDisplayRuleSet.NamedRuleGroup CreateFollowerDisplayRule(string itemName, GameObject itemPrefab, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            ItemDisplayRuleSet.NamedRuleGroup displayRule = new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = itemName,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            childName = "Base",
                            followerPrefab = itemPrefab,
                            limbMask = LimbFlags.None,
                            localPos = position,
                            localAngles = rotation,
                            localScale = scale
                        }
                    }
                }
            };

            return displayRule;
        }

        public static GameObject LoadDisplay(string name)
        {
            if (itemDisplayPrefabs.ContainsKey(name.ToLower()))
            {
                if (itemDisplayPrefabs[name.ToLower()]) return itemDisplayPrefabs[name.ToLower()];
            }
            return null;
        }

        private static void PopulateDisplays()
        {
            ItemDisplayRuleSet itemDisplayRuleSet = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").GetComponent<ModelLocator>().modelTransform.GetComponent<CharacterModel>().itemDisplayRuleSet;

            BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            ItemDisplayRuleSet.NamedRuleGroup[] array = typeof(ItemDisplayRuleSet).GetField("namedItemRuleGroups", bindingAttr).GetValue(itemDisplayRuleSet) as ItemDisplayRuleSet.NamedRuleGroup[];
            ItemDisplayRuleSet.NamedRuleGroup[] array2 = typeof(ItemDisplayRuleSet).GetField("namedEquipmentRuleGroups", bindingAttr).GetValue(itemDisplayRuleSet) as ItemDisplayRuleSet.NamedRuleGroup[];
            ItemDisplayRuleSet.NamedRuleGroup[] array3 = array;

            for (int i = 0; i < array3.Length; i++)
            {
                ItemDisplayRule[] rules = array3[i].displayRuleGroup.rules;
                for(int j = 0; j < rules.Length; j++)
                {
                    GameObject followerPrefab = rules[j].followerPrefab;
                    if (!(followerPrefab == null))
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
            array3 = array2;
            for(int i = 0; i < array3.Length; i++)
            {
                ItemDisplayRule[] rules = array3[i].displayRuleGroup.rules;
                for(int j = 0; j < rules.Length; j++)
                {
                    GameObject followerPrefab2 = rules[j].followerPrefab;
                    if (!(followerPrefab2 == null))
                    {
                        string name2 = followerPrefab2.name;
                        string key2 = (name2 != null) ? name2.ToLower() : null;
                        if (!itemDisplayPrefabs.ContainsKey(key2))
                        {
                            itemDisplayPrefabs[key2] = followerPrefab2;
                        }
                    }
                }
            }
        }
    }
}
