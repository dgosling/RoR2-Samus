using SamusMod.States;
namespace SamusMod.States
{
    public class FireBeam : BaseFireBeam
    {
        public override void OnEnter()
        {
            this.baseDuration = .5f;

            this.force = 5f;
            this.maxDamageCoefficient = StaticValues.cshootDamageCoefficient;
            this.minDamageCoefficient = StaticValues.shootDamageCoefficient;
            //this.muzzleflashEffectPrefab
            this.projectilePrefab = Modules.Projectiles.beam;
            if (ChargeBeamBase.size.x == 1)
            {
                this.speed = StaticValues.cbeamSpeed;
            }
            else
            {
                this.speed = StaticValues.beamSpeed;
            }
            base.OnEnter();
        }

       
    }
}
