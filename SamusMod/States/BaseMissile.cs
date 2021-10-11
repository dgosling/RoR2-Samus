using RoR2;
using UnityEngine;
using RoR2.Projectile;
using EntityStates;


namespace SamusMod.States
{


    public abstract class BaseMissile : BaseSkillState
    {
        
        public float damageCoef;
        public float baseDuration;
        public float recoil;
        public GameObject projectilePrefab;
        public static GameObject muzzleEffectPrefab;
       public static int secStock;
        public string Sound;
        private float duration;
        private float fireDuration;
        private bool hasFired;
        private Animator animator;
        private string muzzleString;
        public HurtBox target;

        public override void OnEnter()
        {
            base.OnEnter();
            
            this.duration = this.baseDuration / this.attackSpeedStat;
            this.fireDuration = .5f * this.duration;
            base.characterBody.SetAimTimer(2f);
            this.animator = base.GetModelAnimator();
            this.muzzleString = "gunCon";
            //this.PlayAnimation("Gesture, Override", "Missile", "Missile.playbackRate", this.duration);
            this.PlayAnimation("Gesture, Override", "Missile", "Missile.playbackRate", this.duration);
            secStock = skillLocator.secondary.stock;
            muzzleEffectPrefab = SamusMod.Modules.Assets.missileEffect;
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
                if (VRAPI.Utils.IsUsingMotionControls(characterBody))
                    aimRay = VRAPI.MotionControls.dominantHand.aimRay;
                if (muzzleEffectPrefab != null)
                    EffectManager.SimpleMuzzleFlash(muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
                if (base.isAuthority && this.target != null)
                {
                    FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
                    {
                        projectilePrefab = projectilePrefab,
                        position = aimRay.origin,
                        rotation = Util.QuaternionSafeLookRotation(aimRay.direction),
                        damage = this.damageCoef * this.damageStat,
                        owner = base.gameObject,
                        force = 5f,
                        crit = base.RollCrit(),
                        speedOverride = StaticValues.missileSpeed,
                        target = this.target.gameObject
                    };
                    ProjectileManager.instance.FireProjectile(fireProjectileInfo);
                }

                else if (base.isAuthority&&this.target==null)
                {


                    //sound placeholder
                    FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
                    {
                        projectilePrefab = projectilePrefab,
                        position = aimRay.origin,
                        rotation = Util.QuaternionSafeLookRotation(aimRay.direction),
                        damage = this.damageCoef * this.damageStat,
                        owner = base.gameObject,
                        force = 5f,
                        crit = base.RollCrit(),
                        speedOverride = StaticValues.missileSpeed
                    };
                    ProjectileManager.instance.FireProjectile(fireProjectileInfo);
                }

            }
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

        public  void calculateSMissiles()
        {
            
            int output = 0;
            if (secStock % 5 == 0)
            {
                output = secStock / 5;
            }

            else if(secStock/5>1)
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