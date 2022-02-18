using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SamusMod.Misc
{
    public class hudTimer
    {
        public double timer = 0;
        public float Ftimer = 0;
        // Start is called before the first frame update





        // Update is called once per frame

        public double solveTimer()
        {
            timer += Time.fixedDeltaTime;
            if (timer >= 1)
            {
                timer = 0;
                return 1;
            }
            else if (timer < 1 && timer >= 0)
            {
                return timer;
            }
            else
                return 0;


        }

        public float solveFTimer()
        {
            Ftimer += Time.fixedDeltaTime;
            if (Ftimer >= 1f)
            {
                Ftimer = 0f;
                return 1f;
            }
            else if (Ftimer < 1f && Ftimer >= 0f)
                return Ftimer;
            else
                return 0f;
        }


    }
}