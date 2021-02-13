using UnityEngine;

namespace SamusMod.States
{
     public class ChargeBeam : ChargeBeamBase
    {
        private GameObject chargeEffect;
        private Vector3 originalScale;
        public static Vector3 newSize;
       
      
        public override void OnEnter()
        {
            this.baseDuration = base.baseDuration;
            this.chargeEffectPrefab = SamusMod.Modules.Assets.beam;
            this.chargeSoundString = "";
            this.crosshairOverridePrefab = Resources.Load<GameObject>("Prefabs/Crosshair/ToolbotGrenadeLauncherCrosshair");
            this.maxBloomRadius = .1f;
            this.minBloomRadius = 1f;
            this.originalScale = this.chargeEffectPrefab.transform.localScale;
            base.OnEnter();

            


        }
        public static Vector3 getSize()
        {
            Vector3 vector3 = newSize;
            return vector3;
        }
        //public override float GetCharge()
        //{
        //    return base.GetCharge();
        //}

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            newSize = new Vector3(originalScale.x + (calcCharge() - .01f), originalScale.y + (calcCharge() - .01f), originalScale.z + (calcCharge() - .01f));

            this.chargeEffectInstance.transform.localScale = newSize;
        }
        public override void OnExit()
        {
            
            base.OnExit();
            

            //this.chargeEffectPrefab.transform.localScale = new Vector3(.1f,.1f,.1f);
        }

        protected override BaseFireBeam GetNextState()
        {
            return new FireBeam();
        }
    }


    
}
