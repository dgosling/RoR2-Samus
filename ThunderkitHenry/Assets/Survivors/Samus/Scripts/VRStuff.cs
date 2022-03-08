using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoR2;
//using VRAPI;
using SamusMod.Misc;
namespace SamusMod.Modules
{


    public class VRStuff : MonoBehaviour
    {
        public static Ray domRay;
        public static Ray nonDomRay;
        public static Animator gunAnimator;
        public static Camera hudCamera;
        public static GameObject hudHandle;
        private static Misc.SamusHUD samusHUD;

        internal static SamusHUD SamusHUD { get => samusHUD; set => samusHUD = value; }

        public static void setupVR(CharacterBody body)
        {

            //nonDomRay = MotionControls.nonDominantHand.aimRay;
            //gunAnimator = MotionControls.dominantHand.animator;
            //SceneCamera component = Camera.main.GetComponent<SceneCamera>();
            //if (Config.enableHud.Value)
            //{
            //    hudCamera = Camera.current;
            //    hudHandle = GameObject.Instantiate<GameObject>(Assets.HUDHandler);
            //    //hudHandle.transform.Find("combatVisor").localPosition = new Vector3(0, 0, -1.5f);
            //    SetAllChildrenLayer(hudHandle.transform, LayerIndex.ui.intVal);
            //    hudHandle.transform.SetParent(hudCamera.transform, false);
            //    //SetAllChildrenLayer(hudHandle.transform.Find("combatHud"), Camera.current.gameObject.layer);
            //    //SetAllChildrenLayer(hudHandle.transform.Find("ballHUD"), Camera.current.gameObject.layer);
            //    //SetAllChildrenLayer(hudHandle.transform.Find("bossHud"), Camera.current.gameObject.layer);
            //    hudHandle.transform.Find("combatHud").gameObject.GetComponent<Canvas>().worldCamera = hudCamera;
            //    //hudHandle.transform.Find("combatHud").gameObject.GetComponent<Canvas>().planeDistance = 0.5f;
            //    //hudHandle.transform.Find("combatHud").gameObject.GetComponent<Canvas>().scaleFactor = 0.65f;
            //    hudHandle.transform.Find("ballHUD").gameObject.GetComponent<Canvas>().worldCamera = hudCamera;
            //    //hudHandle.transform.Find("ballHUD").gameObject.GetComponent<Canvas>().planeDistance = 0.5f;
            //    //hudHandle.transform.Find("ballHUD").gameObject.GetComponent<Canvas>().scaleFactor = 0.5f;
            //    hudHandle.transform.Find("bossHud").gameObject.GetComponent<Canvas>().worldCamera = hudCamera;
            //    //hudHandle.transform.Find("bossHud").gameObject.GetComponent<Canvas>().planeDistance = 0.5f;
            //    SamusHUD = hudHandle.AddComponent<Misc.SamusHUD>();




            //    //Debug.Log("number of displays: "+Display.displays.Length);

            //    GameObject temp;
            //    temp = hudHandle.transform.parent.GetComponentInChildren<RoR2.UI.HUD>().gameObject;
            //    Material editedScreenDamage = new Material(hudHandle.transform.parent.GetComponent<RoR2.PostProcessing.ScreenDamage>().mat);
            //    editedScreenDamage.SetTexture("_NormalMap", null);
            //    editedScreenDamage.color = new Color(1, 1, 0.419608f, 1);
            //    editedScreenDamage.SetFloat("_BumpScale", 0);
            //    hudHandle.transform.parent.GetComponent<RoR2.PostProcessing.ScreenDamage>().mat = editedScreenDamage;
            //    if (temp.name.Contains("HUDSimple"))
            //        samusHUD.bossHealthBarRoot = temp.GetComponent<RoR2.UI.HUD>().mainUIPanel.transform.Find("SpringCanvas/TopCenterCluster/BossHealthBarRoot").gameObject;
            //    else
            //        throw new MissingReferenceException();
            //}




            //hudHandle = Assets.HUDHandler;
            //hudHandle.transform.Find("combatHud").gameObject.GetComponent<Canvas>().worldCamera = hudCamera;
            //hudHandle.transform.Find("ballHUD").gameObject.GetComponent<Canvas>().worldCamera = hudCamera;
            //hudHandle.transform.Find("bossHud").gameObject.GetComponent<Canvas>().worldCamera = hudCamera;
            //Misc.SamusHUD samusHUD = hudHandle.AddComponent<Misc.SamusHUD>();
            //GameObject refer = GameObject.Instantiate(hudHandle);
            //refer.transform.SetParent(hudCamera.transform,false);
            //refer.transform.Find("combatHud").localPosition += (Vector3.forward*2);


        }

        static void SetAllChildrenLayer(Transform root, int layer)
        {
            List<Transform> children = new List<Transform>();
            children.AddRange(root.GetComponentsInChildren<Transform>(includeInactive: true));
            foreach (Transform child in children)
            {
                child.gameObject.layer = layer;
            }
        }
    }
}
