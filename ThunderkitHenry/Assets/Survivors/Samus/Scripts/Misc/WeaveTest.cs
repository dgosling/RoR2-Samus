using System;
using UnityEngine.Networking;
namespace SamusMod.Misc
{


    public class WeaveTest : NetworkBehaviour
    {
        // Start is called before the first frame update
        public WeaveTest()
        {

        }

        [SyncVar]
        public int int1 = 69;
        [SyncVar]
        public int int2 = 42069;
        [SyncVar]
        public string MyString = "TestingWeaver";

    }
}

