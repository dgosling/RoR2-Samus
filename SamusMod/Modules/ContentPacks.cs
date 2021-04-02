using System.Collections.Generic;
using RoR2;
using UnityEngine;
using EnigmaticThunder.Modules;

namespace SamusMod.Modules
{
    internal class ContentPacks
    {
        internal static ContentPack contentPack;

        internal void CreateContentPack()
        {
            contentPack = new ContentPack()
            {
                artifactDefs = new ArtifactDef[0],
                bodyPrefabs = Prefabs.bodyPrefabs.ToArray(),
                buffDefs = new BuffDef[0],
                effectDefs = new EffectDef[0],
                eliteDefs = new EliteDef[0],
                entityStateConfigurations = new EntityStateConfiguration[0],
                entityStateTypes = States.entitystates.ToArray(),
                equipmentDefs = new EquipmentDef[0],
                gameEndingDefs = new GameEndingDef[0],
                gameModePrefabs = new Run[0],
                itemDefs = new ItemDef[0],
                masterPrefabs = Modules.Prefabs.masterPrefabs.ToArray(),
                musicTrackDefs = new MusicTrackDef[0],
                networkedObjectPrefabs = new GameObject[0],
                networkSoundEventDefs = Modules.Assets.networkSoundEventDefs.ToArray(),
                projectilePrefabs = Modules.Prefabs.projectilePrefabs.ToArray(),
                sceneDefs = new SceneDef[0],
                skillDefs = Modules.Skills.skillDefs.ToArray(),
                skillFamilies = Modules.Skills.skillFamilies.ToArray(),
                surfaceDefs = new SurfaceDef[0],
                survivorDefs = Modules.Prefabs.survivorDefinitions.ToArray(),
                unlockableDefs = new UnlockableDef[0]
            };
            On.RoR2.ContentManager.SetContentPacks += AddContent;

            
        }

        private void AddContent(On.RoR2.ContentManager.orig_SetContentPacks orig, List<ContentPack> contentPacks)
        {
            contentPacks.Add(contentPack);
            orig(contentPacks);
        }
    }
}
