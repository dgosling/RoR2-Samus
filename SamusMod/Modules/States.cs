using EntityStates;
using R2API;
using RoR2;
using SamusMod.States;
using UnityEngine;
namespace SamusMod.Modules
{
    public static class States
    {
        public static void RegisterStates() 
        {
            LoadoutAPI.AddSkill(typeof(SamusMain));
            Debug.Log("added SamusMain state");
            LoadoutAPI.AddSkill(typeof(SpawnState));
            Debug.Log("added Spawn state");
            LoadoutAPI.AddSkill(typeof(ChargeBeam));
            Debug.Log("added ChargeBeam state");
            Debug.Log("adding firebeam state");
            LoadoutAPI.AddSkill(typeof(FireBeam));
            Debug.Log("added FireBeam state");
            Debug.Log("adding missile state");
            LoadoutAPI.AddSkill(typeof(Missile));
            Debug.Log("added Missile state");
            LoadoutAPI.AddSkill(typeof(SMissile));
            LoadoutAPI.AddSkill(typeof(Roll));


            EntityStateMachine samusStateMachine = Prefabs.samusPrefab.GetComponent<EntityStateMachine>();
            samusStateMachine.mainStateType = new SerializableEntityStateType(typeof(SamusMain));
            samusStateMachine.initialStateType = new SerializableEntityStateType(typeof(SpawnState));
        }
    }
}
