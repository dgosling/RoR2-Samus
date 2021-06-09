using RoR2;
using EntityStates;
using UnityEngine;
using System;
using RewiredConsts;

namespace SamusMod.States
{
    public class SamusMain : GenericCharacterMain
    {
        private ChildLocator ChildLocator;
        private Animator Animator;
        private bool wasActive;
        Collision collision;
        private Collider collider;
        private GameObject ball;
        private Rigidbody rigidbody;
        private CharacterBody body;
        private Vector3 velocity;
        private Vector3 direction;
        public static Vector3 camera;
        private float horizontalInput;
        private Vector3 forwardDir;
        private float stopwatch;

        public static bool morphBall { get; set; }


        public override void OnEnter()
        {
            this.ChildLocator = base.GetModelChildLocator();
            this.ball = this.ChildLocator.FindChild("Ball2").gameObject;
            this.collider = this.ball.GetComponent<Collider>();
            this.rigidbody = this.ball.GetComponent<Rigidbody>();
            this.body = base.characterBody;


            base.OnEnter();
            //KinematicCharacterController.KinematicCharacterMotor kin = ball.AddComponent<KinematicCharacterController.KinematicCharacterMotor>();
            //kin.CharacterController = this.characterMotor;
            //kin.Rigidbody = ball.GetComponent<Rigidbody>();
            //kin.Capsule = ball.GetComponent<CapsuleCollider>();
            //kin.enabled = false;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (this.Animator)
            {
                this.Animator.SetFloat("sprintValue", base.characterBody.isSprinting ? -1 : 0, .2f, Time.fixedDeltaTime);
                this.Animator.SetBool("inCombat", (!base.characterBody.outOfCombat || !base.characterBody.outOfDanger));
            }

            if (base.healthComponent.isInFrozenState == true && (this.ChildLocator.FindChild("Ball").localScale != new Vector3(.5f, .5f, .5f)))
            {
                ChildLocator.FindChild("Ball").localScale = new Vector3(.5f, .5f, .5f);
            }

            if (morphBall == true)
            {
                //Material test = Resources.Load<GameObject>("prefabs/networkedobjects/LockedMage").transform.Find("ModelBase/IceMesh").GetComponent<MeshRenderer>().materials[1];
                //Debug.Log(test.HasProperty("_Magnitude"));
                

                this.velocity = this.characterMotor.velocity;
                this.direction = this.inputBank.moveVector;
                camera = this.cameraTargetParams.cameraPivotTransform.rotation.eulerAngles;
                if (this.characterBody.inventory.GetItemCount(RoR2Content.Items.SecondarySkillMagazine) > 0)
                {
                    this.skillLocator.primary.maxStock = this.characterBody.inventory.GetItemCount(RoR2Content.Items.SecondarySkillMagazine) + 3;
                    
                }
                Vector3 combined = Vector3.Scale(this.velocity,this.direction);
                //this.rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationY;
                //this.characterDirection
                
                this.collider.enabled = true;
                this.rigidbody.isKinematic = false;
                this.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                this.rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
                float amount;
                if (this.characterBody.isSprinting == true)
                {
                    if (this.velocity.x < 0 && this.velocity.z < 0)
                    {
                        amount = -this.velocity.x + (-this.velocity.z);
                    }
                    else if (this.velocity.x < 0 && this.velocity.z > 0)
                    {
                        amount = -this.velocity.x + this.velocity.z;
                    }
                    else if (this.velocity.x > 0 && this.velocity.z < 0)
                    {
                        amount = this.velocity.x + (-this.velocity.z);
                    }
                    else if (this.velocity.x > 0 && this.velocity.z > 0)
                    {
                        amount = this.velocity.x + this.velocity.z;
                    }
                    else
                        amount = this.velocity.x + this.velocity.z;

                    if (amount > 16.8f || amount < -16.8f)
                    {
                        amount = 16.8f;
                    }
                }
                else
                {
                    if (this.velocity.x < 0 && this.velocity.z < 0)
                    {
                        amount = -this.velocity.x + (-this.velocity.z);
                    }
                    else if (this.velocity.x < 0 && this.velocity.z > 0)
                    {
                        amount = -this.velocity.x + this.velocity.z;
                    }
                    else if (this.velocity.x > 0 && this.velocity.z < 0)
                    {
                        amount = this.velocity.x + (-this.velocity.z);
                    }
                    else if (this.velocity.x > 0 && this.velocity.z > 0)
                    {
                        amount = this.velocity.x + this.velocity.z;
                    }
                    else
                        amount = this.velocity.x + this.velocity.z;

                    if (amount > 14 || amount < -14)
                    {
                        amount = 14;
                    }
                }


                //On.RoR2.CharacterMaster.OnBodyDamaged += CharacterMaster_OnBodyDamaged;
                //this.rigidbody.AddForce(combined);
                //this.ball.transform.Rotate(new Vector3(-this.characterMotor.moveDirection.y,this.characterMotor.moveDirection.x,this.characterMotor.moveDirection.z));
                //this.rigidbody.AddForce(new Vector3(this.characterMotor.velocity.x * 100 +( this.characterBody.transform.rotation.eulerAngles.x * -1), this.characterMotor.velocity.y * 100, this.characterMotor.velocity.z * 100), ForceMode.Force);
                //this.ball.transform.rotation = new Quaternion(this.characterBody.transform.rotation.x * -1, this.ball.transform.rotation.y, this.ball.transform.rotation.z, this.ball.transform.rotation.w);
                //if (morphBall == true && this.collision.gameObject.name == "DGmdlSamus")
                //{
                //    Debug.Log("bomb jump test");
                //}
                //if (base.healthComponent.TakeDamage())
                //    Debug.Log("test");

                if (this.characterMotor.velocity != Vector3.zero)
                {
                    this.ball.transform.Rotate(Vector3.up, (-amount ));

                    //Debug.Log("vel: " + this.velocity);
                    //Debug.Log("local vel " + Ivelocity);
                    //Debug.Log("test: " + Vector3.RotateTowards(this.ball.transform.rotation.eulerAngles, this.velocity, this.moveSpeedStat * Time.deltaTime, 0));
                    //Debug.Log("Velocity: "+this.velocity);
                    //Debug.Log("Camera: " + camera);
                    //Debug.Log("Combined: " + combined);
                }

            }






        }






    }
}
