using System;
using DevPlz.CombatText;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerStats : MonoBehaviour, ICommonMethods
{
    public Text currentCashText, bankText;
    public Animator animator;
    public float extraAdrenalineGain;
    public float hp, maxHp, hpRegenRate;
    public float weaponDamage, baseDamage;
    public float defence;
    public Image hpBar;
    public float projectileRange, projectileSpeed;
    public float fireDamage;
    public float maxCritChance, critDamageMultiplier;
    public bool isCritical;
    private float randomizeValue;
    public GameObject fireshield;
    public bool fireShieldOn;
    public Image comboMeter;
    public float comboMeterFillAmount, comboMeterMaxAmountMax;
    public int comboRank;
    public Text comboRankText, comboKillCountText, maxComboKillCountText;
    public GameObject comboRankPanel;
    public int comboKillCount, comboKillCountMax;
    public int currentCoins, totalCoins;
    public float invisibilityFramesRoll, invisibilityFramesAfterDamage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        comboRank = 1;
        comboMeterMaxAmountMax = 100;
        fireDamage = 10;
        maxHp = 150;
        baseDamage = 10f;
        hp = maxHp;
        hpRegenRate = 0.01f;
        projectileRange = 0.85f;
        projectileSpeed = 4f;
        weaponDamage = baseDamage;
        maxCritChance = 0.001f;
        critDamageMultiplier = 1.1f;
        invisibilityFramesRoll = 0.1f;
        invisibilityFramesAfterDamage = 0.1f;

    }
    
    // Update is called once per frame
    void Update()
    {

        RewardMethod();
        SetAnimator();
        SetBaseAttributes();
        ShieldMethod();
        ComboMeterSetup();

    }

    private void SetBaseAttributes()
    {
        if (hp > maxHp)
        {
            hp = maxHp;
        }

        if (hp <= 0)
        {
            hp = 0;
        }
        
        hp += hpRegenRate * Time.deltaTime;
        hpBar.fillAmount = hp / maxHp;
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
            currentCashText.text = currentCoins + " X"+ comboRank;
        }
        else
        {
            currentCashText.text = "BROKE";
        }

       
        //bankText.text = totalCoins.ToString();
    }

    private void ComboMeterSetup()
    {
        comboKillCountText.text = "COMBO x "+comboKillCount.ToString();
        maxComboKillCountText.text = "MAX COMBO x" + comboKillCountMax.ToString();
        comboMeter.fillAmount = comboMeterFillAmount / comboMeterMaxAmountMax;
        if (comboMeterFillAmount >= 100)
        {
            comboMeterFillAmount = 10;
            comboRank += 1;
        }
        
        if (comboRank == 1 && comboMeterFillAmount <0.25f)
        {
            comboRankPanel.SetActive(false);
        }

        if (comboRank < 1)
        {
            comboRank = 1;
        }

        if(comboRank>=1)
        {
            comboRankPanel.SetActive(true);
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
                comboMeterFillAmount -= 4.5f * Time.deltaTime;
                break;
            case 2:
                comboRankText.text = "Relentless Hunter";
                comboMeterFillAmount -= 4.5f * Time.deltaTime;
                break;
            case 3:
                comboRankText.text = "Brutal !";
                comboMeterFillAmount -= 4.5f * Time.deltaTime;
                break;
            case 4:
                comboRankText.text = "Absolute !";
                comboMeterFillAmount -= 5.5f * Time.deltaTime;
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
        hp -= damage + defence;
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

        if (currentCoins >= 0)
        {
            CombatText.Spawn(TextStyle.Gold,"+ " +currentCoins, transform.position,null);
        }

        
    }

    public void CollectReward()
    {
        currentCoins += 1;
        CombatText.Spawn(TextStyle.Gold,"+1", transform.position,null);
    }
}
