using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public int waveStartnumber;
    public int difficulty;
    public GameObject powerUpPanel;
    public GameObject stageGround;
    public GameObject player;
    public AudioSource stageMusic;
    public AudioClip[] songs;
    public Material stageGroundMaterial;
    public int enemyCount;
    public bool canSpawn;
    public int maxEnemySpawn;
    public int killCount;
    public int enemiesToKill;
    public int waveCount;
    public float sec, min, hour;
    public int spawnCount;
    public float cooldownTimer;
    public int spawnRate;
    public float spawnTimer;
    public Text waveText;
    public bool isWaveFinished;
    public Vector3 stageOffset;
    public float waveWeight;
    public int enemyVariationMax;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveStartnumber = PlayerPrefs.GetInt("startWave");
        difficulty = PlayerPrefs.GetInt("wavedifficulty");
        enemyVariationMax = 2 * difficulty;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 1080, true);
        player = GameObject.FindGameObjectWithTag("Player");
        spawnTimer = 0.65f;
        spawnRate = 1;
        cooldownTimer = 1f;
        canSpawn = true;
        waveCount = waveStartnumber;
        maxEnemySpawn = (40 + difficulty) * waveStartnumber;
        enemiesToKill = maxEnemySpawn;
        killCount = 0;
        isWaveFinished = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stageMusic.isPlaying)
        {
            stageMusic.PlayOneShot(songs[Random.Range(0, songs.Length)]);
        }
        powerUpPanel.SetActive(isWaveFinished);
        stageGround.transform.position = player.transform.position +  stageOffset;
        waveText.text = waveCount.ToString();
        if (spawnTimer > 2.5f)
        {
            spawnTimer = 2.5f;
        }

        if (killCount >= enemiesToKill)
        {
            if (!isWaveFinished)
            {
                isWaveFinished = true;
                canSpawn = false;
                cooldownTimer = 2f;
                waveCount += 1;
                killCount = 0;
                canSpawn = true;
                spawnCount = 0;
                waveWeight = 0;
                spawnTimer -= 0.05f;
                if (waveCount % 1 == 0)
                {
                    maxEnemySpawn += 10;
                    int skillpoint = PlayerPrefs.GetInt("currentSkillPoints");
                    PlayerPrefs.SetInt("currentSkillPoints", skillpoint + 1);
                    
                }
                
                
                if (waveCount % 5 == 0)
                {
                    enemyVariationMax += 1;
                    
                    
                    
                }

                

                enemiesToKill = maxEnemySpawn;
            }
        }

        if (spawnCount >= maxEnemySpawn)
        {
            canSpawn = false;
        }

        if (waveWeight >= 200)
        {
            canSpawn = false;
            waveWeight = 300;
            enemiesToKill = spawnCount;
        }

        if (maxEnemySpawn >= 200)
        {
            maxEnemySpawn = 200;
            enemiesToKill = maxEnemySpawn;
        }

        if (isWaveFinished)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (spawnTimer <= 0.25f)
        {
            spawnTimer = 0.25f;
        }


        StageTimer();
        if (enemyVariationMax > 5)
        {
            enemyVariationMax = 5;
        }
    }

   

    private void StageTimer()
    {
        
        
        sec += Time.deltaTime;
        if (sec > 59.99f)
        {
            min += 1;
            sec = 0;
        }
        if (min > 59.9)
        {
            hour += 1;
            min = 0;
        }
    }

    
}
