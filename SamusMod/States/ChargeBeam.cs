using UnityEngine;

namespace SamusMod.States
{
     public class ChargeBeam : ChargeBeamBase
    {
        private GameObject chargeEffect;
        private Vector3 originalScale;
       
      
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

        public override float GetCharge()
        {
            return base.GetCharge();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            
            
            this.chargeEffectInstance.transform.localScale = originalScale + (new Vector3 (this.calcCharge(),this.calcCharge(),this.calcCharge()) - new Vector3(.1f, .1f, .1f));
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
