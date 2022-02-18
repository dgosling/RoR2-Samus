using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hudChangingColors {
    public struct hudColors
    {
        public Color combatDecoColor {get;}
        public Color missileIconColorActive { get; }
        public Color energyBarFilledLow { get; }
        public Color energyBarShadowLow{get;}
        public Color energyBarEmptyLow{get;}
        public Color energyWarningFont{get;}
        public Color threatWarningFont{get;}
        public Color missileWarningFont{get;}
        public Color threatBarFilled{get;}
        public Color threatBarShadow{get;}
        public Color threatBarEmpty{get;}
        public Color missileBarFilled{get;}
        public Color missileBarShadow{get;}
        public Color missileBarEmpty{get;}
        public Color threatIconColor{get;}
        public Color tickDecoColor{get;}
        public Color threatIconSafeColor{get;}
        public Color missileIconColorInactive{get;}
        public Color energyWarningOutline{get;}
        public Color threatWarningOutline{get;}
        public Color missileWarningOutline{get;}
        public Color energyDrainFilterColor{get;}
        public Color energyBarFlashColor{get;}
        public Color powerBombDigitAvaliableFont{get;}
        public Color powerBombDigitAvaliableOutline{get;}
        public Color ballBombFilled{get;}
        public Color ballBombEmpty{get;}
        public Color powerBombIconAvaliable{get;}
        public Color ballBombEnergyDeco{get;}
        public Color ballBombDeco{get;}
        public Color powerBombDigitDepletedFont{get;}
        public Color powerBombDigitDepletedOutline{get;}
        public Color powerBombIconUnavaliable{get;}
        public Color threatIconWarning{get;}
        public Color threatDigitsFont{get;}
        public Color threatDigitsOutline{get;}
        public Color missileDigitsFont{get;}
        public Color missileDigitsOutline{get;}
        public Color energyBarFilled{get;}
        public Color energyBarEmpty{get;}
        public Color energyBarShadow{get;}
        public Color energyTankFilled{get;}
        public Color energyTankEmpty{get;}
        public Color energyDigitsFont{get;}
        public Color energyDigitsOutline{get;}
        public Color missileIconDepleteAlt { get; }
        public Color hudFrameColor { get; }
        public Color helmetLightColor { get; }
        public Color missileIconColorCanAlt { get; }
        public Color missileIconColorNoAlt { get; }
        bool combatVisorCheck;
        public struct EnergyBarColors
        {
            public Color filled;
            public Color empty;
            public Color shadow;
        }

        public struct EnergyInitColors
        {
            public Color tankFilled;
            public Color tankEmpty;
            public Color digitsFont;
            public Color digitsOutline;
        }
        List<EnergyBarColors> energyBarColors;
        List<EnergyInitColors> energyInitColors; 

        public hudColors(bool combatVisor)
        {
            helmetLightColor = new Color(0.294118f, 0.494118f, 0.639216f, 1.0f);
            hudFrameColor = new Color(0.294118f, 0.494118f, 0.639216f, 0.627451f);
            combatDecoColor = new Color(0.294118f, 0.494118f, 0.639216f, 0.627451f);
        missileIconColorActive = new Color(0.537255f, 0.839216f, 1, 1);
        energyBarFilledLow = new Color(1.0f, 0.407843f, 0.019608f, 1);
        energyBarShadowLow = new Color(0.294118f, 0.494118f, 0.639216f, 1);
        energyBarEmptyLow = new Color(0.172549f, 0.290196f, 0.372549f, 1);
        energyWarningFont = new Color(1.0f, 0.407843f, 0.019608f, 0.498039f);
        threatWarningFont = new Color(1.0f, 0.403922f, 0.019608f, 1);
        missileWarningFont = new Color(1.0f, 0.403922f, 0.019608f, 1);
        threatBarFilled = new Color(0.403922f, 0.682353f, 0.882353f, 1.0f);
        threatBarShadow = new Color(0.294118f, 0.494118f, 0.639216f, 1.0f);
        threatBarEmpty = new Color(0.168627f, 0.286275f, 0.368627f, 0.498039f);
        missileBarFilled = new Color(0.403922f, 0.682353f, 0.882353f, 1.0f);
        missileBarShadow = new Color(0.294118f, 0.494118f, 0.639216f, 1.0f);
        missileBarEmpty = new Color(0.168627f, 0.286275f, 0.368627f, 0.498039f);
        threatIconColor = new Color(0.537255f, 0.839216f, 1.0f, 1.0f);
        tickDecoColor = new Color(0.294118f, 0.494118f, 0.639216f, 0.313726f);
        threatIconSafeColor = new Color(0.294118f, 0.494118f, 0.639216f, 0.4f);
        missileIconColorInactive = new Color(0.294118f, 0.494118f, 0.639216f, 0.4f);
        energyWarningOutline = Color.black;
        threatWarningOutline = Color.black;
        missileWarningOutline = Color.black;
        energyDrainFilterColor = new Color(0.419608f, 0.0f, 0.0f, 0.392157f);
        energyBarFlashColor = new Color(1.0f, 0.403922f, 0.019608f, 1.0f);
        powerBombDigitAvaliableFont = new Color(0.403922f, 0.682353f, 0.882353f, 1.0f);
        powerBombDigitAvaliableOutline = Color.black;
        ballBombFilled = new Color(0.403922f, 0.682353f, 0.882353f, 1.0f);
        ballBombEmpty = new Color(0.403922f, 0.682353f, 0.882353f, 0.498039f);
        powerBombIconAvaliable = new Color(0.403922f, 0.682353f, 0.882353f, 1.0f);
        ballBombEnergyDeco = new Color(0.403922f, 0.682353f, 0.882353f, 1.0f);
        ballBombDeco = new Color(0.403922f, 0.682353f, 0.882353f, 1.0f);
        powerBombDigitDepletedFont = new Color(0.403922f, 0.682353f, 0.882353f, 1.0f);        
        powerBombDigitDepletedOutline = new Color(0.0f, 0.0f, 0.0f, 0.5f);
        powerBombIconUnavaliable = new Color(0.403922f, 0.682353f, 0.882353f, 0.5f);
        threatIconWarning = new Color(1.0f, 0.403922f, 0.019608f, 1.0f);
        threatDigitsFont = new Color(0.403922f, 0.682353f, 0.882353f, 1.0f);
            threatDigitsOutline = Color.black;
        missileDigitsFont = new Color(0.403922f, 0.682353f, 0.882353f, 1.0f);
            missileIconDepleteAlt = new Color(0.537255f, 0.839216f, 1.0f, 1.0f);
            missileDigitsOutline = Color.black;
            energyBarColors = new List<EnergyBarColors>();
            energyInitColors = new List<EnergyInitColors>();
            energyBarFilled = new Color(0.403922f, 0.682353f, 0.882353f, 1.0f);

                energyBarEmpty = new Color(0.172549f, 0.290196f, 0.372549f, 1.0f);
                energyBarShadow = new Color(0.294118f, 0.494118f, 0.639216f, 1.0f);
                energyTankFilled = new Color(0.403922f, 0.682353f, 0.882353f, 1.0f);
                energyTankEmpty = new Color(0.403922f, 0.682353f, 0.882353f, 0.254118f);
                energyDigitsFont = new Color(0.2f, 0.337255f, 0.439216f, 1.0f);
                energyDigitsOutline = Color.black;
            missileIconColorCanAlt = new Color(0.403922f, 0.682353f, 0.882353f, 1.0f);
            missileIconColorNoAlt = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            energyBarColors.Add(new EnergyBarColors
            {
                filled =energyBarFilled,
                empty = energyBarEmpty,
                shadow = energyBarShadow
            });
            energyInitColors.Add(new EnergyInitColors { 
                tankEmpty = energyTankEmpty,
                tankFilled = energyTankFilled,
                digitsFont = energyDigitsFont,
                digitsOutline = energyDigitsOutline
            });
            //barColors = new EnergyBarColors()
            //{
            //    filled = energyBarFilled,
            //    empty = energyBarEmpty,
            //    shadow = energyBarShadow

            //};
            //initColors = new EnergyInitColors()
            //{
            //    tankFilled = energyTankFilled,
            //    tankEmpty = energyTankEmpty,
            //    digitsFont = energyDigitsFont,
            //    digitsOutline = energyDigitsOutline
            //};





            energyBarFilled = new Color(0.403922f, 0.682353f, 0.882353f, 1.0f);
                energyBarEmpty = new Color(0.172549f, 0.290196f, 0.372549f, 1.0f);
                energyBarShadow = new Color(0.294118f, 0.494118f, 0.639216f, 1.0f);
                energyTankFilled = new Color(0.454902f, 0.764706f, 1.0f, 1.0f);
                energyTankEmpty = new Color(0.2f, 0.337255f, 0.439216f, 1.0f);
                energyDigitsFont = new Color(0.403922f, 0.682353f, 0.882353f, 1.0f);
                energyDigitsOutline = Color.black;

            energyBarColors.Add(new EnergyBarColors
            {
                filled = energyBarFilled,
                empty = energyBarEmpty,
                shadow = energyBarShadow
            });
            energyInitColors.Add(new EnergyInitColors
            {
                tankEmpty = energyTankEmpty,
                tankFilled = energyTankFilled,
                digitsFont = energyDigitsFont,
                digitsOutline = energyDigitsOutline
            });
            //barColors = new EnergyBarColors()
            //{
            //    filled = energyBarFilled,
            //    empty = energyBarEmpty,
            //    shadow = energyBarShadow

            //};
            //initColors = new EnergyInitColors()
            //{
            //    tankFilled = energyTankFilled,
            //    tankEmpty = energyTankEmpty,
            //    digitsFont = energyDigitsFont,
            //    digitsOutline = energyDigitsOutline
            //};

            combatVisorCheck = combatVisor;
        }

        public EnergyBarColors getVisorEnergyBarColors()
        {
            if (combatVisorCheck)
            {
                return energyBarColors[0];
            }
            else
                return energyBarColors[1];
        }

        public EnergyInitColors getVisorEnergyInitColors()
        {
            if (combatVisorCheck)
                return energyInitColors[0];
            else
                return energyInitColors[1];
        }
        
        public EnergyBarColors getVisorEnergyBarColors(bool combatVisor)
        {
            if (combatVisor)
                return energyBarColors[0];
            else
                return energyBarColors[1];

        }
        public EnergyInitColors getVisorEnergyInitColors(bool combatVisor)
        {
            if (combatVisor)
                return energyInitColors[0];
            else
                return energyInitColors[1];

        }

    }

}
