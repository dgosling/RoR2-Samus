using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class samusHUDtester : MonoBehaviour
{
    // Start is called before the first frame update
    hudThreat threat;
    hudTimer hudTimer;
    [SerializeField]
    float timer;
    [SerializeField]
    [Range(0f,10f)]
    float testDistance;
    float cachedDistance;
    hudBall hudBall;
    hudMissiles hudMissiles;
    hudHealthInterface hudHealth;
    hudBossEnergy hudBossEnergy;
    [SerializeField]
    [Range(0f,99f)]
    float currEnergy;
    float missileTimer;
    uint mRenderTime;
    bool missileTimerRunning;
    //[SerializeField]
    //float TMP,missileAlpha,DebugWarningPulse,debugLowTimer;
    [SerializeField]
    int bombs, maxMiss,totalTanks, numTanks,PBamount;
    [SerializeField]
   // [Range(0,10)]
    int currMiss;
    int cachedBombs, cachedMaxMiss, cachedCurrMis, cachedTotalTanks, cachedNumTanks, cachedPBs;
    float cachedCurrEnergy, cachedBossHealth;
    //[SerializeField]
    float energyLowTimer,bossAlphaValue,bossAlphaTime;
   // [SerializeField]
    float energyLowPulse;
    Material localMat;
    List<Text> texts;
    [SerializeField]
    hudMissiles.invStatus invStatus;
  [SerializeField]
    [Range(0f, 100f)]
    float bossHealth;
    [SerializeField]
    bool debugMissilesActive;
    //[SerializeField]
    
    //float bossHealthP;



    [SerializeField]
    [Range(0f, 1f)]
    float HealthP;
    float cachedHealthP;
    //[SerializeField]
    float tempTime;

    [SerializeField]
    hudTypes hudType;

    hudTypes cachedHudType;
    hudDeco hudDeco;
    //Animator hudAnimator;

    //[SerializeField]
    //Color debugColor;
    [SerializeField]
    GameObject BallHud, CombatHud, Helmet,boss;
    hudHelmet hudHelmet;
    [SerializeField]
    bool activeBoss;
    uint rendertimings;

    [SerializeField]
    float debugBossMaxHealth,debugBossHealth;
    void Start()
    {
        threat = gameObject.GetComponent<hudThreat>();
        bossAlphaValue = 1f;
        bossAlphaTime = 1f;
        hudTimer = new hudTimer();
        cachedDistance = 0f;
        hudBall = gameObject.GetComponent<hudBall>();
        tempTime = 0f;
        hudMissiles = gameObject.GetComponent<hudMissiles>();
        timer = 0f;
        hudHelmet = gameObject.GetComponent<hudHelmet>();
        hudDeco = gameObject.GetComponent<hudDeco>();
        //ah = 0f;
        //Debug.Log(hudMissiles);
        hudHealth = gameObject.GetComponent<hudHealthInterface>();
        hudBossEnergy = gameObject.GetComponent<hudBossEnergy>();
        energyLowTimer = 0f;
        energyLowPulse=0f;
        missileTimer = 0f;
        missileTimerRunning = false;
        texts = new List<Text>();
        foreach (Transform item in gameObject.transform.GetComponentsInChildren<Transform>(true)) 
        { 
            if (item.gameObject.GetComponent<Text>() != null)
            {
                texts.Add(item.gameObject.GetComponent<Text>());
                //Debug.Log("added text: " + item.gameObject);
            }
        }
        
        foreach (Text item in texts)
        {
            localMat = new Material(item.material);
            

            item.material = localMat;
            
        }
        //hudDeco.SetIsVisible(true);
        //hudAnimator = gameObject.GetComponent<Animator>();
    }


    

    // Update is called once per frame
    void FixedUpdate()
    {
        timer = hudTimer.solveFTimer();
        if (threat.ini) { 
            //if (!CheckcachedDistance(testDistance))
            //{
                threat.SetThreatDistance(testDistance);
                cachedDistance = testDistance;
                //Debug.Log("cached");
           // }
            

            
            threat.UpdateHud(timer);
            //TMP = threat.GetDebugTMP();
            //debugArrowTimer = threat.GetArrowTimer();
            //debugGetSetEnergy = threat.GetSetEnergy();
        }
        else if (!threat.ini)
        {
            threat.SetHudType(hudTypes.combat);
            threat.SetThreatDistance(10f);
            cachedDistance = 10;
           
        }

        if (hudBall.ballini)
        {
            if (!checkCachedBombs(bombs) || !checkCachedPB(PBamount))
            {
                hudBall.SetBombParams(PBamount, 1, bombs, false);
                cachedBombs = bombs;
                cachedPBs = PBamount;
            }
            
            
        }
        else if (!hudBall.ballini)
        {
            hudBall.SetBombParams(1, 1, 3, true);
            cachedBombs = 3;
            cachedPBs = 1;
            
        }
            
        if (hudMissiles.missIni==true)
        {
            hudMissiles.UpdateHud(timer);
            if (!checkCachedmissiles(currMiss))
            {


                hudMissiles.SetNumMissiles(currMiss);
                hudMissiles.SetIsMissilesActive(true);
                if(!missileTimerRunning)
                    missileTimerRunning = true;
                else
                {
                    mRenderTime = 0;
                }
                
                cachedCurrMis = currMiss;
            }
            missActiveTimer();
            debugMissilesActive = hudMissiles.GetIsMissilesActive();
            if (!checkCachedMaxmissiles(maxMiss))
            {
                hudMissiles.SetMissileCapacity(maxMiss);
                cachedMaxMiss = maxMiss;
            }
            //invStatus = hudMissiles.GetInvStatus();
            
            //warningPulse = hudMissiles.debugWarnPulse;
            //missileAlpha = hudMissiles.debugMissileWarningAlpha;
        }
        else if (!hudMissiles.missIni)
        {
            hudMissiles.missInit(5, 5, false);
            cachedCurrMis = 5;
            cachedMaxMiss = 5;
        }

        if (hudHealth.initHealth)
        {
            hudHealth.SetEnergyLow(IsEnergyLow());
            //if (hudAnimator)
            //{
                //if (IsEnergyLow())
                  //  hudAnimator.StopPlayback();
                //else
              //      hudAnimator.StartPlayback();
               
            //}
            //else
            //{
                UpdateEnergyLow(tempTimer());
                hudHealth.UpdateHud(timer, energyLowPulse);
            //}
            

            
            
            
            //DebugWarningPulse = energyLowPulse;
            //debugLowTimer = energyLowTimer;

            
            if (!checkCachedEnergy(currEnergy))
            {
                hudHealth.SetCurrEnergy(currEnergy,false);
                cachedCurrEnergy = currEnergy;
            }
            if (!checkCachedtanks(numTanks))
            {
                hudHealth.SetNumFilledEnergyTanks(numTanks);
                cachedNumTanks = numTanks;
            }
            if (!checkCachedTotalTanks(totalTanks))
            {
                hudHealth.SetNumTotalEnergyTanks(totalTanks);
                cachedTotalTanks = totalTanks;
            }
            if (!checkCachedHudType(hudType))
            {
                hudHealth.setHudType(hudType);
                cachedHudType = hudType;
            }

            //if (!chechCachedHealthP(HealthP)&&hudHealth.GetCalculateMode()==hudEnergyBar.CalculateMode.manual)
            //{
            //    hudHealth.SetHealthP(HealthP);
            //    cachedHealthP = HealthP;
            //}
        }
        else if (!hudHealth.initHealth)
        {
            if (hudType == hudTypes.combat)
                hudHealth.initValues(hudTypes.combat, 100f, 6, 1, false);
            else
                hudHealth.initValues(hudType, 100f, 6, 1, false);
            cachedCurrEnergy = 100;
            cachedNumTanks = 0;
            cachedTotalTanks = 6;
            cachedHudType = hudType;
        }

        if (hudBossEnergy.bossIni && activeBoss == true)
        {
            if (!chechCachedBossHealth(bossHealth))
            {
                hudBossEnergy.SetCurrHealth(bossHealth);


                debugBossHealth = hudBossEnergy.GetCurrentHealth();
                debugBossMaxHealth = hudBossEnergy.GetMaxEnergy();
            }
            //if (!chechCachedBossP(bossHealthP))
            //{
            //    hudBossEnergy.SetHealthPer(bossHealthP);
            //    cachedBossHealthP = bossHealthP;
            //}
            hudBossEnergy.SetAlpha(bossAlpha(timer));
            hudBossEnergy.hudUpdate(timer);
            //debugColor = hudBossEnergy.debugColor;
        }
        else if (!hudBossEnergy.bossIni && activeBoss == true)
        {
            boss.SetActive(true);
            hudBossEnergy.SetBossParams(true, "Example Name", "Example Sub", bossHealth, 100f);
            cachedBossHealth = bossHealth;
            
        }
        else if (activeBoss == false)
            boss.SetActive(false);
        if (hudType == hudTypes.combat)
        {
            BallHud.SetActive(false);
            if (!CombatHud.activeInHierarchy || !Helmet.activeInHierarchy)
            {
                CombatHud.SetActive(true);
                Helmet.SetActive(true);
            }
        }
        else if(hudType == hudTypes.ball)
        {
            CombatHud.SetActive(false);
            Helmet.SetActive(false);
            if (!BallHud.activeInHierarchy)
                BallHud.SetActive(true);
        }
        threat.Draw();
    }

    private void Update()
    {
        
    }

    bool CheckcachedDistance(float test)
    {
        //if (cachedDistance != test)
        //{
        //    threat.SetThreatDistance(test);
        //    cachedDistance = test;
        //}
        if (cachedDistance != test)
            return false;
        else
            return true;
        
    }

    bool checkCachedmissiles(int test)
    {
        if (test != cachedCurrMis)
            return false;
        else
            return true;
    }

    bool checkCachedMaxmissiles(int test)
    {
        if (test != cachedMaxMiss)
            return false;
        else
            return true;
    }

    bool checkCachedBombs(int test)
    {
        if (test != cachedBombs)
            return false;
        else
            return true;
    }
    bool checkCachedEnergy(float test)
    {
        if (test != cachedCurrEnergy)
            return false;
        else
            return true;
    }

    bool checkCachedtanks(int test)
    {
        if (test != cachedNumTanks)
            return false;
        else
            return true;
    }

    bool checkCachedPB(int test)
    {
        if (test != cachedPBs)
            return false;
        else
            return true;
    }
    bool checkCachedTotalTanks(int test)
    {
        if (test != cachedTotalTanks)
            return false;
        else
            return true;
    }
    bool chechCachedBossHealth(float test)
    {
        if (test != cachedBossHealth)
            return false;
        else
            return true;
    }

    bool checkCachedHudType(hudTypes test)
    {
        if (test != cachedHudType)
            return false;
        else
            return true;
    }
    //bool chechCachedBossP(float test)
    //{
    //    if (test != cachedBossHealthP)
    //        return false;
    //    else
    //        return true;
    //}
    //bool chechCachedHealthP(float test)
    //{
    //    if (test != cachedHealthP)
    //        return false;
    //    else
    //        return true;

    //}
    float fmod(float numer, float denom)
    {
        float tquot = numer / denom;

        tquot = (float)Math.Truncate(tquot);

        return numer - tquot * denom;
    }
    void UpdateEnergyLow(float dt)
    {
        float oldTimer = energyLowTimer;

        energyLowTimer =Mathf.Abs(fmod(GetSecondsMod900(), 0.5f));
        if (energyLowTimer < 0.25f)
            energyLowPulse = energyLowTimer / 0.25f;
        else
            energyLowPulse = (0.5f - energyLowTimer) / 0.25f;

        //if energyLow and energyLowTimer < oldTimer: play energyLow SFX
    }
    void missActiveTimer()
    {
        if (missileTimerRunning && hudMissiles.GetIsMissilesActive())
        {
            
            mRenderTime = (mRenderTime + 1) % (900 * 60);
            missileTimer = mRenderTime / 60f;
            if (missileTimer >= 1)
            {
                
                hudMissiles.SetIsMissilesActive(false);
                missileTimerRunning = false;
                missileTimer = 0;
                mRenderTime = 0;
            }

                

        }
    }
    bool IsEnergyLow()
    {
        float lowThreathold = hudHealth.numTanksFilled < 1 ? .3f :0f;
        return hudHealth.cEnergyBar.GetActualFraction() < lowThreathold;
    }

    float bossAlpha(float dt)
    {
        
        if (bossAlphaTime > 0f)
            bossAlphaValue = Mathf.Min(bossAlphaTime, 1f);

        bossAlphaTime = Mathf.Max(0f, bossAlphaTime - dt);
        return 1 - bossAlphaValue;



    }
    float tempTimer()
    {
        tempTime += Time.fixedDeltaTime;
        if (tempTime >= 1f)
        {
            tempTime = 0f;
            return 1f;
        }
        else if (tempTime < 1f && tempTime > 0f)
            return tempTime;
        else
            return 0;
    }

    float GetSecondsMod900()
    {
        rendertimings = (rendertimings + 1) % (900 * 60);
        //Debug.Log(rendertimings / 60f);
        return rendertimings / 60f;
    }
}
