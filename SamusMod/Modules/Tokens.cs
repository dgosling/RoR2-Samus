using System;
using R2API;

namespace SamusMod.Modules
{
    public static class Tokens
    {
        public static void AddTokens()
        {
            string desc = "Samus is a galatic bounty hunter.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Tip 1." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Tip 2." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Tip 3." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Tip 4." + Environment.NewLine + Environment.NewLine;


            string outro = StaticValues.characterOutro;

            LanguageAPI.Add("SAMUS_NAME", StaticValues.characterName);
            LanguageAPI.Add("SAMUS_DESCRIPTION", desc);
            LanguageAPI.Add("SAMUS_SUBTITLE", StaticValues.characterSubtitle);
            LanguageAPI.Add("SAMUS_LORE", StaticValues.characterLore);
            LanguageAPI.Add("SAMUS_OUTRO_FLAVOR", outro);

            LanguageAPI.Add("SAMUS_DEFAULT_SKIN_NAME", "Default");

            LanguageAPI.Add("SAMUS_PASSIVE_NAME", "Varia Suit");
            LanguageAPI.Add("SAMUS_PASSIVE_DESCRIPTION", "Become <style=cIsUtility>Immune</style> to <style=cIsDamage>Burn Damage</style> and <style=cIsUtility>Gain a Second Jump.</style>");

            desc = "Hold to charge a beam that does <style=cIsDamage>" + 100f * StaticValues.shootDamageCoefficient + "% to " + 100 * StaticValues.cshootDamageCoefficient + "% damage.</style>";

            LanguageAPI.Add("SAMUS_PRIMARY_BEAM_NAME", "Power Beam");
            LanguageAPI.Add("SAMUS_PRIMARY_BEAM_DESCRIPTION", desc);

            desc = "placeholder missile text";

            LanguageAPI.Add("SAMUS_SECONDARY_MISSILE_NAME", "Missile");
            LanguageAPI.Add("SAMUS_SECONDARY_MISSILE_DESCRIPTION", desc);

            desc = "placeholder dash text";

            LanguageAPI.Add("SAMUS_UTILITY_DASH_NAME", "Dash Attack");
            LanguageAPI.Add("SAMUS_UTILITY_DASH_DESCRIPTION", desc);

            desc = "placeholder super missile text";

            LanguageAPI.Add("SAMUS_SPECIAL_SMISSILE_NAME", "Super Missile");
            LanguageAPI.Add("SAMUS_SPECIAL_SMISSILE_DESCRIPTION", desc);

            LanguageAPI.Add("KEYWORD_BOOST", "<style=cKeywordName>Jet Boosted</style><style=cSub>Using the speed of your jets, you are temporarily <style=cIsUtility>invulnerable</style> and deal <style=cIsDamage>" + 100f * StaticValues.dashDamageCoefficient + "% damage</style> to anyone in your way.</style>");



        }
    }
}
