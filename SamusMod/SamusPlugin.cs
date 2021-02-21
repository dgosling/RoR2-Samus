using System;
using System.Collections.Generic;
using BepInEx;
using R2API;
using R2API.Utils;
using EntityStates;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace SamusMod
{
    [BepInDependency("com.bepis.r2api",BepInDependency.DependencyFlags.HardDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID,"Samus","0.1.0")]
    [R2APISubmoduleDependency(new string[]
    {
        "PrefabAPI",
        "SurvivorAPI",
        "LoadoutAPI",
        "BuffAPI",
        "LanguageAPI",
        "SoundAPI",
        "EffectAPI",
        "UnlockablesAPI",
        "ResourcesAPI"

    })]
    public class SamusPlugin : BaseUnityPlugin
    {
        public const string MODUID = "com.dgosling.Samus";

        public static SamusPlugin instance;

        public GameObject characterPrefab;
        public GameObject doppelganger;

        public static Material commandoMat;

        public static event Action awake;
        public static event Action start;

        public static readonly Color characterColor = new Color(0f, 0f, 0f);

        public SamusPlugin()
        {
            awake += SamusPlugin_Load;
            start += SamusPlugin_LoadStart;
        }

        private void SamusPlugin_Load()
        {
            instance = this;

            Modules.Assets.PopulateAssets();

            Modules.Prefabs.CreatePrefabs();
            characterPrefab = Modules.Prefabs.samusPrefab;
            Modules.States.RegisterStates();
            Modules.Skills.SetupSkills(Modules.Prefabs.samusPrefab);
            Modules.Survivors.RegisterSurvivors();
            Modules.Skins.RegisterSkins();
            Modules.Projectiles.RegisterProjectiles();
            Modules.Tokens.AddTokens();

            CreateDoppelganger();

            Hook();
        }

        private void SamusPlugin_LoadStart()
        {
            Modules.Projectiles.LateSetup();
        }

        public void Awake()
        {
            Action awake = SamusPlugin.awake;
            if (awake == null) 
            { 
                return; 
            }
            awake();

        }

        public void Start()
        {
            Action start = SamusPlugin.start;
            if (start == null)
            {
                return;
            }
            start();
        }

        private void CreateDoppelganger()
        {
            doppelganger = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/CharacterMasters/CommandoMonsterMaster"), "SamusMonsterMaster");
            doppelganger.GetComponent<CharacterMaster>().bodyPrefab = Modules.Prefabs.samusPrefab;

            MasterCatalog.getAdditionalEntries += delegate (List<GameObject> list)
            {
                list.Add(doppelganger);
            };
        }

        private void Hook()
        {
            //On.RoR2.DotController.AddDot += (orig, vo, ao, duration, index, multiplier) =>
            //{

            //   // if (ao.GetComponent<CharacterBody>().name == "dgoslingSamusBody")
            //    //{
            //        index = DotController.DotIndex.PercentBurn;
            //        duration = 0;
            //        multiplier = 0;
            //        Debug.Log("PercentBurntest");

            //    //}

            //    orig(vo, ao, duration, index, multiplier);
            //};
           // On.RoR2.DotController.AddDot += DotController_AddDot;
            //On.RoR2.CharacterBody.AddBuff += CharacterBody_AddBuff;
            //On.RoR2.CharacterBody.RecalculateStats += recalculateSuperMissiles;
            On.RoR2.CharacterBody.FixedUpdate += CharacterBody_FixedUpdate;
            //On.RoR2.DotController.InflictDot += DotController_InflictDot;
            
        }

        private void DotController_InflictDot(On.RoR2.DotController.orig_InflictDot orig, GameObject victimObject, GameObject attackerObject, DotController.DotIndex dotIndex, float duration, float damageMultiplier)
        {
            if ((victimObject.gameObject.GetComponent<CharacterBody>().baseNameToken == "SAMUS_NAME"||attackerObject.gameObject.GetComponent<CharacterBody>().baseNameToken=="SAMUS_NAME") && dotIndex == DotController.DotIndex.PercentBurn)
            {
                duration = 0;
                damageMultiplier = 0;
                //Debug.Log("testing inflict");
            }
            orig(victimObject, attackerObject, dotIndex, duration, damageMultiplier);
        }

        private void CharacterBody_AddBuff(On.RoR2.CharacterBody.orig_AddBuff orig, CharacterBody self, BuffIndex buffType)
        {
           // if (self.gameObject.name == "mdlSamus" && buffType == BuffIndex.OnFire)
           // {
                buffType = BuffIndex.None;
                //Debug.Log("test onfire buff");
            //}
            orig(self, buffType);
        }

        private void DotController_AddDot(On.RoR2.DotController.orig_AddDot orig, DotController self, GameObject attackerObject, float duration, DotController.DotIndex dotIndex, float damageMultiplier)
        {
           // if (attackerObject.GetComponent<CharacterBody>().name== "dgoslingSamusBody")
           //{
                dotIndex = DotController.DotIndex.Burn;
                duration = 0;
                damageMultiplier = 0;
                //Debug.Log("BurnTest");
            //}

            orig(self, attackerObject, duration, dotIndex, damageMultiplier);
        }
        

        private void CharacterBody_FixedUpdate(On.RoR2.CharacterBody.orig_FixedUpdate orig, CharacterBody self)
        {
            
            if (self&&self.skillLocator.secondary&&self.skillLocator.special&&self.baseNameToken=="SAMUS_NAME")
            {
                if (self.skillLocator.secondary.stock >= 5)
                    self.skillLocator.special.stock = 1;
                else
                    self.skillLocator.special.RemoveAllStocks();

                
                //Debug.Log("test");
            }

            orig(self);


            //if (self.skillLocator.secondary.stock >=5)
            //    {
            //        self.skillLocator.special.maxStock = 1;
            //    }
            //    if (self.skillLocator.secondary.stock < 5)
            //    {
            //        self.skillLocator.special.maxStock = 0;
            //    }
            //    else
            //        self.skillLocator.special.maxStock = 0;

            //    self.skillLocator.special.RecalculateMaxStock();





        }




    }
}
