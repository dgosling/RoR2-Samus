using R2API;
using RoR2;
using UnityEngine.Networking;

namespace SamusMod.Modules
{
    public static class Survivors
    {
        public static void RegisterSurvivors()
        {
            Prefabs.samusDisplayPrefab.AddComponent<NetworkIdentity>();

            SurvivorDef survivorDef = new SurvivorDef
            {
                name = "SAMUS_NAME",
                descriptionToken = "SAMUS_DESCRIPTION",
                primaryColor = SamusPlugin.characterColor,
                bodyPrefab = Prefabs.samusPrefab,
                displayPrefab = Prefabs.samusDisplayPrefab,
                outroFlavorToken = "SAMUS_OUTRO_FLAVOR"
            };
            
            SurvivorAPI.AddSurvivor(survivorDef);
        }
    }
}
