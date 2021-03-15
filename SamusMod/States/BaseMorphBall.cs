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
    public abstract class BaseMorphBall : EntityStates.BaseSkillState
    {
        private ChildLocator ChildLocator;
        private bool onEnter;
        public float speedMult=1.2f;
        private GameObject ball;
        private GameObject armature;
        private GameObject mesh;
        private GameObject bone;
        private float normalSpeed;
        private Transform tran;
        private float velx,vely,velz;
        public static SkillDef bomb = SamusMod.Modules.Skills.morphBallBomb;
        public static SkillDef powerBomb= SamusMod.Modules.Skills.morphBallPowerBomb;
        public static SkillDef exitMorph = SamusMod.Modules.Skills.morphBallExit;
        public override void OnEnter()
        {
            base.OnEnter();
            //if(this.bone.GetComponent<Misc.colision_test>()==null)
            //    this.bone.AddComponent<Misc.colision_test>();
            this.onEnter = true;
            Debug.Log("onenter true");

            this.skillLocator.utility.SetSkillOverride(this.skillLocator.utility, BaseMorphBall.exitMorph, GenericSkill.SkillOverridePriority.Contextual);
            this.ChildLocator = base.GetModelChildLocator();
            this.characterBody.gameObject.GetComponent<Collider>().enabled = false;

            this.ball = ChildLocator.FindChild("Ball2").gameObject;
            this.armature = ChildLocator.FindChild("armature").gameObject;
            this.mesh = ChildLocator.FindChild("Body").gameObject;
            this.bone = ChildLocator.FindChild("Ball2Bone").gameObject;
            this.ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            
            normalSpeed = base.moveSpeedStat;
            this.moveSpeedStat = normalSpeed * 2;
            if (NetworkServer.active)
            {
                this.characterBody.AddBuff(BuffIndex.ArmorBoost);
            }
            this.ball.SetActive(true);
            Debug.Log("isBall2Active " + this.ChildLocator.FindChild("Ball2").gameObject.activeSelf);
            this.armature.SetActive(false);
            Debug.Log("isarmatureActive " + this.ChildLocator.FindChild("armature").gameObject.activeSelf);
            this.mesh.SetActive(false);
            //this.ball.transform.rotation = (Quaternion.Euler(new Vector3(0, 0, 270)));

        }

        private Vector3 IdealVelocity() => this.characterDirection.forward * this.characterBody.moveSpeed * this.speedMult;

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (this.fixedAge >= .5f && this.onEnter == true)
            {
                this.onEnter = false;
                Debug.Log("onenter false");
            }
            if (this.inputBank && this.characterDirection)
            {
                //Debug.Log(this.inputBank.moveVector);
                //this.characterBody.rigidbody.isKinematic = false;
                //this.characterBody.rigidbody.AddForce(this.inputBank.moveVector);

                //this.rigidbody.AddForce(this.inputBank.moveVector);
                //this.rigidbody.isKinematic = false;
                //this.rigidbody.AddForce(this.inputBank.moveVector, ForceMode.Acceleration);
                //this.ChildLocator.FindChild("Ball2").gameObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
                //this.ChildLocator.FindChild("Ball2").gameObject.GetComponentInChildren<Rigidbody>().AddForce(this.inputBank.moveVector, ForceMode.Acceleration);
                //Quaternion quaternion = Quaternion.LookRotation();

                //Vector3 vector3 = this.inputBank.moveVector;
                //this.characterMotor.UpdateVelocity(ref vector3, Time.deltaTime);
                //this.ball.transform.Rotate(Vector3.RotateTowards(this.ball.transform.forward, this.characterMotor.velocity, Time.deltaTime, 0f));
                //this.characterMotor.Motor.RotateCharacter(Quaternion.Euler(this.characterMotor.rootMotion)); 
                Collision collision;
                Collider collider = this.ball.GetComponent<Collider>();
                Rigidbody rigidbody = this.ball.GetComponent<Rigidbody>();

                
                //this.ball.GetComponent<Animator>().enabled = false;
                collider.enabled = true;
                rigidbody.isKinematic = false;
                rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;

                rigidbody.AddForce(this.characterMotor.velocity*100,ForceMode.Force);
                // rigidbody.rotation.SetLookRotation(this.characterDirection.forward);
                //rigidbody.MoveRotation(Quaternion.Euler(this.characterMotor.velocity + new Vector3(this.inputBank.moveVector.x*rigidbody.rotation.eulerAngles.x, this.inputBank.moveVector.y * rigidbody.rotation.eulerAngles.y, this.inputBank.moveVector.z * rigidbody.rotation.eulerAngles.z)));

                //rigidbody.AddRelativeTorque(test,ForceMode.Force);
                

                //this.characterMotor.UpdateRotation(ref quaternion, Time.deltaTime);

                //{
                //    this.characterMotor.rootMotion += IdealVelocity()*Time.fixedDeltaTime;
            }

        }





        public override void Update()
        {

            base.Update();
            this.velx = this.characterMotor.velocity.x;
            this.vely = this.characterMotor.velocity.y;
            this.velz = this.characterMotor.velocity.z;
            //if (this.velx > 0)
            //{
            //    this.velx += 90;
            //}
            //else
            //    this.velx = this.characterMotor.velocity.x;
        }

        public override void OnExit()
        {
            if (NetworkServer.active)
            {
                this.characterBody.RemoveBuff(BuffIndex.ArmorBoost);
            }
            //SamusMain.Destroy(this.bone.GetComponent<Misc.colision_test>());
            this.characterBody.gameObject.GetComponent<Collider>().enabled = true;
            Collider collider = this.ball.GetComponent<Collider>();
            Rigidbody rigidbody = this.ball.GetComponent<Rigidbody>();
            //this.ball.GetComponent<Animator>().enabled = true;
            //this.ball.transform.SetParent(this.tran);
            collider.enabled = false;
            rigidbody.isKinematic = true;
            rigidbody.interpolation = RigidbodyInterpolation.None;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            //this.ball.GetComponent<KinematicCharacterMotor>().enabled = false;
            //this.characterMotor.Motor = this.characterMotor.gameObject.GetComponent<KinematicCharacterMotor>() ;
            //this.characterBody.mainHurtBox.collider.enabled = true;
            this.ChildLocator.FindChild("Ball2").gameObject.SetActive(false);
            this.ChildLocator.FindChild("armature").gameObject.SetActive(true);
            this.ChildLocator.FindChild("Body").gameObject.SetActive(true);
            //base.moveSpeedStat = normalSpeed;
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
