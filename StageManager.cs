using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    public int randStage = Random.Range(0, 4);
    public GameObject [] stages;
    public GameObject[] bosses;
    public StageProperties stageProperties;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
    }

    void Start()
    {
        stageProperties = FindFirstObjectByType<StageProperties>();
        waveStartnumber = PlayerPrefs.GetInt("startWave");
        difficulty = PlayerPrefs.GetInt("difficulty");
        enemyVariationMax = 2;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 1080, true);
        player = GameObject.FindGameObjectWithTag("Player");
        spawnTimer = 1.25f;
        spawnRate = 1;
        cooldownTimer = 1f;
        canSpawn = true;
        waveCount = waveStartnumber;
        maxEnemySpawn = 10 + difficulty;
        enemiesToKill = maxEnemySpawn;
        killCount = 0;
        isWaveFinished = false;
        if (difficulty > 10)
        {
            stages[0].SetActive(false);
            stages[1].SetActive(true);
        }

        if (difficulty <= 10)
        {
            stages[0].SetActive(true);
            stages[1].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!stageMusic.isPlaying)
        {
            stageMusic.PlayOneShot(songs[Random.Range(0, songs.Length)]);
        }
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
                
                //canSpawn = false;
                cooldownTimer = 0f;
                waveCount += 1;
                killCount = 0;
                canSpawn = true;
                spawnCount = 0;
                waveWeight = 0;
                if (waveCount % 1 == 0)
                {
                    maxEnemySpawn += 10;
                    
                    
                }
                if (waveCount % 2 == 0)
                {
                    enemyVariationMax += 1;
                    int skillpoint = PlayerPrefs.GetInt("currentSkillPoints");
                    PlayerPrefs.SetInt("currentSkillPoints", skillpoint + 1);
                }
                if (waveCount % 4 == 0)
                {
                    spawnTimer -= 0.1f;
                }
                if (waveCount % 2 == 0)
                {
                    GameObject boss = Instantiate(bosses[Random.Range(0, bosses.Length)], player.transform.position + new Vector3(0,0,10f), Quaternion.identity);
                    
                }
                
                if (waveCount % 10 == 0)
                {
                    maxEnemySpawn += difficulty;
                }
                
                enemiesToKill = maxEnemySpawn;
            }
        }

        if (spawnCount >= maxEnemySpawn)
        {
            canSpawn = false;
        }

        if (waveWeight >= 300)
        {
            canSpawn = false;
            waveWeight = 300;
            enemiesToKill = spawnCount;
        }

        if (maxEnemySpawn >= 300)
        {
            maxEnemySpawn = 300;
            enemiesToKill = maxEnemySpawn;
        }
        

        if (spawnTimer <= 0.25f)
        {
            spawnTimer = 0.25f;
        }


        StageTimer();
        if (enemyVariationMax > 8)
        {
            enemyVariationMax = 8;
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
