using SamusMod.States;

namespace SamusMod.States
{
    public class Missile : BaseMissile
    {
        
        public override void OnEnter()
        {
            
            this.baseDuration = .85f;
            this.damageCoef = StaticValues.missileDamageCoefficient;
            this.recoil = .5f;
            
            this.Sound = SamusMod.Modules.Sounds.missileSound;
            

            
                this.projectilePrefab = SamusMod.Modules.Projectiles.missile;
            base.OnEnter();
            
        }
    }
}