using RoR2;
using EntityStates;
using UnityEngine;

namespace SamusMod.States
{
    public class SamusMain : GenericCharacterMain
    {
        private ChildLocator ChildLocator;
        private Animator Animator;
        private bool wasActive;

        public override void OnEnter()
        {
            base.OnEnter();

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (this.Animator)
            {
                this.Animator.SetFloat("sprintValue", base.characterBody.isSprinting ? -1 : 0, .2f, Time.fixedDeltaTime);
                this.Animator.SetBool("inCombat", (!base.characterBody.outOfCombat || !base.characterBody.outOfDanger));
            }
        }





    }
}
