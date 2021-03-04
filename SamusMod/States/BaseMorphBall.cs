using EntityStates;
using UnityEngine;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using KinematicCharacterController;

namespace SamusMod.States
{
    public abstract class BaseMorphBall : EntityStates.BaseState
    {
        private ChildLocator ChildLocator;
        private bool onEnter;
        public float speedMult=1.2f;
        public override void OnEnter()
        {
            base.OnEnter();
            onEnter = true;
            this.ChildLocator = base.GetModelChildLocator();
           
            if (NetworkServer.active)
            {
                this.characterBody.AddBuff(BuffIndex.ArmorBoost);
            }
            this.ChildLocator.FindChild("Ball2").gameObject.SetActive(true);
            Debug.Log("isBall2Active " + this.ChildLocator.FindChild("Ball2").gameObject.activeSelf);
            this.ChildLocator.FindChild("armature").gameObject.SetActive(false);
            Debug.Log("isarmatureActive " + this.ChildLocator.FindChild("armature").gameObject.activeSelf);
            this.ChildLocator.FindChild("Body").gameObject.SetActive(false);


        }

        private Vector3 IdealVelocity() => this.characterDirection.forward * this.characterBody.moveSpeed * this.speedMult;

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.fixedAge >= .1f && onEnter == true)
            {
                onEnter = false;
            }

            if (this.inputBank && this.characterDirection)
            {
                //this.characterBody.rigidbody.isKinematic = false;
                //this.characterBody.rigidbody.AddForce(this.inputBank.moveVector,ForceMode.Acceleration);
                //this.rigidbody.isKinematic = false;
                //this.rigidbody.AddForce(this.inputBank.moveVector, ForceMode.Acceleration);
                //this.ChildLocator.FindChild("Ball2").gameObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
                //this.ChildLocator.FindChild("Ball2").gameObject.GetComponentInChildren<Rigidbody>().AddForce(this.inputBank.moveVector, ForceMode.Acceleration);
                
            //{
            //    this.characterMotor.rootMotion += IdealVelocity()*Time.fixedDeltaTime;
            }
            if (inputBank.skill3.down && onEnter == false)
            {
                this.outer.SetNextStateToMain();
            }
        }

        public override void Update() => base.Update();

        public override void OnExit()
        {
            if (NetworkServer.active)
            {
                this.characterBody.RemoveBuff(BuffIndex.ArmorBoost);
            }
            this.ChildLocator.FindChild("Ball2").gameObject.SetActive(false);
            this.ChildLocator.FindChild("armature").gameObject.SetActive(true);
            this.ChildLocator.FindChild("Body").gameObject.SetActive(true);
            //IEnumerator enumerator = this.ChildLocator.FindChild("Body").GetEnumerator();
            //try
            //{
            //    while (enumerator.MoveNext())
            //    {
            //        ((Component)enumerator.Current).gameObject.SetActive(true);
            //    }
            //}
            //finally
            //{
            //    if (enumerator is IDisposable disposable)
            //        disposable.Dispose();
            //}

            base.OnExit();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
