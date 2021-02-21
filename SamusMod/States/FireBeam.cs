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
            this.baseDuration = .1f;

            this.force = 5f;
            this.maxDamageCoefficient = StaticValues.cshootDamageCoefficient;
            this.minDamageCoefficient = StaticValues.shootDamageCoefficient;
            this.selfForce = 0f;
            //this.muzzleflashEffectPrefab
            this.projectilePrefab = Modules.Projectiles.beam;
            this.speed = 200f;
            this.ResizeProjectile();
            this.tracerPrefab = Resources.Load<GameObject>("Prefabs/Effects/Tracers/TracerNoSmoke");
            this.muzzleName = "gunCon";
            this.bulletHitEffect = Resources.Load<GameObject>("Prefabs/Effects/ImpactEffects/BulletImpactSoft");
            //Debug.Log(this.charge);
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
