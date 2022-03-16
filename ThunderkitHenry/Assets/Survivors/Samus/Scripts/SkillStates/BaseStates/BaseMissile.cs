using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using RoR2;
using RoR2.Projectile;
using EntityStates;

namespace SamusMod.SkillStates.BaseStates
{

    public abstract class BaseMissile : BaseSkillState
    {
       
        public float damageCoef;
        
        public float baseDuration;
   
        public float recoil;
        [SerializeField]
        public GameObject projectilePrefab;
        [SerializeField]
        public GameObject muzzleEffectPrefab;
        [SerializeField]
        public GameObject smissleObject;
        public GameObject sMissileExtraMissiles;
        protected static int secStock;
        public bool sMissile;
        public string Sound;
        private float duration;
        private float fireDuration;
        private bool hasFired;
        private Animator animator;
        private string muzzleString;
        private Transform muzzleTransform;
        public HurtBox target;
        private Ray gunRay;
        public override void OnEnter()
        {
            base.OnEnter();
            sMissileExtraMissiles = smissleObject != null ? smissleObject : null;
            this.duration = this.baseDuration / this.attackSpeedStat;
            this.fireDuration = .5f * this.duration;
            base.characterBody.SetAimTimer(2f);
            this.animator = base.GetModelAnimator();
            this.muzzleString = "gunCon";
            //this.PlayAnimation("Gesture, Override", "Missile", "Missile.playbackRate", this.duration);
            this.PlayAnimation("Gesture, Override", "Missile", "Missile.playbackRate", this.duration);
            secStock = skillLocator.secondary.stock;
            //muzzleEffectPrefab = SamusMod.Modules.Assets.missileEffect;
            muzzleTransform = GetModelChildLocator().FindChild(muzzleString);
        }

        public override void OnExit()
        {
            //  this.calculateSMissiles();
            // base.skillLocator.special.RecalculateMaxStock();
            if (VRAPI.Utils.IsUsingMotionControls(characterBody))
            {
                Misc.SamusHUD samusHUD = Modules.VRStuff.SamusHUD;
                samusHUD.setMissileActive(true);
            }

            base.OnExit();
        }

        private void fireMissile()
        {
            if (!hasFired)
            {
                this.hasFired = true;
                if (VRAPI.Utils.IsUsingMotionControls(characterBody))
                    Util.PlaySound(this.Sound, VRAPI.MotionControls.dominantHand.muzzle.gameObject);
                else
                    Util.PlaySound(this.Sound, this.gameObject);
                base.characterBody.AddSpreadBloom(.75f);
                //Debug.Log(target);
                Ray aimRay = base.GetAimRay();
                gunRay = new Ray(muzzleTransform.position,aimRay.direction);
                if (VRAPI.Utils.IsUsingMotionControls(characterBody))
                    aimRay = VRAPI.MotionControls.dominantHand.aimRay;
                if (muzzleEffectPrefab != null)
                    EffectManager.SimpleMuzzleFlash(muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
                if (base.isAuthority && this.target != null)
                {
                    //FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
                    //{
                    //    projectilePrefab = projectilePrefab,
                    //    position = aimRay.origin,
                    //    rotation = Util.QuaternionSafeLookRotation(aimRay.direction),
                    //    damage = this.damageCoef * this.damageStat,
                    //    owner = base.gameObject,
                    //    force = 5f,
                    //    crit = base.RollCrit(),
                    //    speedOverride = Modules.StaticValues.missileSpeed,
                    //    target = this.target.gameObject
                    //};
                    MissileUtils.FireMissile(gunRay.origin, characterBody, new ProcChainMask(), target.gameObject, damageCoef * damageStat, RollCrit(), projectilePrefab, DamageColorIndex.Default,gunRay.direction + UnityEngine.Random.insideUnitSphere * 0.1f, 100f,true);
                }

                else if (base.isAuthority && this.target == null&&!sMissile)
                {


                    //sound placeholder
                    //FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
                    //{
                    //    projectilePrefab = projectilePrefab,
                    //    position = aimRay.origin,
                    //    rotation = Util.QuaternionSafeLookRotation(aimRay.direction),
                    //    damage = this.damageCoef * this.damageStat,
                    //    owner = base.gameObject,
                    //    force = 5f,
                    //    crit = base.RollCrit(),
                    //    speedOverride = Modules.StaticValues.missileSpeed
                    //};
                    MissileUtils.FireMissile(gunRay.origin, characterBody, new ProcChainMask(), (GameObject)null, damageCoef * damageStat, RollCrit(), projectilePrefab, DamageColorIndex.Default,gunRay.direction + UnityEngine.Random.insideUnitSphere * 0.25f, 100f, true);
                }
                else if (base.isAuthority && this.target == null && sMissile)
                {
                    SuperMissileICBMLaunch(gunRay.origin, characterBody, new ProcChainMask(), (GameObject)null, damageCoef * damageStat, RollCrit(), projectilePrefab, DamageColorIndex.Default);
                }

            }
        }
        private void SuperMissileICBMLaunch(Vector3 position, CharacterBody attacker, ProcChainMask procChainMask, GameObject victim,float missileDamage,bool isCrit,GameObject projectilePrefab,DamageColorIndex damageColorIndex)
        {
            Vector3 initialDirection = Vector3.up + UnityEngine.Random.insideUnitSphere * 0.1f;
            float force = 200f;
            bool addMissileProc = true;
            int num1 = characterBody.inventory?.GetItemCount(DLC1Content.Items.MoreMissile)??0;
            float num2 = Mathf.Max(1f, (1 + 0.5f * (num1 - 1)));
            InputBankTest component = inputBank;
            ProcChainMask procChainMask1 = procChainMask;
            if (addMissileProc)
                procChainMask1.AddProc(ProcType.Missile);
            FireProjectileInfo fireProjectileInfo = new FireProjectileInfo()
            {
                projectilePrefab = projectilePrefab,
                position = position,
                rotation = Util.QuaternionSafeLookRotation(gunRay.direction),
                procChainMask = procChainMask1,
                target = victim,
                owner = attacker.gameObject,
                damage = missileDamage*num2,
                crit = isCrit,
                force = force,
                damageColorIndex = damageColorIndex
            };
            ProjectileManager.instance.FireProjectile(fireProjectileInfo);
            if (num1 <= 0)
                return;
            Vector3 axis = component ? component.aimDirection : attacker.transform.position;
            FireProjectileInfo fireProjectileInfo1 = fireProjectileInfo;
            fireProjectileInfo1.rotation = Util.QuaternionSafeLookRotation(Quaternion.AngleAxis(45f, axis) * initialDirection);
            fireProjectileInfo1.projectilePrefab = sMissileExtraMissiles;
            fireProjectileInfo1.damage = Modules.StaticValues.missileDamageCoefficient * damageStat;
            FireProjectileInfo fireProjectileInfo2 = fireProjectileInfo;
            fireProjectileInfo2.rotation = Util.QuaternionSafeLookRotation(Quaternion.AngleAxis(-45f, axis) * initialDirection);
            fireProjectileInfo2.projectilePrefab = sMissileExtraMissiles;
            fireProjectileInfo2.damage = Modules.StaticValues.missileDamageCoefficient * damageStat;
            ProjectileManager.instance.FireProjectile(fireProjectileInfo1);
            ProjectileManager.instance.FireProjectile(fireProjectileInfo2);
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge >= this.fireDuration)
                fireMissile();
            if (base.fixedAge >= this.duration && base.isAuthority)
                this.outer.SetNextStateToMain();

        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

        public void calculateSMissiles()
        {

            int output = 0;
            if (secStock % 5 == 0)
            {
                output = secStock / 5;
            }

            else if (secStock / 5 > 1)
            {
                output = secStock / 5;
            }
            else if (secStock < 1)
            {
                output = 0;
            }

            base.skillLocator.special.maxStock = output;
        }
    }
}
