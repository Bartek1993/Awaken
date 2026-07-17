using System;
using System.Collections;
using LootLocker;
using LootLocker.Requests;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MENU : MonoBehaviour
{
    public AudioSource notification;
    public AudioClip[] notificationClip;
    public GameObject title;
    public GameObject TopBar;
    public GameObject[] UIWindows;
    public GameObject Player;
    public GameObject waveCamera, characterCamera;
    public GameObject wavePanel, characterPanel, statsPanel;
    public int startWave;
    public Slider waveSlider;
    public Text waveText;
    public int difficulty;
    public Text difficultyText;
    public Text scoreText;
    public int score;
    public AudioSource audioSourceButtons;
    public AudioClip[]  buttonClick;
    private string difficutyName;
    public bool UIToggleMode, rotateButton;
    public GameObject accountWindow;
    public TMP_InputField username, password;
    public string currentSession;
    public Text sessionText;
    public string currentUsername, currentUserID, currentUsernamePassword;
    public PlayerStats playerStats;
    public Text[] playerStatsText;
    public int currentSkillPoints, totalAllocatedSkillPoints;
    public Text skillpointsText;
    public Button[] buttonsSkills;
    void Start()
    {
        score = PlayerPrefs.GetInt("score");
        SetPlayerPrefs();
        playerStats = FindFirstObjectByType<PlayerStats>();
        playerStats.SetPlayerStats();
        TopBar.SetActive(false);
        startWave = 1;
        difficulty = 40;
        UIToggleMode = false;
        difficultyText.text = difficutyName;

    }
    // Update is called once per frame
    void Update()
    {
       scoreText.text = score.ToString();
        if (difficulty > 95)
        {
            difficulty = 10;
        }

        if (currentSkillPoints <= 0)
        {
            currentSkillPoints = 0;
        }
        if (currentSkillPoints <= 0)
        {
            foreach (Button button in buttonsSkills)
            {
                button.interactable = false;
            }
            
        }
        else
        {
            foreach (Button button in buttonsSkills)
            {
                button.interactable = true;
            }
        }

        switch (difficulty)
        {
            case 10:
                difficutyName = "STANDARD";
                break;
            case 40:
                difficutyName = "HARD";
                break;
            case 70:
                difficutyName = "CHALLENGE";
                break;
            case 100:
                difficutyName = "NIGHTMARE";
                break;
        }
        difficultyText.text = difficutyName;

        if (UIToggleMode)
        {
            foreach (var VARIABLE in UIWindows)
            {
                VARIABLE.SetActive(true);
            }
        }
        else
        {
            foreach (var VARIABLE in UIWindows)
            {
                VARIABLE.SetActive(false);
            }
        }
        GetPlayerStats();
    }
 private void SetPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("maxHp"))
        {
            PlayerPrefs.SetFloat("maxHp", 0);
        }

        if (!PlayerPrefs.HasKey("hpRegRate"))
        {
            PlayerPrefs.SetFloat("hpRegRate", 0);
        }

        if (!PlayerPrefs.HasKey("physicalAttack"))
        {
            PlayerPrefs.SetFloat("physicalAttack", 0);
        }

        if (!PlayerPrefs.HasKey("criticalChance"))
        {
            PlayerPrefs.SetFloat("criticalChance", 0);
        }

        if (!PlayerPrefs.HasKey("criticalDamage"))
        {
            PlayerPrefs.SetFloat("criticalDamage", 0);
        }

        if (!PlayerPrefs.HasKey("maxMp"))
        {
            PlayerPrefs.SetFloat("maxMp", 100);
        }

        if (!PlayerPrefs.HasKey("mpRegRate"))
        {
            PlayerPrefs.SetFloat("mpRegRate", 0);
        }

        if (!PlayerPrefs.HasKey("magicPower"))
        {
            PlayerPrefs.SetFloat("magicPower", 0);
        }

        if (!PlayerPrefs.HasKey("fireChance"))
        {
            PlayerPrefs.SetFloat("fireChance", 0);
        }

        if (!PlayerPrefs.HasKey("iceChance"))
        {
            PlayerPrefs.SetFloat("iceChance", 0);
        }

        if (!PlayerPrefs.HasKey("moveSpeed"))
        {
            PlayerPrefs.SetFloat("moveSpeed", 0);
        }

        if (!PlayerPrefs.HasKey("weaponRange"))
        {
            PlayerPrefs.SetFloat("weaponRange", 0);
        }

        if (!PlayerPrefs.HasKey("weaponReach"))
        {
            PlayerPrefs.SetFloat("weaponReach", 0);
        }

        if (!PlayerPrefs.HasKey("currentSkillPoints"))
        {
            StartCoroutine("NoSkillPoints");
            PlayerPrefs.SetInt("currentSkillPoints", 4);
        }
        else
        {
            notification.PlayOneShot(notificationClip[1]);
        }

        if (!PlayerPrefs.HasKey("totalSkillPoints"))
        {
            PlayerPrefs.SetInt("totalSkillPoints", 0);
        }
    }

    private IEnumerator NoSkillPoints()
    {
        notification.PlayOneShot(notificationClip[0]);
        yield return new WaitForSeconds(2f);
    }

    public void GetPlayerStats()
    {
       
        currentSkillPoints =  PlayerPrefs.GetInt("currentSkillPoints");
        skillpointsText.text = "SKILL POINTS: " + currentSkillPoints;
        playerStatsText[0].text = "" + playerStats.maxHp;
        playerStatsText[1].text = "" + playerStats.hpRegenRate + "% / sec";
        playerStatsText[2].text = "" + playerStats.baseDamage;
        playerStatsText[3].text = "" + playerStats.maxCritChance + "%";;
        playerStatsText[4].text = "" + playerStats.critDamageMultiplier + "%";
        playerStatsText[5].text = "" + playerStats.maxMana;
        playerStatsText[6].text = "" + playerStats.manaRegenRate + "% / sec";
        playerStatsText[7].text = "" + playerStats.magicStrength;
        playerStatsText[8].text = "" + playerStats.fireChance + "%";
        playerStatsText[9].text = "" + playerStats.iceChance + "%";
        playerStatsText[10].text = "" + playerStats.animator.speed;
        playerStatsText[11].text = "" + playerStats.projectileRange;
        playerStatsText[12].text = "" + playerStats.projectileSpeed;
        
    }



    public void LoadNewScene(int sceneID)
    {
        PlayerPrefs.SetInt("wavedifficulty", difficulty);
        PlayerPrefs.SetInt("startWave", startWave);
        SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Single);
    }

    public void SetDifficulty()
    {
        difficulty += 30;
    }

    public void buttonSound()
    {
        audioSourceButtons.pitch = 1;
        audioSourceButtons.PlayOneShot(buttonClick[0]);
        title.SetActive(false);
    }

    public void onCharacterButton()
    {
        waveCamera.SetActive(false);
        characterCamera.SetActive(true);
        wavePanel.SetActive(false);
        characterPanel.SetActive(true);
        statsPanel.SetActive(false);
    }

    public void onWaveButton()
    {
        waveCamera.SetActive(true);
        characterCamera.SetActive(false);
        wavePanel.SetActive(true);
        characterPanel.SetActive(false);
        statsPanel.SetActive(false);
        score = PlayerPrefs.GetInt("score");
    }
    
    public void onStatsButton()
    {
        
        waveCamera.SetActive(false);
        characterCamera.SetActive(true);
        wavePanel.SetActive(false);
        characterPanel.SetActive(false);
        statsPanel.SetActive(true);
    }


    public void UIToggle()
    {
        UIToggleMode = !UIToggleMode;
    }

    public void RotateCharacter()
    {
        Player.transform.Rotate(new Vector3(0, 22.5f, 0));
    }

    public void AddPlayerStat(int id)
    {
        currentSkillPoints -= 1;
        PlayerPrefs.SetInt("currentSkillPoints", currentSkillPoints);
        int totalSkillPoints = PlayerPrefs.GetInt("totalSkillPoints");
        PlayerPrefs.SetInt("totalSkillPoints", totalSkillPoints + 1);
        switch (id)
        {
            case 1:
                float maxHp = PlayerPrefs.GetFloat("maxHp");
                PlayerPrefs.SetFloat("maxHp", maxHp + 2.5f);
                float hpRegenRate = PlayerPrefs.GetFloat("hpRegRate");
                PlayerPrefs.SetFloat("hpRegRate", hpRegenRate + 0.01f);
                break;
            case 2:
                float baseattack = PlayerPrefs.GetFloat("physicalAttack");
                PlayerPrefs.SetFloat("physicalAttack", baseattack + 0.15f);
                float critticalChance =  PlayerPrefs.GetFloat("criticalChance");
                PlayerPrefs.SetFloat("criticalChance", critticalChance + 0.0015f);
                float critDamage = PlayerPrefs.GetFloat("criticalDamage");
                PlayerPrefs.SetFloat("criticalDamage", critDamage + 0.0015f);
                break;
            case 3:
                float playermovement = PlayerPrefs.GetFloat("moveSpeed");
                PlayerPrefs.SetFloat("moveSpeed", playermovement + 0.001f);
                float weaponRange = PlayerPrefs.GetFloat("weaponRange");
                PlayerPrefs.SetFloat("weaponRange", weaponRange + 0.015f);
                float weaponReach = PlayerPrefs.GetFloat("weaponReach");
                PlayerPrefs.SetFloat("weaponReach", weaponReach + 0.015f);
                break;
            case 4:
                float maxMp = PlayerPrefs.GetFloat("maxMp");
                PlayerPrefs.SetFloat("maxMp", maxMp + 2.5f);
                float mpRegenRate = PlayerPrefs.GetFloat("mpRegRate");
                PlayerPrefs.SetFloat("mpRegRate", mpRegenRate + 0.025f);
                float magicPower = PlayerPrefs.GetFloat("magicPower");
                PlayerPrefs.SetFloat("magicPower", magicPower + 0.025f);
                break;
        }
        playerStats.SetPlayerStats();
      
        
       
       
    }

    public void StartGame(GameObject button)
    {
        title.SetActive(false);
        onCharacterButton();
        TopBar.SetActive(true);
        Destroy(button);
    }


    public void OnAccountSignUp()
    {
        LootLockerSDKManager.WhiteLabelSignUp(username.text, password.text,(response =>
        {
            if (!response.success)
            {
                Debug.Log("something went wrong");
            }
            else
            {
             Debug.Log("user created");   
            }

        }));
    }


    public void OnAccountLogIn()
    {
        LootLockerSDKManager.WhiteLabelLoginAndStartSession(username.text, password.text, false,(responseLogin =>
        {
            if (!responseLogin.success)
            {
                Debug.Log("something went wrong with logging in");
            }
            else
            {
                Debug.Log("user logged in");
                accountWindow.SetActive(false);
                currentUsername = username.text;
                PlayerPrefs.SetString("currentUsername", currentUsername);
                currentUsernamePassword = password.text;
                PlayerPrefs.SetString("currentPassword", password.text);
                currentSession = responseLogin.SessionResponse.session_token;
            }

        }));
    }

    public void GetSkillPoints()
    {
        currentSkillPoints = PlayerPrefs.GetInt("currentSkillPoints");
        skillpointsText.text = currentSkillPoints.ToString();
       
     
    }

}
