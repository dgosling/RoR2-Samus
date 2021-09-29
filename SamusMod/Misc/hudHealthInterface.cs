using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoR2;
using UnityEngine.UI;
using System;
using System.Runtime;
namespace SamusMod.Misc { 
public class hudHealthInterface : MonoBehaviour
{
    hudTypes hudTypes;
    float energyLowFader = 0f;
    float flashMag = 0f;
    float tankEnergy;
        float maxEnergy;
    public int totalEnergyTanks;
    public int numTanksFilled;
    float cachedBarEnergy = 0f;
    bool barDirty = true;
    bool energyLow;
    Text cEnergyDigits;
    List<Image> cEmptyTanks;
    List<Image> cFilledTanks;
    Text cEnergyWarning;
    public hudEnergyBar cEnergyBar;
    [SerializeField]
    Text combatEnergyDigits, ballEnergyDigits;
    [SerializeField]
    List<Image> combatEmptyTanks, combatFilledTanks, ballEmptyTanks, ballFilledTanks;
    [SerializeField]
    Text combatEnergyWarning, ballEnergyWarning;
    [SerializeField]
    hudEnergyBar combatEnergyBar, ballEnergyBar;
    lineTest lineTest;
    hudColors cHudColors,combatHudColors,ballHudColors;
    hudColors.EnergyInitColors cInitColors,combatInitColors,ballInitColors;
    hudColors.EnergyBarColors cBarColors,combatBarColors,ballBarColors;
    public bool initHealth;
        HealthComponent healthComponent;

    //float healthP;
    //hudEnergyBar.CalculateMode calculateMode;
    private void Awake()
    {
            combatHudColors = new hudColors(true);
            combatBarColors = combatHudColors.GetEnergyBarColors();
            combatInitColors = combatHudColors.GetEnergyInitColors();
            ballHudColors = new hudColors(false);
            ballBarColors = ballHudColors.GetEnergyBarColors();
            ballInitColors = ballHudColors.GetEnergyInitColors();


    }


    public void UpdateHud(float dt, float energyLowPulse)
    {

        //if (energyBar.GetCalculateMode() != calculateMode)
        //    energyBar.SetCalulateMode(calculateMode);
        if (cEnergyWarning)
        {
            if (energyLow)
            {
                energyLowFader = Mathf.Min(energyLowFader + 2f * dt, 1f);
                Color color = Color.white;
                color.a = energyLowFader * energyLowPulse;
                cEnergyWarning.material.color = color;
                combatEnergyWarning.material.color = color;
                ballEnergyWarning.material.color = color;
            }
            else
            {
                energyLowFader = Mathf.Max(0f, energyLowFader - 2f * dt);
                Color color = Color.white;
                color.a = energyLowFader * energyLowPulse;
                cEnergyWarning.material.color = color;
                combatEnergyWarning.material.color = color;
                ballEnergyWarning.material.color = color;
            }
            if (cEnergyWarning.material.color.a > 0f)
            {
                cEnergyWarning.enabled = true;
                combatEnergyWarning.enabled = true;
                ballEnergyWarning.enabled = true;
            }

            else
            {
                cEnergyWarning.enabled = false;
                combatEnergyWarning.enabled = false;
                ballEnergyWarning.enabled = false;
            }


        }

        if (cEnergyBar.GetFilledEnergy() != cachedBarEnergy || barDirty)
        {
            barDirty = false;
            cachedBarEnergy = cEnergyBar.GetFilledEnergy();
            string lString = ((int)Mathf.Ceil(healthComponent.combinedHealthFraction*100)).ToString("d2");
            cEnergyDigits.text = lString;
            combatEnergyDigits.text = lString;
            ballEnergyDigits.text = lString;
        }

        //hudColors.EnergyBarColors barColors = hudColors.getVisorEnergyBarColors();
        Color emptyColor = energyLow ? cHudColors.energyBarEmptyLow : cBarColors.empty;
        Color filledColor = energyLow ? cHudColors.energyBarFilledLow : cBarColors.filled;
        Color shadowColor = energyLow ? cHudColors.energyBarShadowLow : cBarColors.shadow;
        Color useFillColor = Color.Lerp(filledColor, cHudColors.energyBarFlashColor, flashMag);
        if (energyLow)
            useFillColor = Color.Lerp(useFillColor, new Color(1f, 0.8f, 0.4f, 1f), energyLowPulse);
        cEnergyBar.SetFilledColor(useFillColor);

        cEnergyBar.SetShadowColor(shadowColor);
        cEnergyBar.SetEmptyColor(emptyColor);
        updateEnergyBarColors(useFillColor, shadowColor, emptyColor);
        cEnergyBar.updateBar(dt);
        //if (calculateMode == hudEnergyBar.CalculateMode.manual)
        //{

        //    energyBar.SetBarPercentage(healthP);

        //}
        cEnergyBar.Draw();
    }
    public void SetEnergyLow(bool EnergyLow)
    {
        if (energyLow == EnergyLow)
            return;
        string lString = "";
        if (EnergyLow)
            lString = "Energy Low";
        if (cEnergyWarning)
        {
            cEnergyWarning.text = lString;
            combatEnergyWarning.text = lString;
            ballEnergyWarning.text = lString;
        }

            if (EnergyLow)
            {
                //AkSoundEngine.PostEvent(1631926714, gameObject);
                SamusHUD.playSound(1631926714, gameObject);
            }

            energyLow = EnergyLow;
    }
    public void SetFlashMagnitude(float mag) { flashMag = Mathf.Clamp01(mag); }
    public void SetNumFilledEnergyTanks(int NumTanksFilled)
    {
        numTanksFilled = NumTanksFilled;
        for (int i = 0; i < 6; i++)
        {
            if (i < numTanksFilled)
            {
                cFilledTanks[i].gameObject.SetActive(true);
                combatFilledTanks[i].gameObject.SetActive(true);
                ballFilledTanks[i].gameObject.SetActive(true);
            }

            else
            {
                cFilledTanks[i].gameObject.SetActive(false);
                combatFilledTanks[i].gameObject.SetActive(false);
                ballFilledTanks[i].gameObject.SetActive(false);
            }

        }
    }
    public void SetNumTotalEnergyTanks(int TotalEnergyTanks)
    {
        totalEnergyTanks = TotalEnergyTanks;
        for (int i = 0; i < 6; i++)
        {
            if (i < totalEnergyTanks)
            {
                cEmptyTanks[i].gameObject.SetActive(true);
                combatEmptyTanks[i].gameObject.SetActive(true);
                ballEmptyTanks[i].gameObject.SetActive(true);
            }

            else
            {
                cEmptyTanks[i].gameObject.SetActive(false);
                combatEmptyTanks[i].gameObject.SetActive(false);
                ballEmptyTanks[i].gameObject.SetActive(false);
                if (cFilledTanks[i].gameObject.activeInHierarchy)
                    cFilledTanks[i].gameObject.SetActive(false);
                if (combatFilledTanks[i].gameObject.activeInHierarchy)
                    combatFilledTanks[i].gameObject.SetActive(false);
                if (ballFilledTanks[i].gameObject.activeInHierarchy)
                    ballFilledTanks[i].gameObject.SetActive(false);
            }

        }
    }
    public void SetCurrEnergy(float TankEnergy,float MaxEnergy, bool wrapped)
    {
        tankEnergy = TankEnergy;
            maxEnergy = MaxEnergy;
            cEnergyBar.SetMaxEnergy(maxEnergy);
        cEnergyBar.SetCurrEnergy(tankEnergy, tankEnergy == 0f ? hudEnergyBar.ESetMode.Insta : wrapped == true ? hudEnergyBar.ESetMode.Wrapped : hudEnergyBar.ESetMode.Normal);
        updateEnergyBarCurrEnergy(wrapped);
    }
    public void SetLineTest(lineTest lTest) { lineTest = lTest; }
    float fmod(float numer, float denom)
    {
        float tquot = numer / denom;

        tquot = (float)Math.Truncate(tquot);

        return numer - tquot * denom;
    }

    public void setHudType(hudTypes hud)
    {
        hudTypes = hud;
        updateItems(hud);
    }
    //  public void SetCalculateMode(hudEnergyBar.CalculateMode calculate) { calculateMode = calculate; }
    // public hudEnergyBar.CalculateMode GetCalculateMode(){ return calculateMode; }
    //  public void SetHealthP(float per) { healthP = per; }
    public void initValues(hudTypes hud, float TankEnergy,float MaxEnergy, int totalTanks, int NumTanksFilled,HealthComponent health, bool EnergyLow)
    {
        //calculateMode = calculate;
        hudTypes = hud;
        tankEnergy = TankEnergy;
        totalEnergyTanks = totalTanks;
        numTanksFilled = NumTanksFilled;
        energyLow = EnergyLow;
            maxEnergy = MaxEnergy;
            healthComponent = health;
        cEmptyTanks = new List<Image>();
        cFilledTanks = new List<Image>();
        updateItems(hud);
            //cHudColors = colors;
            //barColors = colors.GetEnergyBarColors();
            //initColors = colors.GetEnergyInitColors();
        combatEnergyBar.SetTesselation(1f);
        ballEnergyBar.SetTesselation(1f);
        //energyBar.SetCalulateMode(calculate);

        combatEnergyBar.SetMaxEnergy(maxEnergy);
        ballEnergyBar.SetMaxEnergy(maxEnergy);
        combatEnergyBar.SetFilledColor(combatBarColors.filled);
        ballEnergyBar.SetFilledColor(ballBarColors.filled);
        combatEnergyBar.SetShadowColor(combatBarColors.shadow);
        ballEnergyBar.SetShadowColor(ballBarColors.shadow);
        combatEnergyBar.SetEmptyColor(combatBarColors.empty);
        ballEnergyBar.SetEmptyColor(ballBarColors.empty);

        combatEnergyBar.SetFilledDrainSpeed(99f);
        ballEnergyBar.SetFilledDrainSpeed(99f);
        combatEnergyBar.SetShadowDrainSpeed(20f);
        ballEnergyBar.SetShadowDrainSpeed(20f);
        combatEnergyBar.SetShadowDrainDelay(0.7f);
        ballEnergyBar.SetShadowDrainDelay(0.7f);
        combatEnergyBar.SetIsAlwaysResetTimer(false);
        ballEnergyBar.SetIsAlwaysResetTimer(false);
        cEnergyDigits.color = cInitColors.digitsFont;
        combatEnergyDigits.color = combatInitColors.digitsFont;
        ballEnergyDigits.color = ballInitColors.digitsFont;

        cEnergyDigits.gameObject.GetComponent<Outline>().effectColor = cInitColors.digitsOutline;
        combatEnergyDigits.gameObject.GetComponent<Outline>().effectColor = combatInitColors.digitsOutline;
        ballEnergyDigits.gameObject.GetComponent<Outline>().effectColor = ballInitColors.digitsOutline;

        cEnergyWarning.color = cHudColors.energyWarningFont;
        combatEnergyWarning.color = combatHudColors.energyWarningFont;
        ballEnergyWarning.color = ballHudColors.energyWarningFont;
        cEnergyWarning.gameObject.GetComponent<Outline>().effectColor = cHudColors.energyWarningOutline;
        combatEnergyWarning.gameObject.GetComponent<Outline>().effectColor = combatHudColors.energyWarningOutline;
        ballEnergyWarning.gameObject.GetComponent<Outline>().effectColor = ballHudColors.energyWarningOutline;
        if (energyLow)
        {
            cEnergyWarning.text = "Energy Low";
            combatEnergyWarning.text = "Energy Low";
            ballEnergyWarning.text = "Energy Low";
        }

        else
        {
            cEnergyWarning.text = "";
            combatEnergyWarning.text = "";
            ballEnergyWarning.text = "";
        }



        for (int i = 0; i < 6; i++)
        {
            cEmptyTanks[i].color = cInitColors.tankEmpty;
            combatEmptyTanks[i].color = combatInitColors.tankEmpty;
            ballEmptyTanks[i].color = ballInitColors.tankEmpty;
            cFilledTanks[i].color = cInitColors.tankFilled;
            combatFilledTanks[i].color = combatInitColors.tankFilled;
            ballFilledTanks[i].color = ballInitColors.tankFilled;
        }
        for (int i = 0; i < 6; i++)
        {
            if (i < numTanksFilled)
            {
                cFilledTanks[i].gameObject.SetActive(true);
                combatFilledTanks[i].gameObject.SetActive(true);
                ballFilledTanks[i].gameObject.SetActive(true);
            }


            else
            {
                cFilledTanks[i].gameObject.SetActive(false);
                combatFilledTanks[i].gameObject.SetActive(false);
                ballFilledTanks[i].gameObject.SetActive(false);
            }

        }
        initHealth = true;
    }

    void updateItems(hudTypes hud)
    {
        if (hud == hudTypes.combat)
        {
            //string path = "combatHud";

            //cEnergyDigits = gameObject.transform.Find(path + "/EnergyBar/healthDigits").gameObject.GetComponent<Text>();
            //List<Transform> tempList = new List<Transform>();
            //tempList.AddRange(gameObject.transform.Find(path).GetComponentsInChildren<Transform>());
            //foreach (Transform item in tempList)
            //{
            //    if (item.name.StartsWith("EnergyTankBack"))
            //    {
            //        cEmptyTanks.Add(item.gameObject.GetComponent<Image>());
            //    }
            //    else if (item.name.StartsWith("EnergyTank") && !item.name.Contains("Back"))
            //    {
            //        cFilledTanks.Add(item.gameObject.GetComponent<Image>());
            //    }
            //}
            //cEnergyWarning = gameObject.transform.Find(path + "/EnergyBar/EnergyWarning").gameObject.GetComponent<Text>();
            //cEnergyBar = gameObject.transform.Find("combatVisor/basewidget_energystuff/energybart01_energybar/HealthBack/HealthBar").gameObject.GetComponent<hudEnergyBar>();
            cEnergyDigits = combatEnergyDigits;
            cEmptyTanks = combatEmptyTanks;
            cFilledTanks = combatFilledTanks;
            cEnergyWarning = combatEnergyWarning;
            cEnergyBar = combatEnergyBar;
        }
        else
        {
            //string path = "ballHUD";

            //cEnergyDigits = gameObject.transform.Find(path + "/Text/healthText").gameObject.GetComponent<Text>();
            //List<Transform> tempList = new List<Transform>();
            //tempList.AddRange(gameObject.transform.Find(path + "/EnergyTanks").GetComponentsInChildren<Transform>());
            //foreach (Transform item in tempList)
            //{
            //    if (item.name.StartsWith("empty"))
            //    {
            //        cEmptyTanks.Add(item.gameObject.GetComponent<Image>());
            //    }
            //    else if (item.name.StartsWith("fill") && !item.name.Contains("empty"))
            //    {
            //        cFilledTanks.Add(item.gameObject.GetComponent<Image>());
            //    }
            //}
            //cEnergyWarning = gameObject.transform.Find(path + "/Text/EnergyWarning").gameObject.GetComponent<Text>();
            //cEnergyBar = gameObject.transform.Find(path + "/healthBar/background/healthBar").gameObject.GetComponent<hudEnergyBar>();
            cEnergyDigits = ballEnergyDigits;
            cEnergyBar = ballEnergyBar;
            cEmptyTanks = ballEmptyTanks;
            cFilledTanks = ballFilledTanks;
            cEnergyWarning = ballEnergyWarning;

        }
        if (hud == hudTypes.combat)
        {
                cHudColors = combatHudColors;
            cInitColors = combatInitColors;
            cBarColors = combatBarColors;
        }
        else
        {
                cHudColors = ballHudColors;
            cInitColors = ballInitColors;
            cBarColors = ballBarColors;
        }
    }

    void updateEnergyBarColors(Color filled, Color shadow, Color empty)
    {
        combatEnergyBar.SetEmptyColor(empty);
        ballEnergyBar.SetEmptyColor(empty);
        combatEnergyBar.SetFilledColor(filled);
        ballEnergyBar.SetFilledColor(filled);
        combatEnergyBar.SetShadowColor(shadow);
        ballEnergyBar.SetShadowColor(shadow);
    }

    void updateEnergyBarCurrEnergy(bool Wrapped)
    {
        combatEnergyBar.SetCurrEnergy(tankEnergy, tankEnergy == 0f ? hudEnergyBar.ESetMode.Insta : Wrapped == true ? hudEnergyBar.ESetMode.Wrapped : hudEnergyBar.ESetMode.Normal);
            combatEnergyBar.SetMaxEnergy(maxEnergy);
        ballEnergyBar.SetCurrEnergy(tankEnergy, tankEnergy == 0f ? hudEnergyBar.ESetMode.Insta : Wrapped == true ? hudEnergyBar.ESetMode.Wrapped : hudEnergyBar.ESetMode.Normal);
            ballEnergyBar.SetMaxEnergy(maxEnergy);
    }

        public bool checkEnergyBarIsActive(bool init)
        {
            if (init)
                return true;



            if (!cEnergyBar.gameObject.activeSelf)
                return false;
            else if (!ballEnergyBar.gameObject.activeSelf)
                return false;
            else if (!combatEnergyBar.gameObject.activeSelf)
                return false;
            else
                return true;
        }
}
}