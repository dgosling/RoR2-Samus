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
            Debug.Log("collision started with "+collision.collider.name);
        }

        private void OnCollisionExit(Collision collision)
        {
            Debug.Log("collision stopped with "+collision.collider.name);
        }
    }
}
