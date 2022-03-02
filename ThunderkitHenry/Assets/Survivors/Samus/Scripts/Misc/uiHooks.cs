using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using RoR2.UI;
using System;
using UnityEngine;
namespace SamusMod.Misc
{
    internal static class uiHooks
    {


        internal static void AddActionBindingToSettings(string actionName,Transform buttonToCopy)
        {
            var inputBindingObject = GameObject.Instantiate(buttonToCopy, buttonToCopy.parent);
            var inputBindingControl = inputBindingObject.GetComponent<InputBindingControl>();
            inputBindingControl.actionName = actionName;
            inputBindingControl.Awake();
        }
    }
}

