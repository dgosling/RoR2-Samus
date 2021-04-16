using System.Collections.Generic;
using RoR2.ContentManagement;
using UnityEngine;
using R2API;

namespace SamusMod.Modules
{
    internal class ContentPacks : IContentPackProvider
    {
        internal ContentPack contentPack = new ContentPack();

        public string identifier => SamusPlugin.MODUID;

        public void Initialize()
        {
            ContentManager.collectContentPackProviders += ContentManager_collectContentPackProviders;
        }

        private void ContentManager_collectContentPackProviders(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(this);
        }

        public System.Collections.IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
        {
            this.contentPack.identifier = this.identifier;
            contentPack.bodyPrefabs.Add(Prefabs.bodyPrefabs.ToArray());
            contentPack.entityStateTypes.Add(States.entitystates.ToArray());
            contentPack.masterPrefabs.Add(Prefabs.masterPrefabs.ToArray());
            contentPack.networkSoundEventDefs.Add(Assets.networkSoundEventDefs.ToArray());
            contentPack.projectilePrefabs.Add(Prefabs.projectilePrefabs.ToArray());
            contentPack.skillDefs.Add(Skills.skillDefs.ToArray());
            contentPack.skillFamilies.Add(Skills.skillFamilies.ToArray());
            contentPack.survivorDefs.Add(Prefabs.survivorDefinitions.ToArray());

            args.ReportProgress(1f);
            yield break;
        }
        public System.Collections.IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
        {
            ContentPack.Copy(this.contentPack, args.output);
            args.ReportProgress(1);
            yield break;
        }
            
        public System.Collections.IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
        {
            args.ReportProgress(1);
            yield break;
        }
            
        }


    }

