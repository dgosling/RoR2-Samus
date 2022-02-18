using R2API;
using System;

namespace SamusMod.Modules
{
    internal static class Tokens
    {
        // Used everywhere within tokens. Format is DeveloperPrefix + BodyPrefix + unique per token
        // A full example token for ThunderHenry would be ROBVALE_THUNDERHENRY_BODY_UNLOCK_SURVIVOR_NAME.
        //internal const string henryPrefix = "_THUNDERHENRY_BODY_";
        internal const string samusPrefix = "_SAMUS_";
        internal static void Init()
        {
            #region Henry
            //string prefix = ThunderSamusPlugin.developerPrefix + henryPrefix;

            //string desc = "Thunder Henry is a skilled fighter who makes use of a wide arsenal of weaponry to take down his foes.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            //desc = desc + "< ! > Sword is a good all-rounder while Boxing Gloves are better for laying a beatdown on more powerful foes." + Environment.NewLine + Environment.NewLine;
            //desc = desc + "< ! > Pistol is a powerful anti air, with its low cooldown and high damage." + Environment.NewLine + Environment.NewLine;
            //desc = desc + "< ! > Roll has a lingering armor buff that helps to use it aggressively." + Environment.NewLine + Environment.NewLine;
            //desc = desc + "< ! > Bomb can be used to wipe crowds with ease." + Environment.NewLine + Environment.NewLine;

            //string outro = "..and so he left, searching for a new identity.";
            //string outroFailure = "..and so he vanished, forever a blank slate.";

            //LanguageAPI.Add(prefix + "NAME", "Thunder Henry");
            //LanguageAPI.Add(prefix + "DESCRIPTION", desc);
            //LanguageAPI.Add(prefix + "SUBTITLE", "The Chosen One");
            //LanguageAPI.Add(prefix + "LORE", "sample lore");
            //LanguageAPI.Add(prefix + "OUTRO_FLAVOR", outro);
            //LanguageAPI.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            //LanguageAPI.Add(prefix + "DEFAULT_SKIN_NAME", "Default");
            //LanguageAPI.Add(prefix + "MASTERY_SKIN_NAME", "Alternate");

            #endregion

            #region Passive
            //LanguageAPI.Add(prefix + "PASSIVE_NAME", "Thunder Henry passive");
            //LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION", "Sample text.");
            #endregion

            #region Primary
            //LanguageAPI.Add(prefix + "PRIMARY_SLASH_NAME", "Sword");
            //LanguageAPI.Add(prefix + "PRIMARY_SLASH_DESCRIPTION", Helpers.agilePrefix + $"Swing forward for <style=cIsDamage>{100f * StaticValues.swordDamageCoefficient}% damage</style>.");
            //LanguageAPI.Add(prefix + "PRIMARY_PUNCH_NAME", "Boxing Gloves");
            //LanguageAPI.Add(prefix + "PRIMARY_PUNCH_DESCRIPTION", Helpers.agilePrefix + $"Punch rapidly for <style=cIsDamage>{100f * 2.4f}% damage</style>. <style=cIsUtility>Ignores armor.</style>");
            #endregion

            #region Secondary
            //LanguageAPI.Add(prefix + "SECONDARY_GUN_NAME", "Handgun");
            //LanguageAPI.Add(prefix + "SECONDARY_GUN_DESCRIPTION", Helpers.agilePrefix + $"Fire a handgun for <style=cIsDamage>{100f * StaticValues.gunDamageCoefficient}% damage</style>.");
            //LanguageAPI.Add(prefix + "SECONDARY_UZI_NAME", "Uzi");
            //LanguageAPI.Add(prefix + "SECONDARY_UZI_DESCRIPTION", $"Fire an uzi for <style=cIsDamage>{100f * StaticValues.uziDamageCoefficient}% damage</style>.");
            #endregion

            #region Utility
            //LanguageAPI.Add(prefix + "UTILITY_ROLL_NAME", "Roll");
            //LanguageAPI.Add(prefix + "UTILITY_ROLL_DESCRIPTION", "Roll a short distance, gaining <style=cIsUtility>300 armor</style>. <style=cIsUtility>You cannot be hit during the roll.</style>");
            #endregion

            #region Special
            //LanguageAPI.Add(prefix + "SPECIAL_BOMB_NAME", "Bomb");
            //LanguageAPI.Add(prefix + "SPECIAL_BOMB_DESCRIPTION", $"Throw a bomb for <style=cIsDamage>{100f * StaticValues.bombDamageCoefficient}% damage</style>.");
            #endregion

            #region Achievements
            //LanguageAPI.Add(prefix + "UNLOCK_SURVIVOR_NAME", "Thunderous Prelude");
            //LanguageAPI.Add(prefix + "UNLOCK_SURVIVOR_DESC", "Enter Distant Roost.");
            //LanguageAPI.Add(prefix + "UNLOCK_SURVIVOR_UNLOCKABLE_NAME", "Thunderous Prelude");

            //LanguageAPI.Add(prefix + "UNLOCK_MASTERY_NAME", "Thunder Henry: Mastery");
            //LanguageAPI.Add(prefix + "UNLOCK_MASTERY_DESC", "As Thunder Henry, beat the game or obliterate on Monsoon.");
            //LanguageAPI.Add(prefix + "UNLOCK_MASTERY_UNLOCKABLE_NAME", "Thunder Henry: Mastery");

            //LanguageAPI.Add(prefix + "UNLOCK_UZI_NAME", "Thunder Henry: Shoot 'em up");
            //LanguageAPI.Add(prefix + "UNLOCK_UZI_DESC", "As Thunder Henry, reach +250% attack speed.");
            //LanguageAPI.Add(prefix + "UNLOCK_UZI_UNLOCKABLE_NAME", "Thunder Henry: Shoot 'em up");
            #endregion
            #endregion

            #region Samus
            string prefix = SamusPlugin.developerPrefix + samusPrefix;
            string desc = "Samus is a galatic bounty hunter.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > The Power Beam can be rapidly shot or charged." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Backup Magazines give you 5 more missiles!" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Morph Bomb Dash can be good for escaping when surrounded." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Super Missiles can clear a group of enemies, but cost 5 missiles to use." + Environment.NewLine + Environment.NewLine;

            string outro = "..and so she left, her mission complete.";
            string outroFailure = "..and so she vanished, her mission complete.";

            LanguageAPI.Add(prefix + "NAME", "Samus");
            LanguageAPI.Add(prefix + "DESCRIPTION", desc);
            LanguageAPI.Add(prefix + "SUBTITLE", "Intergalatic Bounty Hunter");
            LanguageAPI.Add(prefix + "LORE", "Samus Aran is an intergalatic bounty hunter, she was raised by the mysterious Chozo.");
            LanguageAPI.Add(prefix + "OUTRO_FLAVOR", outro);
            LanguageAPI.Add(prefix + "OUTRO_FAILURE", outroFailure);
            #region skin
            LanguageAPI.Add(prefix + "DEFAULT_SKIN_NAME", "Default");
            #endregion
            #region passive
            LanguageAPI.Add(prefix + "PASSIVE_NAME", "Varia Suit");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION", "Become <style=cIsUtility>Immune</style> to <style=cIsDamage>PercentBurn Damage</style> and <style=cIsUtility>Gain a Second Jump.</style>");
            #endregion
            #region primary
            LanguageAPI.Add(prefix + "PRIMARY_BEAM_NAME", "Power Beam");
            LanguageAPI.Add(prefix + "PRIMARY_BEAM_DESCRIPTION", "Click to do <style=cIsDamage>" + StaticValues.baseDamage + "</style> and hold to charge a beam that does <style=cIsDamage>" + 100f * StaticValues.shootDamageCoefficient + "% to " + 100 * StaticValues.cshootDamageCoefficient + "% damage.</style>");

            #endregion
            desc = "Fire a Homing Missile that homes in on the nearest enemy and does <style=cIsDamage>" + 100f * StaticValues.missileDamageCoefficient + "% damage.</style>";
            #region secondary
            LanguageAPI.Add(prefix + "SECONDARY_MISSILE_NAME", "Missile");
            LanguageAPI.Add(prefix + "SECONDARY_MISSILE_DESCRIPTION", desc);
            desc = "Fire a Tracking Missile that will home in on the targeted enemy if one is highlighted, otherwise will act like the default missile.";
            LanguageAPI.Add(prefix + "SECONDARY_TMISSILE_NAME", "Tracking Missile");
            LanguageAPI.Add(prefix + "SECONDARY_TMISSILE_DESCRIPTION", desc);
            #endregion
            desc = "<style=cIsUtility>Dash</style> forward and drop a <style=cKeywordName>bomb</style> that does <style=cIsDamage>" + 100f * StaticValues.dashDamageCoefficient + "% damage</style> midway through.";
            #region utility
            LanguageAPI.Add(prefix + "UTILITY_DASH_NAME", "Morph Bomb Dash");
            LanguageAPI.Add(prefix + "UTILITY_DASH_DESCRIPTION", desc);
            desc = "Go into and out of Morph Ball mode, where you can roll around and drop bombs and super bombs.";
            LanguageAPI.Add(prefix + "UTILITY_MORPH_NAME", "Morph Ball Mode");
            LanguageAPI.Add(prefix + "UTILITY_MORPH_DESCRIPTION", desc);
            desc = "Exit out of Morph Ball Mode.";
            LanguageAPI.Add(prefix+"UTILITY_MORPH_EXIT_NAME", "Exit Morph Ball Mode");
            LanguageAPI.Add(prefix+"UTILITY_MORPH_EXIT_DESCRIPTION", desc);
            desc = "Drop a Morph Bomb for <style=cIsDamage>" + (StaticValues.baseDamage * 3) * 100 + "</style>";
            LanguageAPI.Add(prefix+"PRIMARY_MORPH_BOMB_NAME", "Morph Bomb");
            LanguageAPI.Add(prefix+"PRIMARY_MORPH_BOMB_DESCRIPTION", desc);
            desc = "Drop a Power Bomb for <style=cIsDamage>" + (StaticValues.baseDamage * 10) * 100 + "</style>";
            LanguageAPI.Add(prefix+"SECONDARY_MORPH_PBOMB_NAME", "Power Bomb");
            LanguageAPI.Add(prefix+"SECONDARY_MORPH_PBOMB_DESCRIPTION", desc);
            #endregion
            desc = "Fire a large missile that shoots straight with a large explosion that does <style=cIsDamage>" + 100f * StaticValues.smissileDamageCoefficient + "% damage.</style> <style=cSub>Consumes 5 Missiles</style>";
            #region special
            LanguageAPI.Add(prefix+"SPECIAL_SMISSILE_NAME", "Super Missile");
            LanguageAPI.Add(prefix+"SPECIAL_SMISSILE_DESCRIPTION", desc);
            #endregion
            #endregion
        }
    }
}