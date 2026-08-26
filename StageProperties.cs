using System.Collections;
using System.Collections.Generic;
using a;
using FischlWorks_FogWar;
using PixeLadder.EasyTransition;
using PixeLadder.EasyTransition.Effects;
using UnityEngine;
using UnityEngine.UI;

public class StageProperties : MonoBehaviour
{
    public GameObject player;
    public bool isPaused = false;
    public bool isLevelingUp = false;
    public bool isInPauseMenu = false;
    public bool isDisplayingStats;
    public GameObject mainPauseBar;
    public GameObject playerLevelUpWindow, pauseMenu, displayStats, craftWindow, inventoryWindow;
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
    public GameObject transitionScreen;
    public Transform startPosition;
    public bool isInDialogue = false;
    public GameObject playerUI;
    public GameObject dialogueUI;
    public bool [] path;
    public bool currentPath;
    public Color [] stageAmbientColors;
    public Light ambientLight;
    public csFogWar fogOfWar;
    public Crafting_Panel craftingPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetStageAmbient();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = FindFirstObjectByType<PlayerStats>();
        stageManager = FindFirstObjectByType<StageManager>();
        craftingPanel = FindFirstObjectByType<Crafting_Panel>();
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
        playerUI.gameObject.SetActive(!isInDialogue);
        dialogueUI.gameObject.SetActive(isInDialogue);
        
    }


    public void PauseSwitch()
    {
        isPaused = !isPaused;
        isDisplayingStats = !isDisplayingStats;
        GetPlayerStats();
       
    }

    public void SetStageAmbient()
    {
        int startPath = Random.Range(0, path.Length);
        path[startPath] = true;
        currentPath = path[startPath];
        if (path[0])
        {
            RenderSettings.fogColor = stageAmbientColors[0];
            //fogOfWar.fogColor = stageAmbientColors[0];
            //ambientLight.color = stageAmbientColors[0];
        }

        if (path[1])
        {
            RenderSettings.fogColor = stageAmbientColors[1];
            //fogOfWar.fogColor = stageAmbientColors[1];
            //mbientLight.color = stageAmbientColors[1];
        }

        if (path[2])
        {
            RenderSettings.fogColor = stageAmbientColors[2];
            //fogOfWar.fogColor = stageAmbientColors[2];
            //ambientLight.color = stageAmbientColors[2];
        }

        if (path[3])
        {
            RenderSettings.fogColor = stageAmbientColors[3];
            //fogOfWar.fogColor = stageAmbientColors[3];
            //mbientLight.color = stageAmbientColors[3];
        }

        if (path[4])
        {
            RenderSettings.fogColor = stageAmbientColors[4];
            //fogOfWar.fogColor = stageAmbientColors[4];
            //ambientLight.color = stageAmbientColors[4];
        }
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

    public void LoadNewWave()
    {
        StartCoroutine("LoadNewWaveCoroutine");
    }

    private IEnumerator LoadNewWaveCoroutine()
    {
        
        transitionScreen.SetActive(true);
        isPaused = true;
        stageManager.stages[0].SetActive(false);
        for (int i = 0; i < path.Length; i++)
        {
            path[i] = false;
        }

        player.transform.position = startPosition.position;
        fogOfWar.keepRevealedTiles = false;
        yield return new WaitForSecondsRealtime(1f);
        fogOfWar.keepRevealedTiles = true;
        stageManager.stages[0].SetActive(true);
        isPaused = false;
        stageManager.canSpawn = true;
        stageManager.StatueOfProgression.SetActive(false);
        System.GC.Collect();
        yield return new WaitForSecondsRealtime(0.2f);
        SetStageAmbient();
        transitionScreen.SetActive(false);
        
    }

    public void OpenChoiceDialogueMenu()
    {
        dialogueUI.gameObject.SetActive(true);
    }

    public void OnClose(GameObject go)
    {
        go.SetActive(false);
    }

    public void OnCraftWindowOpen()
    {
        
        inventoryWindow.SetActive(false);
        craftWindow.SetActive(true);
        displayStats.SetActive(false);
    }

    public void OnDisplayStatsOpen()
    {
        inventoryWindow.SetActive(false);
        craftWindow.SetActive(false);
        displayStats.SetActive(true);
    }

    public void OnDisplayInventoryOpen()
    {
        
        inventoryWindow.SetActive(true);
        craftingPanel.ClearReqPanel();
        craftWindow.SetActive(false);
        displayStats.SetActive(false);
    }
}
