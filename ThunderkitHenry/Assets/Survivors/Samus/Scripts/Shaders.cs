
using System.Collections.Generic;
using UnityEngine;

namespace SamusMod.Modules
{
    public class Shaders
    {
        internal static List<Material> materialStorage = new List<Material>();

        internal static void init()
        {

                ConvertCloudMaterials(Assets.beamghost.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material);
            ConvertCloudMaterials(Assets.cBeam.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material);

            ConvertBundleMats(Assets.mainAssetBundle);
            CreateMaterialStorage(Modules.Assets.mainAssetBundle);
        }

        // Uses StubbedShaderConverter to convert all materials within the Asset Bundle on Awake().
        // This now sorts out CloudRemap materials as well! Woo!
        private static void ConvertCloudMaterials(Material inAssetBundle)
        {
            string temp;
            Shaders.nameConversion.TryGetValue(inAssetBundle.shader.name,out temp);
            inAssetBundle.shader = RoR2.LegacyShaderAPI.Find(temp);
            if (inAssetBundle.GetFloat("_SrcBlend") == 0f)
            {
                inAssetBundle.SetFloat("_SrcBlend", 1f);
            }

            if (inAssetBundle.GetFloat("_DstBlend") == 0f)
            {
                inAssetBundle.SetFloat("_DstBlend", 1f);
            }
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
            {
                if (material.shader.name.StartsWith("StubbedShader") && material != Assets.beamghost.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material)
                {
                    string newName; 
                    Shaders.nameConversion.TryGetValue(material.shader.name,out newName);
                    material.shader = RoR2.LegacyShaderAPI.Find(newName);
                }
            }

                        
            
        }
        //private static void ConvertCloudMaterials(Material inAssetBundle,bool debug)


        private static Dictionary<string, string> nameConversion = new Dictionary<string, string>()
        {
            ["StubbedShader/Deferred/Standard"] = "Hopoo Games/Deferred/Standard",
            ["StubbedShader/UI/Default Overbrighten"]= "Hopoo Games/UI/Default Overbrighten",
            ["stubbed_Hopoo Games/FX/Cloud Remap Proxy"]= "Hopoo Games/FX/Cloud Remap"
        };
        public static Material GetMaterialFromStorage(string matName)
        {
            return materialStorage.Find(x => x.name == matName);
        }
    }
}
