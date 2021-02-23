using SamusMod.Modules;

namespace SamusMod.States
{
    class Roll : BaseRoll
    {
        public override void OnEnter()
        {
            this.duration = .8f;
            this.projectilePrefab = Projectiles.bomb;
            this.dodgeFOV = 110;
            this.damageCoefficient = StaticValues.dashDamageCoefficient;
            this.bombSoundString = SamusMod.Modules.Sounds.morphBomb;
            this.dodgeSoundString = SamusMod.Modules.Sounds.rollSound;
            base.OnEnter();
        }
    }
}
