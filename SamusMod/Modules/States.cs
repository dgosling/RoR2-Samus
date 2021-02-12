using EntityStates;
using R2API;
using RoR2;
using SamusMod.States;

namespace SamusMod.Modules
{
    public static class States
    {
        public static void RegisterStates() 
        {
            LoadoutAPI.AddSkill(typeof(SamusMain));
            LoadoutAPI.AddSkill(typeof(SpawnState));
            LoadoutAPI.AddSkill(typeof(FireBeam));
            LoadoutAPI.AddSkill(typeof(ChargeBeam));

            EntityStateMachine samusStateMachine = Prefabs.samusPrefab.GetComponent<EntityStateMachine>();
            samusStateMachine.mainStateType = new SerializableEntityStateType(typeof(SamusMain));
            samusStateMachine.initialStateType = new SerializableEntityStateType(typeof(SpawnState));
        }
    }
}
