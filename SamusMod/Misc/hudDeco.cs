using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SamusMod.Misc
{
    public class hudDeco : MonoBehaviour
    {
        hudColors hudColors;
        Quaternion rot;
        Vector3 offset, camPos, basePosition;
        bool visibility;
        //Camera camera;
        List<GameObject> pivot, decos, tickdec0s;
        GameObject frame;
        GameObject combatHud;
        public bool init;
        public bool changedColor;
        // Start is called before the first frame update
        void Awake()
        {
            hudColors = new hudColors(true);
            changedColor = false;
            string combatVisorName = "combatVisor";
            string combatHudName = "combatHud";
            //for (int i = 0; i < gameObject.transform.childCount; i++)
            //{
            //    if (gameObject.transform.GetChild(i).name.StartsWith("combatVisor"))
            //        combatVisorName = gameObject.transform.GetChild(i).name;
            //    else if (gameObject.transform.GetChild(i).name.StartsWith("combatHud"))
            //        combatHudName = gameObject.transform.GetChild(i).name;
            //}
            pivot = new List<GameObject>();
            decos = new List<GameObject>();
            combatHud = gameObject.transform.Find(combatHudName).gameObject;
            frame = gameObject.transform.Find(combatVisorName + "/BaseWidget_Pivot/basewidget_nonfunctional/basewidget_deco/basewidget_frame/model_frame/CMDL_E64E5DBA").gameObject;
            
            for (int i = 0; i < frame.GetComponent<MeshRenderer>().materials.Length; i++)
            {
                frame.GetComponent<MeshRenderer>().materials[i].color *= hudColors.hudFrameColor;
                
            }
            decos.Add(frame);
            foreach (Transform item in combatHud.GetComponentsInChildren<Transform>())
            {
                if (item.name.StartsWith("energyLetter"))
                    decos.Add(item.gameObject);
            }

            //decos.Add(gameObject.transform.Find(combatHudName + "/energy").gameObject);
            pivot.AddRange(decos);
            
            tickdec0s = new List<GameObject>
        {
            gameObject.transform.Find(combatHudName+"/Missiles/Mtick").gameObject,
            gameObject.transform.Find(combatHudName+"/Envirorment/Entick").gameObject
        };
            pivot.AddRange(tickdec0s);
            
            for (int i = 0; i < tickdec0s.Count; i++)
            {
                tickdec0s[i].GetComponent<Image>().color = hudColors.tickDecoColor;
            }
            basePosition = frame.transform.localPosition;
            SetIsVisible(true);
            init = true;
        }

        void updateVisibility()
        {
            bool vis = visibility;
            for (int i = 0; i < pivot.Count; i++)
            {
                pivot[i].SetActive(vis);
            }
            combatHud.SetActive(vis);
            if (combatHud.GetComponent<Canvas>().scaleFactor != 0.65f)
                combatHud.GetComponent<Canvas>().scaleFactor = 0.65f;
        }

        public void SetIsVisible(bool v)
        {
            visibility = v;
            updateVisibility();
        }

        public void SetHUDRotation(Quaternion Rot) { rot = Rot; }
        public void SetHUDOffset(Vector3 off) { offset = off; }
        public void SetFrameColorValue(float v)
        {
            Color color = v > 0f ? Color.white : hudColors.hudFrameColor;
            Material[] mat = frame.GetComponent<MeshRenderer>().materials;
            for (int i = 0; i < mat.Length; i++)
            {
                frame.GetComponent<MeshRenderer>().materials[i].color = color;
            }
            changedColor = true;
        }
        // Update is called once per frame
        public bool isVisible(bool init)
        {
            if (init)
                return true;
            if (visibility)
                return true;
            else
                return false;
        }
        void Update()
        {

        }
        public void SetSize()
        {
            Camera camera = gameObject.transform.root.GetComponentInChildren<Camera>();
            float dist = Vector3.Distance(camera.transform.position, combatHud.transform.position);
            float scale = 1.5f * dist * Mathf.Tan(Mathf.Deg2Rad * (camera.fieldOfView * 0.5f));
            GameObject basewidget = gameObject.transform.Find("combatVisor/BaseWidget_Pivot/basewidget_nonfunctional").gameObject;
            GameObject ball = gameObject.transform.Find("ballHUD").gameObject;
            GameObject boss = gameObject.transform.Find("bossHud").gameObject;
            Vector3 widgetScale = basewidget.transform.localScale;
            Vector3 ballScale = ball.transform.localScale;
            Vector3 bossScale = boss.transform.localScale;
            Vector3 hudScale = combatHud.transform.localScale;
            basewidget.transform.localScale = widgetScale*scale;
            combatHud.transform.localScale = hudScale * scale;
            ball.transform.localScale = ballScale * scale;
            boss.transform.localScale=bossScale*scale;
        }
        public GameObject GetFrame() { return frame; }
    }
}