using BepInEx.Configuration;
using System;
using UnityEngine;

namespace SamusMod.Modules
{
    public static class Config
    {
        public static ConfigEntry<bool> enableHud;

        public static void ReadConfig()
        {
            enableHud = SamusPlugin.instance.Config.Bind<bool>("VR Settings", "Enable VR Visor", true, "Enables the Metroid Prime Style Visor When playing in VR.");
        }
    }
}
