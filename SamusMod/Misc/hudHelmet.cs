using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SamusMod.Misc
{
    public class hudHelmet : MonoBehaviour
    {
        //Vector3 pivotPosition;
        bool helmetVis, glowVis;
        //Camera camera;
        GameObject Pivot, glow,baseObject;
        List<GameObject> helmet, helmetLight;
        hudColors hudColors;
        string path;
        // Start is called before the first frame update
        void Awake()
        {
            hudColors = new hudColors(true);
            path = "combatVisor";
            baseObject = gameObject.transform.Find(path).gameObject;
            Pivot = gameObject.transform.Find(path + "/BaseWidget_Pivot").gameObject;
            helmet = new List<GameObject>();
            helmet.Add(Pivot.transform.Find("BaseWidget_Helmet/model_bottomhel").gameObject);
            helmet.Add(Pivot.transform.Find("BaseWidget_Helmet/model_tophel3").gameObject);
            glow = Pivot.transform.Find("BaseWidget_Glow").gameObject;
            helmetLight = new List<GameObject>();
            GameObject parent = Pivot.transform.Find("BaseWidget_Helmet/BaseWidget_HelmetLight").gameObject;
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in parent.transform.GetComponentsInChildren<Transform>())
            {
                if (child != parent.transform)
                    children.Add(child.gameObject);
            }

            foreach (GameObject mesh in children)
            {
                if (mesh.GetComponent<MeshRenderer>() != null)
                    helmetLight.Add(mesh);
            }
            //pivotPosition = 
            foreach (GameObject item in helmetLight)
            {
                if (item.GetComponent<MeshRenderer>().materials.Length > 1)
                {
                    foreach (Material mat in item.GetComponent<MeshRenderer>().materials)
                    {
                        mat.color = hudColors.helmetLightColor;
                    }
                }
                else if (item.GetComponent<MeshRenderer>().materials.Length == 1)
                    item.GetComponent<MeshRenderer>().material.color = hudColors.helmetLightColor;
            }
        }

        // Update is called once per frame

        public void SetSize()
        {
            Camera camera = gameObject.transform.root.GetComponentInChildren<Camera>();
            float dist = Vector3.Distance(camera.transform.position, baseObject.transform.position);
            float scale = 1.5f * dist * Mathf.Tan(Mathf.Deg2Rad * (camera.fieldOfView * 0.5f));
            GameObject helmet = Pivot.transform.Find("BaseWidget_Helmet").gameObject;
            GameObject energy = baseObject.transform.Find("basewidget_energystuff").gameObject;
            Vector3 energyScale = energy.transform.localScale;
            Vector3 helmetScale = helmet.transform.localScale;
            Vector3 glowScale = glow.transform.localScale;
            helmet.transform.localScale = helmetScale*scale;
            glow.transform.localScale = glowScale*scale;
            energy.transform.localScale = energyScale * scale;
        }

        public void AddHelmetLightValue(float val)
        {
            foreach (GameObject item in helmetLight)
            {
                if (item.GetComponent<MeshRenderer>().materials.Length > 1)
                {
                    foreach (Material mat in item.GetComponent<MeshRenderer>().materials)
                    {
                        mat.color = hudColors.helmetLightColor + new Color(val, val, val, val);
                    }
                }
                else if (item.GetComponent<MeshRenderer>().materials.Length == 1)
                    item.GetComponent<MeshRenderer>().material.color = hudColors.helmetLightColor + new Color(val, val, val, val);
            }
        }

        void UpdateVisibility()
        {
            baseObject.SetActive(helmetVis);
        }

        public void SetVisible(bool visible) 
        { 
            helmetVis = visible;
            UpdateVisibility();
        }

    }
}