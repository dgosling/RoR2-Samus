using SamusMod.SkillStates.BaseStates;
using UnityEngine;
namespace SamusMod.SkillStates.Samus
{


    public class trackingMissile : BaseMissile
    {
        protected Misc.SamusTracker SamusTracker;

        public override void OnEnter()
        {
            this.SamusTracker = gameObject.GetComponent<Misc.SamusTracker>();
            this.target = SamusTracker.GetTrackingTarget();
            //this.projectilePrefab = Modules.Projectiles.altmissile;
            this.baseDuration = .1f;
            this.damageCoef = Modules.StaticValues.missileDamageCoefficient;
            this.recoil = .5f;

            this.Sound = SamusMod.Modules.Sounds.missileSound;

            base.OnEnter();
        }
    }
}
