using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BepInEx.Configuration;
using EmotesAPI;
using RoR2;
namespace SamusMod.Modules
{
public static class EmoteAPICompatibility
    {
        private static bool? _enabled;
        
        public static bool enabled
        {
            get
            {
                if (_enabled == null)
                {
                    _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.weliveinasociety.CustomEmotesAPI");
                }
                return (bool)_enabled;
            }

        }







        public static void SurvivorCatalog_Init(On.RoR2.SurvivorCatalog.orig_Init orig)
        {
            orig();
            foreach (var item in SurvivorCatalog.allSurvivorDefs)
            {
                if (item.bodyPrefab.name == "DGSamusBody")
                {
                    var skele = Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("DGSamusHUM2");
                    CustomEmotesAPI.ImportArmature(Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("DGSamusBody"), skele);
                    Debug.Log("Added emote armature");
                }
            }
        }
    }
}
