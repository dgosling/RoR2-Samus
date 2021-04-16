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
        Collision collision;
        private Collider collider;
        private GameObject ball;
        private Rigidbody rigidbody;
        public static bool morphBall { get; set; }


        public override void OnEnter()
        {
            this.ChildLocator = base.GetModelChildLocator();
            this.ball = this.ChildLocator.FindChild("Ball2").gameObject;
            this.collider = this.ball.GetComponent<Collider>();
            this.rigidbody = this.ball.GetComponent<Rigidbody>();
            
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
                collider.enabled = true;
                rigidbody.isKinematic = false;
                rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
                
                //rigidbody.AddForce(this.characterMotor.velocity * 100, ForceMode.VelocityChange);
                rigidbody.AddForce(new Vector3((this.characterMotor.velocity.x * 100) + base.characterBody.transform.rotation.eulerAngles.x * -1, this.characterMotor.velocity.y * 100, this.characterMotor.velocity.z * 100), ForceMode.Force);
                this.ball.transform.rotation = new Quaternion(base.characterBody.transform.rotation.x * -1, this.ball.transform.rotation.y, this.ball.transform.rotation.z, this.ball.transform.rotation.w);
                //if (morphBall == true && this.collision.gameObject.name == "DGmdlSamus")
                //{
                //    Debug.Log("bomb jump test");
                //}
                //if (base.healthComponent.TakeDamage())
                //    Debug.Log("test");
            }





        }
    }
}
