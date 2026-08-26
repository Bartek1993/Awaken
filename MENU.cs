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
    public GameObject wavePanel, characterPanel;
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
    void Start()
    {
        score = PlayerPrefs.GetInt("score");
        SetPlayerPrefs();
        TopBar.SetActive(false);
        startWave = 1;
        difficulty = 10;
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
    }
 private void SetPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("PiercePower"))
        {
            PlayerPrefs.SetFloat("PiercePower", 0.005f);
        }

        if (!PlayerPrefs.HasKey("PlayerLevel"))
        {
            PlayerPrefs.SetInt("PlayerLevel", 1);
        }

        if (!PlayerPrefs.HasKey("MagicCriticalChance"))
        {
            PlayerPrefs.SetFloat("MagicCriticalChance", 0);
        }
        if (!PlayerPrefs.HasKey("MagicCriticalDamage"))
        {
            PlayerPrefs.SetFloat("MagicCriticalDamage", 1.25f);
        }

        if (!PlayerPrefs.HasKey("ToxicityMax"))
        {
            PlayerPrefs.SetFloat("ToxicityMax", 0);
        }
        
        if(!PlayerPrefs.HasKey("ToxicityThreshold"))
        {
            PlayerPrefs.SetFloat("ToxicityThreshold", 0);
        }

        if (!PlayerPrefs.HasKey("PlayerEXP"))
        {
            PlayerPrefs.SetFloat("PlayerEXP", 0);
        }

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
            PlayerPrefs.SetInt("currentSkillPoints", 1);
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
        
    }

    public void onWaveButton()
    {
        waveCamera.SetActive(true);
        characterCamera.SetActive(false);
        wavePanel.SetActive(true);
        characterPanel.SetActive(false);
        score = PlayerPrefs.GetInt("score");
        difficulty = PlayerPrefs.GetInt("difficulty");
    }
    
    public void onStatsButton()
    {
        
        waveCamera.SetActive(false);
        characterCamera.SetActive(true);
        wavePanel.SetActive(false);
        characterPanel.SetActive(false);
        
    }


    public void UIToggle()
    {
        UIToggleMode = !UIToggleMode;
    }

    public void RotateCharacter()
    {
        Player.transform.Rotate(new Vector3(0, 22.5f, 0));
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

}
