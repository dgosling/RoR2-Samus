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
        GameObject Pivot, glow;
        List<GameObject> helmet, helmetLight;
        hudChangingColors.hudColors hudColors;
        string path;
        // Start is called before the first frame update
        void Start()
        {
            hudColors = new hudChangingColors.hudColors(true);
            path = "combatVisor";
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


    }
}