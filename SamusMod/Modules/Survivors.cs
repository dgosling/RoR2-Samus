using EnigmaticThunder.Modules;
using RoR2;
using UnityEngine.Networking;
using EntityStates;
using UnityEngine;

namespace SamusMod.Modules
{
    public static class Survivors
    {
        public static void RegisterSurvivors()
        {
            Prefabs.samusDisplayPrefab.AddComponent<NetworkIdentity>();


            SurvivorDef survivor = ScriptableObject.CreateInstance<SurvivorDef>();
            survivor.bodyPrefab = Prefabs.samusPrefab;
            survivor.displayPrefab = Prefabs.samusDisplayPrefab;
            survivor.primaryColor = SamusPlugin.characterColor;
            survivor.displayNameToken = "DG_SAMUS_NAME";
            survivor.descriptionToken = "DG_SAMUS_DESCRIPTION";
            survivor.outroFlavorToken = "DG_SAMUS_OUTRO_FLAVOR";
            survivor.mainEndingEscapeFailureFlavorToken = "DG_SAMUS_OUTRO_FAILURE";
            survivor.desiredSortPosition = 50f;
            


            Loadouts.RegisterSurvivorDef(survivor);
        }
    }
}
