using System.Reflection;
using R2API;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using RoR2;
using RoR2.Projectile;
using RoR2.Orbs;

namespace SamusMod.Modules
{
public static class Assets    
    
    {
        //bundle
        public static AssetBundle mainAssetBundle;
        //Character portrait
        public static Texture charPortrait;
        //skill icons
        public static Sprite iconP;
        public static Sprite icon1;
        public static Sprite icon2;
        public static Sprite icon3;
        public static Sprite icon4;
        //Projectile ghosts
        public static GameObject beam;
        public static GameObject cbeam;
        public static GameObject missile;
        public static GameObject smissile;
        public static GameObject beamTrail;
        public static GameObject bomb;

        //skin meshes
        public static Mesh body;
        public static Mesh ball;


        public static void PopulateAssets()
        {
            if(mainAssetBundle == null)
            {
                using(var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SamusMod.samusbundle"))
                {
                    mainAssetBundle = AssetBundle.LoadFromStream(assetStream);
                    var provider = new AssetBundleResourcesProvider("@Samus", mainAssetBundle);
                    ResourcesAPI.AddProvider(provider);
                }
                using(Stream manifestResourceStream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream("SamusMod.Samus.bnk"))
                {
                    byte[] array = new byte[manifestResourceStream2.Length];
                    manifestResourceStream2.Read(array, 0, array.Length);
                    SoundAPI.SoundBanks.Add(array);
                }
            }
            #region Icons
            charPortrait = mainAssetBundle.LoadAsset<Texture>("texSamusIcon");

            iconP = mainAssetBundle.LoadAsset<Sprite>("skillP");
            icon1 = mainAssetBundle.LoadAsset<Sprite>("skill");
            icon2 = mainAssetBundle.LoadAsset<Sprite>("skill2");
            icon3 = mainAssetBundle.LoadAsset<Sprite>("skill3");
            icon4 = mainAssetBundle.LoadAsset<Sprite>("skill4");
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
            body = mainAssetBundle.LoadAsset<Mesh>("meshSamus");
            ball = mainAssetBundle.LoadAsset<Mesh>("meshBall");
            
            #endregion

           // InitCustomItems();
        }

        //public static GameObject CreateCBeam(Vector3 vector3)
        //{
        //    GameObject Chargebeam = mainAssetBundle.LoadAsset<GameObject>("cbeamproj");
        //    Chargebeam.GetComponent<Transform>().localScale = vector3;
        //    return Chargebeam;
        //}

        private static void InitCustomItems()
        {

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
            newEffect.AddComponent<OrbEffect>();
            var effect = newEffect.AddComponent<EffectComponent>();
            effect.applyScale = false;
            effect.effectIndex = EffectIndex.Invalid;
            effect.parentToReferencedTransform = true;
            effect.positionAtReferencedTransform = true;
            effect.soundName = soundName;

            EffectAPI.AddEffect(newEffect);

            return newEffect;
        }

        private static GameObject orbLoadEffect(string resourceName, string soundName)
        {
            GameObject newEffect = mainAssetBundle.LoadAsset<GameObject>(resourceName);

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


    }
}
