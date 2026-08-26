using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerTrapScript : MonoBehaviour
{
    public bool isFire, isIce, isThunder;
    public PlayerStats playerStats;
    public float damage, speed;
    public bool isStatic;
    public float trapTimer;
    public LayerMask enemyLayer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 8f;
        playerStats = FindFirstObjectByType<PlayerStats>();
        if (isFire)
        {
            damage = playerStats.GetComponent<PlayerStats>().fireDamage * 0.75f;
        }

        if (isIce)
        {
            damage = playerStats.GetComponent<PlayerStats>().iceDamage * 0.75f;
        }

        if (isThunder)
        {
            damage = playerStats.GetComponent<PlayerStats>().thunderDamage * 0.75f;
        }
        Collider [] colliders = Physics.OverlapSphere(transform.position, 5, enemyLayer);
        if (colliders.Length == 0)
        {
            gameObject.SetActive(false);
        }

        if (colliders.Length > 0)
        {
            Transform target = colliders[Random.Range(0, colliders.Length)].transform;
            transform.position = target.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        trapTimer += Time.deltaTime;
    }


    private void OnTriggerStay(Collider other)
    {
       
        
        if (other.TryGetComponent(out AbstractEnemy enemy))
        {
            
            if (isFire)
            {
                if (trapTimer > 0.35f)
                {
                    enemy.TakeDamage(damage);
                    trapTimer = 0;
                }

                
            }

            if (isIce)
            {
                if (trapTimer > 0.35f)
                {
                    enemy.TakeDamage(damage);
                    trapTimer = 0;
                }

                
            }

            if (isThunder)
            {
                if (trapTimer > 0.35f)
                {
                    enemy.TakeDamage(damage);
                    trapTimer = 0;
                }

                
            }
           
        }
    }
}
