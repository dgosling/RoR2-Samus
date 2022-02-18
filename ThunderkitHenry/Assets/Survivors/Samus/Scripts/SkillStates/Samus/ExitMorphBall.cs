using UnityEngine;
using RoR2;
using RoR2.Skills;
using EntityStates;
using UnityEngine.Networking;
using VRAPI;
namespace SamusMod.SkillStates.Samus
{



    public class ExitMorphBall : BaseSkillState
    {

        private static float baseDuration = .42f;
        private float duration;
        private ChildLocator childLocator;
        private GameObject ball;
        [SerializeField]
        public SkillDef bomb;
        [SerializeField]
        public SkillDef exitMorph;
        [SerializeField]
        public SkillDef powerBomb;
        private static float recharge;
        private static int pstock;

        public static float BaseDuration { get => baseDuration; set => baseDuration = value; }
        public static float Recharge { get => recharge; set => recharge = value; }
        public static int Pstock { get => pstock; set => pstock = value; }

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = BaseDuration / this.attackSpeedStat;
            this.childLocator = base.GetModelChildLocator();
            this.ball = this.childLocator.FindChild("Ball2").gameObject;
            if (NetworkServer.active)
            {
                this.characterBody.RemoveBuff(RoR2Content.Buffs.ArmorBoost);
            }
            //this.characterBody.gameObject.GetComponent<Collider>().enabled = true;
            Collider collider = this.ball.GetComponent<Collider>();
            Rigidbody rigidbody = this.ball.GetComponent<Rigidbody>();
            collider.enabled = false;
            //rigidbody.isKinematic = false;
            //rigidbody.interpolation = RigidbodyInterpolation.None;
            //rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            this.childLocator.FindChild("Ball2").gameObject.SetActive(false);
            // this.childLocator.FindChild("armature").gameObject.SetActive(true);
            this.childLocator.FindChild("Body").gameObject.SetActive(true);
            base.PlayAnimation("Body", "transformOut", "Roll.playbackRate", this.duration);
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.fixedAge < this.duration || !this.isAuthority)
                return;
            this.outer.SetNextStateToMain();

        }
        public override void OnExit()
        {
            base.OnExit();
            this.characterBody.baseMoveSpeed = morphBallEnter.NormalSpeed;
            this.characterBody.baseJumpCount = morphBallEnter.NormalJumps;
            this.characterBody.sprintingSpeedMultiplier = morphBallEnter.NormalSprint;
            this.characterBody.RecalculateStats();
            Recharge = this.skillLocator.secondary.rechargeStopwatch;
            Pstock = this.skillLocator.secondary.stock;
            this.skillLocator.primary.UnsetSkillOverride(this.skillLocator.primary, bomb, GenericSkill.SkillOverridePriority.Contextual);
            this.skillLocator.utility.UnsetSkillOverride(this.skillLocator.utility, exitMorph, GenericSkill.SkillOverridePriority.Contextual);
            this.skillLocator.secondary.UnsetSkillOverride(this.skillLocator.secondary, powerBomb, GenericSkill.SkillOverridePriority.Contextual);
            this.skillLocator.secondary.stock = morphBallEnter.Stock1;
            //this.skillLocator.secondary.RecalculateMaxStock();
            this.skillLocator.special.stock = morphBallEnter.Stock2;
            if (Utils.IsUsingMotionControls(this.characterBody))
            {


                foreach (SkinnedMeshRenderer renderer in morphBallEnter.DsMR)
                {
                    renderer.enabled = true;
                }
                foreach (SkinnedMeshRenderer rend in morphBallEnter.NDsMR)
                {
                    rend.enabled = true;
                }
                morphBallEnter.VRCamera.localPosition = morphBallEnter.CameraPOS;


            }
            //this.skillLocator.special.RecalculateMaxStock();
            BaseStates.BaseSamus.morphBall = false;
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}
