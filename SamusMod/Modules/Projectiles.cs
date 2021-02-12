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
        //public static GameObject cbeam;

        public static void LateSetup()
        {
            var overlapAttack = beam.GetComponent<ProjectileOverlapAttack>();
            if (overlapAttack) overlapAttack.damageCoefficient = 1f;
        }

        public static void RegisterProjectiles()
        {
            #region missile
            missile = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/ToolbotGrenadeLauncherProjectile"), "SamusMissile", true);
            GameObject missileGhost = Assets.missile.InstantiateClone("SamusMissileGhost", false);
            missileGhost.AddComponent<ProjectileGhostController>();
            missile.GetComponent<ProjectileController>().ghostPrefab = missileGhost;
            missile.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;
            missile.GetComponent<ProjectileImpactExplosion>().blastRadius = 5;
            missile.GetComponent<Rigidbody>().useGravity = false;
            #endregion

            #region smissile
            smissile = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/ToolbotGrenadeLauncherProjectile"), "SamusSuperMissile", true);
            GameObject smissileGhost = Assets.smissile.InstantiateClone("SamusSuperMissileGhost", false);
            smissileGhost.AddComponent<ProjectileGhostController>();
            smissile.GetComponent<ProjectileController>().ghostPrefab = smissileGhost;
            smissile.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;
            smissile.GetComponent<ProjectileImpactExplosion>().blastRadius = 10;
            smissile.GetComponent<Rigidbody>().useGravity = false;
            #endregion

            #region beam
            beam = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/MageIcebolt"), "SamusBeam", true);
            GameObject beamGhost = Assets.beam.InstantiateClone("SamusBeamGhost", false);
            beamGhost.AddComponent<ProjectileGhostController>();

            beam.GetComponent<ProjectileController>().ghostPrefab = beamGhost;
            beam.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;
            beam.GetComponent<ProjectileImpactExplosion>().blastRadius = 1;
            beam.GetComponent<ProjectileSimple>().velocity = 120;
            beam.GetComponent<Rigidbody>().useGravity = false;
            //beam.GetComponent<Transform>().localScale = new Vector3(.1f, .1f, .1f);
            #endregion

            ProjectileCatalog.getAdditionalEntries += list =>
            {
                list.Add(missile);
                list.Add(smissile);
                list.Add(beam);
            };
        }
    }
}
