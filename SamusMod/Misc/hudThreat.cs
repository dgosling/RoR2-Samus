using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace SamusMod.Misc
{
    public class hudThreat : MonoBehaviour
    {
        enum ThreatStatus { Normal, Warning, Damage };
        hudTypes hudType;
        float damagePulseTimer, damagePulse, threatDist, arrowTimer, warningLerpAlpha, warningColorLerp;
        Transform threatIconTrans;
        ThreatStatus threatStatus;
        bool visible, hasArrows;
        GameObject threatStuff;
        List<GameObject> threatIcon;
        Image arrowUp, arrowDown;
        Text threatWarning;
        hudEnergyBar threatBar;
        float iconTranslateRange;
        hudChangingColors.hudColors hudColors;
        // Material localMat, originalMat;
        //hudTimer lhudTimer;
        float debugTMP;
        float timer;
        public bool ini;
        [SerializeField]
        float debugMaxEnergy;
        [SerializeField]
        float debugIniMaxEnergy;
        uint rendertimings;
        // Start is called before the first frame update
        void Start()
        {
            ini = false;
            string path = "combatHud/Envirorment";
            damagePulse = 0f;
            damagePulseTimer = 0f;
            arrowTimer = 0f;
            warningColorLerp = 0f;
            warningLerpAlpha = 0f;
            threatStatus = ThreatStatus.Normal;
            visible = true;
            iconTranslateRange = 342.5f;
            hudColors = new hudChangingColors.hudColors(true);

            //lhudTimer = new hudTimer();

            threatStuff = gameObject.transform.Find(path).gameObject;
            threatIcon = new List<GameObject>();
            foreach (Transform transform in threatStuff.transform.Find("EnIcon").GetComponentsInChildren<Transform>())
            {
                threatIcon.Add(transform.gameObject);
                //Debug.Log(transform.gameObject + "[" + threatIcon.Count + "]");
            }
            arrowDown = threatStuff.transform.Find("EnIcon/EnarrowDown").gameObject.GetComponent<Image>();
            arrowUp = threatStuff.transform.Find("EnIcon/EnarrowUp").gameObject.GetComponent<Image>();
            threatWarning = threatStuff.transform.Find("EnIcon/warningText").gameObject.GetComponent<Text>();
            threatBar = threatStuff.transform.Find("EnBarBack/EnBar").gameObject.GetComponent<hudEnergyBar>();
            //threatBar.initBars(threatBar.gameObject.GetComponent<Image>(), threatBar.gameObject.transform.parent.Find("EnBarShadow").gameObject.GetComponent<Image>());
            //Debug.Log(threatBar);
            hasArrows = arrowUp != null && arrowDown != null;

            threatIcon[0].GetComponent<Image>().color = hudColors.threatIconColor;
            threatIconTrans = threatIcon[0].transform;

            threatBar.SetFilledColor(hudColors.threatBarFilled);
            //Debug.Log(threatBar.GetFilledColor());
            threatBar.SetShadowColor(hudColors.threatBarShadow);
            //Debug.Log(threatBar.GetShadowColor());
            threatBar.SetEmptyColor(hudColors.threatBarEmpty);
            //Debug.Log(threatBar.GetEmptyColor());
            threatBar.SetTesselation(1f);
            threatBar.SetMaxEnergy(10f);
            threatBar.SetFilledDrainSpeed(9999f);
            threatBar.SetShadowDrainSpeed(9999f);
            threatBar.SetShadowDrainDelay(0f);
            threatBar.SetIsAlwaysResetTimer(false);

            if (threatWarning)
            {
                threatWarning.color = hudColors.threatWarningFont;
                threatWarning.gameObject.GetComponent<Outline>().effectColor = hudColors.threatWarningOutline;
            }

            //localMat = new Material(threatWarning.material);
            //threatWarning.material = localMat;
            debugIniMaxEnergy = threatBar.GetMaxEnergy();
            ini = true;
        }

        // Update is called once per frame


        float fmod(float numer, float denom)
        {
            float tquot = numer / denom;

            tquot = (float)Math.Truncate(tquot);

            return numer - tquot * denom;
        }
        void UpdateVisibility()
        {
            bool vis = visible;
            threatStuff.SetActive(vis);
            if (vis)
                SetThreatDistance(0f);
        }


        public void SetHudType(hudTypes hud) { hudType = hud; }
        public void SetThreatDistance(float ThreatDist) { threatDist = ThreatDist; }
        public void SetIsvisible(bool v)
        {
            visible = v;
            UpdateVisibility();
        }
        public void UpdateHud(float dt)
        {
            threatBar.updateBar(dt);
            debugMaxEnergy = threatBar.GetMaxEnergy();
            Color warningColor = Color.Lerp(hudColors.threatIconColor, hudColors.threatIconWarning, warningColorLerp);

            float maxThreatEnergy = 10f;


            if (arrowTimer > 0f)
            {
                arrowUp.enabled = true;
                arrowTimer = Mathf.Max(0f, arrowTimer - dt);
                Color color = warningColor;
                color.a = arrowTimer / 0.5f;
                arrowUp.color = color;
                arrowDown.enabled = false;
            }
            else if (arrowTimer < 0f)
            {
                arrowDown.enabled = true;
                arrowTimer = Mathf.Min(0f, arrowTimer + dt);
                Color color = warningColor;
                color.a = -arrowTimer / 0.5f;
                arrowDown.color = color;
                arrowUp.enabled = false;
            }
            else
            {
                arrowUp.enabled = false;
                arrowDown.enabled = false;
            }


            if (threatDist <= maxThreatEnergy)
            {
                float tmp = threatDist - (maxThreatEnergy - threatBar.GetSetEnergy());



                debugTMP = tmp;
                if (tmp < -0.01f)
                    arrowTimer = 0.5f;
                else if (tmp > 0.01f)
                    arrowTimer = -0.5f;



            }
            else
                arrowTimer = 0f;

            if (threatDist <= maxThreatEnergy)
            {
                threatBar.SetCurrEnergy(threatBar.GetMaxEnergy() - threatDist, hudEnergyBar.ESetMode.Normal);
                threatIcon[0].GetComponent<Image>().color = warningColor;
            }
            else
            {
                threatBar.SetCurrEnergy(0f, hudEnergyBar.ESetMode.Normal);
                threatIcon[0].GetComponent<Image>().color = hudColors.threatIconSafeColor;
            }

            threatIcon[0].transform.localPosition = new Vector3(0f, Mathf.Max(0f, maxThreatEnergy - threatDist) * iconTranslateRange / maxThreatEnergy, 0f);

            if (threatWarning)
            {
                if (threatBar.GetActualFraction() > 0.8f)
                    threatWarning.enabled = true;
                else
                    threatWarning.enabled = false;

                ThreatStatus newStatus;
                if (maxThreatEnergy == threatBar.GetSetEnergy())
                    newStatus = ThreatStatus.Damage;
                else if (threatBar.GetActualFraction() > 0.8f)
                    newStatus = ThreatStatus.Warning;
                else
                    newStatus = ThreatStatus.Normal;

                if (threatStatus != newStatus)
                {
                    string tString = "";
                    if (newStatus == ThreatStatus.Warning)
                        tString = "Warning";
                    else if (newStatus == ThreatStatus.Damage)
                        tString = "Damage";

                    threatWarning.text = tString;

                    if (threatStatus == ThreatStatus.Normal && newStatus == ThreatStatus.Warning)
                    {
                        //play threatWarning
                    }
                    else if (newStatus == ThreatStatus.Damage)
                    {
                        //play threatDamage
                    }

                    threatStatus = newStatus;
                }
            }

            float oldDPT = damagePulseTimer;
            damagePulseTimer = Mathf.Abs(fmod(GetSecondsMod900(), 0.5f));
            if (damagePulseTimer < 0.25f)
                damagePulse = damagePulseTimer / 0.25f;
            else
                damagePulse = (0.5f - damagePulseTimer) / 0.25f;

            if (threatStatus == ThreatStatus.Damage && damagePulseTimer < oldDPT)
            {
                //play threat damage
            }

            if (threatWarning)
            {
                if (threatStatus != ThreatStatus.Normal)
                {
                    warningLerpAlpha = Mathf.Min(warningLerpAlpha + 2f * dt, 1f);
                    Color color = Color.white;
                    color.a = warningLerpAlpha * damagePulse;
                    threatWarning.material.color = color;
                }
                else
                {
                    warningLerpAlpha = Mathf.Max(0f, warningLerpAlpha - 2f * dt);
                    Color color = Color.white;
                    color.a = warningLerpAlpha * damagePulse;
                    threatWarning.material.color = color;
                }
                if (threatWarning.material.color.a > 0f)
                    threatWarning.enabled = true;
                else
                    threatWarning.enabled = false;
            }

            if (threatStatus == ThreatStatus.Damage)
                warningColorLerp = Mathf.Min(warningColorLerp + 2f * dt, 1f);
            else
                warningColorLerp = Mathf.Max(0f, warningColorLerp - 2f * dt);


        }

        public float GetArrowTimer()
        {
            return arrowTimer;
        }
        public float GetThreatDistance() { return threatDist; }
        public float GetDebugTMP() { return debugTMP; }
        public float GetSetEnergy()
        {

            return threatBar.GetSetEnergy();
        }

        public void Draw()
        {
            threatBar.Draw();
        }

        float GetSecondsMod900()
        {
            rendertimings = (rendertimings + 1) % (900 * 60);
            //Debug.Log(rendertimings / 60f);
            return rendertimings / 60f;
        }
        public void SetRenderTimings(uint renderTimings) { rendertimings = renderTimings; }
    }
}