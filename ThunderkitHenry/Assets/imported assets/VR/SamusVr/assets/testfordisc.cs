using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoR2;
public class testfordisc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        List<Object> objects = new List<Object>();
        objects.Add(gameObject);
        objects.Add(gameObject.transform);
        objects.Add(gameObject.GetComponent<MeshCollider>());
        
        for (int i = 0; i < objects.Count; i++)
        {
            //Debug.Log(objects[i].GetType());
        }
        //Debug.Log(gameObject.transform.GetType());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
