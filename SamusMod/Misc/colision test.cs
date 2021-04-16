using System;
using System.Collections;
using UnityEngine;
using RoR2;
using System.Runtime.CompilerServices;

namespace SamusMod.Misc
{
    public class colision_test : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name=="ball2")
            {

                Debug.Log("test Jump");
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.name == "ball2")
            {

                Debug.Log("test Jump");
            }
        }
    }
}
