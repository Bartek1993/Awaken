using System;
using UnityEngine;

public class hitboxShield : MonoBehaviour
{
    public float timer;
    public PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GetComponentInParent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2.6f)
        {
            timer = 0;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out AbstractEnemy enemy))
        {
            if (timer > 2.5f)
            {
                enemy.TakeDamage(playerStats.fireDamage);
            }
            
        }
    }
}
