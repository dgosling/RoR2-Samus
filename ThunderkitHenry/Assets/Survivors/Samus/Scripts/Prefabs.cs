using RoR2;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine;

namespace SamusMod.Modules
{
    internal static class Prefabs
    {
        // The order of your SurvivorDefs in your SerializableContentPack determines the order of body/displayPrefab variables here.
        // This lets you reference any bodyPrefabs or displayPrefabs throughout your code.

        internal static List<GameObject> bodyPrefabs = new List<GameObject>();
        internal static List<BodyIndex> bodyIndexes = new List<BodyIndex>();
        internal static List<GameObject> displayPrefabs = new List<GameObject>();

        private static PhysicMaterial ragdollMaterial;

        internal static void Init()
        {
            GetPrefabs();
            AddPrefabReferences();
        }

        internal static void AddPrefabReferences()
        {
            ForEachReferences();

            //If you want to change the 'defaults' set in ForEachReferences, then set them for individual bodyPrefabs here.
            //This is if you want to use a custom crosshair or other stuff.

            bodyPrefabs[0].GetComponent<CharacterBody>()._defaultCrosshairPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Crosshair/SimpleDotCrosshair");
            if (Config.customStatsBool)
            {
   
                bodyPrefabs[0].GetComponent<CharacterBody>().baseAcceleration = Config.bAcceleration;
                bodyPrefabs[0].GetComponent<CharacterBody>().baseArmor = Config.bArmor;
                bodyPrefabs[0].GetComponent<CharacterBody>().baseAttackSpeed = Config.bAttackSpeed;
                bodyPrefabs[0].GetComponent<CharacterBody>().baseCrit = Config.bCrit;
                bodyPrefabs[0].GetComponent<CharacterBody>().baseDamage = Config.bDamage;
                bodyPrefabs[0].GetComponent<CharacterBody>().baseJumpPower = Config.bJumpPower;
                bodyPrefabs[0].GetComponent<CharacterBody>().baseMaxHealth = Config.bMaxHealth;
                bodyPrefabs[0].GetComponent<CharacterBody>().baseMaxShield= Config.bMaxShield;
                bodyPrefabs[0].GetComponent<CharacterBody>().baseMoveSpeed= Config.bMoveSpeed;
                bodyPrefabs[0].GetComponent<CharacterBody>().baseRegen = Config.bRegen;
                bodyPrefabs[0].GetComponent<CharacterBody>().levelArmor = Config.lArmor;
                bodyPrefabs[0].GetComponent<CharacterBody>().levelAttackSpeed= Config.lAttackSpeed;
                bodyPrefabs[0].GetComponent<CharacterBody>().levelCrit= Config.lCrit;
                bodyPrefabs[0].GetComponent<CharacterBody>().levelDamage= Config.lDamage;
                bodyPrefabs[0].GetComponent<CharacterBody>().levelJumpPower= Config.lJumpPower;
                bodyPrefabs[0].GetComponent<CharacterBody>().levelMaxHealth= Config.lMaxHealth;
                bodyPrefabs[0].GetComponent<CharacterBody>().levelMaxShield= Config.lMaxShield;
                bodyPrefabs[0].GetComponent<CharacterBody>().levelRegen= Config.lRegen;


            }
            //AddMaterialControllers();
        }

        // Some variables have to be set and reference assets we don't have access to in Thunderkit.
        // So instead we set them here.
        private static void ForEachReferences()
        {
            foreach (GameObject g in bodyPrefabs)
            {
                var cb = g.GetComponent<CharacterBody>();
                cb._defaultCrosshairPrefab = LegacyResourcesAPI.Load<GameObject>("prefabs/crosshair/StandardCrosshair");
                //cb.preferredPodPrefab = LegacyResourcesAPI.Load<GameObject>("prefabs/networkedobjects/SurvivorPod");

                var fs = g.GetComponentInChildren<FootstepHandler>();
                fs.footstepDustPrefab = LegacyResourcesAPI.Load<GameObject>("prefabs/GenericFootstepDust");

                SetupRagdoll(g.GetComponentInChildren<RagdollController>().gameObject);
            }


        }
        // Add Material Controllers to character to edit materials using RoR2 materials in-game.
        private static void AddMaterialControllers()
        {
           // Components.MaterialControllerComponents.HGControllerFinder bodyControllerFinder = bodyPrefabs[0].GetComponent<ModelLocator>().modelTransform.Find("body").gameObject.AddComponent<Components.MaterialControllerComponents.HGControllerFinder>();
           // bodyControllerFinder.Renderer = bodyPrefabs[0].GetComponent<ModelLocator>().modelTransform.Find("body").GetComponent<Renderer>();
           // bodyControllerFinder.Materials = bodyPrefabs[0].GetComponent<ModelLocator>().modelTransform.Find("body").GetComponent<Renderer>().materials;
        }
        // Code from the original henry to setup Ragdolls for you.
        // This is so you dont have to manually set the layers for each object in the bones list.
        private static void SetupRagdoll(GameObject model)
        {
            RagdollController ragdollController = model.GetComponent<RagdollController>();

            if (!ragdollController) return;

            if (ragdollMaterial == null) ragdollMaterial = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").GetComponentInChildren<RagdollController>().bones[1].GetComponent<Collider>().material;

            foreach (Transform i in ragdollController.bones)
            {
                if (i)
                {
                    i.gameObject.layer = LayerIndex.ragdoll.intVal;
                    Collider j = i.GetComponent<Collider>();
                    if (j)
                    {
                        j.material = ragdollMaterial;
                        j.sharedMaterial = ragdollMaterial;
                    }
                }
            }
        }

        // Find all relevant prefabs within the content pack, per SurvivorDefs.
        private static void GetPrefabs() //wack
        {
            var d = Assets.mainContentPack.survivorDefs;
            foreach (SurvivorDef s in d)
            {
                bodyPrefabs.Add(s.bodyPrefab);
                bodyIndexes.Add(BodyCatalog.FindBodyIndex(s.bodyPrefab));
                displayPrefabs.Add(s.displayPrefab);
            }
        }
    }
}
