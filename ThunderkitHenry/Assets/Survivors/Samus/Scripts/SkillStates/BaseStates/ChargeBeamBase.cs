using EntityStates;
using RoR2;
using UnityEngine;
using RoR2.UI;
namespace SamusMod.SkillStates.BaseStates
{


    public abstract class ChargeBeamBase : BaseSkillState
    {
        protected abstract BaseFireBeam GetNextState();
        [SerializeField]
        public GameObject chargeEffectPrefab;
        [SerializeField]
        public string chargeSoundString;
        [SerializeField]
        public GameObject chargeMuzzle;

        public float baseDuration = 2.3f / 2;
        [SerializeField]
        public float minBloomRadius;
        [SerializeField]
        public float maxBloomRadius;
        
        public GameObject crosshairOverridePrefab;
        protected static readonly float minChargeDuration = 0f;
        
        protected static Vector3 size;
        private GameObject defaultCrosshairPrefab;
        private uint loopSoundInstanceId;
        private CrosshairUtils.OverrideRequest crosshairOverrideRequest;
        private float duration { get; set; }
        private Animator animator { get; set; }

        private ChildLocator childLocator { get; set; }
        [SerializeField]
        public GameObject chargeEffectInstance { get; set; }
        private bool isPlayingSound;
        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            this.animator = base.GetModelAnimator();
            this.childLocator = base.GetModelChildLocator();
            Transform transform = this.childLocator.FindChild("gunCon");
            crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(characterBody, crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
            if (!VRAPI.Utils.IsUsingMotionControls(characterBody))
            {



                if (transform && this.chargeEffectPrefab)
                {
                    this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
                    this.chargeEffectInstance.transform.parent = transform;


                }

            }
            else
            {

                this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform);
                this.chargeEffectInstance.transform.Rotate(90, 0, 0);
            }

            base.PlayAnimation("Gesture, Override", "chargeLoop", "Charge.playbackRate", this.duration);

            this.defaultCrosshairPrefab = base.characterBody.defaultCrosshairPrefab;

            //if (this.crosshairOverridePrefab)
            //{
            //    base.characterBody.cro = this.crosshairOverridePrefab;
            //}
            base.StartAimMode(this.duration + 2f, false);
        }
        //public virtual float GetCharge()
        //    {
        //        return this.calcCharge();
        //    }

        public override void OnExit()
        {
            crosshairOverrideRequest?.Dispose();

            AkSoundEngine.StopPlayingID(this.loopSoundInstanceId);

            if (!this.outer.destroying)
            {
                base.PlayAnimation("Gesture, Override", "BufferEmpty");
            }
            size = chargeEffectInstance.transform.localScale;
            EntityState.Destroy(this.chargeEffectInstance);
            base.OnExit();

        }

        protected float calcCharge()
        {
            if (base.age <= Time.fixedDeltaTime)
                return Mathf.Clamp01(Time.fixedDeltaTime / this.duration);
            return Mathf.Clamp01(base.fixedAge / this.duration);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();


            float charge = this.calcCharge();
            TryNextState(charge);


        }

        public override void Update()
        {
            base.Update();
            float Charge = this.calcCharge();
            TryNextState(Charge);
            base.characterBody.SetSpreadBloom(Util.Remap(this.calcCharge(), 0f, 1f, this.minBloomRadius, this.maxBloomRadius), true);
        }
        void TryNextState(float charge)
        {
            if (base.isAuthority && this.isPlayingSound == false && charge > .15)
            {
                if (VRAPI.Utils.IsUsingMotionControls(characterBody))
                    this.loopSoundInstanceId = Util.PlayAttackSpeedSound(this.chargeSoundString, VRAPI.MotionControls.dominantHand.muzzle.gameObject, this.duration - .15f);
                else
                    this.loopSoundInstanceId = Util.PlayAttackSpeedSound(this.chargeSoundString, base.gameObject, this.duration - .15f);
                this.isPlayingSound = true;
            }
            if (base.isAuthority && ((!base.IsKeyDownAuthority() && base.age >= ChargeBeamBase.minChargeDuration)))
            {
                BaseFireBeam nextState = this.GetNextState();
                nextState.charge = charge;
                this.outer.SetNextState(nextState);
            }
        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}
