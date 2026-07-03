using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public GameObject powerUpPanel;
    public GameObject stageGround;
    public GameObject player;
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
        enemyVariationMax = 2;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 1080, true);
        player = GameObject.FindGameObjectWithTag("Player");
        spawnTimer = 1f;
        spawnRate = 1;
        cooldownTimer = 0.25f;
        canSpawn = true;
        waveCount = 1;
        maxEnemySpawn = 20;
        enemiesToKill = maxEnemySpawn;
        killCount = 0;
        Screen.SetResolution(1920, 1080, true);
        isWaveFinished = false;
        
    }

    // Update is called once per frame
    void Update()
    {
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
                if (waveCount % 2 == 0)
                {
                    maxEnemySpawn += 2;
                }
                
                
                if (waveCount % 10 == 0)
                {
                    enemyVariationMax += 1;
                    spawnTimer += 0.015f;
                    
                }
                
                
                
                
                enemiesToKill = maxEnemySpawn;
            }
        }

        if (spawnCount >= maxEnemySpawn)
        {
            canSpawn = false;
        }

        if (waveWeight >= 100)
        {
            canSpawn = false;
            waveWeight = 100;
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


        StageTimer();
        if (enemyVariationMax > 5)
        {
            enemyVariationMax = 5;
        }
    }

   

    private void StageTimer()
    {
        
        
        sec += Time.deltaTime;
        if (sec > 59.9f)
        {
            min += 1;
            sec = 0;
        }
        if (min > 59.0)
        {
            hour += 1;
            min = 0;
        }
    }

    
}
