﻿using EntityStates;
using R2API;
using RoR2;
using RoR2.ContentManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Path = System.IO.Path;
using VRAPI;

namespace SamusMod.Modules
{
    internal static class Assets
    {
		//The file name of your asset bundle
		internal const string assetBundleName = "SamusAssets";
		internal const string VRAssetBundleName = "SamusVRBundle";
		
		//Should be the same name as your SerializableContentPack in the asset bundle
		internal const string contentPackName = "SamusContentPack";

		//Name of the your soundbank file, if any.
		internal const string soundBankName = "Samus"; //HenryBank
		public static GameObject Missile;
		public static GameObject altMissile;
		public static GameObject sMissile;
		public static GameObject beam;
		public static GameObject bomb;
		public static GameObject beamghost;



		public static GameObject VRDomHand;
		public static GameObject VRnDomHand;
		public static GameObject HUDHandler;
		public static GameObject combatVisor;
		public static GameObject combatHUD;
		public static GameObject ballHUD;
		public static GameObject bossHUD;
		public static RuntimeAnimatorController gun;
		public static RuntimeAnimatorController ray;

		internal static AssetBundle mainAssetBundle = null;

		internal static ContentPack mainContentPack = null;

		internal static SerializableContentPack serialContentPack = null;
		internal static GameObject Tracker;
		
		internal static List<EffectDef> effectDefs = new List<EffectDef>();

        internal static void Init()
        {
            if (assetBundleName == "myassetbundle2")
            {
                Debug.LogError(SamusPlugin.MODNAME + ": AssetBundle name hasn't been changed. Not loading any assets to avoid conflicts.");
				SamusPlugin.cancel = true;
                return;
            }
			if (Modules.Config.characterEnabled.Value == false)
			{
				Debug.LogFormat(SamusPlugin.MODNAME + ": Character enabled config value is false. Not loading mod.");
				SamusPlugin.cancel = true;
				return;
			}
			
			LoadAssetBundle();
            LoadSoundBank();
            PopulateAssets();
        }

		// Any extra asset stuff not handled or loaded by the Asset Bundle should be sorted here.
		// This is also a good place to set up any references, if you need to.
		// References within SkillState scripts can be done through EntityStateConfigs instead.
		internal static void PopulateAssets()
		{
			if (!mainAssetBundle)
			{
				Debug.LogError(SamusPlugin.MODNAME + ": AssetBundle not found. Unable to Populate Assets.");
				SamusPlugin.cancel = true;
				return;
			}
			Tracker = mainAssetBundle.LoadAsset<GameObject>("samusTrackingIndicator");
			Missile = mainContentPack.projectilePrefabs.Find("SamusMissile");
			//Missile = (GameObject)result;
            if (Missile)
            {
				Missile.GetComponent<RoR2.Projectile.ProjectileSingleTargetImpact>().impactEffect = Resources.Load<GameObject>("prefabs/effects/impacteffects/missileexplosionvfx");
				GameObject refer = Resources.Load<GameObject>("prefabs/projectiles/missileprojectile");
				
				AkEvent[] akEvents = refer.GetComponents<AkEvent>();
				AkGameObj akGameObj1 = Missile.AddComponent<AkGameObj>();
				akGameObj1 = refer.GetComponent<AkGameObj>();
                foreach (AkEvent item in akEvents)
                {
					AkEvent b = Missile.AddComponent<AkEvent>();
					b = item;
                }
            }
			//mainContentPack.FindAsset("projectilePrefabs", "SamusaltMissile", out object result2);
			altMissile =mainContentPack.projectilePrefabs.Find("SamusaltMissile");
            if (altMissile)
            {
				altMissile.GetComponent<RoR2.Projectile.ProjectileSingleTargetImpact>().impactEffect = Resources.Load<GameObject>("prefabs/effects/impacteffects/MissileExplosionVFX");
				GameObject refer = Resources.Load<GameObject>("prefabs/projectiles/engiharpoon");
				AkGameObj akGameObj = altMissile.AddComponent<AkGameObj>();
				AkEvent akEvent = altMissile.AddComponent<AkEvent>();
				akGameObj = refer.GetComponent<AkGameObj>();
				akEvent = refer.GetComponent<AkEvent>();
			}
			//mainContentPack.FindAsset("projectilePrefabs", "SamusSuperMissile", out object res);
			sMissile = mainContentPack.projectilePrefabs.Find("SamusSuperMissile");
            if (sMissile)
            {
				sMissile.GetComponent<RoR2.Projectile.ProjectileImpactExplosion>().impactEffect = Resources.Load<GameObject>("Prefabs/Effects/ImpactEffects/ExplosionVFX");
				GameObject refer = Resources.Load<GameObject>("prefabs/projectiles/mageicebolt");
				AkEvent akEvent = sMissile.AddComponent<AkEvent>();
				akEvent = refer.GetComponent<AkEvent>();
			}
			//mainContentPack.FindAsset("projectilePrefabs", "SamusBeam", out object ree);
			beam = mainContentPack.projectilePrefabs.Find("SamusBeam");
            if (beam)
            {
				GameObject refer = Resources.Load<GameObject>("prefabs/projectiles/mageicebolt");
				AkEvent akEvent = beam.AddComponent<AkEvent>();
				akEvent = refer.GetComponent<AkEvent>();
			}
			beamghost = beam.GetComponent<RoR2.Projectile.ProjectileController>().ghostPrefab;
			bomb = mainContentPack.projectilePrefabs.Find("SamusBomb");
			if (bomb)
            {
				bomb.GetComponent<SphereCollider>().material = Resources.Load<GameObject>("Prefabs/Projectiles/CommandoGrenadeProjectile").GetComponent<SphereCollider>().material;
				bomb.GetComponent<RoR2.Projectile.ProjectileImpactExplosion>().impactEffect = Resources.Load<GameObject>("prefabs/effects/omnieffect/omniexplosionvfxcommandogrenade");
				bomb.GetComponent<RoR2.Projectile.ProjectileImpactExplosion>().lifetimeExpiredSound = Resources.Load<RoR2.NetworkSoundEventDef>("networksoundeventdefs/nsecommandogrenadebounce");
            }
			bool a;
			if (mainContentPack.projectilePrefabs.Find("SamusBeam").GetComponent<AkEvent>() != null)
				a = true;
			else
				a = false;
			//Debug.Log(a);

			if (VRAPI.VR.enabled)
			{
				VRAPI.VR.PreventRendererDisable("DGSamusBody", "ball2Mesh");
				VRDomHand = mainAssetBundle.LoadAsset<GameObject>("samusGun");
				MotionControls.AddHandPrefab(VRDomHand);
				VRnDomHand = mainAssetBundle.LoadAsset<GameObject>("samusHand");
				MotionControls.AddHandPrefab(VRnDomHand);
				//gun = VRassets.LoadAsset<RuntimeAnimatorController>("gun");
				//ray = VRassets.LoadAsset<RuntimeAnimatorController>("ray");
				combatVisor = mainAssetBundle.LoadAsset<GameObject>("combatVisor");
				combatHUD = mainAssetBundle.LoadAsset<GameObject>("combatHud");
				ballHUD = mainAssetBundle.LoadAsset<GameObject>("ballHUD");
				bossHUD = mainAssetBundle.LoadAsset<GameObject>("bossHud");
				HUDHandler = mainAssetBundle.LoadAsset<GameObject>("hudHandler");

			}
		}

		// Loads the AssetBundle, which includes the Content Pack.
		internal static void LoadAssetBundle()
        {
            if (mainAssetBundle == null)
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                mainAssetBundle = AssetBundle.LoadFromFile(Path.Combine(path, assetBundleName));

				if (!mainAssetBundle) 
				{
					Debug.LogError(SamusPlugin.MODNAME + ": AssetBundle not found. File missing or assetBundleName is incorrect.");
					SamusPlugin.cancel = true;
					return;
				}
				LoadContentPack();
			}

        }

		// Sorts out ContentPack related shenanigans.
		// Sets up variables for reference throughout the
		// mod and initializes a new content pack based on the SerializableContentPack.
		internal static void LoadContentPack()
        {
			serialContentPack = mainAssetBundle.LoadAsset<SerializableContentPack>(contentPackName);
			mainContentPack = serialContentPack.CreateContentPack();

			//AddEntityStateTypes();
			CreateEffectDefs();
			ContentPackProvider.contentPack = mainContentPack;
		}


		// Loads the sound bank for any custom sounds. 
        internal static void LoadSoundBank()
        {
			if (soundBankName == "mysoundbank")
			{
				Debug.LogError(SamusPlugin.MODNAME + ": SoundBank name hasn't been changed - not loading SoundBank to avoid conflicts.");
				return;
			}

			if (soundBankName == "")
            {
				Debug.LogFormat(SamusPlugin.MODNAME + ": SoundBank name is blank. Skipping loading SoundBank.");
				return;

			}

			//using (Stream manifestResourceStream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream(SamusPlugin.MODNAME + "." + soundBankName +".bnk"))
			//{
			//	byte[] array = new byte[manifestResourceStream2.Length];
			//	manifestResourceStream2.Read(array, 0, array.Length);
			//	SoundAPI.SoundBanks.Add(array);
			//}
			var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			SoundAPI.SoundBanks.Add(Path.Combine(path, soundBankName + ".bnk"));
		}

		// Gathers all GameObjects with VFXAttributes attached and creates an EffectDef for each one.
		// Without this, the Effect is unable to be spawned.
		// Any VFX elements must have a NetWorkIdentity, VFXAttributes and EffectComponent on the base in order to be usable.
		internal static void CreateEffectDefs()
        {
			List<GameObject> effects = new List<GameObject>();

			GameObject[] assets = mainAssetBundle.LoadAllAssets<GameObject>();
			foreach (GameObject g in assets)
            {
				if (g.GetComponent<EffectComponent>())
                {
					effects.Add(g);
                }
            }
			foreach (GameObject g in effects)
            {
				EffectDef def = new EffectDef();
				def.prefab = g;

				effectDefs.Add(def);
            }

			mainContentPack.effectDefs.Add(effectDefs.ToArray());
        }



		// Finds all Entity State Types within the mod and adds them to the content pack.
		// Saves fuss of having to add them manually. Credit to KingEnderBrine for this code.
		internal static void AddEntityStateTypes()
        {
			mainContentPack.entityStateTypes.Add(((IEnumerable<System.Type>)Assembly.GetExecutingAssembly().GetTypes()).Where<System.Type>
				((Func<System.Type, bool>)(type => typeof(EntityState).IsAssignableFrom(type))).ToArray<System.Type>());

			if (SamusPlugin.debug)
			{
				foreach (Type t in mainContentPack.entityStateTypes)
				{
					Debug.Log(SamusPlugin.MODNAME + ": Added EntityStateType: " + t);
				}
			}
		}
    }

	public class ContentPackProvider : IContentPackProvider
	{
		public static SerializableContentPack serializedContentPack;
		public static ContentPack contentPack;

		public static string contentPackName = null;
		public string identifier
		{
			get
			{
				return SamusPlugin.MODNAME;
			}
		}

		internal static void Initialize()
		{
			contentPackName = Assets.contentPackName;
			//contentPack = serializedContentPack.CreateContentPack();
			ContentManager.collectContentPackProviders += AddCustomContent;
		}

		private static void AddCustomContent(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
		{
			addContentPackProvider(new ContentPackProvider());
		}

		public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
		{
			args.ReportProgress(1f);
			yield break;
		}

		public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
		{
			ContentPack.Copy(contentPack, args.output);
			args.ReportProgress(1f);
			yield break;
		}

		public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
		{
			args.ReportProgress(1f);
			yield break;
		}
	}
}