using System;
using System.Collections;
using UnityEngine;
using RoR2;
using System.Runtime.CompilerServices;

namespace SamusMod.Misc
{
    public class colision_test : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.name == "SamusMorphBomb(Clone)")
            {
                Debug.Log("collision");
            }
            else
                Debug.Log("Collided with "+collision.name);
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Debug.Log(collision.collider.name);
        }

      



    }
}
