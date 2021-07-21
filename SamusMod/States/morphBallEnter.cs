using RoR2;
using EntityStates;
using UnityEngine;
using UnityEngine.Networking;
using RoR2.Skills;
using RewiredConsts;
using VRAPI;
namespace SamusMod.States
{
    public class morphBallEnter : BaseSkillState
    {
        
        private ChildLocator ChildLocator;

        public float speedMult = 1.2f;
        public float baseDuration = .42f;
        private float duration;
        private GameObject ball;
        private GameObject armature;
        private GameObject mesh;
        private GameObject bone;
        public static float normalSpeed;
        private Transform tran;
        private float velx, vely, velz;
        public static int normalJumps;
        public static float normalSprint;
        public static int stock1,stock2;
        public static SkillDef bomb = SamusMod.Modules.Skills.morphBallBomb;
        public static SkillDef powerBomb = SamusMod.Modules.Skills.morphBallPowerBomb;
        public static SkillDef exitMorph = SamusMod.Modules.Skills.morphBallExit;
        public static SkinnedMeshRenderer[] DsMR, NDsMR;
        public static Vector3 cameraPOS;
        public static Transform VRCamera;
        public static Animator VR;
        public override void OnEnter()
        {
            base.OnEnter();
            //if(this.bone.GetComponent<Misc.colision_test>()==null)
            //    this.bone.AddComponent<Misc.colision_test>();

            //Debug.Log("onenter true");
            stock1 = this.skillLocator.secondary.stock;
            stock2 = this.skillLocator.special.stock;
            this.duration = this.baseDuration / this.attackSpeedStat;
            this.skillLocator.primary.SetSkillOverride(this.skillLocator.primary, morphBallEnter.bomb, GenericSkill.SkillOverridePriority.Contextual);
            this.skillLocator.utility.SetSkillOverride(this.skillLocator.utility, morphBallEnter.exitMorph, GenericSkill.SkillOverridePriority.Contextual);
            this.skillLocator.secondary.SetSkillOverride(this.skillLocator.secondary, morphBallEnter.powerBomb, GenericSkill.SkillOverridePriority.Contextual);

            if (ExitMorphBall.recharge != 0)
            {
                this.skillLocator.secondary.rechargeStopwatch = ExitMorphBall.recharge;
                
            }
            this.skillLocator.secondary.stock = ExitMorphBall.pstock;
            this.ChildLocator = base.GetModelChildLocator();

            //this.characterBody.gameObject.GetComponent<Collider>().enabled = false;

            this.ball = ChildLocator.FindChild("Ball2").gameObject;
            this.armature = ChildLocator.FindChild("armature").gameObject;
            this.mesh = ChildLocator.FindChild("Body").gameObject;
            this.bone = ChildLocator.FindChild("Ball2Bone").gameObject;
            if (Utils.IsUsingMotionControls(this.characterBody))
            {
                VR = MotionControls.dominantHand.animator;
                VRCamera = this.characterBody.transform.Find("VRCamera");
                DsMR = MotionControls.dominantHand.transform.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
                NDsMR = MotionControls.nonDominantHand.transform.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

                foreach (SkinnedMeshRenderer renderer in DsMR)
                {
                    renderer.enabled = false;
                }
                foreach (SkinnedMeshRenderer rend in NDsMR)
                {
                    rend.enabled = false;
                }

                cameraPOS = VRCamera.localPosition;
                VRCamera.Translate(0,-.5f,0);

            }
            
            base.PlayAnimation("Body", "transformIn", "Roll.playbackRate",this.duration);
            
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
            this.ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition|RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationZ;
            this.ball.GetComponent<Rigidbody>().detectCollisions = false;

            normalSpeed = this.characterBody.baseMoveSpeed;

            this.characterBody.baseMoveSpeed = normalSpeed * 2;
            normalJumps = this.characterBody.baseJumpCount;
            this.characterBody.baseJumpCount = 0;
            normalSprint = this.characterBody.sprintingSpeedMultiplier;
            this.characterBody.sprintingSpeedMultiplier = 1.2f;

            //Debug.Log("basemovespeed: " + normalSpeed);
            //this.moveSpeedStat = normalSpeed * 2;
            //Debug.Log("modmovespeed: "+this.moveSpeedStat);
            if (NetworkServer.active)
            {
                this.characterBody.AddBuff(RoR2Content.Buffs.ArmorBoost);
            }
            this.ball.SetActive(true);
            //Debug.Log("isBall2Active " + this.ChildLocator.FindChild("Ball2").gameObject.activeSelf);
            //this.armature.SetActive(false);
            //Debug.Log("isarmatureActive " + this.ChildLocator.FindChild("armature").gameObject.activeSelf);
            this.mesh.SetActive(false);
            //this.ball.transform.rotation = (Quaternion.Euler(new Vector3(0, 0, 270)));
            SamusMain.morphBall = true;
            //SamusMain.camera = this.cameraTargetParams.cameraPivotTransform.rotation.eulerAngles;
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}
