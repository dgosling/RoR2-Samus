using R2API;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace SamusMod.Modules
{
    public static class Projectiles
    {
        public static GameObject missile;
        public static GameObject smissile;
        public static GameObject beam;
        public static GameObject bomb;
        public static GameObject morphBomb;
        public static GameObject pMorphBomb;
        public static GameObject altmissile;

        //public static GameObject cbeam;

        public static void LateSetup()
        {
            var overlapAttack = beam.GetComponent<ProjectileOverlapAttack>();
            if (overlapAttack) overlapAttack.damageCoefficient = 1f;
        }

        public static void RegisterProjectiles()
        {
            #region missile
            missile = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/MissileProjectile"), "SamusMissile", true);
            GameObject missileGhost = Assets.missile.InstantiateClone("SamusMissileGhost", false);
            missileGhost.AddComponent<ProjectileGhostController>();
            missile.GetComponent<ProjectileController>().ghostPrefab = missileGhost;
            missile.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;
            //missile.GetComponent<ProjectileController>().allowPrediction = false;
            missile.GetComponent<MissileController>().maxSeekDistance = 100f;
            missile.GetComponent<MissileController>().acceleration = 5f;
            missile.GetComponent<MissileController>().giveupTimer = 3f;
            missile.GetComponent<MissileController>().maxVelocity = 50f;
            missile.GetComponent<MissileController>().delayTimer = .3f;

            //missile.GetComponent<Rigidbody>().useGravity = false;
            #endregion
            altmissile = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/EngiHarpoon"), "SamusaltMissile", true);
            GameObject altmissileGhost = Assets.missile.InstantiateClone("SamusMissileGhost", false);
            altmissileGhost.AddComponent<ProjectileGhostController>();
            altmissile.GetComponent<ProjectileController>().ghostPrefab = altmissileGhost;
            altmissile.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;
            //altmissile.GetComponent<ProjectileController>().allowPrediction = false;
            altmissile.GetComponent<MissileController>().maxSeekDistance = 100f;
            altmissile.GetComponent<MissileController>().acceleration = 5f;
            altmissile.GetComponent<MissileController>().giveupTimer = 3f;
            altmissile.GetComponent<MissileController>().maxVelocity = 50f;
            altmissile.GetComponent<ProjectileSingleTargetImpact>().impactEffect = Resources.Load<GameObject>("prefabs/effects/impacteffects/MissileExplosionVFX");
            altmissile.GetComponent<MissileController>().delayTimer = 0;
            SamusPlugin.Destroy(altmissile.GetComponent<ApplyTorqueOnStart>());
            #region smissile
            smissile = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/MageIcebolt"), "SamusSuperMissile", true);
            GameObject smissileGhost = Assets.smissile.InstantiateClone("SamusSuperMissileGhost", false);
            smissileGhost.AddComponent<ProjectileGhostController>();
            smissile.GetComponent<ProjectileController>().ghostPrefab = smissileGhost;
            smissile.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;

            smissile.GetComponent<ProjectileImpactExplosion>().blastRadius = 10;
            smissile.GetComponent<Rigidbody>().useGravity = false;
            smissile.GetComponent<ProjectileImpactExplosion>().impactEffect = Resources.Load<GameObject>("Prefabs/Effects/ImpactEffects/ExplosionVFX");
            smissile.GetComponent<ProjectileSimple>().lifetime = 10f;
            smissile.GetComponent<ProjectileSimple>().desiredForwardSpeed = 20f;
            smissile.GetComponent<ProjectileController>().procCoefficient = 1f;
            smissile.GetComponent<ProjectileDamage>().damage = 25;
            smissile.GetComponent<ProjectileImpactExplosion>().lifetime = 5f;
            smissile.GetComponent<ProjectileImpactExplosion>().fireChildren = false;
            #endregion

            #region beam
            beam = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/MageIcebolt"), "SamusBeam", true);
            GameObject beamGhost = Assets.cbeam.InstantiateClone("SamusBeamGhost", false);
            beamGhost.AddComponent<ProjectileGhostController>();
            SamusPlugin.Destroy(beam.GetComponent<ProjectileImpactExplosion>());
            beam.AddComponent<ProjectileSingleTargetImpact>();
            //if (VRAPI.VR.enabled == false) 
           // { 
                beam.AddComponent<Misc.colision_test>();
           // }
            //var beamSingleImpact = beam.GetComponent<ProjectileSingleTargetImpact>();
            beam.GetComponent<ProjectileSingleTargetImpact>().destroyOnWorld = true;
            beam.GetComponent<ProjectileController>().ghostPrefab = beamGhost;
            beam.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;
            //beam.GetComponent<ProjectileImpactExplosion>().blastRadius = .5f;
            beam.GetComponent<ProjectileSimple>().desiredForwardSpeed = 120;
            beam.GetComponent<Rigidbody>().useGravity = false;
            beam.GetComponent<ProjectileSimple>().lifetime = 10f;
            //beam.GetComponent<ProjectileImpactExplosion>().lifetime = 3f;
            beam.GetComponent<ProjectileController>().procCoefficient = 1f;
            beam.GetComponent<ProjectileDamage>().damage = 10;
            foreach(SphereCollider i in beam.GetComponentsInChildren<SphereCollider>())
            {
                var sphere = i;
                i.radius = .6f;
            }
            beam.GetComponent<ProjectileSingleTargetImpact>().impactEffect = Modules.Assets.beamImpactEffect;
            

            //SamusPlugin.Destroy(beam.GetComponent<AntiGravityForce>());
            //SamusPlugin.Destroy(beam.GetComponent<ProjectileProximityBeamController>());

            beam.GetComponent<Transform>().localScale = Vector3.one;
            beam.GetComponent<ProjectileController>().ghostPrefab.transform.localScale = Vector3.one;
            #endregion
            #region bomb
            bomb = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/CommandoGrenadeProjectile"),"SamusBomb",true);
            GameObject bombGhost = Assets.bomb.InstantiateClone("SamusBombGhost", false);
            bombGhost.AddComponent<ProjectileGhostController>();
            bomb.GetComponent<ProjectileController>().ghostPrefab = bombGhost;
            var bombSimple = bomb.GetComponent<ProjectileSimple>();
            var bombControl = bomb.GetComponent<ProjectileController>();
            var bombExpl = bomb.GetComponent<ProjectileImpactExplosion>();
            bombSimple.desiredForwardSpeed = 0;
            bombExpl.falloffModel = BlastAttack.FalloffModel.Linear;
            bombExpl.lifetime = 5;
            bombExpl.blastRadius = 5;
            bombExpl.blastDamageCoefficient = 2f;


            SamusPlugin.Destroy(bomb.GetComponent<PhysicsImpactSpeedModifier>());

            #endregion
            #region morphBomb
            morphBomb = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/CommandoGrenadeProjectile"), "SamusMorphBomb", true);
            GameObject morphGhost = Assets.morphBomb.InstantiateClone("SamusMorphBombGhost", false);
            morphGhost.AddComponent<ProjectileGhostController>();
            morphBomb.GetComponent<ProjectileController>().ghostPrefab = morphGhost;
            
            var morphSimple = morphBomb.GetComponent<ProjectileSimple>();
            var morphControl = morphBomb.GetComponent<ProjectileController>();
            morphSimple.lifetime = 1.02f;
            var morphExpl = morphBomb.GetComponent<ProjectileImpactExplosion>();
            morphBomb.GetComponent<Rigidbody>().useGravity = false;
            morphBomb.GetComponent<Rigidbody>().isKinematic = true;
            morphBomb.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            morphBomb.GetComponent<Rigidbody>().detectCollisions = false;
            //morphBomb.GetComponent<SphereCollider>().radius = .25f;
            //var morphTeam = morphBomb.GetComponent<TeamFilter>();
            //morphTeam.teamIndex = TeamIndex.Player;
            morphSimple.desiredForwardSpeed = 0;
            morphExpl.falloffModel = BlastAttack.FalloffModel.Linear;
            morphExpl.impactEffect = Assets.bombExplosion;
            //morphExpl.blastAttackerFiltering = AttackerFiltering.NeverHit;
            morphControl.startSound = Sounds.primeBomb;
            morphExpl.lifetimeExpiredSound = null;
            morphExpl.lifetime = 1f;
            morphExpl.blastAttackerFiltering = AttackerFiltering.AlwaysHit;
            morphExpl.bonusBlastForce = Vector3.zero;
            morphExpl.blastRadius = 1;
            morphExpl.blastDamageCoefficient = 1f;
            //morphBomb.AddComponent<Misc.colision_test>();
            SamusPlugin.Destroy(morphBomb.GetComponent<PhysicsImpactSpeedModifier>());
            #endregion
            #region pMorphBomb
            pMorphBomb = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/CaptainAirstrikeProjectile1"), "SamusPowerBomb", true);
            GameObject pmorphGhost = Assets.powerbomb1.InstantiateClone("SamusPowerBombGhost", false);
            pmorphGhost.AddComponent<ProjectileGhostController>();
            pMorphBomb.GetComponent<ProjectileController>().ghostPrefab = pmorphGhost;
            //var pmorphSimple = pMorphBomb.GetComponent<ProjectileSimple>();
            var pmorphDam = pMorphBomb.GetComponent<ProjectileDamage>();
            var pmorphControl = pMorphBomb.GetComponent<ProjectileController>();
            var pmorphExpl = pMorphBomb.GetComponent<ProjectileImpactExplosion>();
            InitializeImpactExplosion(pmorphExpl);
            pmorphExpl.falloffModel = BlastAttack.FalloffModel.None;
            pmorphExpl.impactEffect = Assets.powerbomb;
            pmorphDam.damageType = DamageType.Generic;
            //pmorphExpl.explosionEffect = Assets.powerbomb;
            pmorphExpl.timerAfterImpact = false;
            pmorphExpl.lifetimeAfterImpact = 8;
            //pmorphExpl.lifetimeExpiredSound = Assets.powerBombExplosionSound;
            pmorphExpl.lifetime = .5f;
            pmorphExpl.blastRadius = 20;
            pmorphExpl.blastDamageCoefficient = 1f;
            //pMorphBomb.GetComponent<Rigidbody>().useGravity = false;
            //pMorphBomb.GetComponent<Rigidbody>().detectCollisions = false;
            //pmorphSimple.lifetime = 1;
            //pmorphSimple.desiredForwardSpeed = 0;
            //pMorphBomb.GetComponent<SphereCollider>().radius = 1;
            
            //pmorphControl.startSound = Sounds.powerBomb;

            //pmorphExpl.lifetimeAfterImpact = 8;

            //SamusPlugin.Destroy(pMorphBomb.GetComponent<PhysicsImpactSpeedModifier>());

            #endregion



            ProjectileAPI.Add(missile);
            ProjectileAPI.Add(altmissile);
            ProjectileAPI.Add(smissile);
            ProjectileAPI.Add(beam);
            ProjectileAPI.Add(bomb);
            ProjectileAPI.Add(morphBomb);
            ProjectileAPI.Add(pMorphBomb);
        }

        private static void InitializeImpactExplosion(ProjectileImpactExplosion projectileImpactExplosion)
        {
            projectileImpactExplosion.blastDamageCoefficient = 1f;
            projectileImpactExplosion.blastProcCoefficient = 1f;
            projectileImpactExplosion.blastRadius = 1f;
            projectileImpactExplosion.bonusBlastForce = Vector3.zero;
            projectileImpactExplosion.childrenCount = 0;
            projectileImpactExplosion.childrenDamageCoefficient = 0f;
            projectileImpactExplosion.childrenProjectilePrefab = null;
            projectileImpactExplosion.destroyOnEnemy = false;
            projectileImpactExplosion.destroyOnWorld = false;
            projectileImpactExplosion.falloffModel = RoR2.BlastAttack.FalloffModel.None;
            projectileImpactExplosion.fireChildren = false;
            projectileImpactExplosion.impactEffect = null;
            projectileImpactExplosion.lifetime = 0f;
            projectileImpactExplosion.lifetimeAfterImpact = 0f;
            projectileImpactExplosion.lifetimeExpiredSound = null;
            projectileImpactExplosion.lifetimeRandomOffset = 0f;
            projectileImpactExplosion.offsetForLifetimeExpiredSound = 0f;
            projectileImpactExplosion.timerAfterImpact = false;

            projectileImpactExplosion.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;
        }
        //public static void RegisterChargeBeam(GameObject gameObject)
        //{
        //   GameObject cbeam = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/MageIcebolt"), "SamusChargeBeam", true);
        //    GameObject cbeamGhost = Assets.cbeam.InstantiateClone("SamusChargeBeamGhost", false);
        //    cbeamGhost.AddComponent<ProjectileGhostController>();

        //    cbeam.GetComponent<ProjectileController>().ghostPrefab = gameObject;
        //    cbeam.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;
        //    cbeam.GetComponent<ProjectileImpactExplosion>().blastRadius = 2;
        //    cbeam.GetComponent<ProjectileSimple>().velocity = 120;
        //    cbeam.GetComponent<Rigidbody>().useGravity = false;
        //    cbeam.GetComponent<ProjectileSimple>().lifetime = 10f;
        //    cbeam.GetComponent<ProjectileImpactExplosion>().lifetime = 3f;
        //    cbeam.GetComponent<ProjectileController>().procCoefficient = 1f;
        //    cbeam.GetComponent<ProjectileDamage>().damage = 10;
        //    cbeam.GetComponent<SphereCollider>().radius = 1;

        //    ProjectileCatalog.getAdditionalEntries += list =>
        //    {
        //        list.Add(cbeam);
        //    };
        //}

        //public static void DestroyChargeBeam()
        //{
        //    ProjectileCatalog.getAdditionalEntries -= list =>
        //    {

        //    }
        //}
    }
}
