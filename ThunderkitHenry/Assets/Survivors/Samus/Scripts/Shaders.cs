using StubbedConverter;
using System.Collections.Generic;
using UnityEngine;

namespace SamusMod.Modules
{
    public class Shaders
    {
        internal static List<Material> materialStorage = new List<Material>();

        internal static void init()
        {
            if (SamusPlugin.debug)
                ConvertCloudMaterials(Assets.beamghost.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material, true);
            else
                ConvertCloudMaterials(Assets.beamghost.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material);

            ConvertBundleMats(Assets.mainAssetBundle);
            CreateMaterialStorage(Modules.Assets.mainAssetBundle);
        }

        // Uses StubbedShaderConverter to convert all materials within the Asset Bundle on Awake().
        // This now sorts out CloudRemap materials as well! Woo!
        private static void ConvertCloudMaterials(Material inAssetBundle)
        {
            ShaderConverter.ConvertStubbedShaders(inAssetBundle);
        }

        private static void CreateMaterialStorage(AssetBundle inAssetBundle)
        {
            Material[] tempArray = inAssetBundle.LoadAllAssets<Material>();

            materialStorage.AddRange(tempArray);
        }
        private static void ConvertBundleMats(AssetBundle assetBundle)
        {

                Material[] materials = assetBundle.LoadAllAssets<Material>();

                foreach (Material material in materials)
                    if (material.shader.name.StartsWith("StubbedShader")&&material!= Assets.beamghost.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material)
                        material.shader = Resources.Load<Shader>("shaders" + material.shader.name.Substring(13));
            
        }
        private static void ConvertCloudMaterials(Material inAssetBundle,bool debug)
        {
            ShaderConverter.ConvertStubbedShaders(inAssetBundle,debug);
        }


        public static Material GetMaterialFromStorage(string matName)
        {
            return materialStorage.Find(x => x.name == matName);
        }
    }
}
