using EntityStates;
using R2API;
using RoR2;
using SamusMod.States;
using UnityEngine;
using System;
using System.Collections.Generic;
using VRAPI;
namespace SamusMod.Modules
{
    public static class States
    {
        public static List<Type> entitystates = new List<Type>();
        
        public static void RegisterStates() 
        {
            LoadoutAPI.AddSkill(typeof(SamusMain));
            entitystates.Add(typeof(SamusMain));
            //Debug.Log("added SamusMain state");
            LoadoutAPI.AddSkill(typeof(SpawnState));
            entitystates.Add(typeof(SpawnState));
            //Debug.Log("added Spawn state");
            LoadoutAPI.AddSkill(typeof(ChargeBeam));
            entitystates.Add(typeof(ChargeBeam));
            //Debug.Log("added ChargeBeam state");
            //Debug.Log("adding firebeam state");
            LoadoutAPI.AddSkill(typeof(FireBeam));
            entitystates.Add(typeof(FireBeam));
            //Debug.Log("added FireBeam state");
            //Debug.Log("adding missile state");
            LoadoutAPI.AddSkill(typeof(Missile));
            entitystates.Add(typeof(Missile));
            //Debug.Log("added Missile state");
            LoadoutAPI.AddSkill(typeof(SMissile));
            entitystates.Add(typeof(SMissile));
            LoadoutAPI.AddSkill(typeof(Roll));
            entitystates.Add(typeof(Roll));
            //Debug.Log("roll");
            LoadoutAPI.AddSkill(typeof(morphBallEnter));
            entitystates.Add(typeof(morphBallEnter));
            //Debug.Log("morph");
            LoadoutAPI.AddSkill(typeof(ExitMorphBall));
            entitystates.Add(typeof(ExitMorphBall));
            LoadoutAPI.AddSkill(typeof(MorphBallBomb));
            entitystates.Add(typeof(MorphBallBomb));
            LoadoutAPI.AddSkill(typeof(MorphBallPBomb));
            entitystates.Add(typeof(trackingMissile));
            LoadoutAPI.AddSkill(typeof(trackingMissile));
            if(VR.enabled)
                VR.AddVignetteState(typeof(Roll));
            EntityStateMachine samusStateMachine = Prefabs.samusPrefab.GetComponent<EntityStateMachine>();
            samusStateMachine.mainStateType = new SerializableEntityStateType(typeof(SamusMain));
            samusStateMachine.initialStateType = new SerializableEntityStateType(typeof(SpawnState));
        }
    }
}
