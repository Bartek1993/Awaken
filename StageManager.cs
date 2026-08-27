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
    public AudioSource fxMusic;
    public AudioClip[] songs;
    public AudioClip[] fx;
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
    public GameObject [] stages;
    public GameObject[] bosses;
    public StageProperties stageProperties;
    public float hpAddition;
    public float speedIncrease;
    public float damageIncrease;
    public GameObject[] portals;
    public GameObject[] glyphs;
    public int glyphCount;
    public int maxEnemyOnScreen;
    public GameObject [] additionalEnemies;
    public GameObject StatueOfProgression;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
    }

    void Start()
    {
        StatueOfProgression.SetActive(false);
        maxEnemyOnScreen = 5;
        hpAddition = 0;
        speedIncrease = 0.02f;
        stageProperties = FindFirstObjectByType<StageProperties>();
        waveStartnumber = PlayerPrefs.GetInt("startWave");
        difficulty = 10;
        enemyVariationMax = 3;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 1080, true);
        player = GameObject.FindGameObjectWithTag("Player");
        spawnTimer = 0.5f;
        spawnRate = 1;
        cooldownTimer = 0.55f;
        canSpawn = true;
        waveCount = waveStartnumber;
        maxEnemySpawn = 10;
        enemiesToKill = maxEnemySpawn;
        killCount = 0;
        isWaveFinished = false;
        if (difficulty > 10)
        {
            stages[0].SetActive(true);
            stages[1].SetActive(false);
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
                cooldownTimer -= 0.1f;
                waveCount += 1;
                killCount = 0;
                spawnCount = 0;
                waveWeight = 0;
                enemyCount = 0;
                if (waveCount % 1 == 0)
                {
                    StatueOfProgression.SetActive(false);
                    spawnTimer -= 0.015f;
                    speedIncrease += 0.01f;
                    maxEnemySpawn += 15;
                    int skillpoint = PlayerPrefs.GetInt("currentSkillPoints");
                    PlayerPrefs.SetInt("currentSkillPoints", skillpoint + 1);
                    
                   
                    

                }
                if (waveCount % 2 == 0)
                {
                    difficulty += 2;
                    canSpawn = false;
                    damageIncrease += 0.0025f;
                    hpAddition += 10;
                    StatueOfProgression.SetActive(true);
                    fxMusic.PlayOneShot(fx[0]);

                }
                if (waveCount % 3 == 0)
                {
                    enemyVariationMax += 1;
                    if (!StatueOfProgression.activeInHierarchy)
                    {
                        for (int a = 0; a < waveCount; a++)
                        {
                            GameObject extraEnemy = Instantiate(additionalEnemies[Random.Range(0, additionalEnemies.Length)], player.transform.position + new Vector3(Random.Range(-10,10),0, Random.Range(-10,10)), Quaternion.identity);
                        }
                    }

                    

                }
                
                
                if (waveCount % 5 == 0)
                {
                    //canSpawn = false;
                    //GameObject port = Instantiate(portals[Random.Range(0, portals.Length)], player.transform.position+ new Vector3(0,1,2), Quaternion.identity);
                    //GameObject boss = Instantiate(bosses[Random.Range(0, bosses.Length)], player.transform.position + new Vector3(0,0,10f), Quaternion.identity);
                    maxEnemySpawn += difficulty;
                    hpAddition += 25;
                }
                enemiesToKill = maxEnemySpawn;
            }
        }
        

        if (waveWeight >= 300)
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
        

        if (spawnTimer <= 0.01f)
        {
            spawnTimer = 0.01f;
        }


        StageTimer();
        if (enemyVariationMax > 4)
        {
            enemyVariationMax = 4;
        }

        if (cooldownTimer <= 0.05f)
        {
            cooldownTimer = 0.05f;
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
