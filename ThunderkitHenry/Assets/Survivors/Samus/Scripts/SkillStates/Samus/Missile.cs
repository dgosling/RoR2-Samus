using SamusMod.SkillStates.BaseStates;
namespace SamusMod.SkillStates.Samus
{


    public class Missile : BaseMissile
    {
        public override void OnEnter()
        {

            this.baseDuration = .1f;
            this.damageCoef = Modules.StaticValues.missileDamageCoefficient;
            this.recoil = .5f;
            smissleObject = null;
            this.Sound = SamusMod.Modules.Sounds.missileSound;
            this.sMissile = false;


            //this.projectilePrefab = Modules.Projectiles.missile;
            base.OnEnter();

        }
    }
}
