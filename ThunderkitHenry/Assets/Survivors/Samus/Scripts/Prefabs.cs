using RoR2;
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

                SetupRagdoll(g);
            }
        }
        // Add Material Controllers to character to edit materials using RoR2 materials in-game.
        private static void AddMaterialControllers()
        {
            Components.MaterialControllerComponents.HGControllerFinder bodyControllerFinder = bodyPrefabs[0].GetComponent<ModelLocator>().modelTransform.Find("body").gameObject.AddComponent<Components.MaterialControllerComponents.HGControllerFinder>();
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
            var d = Assets.contentPack.survivorDefs;
            foreach (SurvivorDef s in d)
            {
                bodyPrefabs.Add(s.bodyPrefab);
                bodyIndexes.Add(BodyCatalog.FindBodyIndex(s.bodyPrefab));
                displayPrefabs.Add(s.displayPrefab);
            }
        }
    }
}
