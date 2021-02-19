using SamusMod.States;

namespace SamusMod.States
{
    public class SMissile : BaseMissile

    {
       // private int secStock;

        public override void OnEnter()
        {
            this.damageCoef = StaticValues.smissileDamageCoefficient;
            this.baseDuration = .85f;
            this.recoil = .5f;
            this.projectilePrefab = SamusMod.Modules.Projectiles.smissile;

            
           // base.skillLocator.special.RecalculateMaxStock();
            
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
