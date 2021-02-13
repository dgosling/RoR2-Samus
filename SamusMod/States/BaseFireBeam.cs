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
        public float selfForce;
        public float charge;
        public float speed;
        private GameObject guncon;
        public Vector3 size;
        public Vector3 csize;

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log(charge);
            this.duration = this.baseDuration / this.attackSpeedStat;
            ChildLocator childLocator = base.GetModelChildLocator();
            Transform transform = childLocator.FindChild("gunCon");
            guncon = transform.gameObject;
            this.csize = this.projectilePrefab.gameObject.transform.localScale;
            Debug.Log(csize);
              
         //   if (charge == 1)
          //  {
          //      base.PlayAnimation("Gesture, Override", "chargeMaxShoot", "Shoot.playbackRate", this.duration);



         //   }
          //  else
          //  {
                base.PlayAnimation("Gesture, Override", "Beam", "Shoot.playbackRate", this.duration);
            //}
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
            this.projectilePrefab.gameObject.transform.localScale = Vector3.one;
            this.csize = Vector3.zero;
            this.size = Vector3.zero;
            base.OnExit();

            
        }

        private void Fire()
        {
            if (base.isAuthority)
            {
                Ray aimRay = base.GetAimRay();
                if(this.projectilePrefab != null)
                {
                    this.projectilePrefab.gameObject.transform.localScale = this.csize * this.charge;
                    Debug.Log(projectilePrefab.gameObject.transform.localScale);
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
                        owner = base.gameObject,
                        damage = this.damageStat * num,
                        force = num2,
                        crit = base.RollCrit(),
                        speedOverride = this.speed,
                        
                        
                        
                    };

                    ProjectileManager.instance.FireProjectile(fireProjectileInfo);
                }
                //if (base.characterMotor)
                //{
                //    base.characterMotor.ApplyForce(aimRay.direction * (-this.selfForce * this.charge), false, false);
                //}
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}
