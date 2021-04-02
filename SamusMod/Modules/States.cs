using EntityStates;
using EnigmaticThunder.Modules;
using RoR2;
using SamusMod.States;
using UnityEngine;
using System;
using System.Collections.Generic;
namespace SamusMod.Modules
{
    public static class States
    {
        public static List<Type> entitystates = new List<Type>();
        public static void RegisterStates() 
        {
            Loadouts.RegisterEntityState(typeof(SamusMain));
            entitystates.Add(typeof(SamusMain));
            //Debug.Log("added SamusMain state");
            Loadouts.RegisterEntityState(typeof(SpawnState));
            entitystates.Add(typeof(SpawnState));
            //Debug.Log("added Spawn state");
            Loadouts.RegisterEntityState(typeof(ChargeBeam));
            entitystates.Add(typeof(ChargeBeam));
            //Debug.Log("added ChargeBeam state");
            //Debug.Log("adding firebeam state");
            Loadouts.RegisterEntityState(typeof(FireBeam));
            entitystates.Add(typeof(FireBeam));
            //Debug.Log("added FireBeam state");
            //Debug.Log("adding missile state");
            Loadouts.RegisterEntityState(typeof(Missile));
            entitystates.Add(typeof(Missile));
            //Debug.Log("added Missile state");
            Loadouts.RegisterEntityState(typeof(SMissile));
            entitystates.Add(typeof(SMissile));
            Loadouts.RegisterEntityState(typeof(Roll));
            entitystates.Add(typeof(Roll));
            Debug.Log("roll");
            //Loadouts.RegisterEntityState(typeof(morphBallEnter));
            entitystates.Add(typeof(morphBallEnter));
            Debug.Log("morph");
           // Loadouts.RegisterEntityState(typeof(ExitMorphBall));
            entitystates.Add(typeof(ExitMorphBall));
           // Loadouts.RegisterEntityState(typeof(MorphBallBomb));
            entitystates.Add(typeof(MorphBallBomb));


            EntityStateMachine samusStateMachine = Prefabs.samusPrefab.GetComponent<EntityStateMachine>();
            samusStateMachine.mainStateType = new SerializableEntityStateType(typeof(SamusMain));
            samusStateMachine.initialStateType = new SerializableEntityStateType(typeof(SpawnState));
        }
    }
}
