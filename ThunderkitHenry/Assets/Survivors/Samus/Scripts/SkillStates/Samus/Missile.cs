using SamusMod.SkillStates.BaseStates;
namespace SamusMod.SkillStates.Samus
{


    public class Missile : BaseMissile
    {
        public override void OnEnter()
        {

            this.baseDuration = .85f;
            this.damageCoef = Modules.StaticValues.missileDamageCoefficient;
            this.recoil = .5f;

            this.Sound = SamusMod.Modules.Sounds.missileSound;



            //this.projectilePrefab = Modules.Projectiles.missile;
            base.OnEnter();

        }
    }
}
