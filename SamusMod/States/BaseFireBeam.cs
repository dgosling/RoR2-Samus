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
        public string muzzleName;
        public GameObject tracerPrefab;
        public GameObject bulletHitEffect;
        private float duration;
        public float selfForce;
        public float charge;
        public float speed;
        private GameObject guncon;
        public Vector3 size;
        public string tracerSound;
        public string projSound;
        public Vector3 csize;
      //  private Transform test;

        public override void OnEnter()
        {
            base.OnEnter();
            //Debug.Log(this.charge);
            this.duration = this.baseDuration / this.attackSpeedStat;
            ChildLocator childLocator = base.GetModelChildLocator();
            Transform transform = childLocator.FindChild("gunCon");
            guncon = transform.gameObject;
            
            //this.test.position = projectilePrefab.transform.position;
            //this.test.rotation = projectilePrefab.transform.rotation;
            //  this.test.localScale = projectilePrefab.transform.localScale * charge;
            // this.csize = this.projectilePrefab.transform.localScale;
            //Debug.Log(csize);

            if (charge == 1)
            {
                base.PlayAnimation("Gesture, Override", "chargeMaxShoot", "Charge.playbackRate", this.duration);



            }
            else
            {
                base.PlayAnimation("Gesture, Override", "Beam", "Charge.playbackRate", this.duration);
            }
            if (this.muzzleflashEffectPrefab)
            {
                EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, "gunCon", false);
            }
            
            //Util.PlaySound();

            this.Fire();
        }
        public void ResizeProjectile()
        {
            
            this.projectilePrefab.transform.localScale = (this.projectilePrefab.transform.localScale * this.charge)+new Vector3(.1f,.1f,.1f);
            var controller = this.projectilePrefab.GetComponent<ProjectileController>();
            controller.ghostPrefab.transform.localScale = controller.ghostPrefab.transform.localScale * this.charge;

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
                if (charge <= .15f)
                {
                    foreach (SphereCollider i in this.projectilePrefab.GetComponentsInChildren<SphereCollider>())
                    {
                        var sphereCollider = i.radius;
                        sphereCollider = .6f;
                    }
                    //this.projectilePrefab.GetComponent<ProjectileDamageTrail>().trailPrefab.gameObject.GetComponent<TrailRenderer>().widthMultiplier = .1f;
                    this.charge = .1f;
                    //this.projectilePrefab.GetComponent<ProjectileController>().ghostPrefab.gameObject.GetComponent<TrailRenderer>().widthMultiplier = .1f;
                }
                Ray aimRay = base.GetAimRay();
                if (this.projectilePrefab != null)
                {
                    // this.ResizeProjectile();
                    //var controller = this.projectilePrefab.GetComponent<ProjectileController>();
                   // Debug.Log(this.projectilePrefab.transform.localScale);
                    //Debug.Log(this.projectilePrefab.GetComponent<ProjectileController>().ghostPrefab.transform.localScale);
                    // controller.GetComponentInChildren<TrailRenderer>().widthMultiplier = this.charge * .75f;
                }

                if (this.projectilePrefab != null && this.charge > .15f)
                {
                    float num = Util.Remap(this.charge, 0f, 1f, this.minDamageCoefficient, this.maxDamageCoefficient);
                    float num2 = this.charge * this.force;
                    Util.PlaySound(this.projSound, this.gameObject);
                    this.projectilePrefab.GetComponent<Misc.colision_test>().inTransform = this.guncon.transform;
                    
                    FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
                    {
                        projectilePrefab = this.projectilePrefab,
                        position = aimRay.origin,
                        rotation = Util.QuaternionSafeLookRotation(aimRay.direction),
                        owner = base.gameObject,
                        damage = this.damageStat * num,
                        force = num2,
                        crit = base.RollCrit(),
                        speedOverride = this.speed
                        
                        


                    };

                    ProjectileManager.instance.FireProjectile(fireProjectileInfo);
                }

                else
                {
                    if (base.isAuthority)
                    {
                        // Ray aimRay = this.GetAimRay();
                        float num = Util.Remap(.1f, .1f, 1f, this.minDamageCoefficient, this.maxDamageCoefficient);
                        float num2 = this.charge * this.force;
                        Util.PlaySound(this.tracerSound, this.gameObject);
                        new BulletAttack()
                        {
                            owner = this.gameObject,
                            weapon = this.gameObject,
                            origin = aimRay.origin,
                            aimVector = aimRay.direction,
                            minSpread = 0,
                            maxSpread = 0,
                            damage = this.damageStat,
                            force = num2,
                            tracerEffectPrefab = this.tracerPrefab,
                            muzzleName = this.muzzleName,
                            hitEffectPrefab = this.bulletHitEffect,
                            isCrit = this.RollCrit(),
                            radius = .1f,
                            smartCollision = true
                            
                            
                            
                        }.Fire();
                    }
                    //if (base.characterMotor)
                    //{
                    //    base.characterMotor.ApplyForce(aimRay.direction * (-this.selfForce * this.charge), false, false);
                    //}
                }
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}
