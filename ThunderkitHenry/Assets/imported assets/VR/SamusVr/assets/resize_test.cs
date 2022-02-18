using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resize_test : MonoBehaviour
{
    // Start is called before the first frame update
    //[ExecuteAlways]
    [SerializeField]
    Camera Camera;
    [SerializeField]
    Transform visor, combatHud, ballHud, bossHud;
    [SerializeField]
    List<Transform> visortrans = new List<Transform>();
    float visorDistance, combatDistance, ballDistance, bossDistance;
    float vScale,cScale,bScale,bossScale;
    Vector3 combatScale, ballScale, bossoScale;
    List<Vector3> visorScales = new List<Vector3>();
    private void Awake()
    {
        combatScale = combatHud.localScale;
        
        ballScale = ballHud.localScale;
        bossoScale = bossHud.localScale;
        foreach (Transform item in visortrans)
        {
            visorScales.Add(item.localScale);
        }

    }
    void FixedUpdate()
    {
        calculateDistances();
        vScale = 1.5f * visorDistance * Mathf.Tan(Mathf.Deg2Rad * (Camera.fieldOfView * 0.5f));
        for (int i = 0; i < visortrans.Count; i++)
        {
            visortrans[i].localScale = visorScales[i] * vScale;
        }
        cScale = 1.5f * combatDistance * Mathf.Tan(Mathf.Deg2Rad * (Camera.fieldOfView * 0.5f));
        combatHud.localScale = combatScale * cScale;
        bScale = 1.5f * ballDistance * Mathf.Tan(Mathf.Deg2Rad * (Camera.fieldOfView * 0.5f));
        ballHud.localScale = ballScale * bScale;
        bossScale = 1.5f * bossDistance * Mathf.Tan(Mathf.Deg2Rad * (Camera.fieldOfView * 0.5f));
        bossHud.localScale = bossoScale * bossScale;
    }

    // Update is called once per frame
    void calculateDistances()
    {
        visorDistance = Vector3.Distance(Camera.transform.position, visor.position);
        combatDistance = Vector3.Distance(Camera.transform.position, combatHud.position);
        ballDistance = Vector3.Distance(Camera.transform.position, ballHud.position);
        bossDistance = Vector3.Distance(Camera.transform.position, bossHud.position);
    }
}
