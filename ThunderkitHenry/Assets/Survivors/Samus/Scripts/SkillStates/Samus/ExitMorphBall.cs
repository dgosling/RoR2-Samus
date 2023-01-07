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
        //[SerializeField]
       // public SkillDef missile;
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
            //            if (!BaseStates.BaseSamus.PowerBombInit)
            //            {
            //BaseStates.BaseSamus.PowerBombInit = true;
            //                Debug.Log("set powerbombinit");
            //            }


            //this.skillLocator.secondary.onSkillChanged += Secondary_onSkillChanged;
            //skillLocator.special.onSkillChanged += Special_onSkillChanged;

            //BaseStates.BaseSamus.Stock2 = skillLocator.secondary.stock;
            //BaseStates.BaseSamus.Stock2Max = skillLocator.secondary.maxStock;
            //BaseStates.BaseSamus.Stock2Recharge = skillLocator.secondary.finalRechargeInterval;
            //if (skillLocator.secondary.stock < skillLocator.secondary.maxStock)
            //    BaseStates.BaseSamus.CalcRechargeStepWatch(skillLocator.secondary, false);
            this.skillLocator.primary.UnsetSkillOverride(this.skillLocator.primary, bomb, GenericSkill.SkillOverridePriority.Contextual);
            this.skillLocator.utility.UnsetSkillOverride(this.skillLocator.utility, exitMorph, GenericSkill.SkillOverridePriority.Contextual);
            this.skillLocator.secondary = BaseStates.BaseSamus.MissileSkill;

            //var tuple = BaseStates.BaseSamus.CalcSkillInfo(skillLocator.secondary, true);
            //if (tuple.Item1 == 999 && tuple.Item2 == 999)
            //    skillLocator.secondary.Reset();
            //else
            //{
            //    skillLocator.secondary.stock = tuple.Item1;
            //    skillLocator.secondary.rechargeStopwatch = tuple.Item2;
            //}
            //skillLocator.secondary.RecalculateValues();
            //this.skillLocator.secondary.stock = BaseStates.BaseSamus.Stock1;
            //skillLocator.secondary.rechargeStopwatch = BaseStates.BaseSamus.Stopwatch % BaseStates.BaseSamus.Stock1Recharge;
            //this.skillLocator.secondary.RecalculateValues();
            //Debug.Log(skillLocator.secondary.stock);
            //this.skillLocator.special.stock = BaseStates.BaseSamus.Stock2;
            //skillLocator.secondary.rechargeStopwatch += duration;
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
            //  BaseStates.BaseSamus.emoteMorphBall = false;
            BaseStates.BaseSamus.morphBall = false;
            //    if (Modules.EmoteAPICompatibility.enabled)
            //        BaseStates.BaseSamus.boneMapper.gameObject.SetActive(true);
        }

        //private void Special_onSkillChanged(GenericSkill obj)
        //{
        //    obj.stock = morphBallEnter.Stock2;
        //    Debug.Log("SMissile stock: "+obj.stock);
        //}

        //private void Secondary_onSkillChanged(GenericSkill obj)
        //{
        //    obj.stock = morphBallEnter.Stock1;
        //    Debug.Log("missile stock: "+obj.stock);
        //}

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}
