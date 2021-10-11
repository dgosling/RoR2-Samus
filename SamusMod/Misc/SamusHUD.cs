using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using RoR2;

namespace SamusMod.Misc
{
    enum EHudState { Combat,Ball,None};
    class SamusHUD:MonoBehaviour
    {

        GameObject baseHUD;
        GameObject damageLight;
        GameObject camFilter;

        hudHealthInterface energyIntf;
        hudThreat threatIntf;
        hudMissiles missileIntf;
        hudDeco decoIntf;
        hudHelmet helmetIntf;
        hudBall ballIntf;
        public hudBossEnergy bossEnergyIntf;
        hudColors hudcolors,combatHudColors,ballHudColors;
        hudColors.EnergyBarColors energyBar, combatEnergyBar, ballEnergyBar;
        float tempHealth;
        float cachedBossHealth;
        EHudState curState;
        //EHudState nextState;
       // EHudState setState;
        bool envDamage;
        float bossAlpha = 1f;
        List<Color> frameColorTest, baseFrame;
        void OnDestroy()
        {
            bossEnergyIntf.reset();
        }
        void Awake()
        {
            baseHUD = gameObject;
            energyIntf = gameObject.GetComponent<hudHealthInterface>();
            threatIntf = gameObject.GetComponent<hudThreat>();
            missileIntf = gameObject.GetComponent<hudMissiles>();
            decoIntf = gameObject.GetComponent<hudDeco>();
            decoIntf.SetSize();
            helmetIntf = gameObject.GetComponent<hudHelmet>();
            helmetIntf.SetSize();
            ballIntf = gameObject.GetComponent<hudBall>();
            bossEnergyIntf = gameObject.GetComponent<hudBossEnergy>();
            combatHudColors = new hudColors(true);
            combatEnergyBar = combatHudColors.GetEnergyBarColors(true);
            ballHudColors = new hudColors(false);
            ballEnergyBar = ballHudColors.GetEnergyBarColors(false);
            timer = new hudTimer();
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
            baseFrame = new List<Color>()
            {
                new Color(0.156863f,0.156863f,0.156863f),
                new Color(0.4f,0.4f,0.4f),
                new Color(0.8f,0.8f,0.8f)
            };
            Color temp = new Color(0.294118f, 0.494118f, 0.639216f, 0.627451f);
            frameColorTest = new List<Color>()
            {
                baseFrame[0]*temp,
                baseFrame[1]*temp,
                baseFrame[2]*temp
            };
        }
        float playerHealth = 0f;
        float maxPlayerHealth = 100f;
        uint totalEnergyTanks = 6;
        uint missileAmount = 0;
        uint missileCapacity = 0;
        uint rendertimings;
        uint filledEnergyTanks = 0;
        bool energyLow;
        bool missileActive;
        Material localMat;
        List<Text> texts;
        float hudDamagePracticalsInit = 0f;
        float hudDamagePracticals=0f;
        float hudDamagePracticalsGain = 0f;
        float damageTime;
        float damageLightPulser;
        float damageFilterAmt;
        float damageFilterAmtInit=1f;
        float damageFilterAmtGain;
        float energyLowTimer=0f;
        float energyLowPulse;
        float missileActiveTimer;
        bool wasDamaged = false;
        //Transform[] lightTransforms;
        public static float DamageAm;
        public bool inMorphBall;
        bool allInit;
        hudTimer timer;
        float timerV;
        CharacterBody character;
        public GameObject bossHealthBarRoot;
        //Transform baseWidgetPivot;
        BossGroup bossInfo;
        Material damageMat;
        private void FixedUpdate()
        {
            
            timerV = timer.solveFTimer();
            if (missileActive)
                missActiveTimer();
            updateHudState();
            //updateDamage();
            //bossInfo = CurrentBossGroup(bossInfo);
            if (!allInit)
                checkInits(Time.fixedDeltaTime);
            HUDUpdate(Time.fixedDeltaTime, true);
        }
        //private void CheckFrameColors()
        //{
        //    GameObject frame = decoIntf.GetFrame();

        //    Material[] mats = frame.GetComponent<MeshRenderer>().materials;
        //    for (int i = 0; i < mats.Length; i++)
        //    {
        //        Debug.Log("material " + i + "'s color: " + mats[i].color);
        //        Debug.Log("adjusted color " + i + ": " + frameColorTest[i]);

        //        //a = frameColorTest[i];
        //    }
        //    for(int i = 0;i<mats.Length;i++)
        //    { 

        //        switch (mats[i].color.r)
        //        {
        //            case 0.046136231834f:
        //                continue;
        //            case 0.2352944f:
        //                continue;
        //            case 0.1176472f:
        //                continue;
        //            case 0.4f:
        //                R2API.Utils.ChatMessage.Send("Didn't change Frame Color!");
        //                return;
        //            case 0.8f:
        //                R2API.Utils.ChatMessage.Send("Didn't change Frame Color!");
        //                return;
        //            case 0.156863f:
        //                R2API.Utils.ChatMessage.Send("Didn't change Frame Color!");
        //                return;

        //            default:
        //                R2API.Utils.ChatMessage.Send("changed frame color to unexpected color!");
        //                return;
        //        }



        //    }
        //    R2API.Utils.ChatMessage.Send("Changed frame to right colors!");
            
        //}
        void checkInits(float dt)
        {
            int check = 4;
            if (!hudcolors.init)
            {
                updateHudState();
                check--;
            }
                
            if (!energyIntf.initHealth)
            {
                UpdateEnergy(dt, true);
                check--;
            }
            if (!bossEnergyIntf.bossIni)
            {
                bossEnergyIntf.SetBossParams(false, "", "", 0, 0, hudcolors,energyBar);
                check--;
            }
            if (!ballIntf.ballini)
            {
                UpdateBallMode(true);
                check--;
            }
            if (check == 4)
                allInit = true;
        }
        void updateHudState()
        {
            if (character.skillLocator.utility.skillNameToken == "DG_SAMUS_UTILITY_DASH_NAME")
            {
                curState = EHudState.Combat;
                return;
            }
                
            inMorphBall = States.SamusMain.morphBall;
            if (inMorphBall)
                curState = EHudState.Ball;
            else
                curState = EHudState.Combat;

            switch (curState)
            {
                case EHudState.Combat:
                    hudcolors = combatHudColors;
                    energyBar = combatEnergyBar;
                    break;
                case EHudState.Ball:
                    hudcolors = ballHudColors;
                    energyBar = ballEnergyBar;
                    break;
                case EHudState.None:
                    break;

            }
        }
        //void InitializeFrameGluePermanent() 
        //{

        //}
        //void InitializeFrameGlueMutable() 
        //{

        //    float lastTankEnergy = playerHealth;
        //    uint tanksFilled = 0;
        //}
        // void UninitializeFrameGlueMutable() { }

        void UpdateEnergy(float dt, bool init) 
        {
            float energy = Mathf.Max(0f, Mathf.Ceil(character.healthComponent.combinedHealth));
            float fullHealth = Mathf.Ceil(character.healthComponent.fullCombinedHealth);
            uint numEnergyTanks = 6;
            uint filledTanks = 0;
            filledTanks = (uint)character.inventory.GetItemCount(RoR2Content.Items.ExtraLife);

            if (!init)
                energyLow = isEnergyLow();

            if (init || energy != playerHealth || numEnergyTanks != totalEnergyTanks||fullHealth!=maxPlayerHealth||filledTanks!=filledEnergyTanks)
            {
                float lastTankEnergy = energy;
                
                
                

                    if (energyIntf.checkEnergyBarIsActive(init))
                    {
                        if(init)
                            energyIntf.initValues(hudTypes.combat, energy,fullHealth, (int)numEnergyTanks, (int)filledTanks,character.healthComponent, false);
                        energyIntf.SetCurrEnergy(energy,fullHealth, false);
                    energyIntf.SetEnergyLow(energyLow);
                    }
                    playerHealth = energy;
                maxPlayerHealth = fullHealth;
                filledEnergyTanks = filledTanks;
            }
            if (bossEnergyIntf)
            {
                bossHealthBarRoot = gameObject.transform.parent.Find("HUDSimple(Clone)").GetComponent<RoR2.UI.HUD>().mainUIPanel.transform.Find("SpringCanvas/TopCenterCluster/BossHealthBarRoot").gameObject;
                bossInfo = bossHealthBarRoot.GetComponent<RoR2.UI.HUDBossHealthBarController>().currentBossGroup;
                
                //GameObject temp = Camera.current.GetComponent<CameraRigController>().hud.mainUIPanel.transform.Find("SpringCanvas/TopCenterCluster/BossHealthBarRoot").gameObject;
                
                if (bossInfo!=null)
                {
                    bossAlpha = bossEnergyIntf.GetCurrentHealth() > 0f ? 1 : 0;
                    RoR2.UI.HUDBossHealthBarController tempBossHealth = bossHealthBarRoot.GetComponent<RoR2.UI.HUDBossHealthBarController>();
                    
                    string bossName = tempBossHealth.bossNameLabel.GetParsedText();
                    string sub = tempBossHealth.bossSubtitleLabel.GetParsedText();
                    bossHealthBarRoot.GetComponent<Canvas>().enabled = false;
                    //if (cachedBossHealth != bossInfo.totalObservedHealth)
                    //{
                    //    SamusPlugin.logger.Log(BepInEx.Logging.LogLevel.Info, "bossEnergyIntf's currentHealth: " + bossEnergyIntf.GetCurrentHealth());
                    //    SamusPlugin.logger.Log(BepInEx.Logging.LogLevel.Info, "BossGroup's Health: " + bossInfo.totalObservedHealth);
                    //    SamusPlugin.logger.Log(BepInEx.Logging.LogLevel.Info, "BossAlpha: " + bossAlpha);
                    //    cachedBossHealth = bossInfo.totalObservedHealth;
                    //}
                    

                    bossEnergyIntf.SetBossParams(true, bossName, sub, bossInfo.totalObservedHealth, bossInfo.totalMaxObservedMaxHealth, hudcolors, energyBar);

                }
                else
                    bossEnergyIntf.SetBossParams(false, "", "", 0f, 0f,hudcolors,energyBar);

            }
        }


        void UpdateMissile(float dt, bool init) 
        {

            uint numMissiles = (uint)character.skillLocator.secondary.stock;
            uint missileCap = (uint)character.skillLocator.secondary.maxStock;
            int sMissiles = character.skillLocator.secondary.stock/5;
            bool tmp = missileIntf.GetIsMissilesActive();
            if (missileIntf.isActive(init))
                missileIntf.setHasAlt(sMissiles >= 1 ? true : false);
            if (init)
                missileIntf.missInit((int)missileCap, (int)numMissiles, false,character.skillLocator);
            if (numMissiles != missileAmount || missileActive != tmp || missileCap != missileCapacity)
            {
                if (missileIntf.isActive(init))
                {
                    if (missileCap != missileCapacity)
                        missileIntf.SetMissileCapacity((int)missileCap);
                    if (numMissiles != missileAmount)
                        missileIntf.SetNumMissiles((int)numMissiles);
                    if (missileActive != tmp)
                        missileIntf.SetIsMissilesActive(tmp);
                }
                missileAmount = numMissiles;
                missileActive = tmp;
                missileCapacity = missileCap;
            }
           
        }
        void UpdateThreatAssessment(float dt) 
        {
            float threatDist = 1000f;
            float dist = 100f;
            float num = float.PositiveInfinity;
            Collider[] colliders = Physics.OverlapSphere(character.transform.position, 100f,LayerIndex.entityPrecise.mask);
            Collider[] colliders1 = Physics.OverlapSphere(character.transform.position, 25f, LayerIndex.projectile.mask);
            if (colliders.Length > 30 || colliders1.Length > 30)
                threatDist = 0f;
            else
            {
                foreach (Collider collider in colliders)
                {
                    float distance = Vector3.Distance(character.transform.position, collider.transform.position);
                    if (distance < num)
                    {
                        HurtBox a = collider.GetComponent<HurtBox>();
                        if (a)
                        {
                            HealthComponent b = a.healthComponent;
                            if (b && (b.gameObject == character.gameObject || b.body.teamComponent.teamIndex <= TeamIndex.Player))
                            {
                                continue;
                            }
                            if (distance == 0)
                            {
                                threatDist = 0;
                                break;
                            }
                            num = distance;
                        }
                    }

                }
                if (threatDist != 0f)
                {
                    foreach (Collider collider in colliders1)
                    {

                        float distance = Vector3.Distance(character.transform.position, collider.transform.position);
                        if (distance < num)
                        {
                            //HurtBox a = collider.GetComponent<HurtBox>();
                            //if (a)
                            //{
                            //    HealthComponent b = a.healthComponent;
                            //    if (b && b.gameObject == character.gameObject)
                            //    {
                            //        continue;
                            //    }
                            //    if (distance == 0)
                            //    {
                            //        threatDist = 0;
                            //        break;
                            //    }
                            //    num = distance;
                            //}
                            RoR2.Projectile.ProjectileController projectileController = collider.transform.root.GetComponentInChildren<RoR2.Projectile.ProjectileController>();
                            if (projectileController)
                            {
                                if (projectileController.teamFilter.teamIndex <= TeamIndex.Player || projectileController.owner == character.gameObject)
                                    continue;
                            }
                            if (distance == 0)
                            {
                                threatDist = 0;
                                break;
                            }
                            num = distance;
                        }


                    }
                }
                if (!float.IsPositiveInfinity(num))
                    threatDist = num;
                else
                    threatDist = dist;
            }
            



            //List<Collider> colliderList = new List<Collider>();
            
            //Collider[] tempColliders;
            //Collider[] tempColliders2;
            //tempColliders = Physics.OverlapBox(character.transform.position, new Vector3(50f, 50f, 50f), character.transform.rotation, LayerIndex.projectile.mask);
            //tempColliders2 = Physics.OverlapBox(character.transform.position, new Vector3(50f, 50f, 50f), character.transform.rotation, LayerIndex.entityPrecise.mask);
            //if (tempColliders.Length > 0)
            //{
            //    foreach (Collider col in tempColliders)
            //    {
            //        if (col.gameObject.transform.root.GetComponentInChildren<RoR2.Projectile.ProjectileController>().owner!=character.gameObject)
            //            colliderList.Add(col);
            //    }
            //}
                
            //if (tempColliders2.Length > 0)
            //{
            //    foreach (Collider coll in tempColliders2)
            //    {
            //        if (coll.gameObject.transform.root.GetComponentInChildren<CharacterBody>() != null) 
            //        {
            //            GameObject root = coll.gameObject.transform.root.gameObject;
            //            if (root.GetComponentInChildren<CharacterBody>().teamComponent.teamIndex >= TeamIndex.Monster && root.GetComponentInChildren<HurtBoxGroup>().mainHurtBox.collider == coll)
            //                colliderList.Add(coll);
            //        }
            //        else
            //        {
            //            R2API.Utils.ChatMessage.Send("Found " + coll.gameObject.name + "'s collider");
                        
            //        }
                        
            //    }
            //}
            //if (colliderList.Contains(character.mainHurtBox.collider))
            //    R2API.Utils.ChatMessage.Send("Still added Samus' collider");
            

            
            //if (colliderList.Count != 0)
            //{
                
                //R2API.Utils.ChatMessage.Send(dt+": found " + colliderList.Count + " colliders");

                //foreach (Collider item in colliderList)
                //{
                //    float tempDist = Vector3.Distance( item.transform.position,temp);
                    
                //    if (tempDist < dist&&item!=character.mainHurtBox.collider)
                //        dist = tempDist;
                //}
                //R2API.Utils.ChatMessage.Send("closest dist: "+dist);
                //if (dist < threatDist)
                //    threatDist = dist;


            //}
            //colliderList.Clear();
            //solveEnvironentDamage();
            //if(envDamage)
            //{
            //    threatDist = 0f;
            //}
            if (threatIntf.isActive(false))
                threatIntf.SetThreatDistance(threatDist);
        }
        void UpdateEnergyLow(float dt) 
        {
            float oldTimer = energyLowTimer;

            energyLowTimer = Mathf.Abs(fmod(GetSecondsMod900(), 0.5f));
            if (energyLowTimer < 0.25f)
                energyLowPulse = energyLowTimer / 0.25f;
            else
                energyLowPulse = (0.5f - energyLowTimer) / 0.25f;

            //Util.PlaySound(Modules.Sounds.lowEnergySFX, character.modelLocator.gameObject);
            //AkSoundEngine.PostEvent(1631926714,gameObject);
            if(energyLow)
                playSound(1631926714,gameObject);
        }
        void InitializeDamageLight()
        {
            //Light light = baseHUD.transform.Find("combatVisor/damageLight").gameObject.GetComponent<Light>();
            //damageLight = baseHUD.transform.Find("combatVisor/damageLight").gameObject;
            //camFilter = damageLight.transform.GetChild(0).gameObject;
            //Color lightColor = hudcolors.hudFrameColor;
            //lightColor *= lightColor.a;
            //lightColor.a = 1f;
            //light.color = lightColor;
            GameObject temp = gameObject.transform.parent.GetComponentInChildren<RoR2.UI.HUD>().gameObject;
            
            damageMat = temp.GetComponent<CameraRigController>().GetComponentInChildren<RoR2.PostProcessing.ScreenDamage>().mat;
            //Transform lightXF = light.transform;
            //lightXF.Translate(0, .08f, -.4f);
            ////Transform testTrans = light.transform;
            ////testTrans.Translate(0, 0, -.4f);
            //damageLight.transform.localPosition = lightXF.localPosition;






        }
        //void UpdateHudDamage(float dt) 
        //{
        //    if (tempHealth>character.healthComponent.combinedHealth && !wasDamaged)
        //    {
        //        damageTime++;
        //        wasDamaged = true;
        //    }
        //    else if (wasDamaged&&damageTime<50)
        //    {
        //        damageTime++;
        //    }

        //    else
        //    {
        //        damageTime = 0f;
        //        wasDamaged = false;
        //        tempHealth = character.healthComponent.combinedHealth;
        //    }
                

        //    float pulseDur = 1f;
        //    float pulseTime = Mathf.Abs(fmod(damageTime, pulseDur));
        //    if (pulseTime < 0.5f * pulseDur)
        //        damageLightPulser = pulseTime / (0.5f * pulseDur);
        //    else
        //        damageLightPulser = (pulseDur - pulseTime) / (0.5f * pulseDur);
        //    damageLightPulser = Mathf.Clamp(1.5f * damageLightPulser * Mathf.Min(0.5f, DamageAm),0f,1f);
        //    //Color damageAmbColor = hudcolors.hudFrameColor;
        //    //damageAmbColor *= damageAmbColor.a;
        //    //damageAmbColor += new Color(damageLightPulser,damageLightPulser,damageLightPulser);
        //    //damageAmbColor.a = 1f;

        //    //if (damageLight)
        //    //    damageLight.GetComponent<Light>().color = damageAmbColor;
        //    if (damageFilterAmt > 0f)
        //    {
        //        damageFilterAmt = Mathf.Max(0f, damageFilterAmt - dt);

        //    }
        //    float tmp = damageFilterAmtInit * 0.8f;
        //    float colorGain;
        //    if (damageFilterAmt > tmp)
        //    {
        //        colorGain = (damageFilterAmtInit - damageFilterAmt) / (damageFilterAmtInit - tmp);
        //    }
        //    else
        //        colorGain = damageFilterAmt / tmp;

        //    colorGain = Mathf.Clamp01(colorGain * damageFilterAmtGain);
        //    Color color0 = new Color(1.0f, 0.403922f, 0.019608f, 0.705882f);
        //    color0.a *= colorGain;

        //    Color color1 = new Color(1.0f, 0.7f, 0.4f, 1.0f);
        //    color1.a *= damageLightPulser;
        //    Color color2 = color0 + color1;

        //    if (color2.a != 0)
        //    {
                
        //        damageMat.color = color2;
        //    }
        //    else
        //        camFilter.SetActive(false);

        //    if (hudDamagePracticals > 0f)
        //    {
        //        hudDamagePracticals = Mathf.Max(0f, hudDamagePracticals - dt);
        //        float practicals = hudDamagePracticals / hudDamagePracticalsInit;
        //        if (energyIntf.checkEnergyBarIsActive(false))
        //            energyIntf.SetFlashMagnitude(practicals);
        //        practicals = Mathf.Min(practicals * hudDamagePracticalsGain, 1f);
        //        helmetIntf.AddHelmetLightValue(practicals);
        //        if (decoIntf.isVisible(false))
        //        {
        //            decoIntf.SetFrameColorValue(practicals);
        //            //if (practicals > 0f)
        //            //{
        //            //    damageLight.GetComponent<Light>().color = new Color(1.0f, 0.403922f, 0.0f, 1.0f) * new Color(practicals, practicals, practicals);
        //            //    damageLight.SetActive(true);
        //            //}
        //            //else
        //            //    damageLight.SetActive(false);
        //        }
        //    }

            
        //}
        void UpdateBallMode(bool init)
        {
            
            if (!ballIntf.isActive(init))
                return;
            if (inMorphBall == true) 
            { 
                uint numPbs = (uint)character.skillLocator.secondary.stock;
                uint pbCap = (uint)character.skillLocator.secondary.maxStock;
                uint bombCount = (uint)character.skillLocator.primary.stock;

                ballIntf.SetBombParams((int)numPbs, (int)pbCap, (int)bombCount, init,hudcolors);
            }
        }
        //void ShowDamage(float dam) 
        //{
        //    hudDamagePracticalsGain = 0.2f * dam + 0f;
        //    hudDamagePracticalsInit = Mathf.Max(Mathf.Epsilon, 0.3f * dam + 0f);

        //    hudDamagePracticals = hudDamagePracticalsInit;
        //    damageFilterAmtGain = 0.05f * dam + 0f;
        //    damageFilterAmtInit = .05f * dam + .05f;
        //    damageFilterAmt = damageFilterAmtInit;

        //}
        static EHudState GetDesiredHudState() 
        {
            bool tmp = States.SamusMain.morphBall;

            switch (tmp)
            {
                case true:
                    return EHudState.Ball;
                case false:
                    return EHudState.Combat;
                default:
                    return EHudState.None;
                    
            }
        }

        public void HUDUpdate(float dt,bool HudVis) 
        {
            bool helmetVisible = false;
           // bool glowVisible = false;
            bool decoVisible = false;
            //if (!inMorphBall)
            //{
            //    switch (HudVis)
            //    {
            //        case true:
            //            helmetVisible = true;
            //            //glowVisible = true;
            //            decoVisible = true;
            //            break;

            //        default:
            //            break;
            //    }
            //}

            switch (curState)
            {
                case EHudState.Combat:
                    helmetVisible = true;
                    decoVisible = true;
                    break;
                case EHudState.Ball:
                    helmetVisible = false;
                    decoVisible = false;
                    break;
            }

            if (decoIntf)
                decoIntf.SetIsVisible(decoVisible);
            if (helmetIntf)
                helmetIntf.SetVisible(helmetVisible);
            if(energyLow)
                UpdateEnergyLow(dt);

            //UpdateHudDamage(dt);

            UpdateEnergy(dt, false);

            if (curState==EHudState.Ball)
            {
                ballIntf.SetActive(true);
                UpdateBallMode(false);

            }
            else if (curState == EHudState.Combat)
            {
                ballIntf.SetActive(false);
                UpdateThreatAssessment(dt);
                UpdateMissile(dt, false);

            }

            //if (DamageAm > 0)
            //    ShowDamage(DamageAm);
            if(bossInfo==null)
                bossAlpha = Mathf.Max(0, bossAlpha - dt);
            if (bossEnergyIntf)
                bossEnergyIntf.SetAlpha(bossAlpha);

            if (bossEnergyIntf)
                bossEnergyIntf.hudUpdate(dt);
            if (energyIntf.checkEnergyBarIsActive(false))
                energyIntf.UpdateHud(dt, energyLowPulse);
            if (threatIntf.isActive(false))
                threatIntf.UpdateHud(dt);
            if (missileIntf.isActive(false))
                missileIntf.UpdateHud(dt);
            threatIntf.Draw();
        }
        //public void Draw(float alpha,bool HudVis) 
        //{
        //    if (nextState == EHudState.None)
        //        return;
        //    if (alpha < 1f) 
        //    {
                
        //    }
        //}
        public GameObject GetBaseHudFrame() { return baseHUD; }

        public void initSamusHUD(CharacterBody characterbody)
        {
            character = characterbody;
            threatIntf.SetHudType(hudTypes.combat);
            threatIntf.SetThreatDistance(100f);
            tempHealth = character.healthComponent.fullCombinedHealth;
            curState = EHudState.Combat;
            //nextState = EHudState.None;
            // setState = EHudState.None;
            if (!hudcolors.init)
                updateHudState();
            //InitializeFrameGluePermanent();
            //InitializeDamageLight();
            UpdateEnergy(0f,true);
            energyLow = false;
            UpdateMissile(0f, true);
            UpdateBallMode(true);
            //CheckFrameColors();
        }

        public bool isEnergyLow()
        {
            const float lowThreshold = .3f;
            if (character.healthComponent.combinedHealthFraction <= lowThreshold)
                return true;
            else
                return false;
        }
        float fmod(float numer, float denom)
        {
            float tquot = numer / denom;

            tquot = (float)Math.Truncate(tquot);

            return numer - tquot * denom;
        }
        public void setMissileActive(bool active)
        {
            missileActive = active;
        }

        void missActiveTimer()
        {
            missileActiveTimer += Time.fixedDeltaTime;
            if (missileActiveTimer >= 1)
            {
                missileActiveTimer = 0f;
                missileActive = false;
            }

        }

        float GetSecondsMod900()
        {
            rendertimings = (rendertimings + 1) % (900 * 60);
            //Debug.Log(rendertimings / 60f);
            return rendertimings / 60f;
            
        }

        //void updateDamage() => GlobalEventManager.onClientDamageNotified += (Action<DamageDealtMessage>)(msg =>
        //{
        //    DamageAm = 0f;
        //    if (!msg.victim || msg.isSilent)
        //        return;
        //    HealthComponent component = msg.victim.GetComponent<HealthComponent>();
        //    if (!component)
        //        return;
        //    if (msg.victim.GetComponent<CharacterBody>() == character)
        //        DamageAm = msg.damage;

        //});
        
        //void solveEnvironentDamage()
        //{
        //    DotController dotController = DotController.FindDotController(character.gameObject);
        //    if (character.HasBuff(RoR2Content.Buffs.OnFire) || character.HasBuff(RoR2Content.Buffs.Poisoned))
        //        envDamage = true;
        //    else if (dotController)
        //    {
        //        if (dotController.HasDotActive(DotController.DotIndex.Helfire) || dotController.HasDotActive(DotController.DotIndex.Blight) || dotController.HasDotActive(DotController.DotIndex.Poison))
        //            envDamage = true;
        //    }
        //    else
        //        envDamage = false;
        //}

        public static void playSound(uint ID,GameObject gameObject) 
        {

            if (!IsEventPlayingOnGameObject(ID, gameObject))
            {
                
                AkSoundEngine.PostEvent(ID, gameObject);
            }
            

        }


        static uint[] playingIds = new uint[50];

        static bool IsEventPlayingOnGameObject(uint eventID, GameObject go)
        {
            uint testEventId = eventID;

            uint count = (uint)playingIds.Length;
            AKRESULT result = AkSoundEngine.GetPlayingIDsFromGameObject(go, ref count, playingIds);

            for (int i = 0; i < count; i++)
            {
                uint playingId = playingIds[i];
                uint eventId = AkSoundEngine.GetEventIDFromPlayingID(playingId);

                if (eventId == testEventId)
                    return true;
            }

            return false;
        }
        static BossGroup CurrentBossGroup(BossGroup inp)
        {

            if (!InstanceTracker.Any<BossGroup>())
                return null;
            BossGroup a=null;
            List<BossGroup> instancesList = InstanceTracker.GetInstancesList<BossGroup>();
            int num = 0;
            //if (instancesList.Count == 0)
            //{
            //    bossInfo = null;
            //    return;
            //}
                
            for (int i = 0; i < instancesList.Count; i++)
            {
                if (instancesList[i].shouldDisplayHealthBarOnHud)
                    num++;
            }
            if (inp)
            {
                if (!inp.shouldDisplayHealthBarOnHud)
                    a = null;
            }
            if (num > 0)
            {
                if (num != 1 && inp!=null)
                    a = null;
                for (int i = 0; i < instancesList.Count; i++)
                {
                    if (instancesList[i].shouldDisplayHealthBarOnHud)
                    {
                        a = instancesList[i];
                        break;
                    }
                }
            }
            else
                a = null;

            return a;
        }
    }
}
