using System;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        TopBar.SetActive(false);
        startWave = 1;
        if (!PlayerPrefs.HasKey("wavedifficulty"))
        {
            difficulty = 10;
        }
        else
        {
            difficulty = PlayerPrefs.GetInt("wavedifficulty");
        }

        score = PlayerPrefs.GetInt("score");
        UIToggleMode = true;
        
        username.text =  PlayerPrefs.GetString("currentUsername");
        password.text = PlayerPrefs.GetString("currentPassword");

    }
    

    // Update is called once per frame
    void Update()
    {
        sessionText.text = "Session: " + currentSession + "\n" +
                           "Current User: " + currentUsername + "\n"+
                           "User ID: " + currentUserID;
        scoreText.text = score.ToString();
        waveText.text = "ENTRY WAVE " + startWave.ToString();
        difficultyText.text = difficutyName;
        startWave = Mathf.RoundToInt(waveSlider.value);
        if (difficulty > 95)
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
