using EnigmaticThunder.Modules;
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

        //public static GameObject cbeam;

        public static void LateSetup()
        {
            var overlapAttack = beam.GetComponent<ProjectileOverlapAttack>();
            if (overlapAttack) overlapAttack.damageCoefficient = 1f;
        }

        public static void RegisterProjectiles()
        {
            #region missile
            missile = EnigmaticThunder.Modules.Prefabs.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/MissileProjectile"), "SamusMissile", true);
            GameObject missileGhost = Assets.missile.InstantiateClone("SamusMissileGhost", false);
            missileGhost.AddComponent<ProjectileGhostController>();
            missile.GetComponent<ProjectileController>().ghostPrefab = missileGhost;
            missile.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;
            missile.GetComponent<MissileController>().maxSeekDistance = 100f;
            missile.GetComponent<MissileController>().acceleration = 5f;
            missile.GetComponent<MissileController>().giveupTimer = 3f;
            missile.GetComponent<MissileController>().maxVelocity = 50f;
            missile.GetComponent<MissileController>().delayTimer = .3f;
            
            //missile.GetComponent<Rigidbody>().useGravity = false;
            #endregion

            #region smissile
            smissile = EnigmaticThunder.Modules.Prefabs.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/MageIcebolt"), "SamusSuperMissile", true);
            GameObject smissileGhost = Assets.smissile.InstantiateClone("SamusSuperMissileGhost", false);
            smissileGhost.AddComponent<ProjectileGhostController>();
            smissile.GetComponent<ProjectileController>().ghostPrefab = smissileGhost;
            smissile.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;
            smissile.GetComponent<ProjectileImpactExplosion>().blastRadius = 10;
            smissile.GetComponent<Rigidbody>().useGravity = false;
            smissile.GetComponent<ProjectileImpactExplosion>().impactEffect = Resources.Load<GameObject>("Prefabs/Effects/ImpactEffects/ExplosionVFX");
            smissile.GetComponent<ProjectileSimple>().lifetime = 10f;
            smissile.GetComponent<ProjectileSimple>().velocity = 20f;
            smissile.GetComponent<ProjectileController>().procCoefficient = 1f;
            smissile.GetComponent<ProjectileDamage>().damage = 25;
            smissile.GetComponent<ProjectileImpactExplosion>().lifetime = 5f;
            #endregion

            #region beam
            beam = EnigmaticThunder.Modules.Prefabs.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/MageIcebolt"), "SamusBeam", true);
            GameObject beamGhost = Assets.cbeam.InstantiateClone("SamusBeamGhost", false);
            beamGhost.AddComponent<ProjectileGhostController>();
            SamusPlugin.Destroy(beam.GetComponent<ProjectileImpactExplosion>());
            beam.AddComponent<ProjectileSingleTargetImpact>();
            //var beamSingleImpact = beam.GetComponent<ProjectileSingleTargetImpact>();
            beam.GetComponent<ProjectileSingleTargetImpact>().destroyOnWorld = true;
            beam.GetComponent<ProjectileController>().ghostPrefab = beamGhost;
            beam.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;
            //beam.GetComponent<ProjectileImpactExplosion>().blastRadius = .5f;
            beam.GetComponent<ProjectileSimple>().velocity = 120;
            beam.GetComponent<Rigidbody>().useGravity = false;
            beam.GetComponent<ProjectileSimple>().lifetime = 10f;
            //beam.GetComponent<ProjectileImpactExplosion>().lifetime = 3f;
            beam.GetComponent<ProjectileController>().procCoefficient = 1f;
            beam.GetComponent<ProjectileDamage>().damage = 10;
            foreach(SphereCollider i in beam.GetComponentsInChildren<SphereCollider>())
            {
                var sphere = i;
                i.radius = .5f;
            }
            beam.GetComponent<ProjectileSingleTargetImpact>().impactEffect = Modules.Assets.beamImpactEffect;
            

            //SamusPlugin.Destroy(beam.GetComponent<AntiGravityForce>());
            //SamusPlugin.Destroy(beam.GetComponent<ProjectileProximityBeamController>());

            beam.GetComponent<Transform>().localScale = Vector3.one;
            beam.GetComponent<ProjectileController>().ghostPrefab.transform.localScale = Vector3.one;
            #endregion
            #region bomb
            bomb = EnigmaticThunder.Modules.Prefabs.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/CommandoGrenadeProjectile"),"SamusBomb",true);
            GameObject bombGhost = Assets.bomb.InstantiateClone("SamusBombGhost", false);
            bombGhost.AddComponent<ProjectileGhostController>();
            bomb.GetComponent<ProjectileController>().ghostPrefab = bombGhost;
            var bombSimple = bomb.GetComponent<ProjectileSimple>();
            var bombControl = bomb.GetComponent<ProjectileController>();
            var bombExpl = bomb.GetComponent<ProjectileImpactExplosion>();
            bombSimple.velocity = 0;
            bombExpl.falloffModel = BlastAttack.FalloffModel.Linear;
            bombExpl.lifetime = 5;
            bombExpl.blastRadius = 5;
            bombExpl.blastDamageCoefficient = 2f;


            SamusPlugin.Destroy(bomb.GetComponent<PhysicsImpactSpeedModifier>());

            #endregion
            #region morphBomb
            morphBomb = EnigmaticThunder.Modules.Prefabs.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/CommandoGrenadeProjectile"), "SamusMorphBomb", true);
            GameObject morphGhost = Assets.morphBomb.InstantiateClone("SamusMorphBombGhost", false);
            morphGhost.AddComponent<ProjectileGhostController>();
            morphBomb.GetComponent<ProjectileController>().ghostPrefab = morphGhost;
            var morphSimple = morphBomb.GetComponent<ProjectileSimple>();
            var morphControl = morphBomb.GetComponent<ProjectileController>();
            morphSimple.lifetime = 3;
            var morphExpl = morphBomb.GetComponent<ProjectileImpactExplosion>();
            morphBomb.GetComponent<Rigidbody>().useGravity = false;
            morphBomb.GetComponent<Rigidbody>().detectCollisions = false;
            var morphTeam = morphBomb.GetComponent<TeamFilter>();
            morphTeam.teamIndex = TeamIndex.Player;
            morphSimple.velocity = 0;
            morphExpl.falloffModel = BlastAttack.FalloffModel.Linear;
            morphExpl.impactEffect = Assets.bombExplosion;
            //morphExpl.blastAttackerFiltering = AttackerFiltering.NeverHit;
            morphControl.startSound = Sounds.primeBomb;
            morphExpl.lifetime = 2.5f;
            morphExpl.blastRadius = 5;
            morphExpl.blastDamageCoefficient = 1f;

            SamusPlugin.Destroy(morphBomb.GetComponent<PhysicsImpactSpeedModifier>());
            #endregion



            EnigmaticThunder.Modules.Projectiles.RegisterProjectile(missile);
            EnigmaticThunder.Modules.Projectiles.RegisterProjectile(smissile);
            EnigmaticThunder.Modules.Projectiles.RegisterProjectile(beam);
            EnigmaticThunder.Modules.Projectiles.RegisterProjectile(bomb);
            EnigmaticThunder.Modules.Projectiles.RegisterProjectile(morphBomb);
        }

        //public static void RegisterChargeBeam(GameObject gameObject)
        //{
        //   GameObject cbeam = EnigmaticThunder.Modules.Prefabs.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/MageIcebolt"), "SamusChargeBeam", true);
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
