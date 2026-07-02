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
    PowerUpInstantiator powerUpInstantiator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        powerUpInstantiator = FindFirstObjectByType<PowerUpInstantiator>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 1080, true);
        player = GameObject.FindGameObjectWithTag("Player");
        spawnTimer = 1.5f;
        spawnRate = 2;
        cooldownTimer = 0.25f;
        canSpawn = true;
        waveCount = 1;
        maxEnemySpawn = 15;
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
        if (spawnTimer > 3)
        {
            spawnTimer = 3;
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
                if (waveCount % 5 == 0)
                {
                    
                    spawnRate += 1;
                    spawnTimer += 0.25f;
                    maxEnemySpawn += 1;
                }
                enemiesToKill = maxEnemySpawn;
            }
        }

        if (spawnCount >= maxEnemySpawn)
        {
            canSpawn = false;
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
