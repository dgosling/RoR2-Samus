using EntityStates;
using RoR2;
using UnityEngine;

namespace SamusMod.States
{
    public abstract class ChargeBeamBase : BaseSkillState
    {
        protected abstract BaseFireBeam GetNextState();
        public GameObject chargeEffectPrefab;
        public string chargeSoundString;
        public float baseDuration = 2.3f;
        public float minBloomRadius;
        public float maxBloomRadius;
        public GameObject crosshairOverridePrefab;
        protected static readonly float minChargeDuration = 0f;
        public static Vector3 size;
        private GameObject defaultCrosshairPrefab;
        private uint loopSoundInstanceId;

        private float duration { get; set; }
        private Animator animator { get; set; }

        private ChildLocator childLocator { get; set; }

        public GameObject chargeEffectInstance { get; set; }

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            this.animator = base.GetModelAnimator();
            this.childLocator = base.GetModelChildLocator();

            if (this.childLocator)
            {
                Transform transform = this.childLocator.FindChild("gunCon");

                if(transform && this.chargeEffectPrefab)
                {
                    this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
                    this.chargeEffectInstance.transform.parent = transform;
                    

                }
            }


            base.PlayAnimation("Gesture, Override", "chargeStart", "Charge.playbackRate", this.duration - .217f);
            this.loopSoundInstanceId = Util.PlayScaledSound(this.chargeSoundString, base.gameObject, this.attackSpeedStat);
            this.defaultCrosshairPrefab = base.characterBody.crosshairPrefab;

            if (this.crosshairOverridePrefab)
            {
                base.characterBody.crosshairPrefab = this.crosshairOverridePrefab;
            }
            base.StartAimMode(this.duration + 2f, false);
        }
    public virtual float GetCharge()
        {
            return this.calcCharge();
        }

        public override void OnExit()
        {
            if (base.characterBody)
            {
                base.characterBody.crosshairPrefab = this.defaultCrosshairPrefab;
            }

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
            return Mathf.Clamp01(base.fixedAge / this.duration);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            float charge = this.calcCharge();
            if (base.isAuthority && ((!base.IsKeyDownAuthority() && base.fixedAge >= ChargeBeamBase.minChargeDuration) || base.fixedAge >= this.duration))
            {
                BaseFireBeam nextState = this.GetNextState();
                nextState.charge = charge;
                this.outer.SetNextState(nextState);
            }


        }

        public override void Update()
        {
            base.Update();
            base.characterBody.SetSpreadBloom(Util.Remap(this.calcCharge(), 0f, 1f, this.minBloomRadius, this.maxBloomRadius), true);
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
