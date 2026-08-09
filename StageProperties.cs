using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageProperties : MonoBehaviour
{
    public bool isPaused = false;
    public bool isLevelingUp = false;
    public bool isInPauseMenu = false;
    public bool isDisplayingStats;
    public GameObject mainPauseBar;
    public GameObject playerLevelUpWindow, pauseMenu, displayStats;
    public Text rewardText, rewardDescription;
    public GameObject rewardWindow;
    public PlayerStats playerStats;
    public StageManager stageManager;
    public Text[] playerStatsText;
    public Text playtimeTotalText;
    public float playtimeMin, playtimeSec;
    public float timeTillOverride;
    public AudioSource audioSource;
    public AudioClip[] clips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        stageManager = FindFirstObjectByType<StageManager>();
        rewardWindow.SetActive(false);
        rewardDescription.gameObject.SetActive(false);
        rewardText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            Time.timeScale = 0.0001f;
        }
        else
        {
            Time.timeScale = 1;
        }
        playtimeSec += Time.deltaTime;
        if (playtimeSec >= 59.9)
        {
            playtimeSec = 0;
            playtimeMin += 1;
        }

        if (playtimeMin >= 59.9)
        {
            playtimeMin = 0;
            playtimeSec = 0;
        }
        playtimeTotalText.text = playtimeMin.ToString("F0") + " : " + playtimeSec.ToString("F0");
        playerLevelUpWindow.SetActive(isLevelingUp);
        pauseMenu.SetActive(isPaused);
        displayStats.SetActive(isDisplayingStats);
        mainPauseBar.SetActive(isInPauseMenu);
    }


    public void PauseSwitch()
    {
        isPaused = !isPaused;
        isDisplayingStats = !isDisplayingStats;
        GetPlayerStats();
       
    }

    public void PauseMenuBarSwitch()
    {
        isInPauseMenu = !isInPauseMenu;
    }




    public void GetPlayerStats()
    {
        playerStatsText[0].text = "HP: " + playerStats.hp +  " / " + playerStats.maxHp;
        playerStatsText[1].text = "DEFENCE: " + playerStats.defence;
        playerStatsText[2].text = "LIFESTEAL CHANCE: " + playerStats.lifeStealChance + "%";
        playerStatsText[3].text = "LIFESTEAL AMOUNT: " + playerStats.lifestealamount;
        playerStatsText[4].text = "KNOCKBACK STRENGTH: " + playerStats.knockBasckStrength;
        playerStatsText[5].text = "BASE ATTACK: " + playerStats.baseDamage;
        playerStatsText[6].text = "CRITICAL CHANCE: " + playerStats.maxCritChance + "%";
        playerStatsText[7].text = "CRITICAL DAMAGE: x" + playerStats.critDamageMultiplier;
        playerStatsText[8].text = "MAGIC POWER: " + playerStats.magicStrength;
        playerStatsText[9].text = "MAGIC COOLDOWN: " + playerStats.magicCooldown +"s";
        playerStatsText[10].text = "FIRE POWER: " + playerStats.fireDamage;
        playerStatsText[11].text = "FIRE CHANCE: " + playerStats.fireChance + "%";
        playerStatsText[12].text = "ICE CHANCE: " + playerStats.iceChance + "%";
        playerStatsText[13].text = "FREEZE TIME: " + playerStats.frozenTimer+ "s";
        playerStatsText[14].text = "MAGIC DISTANCE: " + playerStats.AOEDistance + "m";
        playerStatsText[15].text = "HP REG RATE: " + playerStats.hpRegenRate + "p/s";
        playerStatsText[16].text = "MOVEMENT SPEED: " + playerStats.animator.speed;
        playerStatsText[17].text = "PROJECTILE RANGE: " + playerStats.projectileRange;
        playerStatsText[18].text = "PROJECTILE SPEED: " + playerStats.projectileSpeed + "m";
        playerStatsText[19].text = "EXP x " + playerStats.expMultiplier;
    }


    public IEnumerator OpenChest(string itemName, string itemDescription, GameObject chestObject, ChestItemSO chestItem, int cost)
    {
        rewardWindow.SetActive(true);
        rewardText.gameObject.SetActive(true);
        rewardText.text = itemName;
        rewardDescription.text = itemDescription;
        rewardDescription.gameObject.SetActive(true);
        isPaused = true;
        yield return new WaitForSecondsRealtime(3f);
        isPaused = false;
        rewardText.gameObject.SetActive(false);
        rewardText.gameObject.SetActive(false);
        rewardText.text = "";
        rewardDescription.text = "";
        Destroy(chestObject);
        chestItem.AddItem(playerStats, stageManager);
        rewardWindow.SetActive(false);
        playerStats.currentCoins -= cost;

    }


    public void PlayCheerVoid()
    {
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}
