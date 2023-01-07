using RoR2;
using EntityStates;
using UnityEngine;
using System;

namespace SamusMod.SkillStates.BaseStates
{


    public class BaseSamus : GenericCharacterMain
    {
        private ChildLocator ChildLocator;
        private Animator Animator;
        private static bool noPowerBomb;
        private bool filled;
        Collision collision;
        private Collider collider;
        private GameObject ball;
        private Rigidbody BallRigidBody;
        private Vector3 velocity;
        private Vector3 direction;
        public Vector3 camera;
        private float horizontalInput;
        private Vector3 forwardDir;
        private static float stopwatch;
        private static float stopwatch2;
        private static float cacheStopwatch;
        private static float cacheStopwatch2;
        private static float maxStopwatch;
        private static float maxStopwatch2;
        //private static bool _emoteMorphBall=false;
        private static bool powerBombInit;
        private static bool vrCheck;
        private static CharacterBody body;
        [SerializeField]
        public RoR2.Skills.SkillDef morphBallRef;
        [SerializeField]
        public RoR2.Skills.SkillDef autoFireSkill;
        private static GenericSkill powerBallskill;
        private static GenericSkill missileSkill;
        bool effectiveAuth;
      //  public static BoneMapper boneMapper;
        //private static Components.ExtraInputBankTest extraInput;
       // private bool AutoFireEnabled;
       //public static bool emoteMorphBall
       // {
       //     get => _emoteMorphBall;
       //     set => _emoteMorphBall = value;
       // }
        public static bool morphBall { get; set; }
        public static CharacterBody Body { get => body; set => body = value; }
        public static bool VrCheck { get => vrCheck; set => vrCheck = value; }
        public static float Stopwatch { get => stopwatch; set => stopwatch = value; }
        public static float Stopwatch2 { get => stopwatch2; set => stopwatch2 = value; }
        public static bool PowerBombInit { get => powerBombInit; set => powerBombInit = value; }
        public static GenericSkill PowerBallskill { get => powerBallskill; set => powerBallskill = value; }
        public static GenericSkill MissileSkill { get => missileSkill; set => missileSkill = value; }
        PlayerCharacterMasterController PlayerCharacterMasterController;
        Rewired.Player player;
       // public bool autoFireEnabled { get => AutoFireEnabled; set => AutoFireEnabled = value; }

        //public static int Stock1 { get => stock1; set => stock1 = value; }
        //public static int Stock2 { get => stock2; set => stock2 = value; }
        //public static int Stock1Max { get => stock1Max; set => stock1Max = value; }
        //public static int Stock2Max { get => stock2Max; set => stock2Max = value; }
        //public static float Stock1Recharge { get => stock1Recharge; set => stock1Recharge = value; }
        //public static float Stock2Recharge { get => stock2Recharge; set => stock2Recharge = value; }
        //public static bool NoPowerBomb { get => noPowerBomb; set => noPowerBomb = value; }
        //public static float  Stopwatch { get => stopwatch; set => stopwatch = value; }
        //public static float  Stopwatch2 { get => stopwatch2; set => stopwatch2 = value; }
        //private float maxtime;
        //private float maxtime2;
        //private int cacheBackup;

        // Start is called before the first frame update
        public override void OnEnter()
        {

            this.ChildLocator = base.GetModelChildLocator();
            this.ball = this.ChildLocator.FindChild("Ball2").gameObject;
            this.collider = this.ball.GetComponent<Collider>();
            this.BallRigidBody = this.ball.GetComponent<Rigidbody>();

            PlayerCharacterMasterController = characterBody.master.playerCharacterMasterController;
            
                PlayerCharacterMasterController.CanSendBodyInput(PlayerCharacterMasterController.networkUser, out _, out player, out _);
            if (skillLocator.utility.skillDef == morphBallRef)
            {
                MissileSkill = skillLocator.FindSkillByFamilyName("SamusSecondary");
                powerBallskill = skillLocator.FindSkillByFamilyName("SamusSecondary2");

            }
            else
            {
                 skillLocator.FindSkillByFamilyName("SamusSecondary2").enabled = false;
                
            }
            //if (Modules.EmoteAPICompatibility.enabled)
            //{
            //    boneMapper = modelLocator.modelTransform.GetComponentInChildren<BoneMapper>();
            //}
                //if (SamusPlugin.autoFireEnabled)
                //    extraInput = gameObject.GetComponent<Components.ExtraInputBankTest>();
                Stopwatch = 0f;
            Stopwatch2 = 0f;
            powerBombInit = false;
            //noPowerBomb = true;
            Body = characterBody;

            if (VRAPI.VR.enabled)
            {
                VrCheck = VRAPI.Utils.IsUsingMotionControls(Body);
                if (VrCheck == true)
                {
                    SamusMod.Modules.VRStuff.setupVR(Body);

                    Camera.main.nearClipPlane = 0.05f;

                    this.ChildLocator.FindChild("chargeEffect").gameObject.SetActive(false);
                    //Debug.Log("dom: " + VRAPI.MotionControls.dominantHand);
                    //Debug.Log("ndom: " + VRAPI.MotionControls.nonDominantHand);
                    if (Modules.Config.enableHud.Value)
                        Modules.VRStuff.SamusHUD.initSamusHUD(Body);
                }

            }
            skillLocator.secondary.Reset();
            skillLocator.special.Reset();
            //maxtime = skillLocator.secondary.maxStock * skillLocator.secondary.finalRechargeInterval;
            
            base.OnEnter();
        }
        public override void Update()
        {
            base.Update();
            // RoR2.DotController.onDotInflictedServerGlobal += DotController_onDotInflictedServerGlobal;



            
            if (DotController.FindDotController(gameObject) != null)
            {
                DotController dotController = DotController.FindDotController(gameObject);
                switch (dotController.activeDotFlags)
                {
                    case (uint)DotController.DotIndex.PercentBurn:
                        dotController.RemoveDotStackAtServer(((int)DotController.DotIndex.PercentBurn));
                        break;
                    case (uint)DotController.DotIndex.Burn:
                        dotController.RemoveDotStackAtServer(((int)DotController.DotIndex.Burn));
                        break;
                    case (uint)DotController.DotIndex.Helfire:
                        dotController.RemoveDotStackAtServer(((int)DotController.DotIndex.Helfire));
                        break;
                    default:
                        break;
                }
            }
            if (effectiveAuth)
            {
                if ( !morphBall && player.GetButtonDown(Modules.RewiredAction.autoFire.ActionId))
                {
                    bool a = skillLocator.primary.skillDef == autoFireSkill;

                    if (a)
                        skillLocator.primary.UnsetSkillOverride(skillLocator.primary, autoFireSkill, GenericSkill.SkillOverridePriority.Contextual);
                    else
                        skillLocator.primary.SetSkillOverride(skillLocator.primary, autoFireSkill, GenericSkill.SkillOverridePriority.Contextual);
                }
            }
        }
        

        // Update is called once per frame
        public override void FixedUpdate()
        {
            base.FixedUpdate();


            this.characterBody.skillLocator.FindSkill("");
            effectiveAuth = characterBody.master.hasEffectiveAuthority;
            if (this.Animator)
            {
                this.Animator.SetFloat("sprintValue", base.characterBody.isSprinting ? -1 : 0, .2f, Time.fixedDeltaTime);
                this.Animator.SetBool("inCombat", (!base.characterBody.outOfCombat || !base.characterBody.outOfDanger));
            }
            if (!healthComponent.alive)
            {

                Destroy(Modules.VRStuff.hudHandle);

            }
            
            //maxtime2 = stock2Max * stock2Recharge;
            //if (!morphBall && cacheBackup != characterBody.inventory.GetItemCount(RoR2Content.Items.SecondarySkillMagazine))
            //{
            //    cacheBackup = characterBody.inventory.GetItemCount(RoR2Content.Items.SecondarySkillMagazine);
            //    maxtime = skillLocator.secondary.maxStock * skillLocator.secondary.finalRechargeInterval;
            //    if (Stock2Max >= 6)
            //    {
            //        maxtime2 = stock2Max * stock2Recharge;
            //    }
            //}
                

            if (base.healthComponent.isInFrozenState == true && (this.ChildLocator.FindChild("Ball").localScale != new Vector3(.5f, .5f, .5f)))
            {
                ChildLocator.FindChild("Ball").localScale = new Vector3(.5f, .5f, .5f);
            }
            //if (!morphBall)
            //    stopwatch = (skillLocator.secondary.stock * skillLocator.secondary.finalRechargeInterval)+skillLocator.secondary.rechargeStopwatch;
            //if (stopwatch2 < maxtime2)
            //{
            //    stopwatch2 += Time.fixedDeltaTime;
            //    if((stopwatch2 / stock2Recharge) > stock2)
            //    {
            //        stock2 = Mathf.FloorToInt(stopwatch2 / stock2Recharge);
            //    }
            //}
            if (stopwatch2 < maxStopwatch2)
                stopwatch2 += Time.fixedDeltaTime;
                
            if (morphBall == true)
            {
                //Material test = LegacyResourcesAPI.Load<GameObject>("prefabs/networkedobjects/LockedMage").transform.Find("ModelBase/IceMesh").GetComponent<MeshRenderer>().materials[1];
                //Debug.Log(test.HasProperty("_Magnitude"));
                //noPowerBomb = true;
                //if (stopwatch < maxtime)
                //{
                //    stopwatch += Time.fixedDeltaTime;
                //    if ((stopwatch / Stock1Recharge) > stock1)
                //        stock1= Mathf.FloorToInt(stopwatch/stock1Recharge);
                //}
                //stopwatch2 = (skillLocator.secondary.stock * skillLocator.secondary.finalRechargeInterval) + skillLocator.secondary.rechargeStopwatch;
                //if (Stock2 < Stock2Max)
                //{
                //    stopwatch2 += Time.fixedDeltaTime;
                //    if (stopwatch2 >= Stock2Recharge)
                //    {
                //        Stock2++;
                //        stopwatch2 = 0f;
                //        if (SamusPlugin.debug)
                //            Debug.Log("added stock to special!");
                //    }
                //}
                if(stopwatch<maxStopwatch)
                    stopwatch += Time.fixedDeltaTime;
                this.velocity = this.characterMotor.velocity;
                this.direction = this.inputBank.moveVector;
                camera = this.cameraTargetParams.cameraPivotTransform.rotation.eulerAngles;
                //if (this.characterBody.inventory.GetItemCount(RoR2Content.Items.SecondarySkillMagazine) > 0)
                //{
                //    this.skillLocator.primary.maxStock = this.characterBody.inventory.GetItemCount(RoR2Content.Items.SecondarySkillMagazine) + 3;

                //}
                Vector3 combined = Vector3.Scale(this.velocity, this.direction);
                //this.rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationY;
                //this.characterDirection

                this.collider.enabled = true;
                this.BallRigidBody.isKinematic = false;
                this.BallRigidBody.interpolation = RigidbodyInterpolation.Interpolate;
                this.BallRigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
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
                    this.ball.transform.Rotate(Vector3.up, (-amount));

                    //Debug.Log("vel: " + this.velocity);
                    //Debug.Log("local vel " + Ivelocity);
                    //Debug.Log("test: " + Vector3.RotateTowards(this.ball.transform.rotation.eulerAngles, this.velocity, this.moveSpeedStat * Time.deltaTime, 0));
                    //Debug.Log("Velocity: "+this.velocity);
                    //Debug.Log("Camera: " + camera);
                    //Debug.Log("Combined: " + combined);
                }

            }
        }

        private void DotController_onDotInflictedServerGlobal(DotController dotController, ref InflictDotInfo inflictDotInfo)
        {
            if ((inflictDotInfo.victimObject.gameObject.GetComponent<CharacterBody>().baseNameToken == "DG_SAMUS_NAME" || inflictDotInfo.attackerObject.gameObject.GetComponent<CharacterBody>().baseNameToken == "DG_SAMUS_NAME") && inflictDotInfo.dotIndex == DotController.DotIndex.PercentBurn)
            {
                inflictDotInfo.duration = 0;
                inflictDotInfo.damageMultiplier = 0;
                //Debug.Log("testing inflict");
            }
            
        }

        public override void OnExit()
        {
            base.OnExit();
            //Modules.VRStuff.SamusHUD.bossEnergyIntf.reset();
            Destroy(Modules.VRStuff.hudHandle);
        }

//        public static void CalcRechargeStepWatch(GenericSkill genericSkill,bool sec)
//        {
//            float a = genericSkill.stock;
//            a *= genericSkill.finalRechargeInterval;
//            if (genericSkill.stock == 0)
//                a -= genericSkill.rechargeStopwatch;
//            else
//                a += genericSkill.rechargeStopwatch;
//            if (sec) 
//            {

//                cacheStopwatch = a;
//                maxStopwatch = (genericSkill.maxStock * genericSkill.finalRechargeInterval)-a;
//                Debug.Log("stopwatch set to: " + cacheStopwatch);
//            }
//            else
//            {
//                cacheStopwatch2 = a;
//                maxStopwatch2 = (genericSkill.maxStock * genericSkill.finalRechargeInterval) - a;
//                Debug.Log("stopwatch2 set to: " + cacheStopwatch2);
//            }
//        }
//        public static Tuple<int,float> CalcSkillInfo(GenericSkill genericSkill,bool sec)
//        {
            
//            if (sec)
//            {
//                float max = genericSkill.maxStock * genericSkill.finalRechargeInterval;
//                float watch = cacheStopwatch + stopwatch;
//                if (watch >= max)
//                {
//                    ResetStopWatch(sec);
//                    return new Tuple<int, float>(999, 999);
//                }
                    
//                else
//                {
//                    int a = Mathf.FloorToInt(watch / genericSkill.finalRechargeInterval);
//                    float b = watch % genericSkill.finalRechargeInterval;
//                    Debug.Log("new missile stock: " + a);
//                    Debug.Log("new missile recharge: " + b);
//                    ResetStopWatch(sec);
//                    return new Tuple<int, float>(Mathf.FloorToInt(watch / genericSkill.finalRechargeInterval), watch % genericSkill.finalRechargeInterval);
//                    //genericSkill.stock = ;
//                    //genericSkill.rechargeStopwatch = ;
//                    //Debug.Log("missiles max: " + max);
//                    //Debug.Log("missiles watch: " + watch);
//                }
//            }
//            else
//            {
//                float max = genericSkill.maxStock * genericSkill.finalRechargeInterval;
//                float watch = cacheStopwatch2 + stopwatch2;
//                if (watch >= max)
//                {
//                    ResetStopWatch(sec);
//                    return new Tuple<int, float>(999, 999);
//                }
                    
//                else
//                {
//                    int a = Mathf.FloorToInt(watch / genericSkill.finalRechargeInterval);
//                    float b = watch % genericSkill.finalRechargeInterval;
//                    Debug.Log("new powerbomb stock: " + a);
//                    Debug.Log("new powerbomb recharge: " + b);
//                    ResetStopWatch(sec);
//                    return new Tuple<int, float>(Mathf.FloorToInt(watch / genericSkill.finalRechargeInterval), watch % genericSkill.finalRechargeInterval);
//                    //genericSkill.stock = Mathf.FloorToInt(watch / genericSkill.finalRechargeInterval);
//                    //genericSkill.rechargeStopwatch = watch % genericSkill.finalRechargeInterval;
//                    //Debug.Log("power bomb max: " + max);
//                    //Debug.Log("powerbomb ab: " + watch);
//                }
//            }
//            //genericSkill.RecalculateValues();
            
//        }

//        public static void ResetStopWatch(bool sec)
//        {
//            if (sec)
//            {
//                stopwatch = 0f;
//                cacheStopwatch = 0f;
//                maxStopwatch = 0f;
                
//                Debug.Log("reset missile stopwatch");
//            }

//            else
//            {
//stopwatch2 = 0f;
//                cacheStopwatch2 = 0f;
//                maxStopwatch2 = 0f;
//                Debug.Log("reset powerbomb stopwatch");
//            }
                
//        }
    }
}
