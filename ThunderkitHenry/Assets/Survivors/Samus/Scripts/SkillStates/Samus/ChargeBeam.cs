using UnityEngine;
using SamusMod.SkillStates.BaseStates;
using RoR2;
using VRAPI;
namespace SamusMod.SkillStates.Samus
{


    public class ChargeBeam : ChargeBeamBase
    {
        
        private GameObject chargeEffect;

        private Vector3 originalScale;

        public Vector3 newSize;


        public override void OnEnter()
        {
            //this.baseDuration = base.baseDuration;
            
            
            this.crosshairOverridePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Crosshair/ToolbotGrenadeLauncherCrosshair");
            this.maxBloomRadius = .1f;
            this.minBloomRadius = 1f;
            this.originalScale = this.chargeEffectPrefab.transform.localScale;
            base.OnEnter();
            ChildLocator childLocator = base.GetModelChildLocator();

            //if (VRAPI.Utils.IsUsingMotionControls(this.characterBody) == true)
            //{
            //    Transform temp = MotionControls.dominantHand.transform.Find("Muzzle");
            //    chargeEffect = temp.GetChild(0).gameObject;


            //}
            if (childLocator)
            {


                chargeEffect = childLocator.FindChild("chargeEffect").gameObject;
                chargeEffect.SetActive(false);//temp
            }
            





        }
        //public static Vector3 getSize()
        //{
        //    Vector3 vector3 = newSize;
        //    return vector3;
        //}
        //public override float GetCharge()
        //{
        //    return base.GetCharge();
        //}

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.chargeEffect.activeSelf == false && this.calcCharge() > .15f)
            {
                this.chargeEffect.SetActive(true);
            }
            if (!VRAPI.Utils.IsUsingMotionControls(characterBody))
                newSize = new Vector3(calcCharge() * 10, calcCharge() * 10, calcCharge() * 10);
            else
                newSize = new Vector3(calcCharge() / 2, calcCharge() / 2, calcCharge() / 2);

            this.chargeEffectInstance.transform.localScale = newSize;
        }
        public override void OnExit()
        {

            base.OnExit();
            if (this.chargeEffect)
            {
                this.chargeEffect.SetActive(false);
            }

            //this.chargeEffectPrefab.transform.localScale = new Vector3(.1f,.1f,.1f);
        }

        protected override BaseFireBeam GetNextState()
        {
            return new FireBeam();
        }
    }
}
