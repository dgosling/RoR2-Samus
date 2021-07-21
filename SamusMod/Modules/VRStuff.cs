using UnityEngine;
using RoR2;
using R2API;
using VRAPI;

namespace SamusMod.Modules
{
    public class VRStuff
    {
        public static Ray domRay;
        public static Ray nonDomRay;
        public static Animator gunAnimator;
        
        public static void setupVR(CharacterBody body)
        {

            nonDomRay = MotionControls.nonDominantHand.aimRay;
            gunAnimator = MotionControls.dominantHand.animator;
            
        }
    }
}
