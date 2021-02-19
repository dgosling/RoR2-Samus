using RoR2;
using System.Runtime.CompilerServices;
using UnityEngine;
namespace SamusMod.Misc
{
    class SuperMissileController : MonoBehaviour
    {
        private CharacterBody characterBody;
        private SkillLocator skillLocator;
        private int missiles;

        public void Start()
        {
            this.characterBody = base.gameObject.GetComponent<CharacterBody>();
            this.skillLocator = this.characterBody.skillLocator;
            calcSMissiles();
        }

        private void calcSMissiles()
        {
            missiles = skillLocator.secondary.stock;
            if (missiles >= 5)
            {
                skillLocator.special.maxStock = 1;
            }
            else
                skillLocator.special.maxStock = 0;
            skillLocator.special.RecalculateMaxStock();
            Debug.Log("calculated super missiles");
        }
    }
}
