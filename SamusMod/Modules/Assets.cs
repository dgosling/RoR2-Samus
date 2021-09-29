using System.Reflection;
using System;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using RoR2;
using R2API;
using RoR2.Audio;
using System.Collections.Generic;
using Rewired;
using VRAPI;
namespace SamusMod.Modules
{
public static class Assets    
    
    {
        //bundle
        public static AssetBundle mainAssetBundle;
        public static AssetBundle VRassets;
        public static uint unloadingID;
        //Character portrait
        public static Texture charPortrait;
        //skill icons
        public static Sprite iconP;
        public static Sprite icon1;
        public static Sprite icon2;
        public static Sprite icon3;
        public static Sprite icon3b;
        public static Sprite icon4;
        public static Sprite icon2b;
        //Projectile ghosts
        public static GameObject beam;
        public static GameObject cbeam;
        public static GameObject missile;
        public static GameObject smissile;
        public static GameObject beamTrail;
        public static GameObject bomb;
        //public static GameObject chargeEffect;
        public static GameObject beamShootEffect;
        public static GameObject missileEffect;
        public static GameObject beamImpactEffect;
        public static GameObject morphBomb;
        public static GameObject bombExplosion;
        public static GameObject powerbomb;
        public static GameObject powerbomb1;
        public static GameObject Tracker;
        public static GameObject VRDomHand;
        public static GameObject VRnDomHand;
        public static GameObject HUDHandler;
        public static GameObject combatVisor;
        public static GameObject combatHUD;
        public static GameObject ballHUD;
        public static GameObject bossHUD;
        public static RuntimeAnimatorController gun;
        public static RuntimeAnimatorController ray;
        internal static NetworkSoundEventDef bombExplosionSound;
        internal static NetworkSoundEventDef powerBombExplosionSound;
        

        //skin meshes
        public static Mesh body;
        public static Mesh ball;
        public static Mesh ball2;

        internal static List<EffectDef> effectDefs = new List<EffectDef>();
        internal static List<NetworkSoundEventDef> networkSoundEventDefs = new List<NetworkSoundEventDef>();

        public static void PopulateAssets()
        {
            if (mainAssetBundle == null)
            {
                //using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SamusMod.samusbundle"))
                //{
                //    mainAssetBundle = AssetBundle.LoadFromStream(assetStream);
                //    //var provider = new AssetBundleResourcesProvider("@Samus", mainAssetBundle);
                //    //ResourcesAPI.AddProvider(provider);

                //}
                mainAssetBundle = LoadAssetBundle(Properties.Resources.samusbundle);
                //using (Stream manifestResourceStream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream("SamusMod.Samus.bnk"))
                //{
                //    byte[] array = new byte[manifestResourceStream2.Length];
                //    manifestResourceStream2.Read(array, 0, array.Length);
                //    SoundAPI.SoundBanks.Add(array);
                //}
                unloadingID = LoadSoundBank(Properties.Resources.Samus);


            }

            if (VRassets == null&&VR.enabled)
            {
                //using (var vrassetstream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SamusMod.samusvr"))
                //{
                //    VRassets = AssetBundle.LoadFromStream(vrassetstream);
                //    var provider = new AssetBundleResourcesProvider("@Samus", VRassets);
                //    ResourcesAPI.AddProvider(provider);
                //}
                VRassets = LoadAssetBundle(Properties.Resources.samusvr);
            }

            #region Icons
            charPortrait = mainAssetBundle.LoadAsset<Texture>("texSamusIcon");

            iconP = mainAssetBundle.LoadAsset<Sprite>("skillP");
            icon1 = mainAssetBundle.LoadAsset<Sprite>("skill");
            icon2 = mainAssetBundle.LoadAsset<Sprite>("skill2");
            icon3 = mainAssetBundle.LoadAsset<Sprite>("skill3");
            icon3b = mainAssetBundle.LoadAsset<Sprite>("skill2b");
            icon4 = mainAssetBundle.LoadAsset<Sprite>("skill4");
            icon2b = mainAssetBundle.LoadAsset<Sprite>("skill2Track");
            #endregion

            #region ProjectileGhosts
            beam = mainAssetBundle.LoadAsset<GameObject>("beamproj");
            cbeam = mainAssetBundle.LoadAsset<GameObject>("cbeamproj");
            missile = mainAssetBundle.LoadAsset<GameObject>("missilePref");
            smissile = mainAssetBundle.LoadAsset<GameObject>("supermissilePref");
            beamTrail = mainAssetBundle.LoadAsset<GameObject>("beamTrail");
            bomb = mainAssetBundle.LoadAsset<GameObject>("bombproj");

            #endregion
            #region Meshes
            body = mainAssetBundle.LoadAsset<Mesh>("meshDGSsamus");
            ball = mainAssetBundle.LoadAsset<Mesh>("meshDGBall");
            ball2 = mainAssetBundle.LoadAsset<Mesh>("meshBall2");
            
            #endregion
            #region effects
            beamShootEffect = LoadEffect("beamFireMuzzle", "");
            //chargeEffect = LoadEffect("chargeMuzzle", "");
            beamImpactEffect = LoadEffect("beamImpact", "");
            missileEffect = LoadEffect("missileMuzzle", "");
            powerbomb1 = mainAssetBundle.LoadAsset<GameObject>("powerBombExplosion1");
            powerbomb = LoadEffect("powerBombExplosion", Sounds.powerBomb);
            //powerbomb.GetComponent<DestroyOnTimer>().duration = 20;
            powerbomb.GetComponent<EffectComponent>().applyScale = true;
            //powerbomb.GetComponent<EffectComponent>().parentToReferencedTransform = false;
            //powerbomb.GetComponent<EffectComponent>().positionAtReferencedTransform = false;
            morphBomb = mainAssetBundle.LoadAsset<GameObject>("morphBomb");
            bombExplosion = LoadEffect("bombExplosion", Sounds.bombExplode);
            Tracker = mainAssetBundle.LoadAsset<GameObject>("samusTrackingIndicator");
            InitTracker();

            bombExplosionSound = CreateNetworkSoundEventDef(Sounds.bombExplode);
            powerBombExplosionSound = CreateNetworkSoundEventDef(Sounds.powerBomb);
            
            #endregion
            #region vrprefabs
            if (VRAPI.VR.enabled) 
            { 
            VRAPI.VR.PreventRendererDisable("dgoslingSamusBody", "ball2Mesh");
            VRDomHand = VRassets.LoadAsset<GameObject>("samusGun");
            MotionControls.AddHandPrefab(VRDomHand);
            VRnDomHand = VRassets.LoadAsset<GameObject>("samusHand");
            MotionControls.AddHandPrefab(VRnDomHand);
                //gun = VRassets.LoadAsset<RuntimeAnimatorController>("gun");
                //ray = VRassets.LoadAsset<RuntimeAnimatorController>("ray");
                combatVisor = VRassets.LoadAsset<GameObject>("combatVisor");
                combatHUD = VRassets.LoadAsset<GameObject>("combatHud");
                ballHUD = VRassets.LoadAsset<GameObject>("ballHUD");
                bossHUD = VRassets.LoadAsset<GameObject>("bossHud");
                HUDHandler = VRassets.LoadAsset<GameObject>("hudHandler");
                
            }

            #endregion

            // InitCustomItems();
        }
        static AssetBundle LoadAssetBundle(Byte[] resourceBytes)
        {
            //Check to make sure that the byte array supplied is not null, and throw an appropriate exception if they are.
            if (resourceBytes == null) throw new ArgumentNullException(nameof(resourceBytes));

            //Actually load the bundle with a Unity function.
            var bundle = AssetBundle.LoadFromMemory(resourceBytes);

            return bundle;
        }

        static UInt32 LoadSoundBank(Byte[] resourceBytes)
        {
            //Check to make sure that the byte array supplied is not null, and throw an appropriate exception if they are.
            if (resourceBytes == null) throw new ArgumentNullException(nameof(resourceBytes));

            //Register the soundbank and return the ID
            return SoundAPI.SoundBanks.Add(resourceBytes);
        }
        internal static NetworkSoundEventDef CreateNetworkSoundEventDef(string eventName)
        {
            NetworkSoundEventDef networkSoundEventDef = ScriptableObject.CreateInstance<NetworkSoundEventDef>();
            networkSoundEventDef.akId = AkSoundEngine.GetIDFromString(eventName);
            networkSoundEventDef.eventName = eventName;

            networkSoundEventDefs.Add(networkSoundEventDef);

            return networkSoundEventDef;
        }


        //public static GameObject CreateCBeam(Vector3 vector3)
        //{
        //    GameObject Chargebeam = mainAssetBundle.LoadAsset<GameObject>("cbeamproj");
        //    Chargebeam.GetComponent<Transform>().localScale = vector3;
        //    return Chargebeam;
        //}

        private static void InitTracker()
        {
            GameObject HuntressTracker = Resources.Load<GameObject>("Prefabs/HuntressTrackingIndicator");
            Rewired.ComponentControls.Effects.RotateAroundAxis rotateAroundAxis = HuntressTracker.transform.Find("Holder").gameObject.GetComponent<Rewired.ComponentControls.Effects.RotateAroundAxis>();

           var SamusTrackerRAA = Tracker.transform.Find("Holder").gameObject.AddComponent<Rewired.ComponentControls.Effects.RotateAroundAxis>();
            SamusTrackerRAA.fastRotationSpeed = rotateAroundAxis.fastRotationSpeed;
            SamusTrackerRAA.slowRotationSpeed = rotateAroundAxis.slowRotationSpeed;
            SamusTrackerRAA.speed = rotateAroundAxis.speed;
            SamusTrackerRAA.rotateAroundAxis = rotateAroundAxis.rotateAroundAxis;
            SamusTrackerRAA.relativeTo = rotateAroundAxis.relativeTo;
            SamusTrackerRAA.reverse = rotateAroundAxis.reverse;

            ObjectScaleCurve objectScaleCurve = HuntressTracker.transform.Find("Holder").gameObject.GetComponent<ObjectScaleCurve>();

            var SamusScaleCurve = Tracker.transform.Find("Holder").gameObject.AddComponent<ObjectScaleCurve>();
            SamusScaleCurve.curveX = objectScaleCurve.curveX;
            SamusScaleCurve.curveY = objectScaleCurve.curveY;
            SamusScaleCurve.curveZ = objectScaleCurve.curveZ;
            SamusScaleCurve.overallCurve = objectScaleCurve.overallCurve;
            SamusScaleCurve.useOverallCurveOnly = objectScaleCurve.useOverallCurveOnly;
            SamusScaleCurve.timeMax = objectScaleCurve.timeMax;
        }

        private static GameObject CreateItemDisplay(string prefabName,string matName)
        {
            GameObject displayPrefab = mainAssetBundle.LoadAsset<GameObject>(prefabName);
            Material itemMat = Skins.CreateMaterial(matName, 0, Color.black, 0);
            MeshRenderer renderer = displayPrefab.GetComponent<MeshRenderer>();

            renderer.material = itemMat;
            displayPrefab.AddComponent<ItemDisplay>().rendererInfos = new CharacterModel.RendererInfo[]
            {
                new CharacterModel.RendererInfo
                {
                    defaultMaterial = itemMat,
                    renderer = renderer,
                    ignoreOverlays = false,
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
                }
            };

            return displayPrefab;
        }

        private static GameObject LoadEffect(string resourceName,string soundName)
        {
            GameObject newEffect = mainAssetBundle.LoadAsset<GameObject>(resourceName);

            newEffect.AddComponent<DestroyOnTimer>().duration = 12;
            newEffect.AddComponent<NetworkIdentity>();
            newEffect.AddComponent<VFXAttributes>().vfxPriority = VFXAttributes.VFXPriority.Always;
            
            var effect = newEffect.AddComponent<EffectComponent>();
            effect.applyScale = false;
            effect.effectIndex = EffectIndex.Invalid;
            effect.parentToReferencedTransform = true;
            effect.positionAtReferencedTransform = true;
            effect.soundName = soundName;


            EffectAPI.AddEffect(newEffect);


            return newEffect;
        }

        private static void AddEffect(GameObject effectPrefab)
        {
            AddEffect(effectPrefab, "");
        }

        private static void AddEffect(GameObject effectPrefab,string soundName)
        {
            EffectDef newEffectDef = new EffectDef();
            newEffectDef.prefab = effectPrefab;
            newEffectDef.prefabEffectComponent = effectPrefab.GetComponent<EffectComponent>();
            newEffectDef.prefabName = effectPrefab.name;
            newEffectDef.prefabVfxAttributes = effectPrefab.GetComponent<VFXAttributes>();
            newEffectDef.spawnSoundEventName = soundName;

            
        }




    }
}
