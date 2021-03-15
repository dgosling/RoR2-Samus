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
            if(collision.gameObject.name== "SamusMorphBomb(Clone)")
            {
                gameObject.GetComponent<CharacterMotor>().velocity += new Vector3(0, gameObject.GetComponent<CharacterBody>().jumpPower * .8f, 0);
                Debug.Log("test");
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            
        }
    }
}
