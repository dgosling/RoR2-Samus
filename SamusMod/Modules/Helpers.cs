using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using EntityStates;

namespace SamusMod.Modules
{
   public static class Helpers
    {
        public static void CreateHitbox(GameObject prefab,Transform hitboxTransform,string hitboxName)
        {
            HitBoxGroup hitBoxGroup = prefab.AddComponent<HitBoxGroup>();

            HitBox hitBox = hitboxTransform.gameObject.AddComponent<HitBox>();
            hitboxTransform.gameObject.layer = LayerIndex.projectile.intVal;

            hitBoxGroup.hitBoxes = new HitBox[]
            {
                hitBox
            };

            hitBoxGroup.groupName = hitboxName;
        }
    }

    internal class SkillDefInfo
    {
        public string skillName;
        public string skillNameToken;
        public string skillDescriptionToken;
        public Sprite skillIcon;
        public SerializableEntityStateType activationState;
        public string activationStateMachineName;
        public int baseMaxStock;
        public float baseRechargeInterval;
        public bool beginSkillCooldownOnSkillEnd;
        public bool canceledFromSprinting;
        public bool forceSprintDuringState;
        public bool fullRestockOnAssign;
        public InterruptPriority interruptPriority;
        public bool isBullets;
        public bool isCombatSkill;
        public bool mustKeyPress;
        public bool noSprint;
        public int rechargeStock;
        public int requiredStock;
        public float shootDelay;
        public int stockToConsume;
        public string[] keywordTokens;
    }
}
