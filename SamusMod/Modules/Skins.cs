using System;
using UnityEngine;
using R2API;
using RoR2;
using System.Collections.Generic;


namespace SamusMod.Modules
{
    public static class Skins
    {
        internal static UnlockableDef unlockableDef;
        public static SkinDef CreateSkinDef(string skinName,Sprite skinIcon,CharacterModel.RendererInfo[] rendererInfos,SkinnedMeshRenderer meshRenderer, GameObject root,string unlockName)
        {
            LoadoutAPI.SkinDefInfo skinDefInfo = new LoadoutAPI.SkinDefInfo
            {
                BaseSkins = Array.Empty<SkinDef>(),
                GameObjectActivations = new SkinDef.GameObjectActivation[0],
                Icon = skinIcon,
                MeshReplacements = new SkinDef.MeshReplacement[0],
                MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0],
                Name = skinName,
                NameToken = skinName,
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
                RendererInfos = rendererInfos,
                RootObject = root,
                UnlockableDef = unlockableDef
            };

            SkinDef skin = LoadoutAPI.CreateNewSkinDef(skinDefInfo);

            return skin;
        }

        public static SkinDef CreateSkinDef(string skinName,Sprite skinIcon, CharacterModel.RendererInfo[] rendererInfos,SkinnedMeshRenderer meshRenderer, GameObject root,string unlockName, Mesh skinMesh)
        {
            LoadoutAPI.SkinDefInfo skinDefInfo = new LoadoutAPI.SkinDefInfo
            {
                BaseSkins = Array.Empty<SkinDef>(),
                GameObjectActivations = new SkinDef.GameObjectActivation[0],
                Icon = skinIcon,
                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        renderer = meshRenderer,
                        mesh = skinMesh
                    }
                },
                MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0],
                Name = skinName,
                NameToken = skinName,
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
                RendererInfos = rendererInfos,
                RootObject = root,
                UnlockableDef = unlockableDef
            };

            SkinDef skin = LoadoutAPI.CreateNewSkinDef(skinDefInfo);
            return skin;
        }

        public static Material CreateMaterial(string materialName)
        {
            return CreateMaterial(materialName, 0);
        }

        public static Material CreateMaterial(string materialName, float emmission)
        {
            return CreateMaterial(materialName, emmission, Color.black);
        }
        public static Material CreateMaterial(string materialName,float emmision,Color emissionColor)
        {
            return CreateMaterial(materialName, emmision, emissionColor, 0);
        }
        public static Material CreateMaterial(string materialName,float emission,Color emissionColor,float normalStrength)
        {
            if (!SamusPlugin.commandoMat) SamusPlugin.commandoMat = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial;

            Material mat = UnityEngine.Object.Instantiate<Material>(SamusPlugin.commandoMat);
            Material tempMat = Assets.mainAssetBundle.LoadAsset<Material>(materialName);
            if (!tempMat)
            {
                return SamusPlugin.commandoMat;
            }
            mat.name = materialName;
            mat.SetColor("_Color", tempMat.GetColor("_Color"));
            mat.SetTexture("_MainTex", tempMat.GetTexture("_MainTex"));
            mat.SetColor("_EmColor", emissionColor);
            mat.SetFloat("_EmPower", emission);
            mat.SetTexture("_EmTex", tempMat.GetTexture("_EmissionMap"));
            mat.SetFloat("_NormalStrength", normalStrength);

            return mat;
        }

        public static void RegisterSkins()
        {
            GameObject bodyPrefab = Prefabs.samusPrefab;
            GameObject model = bodyPrefab.GetComponentInChildren<ModelLocator>().modelTransform.gameObject;
            CharacterModel characterModel = model.GetComponent<CharacterModel>();

            ModelSkinController skinController = model.AddComponent<ModelSkinController>();
            ChildLocator childLocator = model.GetComponent<ChildLocator>();

            SkinnedMeshRenderer mainRenderer = characterModel.mainSkinnedMeshRenderer;

            List<SkinDef> skinDefs = new List<SkinDef>();
            
            #region DefaultSkin
            CharacterModel.RendererInfo[] defaultRenderers = characterModel.baseRendererInfos;
            SkinDef defaultSkin = CreateSkinDef("DG_SAMUS_DEFAULT_SKIN_NAME", Assets.mainAssetBundle.LoadAsset<Sprite>("texMainSkin"), defaultRenderers, mainRenderer, model, "");
            defaultSkin.meshReplacements = new SkinDef.MeshReplacement[]
            {
                new SkinDef.MeshReplacement
                {
                    mesh = Assets.body,
                    renderer = defaultRenderers[0].renderer
                },

                new SkinDef.MeshReplacement
                {
                    mesh = Assets.ball,
                    renderer = defaultRenderers[1].renderer
                },
                new SkinDef.MeshReplacement
                {
                    mesh = Assets.ball2,
                    renderer = defaultRenderers[2].renderer
                } 

            };

            skinDefs.Add(defaultSkin);
            #endregion

            skinController.skins = skinDefs.ToArray();
        }
    }
}
