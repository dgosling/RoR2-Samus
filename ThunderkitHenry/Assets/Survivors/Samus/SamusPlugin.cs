﻿using BepInEx;
using BepInEx.Logging;
using R2API.Utils;
using UnityEngine;
using RoR2;
using System.Security;
using System.Security.Permissions;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

//Do a 'Find and Replace' on the ThunderHenry namespace. Make your own namespace, please.
namespace SamusMod
{
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.valex.ShaderConverter", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.DrBibop.VRAPI", BepInDependency.DependencyFlags.HardDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]
    [R2APISubmoduleDependency(new string[]
    {
        "PrefabAPI",
        "LanguageAPI",
        "SoundAPI",
        "UnlockableAPI"
    })]
    public class SamusPlugin : BaseUnityPlugin
    {
        // if you don't change these you're giving permission to deprecate the mod-
        //  please change the names to your own stuff, thanks
        //   this shouldn't even have to be said
        public const string MODUID = "com.dgosling.Samus";
        public const string MODNAME = "Samus";
        public const string MODVERSION = "2.0.0";

        // a prefix for name tokens to prevent conflicts- please capitalize all name tokens for convention
        public const string developerPrefix = "DG";
        public static ManualLogSource logger;
        // use this to toggle debug on stuff, make sure it's false before releasing
        public static bool debug = false;

        public static bool cancel;
        public static float jumps;
        public static SamusPlugin instance;

        private void Awake()
        {
            instance = this;

            // Load/Configure assets and read Config
            Modules.Config.ReadConfig();
            Modules.Assets.Init();
            if (cancel) return;
            Modules.Shaders.init();
            Modules.Tokens.Init();
            Modules.Prefabs.Init();
            Modules.Buffs.Init();
            Modules.ItemDisplays.Init();
            Modules.Unlockables.Init();
            
            
            // Any debug stuff you need to do can go here before initialisation
            if (debug) { Modules.Helpers.AwakeDebug(); }

            //Initialize Content Pack
            Modules.ContentPackProvider.Initialize();

            Hook();
        }

        private void Start()
        {
            // If Awake isn't the right place for launch debug, you can put some in Start here.
            // Most of the time Awake will do fine though.
            if (debug) { Modules.Helpers.StartDebug(); }
        }

        private void Hook()
        {
            On.RoR2.CharacterBody.FixedUpdate += CharacterBody_FixedUpdate;
            On.RoR2.GenericSkill.SetBonusStockFromBody += GenericSkill_SetBonusStockFromBody;
            On.RoR2.DamageTrail.DoDamage += DamageTrail_DoDamage;
            On.RoR2.DotController.InflictDot_GameObject_GameObject_DotIndex_float_float += DotController_InflictDot_GameObject_GameObject_DotIndex_float_float;
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
            On.RoR2.CharacterMotor.FixedUpdate += CharacterMotor_FixedUpdate;
            On.RoR2.CharacterMotor.OnLanded += CharacterMotor_OnLanded;
            On.RoR2.CharacterMaster.OnBodyDamaged += CharacterMaster_OnBodyDamaged;
        }

        private void CharacterMaster_OnBodyDamaged(On.RoR2.CharacterMaster.orig_OnBodyDamaged orig, CharacterMaster self, DamageReport damageReport)
        {
            if (self)
            {
                if (self.GetBody().baseNameToken == "DG_SAMUS_NAME")
                {

                    //Debug.Log("worked");
                    Util.PlaySound(SamusMod.Modules.Sounds.hurtSound, self.bodyInstanceObject);
                }

                orig(self, damageReport);
            }
        }

        private void CharacterMotor_OnLanded(On.RoR2.CharacterMotor.orig_OnLanded orig, CharacterMotor self)
        {
            if (self)
            {
                if (self.body.baseNameToken == "DG_SAMUS_NAME" && jumps > 0)
                {
                    jumps = 0;
                }
                orig(self);
            }
        }

        private void CharacterMotor_FixedUpdate(On.RoR2.CharacterMotor.orig_FixedUpdate orig, CharacterMotor self)
        {
            if (self)
            {
                if (self.body.baseNameToken == "DG_SAMUS_NAME")
                {

                    if (self.jumpCount == 1 && jumps == 0)
                    {
                        //Debug.Log("jumped");
                        jumps = 1;
                        Util.PlaySound(SamusMod.Modules.Sounds.JumpSound, self.gameObject);
                    }
                    else if (self.jumpCount >= 2 && jumps + 1 == self.jumpCount)
                    {
                        //Debug.Log("djump");
                        jumps++;
                        Util.PlaySound(SamusMod.Modules.Sounds.doubleJumpSound, self.gameObject);
                    }

                }
                orig(self);

            }
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            if (self != null && damageInfo.inflictor != null && self.body != null)
            {
                if (self.body.baseNameToken == "DG_SAMUS_NAME" && (damageInfo.inflictor.name == "bombExplosion(Clone)" || damageInfo.inflictor.name == "SamusMorphBomb(Clone)"))
                {
                    damageInfo.damage = 0;
                    if (self.body.characterMotor.isGrounded == true || self.body.characterMotor.velocity.y > -10)
                    {
                        damageInfo.force = new Vector3(0, 1200, 0);
                    }
                    else
                        damageInfo.force = new Vector3(0, 3000, 0);

                }

            }
            orig(self, damageInfo);
        }

        private void DotController_InflictDot_GameObject_GameObject_DotIndex_float_float(On.RoR2.DotController.orig_InflictDot_GameObject_GameObject_DotIndex_float_float orig, UnityEngine.GameObject victimObject, UnityEngine.GameObject attackerObject, DotController.DotIndex dotIndex, float duration, float damageMultiplier)
        {
            if ((victimObject.gameObject.GetComponent<CharacterBody>().baseNameToken == "DG_SAMUS_NAME" || attackerObject.gameObject.GetComponent<CharacterBody>().baseNameToken == "DG_SAMUS_NAME") && dotIndex == DotController.DotIndex.PercentBurn)
            {
                duration = 0;
                damageMultiplier = 0;
                //Debug.Log("testing inflict");
            }
            orig(victimObject, attackerObject, dotIndex, duration, damageMultiplier);
        }

        private void DamageTrail_DoDamage(On.RoR2.DamageTrail.orig_DoDamage orig, DamageTrail self)
        {
            if (self)
            {
                if(self.segmentPrefab.name == "FireTrailSegment")
                {
                    self.damagePerSecond = 0;
                }

            }
            orig(self);
        }

        private void GenericSkill_SetBonusStockFromBody(On.RoR2.GenericSkill.orig_SetBonusStockFromBody orig, GenericSkill self, int newBonusStockFromBody)
        {
            if (self)
            {
                if (self.characterBody.skillLocator.secondary == self && self.characterBody.baseNameToken == "DG_SAMUS_NAME")
                {
                    //if (SamusMod.States.SamusMain.morphBall == true)
                    //{
                    //    float test = self.characterBody.skillLocator.primary.stock%3;
                    //    if (test == 0)
                    //    {
                    //        newBonusStockFromBody = (self.characterBody.skillLocator.primary.maxStock / 3);
                    //    }
                    //    else
                    //    {
                    //        newBonusStockFromBody = Mathf.FloorToInt(self.characterBody.skillLocator.primary.maxStock / 3);
                    //    }
                    //    orig(self, newBonusStockFromBody);
                    //}
                    //else
                    //{
                    if (SamusMod.SkillStates.BaseStates.BaseSamus.morphBall != true)
                    {
                        newBonusStockFromBody *= 5;
                        orig(self, newBonusStockFromBody);
                    }

                }
                else
                    orig(self, newBonusStockFromBody);
            }
        }

        private void CharacterBody_FixedUpdate(On.RoR2.CharacterBody.orig_FixedUpdate orig, CharacterBody self)
        {

            if (self && self.skillLocator.secondary && self.skillLocator.special && self.baseNameToken == "DG_SAMUS_NAME")
            {
                if (SamusMod.SkillStates.BaseStates.BaseSamus.morphBall == true)
                {
                    if (self.skillLocator.primary.maxStock == 3)
                    {
                        self.skillLocator.secondary.maxStock = 1;
                    }
                }

                if (self.skillLocator.secondary.stock >= 5)
                    self.skillLocator.special.stock = 1;
                else
                    self.skillLocator.special.RemoveAllStocks();


                //Debug.Log("test");
            }



            orig(self);
        }
    }
}