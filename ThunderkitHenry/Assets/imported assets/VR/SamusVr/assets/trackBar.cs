using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trackBar : MonoBehaviour
{   
    [SerializeField]
    public Image barTracker;
    private Transform localTransform;
    float barFill;
    // Start is called before the first frame update
    void Start()
    {
        localTransform = gameObject.transform;
        //barTracker = gameObject.transform.parent.Find("MBarBack/MBar").gameObject.GetComponent<Image>();
        barFill = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        barFill = barTracker.fillAmount;
        Vector3 localPos = localTransform.localPosition;

        float max = 342.5f;
        float min = 1;

        localPos.y = barFill * max;
        localPos.y = Mathf.Clamp(localPos.y, min, max);

        localTransform.localPosition = localPos;

    }

    
}
