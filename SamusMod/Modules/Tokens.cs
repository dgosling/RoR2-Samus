using System;
using R2API;

namespace SamusMod.Modules
{
    public static class Tokens
    {
        public static void AddTokens()
        {
            string desc = "Samus is a galatic bounty hunter.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Tip 1. WIP" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Tip 2. WIP" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Tip 3. WIP" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Tip 4. WIP" + Environment.NewLine + Environment.NewLine;


            string outro = StaticValues.characterOutro;

            LanguageAPI.Add("DG_SAMUS_NAME", StaticValues.characterName);
            LanguageAPI.Add("DG_SAMUS_DESCRIPTION", desc);
            LanguageAPI.Add("DG_SAMUS_SUBTITLE", StaticValues.characterSubtitle);
            LanguageAPI.Add("DG_SAMUS_LORE", StaticValues.characterLore);
            LanguageAPI.Add("DG_SAMUS_OUTRO_FLAVOR", outro);

            LanguageAPI.Add("DG_SAMUS_DEFAULT_SKIN_NAME", "Default");

            LanguageAPI.Add("DG_SAMUS_PASSIVE_NAME", "Varia Suit");
            LanguageAPI.Add("DG_SAMUS_PASSIVE_DESCRIPTION", "Become <style=cIsUtility>Immune</style> to <style=cIsDamage>PercentBurn Damage</style> and <style=cIsUtility>Gain a Second Jump.</style>");

            desc = "Click to do <style=cIsDamage>"+StaticValues.baseDamage+"</style> and hold to charge a beam that does <style=cIsDamage>" + 100f * StaticValues.shootDamageCoefficient + "% to " + 100 * StaticValues.cshootDamageCoefficient + "% damage.</style>";

            LanguageAPI.Add("DG_SAMUS_PRIMARY_BEAM_NAME", "Power Beam");
            LanguageAPI.Add("DG_SAMUS_PRIMARY_BEAM_DESCRIPTION", desc);

            desc = "Fire a Homing Missile that homes in on the nearest enemy and does <style=cIsDamage>"+100f*StaticValues.missileDamageCoefficient+"% damage.</style>";

            LanguageAPI.Add("DG_SAMUS_SECONDARY_MISSILE_NAME", "Missile");
            LanguageAPI.Add("DG_SAMUS_SECONDARY_MISSILE_DESCRIPTION", desc);

            desc = "<style=cIsUtility>Dash</style> forward and drop a <style=cKeywordName>bomb</style> that does <style=cIsDamage>" + 100f * StaticValues.dashDamageCoefficient + "% damage</style> midway through.";

            LanguageAPI.Add("DG_SAMUS_UTILITY_DASH_NAME", "Morph Ball and Bomb");
            LanguageAPI.Add("DG_SAMUS_UTILITY_DASH_DESCRIPTION", desc);

            desc = "test";
            LanguageAPI.Add("DG_SAMUS_UTILITY_MORPH_NAME", "Morph Ball Mode");
            LanguageAPI.Add("DG_SAMUS_UTILITY_MORPH_DESCRIPTION", desc);
            desc = "test";
            LanguageAPI.Add("DG_SAMUS_UTILITY_MORPH_EXIT_NAME", "Exit Morph Ball Mode");
            LanguageAPI.Add("DG_SAMUS_UTILITY_MORPH_EXIT_DESCRIPTION", desc);
            LanguageAPI.Add("DG_SAMUS_PRIMARY_MORPH_BOMB_NAME", "Morph Bomb");
            LanguageAPI.Add("DG_SAMUS_PRIMARY_MORPH_BOMB_DESCRIPTION", desc);

            desc = "Fire a large missile that shoots straight with a large explosion that does <style=cIsDamage>"+100f*StaticValues.smissileDamageCoefficient+"% damage.</style> <style=cSub>Consumes 5 Missiles</style>";

            LanguageAPI.Add("DG_SAMUS_SPECIAL_SMISSILE_NAME", "Super Missile");
            LanguageAPI.Add("DG_SAMUS_SPECIAL_SMISSILE_DESCRIPTION", desc);

            //LanguageAPI.Add("KEYWORD_BOOST", "<style=cKeywordName>Jet Boosted</style><style=cSub>Using the speed of your jets, you are temporarily <style=cIsUtility>invulnerable</style> and deal <style=cIsDamage>" + 100f * StaticValues.dashDamageCoefficient + "% damage</style> to anyone in your way.</style>");



        }
    }
}
