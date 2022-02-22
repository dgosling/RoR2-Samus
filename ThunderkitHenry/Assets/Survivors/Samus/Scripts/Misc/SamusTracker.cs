using RoR2;
using UnityEngine;
using System.Linq;
using RoR2.Skills;

namespace SamusMod.Misc
{
    public class SamusTracker : MonoBehaviour
    {
        public float maxTrackingDistance = 40f;
        public float maxTrackingAngle = 45f;
        public float trackerUpdateFrequency = 10f;
        private HurtBox trackingTarget;
        private CharacterBody CharacterBody;
        private TeamComponent TeamComponent;
        private InputBankTest InputBank;
        private float trackerUpdateStopwatch;
        private Indicator Indicator;
        private readonly BullseyeSearch search = new BullseyeSearch();
        private SkillLocator SkillLocator;

        private void Awake() => this.Indicator = new Indicator(this.gameObject, Modules.Assets.Tracker);

        private void Start()
        {
            this.CharacterBody = this.GetComponent<CharacterBody>();
            this.InputBank = this.GetComponent<InputBankTest>();
            this.TeamComponent = this.GetComponent<TeamComponent>();
            this.SkillLocator = this.CharacterBody.skillLocator;

            if (this.SkillLocator.secondary.skillNameToken != "DG_SAMUS_SECONDARY_TMISSILE_NAME")
            {
                enabled = false;
            }
        }

        public HurtBox GetTrackingTarget() => this.trackingTarget;

        private void OnEnable() => this.Indicator.active = true;

        private void OnDisable() => this.Indicator.active = false;

        private void OnDestroy() => this.Indicator.active = false;

        private void FixedUpdate()
        {
            this.trackerUpdateStopwatch += Time.fixedDeltaTime;
            if (this.trackerUpdateStopwatch < 1 / this.trackerUpdateFrequency)
                return;
            this.trackerUpdateStopwatch -= 1f / this.trackerUpdateFrequency;
            if (VRAPI.Utils.IsUsingMotionControls(CharacterBody))
            {
                SearchForTarget(VRAPI.MotionControls.dominantHand.aimRay);
            }
            else
                this.SearchForTarget(new Ray(this.InputBank.aimOrigin, this.InputBank.aimDirection));
            this.Indicator.targetTransform = this.trackingTarget ? this.trackingTarget.transform : null;
        }

        private void SearchForTarget(Ray aimRay)
        {
            this.search.teamMaskFilter = TeamMask.GetUnprotectedTeams(this.TeamComponent.teamIndex);
            this.search.filterByLoS = true;
            this.search.searchOrigin = aimRay.origin;
            this.search.searchDirection = aimRay.direction;
            this.search.sortMode = BullseyeSearch.SortMode.Distance;
            this.search.maxDistanceFilter = this.maxTrackingDistance;
            this.search.maxAngleFilter = this.maxTrackingAngle;
            this.search.RefreshCandidates();
            this.search.FilterOutGameObject(this.gameObject);
            this.trackingTarget = this.search.GetResults().FirstOrDefault<HurtBox>();
        }

    }
}
