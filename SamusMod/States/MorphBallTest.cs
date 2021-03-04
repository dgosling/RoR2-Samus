

namespace SamusMod.States
{
    public class MorphBallTest : BaseMorphBall
    {
        public override void OnEnter()
        {
            this.speedMult = 1.2f;
            base.OnEnter();
        }
    }
}
