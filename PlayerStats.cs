using System;
using System.Collections;
using DevPlz.CombatText;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerStats : MonoBehaviour, ICommonMethods
{
    public GameObject[] elementals;
    public int totalEnemiesKilled;
    public Text currentCashText, bankText;
    public Animator animator;
    public float extraAdrenalineGain;
    public float hp, maxHp, hpRegenRate;
    public float baseDamage;
    public float mana, maxMana, manaRegenRate, AOEDistance;
    public float exp, maxExp;
    public float defence;
    public Image hpBar, toxicityBar, braveryBar;
    public float projectileRange, projectileSpeed;
    public float fireDamage, iceDamage, thunderDamage, earthDamage;
    public float maxCritChance, critDamageMultiplier;
    public float enemyStunTime;
    private float randomizeValue;
    public Image comboMeter, expMeter;
    public float comboMeterFillAmount, comboMeterMaxAmountMax;
    public int comboRank;
    public Text comboRankText, comboKillCountText, maxComboKillCountText;
    public int comboKillCount, comboKillCountMax;
    public int currentCoins, totalCoins;
    public float expMultiplier;
    public float invisibilityFramesRoll, invisibilityFramesAfterDamage, moveSpeed;
    public bool canTakeDamage;
    public float iceChance, fireChance;
    public GameObject deathScreen;
    public Text deathScreenText;
    public float magicCooldown, magicStrength, magicCritRate, magicCritDamage;
    public float staminaCoolDown;
    public StageProperties stageProperties;
    public float magnetDistance;
    public float frozenTimer;
    public float knockBasckStrength, evadeChance;
    public float lifeStealChance, lifestealamount, thunderChance;
    public float stageExtraDamage, stageExtraMaxHp, stageExtraSpeed, stageExtraIceChance, stageExtraFireChance, 
        stageExtraStunTime, stageExtraFirePower,stageExtraFrozenTime, stageExtraEarthPower, stageExtraThunderPower,
        stageExtraProjectileRange, stageExtraProjectileSpeed, stageExtraRegenRate, stageExtraLifeStealChance, stageExtraLifeStealAmount,
        stageExtraMagnetDistance, stageExtraKnockBackStrength, stageExtraCriticalChance, stageExtraCriticalDamagePower, stageExtraEvadeChance,
        stageExtraDefence, stageExtraInviAfterDamage, stageExtraMagicDistance, stageExtraExpMultiplier, extraPiercePower, magicExtraCriticalRate,
        magicExtraCriticalDamage;

    public float fireOrbSpeed = 5f;
    public float iceOrbSpeed = 5f;
    public float thunderOrbSpeed = 5f;
    public float piercePower;
    public bool isStaticHitBox;
    
    [Tooltip("0 for green, 1 for red, 2 for yellow, 3 for blue")]
    public int [] potionsOwned;
    [Tooltip("0 for green, 1 for red, 2 for yellow, 3 for blue")]
    public int[] herbsOwned;
    public bool playerStatGreen, playerStatRed, playerStatYellow, playerStatBlue;
    public float greenStatTimer,  redStatTimer, yellowStatTimer, blueStatTimer;
    public float toxicityMeterMax, curentToxicity, toxicityDegRate, toxicityAddition, toxicityThreshold;
    public bool setGreenTimer, setRedTimer, setYellowTimer, setBlueTimer;
    public float potionExtraHpAmount, potionExtraHpRegAmount, potionExtraDefenceAmount, potionExtraDamage, potionExtraCriticalDamage, potionExtraCriticalChance;
    public int maxDashBeforeCooldown = 2;
    public int currentDashBeforeCooldown;
    public bool isAttacking;
    public float bravery;
    void Start()
    {
        stageProperties = FindFirstObjectByType<StageProperties>();
        animator = GetComponent<Animator>();
        SetPlayerStats();
        maxHp = 100 + PlayerPrefs.GetFloat("maxHp");
        hp = maxHp;
        exp = 0;
        maxExp = 50;
        currentDashBeforeCooldown = maxDashBeforeCooldown;


    }

    public void SetPlayerStats()
    {
        toxicityMeterMax = 50 + PlayerPrefs.GetFloat("ToxicityMax");
        toxicityThreshold = (toxicityMeterMax * 0.9f) + PlayerPrefs.GetFloat("ToxicityThreshold");
        toxicityDegRate = 0.75f;
        piercePower = 0.15f + PlayerPrefs.GetFloat("PiercePower") + extraPiercePower;
        maxMana = 50 + PlayerPrefs.GetFloat("maxMp");
        mana = maxMana;
        magicStrength = 3f +  PlayerPrefs.GetFloat("magicPower");
        manaRegenRate = 2f + PlayerPrefs.GetFloat("mpRegRate");
        magicCritRate = 0.001f + magicExtraCriticalRate + PlayerPrefs.GetFloat("MagicCriticalChance");
        magicCritDamage = 1.15f + magicExtraCriticalRate + PlayerPrefs.GetFloat("MagicCriticalDamage");
        AOEDistance = 5f + stageExtraMagicDistance;
        float basmagiccooldown = 16;
        magicCooldown = basmagiccooldown - PlayerPrefs.GetFloat("magicCooldown");
        iceChance = 0.01f +  PlayerPrefs.GetFloat("iceChance") + stageExtraIceChance;
        fireChance = 0.01f  +  PlayerPrefs.GetFloat("fireChance") + stageExtraFireChance;
        enemyStunTime = 0.15f + stageExtraStunTime;
        canTakeDamage = true;
        moveSpeed = PlayerPrefs.GetFloat("moveSpeed");
        animator.speed = 0.42f+ moveSpeed + stageExtraSpeed;
        comboRank = 1;
        comboMeterMaxAmountMax = 100;
        fireDamage = 1f * magicStrength + stageExtraFirePower;
        iceDamage = 1f *  magicStrength;
        thunderDamage = 1f * magicStrength + stageExtraThunderPower;
        earthDamage = 1f * magicStrength + stageExtraEarthPower;
        maxHp = 100 + PlayerPrefs.GetFloat("maxHp") + stageExtraMaxHp;
        hpRegenRate = 0.0f + PlayerPrefs.GetFloat("hpRegRate") + stageExtraRegenRate;
        baseDamage = 5f + PlayerPrefs.GetFloat("physicalAttack") + stageExtraDamage;
        projectileRange = 10f + PlayerPrefs.GetFloat("weaponRange") + stageExtraProjectileRange;
        projectileSpeed = 2.5f  + PlayerPrefs.GetFloat("weaponReach") + stageExtraProjectileSpeed;
        maxCritChance = 0.01f +  PlayerPrefs.GetFloat("criticalChance") + stageExtraCriticalChance;
        critDamageMultiplier = 1.1f +  PlayerPrefs.GetFloat("criticalDamage") +  stageExtraCriticalDamagePower;
        invisibilityFramesRoll = 0.75f;
        invisibilityFramesAfterDamage = 0.5f + stageExtraInviAfterDamage;
        float basestamina = 4f;
        staminaCoolDown = basestamina -  PlayerPrefs.GetFloat("staminaCoolDown");
        defence = 1f + stageExtraDefence;
        lifestealamount = 0.25f + stageExtraLifeStealAmount;
        thunderChance = 0.01f;
        lifeStealChance = 0.005f + stageExtraLifeStealChance;
        frozenTimer = 1.5f + stageExtraFrozenTime;
        knockBasckStrength = 170 + stageExtraKnockBackStrength;
        evadeChance = 0.005f + stageExtraEvadeChance;
        magnetDistance = 2.5f + stageExtraMagnetDistance;
        expMultiplier = 1f + stageExtraExpMultiplier;
        
    }

    // Update is called once per frame
    void Update()
    {

        RewardMethod();
        SetAnimator();
        SetBaseAttributes();
        ComboMeterSetup();
        OnDeathScreen();
        SetPlayerBoostStatus();
        


    }

    /// <summary>
    /// FOR NOW IT'S OK, NEED TO SEPARATE STATUSES LATER
    /// </summary>
    public void SetPlayerBoostStatus()
    {
        if (setGreenTimer)
        {
            greenStatTimer -= 1 *  Time.deltaTime;
            if (greenStatTimer <= 0)
            {
                setGreenTimer = false;
                RemovePotionStatus();
            }
        }
        
        if (setRedTimer)
        {
            redStatTimer -= 1 *  Time.deltaTime;
            if (redStatTimer <= 0)
            {
                setRedTimer = false;
                RemovePotionStatus();
            }
        }
    }

    private void RemovePotionStatus()
    {
        maxHp -= potionExtraHpAmount;
        hpRegenRate -= potionExtraHpRegAmount;
        defence -= potionExtraDefenceAmount;
        baseDamage -= potionExtraDamage;
        critDamageMultiplier -=  potionExtraCriticalDamage;
        maxCritChance -= potionExtraCriticalChance;
        
    }

    public void AddPotionStatus(float toxicity,float potionMaxHp, float potionDefence, float potionRegRate, float physicalDamage, float critDamageAmount, float critChanceAmount )
    {
        curentToxicity += toxicity;
        potionExtraHpAmount += potionMaxHp;
        potionExtraDefenceAmount += potionDefence;
        potionExtraHpRegAmount += potionRegRate;
        ////////
        potionExtraDamage += physicalDamage;
        potionExtraCriticalDamage  += critDamageAmount;
        potionExtraCriticalChance += critChanceAmount;
        
        maxHp += potionExtraHpAmount;
        defence += potionExtraDefenceAmount;
        baseDamage += potionExtraDamage;
        critDamageMultiplier +=  potionExtraCriticalDamage;
        maxCritChance += potionExtraCriticalChance;
        

    }

    private void OnDeathScreen()
    {
        if (hp <= 0)
        {
            stageProperties.isPaused =  true;
            deathScreen.SetActive(true);
            UIControllsButtons uiControllsButtons = FindFirstObjectByType<UIControllsButtons>();
            uiControllsButtons.animator.speed = 0;
            uiControllsButtons.attackID = 0;
            uiControllsButtons.isAttacking = false;
            uiControllsButtons.enabled = false;
            var go = FindFirstObjectByType<StageManager>();
            var score = (totalEnemiesKilled * go.waveCount) * go.difficulty ;
            deathScreenText.text = "TOTAL SCORE: " + score;
            StartCoroutine(DisplayDeathScreen(score));
        }
        else
        {
            deathScreen.SetActive(false);
        }
        
        
    }

    private IEnumerator DisplayDeathScreen(int score)
    {
       
       yield return new WaitForSecondsRealtime(2.5f);
       PlayerPrefs.SetInt("score",score);
       stageProperties.isPaused =  false;
       SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void SetBaseAttributes()
    {
        if (hp > maxHp)
        {
            hp = maxHp;
        }

        if (AOEDistance > 40)
        {
            AOEDistance = 40;
        }

        hp += hpRegenRate * Time.deltaTime;
        hpBar.fillAmount = hp / maxHp;
        
        expMeter.fillAmount = exp / maxExp;
        if (exp > maxExp)
        {
           
            stageProperties.isPaused =  true;
            stageProperties.isLevelingUp = true;
            exp = 0;
            maxExp += 50;
        }

        toxicityBar.fillAmount = curentToxicity / toxicityMeterMax;
        curentToxicity -= toxicityDegRate *  Time.deltaTime;
        if (curentToxicity <= 0)
        {
            curentToxicity = 0;
        }

        
        if (curentToxicity > toxicityThreshold)
        {
            hp -= maxHp * 0.025f * Time.deltaTime;
        }

        isAttacking = FindFirstObjectByType<UIControllsButtons>().isAttacking;
        if (isAttacking)
        {
            bravery += 10f * Time.deltaTime;
        }
        else
        {
            bravery -= 25 * Time.deltaTime;
        }

        if (bravery < 0)
        {
            bravery = 0;
        }

        if (bravery > 100)
        {
            bravery = 100;
        }

        braveryBar.fillAmount = bravery / 100f;


    }
    private void SetAnimator()
    {
        if (animator.speed > 1.7f)
        {
            animator.speed = 1.7f;
        }
    }

    private void RewardMethod()
    {
        if (currentCoins > 0)
        {
            currentCashText.text = currentCoins.ToString();
        }
        else
        {
            currentCashText.text = "0";
        }

       
        //bankText.text = totalCoins.ToString();
    }

    private void ComboMeterSetup()
    {
        comboKillCountText.text = "COMBO x "+comboKillCount.ToString();
        maxComboKillCountText.text = "MAX COMBO x " + comboKillCountMax.ToString();
        comboMeter.fillAmount = comboMeterFillAmount / comboMeterMaxAmountMax;
        if (comboMeterFillAmount >= 100)
        {
            comboMeterFillAmount = 10;
            comboRank += 1;
        }

        if (comboRank < 1)
        {
            comboRank = 1;
        }
        

        if (comboRank > 8)
        {
            comboRank = 8;
        }

        switch (comboRank)
        {
            case 0:
                break;
            case 1:
                comboRankText.text = "Mare Mortal";
                comboMeterFillAmount -= 2.5f * Time.deltaTime;
                break;
            case 2:
                comboRankText.text = "Relentless Hunter";
                comboMeterFillAmount -= 2.5f * Time.deltaTime;
                break;
            case 3:
                comboRankText.text = "Brutal !";
                comboMeterFillAmount -= 2.5f * Time.deltaTime;
                break;
            case 4:
                comboRankText.text = "Absolute !";
                comboMeterFillAmount -= 3.5f * Time.deltaTime;
                break;
            case 5:
                comboRankText.text = "Tyrant !";
                comboMeterFillAmount -= 6.5f * Time.deltaTime;
                break;
            case 6:
                comboRankText.text = "Torment !";
                comboMeterFillAmount -= 7.5f * Time.deltaTime;
                break;
            case 7:
                comboRankText.text = "Devour of Souls !";
                comboMeterFillAmount -= 8.5f * Time.deltaTime;
                break;
            case 8:
                comboRankText.text = "HELL BRINGER MAX !!!";
                comboMeterFillAmount -= 10.5f * Time.deltaTime;
                break;
        }
    }
    
    public void TakeDamage(float damage)
    {
        float evadeChanceRandom = Random.value;
        switch (canTakeDamage)
        {
            case true:
                if (evadeChanceRandom < evadeChance)
                {
                    CombatText.Spawn(TextStyle.Dodge,"ENEMY MISSED", transform.position,null);
                }
                else
                {
                    hp -= (damage - defence);
                    StartCoroutine("InvincibleFramesDamage");
                    if (comboKillCount > comboKillCountMax)
                    {
                        comboKillCountMax =  comboKillCount;
                    }
                    CombatText.Spawn(TextStyle.DamagePlayer,"-" +damage.ToString("F1"), transform.position,null);
                    comboMeterFillAmount += 1f;
                    comboKillCount = 0;
                    comboRank = -1;
                }

              
                break;
        }
    }

    private IEnumerator InvincibleFramesDamage()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(invisibilityFramesAfterDamage);
        canTakeDamage = true;
    }

    public void CollectReward()
    {
        currentCoins += 1;
        CombatText.Spawn(TextStyle.Gold,"+1", transform.position,null);
    }

    public void PowerUpNotifier(string notification)
    {
        CombatText.Spawn(TextStyle.Dialogue,"-" + notification, transform.position);
    }

    public IEnumerator SetElemental(GameObject element)
    {
        yield return null;
    }
}
