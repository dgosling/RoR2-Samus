using RoR2;
using EntityStates;
using UnityEngine;
using UnityEngine.Networking;
using RoR2.Skills;
using VRAPI;
namespace SamusMod.SkillStates.Samus
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

        private static float normalSpeed;
        private Transform tran;
        private float velx, vely, velz;
        private static int normalJumps;
        private static float normalSprint;
        private static int stock2;
        [SerializeField]
        public  SkillDef bomb;
        [SerializeField]
        public  SkillDef powerBomb;
        [SerializeField]
        public  SkillDef exitMorph;

        private static SkinnedMeshRenderer[] nDsMR;

        private static Vector3 cameraPOS;

        private static Transform vRCamera;

        private static Animator vR;
        private static int stock1;
        private static SkinnedMeshRenderer[] dsMR;

        public static int NormalJumps { get => normalJumps; set => normalJumps = value; }
        public static float NormalSprint { get => normalSprint; set => normalSprint = value; }
        public static int Stock1 { get => stock1; set => stock1 = value; }
        public static int Stock2 { get => stock2; set => stock2 = value; }
        public static SkinnedMeshRenderer[] DsMR { get => dsMR; set => dsMR = value; }
        public static SkinnedMeshRenderer[] NDsMR { get => nDsMR; set => nDsMR = value; }
        public static Vector3 CameraPOS { get => cameraPOS; set => cameraPOS = value; }
        public static Transform VRCamera { get => vRCamera; set => vRCamera = value; }
        public static Animator VR { get => vR; set => vR = value; }
        public static float NormalSpeed { get => normalSpeed; set => normalSpeed = value; }

        public override void OnEnter()
        {
            base.OnEnter();
            //if(this.bone.GetComponent<Misc.colision_test>()==null)
            //    this.bone.AddComponent<Misc.colision_test>();

            //Debug.Log("onenter true");
            Stock1 = this.skillLocator.secondary.stock;
            Stock2 = this.skillLocator.special.stock;
            this.duration = this.baseDuration / this.attackSpeedStat;
            this.skillLocator.primary.SetSkillOverride(this.skillLocator.primary, bomb, GenericSkill.SkillOverridePriority.Contextual);
            this.skillLocator.utility.SetSkillOverride(this.skillLocator.utility, exitMorph, GenericSkill.SkillOverridePriority.Contextual);
            this.skillLocator.secondary.SetSkillOverride(this.skillLocator.secondary, powerBomb, GenericSkill.SkillOverridePriority.Contextual);

            if (ExitMorphBall.Recharge != 0)
            {
                this.skillLocator.secondary.rechargeStopwatch = ExitMorphBall.Recharge;

            }
            this.skillLocator.secondary.stock = ExitMorphBall.Pstock;
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
                Modules.VRStuff.SamusHUD.inMorphBall = true;
                foreach (SkinnedMeshRenderer renderer in DsMR)
                {
                    renderer.enabled = false;
                }
                foreach (SkinnedMeshRenderer rend in NDsMR)
                {
                    rend.enabled = false;
                }

                CameraPOS = VRCamera.localPosition;
                VRCamera.Translate(0, -.5f, 0);

            }

            base.PlayAnimation("Body", "transformIn", "Roll.playbackRate", this.duration);

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
            this.ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            this.ball.GetComponent<Rigidbody>().detectCollisions = false;

            NormalSpeed = this.characterBody.baseMoveSpeed;

            this.characterBody.baseMoveSpeed = NormalSpeed * 2;
            NormalJumps = this.characterBody.baseJumpCount;
            this.characterBody.baseJumpCount = 0;
            NormalSprint = this.characterBody.sprintingSpeedMultiplier;
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
            BaseStates.BaseSamus.morphBall = true;
            //SamusMain.camera = this.cameraTargetParams.cameraPivotTransform.rotation.eulerAngles;
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}
