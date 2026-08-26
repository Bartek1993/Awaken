using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Glyph : MonoBehaviour
{
    public LayerMask playerLayer, enemyLayer;
    public bool playerEntered = false;
    public float timer;
    public GameObject fireShield, iceShield, thunderShield;
    public bool isFire, isIce, isThunder;
    public bool isFireProjectile, isIceProjectile, isThunderProjectile;
    public bool isFireTrap, isIceTrap, isThunderTrap;
    public bool isFireOrb, isIceOrb, isThunderOrb;
    public float radiusForPlayer = 2f;
    public float radiusForEnemy = 1f;
    public float glyphHp;
    public PlayerActiveBoosts playerActiveBoosts;
    public StageManager stageManager;
    public PlayerStats playerStats;
    public int RandomGlyphPowerUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageManager = FindFirstObjectByType<StageManager>();
        playerActiveBoosts = FindFirstObjectByType<PlayerActiveBoosts>();
        playerStats = FindFirstObjectByType<PlayerStats>();
        stageManager.glyphCount += 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (glyphHp <= 0)
        {
            Destroy(gameObject);
        }
        RandomGlyphPowerUp = Random.Range(0, 2);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusForPlayer,playerLayer);
        if (colliders.Length > 0)
        {
            playerEntered = true;
        }
        else
        {
            playerEntered = false;
        }
        
        if (playerEntered)
        {
            Collider [] colEnemy = Physics.OverlapSphere(transform.position, radiusForPlayer,enemyLayer);
            if (colEnemy.Length > 0)
            {
                glyphHp -= 1f * Time.deltaTime;
            }

            radiusForPlayer += 0.5f * Time.deltaTime;
            if (isFire)
            {
                fireShield.SetActive(true);
                fireShield.transform.localScale -=  new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime;
            }
            if (isIce)
            {
                iceShield.SetActive(true);
                iceShield.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime;
            }

            if (isThunder)
            {
                thunderShield.SetActive(true);
                thunderShield.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime;
            }
            timer += Time.deltaTime;
            if (timer >= 10f)
            {
                if (isFire)
                {
                    if (isFireProjectile)
                    {
                        playerActiveBoosts.fireProjectile = true;
                        switch (RandomGlyphPowerUp)
                        {
                            case 0:
                                playerStats.fireOrbSpeed += 2;
                                Debug.Log("FIRE PROJ SPEED INCREASED");
                                break;
                            case 1:
                                playerActiveBoosts.fireProjectileSpawnRate -= 0.5f;
                                Debug.Log("FIRE PROJ SPAWN SPEED INCREASED");
                                break;
                            case 2:
                                playerStats.fireDamage += 1f;
                                playerStats.SetPlayerStats();
                                Debug.Log("FIRE PROJ DAMAGE INCREASED");
                                break;
                        }
                    }
                    
                    if (isFireTrap)
                    {
                        playerActiveBoosts.fireTrap = true;
                        switch (RandomGlyphPowerUp)
                        {
                            case 0:
                                playerActiveBoosts.fireTrapSize += 0.15f;
                                Debug.Log("FIRE TRAP DISTANCE INCREASED");
                                break;
                            case 1:
                                playerActiveBoosts.fireTrapSpawnTime -= 0.5f;
                                Debug.Log("FIRE PROJ SPAWN SPEED INCREASED");
                                break;
                            case 2:
                                playerStats.fireDamage += 1f;
                                playerStats.SetPlayerStats();
                                Debug.Log("FIRE TRAP DAMAGE INCREASED");
                                break;
                        }
                    }
                    if (isFireOrb)
                    {
                        playerActiveBoosts.fireOrb = true;
                        switch (RandomGlyphPowerUp)
                        {
                            case 0:
                                playerActiveBoosts.fireOrbAmount += 1;
                                Debug.Log("FIRE ORB AMOUNT INCREASED");
                                break;
                            case 1:
                                playerActiveBoosts.fireOrbRotationSpeed += 5f;
                                Debug.Log("FIRE ORB ROT SPEED INCREASED");
                                break;
                            case 2:
                                playerStats.fireDamage += 1f;
                                playerStats.SetPlayerStats();
                                Debug.Log("FIRE TRAP DAMAGE INCREASED");
                                break;
                        }
                    }
                }

                if (isIce)
                {
                    if (isIceProjectile)
                    {
                        playerActiveBoosts.iceProjectile = true;
                        switch (RandomGlyphPowerUp)
                        {
                            case 0:
                                playerStats.iceOrbSpeed += 1;
                                Debug.Log("ICE PROJ SPEED INCREASED");
                                break;
                            case 1:
                                playerActiveBoosts.iceProjectileSpawnRate -= 0.5f;
                                Debug.Log("ICE PROJ SPAWN SPEED INCREASED");
                                break;
                            case 2:
                                playerStats.iceDamage += 1f;
                                playerStats.SetPlayerStats();
                                Debug.Log("ICE PROJ DAMAGE INCREASED");
                                break;
                        }
                    }
                    
                    if (isIceTrap)
                    {
                        playerActiveBoosts.iceTrap = true;
                        switch (RandomGlyphPowerUp)
                        {
                            case 0:
                                playerActiveBoosts.iceTrapSize += 0.1f;
                                Debug.Log("ICE TRAP SIZE INCREASED");
                                break;
                            case 1:
                                playerActiveBoosts.iceTrapSpawnTime -= 0.5f;
                                Debug.Log("FIRE PROJ SPAWN SPEED INCREASED");
                                break;
                            case 2:
                                playerStats.iceDamage += 1f;
                                playerStats.SetPlayerStats();
                                Debug.Log("ICE PROJ DAMAGE INCREASED");
                                break;
                        }
                    }
                    
                    if (isIceOrb)
                    {
                        playerActiveBoosts.iceOrb = true;
                        switch (RandomGlyphPowerUp)
                        {
                            case 0:
                                playerActiveBoosts.iceOrbAmount += 1;
                                Debug.Log("ICE ORB AMOUNT INCREASED");
                                break;
                            case 1:
                                playerActiveBoosts.iceOrbRotationSpeed += 5f;
                                Debug.Log("ICE ORB ROT SPEED INCREASED");
                                break;
                            case 2:
                                playerStats.iceDamage += 1f;
                                playerStats.SetPlayerStats();
                                Debug.Log("ICE ORB DAMAGE INCREASED");
                                break;
                        }
                    }
                }

                if (isThunder)
                {
                    if (isThunderProjectile)
                    {
                        playerActiveBoosts.thunderProjectile = true;
                        switch (RandomGlyphPowerUp)
                        {
                            case 0:
                                playerStats.thunderOrbSpeed += 1;
                                Debug.Log("THUNDER PROJ SPEED INCREASED");
                                break;
                            case 1:
                                playerActiveBoosts.thunderProjectileSpawnRate -= 0.5f;
                                Debug.Log("THUNDER PROJ SPAWN SPEED INCREASED");
                                break;
                            case 2:
                                playerStats.thunderDamage += 1f;
                                playerStats.SetPlayerStats();
                                Debug.Log("THUNDER PROJ DAMAGE INCREASED");
                                break;
                        }
                    }
                    
                    if (isThunderTrap)
                    {
                        playerActiveBoosts.thunderTrap = true;
                        switch (RandomGlyphPowerUp)
                        {
                            case 0:
                                playerActiveBoosts.thunderTrapSize += 0.1f;
                                Debug.Log("THUNDER TRAP SIZE INCREASED");
                                break;
                            case 1:
                                playerActiveBoosts.thunderTrapSpawnTime -= 0.5f;
                                Debug.Log("THUNDER PROJ SPAWN SPEED INCREASED");
                                break;
                            case 2:
                                playerStats.thunderDamage += 1f;
                                playerStats.SetPlayerStats();
                                Debug.Log("ICE PROJ DAMAGE INCREASED");
                                break;
                        }
                    }
                    
                    if (isThunderOrb)
                    {
                        playerActiveBoosts.thunderOrb = true;
                        switch (RandomGlyphPowerUp)
                        {
                            case 0:
                                playerActiveBoosts.thunderOrbAmount += 1;
                                Debug.Log("THUNDER ORB AMOUNT INCREASED");
                                break;
                            case 1:
                                playerActiveBoosts.thunderOrbRotationSpeed += 5f;
                                Debug.Log("THUNDER ORB ROT SPEED INCREASED");
                                break;
                            case 2:
                                playerStats.thunderDamage += 1f;
                                playerStats.SetPlayerStats();
                                Debug.Log("ICE ORB DAMAGE INCREASED");
                                break;
                        }
                    }
                }
                Destroy(gameObject);
               
            }
        }
        else
        {
            radiusForPlayer = 2f;
            fireShield.SetActive(false);
            iceShield.SetActive(false);
            thunderShield.SetActive(false);
            timer = 0;
            fireShield.transform.localScale = new Vector3(2f, 2f, 2f) * Time.deltaTime;
            iceShield.transform.localScale = new Vector3(2f, 2f, 2f) * Time.deltaTime;
            thunderShield.transform.localScale = new Vector3(2f, 2f, 2f) * Time.deltaTime;
        }




    }

    private void OnDestroy()
    {
        stageManager.glyphCount -= 1;
    }
}
