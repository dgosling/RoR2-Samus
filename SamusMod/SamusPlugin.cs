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
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]

    [BepInPlugin(MODUID,"Samus","1.1.0")]
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
        "ResourcesAPI",
        "ProjectileAPI"

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
        public static float jumps;

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
            Modules.ItemDisplays.InitializeItemDisplays();
            Modules.Tokens.AddTokens();

            CreateDoppelganger();
            //new Modules.ContentPacks().CreateContentPack();
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
            //On.RoR2.Networking.GameNetworkManager.OnClientConnect += (self, user, t) => { };
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

            Modules.Prefabs.masterPrefabs.Add(doppelganger);
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
            //On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
            On.RoR2.GenericSkill.SetBonusStockFromBody += GenericSkill_SetBonusStockFromBody;
            //On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
            On.RoR2.DamageTrail.DoDamage += DamageTrail_DoDamage;
            On.RoR2.DotController.InflictDot_GameObject_GameObject_DotIndex_float_float += DotController_InflictDot_GameObject_GameObject_DotIndex_float_float;

            On.RoR2.CharacterMotor.FixedUpdate += CharacterMotor_FixedUpdate;
            On.RoR2.CharacterMotor.OnLanded += CharacterMotor_OnLanded;
            On.RoR2.CharacterMaster.OnBodyDamaged += CharacterMaster_OnBodyDamaged;
           // On.EntityStates.FrozenState.OnExit += FrozenState_OnExit;
        }

        private void DotController_InflictDot_GameObject_GameObject_DotIndex_float_float(On.RoR2.DotController.orig_InflictDot_GameObject_GameObject_DotIndex_float_float orig, GameObject victimObject, GameObject attackerObject, DotController.DotIndex dotIndex, float duration, float damageMultiplier)
        {
            if ((victimObject.gameObject.GetComponent<CharacterBody>().baseNameToken == "DG_SAMUS_NAME" || attackerObject.gameObject.GetComponent<CharacterBody>().baseNameToken == "DG_SAMUS_NAME") && dotIndex == DotController.DotIndex.PercentBurn)
            {
                duration = 0;
                damageMultiplier = 0;
                //Debug.Log("testing inflict");
            }
            orig(victimObject, attackerObject, dotIndex, duration, damageMultiplier);
        }



        //private void FrozenState_OnExit(On.EntityStates.FrozenState.orig_OnExit orig, FrozenState self)
        //{
        //    if (self.GetModelChildLocator().FindChild("Ball").gameObject.activeSelf == true && self.characterBody.baseNameToken == "DG_SAMUS_NAME")
        //    {
        //        Debug.Log("hookd");
        //        self.GetModelChildLocator().FindChild("Ball").gameObject.SetActive(false);
        //        Debug.Log(self.GetModelChildLocator().FindChild("Body").gameObject.GetComponent<Material>().);
        //        if (self.GetModelChildLocator().FindChild("Body").gameObject.activeSelf == false)
        //        {
        //            Debug.Log("body not active");

        //            self.GetModelChildLocator().FindChild("Body").gameObject.SetActive(true);
        //        }

        //    }
        //    orig(self);
        //}

        private void FrozenState_FixedUpdate(On.EntityStates.FrozenState.orig_FixedUpdate orig, FrozenState self)
        {
            if (self != null)
            {
                //string name="";
                //AnimatorClipInfo[] animatorClipInfo = self.GetModelAnimator().GetCurrentAnimatorClipInfo(0);
                //for (int i = 0; i < animatorClipInfo.Length; i++)
                //{
                //    if (animatorClipInfo[i].clip.name == "Roll")
                //    {
                //        name = animatorClipInfo[i].clip.name;
                //        break;

                //    }
                //}
                //if (self.modelAnimator.gameObject.GetComponent<CharacterBody>().baseNameToken == "DG_SAMUS_NAME"&&name!="Roll")
                {
                    var child = self.GetModelChildLocator();
                    
                    if (child.FindChild("Ball").gameObject.activeSelf == true)
                    {
                        child.FindChild("Ball").gameObject.SetActive(false);
                    }
                }
                
            }
            orig(self);
        }

        private void FrozenState_OnEnter(On.EntityStates.FrozenState.orig_OnEnter orig, FrozenState self)
        {
            if (self!=null)
            {
                if (self.modelAnimator.gameObject.GetComponent<CharacterBody>().baseNameToken == "DG_SAMUS_NAME")
                {

                }
            }
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
                if (self.body.baseNameToken == "DG_SAMUS_NAME" && jumps>0)
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
                    else if (self.jumpCount >= 2 && jumps+1==self.jumpCount)
                    {
                        //Debug.Log("djump");
                        jumps++;
                        Util.PlaySound(SamusMod.Modules.Sounds.doubleJumpSound, self.gameObject);
                    }

                }
                orig(self);
                
            }
        }

        private void DamageTrail_DoDamage(On.RoR2.DamageTrail.orig_DoDamage orig, DamageTrail self)
        {
            if (self)
            {
                //CharacterBody characterBody;
                //Array array = CharacterBody.instancesList.ToArray();
                //for (int i = 0; i < array.Length; i++)
                //{
                //    string name = CharacterBody.instancesList[i].baseNameToken;

                //    if (name == "DG_SAMUS_NAME")
                //    {
                //        characterBody = CharacterBody.instancesList[i];
                //        return;
                //    }
                //    else
                //        return;
                //}
                //if (self.gameObject.name=="FireTrail"  /* CharacterBody.instancesList.Contains(characterBody)*/)
               // {
               if(self.segmentPrefab.name== "FireTrailSegment")
                {
                    //Debug.Log("test damagetrail");
                    self.damagePerSecond = 0;
                    return;
                }
                //Debug.Log(self.damagePerSecond);     
               // }
                orig(self);
            }
        }

        //private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        //{
        //    if (self!=null)
        //    {
        //        float origDam = damageInfo.damage;
        //        DamageTrail reference = gameObject.GetComponent<DamageTrail>();
        //        Debug.Log(damageInfo.damage);
        //        if (damageInfo.attacker==reference/*&&gameObject.GetComponent<DamageTrail>().name=="FireTrail"*/ && self.body.baseNameToken == "DG_SAMUS_NAME")
        //        {
        //            Debug.Log("test firetrail");
        //            damageInfo.damage = 0;
        //            orig(self, damageInfo);
        //        }
        //        else
        //        {
        //            damageInfo.damage = origDam;
        //            Debug.Log(damageInfo.damage);
        //        }
        //        orig(self, damageInfo);
                
        //    }
            
        //}

        private void GenericSkill_SetBonusStockFromBody(On.RoR2.GenericSkill.orig_SetBonusStockFromBody orig, GenericSkill self, int newBonusStockFromBody)
        {
            if (self)
            {
                if (self.characterBody.skillLocator.secondary == self && self.characterBody.baseNameToken == "DG_SAMUS_NAME")
                {
                    newBonusStockFromBody *= 5;
                    orig(self, newBonusStockFromBody);
                }
                else
                    orig(self, newBonusStockFromBody);
            }
        }

        //private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        //{
        //    if (self)
        //    {

        //        if (self.baseNameToken == "DG_SAMUS_NAME")
        //        {
        //            int missiles = self.skillLocator.secondary.maxStock;
        //            int magazines = self.inventory.GetItemCount(ItemIndex.SecondarySkillMagazine);
        //            Debug.Log(missiles);
        //            Debug.Log(magazines);
        //            if (missiles < magazines + 1 * 5)
        //            {
        //                missiles += 4;
        //                self.skillLocator.secondary.SetBonusStockFromBody(missiles);

        //                Debug.Log("added 4 missiles");
        //                Debug.Log(self.skillLocator.secondary.maxStock);
        //            }
                        



        //        }
        //        orig(self);
                
        //    }
        //}

        //private void SkillLocator_ApplyAmmoPack(On.RoR2.SkillLocator.orig_ApplyAmmoPack orig, SkillLocator self)
        //{
        //    int missiles = self.secondary.maxStock;
        //    Debug.Log("hooked");
            
        //    if (self.gameObject.GetComponent<CharacterBody>().baseNameToken == "DG_SAMUS_BODY")
        //    {
        //        for (int i=0;missiles < missiles + 5; i++){
        //            self.secondary.AddOneStock();
        //            Debug.Log("added missile");
        //        }
        //    }
        //    orig(self);
        //}



        //private void CharacterBody_AddBuff(On.RoR2.CharacterBody.orig_AddBuff orig, CharacterBody self, BuffIndex buffType)
        //{
        //   // if (self.gameObject.name == "mdlSamus" && buffType == BuffIndex.OnFire)
        //   // {
        //        buffType = BuffIndex.None;
        //        //Debug.Log("test onfire buff");
        //    //}
        //    orig(self, buffType);
        //}

        //private void DotController_AddDot(On.RoR2.DotController.orig_AddDot orig, DotController self, GameObject attackerObject, float duration, DotController.DotIndex dotIndex, float damageMultiplier)
        //{
        //   // if (attackerObject.GetComponent<CharacterBody>().name== "dgoslingSamusBody")
        //   //{
        //        dotIndex = DotController.DotIndex.Burn;
        //        duration = 0;
        //        damageMultiplier = 0;
        //        //Debug.Log("BurnTest");
        //    //}

        //    orig(self, attackerObject, duration, dotIndex, damageMultiplier);
        //}
        

        private void CharacterBody_FixedUpdate(On.RoR2.CharacterBody.orig_FixedUpdate orig, CharacterBody self)
        {
            
            if (self&&self.skillLocator.secondary&&self.skillLocator.special&&self.baseNameToken=="DG_SAMUS_NAME")
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
