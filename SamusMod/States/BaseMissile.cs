using RoR2;
using UnityEngine;
using RoR2.Projectile;
using EntityStates;
using R2API;

namespace SamusMod.States
{


    public abstract class BaseMissile : BaseSkillState
    {

        public float damageCoef;
        public float baseDuration;
        public float recoil;
        public GameObject projectilePrefab;
        public static GameObject muzzleEffectPrefab;

        private float duration;
        private float fireDuration;
        private bool hasFired;
        private Animator animator;
        private string muzzleString;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            this.fireDuration = .5f * this.duration;
            base.characterBody.SetAimTimer(2f);
            this.animator = base.GetModelAnimator();
            this.muzzleString = "gunCon";
            this.PlayAnimation("Gesture, Override", "Missile", "Missile.playbackRate", this.duration);
            this.PlayAnimation("Gesture, Additive", "Missile", "Missile.playbackRate", this.duration);
            
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        private void fireMissile()
        {
            if (!hasFired)
            {
                this.hasFired = true;

                base.characterBody.AddSpreadBloom(.75f);
                Ray aimRay = base.GetAimRay();
                if (muzzleEffectPrefab != null)
                    EffectManager.SimpleMuzzleFlash(muzzleEffectPrefab, base.gameObject, this.muzzleString, false);

                if (base.isAuthority)
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



    }
}