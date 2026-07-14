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
    public float mana, maxMana, manaRegenRate;
    public float defence;
    public Image hpBar;
    public float projectileRange, projectileSpeed;
    public float fireDamage, iceDamage, earthDamage;
    public float maxCritChance, critDamageMultiplier;
    public float enemyStunTime;
    private float randomizeValue;
    public GameObject fireshield;
    public bool fireShieldOn;
    public Image comboMeter, manaMeter;
    public float comboMeterFillAmount, comboMeterMaxAmountMax;
    public int comboRank;
    public Text comboRankText, comboKillCountText, maxComboKillCountText;
    public int comboKillCount, comboKillCountMax;
    public int currentCoins, totalCoins;
    public float invisibilityFramesRoll, invisibilityFramesAfterDamage;
    public bool canTakeDamage;
    public float iceChance, fireChance;
    public GameObject deathScreen;
    public Text deathScreenText;
    public float magicCooldown, magicConsumption;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        maxMana = 100;
        mana = maxMana;
        manaRegenRate = 5f;
        iceChance = 0.01f;
        fireChance = 0.01f;
        enemyStunTime = 0.085f;
        canTakeDamage = true;
        animator = GetComponent<Animator>();
        comboRank = 1;
        comboMeterMaxAmountMax = 100;
        fireDamage = 2.5f;
        iceDamage = 1f;
        earthDamage = 1.5f;
        maxHp = 100;
        hp = maxHp;
        hpRegenRate = 0.01f;
        baseDamage = 10f;
        projectileRange = 1f;
        projectileSpeed = 4f;
        maxCritChance = 0.01f;
        critDamageMultiplier = 1.05f;
        invisibilityFramesRoll = 0.75f;
        invisibilityFramesAfterDamage = 0.15f;
        magicCooldown = 5f;
        magicConsumption = 15f;

    }
    
    // Update is called once per frame
    void Update()
    {

        RewardMethod();
        SetAnimator();
        SetBaseAttributes();
        ShieldMethod();
        ComboMeterSetup();
        OnDeathScreen();

    }

    private void OnDeathScreen()
    {
        if (hp <= 0)
        {
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
       SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void SetBaseAttributes()
    {
        if (hp > maxHp)
        {
            hp = maxHp;
        }
        
        
        hp += hpRegenRate * Time.deltaTime;
        hpBar.fillAmount = hp / maxHp;

        if (mana > maxMana)
        {
            mana = maxMana;
        }
        mana += manaRegenRate * Time.deltaTime;
        manaMeter.fillAmount = mana / maxMana;
        
    }

    private void ShieldMethod()
    {
        if (fireShieldOn)
        {
            fireshield.SetActive(true);
        }
        else
        {
            fireshield.SetActive(false);
        }
    }

    private void SetAnimator()
    {
        if (animator.speed > 1.5f)
        {
            animator.speed = 1.5f;
        }
    }

    private void RewardMethod()
    {
        if (currentCoins > 0)
        {
            currentCashText.text = currentCoins + " X "+ comboRank;
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
        if (canTakeDamage)
        {
            hp -= damage + defence;
            StartCoroutine("InvincibleFramesDamage");
            if (comboKillCount > comboKillCountMax)
            {
                comboKillCountMax =  comboKillCount;
            }
            CombatText.Spawn(TextStyle.DamagePlayer,"-" +damage.ToString("F1"), transform.position,null);
            comboMeterFillAmount += 1f;
            comboKillCount = 0;
            comboRank = -1;
            if (hp <= 0)
            {
                Time.timeScale = 0.0001f;
            }
            
        }
        
        CombatText.Spawn(TextStyle.Dodge,"-" +"ENEMY MISSED", transform.position,null);

       

        
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
