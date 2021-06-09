using System;
using System.Collections.Generic;
using System.Text;

namespace SamusMod.States
{
    class trackingMissile : BaseMissile
    {
        protected Misc.SamusTracker SamusTracker;

        public override void OnEnter()
        {
            this.SamusTracker = gameObject.GetComponent<Misc.SamusTracker>();
            this.target = SamusTracker.GetTrackingTarget();
            this.projectilePrefab = Modules.Projectiles.altmissile;
            this.baseDuration = .85f;
            this.damageCoef = StaticValues.missileDamageCoefficient;
            this.recoil = .5f;

            this.Sound = SamusMod.Modules.Sounds.missileSound;

            base.OnEnter();
        }
    }
}
