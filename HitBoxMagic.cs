using System;
using UnityEngine;

public class HitBoxMagic : MonoBehaviour
{
    public GameObject iceAge, fireStorm, earthBound;
    public UIControllsButtons uiControlls;
    public PlayerStats playerStats;
    public int getMagicID;
    public float timer = 0f;
    public LayerMask vegetationMask;
    public GameObject fireObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        getMagicID = uiControlls.magicIdentity;
        switch (getMagicID)
        {
            case 0:
                iceAge.SetActive(true);
                break;
            case 1:
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
        
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AbstractEnemy enemy))
        {
            switch (getMagicID)
            {
                case 0:

                    break;
                case 1:
                    Collider [] colliders = Physics.OverlapSphere(transform.position, 5f, vegetationMask);
                    foreach (Collider col in colliders)
                    {
                        GameObject fire = Instantiate(fireObject, col.transform.position, col.transform.rotation);
                        fire.transform.localScale = col.transform.localScale;
                        Destroy(fire, 8f);
                        Destroy(col.gameObject,5);
            
                    }
                    enemy.TakeDamage(playerStats.fireDamage);
                    break;
                case 2:
                    enemy.TakeDamage(playerStats.fireDamage);
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
                    if (timer > 0.15f)
                    {
                        timer = 0;
                        enemy.TakeDamage(playerStats.fireDamage);
                    }

                    
                    break;
                case 2:
                    if (timer > 0.15f)
                    {
                        timer = 0;
                        enemy.TakeDamage(playerStats.fireDamage);
                    }
                    break;
            }
        }
    }
}
