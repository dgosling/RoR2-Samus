﻿using SamusMod.SkillStates.BaseStates;
using UnityEngine;
namespace SamusMod.SkillStates.Samus
{


    public class SMissile : BaseMissile
    {

        // Start is called before the first frame update
        public override void OnEnter()
        {
            this.damageCoef = Modules.StaticValues.smissileDamageCoefficient;
            this.baseDuration = .1f;
            this.recoil = .5f;
            //this.projectilePrefab = SamusMod.Modules.Projectiles.smissile;
            
            this.Sound = SamusMod.Modules.Sounds.sMissileSound;
            // base.skillLocator.special.RecalculateMaxStock();
            this.sMissile = true;
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.skillLocator.secondary.DeductStock(5);
            base.skillLocator.secondary.RecalculateMaxStock();
            //base.skillLocator.special.DeductStock(1);
            //this.calculateSMissiles();
            base.OnExit();
        }
    }
}
