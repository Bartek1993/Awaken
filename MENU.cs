using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MENU : MonoBehaviour
{
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startWave = 1;
        difficulty = 1;
        score = PlayerPrefs.GetInt("score");
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        waveText.text = "ENTRY WAVE " + startWave.ToString();
        difficultyText.text = difficutyName;
        startWave = Mathf.RoundToInt(waveSlider.value);
        if (difficulty > 3)
        {
            difficulty = 1;
        }

        switch (difficulty)
        {
            case 1:
                difficutyName = "EASY";
                break;
            case 2:
                difficutyName = "STANDARD";
                break;
            case 3:
                difficutyName = "CHALLENGING";
                break;
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
        difficulty += 1;
    }

    public void buttonSound()
    {
        audioSourceButtons.pitch = Random.Range(1f, 1.1f);
        audioSourceButtons.PlayOneShot(buttonClick[0]);

        
    }

}
