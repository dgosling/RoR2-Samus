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

            LanguageAPI.Add("SAMUS_NAME", StaticValues.characterName);
            LanguageAPI.Add("SAMUS_DESCRIPTION", desc);
            LanguageAPI.Add("SAMUS_SUBTITLE", StaticValues.characterSubtitle);
            LanguageAPI.Add("SAMUS_LORE", StaticValues.characterLore);
            LanguageAPI.Add("SAMUS_OUTRO_FLAVOR", outro);

            LanguageAPI.Add("SAMUS_DEFAULT_SKIN_NAME", "Default");

            LanguageAPI.Add("SAMUS_PASSIVE_NAME", "Varia Suit");
            LanguageAPI.Add("SAMUS_PASSIVE_DESCRIPTION", "Become <style=cIsUtility>Immune</style> to <style=cIsDamage>Burn Damage</style> <style=cSub>Not Implemented</style> and <style=cIsUtility>Gain a Second Jump.</style>");

            desc = "Click to do <style=cIsDamage>"+StaticValues.baseDamage+"</style> and hold to charge a beam that does <style=cIsDamage>" + 100f * StaticValues.shootDamageCoefficient + "% to " + 100 * StaticValues.cshootDamageCoefficient + "% damage.</style>";

            LanguageAPI.Add("SAMUS_PRIMARY_BEAM_NAME", "Power Beam");
            LanguageAPI.Add("SAMUS_PRIMARY_BEAM_DESCRIPTION", desc);

            desc = "Fire a Homing Missile that homes in on the nearest enemy and does <style=cIsDamage>"+100f*StaticValues.missileDamageCoefficient+"% damage.</style>";

            LanguageAPI.Add("SAMUS_SECONDARY_MISSILE_NAME", "Missile");
            LanguageAPI.Add("SAMUS_SECONDARY_MISSILE_DESCRIPTION", desc);

            desc = "<style=cIsUtility>Dash</style> forward and drop a <style=cKeywordName>bomb</style> that does <style=cIsDamage>" + 100f * StaticValues.dashDamageCoefficient + "% damage</style> midway through.";

            LanguageAPI.Add("SAMUS_UTILITY_DASH_NAME", "Morph Ball and Bomb");
            LanguageAPI.Add("SAMUS_UTILITY_DASH_DESCRIPTION", desc);

            desc = "Fire a large missile that shoots straight with a large explosion that does <style=cIsDamage>"+100f*StaticValues.smissileDamageCoefficient+"% damage.</style> <style=cSub>Consumes 5 Missiles</style>";

            LanguageAPI.Add("SAMUS_SPECIAL_SMISSILE_NAME", "Super Missile");
            LanguageAPI.Add("SAMUS_SPECIAL_SMISSILE_DESCRIPTION", desc);

            //LanguageAPI.Add("KEYWORD_BOOST", "<style=cKeywordName>Jet Boosted</style><style=cSub>Using the speed of your jets, you are temporarily <style=cIsUtility>invulnerable</style> and deal <style=cIsDamage>" + 100f * StaticValues.dashDamageCoefficient + "% damage</style> to anyone in your way.</style>");



        }
    }
}
