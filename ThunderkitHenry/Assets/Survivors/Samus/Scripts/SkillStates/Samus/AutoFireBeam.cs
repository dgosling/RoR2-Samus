using UnityEngine;
using EntityStates;
using RoR2;
using RoR2.Projectile;


namespace SamusMod.SkillStates.Samus
{
    public class AutoFireBeam : BaseSkillState
    {
        
        private GameObject projectilePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/TracerNoSmoke");
        [SerializeField]
        public GameObject muzzleFlashPrefab;
        [SerializeField]
        public GameObject hitEffectPrefab;
        [SerializeField]
        public float baseDuration;
        private float duration;
        private float force = 5f;
        private string muzzleName = "gunCon";
        private Ray aimRay;
        private float timer;
        public override void OnEnter()
        {
            base.OnEnter();
            duration = baseDuration / attackSpeedStat;
            aimRay = GetAimRay();
            
            
        }
        private void Fire()
        {
            if (isAuthority)
            {



                if (VRAPI.Utils.IsUsingMotionControls(characterBody))
                {
                    aimRay = VRAPI.MotionControls.dominantHand.aimRay;
                    Animator VR = VRAPI.MotionControls.dominantHand.animator;
                    PlayAnimationOnAnimator(VR, "Base Layer", "shoot", "Shoot.playbackRate", .1f);
                }
                else
                    PlayAnimation("Gesture, Override", "Beam", "Charge.playbackRate", 0.05f);
                if (muzzleFlashPrefab)
                    EffectManager.SimpleMuzzleFlash(muzzleFlashPrefab, gameObject, muzzleName, false);  
                float num = .1f * force;
                if (VRAPI.Utils.IsUsingMotionControls(characterBody))
                    Util.PlayAttackSpeedSound(Modules.Sounds.beamSound, VRAPI.MotionControls.dominantHand.muzzle.gameObject, attackSpeedStat);
                else
                    Util.PlayAttackSpeedSound(Modules.Sounds.beamSound, gameObject,attackSpeedStat);
                new BulletAttack()
                {
                    owner = gameObject,
                    weapon = gameObject,
                    origin = aimRay.origin,
                    aimVector = aimRay.direction,
                    minSpread = 0,
                    maxSpread = 0,
                    damage = damageStat,
                    force = num,
                    tracerEffectPrefab = projectilePrefab,
                    muzzleName = muzzleName,
                    hitEffectPrefab = hitEffectPrefab,
                    isCrit = RollCrit(),
                    radius = .1f,
                    smartCollision = true
                }.Fire();
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            aimRay = GetAimRay();
            timer += Time.fixedDeltaTime;
            if (timer >= duration)
            {
                timer = 0f;
                Fire();
            }
            
            if (!isAuthority || IsKeyDownAuthority())
                return;

            outer.SetNextStateToMain();

            
                
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override InterruptPriority GetMinimumInterruptPriority() => InterruptPriority.PrioritySkill;

    }
}
