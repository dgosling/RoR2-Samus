using EntityStates;
using R2API;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SamusMod.Modules
{
    public static class Skills
    {
        private static SkillLocator SkillLocator;
        internal static SkillDef morphBallExit,morphBallBomb,morphBallPowerBomb;
        

        public static void SetupSkills(GameObject bodyPrefab)
        {
            foreach (GenericSkill obj in bodyPrefab.GetComponentsInChildren<GenericSkill>())
            {
                SamusPlugin.DestroyImmediate(obj);
            }

            SkillLocator = bodyPrefab.GetComponent<SkillLocator>();

            PassiveSetup();
            Debug.Log("Setup passive skills");
            PrimarySetup(bodyPrefab);
           Debug.Log("setup primary skills");
            SecondarySetup(bodyPrefab);
           Debug.Log("Setup secondary skills");
            morphBallExit = CreateSkillDef(new SkillDefInfo()
            {
                skillName = "DG_SAMUS_UTILITY_MORPH_EXIT_NAME",
                skillNameToken = "DG_SAMUS_UTILITY_MORPH_EXIT_NAME",
                skillDescriptionToken = "DG_SAMUS_UTILITY_MORPH_EXIT_DESCRIPTION",
                skillIcon = Assets.icon3,
                activationState = new SerializableEntityStateType(typeof(SamusMod.States.ExitMorphBall)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = InterruptPriority.Any,
                isBullets = false,
                isCombatSkill = false,
                mustKeyPress = true,
                noSprint = true,
                rechargeStock = 1,
                requiredStock = 1,
                shootDelay = 0f,
                stockToConsume = 1

            }) ;
            morphBallBomb = CreateSkillDef(new SkillDefInfo()
            {
                skillName = "DG_SAMUS_PRIMARY_MORPH_BOMB_NAME",
                skillNameToken = "DG_SAMUS_PRIMARY_MORPH_BOMB_NAME",
                skillDescriptionToken = "DG_SAMUS_PRIMARY_MORPH_BOMB_DESCRIPTION",
                skillIcon = Assets.icon3,
                activationState = new SerializableEntityStateType(typeof(SamusMod.States.MorphBallBomb)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 3,
                baseRechargeInterval = 3,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                fullRestockOnAssign = true,
                interruptPriority = InterruptPriority.PrioritySkill,
                isBullets = false,
                isCombatSkill = true,
                mustKeyPress = true,
                noSprint = true,
                rechargeStock = 1,
                requiredStock = 1,
                shootDelay = .4f,
                stockToConsume = 1
            });
            //morphBallPowerBomb = CreateSkillDef(new SkillDefInfo()
            //{

            //});
            UtilitySetup(bodyPrefab);

            Debug.Log("utility");
            SpecialSetup(bodyPrefab);
            Debug.Log("special");
        }
        
        private static void PassiveSetup()
        {
            SkillLocator.passiveSkill.enabled = true;
            SkillLocator.passiveSkill.skillNameToken = "DG_SAMUS_PASSIVE_NAME";
            SkillLocator.passiveSkill.skillDescriptionToken = "DG_SAMUS_PASSIVE_DESCRIPTION";
            SkillLocator.passiveSkill.skillDescriptionToken = "DG_SAMUS_PASSIVE_DESCRIPTION";
            SkillLocator.passiveSkill.icon = Assets.iconP;
        }
        internal static SkillDef CreateSkillDef(SkillDefInfo skillDefInfo)
        {
            SkillDef instance = ScriptableObject.CreateInstance<SkillDef>();
            instance.skillName = skillDefInfo.skillName;
            instance.skillNameToken = skillDefInfo.skillNameToken;
            instance.skillDescriptionToken = skillDefInfo.skillDescriptionToken;
            instance.icon = skillDefInfo.skillIcon;
            instance.activationState = skillDefInfo.activationState;
            instance.activationStateMachineName = skillDefInfo.activationStateMachineName;
            instance.baseMaxStock = skillDefInfo.baseMaxStock;
            instance.baseRechargeInterval = skillDefInfo.baseRechargeInterval;
            instance.beginSkillCooldownOnSkillEnd = skillDefInfo.beginSkillCooldownOnSkillEnd;
            instance.canceledFromSprinting = skillDefInfo.canceledFromSprinting;
            instance.forceSprintDuringState = skillDefInfo.forceSprintDuringState;
            instance.fullRestockOnAssign = skillDefInfo.fullRestockOnAssign;
            instance.interruptPriority = skillDefInfo.interruptPriority;
            instance.isBullets = skillDefInfo.isBullets;
            instance.isCombatSkill = skillDefInfo.isCombatSkill;
            instance.mustKeyPress = skillDefInfo.mustKeyPress;
            instance.noSprint = skillDefInfo.noSprint;
            instance.rechargeStock = skillDefInfo.rechargeStock;
            instance.requiredStock = skillDefInfo.requiredStock;
            instance.shootDelay = skillDefInfo.shootDelay;
            instance.stockToConsume = skillDefInfo.stockToConsume;
            instance.keywordTokens = skillDefInfo.keywordTokens;
            LoadoutAPI.AddSkillDef(instance);
            return instance;
        }
        private static void PrimarySetup(GameObject bodyPrefab)
        {
            SkillDef mySkillDef = ScriptableObject.CreateInstance<SkillDef>();
            mySkillDef.activationState = new SerializableEntityStateType(typeof(SamusMod.States.ChargeBeam));
            mySkillDef.activationStateMachineName = "Weapon";
            mySkillDef.baseMaxStock = 1;
            mySkillDef.baseRechargeInterval = 0f;
            mySkillDef.beginSkillCooldownOnSkillEnd = false;
            mySkillDef.canceledFromSprinting = true;
            mySkillDef.fullRestockOnAssign = true;
            mySkillDef.interruptPriority = InterruptPriority.Skill;
            mySkillDef.isBullets = false;
            mySkillDef.isCombatSkill = true;
            mySkillDef.mustKeyPress = false;
            mySkillDef.noSprint = true;
            mySkillDef.rechargeStock = 1;
            mySkillDef.requiredStock = 1;
            mySkillDef.shootDelay = .25f;
            mySkillDef.stockToConsume = 1;
            mySkillDef.icon = Assets.icon1;
            mySkillDef.skillDescriptionToken = "DG_SAMUS_PRIMARY_BEAM_DESCRIPTION";
            mySkillDef.skillName = "DG_SAMUS_PRIMARY_BEAM_NAME";
            mySkillDef.skillNameToken = "DG_SAMUS_PRIMARY_BEAM_NAME";
            mySkillDef.keywordTokens = new string[]
            {
                "KEYWORD_AGILE"
            };

            LoadoutAPI.AddSkillDef(mySkillDef);

            SkillLocator.primary = bodyPrefab.AddComponent<GenericSkill>();
            SkillFamily newFamily = ScriptableObject.CreateInstance<SkillFamily>();
            newFamily.variants = new SkillFamily.Variant[1];
            
            LoadoutAPI.AddSkillFamily(newFamily);
            SkillLocator.primary._skillFamily = newFamily;
            SkillFamily skillFamily = SkillLocator.primary.skillFamily;
            
            skillFamily.variants[0] = new SkillFamily.Variant
            {
                skillDef = mySkillDef,
                unlockableName = "",
                viewableNode = new ViewablesCatalog.Node(mySkillDef.skillNameToken, false, null)
            };
        }

        private static void SecondarySetup(GameObject bodyPrefab)
        {
            SkillDef skillDef = ScriptableObject.CreateInstance<SkillDef>();
            skillDef.activationState = new SerializableEntityStateType(typeof(SamusMod.States.Missile));
            skillDef.activationStateMachineName = "Weapon";
            skillDef.baseMaxStock = 5;
            skillDef.baseRechargeInterval = 5f;
            skillDef.beginSkillCooldownOnSkillEnd = false;
            skillDef.canceledFromSprinting = false;
            skillDef.fullRestockOnAssign = true;
            skillDef.interruptPriority = InterruptPriority.Any;
            skillDef.isBullets = false;
            skillDef.isCombatSkill = true;
            skillDef.mustKeyPress = false;
            skillDef.noSprint = false;
            skillDef.rechargeStock = 1;
            skillDef.requiredStock = 1;
            skillDef.shootDelay = .5f;
            skillDef.stockToConsume = 1;
            skillDef.icon = Assets.icon2;
            skillDef.skillDescriptionToken = "DG_SAMUS_SECONDARY_MISSILE_DESCRIPTION";
            skillDef.skillName = "DG_SAMUS_SECONDARY_MISSILE_NAME";
            skillDef.skillNameToken = "DG_SAMUS_SECONDARY_MISSILE_NAME";
            skillDef.keywordTokens = new string[]
            {
                "KEYWORD_AGILE"
            };

            LoadoutAPI.AddSkillDef(skillDef);

            SkillLocator.secondary = bodyPrefab.AddComponent<GenericSkill>();
            SkillFamily newFamily = ScriptableObject.CreateInstance<SkillFamily>();
            newFamily.variants = new SkillFamily.Variant[1];
            LoadoutAPI.AddSkillFamily(newFamily);
            SkillLocator.secondary._skillFamily = newFamily;
            SkillFamily skillFamily = SkillLocator.secondary.skillFamily;

            skillFamily.variants[0] = new SkillFamily.Variant
            {
                skillDef = skillDef,
                unlockableName = "",
                viewableNode = new ViewablesCatalog.Node(skillDef.skillNameToken, false, null)
            };
                
        }

        private static void UtilitySetup(GameObject bodyPrefab)
        {
            SkillDef mySkillDef = ScriptableObject.CreateInstance<SkillDef>();
            mySkillDef.activationState = new SerializableEntityStateType(typeof(SamusMod.States.Roll));
            mySkillDef.activationStateMachineName = "Weapon";
            mySkillDef.baseMaxStock = 1;
            mySkillDef.baseRechargeInterval = 5;
            mySkillDef.beginSkillCooldownOnSkillEnd = false;
            mySkillDef.canceledFromSprinting = false;
            mySkillDef.fullRestockOnAssign = true;
            mySkillDef.interruptPriority = InterruptPriority.Skill;
            mySkillDef.isBullets = false;
            mySkillDef.isCombatSkill = true;
            mySkillDef.mustKeyPress = true;
            mySkillDef.noSprint = true;
            mySkillDef.rechargeStock = 1;
            mySkillDef.requiredStock = 1;
            mySkillDef.shootDelay = 1;
            mySkillDef.stockToConsume = 1;
            mySkillDef.icon = Assets.icon3;
            mySkillDef.skillDescriptionToken = "DG_SAMUS_UTILITY_DASH_DESCRIPTION";
            mySkillDef.skillName = "DG_SAMUS_UTILITY_DASH_NAME";
            mySkillDef.skillNameToken = "DG_SAMUS_UTILITY_DASH_NAME";

            LoadoutAPI.AddSkillDef(mySkillDef);

            SkillLocator.utility = bodyPrefab.AddComponent<GenericSkill>();
            SkillFamily newFamily = ScriptableObject.CreateInstance<SkillFamily>();
            newFamily.variants = new SkillFamily.Variant[1];
            LoadoutAPI.AddSkillFamily(newFamily);
            SkillLocator.utility._skillFamily = newFamily;
            SkillFamily skillFamily = SkillLocator.utility.skillFamily;

            skillFamily.variants[0] = new SkillFamily.Variant
            {
                skillDef = mySkillDef,
                unlockableName = "",
                viewableNode = new ViewablesCatalog.Node(mySkillDef.skillNameToken, false, null)
            };

            mySkillDef = ScriptableObject.CreateInstance<SkillDef>();
            mySkillDef.activationState = new SerializableEntityStateType(typeof(SamusMod.States.morphBallEnter));
            mySkillDef.activationStateMachineName = "Weapon";
            mySkillDef.baseMaxStock = 1;
            mySkillDef.baseRechargeInterval = 0f;
            mySkillDef.beginSkillCooldownOnSkillEnd = false;
            mySkillDef.canceledFromSprinting = false;
            mySkillDef.fullRestockOnAssign = true;
            mySkillDef.interruptPriority = InterruptPriority.PrioritySkill;
            mySkillDef.isBullets = false;
            mySkillDef.isCombatSkill = false;
            mySkillDef.mustKeyPress = true;
            mySkillDef.noSprint = true;
            mySkillDef.rechargeStock = 1;
            mySkillDef.requiredStock = 1;
            mySkillDef.shootDelay = 0;
            mySkillDef.stockToConsume = 1;
            mySkillDef.icon = Assets.icon3;
            mySkillDef.skillDescriptionToken = "DG_SAMUS_UTILITY_MORPH_DESCRIPTION";
            mySkillDef.skillName = "DG_SAMUS_UTILITY_MORPH_NAME";
            mySkillDef.skillNameToken = "DG_SAMUS_UTILITY_MORPH_NAME";
            mySkillDef.keywordTokens = new string[]
            {
                "KEYWORD_AGILE"
            };
            LoadoutAPI.AddSkillDef(mySkillDef);

            Array.Resize(ref skillFamily.variants, skillFamily.variants.Length + 1);
            skillFamily.variants[skillFamily.variants.Length - 1] = new SkillFamily.Variant
            {
                skillDef = mySkillDef,
                unlockableName = "",
                viewableNode = new ViewablesCatalog.Node(mySkillDef.skillNameToken, false, null)
            };

        }

        private static void SpecialSetup(GameObject bodyPrefab)
        {
            SkillDef mySkillDef = ScriptableObject.CreateInstance<SkillDef>();
            mySkillDef.activationState = new SerializableEntityStateType(typeof(SamusMod.States.SMissile));
            mySkillDef.activationStateMachineName = "Weapon";
            mySkillDef.baseMaxStock = 1;
            mySkillDef.baseRechargeInterval = 0;
            mySkillDef.beginSkillCooldownOnSkillEnd = false;
            mySkillDef.canceledFromSprinting = false;
            mySkillDef.fullRestockOnAssign = true;
            mySkillDef.interruptPriority = InterruptPriority.Skill;
            mySkillDef.isBullets = false;
            mySkillDef.isCombatSkill = true;
            mySkillDef.mustKeyPress = true;
            mySkillDef.noSprint = false;
            mySkillDef.rechargeStock = 0;
            mySkillDef.requiredStock = 1;
            mySkillDef.shootDelay = 1f;
            mySkillDef.stockToConsume = 1;
            mySkillDef.icon = Assets.icon4;
            mySkillDef.skillDescriptionToken = "DG_SAMUS_SPECIAL_SMISSILE_DESCRIPTION";
            mySkillDef.skillName = "DG_SAMUS_SPECIAL_SMISSILE_NAME";
            mySkillDef.skillNameToken = "DG_SAMUS_SPECIAL_SMISSILE_NAME";

            LoadoutAPI.AddSkillDef(mySkillDef);

            SkillLocator.special = bodyPrefab.AddComponent<GenericSkill>();
            SkillFamily newFamily = ScriptableObject.CreateInstance<SkillFamily>();
            newFamily.variants = new SkillFamily.Variant[1];
            LoadoutAPI.AddSkillFamily(newFamily);
            SkillLocator.special._skillFamily = newFamily;
            SkillFamily skillFamily = SkillLocator.special.skillFamily;

            skillFamily.variants[0] = new SkillFamily.Variant
            {
                skillDef = mySkillDef,
                unlockableName = "",
                viewableNode = new ViewablesCatalog.Node(mySkillDef.skillNameToken, false, null)
            };

        }

        
    }
}
