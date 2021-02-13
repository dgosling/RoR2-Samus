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

        public static void SetupSkills(GameObject bodyPrefab)
        {
            foreach (GenericSkill obj in bodyPrefab.GetComponentsInChildren<GenericSkill>())
            {
                SamusPlugin.DestroyImmediate(obj);
            }

            SkillLocator = bodyPrefab.GetComponent<SkillLocator>();

            PassiveSetup();
            PrimarySetup(bodyPrefab);
            //SecondarySetup(bodyPrefab);
            //UtilitySetup(bodyPrefab);
            //SpecialSetup(bodyPrefab);
        }

        private static void PassiveSetup()
        {
            SkillLocator.passiveSkill.enabled = true;
            SkillLocator.passiveSkill.skillNameToken = "SAMUS_PASSIVE_NAME";
            SkillLocator.passiveSkill.skillDescriptionToken = "SAMUS_PASSIVE_DESCRIPTION";
            SkillLocator.passiveSkill.skillDescriptionToken = "SAMUS_PASSIVE_DESCRIPTION";
            SkillLocator.passiveSkill.icon = Assets.iconP;
        }

        private static void PrimarySetup(GameObject bodyPrefab)
        {
            SkillDef mySkillDef = ScriptableObject.CreateInstance<SkillDef>();
            mySkillDef.activationState = new SerializableEntityStateType(typeof(SamusMod.States.ChargeBeam));
            mySkillDef.activationStateMachineName = "Weapon";
            mySkillDef.baseMaxStock = 1;
            mySkillDef.baseRechargeInterval = 0f;
            mySkillDef.beginSkillCooldownOnSkillEnd = false;
            mySkillDef.canceledFromSprinting = false;
            mySkillDef.fullRestockOnAssign = true;
            mySkillDef.interruptPriority = InterruptPriority.Skill;
            mySkillDef.isBullets = false;
            mySkillDef.isCombatSkill = true;
            mySkillDef.mustKeyPress = false;
            mySkillDef.noSprint = false;
            mySkillDef.rechargeStock = 1;
            mySkillDef.requiredStock = 1;
            mySkillDef.shootDelay = .01f;
            mySkillDef.stockToConsume = 1;
            mySkillDef.icon = Assets.icon1;
            mySkillDef.skillDescriptionToken = "SAMUS_PRIMARY_BEAM_DESCRIPTION";
            mySkillDef.skillName = "SAMUS_PRIMARY_BEAM_NAME";
            mySkillDef.skillNameToken = "SAMUS_PRIMARY_BEAM_NAME";
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
    }
}
