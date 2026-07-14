using System;
using UnityEngine;

public class ElementalDamage : MonoBehaviour
{
    public float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
       
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out AbstractEnemy enemy))
        {
            if (timer > 0.75f && enemy.enemyType != AbstractEnemy.EnemyType.Tank)
            {
                enemy.TakeDamage(0.1f);
                timer = 0;
            }
        }
    }
}
