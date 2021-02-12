using EntityStates;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using SamusMod.States;


namespace SamusMod.States
{
    public abstract class BaseFireBeam : BaseSkillState
    {
        public GameObject projectilePrefab;
        public GameObject muzzleflashEffectPrefab;
        public float baseDuration;
        public float minDamageCoefficient;
        public float maxDamageCoefficient;
        public float force;
        private float duration;
        public float charge;
        public float speed;
        private GameObject guncon;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            ChildLocator childLocator = base.GetModelChildLocator();
            Transform transform = childLocator.FindChild("Base");
            guncon = transform.gameObject;

              
            if (charge == 1)
            {
                base.PlayAnimation("Gesture, Override", "chargeMaxShoot", "Shoot.playbackRate", this.duration);



            }
            else
            {
                base.PlayAnimation("Gesture, Override", "Beam", "Shoot.playbackRate", this.duration);
            }
            if (this.muzzleflashEffectPrefab)
            {
                EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, "gunCon", false);
            }
            
            //Util.PlaySound();

            this.Fire();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(base.isAuthority && base.fixedAge >= this.duration)
            {
                this.outer.SetNextStateToMain();
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            
        }

        private void Fire()
        {
            if (base.isAuthority)
            {
                Ray aimRay = base.GetAimRay();
                if(this.projectilePrefab != null)
                {
                    projectilePrefab.transform.localScale = SamusMod.States.ChargeBeamBase.size * 100;
                }
                if(this.projectilePrefab != null)
                {
                    float num = Util.Remap(this.charge, 0f, 1f, this.minDamageCoefficient, this.maxDamageCoefficient);
                    float num2 = this.charge * this.force;

                    FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
                    {
                        projectilePrefab = this.projectilePrefab,
                        position = aimRay.origin,
                        rotation = Util.QuaternionSafeLookRotation(aimRay.direction),
                        owner = this.guncon,
                        damage = this.damageStat * num,
                        force = num2,
                        crit = base.RollCrit(),
                        speedOverride = this.speed,
                        
                        
                    };

                    ProjectileManager.instance.FireProjectile(fireProjectileInfo);
                }
                if (base.characterMotor)
                {
                    base.characterMotor.ApplyForce(aimRay.direction * (this.charge), false, false);
                }
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}
