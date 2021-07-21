using UnityEngine;
using RoR2;
using EntityStates;
using UnityEngine.Networking;
using VRAPI;
namespace SamusMod.States
{
     public class ExitMorphBall : BaseSkillState
    {
        public static float baseDuration = .42f;
        private float duration;
        private ChildLocator childLocator;
        private GameObject ball;
        public static float recharge;
        public static int pstock;
        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = baseDuration / this.attackSpeedStat;
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
            this.characterBody.baseMoveSpeed = morphBallEnter.normalSpeed;
            this.characterBody.baseJumpCount = morphBallEnter.normalJumps;
            this.characterBody.sprintingSpeedMultiplier = morphBallEnter.normalSprint;
            this.characterBody.RecalculateStats();
            recharge = this.skillLocator.secondary.rechargeStopwatch;
            pstock = this.skillLocator.secondary.stock;
            this.skillLocator.primary.UnsetSkillOverride(this.skillLocator.primary, morphBallEnter.bomb, GenericSkill.SkillOverridePriority.Contextual);
            this.skillLocator.utility.UnsetSkillOverride(this.skillLocator.utility, morphBallEnter.exitMorph, GenericSkill.SkillOverridePriority.Contextual);
            this.skillLocator.secondary.UnsetSkillOverride(this.skillLocator.secondary, morphBallEnter.powerBomb, GenericSkill.SkillOverridePriority.Contextual);
            this.skillLocator.secondary.stock = morphBallEnter.stock1;
            //this.skillLocator.secondary.RecalculateMaxStock();
            this.skillLocator.special.stock = morphBallEnter.stock2;
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
                morphBallEnter.VRCamera.localPosition = morphBallEnter.cameraPOS;


            }
            //this.skillLocator.special.RecalculateMaxStock();
            SamusMain.morphBall = false;
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}
