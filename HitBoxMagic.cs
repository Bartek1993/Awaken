using System;
using UnityEngine;

public class HitBoxMagic : MonoBehaviour
{
    public GameObject iceAge, fireStorm, earthBound, thunderStorm;
    public UIControllsButtons uiControlls;
    public PlayerStats playerStats;
    public int getMagicID;
    public float timer = 0f;
    public LayerMask vegetationMask, enemyMask;
    public GameObject fireObject;
    public float hitboxSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        getMagicID = uiControlls.magicIdentity;
        switch (getMagicID)
        {
            case 0:
                hitboxSize = 2;
                iceAge.SetActive(true);
                break;
            case 1:
                hitboxSize = 2;
                Collider [] colliders = Physics.OverlapSphere(transform.position, 5f, vegetationMask);
                foreach (Collider col in colliders)
                {
                    GameObject fire = Instantiate(fireObject, col.transform.position, col.transform.rotation);
                    fire.transform.localScale = col.transform.localScale;
                    fire.transform.parent = null;
                    Destroy(fire, 10f);
                    Destroy(col.gameObject,3);
                }
                fireStorm.SetActive(true);
                break;
            case 2:
                earthBound.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(hitboxSize, 1.5f, hitboxSize);
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AbstractEnemy enemy))
        {
            switch (getMagicID)
            {
                case 0:
                    StartCoroutine(enemy.isOnFrozen());
                    break;
                case 1:
                   
                    enemy.TakeDamage(playerStats.fireDamage);
                    Collider[] colliders = Physics.OverlapSphere(transform.position, hitboxSize, enemyMask);
                    foreach (Collider col in colliders)
                    {
                        GameObject fire = Instantiate(fireObject, col.transform.position, col.transform.rotation);
                        Destroy(fire, 2f);
                    }

                    break;
                case 2:
                    enemy.TakeDamage(playerStats.earthDamage);
                    break;
            }
        }
        
        

    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.TryGetComponent(out AbstractEnemy enemy))
        {
            
            switch (getMagicID)
            {
                case 0:
                    if (timer > 0.25f)
                    {
                        timer = 0;
                        enemy.TakeDamage(playerStats.iceDamage);
                    }
                    break;
                case 1:
                    if (timer > 0.25f)
                    {
                        timer = 0;
                        enemy.TakeDamage(playerStats.fireDamage);
                    }

                    
                    break;
                case 2:
                    if (timer > 0.15f)
                    {
                        timer = 0;
                        enemy.TakeDamage(playerStats.earthDamage);
                    }
                    break;
            }
        }
    }
}
