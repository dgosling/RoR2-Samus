using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SamusMod.Misc
{
    public class hudDeco : MonoBehaviour
    {
        hudChangingColors.hudColors hudColors;
        Quaternion rot;
        Vector3 offset, camPos, basePosition;
        bool visibility;
        Camera camera;
        List<GameObject> pivot, decos, tickdec0s;
        GameObject frame;

        // Start is called before the first frame update
        void Start()
        {
            hudColors = new hudChangingColors.hudColors(true);
            camera = transform.parent.gameObject.GetComponent<Camera>();
            camPos = camera.transform.localPosition;
            string combatVisorName = "";
            string combatHudName = "";
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).name.StartsWith("combatVisor"))
                    combatVisorName = gameObject.transform.GetChild(i).name;
                else if (gameObject.transform.GetChild(i).name.StartsWith("combatHud"))
                    combatHudName = gameObject.transform.GetChild(i).name;
            }
            pivot = new List<GameObject>();
            decos = new List<GameObject>();
            frame = gameObject.transform.Find(combatVisorName + "/BaseWidget_Pivot/basewidget_nonfunctional/basewidget_deco/basewidget_frame/model_frame/CMDL_E64E5DBA").gameObject;
            for (int i = 0; i < frame.GetComponent<MeshRenderer>().materials.Length; i++)
            {
                frame.GetComponent<MeshRenderer>().materials[i].color *= hudColors.hudFrameColor;
            }
            decos.Add(frame);

            decos.Add(gameObject.transform.Find(combatHudName + "/energy").gameObject);
            pivot.Add(decos[0]);
            pivot.Add(decos[1]);
            tickdec0s = new List<GameObject>
        {
            gameObject.transform.Find(combatHudName+"/Missiles/Mtick").gameObject,
            gameObject.transform.Find(combatHudName+"/Envirorment/Entick").gameObject
        };
            pivot.Add(tickdec0s[0]);
            pivot.Add(tickdec0s[1]);
            for (int i = 0; i < tickdec0s.Count; i++)
            {
                tickdec0s[i].GetComponent<Image>().color = hudColors.tickDecoColor;
            }
            basePosition = frame.transform.localPosition;
            SetIsVisible(true);
        }

        void updateVisibility()
        {
            bool vis = visibility;
            for (int i = 0; i < decos.Count; i++)
            {
                decos[i].SetActive(vis);
            }
            for (int i = 0; i < tickdec0s.Count; i++)
            {
                tickdec0s[i].SetActive(vis);
            }
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
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}