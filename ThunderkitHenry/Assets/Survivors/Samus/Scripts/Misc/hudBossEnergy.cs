﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SamusMod.Misc
{
    public class hudBossEnergy : MonoBehaviour
    {
        float alpha, fader, curEnergy, maxEnergy;

        bool visible;
        [SerializeField]
        GameObject root;
        [SerializeField]
        List<GameObject> rootChildren;
        [SerializeField]
        hudEnergyBar bossBar;
        Text textBoss, textBossSub;
        public bool bossIni;
        hudColors hudColors;
        hudColors.EnergyBarColors EnergyBarColors;
        public Color debugColor;
        public bool loaded = false;
        // hudEnergyBar.CalculateMode calculateMode;
        // float healthP;
        // Start is called before the first frame update
        void Start()
        {
            bossIni = false;
            alpha = 1f;
            fader = 0f;
            curEnergy = 0f;
            maxEnergy = 0f;
            visible = false;

            //hudColors = new hudColors(true);
            //EnergyBarColors = hudColors.getVisorEnergyBarColors();
            root = gameObject.transform.Find("bossHud").gameObject;
            bossBar = root.transform.Find("BossHealth/BossBar").gameObject.GetComponent<hudEnergyBar>();
            textBoss = root.transform.Find("BossFrame/bossName").gameObject.GetComponent<Text>();
            textBossSub = textBoss.transform.GetChild(0).GetComponent<Text>();
            rootChildren = new List<GameObject>();
            foreach (Transform child in root.transform.GetComponentsInChildren<Transform>(true))
            {
                if (child != root.transform)
                {
                    rootChildren.Add(child.gameObject);
                    //Debug.Log(child.name);
                }

            }
            bossBar.SetTesselation(0.2f);
            //calculateMode = hudEnergyBar.CalculateMode.automatic;

            //bossBar.SetFilledDrainSpeed(0f); //test

            loaded = true;

        }

        // Update is called once per frame

        public bool isVisible(bool init)
        {
            if (init)
                return false;
            if (visible)
                return true;
            else
                return false;
        }
        public void hudUpdate(float dt)
        {
            //if (bossBar.GetCalculateMode() != calculateMode)
            //    bossBar.SetCalulateMode(calculateMode);

            bossBar.updateBar(dt);

            if (visible)
                fader = Mathf.Min(fader + dt, 1f);
            else
                fader = Mathf.Max(0f, fader - dt);

            if (fader > 0f)
            {
                Color color = Color.white;
                color.a = alpha * fader;
                debugColor = color;
                for (int i = 0; i < rootChildren.Count; i++)
                {

                    if (rootChildren[i].gameObject.GetComponent<Image>() != null)
                    {
                        rootChildren[i].gameObject.GetComponent<Image>().material.color = color;

                    }

                    else if (rootChildren[i].gameObject.GetComponent<Text>() != null)
                    {
                        rootChildren[i].gameObject.GetComponent<Text>().material.color = color;
                        //Debug.Log(rootChildren[i].gameObject.GetComponent<Text>().material.color);
                    }
                    else if (rootChildren[i].gameObject.GetComponent<RawImage>() != null)
                        rootChildren[i].gameObject.GetComponent<RawImage>().material.color = color;


                }
                for (int i = 0; i < rootChildren.Count; i++)
                {
                    root.SetActive(true);
                    rootChildren[i].SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < rootChildren.Count; i++)
                {
                    root.SetActive(false);
                    rootChildren[i].SetActive(false);
                }
            }
            //if (calculateMode == hudEnergyBar.CalculateMode.manual)
            //    bossBar.SetBarPercentage(healthP);
            bossBar.Draw();
        }
        public void SetAlpha(float a) { alpha = a; }
        public void SetBossParams(bool Visible, string name, string subtitle, float curenerg, float maxenerg, hudColors colors, hudColors.EnergyBarColors barColors)
        {
            //calculateMode = calculate;
            hudColors = colors;
            EnergyBarColors = barColors;

            visible = Visible;
            if (Visible)
            {
                bossBar.SetFilledColor(EnergyBarColors.filled);
                bossBar.SetShadowColor(EnergyBarColors.shadow);
                bossBar.SetEmptyColor(EnergyBarColors.empty);
                bossBar.SetColor2(Color.white);
                bossBar.SetFilledDrainSpeed(maxenerg);
                bossBar.SetCurrEnergy(curenerg, hudEnergyBar.ESetMode.Normal);
                bossBar.SetMaxEnergy(maxenerg);
                textBoss.text = name;
                textBossSub.text = subtitle;
                //if (bossBar.GetComponent<Image>().color.a != 1 )
                //{
                //    Color a = new Color(bossBar.GetComponent<Image>().color.r, bossBar.GetComponent<Image>().color.g, bossBar.GetComponent<Image>().color.b, 1);
                //    bossBar.GetComponent<Image>().color = a;

                //    SamusPlugin.logger.Log(BepInEx.Logging.LogLevel.Message, "set bossBar image alpha to 1 manually");
                //} 
            }

            curEnergy = curenerg;
            maxEnergy = maxenerg;
            bossIni = true;
        }
        public void SetCurrHealth(float CurrEnergy)
        {
            curEnergy = CurrEnergy;

            bossBar.SetCurrEnergy(CurrEnergy, hudEnergyBar.ESetMode.Normal);

        }
        public void reset()
        {

            bossBar.ResetMaxEnergy();
            bossBar.SetCurrEnergy(0f, hudEnergyBar.ESetMode.Normal);
            textBoss.text = "";
            textBossSub.text = "";
            alpha = 1f;
            fader = 0f;
            visible = false;
            bossIni = false;
        }
        public float GetCurrentHealth() 
        {
            if(bossBar)
                return bossBar.GetSetEnergy();
            else return 0f;
        }
        public float GetMaxEnergy() { return maxEnergy; }
        //public hudEnergyBar.CalculateMode GetCalculateMode() { return calculateMode; }
        //public void SetCalculateMode(hudEnergyBar.CalculateMode calculate) { calculateMode = calculate; }
        //public void SetHealthPer(float per) { healthP = per; }
    }

}
