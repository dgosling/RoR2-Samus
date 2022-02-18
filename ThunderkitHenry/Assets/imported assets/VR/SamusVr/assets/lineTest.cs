using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

[RequireComponent(typeof(LineRenderer))] // without a LineRenderer, this script is pointless
public class lineTest : MonoBehaviour
    {
    
    [ExecuteAlways] // makes it work in the editor
    [SerializeField]
        private int numSegments = 30; // quality setting - the higher the better it looks in close-ups
        [SerializeField]
        [Range(0f, 1f)]
        private float fillState=1; // how full the bar is
    private float offsetAmount;
    public float offSet
    {
        get => offsetAmount;
        set
        {
            offsetAmount = value;
        }
    }

        public float FillState
        {
            get => fillState;
            set
            {
                fillState = value;
                RecalculatePoints();
            }
        }
    


    public void OnValidate() => RecalculatePoints();

        // Called when you change something in the inspector 
        // or change the FillState via another script
        private void RecalculatePoints()
        {
            //// calculate the positions of the points
            //float angleIncrement = Mathf.PI * fillState / numSegments;
            //float angle = 0.0f;

            Vector3[] positions = new Vector3[numSegments+1];
        for (int i = 0; i <= numSegments; i++)
        {
            positions[i] = calculateSegment(((float)i / numSegments)*fillState);
            //Debug.Log("i = " + i + ", fraction = " + ((float)i / 30));
        }



            //for (var i = 0; i <= numSegments; i++)
            //{
            //    positions[i] = new Vector3(
            //        Mathf.Cos(angle),
            //        0.0f,
            //        Mathf.Sin(angle)
            //    );
            //    angle += angleIncrement;
            //}
            //// apply the new points to the LineRenderer
            ///
            LineRenderer myLineRenderer = GetComponent<LineRenderer>();
            myLineRenderer.positionCount = numSegments+1;
            myLineRenderer.SetPositions(positions);
        }


    Vector3 calculateSegment(float inp)
    {
        float theta = 0.46764705f * inp - 0.15882353f;
        float x = 17 * Mathf.Sin(theta);
        float y = 17 * Mathf.Cos(theta) - 17;

        if (offsetAmount > 0)
            x += offsetAmount;

        return new Vector3(x,0,y);
    }
    }
