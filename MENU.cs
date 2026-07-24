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
    public Text[] playerStatNamesText;
    public int currentSkillPoints, totalAllocatedSkillPoints;
    public Text skillpointsText;
    public Button[] buttonsSkills;
    public float vitFill, strengthFill, agilityFill, mindFill, elementalFill;
    void Start()
    {
        score = PlayerPrefs.GetInt("score");
        SetPlayerPrefs();
        playerStats = FindFirstObjectByType<PlayerStats>();
        playerStats.SetPlayerStats();
        TopBar.SetActive(false);
        startWave = 1;
        difficulty = PlayerPrefs.GetInt("difficulty");
        UIToggleMode = false;
        difficultyText.text = difficutyName;

    }
    // Update is called once per frame
    void Update()
    {
       scoreText.text = score.ToString();
        if (difficulty > 100)
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
        if (!PlayerPrefs.HasKey("ElementalLV"))
        {
            PlayerPrefs.SetInt("ElementalLV", 1);
        }
        if (!PlayerPrefs.HasKey("difficulty"))
        {
            PlayerPrefs.SetInt("difficulty", 10);
        }

        if (!PlayerPrefs.HasKey("VitalityLV"))
        {
            PlayerPrefs.SetInt("VitalityLV", 1);
        }

        if (!PlayerPrefs.HasKey("StrengthLV"))
        {
            PlayerPrefs.SetInt("StrengthLV", 1);
        }

        if (!PlayerPrefs.HasKey("AgilityLV"))
        {
            PlayerPrefs.SetInt("AgilityLV", 1);
        }

        if (!PlayerPrefs.HasKey("MindLV"))
        {
            PlayerPrefs.SetInt("MindLV", 1);
        }

        if (!PlayerPrefs.HasKey("staminaCoolDown"))
        {
            PlayerPrefs.SetInt("staminaCoolDown", 0);
        }

        if (!PlayerPrefs.HasKey("magicCooldown"))
        {
            PlayerPrefs.SetFloat("magicCooldown", 0);
        }

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
            PlayerPrefs.SetInt("currentSkillPoints", 8);
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
        playerStatsText[1].text = "" + playerStats.staminaCoolDown.ToString("F2") + " sec";
        playerStatsText[2].text = "" + playerStats.baseDamage;
        playerStatsText[3].text = "" + playerStats.maxCritChance.ToString("F2") + " %";;
        playerStatsText[4].text = "" + playerStats.critDamageMultiplier.ToString("F2") + " %";
        playerStatsText[5].text = "" + playerStats.magicCooldown.ToString("F1") + "sec";
        playerStatsText[6].text = "" + playerStats.magicCooldown +  " sec";
        playerStatsText[7].text = "" + playerStats.magicStrength;
        playerStatsText[8].text = "" + playerStats.fireChance.ToString("F2") + " %";
        playerStatsText[9].text = "" + playerStats.iceChance.ToString("F2") + "%";
        playerStatsText[10].text = "" + playerStats.animator.speed.ToString("F2");
        playerStatsText[11].text = "" + playerStats.projectileRange.ToString("F2");
        playerStatsText[12].text = "" + playerStats.projectileSpeed.ToString("F2");
        
        playerStatNamesText[0].text = "VITALITY LV " + PlayerPrefs.GetInt("VitalityLV");
        playerStatNamesText[1].text = "STRENGTH LV " + PlayerPrefs.GetInt("StrengthLV");
        playerStatNamesText[2].text = "AGILITY LV" + PlayerPrefs.GetInt("AgilityLV");
        playerStatNamesText[3].text = "WISDOM LV " + PlayerPrefs.GetInt("MindLV");
        playerStatNamesText[4].text = "ELEMENTAL LV " + PlayerPrefs.GetInt("ElementalLV");
        
    }



    public void LoadNewScene(int sceneID)
    {
        
        PlayerPrefs.SetInt("startWave", startWave);
        SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Single);
    }

    public void SetDifficulty()
    {
        difficulty += 30;
        PlayerPrefs.SetInt("difficulty", difficulty);
        if (difficulty > 100)
        {
            difficulty = 10;
            PlayerPrefs.SetInt("difficulty", difficulty);
        }


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
        difficulty = PlayerPrefs.GetInt("difficulty");
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
                int vit =  PlayerPrefs.GetInt("VitalityLV");
                PlayerPrefs.SetInt("VitalityLV", vit +1);
                float maxHp = PlayerPrefs.GetFloat("maxHp");
                PlayerPrefs.SetFloat("maxHp", maxHp + 2.5f);
                float staminaCooldownRate = PlayerPrefs.GetFloat("staminaCoolDown");
                PlayerPrefs.SetFloat("staminaCoolDown", staminaCooldownRate + 0.02f);
                break;
            case 2:
                int strength =  PlayerPrefs.GetInt("StrengthLV");
                PlayerPrefs.SetInt("StrengthLV", strength +1);
                float baseattack = PlayerPrefs.GetFloat("physicalAttack");
                PlayerPrefs.SetFloat("physicalAttack", baseattack + 0.35f);
                float critticalChance =  PlayerPrefs.GetFloat("criticalChance");
                PlayerPrefs.SetFloat("criticalChance", critticalChance + 0.0025f);
                float critDamage = PlayerPrefs.GetFloat("criticalDamage");
                PlayerPrefs.SetFloat("criticalDamage", critDamage + 0.0025f);
                break;
            case 3:
                int agility =  PlayerPrefs.GetInt("AgilityLV");
                PlayerPrefs.SetInt("AgilityLV", agility +1);
                float weaponRange = PlayerPrefs.GetFloat("weaponRange");
                PlayerPrefs.SetFloat("weaponRange", weaponRange + 0.015f);
                float weaponReach = PlayerPrefs.GetFloat("weaponReach");
                PlayerPrefs.SetFloat("weaponReach", weaponReach + 0.015f);
                float playermovement = PlayerPrefs.GetFloat("moveSpeed");
                PlayerPrefs.SetFloat("moveSpeed", playermovement + 0.001f);
                break;
            case 4:
                int mind =  PlayerPrefs.GetInt("MindLV");
                PlayerPrefs.SetInt("MindLV", mind + 1);
                float maxMp = PlayerPrefs.GetFloat("maxMp");
                PlayerPrefs.SetFloat("maxMp", maxMp + 1f);
                float mpRegenRate = PlayerPrefs.GetFloat("magicCooldown");
                PlayerPrefs.SetFloat("magicCooldown", mpRegenRate + 0.015f);
                float magicPower = PlayerPrefs.GetFloat("magicPower");
                PlayerPrefs.SetFloat("magicPower", magicPower + 0.25f);
                break;
            case 5:
                int elemental =  PlayerPrefs.GetInt("ElementalLV");
                PlayerPrefs.SetInt("ElementalLV", elemental + 1);
                float ice = PlayerPrefs.GetFloat("iceChance");
                PlayerPrefs.SetFloat("iceChance", ice + 0.005f);
                float fire = PlayerPrefs.GetFloat("fireChance");
                PlayerPrefs.SetFloat("fireChance", fire + 0.005f);
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

    public void GetPlayerLevels()
    {
        playerStatNamesText[0].text = "VITALITY LV " + PlayerPrefs.GetInt("VitalityLV");
        playerStatNamesText[1].text = "STRENGTH LV " + PlayerPrefs.GetInt("StrengthLV");
        playerStatNamesText[2].text = "AGILITY LV " + PlayerPrefs.GetInt("AgilityLV");
        playerStatNamesText[3].text = "WISDOM LV " + PlayerPrefs.GetInt("MindLV");
        playerStatNamesText[4].text = "ELEMENTAL LV " + PlayerPrefs.GetInt("ElementalLV");
        
        
        


    }

}
