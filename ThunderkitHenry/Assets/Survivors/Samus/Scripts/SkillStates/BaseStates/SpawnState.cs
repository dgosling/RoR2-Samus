using RoR2;
using UnityEngine.Networking;
using EntityStates;
using UnityEngine;
using VRAPI;
namespace SamusMod.SkillStates.BaseStates
{


    public class SpawnState : BaseState
    {
        [SerializeField]
        public float duration;

        private Transform modelTransform;
        private Animator animator;

        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();
            this.modelTransform = base.GetModelTransform();

            if (NetworkServer.active) base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);

            base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", duration);
            //Util.

            //if (this.modelTransform)
            //{
            //    TemporaryOverlay overlay = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
            //    overlay.duration = SpawnState.duration * .75f;
            //    overlay.animateShaderAlpha = true;
            //    overlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
            //    overlay.destroyComponentOnEnd = true;
            //    overlay.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());



            //}
            if (this.animator)
            {
                this.animator.SetFloat(AnimationParameters.aimWeight, 0f);
            }


        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.animator) this.animator.SetBool("inCombat", true);

            if (base.fixedAge >= duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            if (this.animator)
            {
                this.animator.SetFloat(AnimationParameters.aimWeight, 1f);
            }
            if (NetworkServer.active) base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Death;
        }
    }
}
