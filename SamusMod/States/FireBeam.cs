using SamusMod.States;
using UnityEngine;
namespace SamusMod.States
{
    public class FireBeam : BaseFireBeam
    {
        public Vector3 sizes;
        





        public override void OnEnter()
        {
            this.baseDuration = .8f;

            this.force = 5f;
            this.maxDamageCoefficient = StaticValues.cshootDamageCoefficient;
            this.minDamageCoefficient = StaticValues.shootDamageCoefficient;
            this.selfForce = 0f;
            //this.muzzleflashEffectPrefab
            this.projectilePrefab = Modules.Projectiles.beam;
            this.speed = 200f;
            
            base.OnEnter();
        }

       
    }
}
