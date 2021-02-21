using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using EntityStates;
using RoR2.Projectile;

namespace SamusMod.States
{
    public class BaseRoll : BaseState
    {
        public float duration;
        public float initialSpeedCoefficient = StaticValues.rollSpeedCoefficientIni;
        public float finalSpeedCoefficient = StaticValues.rollSpeedCoefficientFin;
        public string dodgeSoundString;
        public GameObject projectilePrefab;
        public float damageCoefficient;
        public float dodgeFOV;
        private float rollSpeed;
        private Vector3 forwardDirection;
        private Animator animator;
        private Vector3 previousPosition;
        private bool hasFired;

        public override void OnEnter()
        {
            base.OnEnter();
            
            if (dodgeSoundString != null)
            {
                uint num = Util.PlaySound(this.dodgeSoundString, this.gameObject);
            }
            this.animator = this.GetModelAnimator();
            ChildLocator component = this.animator.GetComponent<ChildLocator>();
            if(this.isAuthority && this.inputBank && this.characterDirection)
            {
                this.forwardDirection = (this.inputBank.moveVector == Vector3.zero ? this.characterDirection.forward : this.inputBank.moveVector).normalized;
            }
            Vector3 rhs1 = this.characterDirection ? this.characterDirection.forward : this.forwardDirection;
            Vector3 rhs2 = Vector3.Cross(Vector3.up, rhs1);
            float f1 = Vector3.Dot(this.forwardDirection, rhs1);
            float f2 = Vector3.Dot(this.forwardDirection, rhs2);
            this.animator.SetFloat("forwardSpeed", f1, 0.1f, Time.fixedDeltaTime);
            this.animator.SetFloat("rightSpeed", f2, 0.1f, Time.fixedDeltaTime);
            if (Mathf.Abs(f1) > Mathf.Abs(f2))
                this.PlayAnimation("Body", f1 > 0 ? "Roll" : "Roll", "Roll.playbackRate", this.duration);
            else
                this.PlayAnimation("Body", f2 > 0 ? "Roll" : "Roll", "Roll.playbackRate", this.duration);
            this.RecalculateRollSpeed();
            if(this.characterMotor && this.characterDirection)
            {
                this.characterMotor.velocity.y = 0f;
                this.characterMotor.velocity = this.forwardDirection * this.rollSpeed;
            }
            this.previousPosition = this.transform.position - (this.characterMotor ? this.characterMotor.velocity : Vector3.zero);
        }

        private void RecalculateRollSpeed() => this.rollSpeed = this.moveSpeedStat * Mathf.Lerp(this.initialSpeedCoefficient, this.finalSpeedCoefficient, this.fixedAge / this.duration);

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.RecalculateRollSpeed();
            //Debug.Log(this.animator.ToString());
            if(this.fixedAge >= this.duration/2&&hasFired==false)
                Fire();


            if (this.cameraTargetParams)
                this.cameraTargetParams.fovOverride = Mathf.Lerp(this.dodgeFOV, 60f, this.fixedAge / this.duration);
            Vector3 normalized = (this.transform.position - this.previousPosition).normalized;
            if (this.characterMotor && this.characterDirection && normalized != Vector3.zero)
            {
                Vector3 lhs = normalized * this.rollSpeed;
                float y = lhs.y;
                lhs.y = 0f;
                Vector3 vector3 = this.forwardDirection * Mathf.Max(Vector3.Dot(lhs, this.forwardDirection), 0f);
                vector3.y += Mathf.Max(y, 0f);
                this.characterMotor.velocity = vector3;
            }
            this.previousPosition = this.transform.position;
            if (this.fixedAge < this.duration || !this.isAuthority)
                return;
            this.outer.SetNextStateToMain();
        }
        private void Fire()
        {
            if (this.isAuthority)
            {
                Ray aimRay = this.GetAimRay();
                if (projectilePrefab != null)
                {
                    FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
                    {
                        projectilePrefab = projectilePrefab,
                        owner = this.gameObject,
                        position = aimRay.origin - new Vector3(0,2f,0),
                        rotation = Util.QuaternionSafeLookRotation(aimRay.direction),
                        damage = this.damageCoefficient * this.damageStat,
                        force = 0,
                        crit = this.RollCrit()

                    };
                    ProjectileManager.instance.FireProjectile(fireProjectileInfo);
                    hasFired = true;
                }
            }
        }
        public override void OnExit()
        {
            if (this.cameraTargetParams)
                this.cameraTargetParams.fovOverride = -1f;
            if (hasFired == true)
                hasFired = false;
            base.OnExit();
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write(this.forwardDirection);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            this.forwardDirection = reader.ReadVector3();
        }

    }
}
