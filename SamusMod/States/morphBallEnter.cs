using RoR2;
using EntityStates;
using UnityEngine;
using UnityEngine.Networking;
using RoR2.Skills;

namespace SamusMod.States
{
    public class morphBallEnter : BaseSkillState
    {
        
        private ChildLocator ChildLocator;

        public float speedMult = 1.2f;
        public float baseDuration = .45f;
        private float duration;
        private GameObject ball;
        private GameObject armature;
        private GameObject mesh;
        private GameObject bone;
        private float normalSpeed;
        private Transform tran;
        private float velx, vely, velz;
        public static SkillDef bomb = SamusMod.Modules.Skills.morphBallBomb;
        public static SkillDef powerBomb = SamusMod.Modules.Skills.morphBallPowerBomb;
        public static SkillDef exitMorph = SamusMod.Modules.Skills.morphBallExit;
        
        public override void OnEnter()
        {
            base.OnEnter();
            //if(this.bone.GetComponent<Misc.colision_test>()==null)
            //    this.bone.AddComponent<Misc.colision_test>();

            //Debug.Log("onenter true");
            this.duration = this.baseDuration / this.attackSpeedStat;
            this.skillLocator.primary.SetSkillOverride(this.skillLocator.primary, morphBallEnter.bomb, GenericSkill.SkillOverridePriority.Contextual);
            this.skillLocator.utility.SetSkillOverride(this.skillLocator.utility, morphBallEnter.exitMorph, GenericSkill.SkillOverridePriority.Contextual);
            this.skillLocator.secondary.SetSkillOverride(this.skillLocator.secondary, morphBallEnter.powerBomb, GenericSkill.SkillOverridePriority.Contextual);
            this.ChildLocator = base.GetModelChildLocator();

            this.characterBody.gameObject.GetComponent<Collider>().enabled = false;

            this.ball = ChildLocator.FindChild("Ball2").gameObject;
            this.armature = ChildLocator.FindChild("armature").gameObject;
            this.mesh = ChildLocator.FindChild("Body").gameObject;
            this.bone = ChildLocator.FindChild("Ball2Bone").gameObject;

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
            this.ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

            normalSpeed = base.moveSpeedStat;
            this.moveSpeedStat = normalSpeed * 2;
            if (NetworkServer.active)
            {
                this.characterBody.AddBuff(RoR2Content.Buffs.ArmorBoost);
            }
            this.ball.SetActive(true);
            //Debug.Log("isBall2Active " + this.ChildLocator.FindChild("Ball2").gameObject.activeSelf);
            this.armature.SetActive(false);
            //Debug.Log("isarmatureActive " + this.ChildLocator.FindChild("armature").gameObject.activeSelf);
            this.mesh.SetActive(false);
            //this.ball.transform.rotation = (Quaternion.Euler(new Vector3(0, 0, 270)));
            SamusMain.morphBall = true;
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}
