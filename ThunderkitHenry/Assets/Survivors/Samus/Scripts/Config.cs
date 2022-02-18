using BepInEx.Configuration;

namespace SamusMod.Modules
{
    class Config
    {
        //Config entry variables go here. Use these to get the config settings in Mod Manager.
        public static ConfigEntry<bool> characterEnabled;
        public static ConfigEntry<bool> enableHud;
        //public static ConfigEntry<bool> forceUnlock;

        public static void ReadConfig()
        {
            //Template
            //ThunderHenryPlugin.instance.Config.Bind<bool>(new ConfigDefinition("section", "name"), false, new ConfigDescription("description"));

            //General
            characterEnabled = SamusPlugin.instance.Config.Bind<bool>(new ConfigDefinition("General", "Character Enabled"), true, new ConfigDescription("Set to false to disable this Survivor."));
            //forceUnlock = SamusPlugin.instance.Config.Bind<bool>(new ConfigDefinition("General", "Force Unlock"), false, new ConfigDescription("Set to true to force this Survivor's content to be unlocked."));

            //VR
            enableHud = SamusPlugin.instance.Config.Bind<bool>("VR Settings", "Enable VR Visor", true, "Enables the Metroid Prime Style Visor When playing in VR.");
        }
    }
}
