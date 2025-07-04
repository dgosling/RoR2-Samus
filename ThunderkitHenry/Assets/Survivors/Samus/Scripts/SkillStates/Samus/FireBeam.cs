﻿using SamusMod.SkillStates.BaseStates;
using UnityEngine;
using RoR2.Projectile;
using RoR2;
namespace SamusMod.SkillStates.Samus
{


    public class FireBeam : BaseFireBeam
    {
        public Vector3 sizes;






        public override void OnEnter()
        {
            this.baseDuration = 0.05f;

            this.force = 5f;
            this.maxDamageCoefficient = Modules.StaticValues.cshootDamageCoefficient;
            this.minDamageCoefficient = Modules.StaticValues.shootDamageCoefficient;
            this.selfForce = 0f;
            //this.muzzleflashEffectPrefab = Modules.Assets.beamShootEffect;
            //this.projectilePrefab = Modules.Projectiles.beam;
            this.speed = 200f;
            this.ResizeProjectile();
            this.tracerPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/TracerNoSmoke");
            this.muzzleName = "gunCon";
            //this.bulletHitEffect = Modules.Assets.beamImpactEffect;
            if (this.charge <= .4f)
            {
                this.projSound = SamusMod.Modules.Sounds.cShoot25Sound;

            }
            else if (this.charge <= .7f)
            {
                this.projSound = SamusMod.Modules.Sounds.cShoot50Sound;
            }
            else if (this.charge <= .9f)
            {
                this.projSound = SamusMod.Modules.Sounds.cShoot75Sound;
            }
            else
            {
                this.projSound = SamusMod.Modules.Sounds.cShootFullSound;
            }

            this.tracerSound = SamusMod.Modules.Sounds.beamSound;

            //Debug.Log(this.projectilePrefab.GetComponent<SphereCollider>().radius);
            //Debug.Log(this.charge);

            base.OnEnter();
        }


        public override void OnExit()
        {
            this.projectilePrefab.transform.localScale = Vector3.one;
            this.projectilePrefab.GetComponent<ProjectileController>().ghostPrefab.transform.localScale = Vector3.one;
            this.projectilePrefab.GetComponent<SphereCollider>().radius = .6f;

            base.OnExit();
        }
    }
}
