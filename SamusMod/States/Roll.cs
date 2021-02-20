using SamusMod.Modules;

namespace SamusMod.States
{
    class Roll : BaseRoll
    {
        public override void OnEnter()
        {
            this.duration = .9f;
            this.projectilePrefab = Projectiles.bomb;
            this.dodgeFOV = 110;
            base.OnEnter();
        }
    }
}
