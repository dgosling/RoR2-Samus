
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using EntityStates;

namespace SamusMod.States
{
    public class MorphBallPBomb : BaseSkillState
    {
        public float baseDuration = .5f;
        public string bombSound;
        public GameObject projectilePrefab;
        private bool hasFired;
        private ChildLocator childLocator;
        public float damageCoef = 10f;


        public override void OnEnter()
        {
            base.OnEnter();
            this.bombSound = SamusMod.Modules.Sounds.powerBomb;

            childLocator = base.GetModelChildLocator();
            this.projectilePrefab = SamusMod.Modules.Projectiles.pMorphBomb;

            this.hasFired = false;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.hasFired == false)
                Fire();


            if (this.fixedAge < this.baseDuration || !this.isAuthority)
                return;
            this.outer.SetNextStateToMain();
        }

        private void Fire()
        {
            if (this.isAuthority)
            {
                Ray aimRay = this.GetAimRay();
                if (this.projectilePrefab != null)
                {
                    FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
                    {
                        projectilePrefab = this.projectilePrefab,
                        owner = base.gameObject,
                        position = base.GetModelChildLocator().FindChild("Ball2").position,
                        rotation = Util.QuaternionSafeLookRotation(aimRay.direction),
                        damage = this.damageCoef * base.damageStat,
                        force = 0,
                        crit = this.RollCrit()

                    };
                    //Util.PlaySound(this.bombSound, this.gameObject);

                    ProjectileManager.instance.FireProjectile(fireProjectileInfo);

                    this.hasFired = true;
                }
            }
        }

        public override void OnExit()
        {
            base.skillLocator.primary.RemoveAllStocks();
            
            //base.skillLocator.primary.RecalculateMaxStock();
            base.OnExit();
            
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}
