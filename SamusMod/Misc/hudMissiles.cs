using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using RoR2;

namespace SamusMod.Misc
{
    public class hudMissiles : MonoBehaviour
    {
        public enum invStatus { Normal, Warning, Depleted };
        //hudTypes hudType;
        int missileCapacity, numMissiles;
        float arrowTimer;
        Transform iconTransform;
        float missileWarningAlpha;
        invStatus latestStatus;
        float missileWarningPulse, missileIconAltDeplete, missileIconIncrement;
        [SerializeField]
        bool missilesActive;
        bool visible, hasArrows;
        Image missileIcon, arrowUp, arrowDown;
        Text missileDigits, missileWarning;
        hudEnergyBar missileBar;
        float IconTranslateRange;
        hudColors hudColors;
        //hudTimer timer;
        uint rendertimings;
        public bool missIni;
        bool hasAlt;
        public float debugTMP, debugMissileWarningAlpha, debugWarnPulse;
        float iconLerp, rIconLerp;
        SkillLocator reference;
        //Material localMat;
        // Start is called before the first frame update
        void Awake()
        {
            rendertimings = 0;
            arrowTimer = 0f;
            missileWarningAlpha = 0f;
            latestStatus = invStatus.Normal;
            missileWarningPulse = 0f;
            missileIconAltDeplete = 0f;
            missileIconIncrement = 0f;
            visible = true;
            IconTranslateRange = 342.5f;
            hudColors = new hudColors(true);
            string path = "combatHud/Missiles";
            //hudType = HudType;
            //missileCapacity = MissileCapacity;
            //numMissiles = NumMissiles;
            //missilesActive = MissilesActive;
            missileIcon = gameObject.transform.Find(path + "/MIcon").gameObject.GetComponent<Image>();
            missileDigits = gameObject.transform.Find(path + "/MIcon/count").gameObject.GetComponent<Text>();
            missileBar = gameObject.transform.Find(path + "/MBarBack/MBar").gameObject.GetComponent<hudEnergyBar>();
            missileWarning = gameObject.transform.Find(path + "/MIcon/depleted").gameObject.GetComponent<Text>();
            arrowUp = gameObject.transform.Find(path + "/MIcon/arrowUp").gameObject.GetComponent<Image>();
            arrowDown = gameObject.transform.Find(path + "/MIcon/arrowDown").gameObject.GetComponent<Image>();
            hasArrows = arrowUp && arrowDown;
            iconTransform = missileIcon.transform;
            missileDigits.color = hudColors.missileDigitsFont;
            missileDigits.gameObject.GetComponent<Outline>().effectColor = hudColors.missileDigitsOutline;
            missileIcon.color = hudColors.missileIconColorInactive;
            missileBar.SetEmptyColor(hudColors.missileBarEmpty);
            missileBar.SetFilledColor(hudColors.missileBarFilled);
            missileBar.SetShadowColor(hudColors.missileBarShadow);
            missileBar.SetTesselation(1f);
            missileBar.SetMaxEnergy(5f);
            missileBar.SetFilledDrainSpeed(99f);
            missileBar.SetShadowDrainSpeed(20f);
            missileBar.SetShadowDrainDelay(0.7f);
            missileBar.SetIsAlwaysResetTimer(true);
            rIconLerp = 1f;
            missIni = false;
            if (missileWarning)
            {
                missileWarning.color = hudColors.missileWarningFont;
                missileWarning.gameObject.GetComponent<Outline>().effectColor = hudColors.missileWarningOutline;
            }
            //localMat = new Material(missileDigits.material);
            //missileDigits.material = localMat;
            latestStatus = GetInvStatus();
        }

        // Update is called once per frame

        void UpdateVisibility()
        {
            bool vis = visible;
            missileIcon.gameObject.SetActive(vis);
            missileBar.gameObject.SetActive(vis);
            if (vis)
                UpdateHud(0f);
        }
        public bool isActive(bool init)
        {
            if (init)
                return true;
            if (missileIcon.gameObject.transform.parent.gameObject.activeInHierarchy)
                return true;
            else
                return false;
        }
        public void UpdateHud(float dt)
        {

            if (missileCapacity < 1)
            {
                if (missileIcon.enabled)
                    missileIcon.enabled = false;
            }
            else
            {
                if (!missileIcon.enabled)
                    missileIcon.enabled = true;
            }

            if (missileIconIncrement < 0f)
            {
                missileIconIncrement -= 3f * dt;
                if (missileIconIncrement <= -1f)
                    missileIconIncrement = 1f;
            }
            else if (missileIconIncrement > 0f)
                missileIconIncrement = Mathf.Max(0f, missileIconIncrement - dt);

            Color addColor = hudColors.missileIconColorActive * missileIconIncrement;

            if (missileIconAltDeplete > 0f)
            {
                missileIcon.color = Color.Lerp(hudColors.missileIconColorInactive, hudColors.missileIconDepleteAlt, missileIconAltDeplete) + addColor;
            }
            else
            {
                if (hasAlt)
                {
                    //if (numMissiles > 5)
                    //{
                        missileIcon.color = hudColors.missileIconColorCanAlt + addColor;
                    //}
                    //else
                    //    missileIcon.color = hudColors.missileIconColorNoAlt + addColor;
                }
                else
                {


                    // Color lerp2Test = Color.Lerp(hudColors.missileIconColorInactive, hudColors.missileIconColorActive, iconLerp);
                    if (missilesActive)
                        missileIcon.color = hudColors.missileIconColorActive + addColor;
                    //missileIcon.color = lerp2Test + addColor;
                    else
                        missileIcon.color = hudColors.missileIconColorInactive + addColor;
                    //missileIcon.color = lerp1Test + addColor;
                    //if (iconLerp < 1 && missilesActive) {
                    //missileIcon.color = lerp2Test + addColor;
                    //    if (rIconLerp != 0)
                    //        rIconLerp = 0;
                    //}
                    //if (!missilesActive)
                    //{
                    //    getReverseIconLerp();
                    //    Color lerp1Test = Color.Lerp(hudColors.missileIconColorActive, hudColors.missileIconColorInactive, rIconLerp);
                    //    missileIcon.color = lerp1Test + addColor;
                    //}
                }
            }


            missileIconAltDeplete = Mathf.Max(0f, missileIconAltDeplete - dt);

            missileBar.SetMaxEnergy(missileCapacity);
            missileBar.SetCurrEnergy(numMissiles, hudEnergyBar.ESetMode.Normal);

            missileIcon.transform.localPosition = new Vector3(0f, numMissiles * IconTranslateRange / (float)missileCapacity, 0f);
            if (hasArrows)
            {
                if (arrowTimer > 0f)
                {
                    arrowTimer = Mathf.Max(0f, arrowTimer - dt);
                    Color color = hudColors.missileIconColorActive;
                    color.a *= arrowTimer / 0.5f;
                    arrowUp.color = color;
                    if (!arrowUp.enabled)
                        arrowUp.enabled = true;
                    if (arrowDown.enabled)
                        arrowDown.enabled = false;
                }
                else if (arrowTimer < 0f)
                {
                    arrowTimer = Mathf.Min(0f, arrowTimer + dt);
                    Color color = hudColors.missileIconColorActive;
                    color.a *= -arrowTimer / 0.5f;
                    arrowDown.color = color;
                    if (!arrowDown.enabled)
                        arrowDown.enabled = true;
                    if (arrowUp.enabled)
                        arrowUp.enabled = false;
                }
                else
                {
                    //if (arrowUp.enabled || arrowDown.enabled)
                    //{
                    arrowDown.enabled = false;
                    arrowUp.enabled = false;
                    //}
                }
            }

            if (missileWarning)
            {
                invStatus curStatus = GetInvStatus();
                if (curStatus != latestStatus)
                {
                    string tString = "";
                    switch (curStatus)
                    {
                        case invStatus.Warning:
                            tString = "Missiles Low";
                            break;
                        case invStatus.Depleted:
                            tString = "Depleted";
                            break;
                        default:
                            break;
                    }
                    missileWarning.text = tString;
                    if (latestStatus == invStatus.Normal && curStatus == invStatus.Warning)
                    {
                        //AkSoundEngine.PostEvent(2892136322, gameObject);
                        if(reference.secondary.skillDef==reference.secondary.baseSkill)
                            SamusHUD.playSound(2892136322, gameObject);
                        missileWarningPulse = 7f;
                        
                    }
                    else if (curStatus == invStatus.Depleted)
                    {
                        //AkSoundEngine.PostEvent(2892136322, gameObject);
                        if (reference.secondary.skillDef == reference.secondary.baseSkill)
                            SamusHUD.playSound(2892136322, gameObject);
                        missileWarningPulse = 7f;
                    }
                    latestStatus = curStatus;
                }

                missileWarningPulse = Mathf.Max(0f, missileWarningPulse - dt);
                float warnPulse = Mathf.Min(missileWarningPulse, 1f);
                debugWarnPulse = warnPulse;
                if (latestStatus != invStatus.Normal)
                    missileWarningAlpha = Mathf.Min(missileWarningAlpha + 2f * dt, 1f);
                else
                    missileWarningAlpha = Mathf.Max(0f, missileWarningAlpha - 2f * dt);
                debugMissileWarningAlpha = missileWarningAlpha;
                float tmp = Mathf.Abs(fmod(GetSecondsMod900(), 0.5f));
                debugTMP = tmp;
                if (tmp < 0.25f)
                    tmp = tmp / 0.25f;
                else
                    tmp = (0.5f - tmp) / 0.25f;

                Color color = Color.white;
                color.a = missileWarningAlpha * tmp * warnPulse;
                missileWarning.material.color = color;
                if (missileWarning.material.color.a > 0)
                {
                    //if (!missileWarning.enabled)
                    missileWarning.enabled = true;
                }
                else
                {
                    //if (missileWarning.enabled)
                    missileWarning.enabled = false;
                }


            }
            missileBar.updateBar(dt);
            missileBar.Draw();
        }
        public void SetIsVisible(bool v)
        {
            visible = v;
            UpdateVisibility();
        }
        public void SetIsMissilesActive(bool Active) { missilesActive = Active; }
        public bool GetIsMissilesActive() { return missilesActive; }
        public void SetNumMissiles(int NumMissiles)
        {
            //Debug.Log("NumMissiles: " + NumMissiles);
            int tmp = NumMissiles;
            tmp = Mathf.Clamp(NumMissiles, 0, 999);

            missileDigits.text = tmp.ToString("d3");

            if (numMissiles < tmp)
            {
                arrowTimer = 0.5f;
                missileIconIncrement = -Mathf.Epsilon;
            }
            else if (numMissiles > tmp)
            {
                arrowTimer = -0.5f;
            }

            if (5 + tmp <= numMissiles)
                missileIconAltDeplete = 1f;

            numMissiles = tmp;
            //Debug.Log("numMissiles: " + numMissiles);
        }
        public void SetMissileCapacity(int MissileCapacity) { missileCapacity = MissileCapacity; }
        public invStatus GetInvStatus()
        {
            if (missileBar.GetSetEnergy() == 0f)
                return invStatus.Depleted;
            else if (missileBar.GetActualFraction() < 0.2f && missileBar.GetActualFraction() > 0)
                return invStatus.Warning;
            else
                return invStatus.Normal;
        }

        float fmod(float numer, float denom)
        {
            float tquot = numer / denom;

            tquot = (float)Math.Truncate(tquot);

            return numer - tquot * denom;
        }

        float GetSecondsMod900()
        {
            rendertimings = (rendertimings + 1) % (900 * 60);
            //Debug.Log(rendertimings / 60f);
            return rendertimings / 60f;
        }

        public void missInit(int MissileCapacity, int NumMissiles, bool MissilesActive,SkillLocator skillLocator)
        {
            missileCapacity = MissileCapacity;
            numMissiles = NumMissiles;
            missilesActive = MissilesActive;
            SetNumMissiles(numMissiles);
            reference = skillLocator;
            missIni = true;
        }
        public void setHasAlt(bool alt) { hasAlt = alt; }
        public void SetRenderTimings(uint renderTimings) { rendertimings = renderTimings; }
        //public void SetIconLerp(float lerp) { iconLerp = lerp; }
        //public void getReverseIconLerp()
        //{
        //    if (rIconLerp<1)
        //    {
        //        rIconLerp += (1f / 30f);

        //    }


        //}
    }
}