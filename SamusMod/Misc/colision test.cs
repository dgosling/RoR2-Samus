using System;
using System.Collections;
using UnityEngine;
using RoR2;
using RoR2.Projectile;
using System.Runtime.CompilerServices;

namespace SamusMod.Misc
{
    public class colision_test : MonoBehaviour
    {
        public Transform inTransform;
        ProjectileSingleTargetImpact projectileSingle;
        float length;
        float dist;
        void Awake()
        {
            projectileSingle = gameObject.GetComponent<ProjectileSingleTargetImpact>();
            length = 1;

            projectileSingle.destroyOnWorld = false;
        }

        void FixedUpdate()
        {
            //if (!inTransform)
            //    Debug.LogError("inTransform is null!");
            if(inTransform)
                dist = Vector3.Distance(gameObject.transform.position, inTransform.position);
            
            //Debug.Log(dist);
            if(dist >= length)
            {
                projectileSingle.destroyOnWorld = true;
                enabled = false;
            }

        }

      



    }
}
