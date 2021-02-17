using SamusMod.States;
using UnityEngine;
using RoR2.Projectile;
namespace SamusMod.States
{
    public class FireBeam : BaseFireBeam
    {
        public Vector3 sizes;
        





        public override void OnEnter()
        {
            this.baseDuration = .5f;

            this.force = 5f;
            this.maxDamageCoefficient = StaticValues.cshootDamageCoefficient;
            this.minDamageCoefficient = StaticValues.shootDamageCoefficient;
            this.selfForce = 0f;
            //this.muzzleflashEffectPrefab
            this.projectilePrefab = Modules.Projectiles.beam;
            this.speed = 200f;
            this.ResizeProjectile();
            base.OnEnter();
        }


        public override void OnExit()
        {
            this.projectilePrefab.transform.localScale = Vector3.one;
            this.projectilePrefab.GetComponent<ProjectileController>().ghostPrefab.transform.localScale = Vector3.one;
            
            base.OnExit();
        }


    }
}
