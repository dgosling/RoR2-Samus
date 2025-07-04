﻿using RoR2;
using UnityEngine;
using EntityStates;
using RoR2.Projectile;
namespace SamusMod.SkillStates.Samus
{


    public class MorphBallBomb : BaseSkillState
    {

        public float baseDuration = .1f;

        public string bombSound;
        [SerializeField]
        public GameObject projectilePrefab;
        private bool hasFired;
        private ChildLocator childLocator;

        public float damageCoef = 3f*Modules.Config.pBombMult;


        public override void OnEnter()
        {
            base.OnEnter();
            this.bombSound = SamusMod.Modules.Sounds.primeBomb;

            childLocator = base.GetModelChildLocator();
            //this.projectilePrefab = SamusMod.Modules.Projectiles.morphBomb;

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
            base.OnExit();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}
