using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerOrbScript : MonoBehaviour
{

    public bool isFire, isIce, isThunder;
    public PlayerStats playerStats;
    public float damage, speed;
    public bool isStatic;
    public LayerMask enemyLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        playerStats = FindFirstObjectByType<PlayerStats>();
        if (isFire)
        {
            speed = playerStats.fireOrbSpeed;
            damage = playerStats.GetComponent<PlayerStats>().fireDamage * 0.5f;
        }

        if (isIce)
        {
            speed = playerStats.iceOrbSpeed;
            damage = playerStats.GetComponent<PlayerStats>().iceDamage * 0.5f;
        }

        if (isThunder)
        {
            speed = playerStats.thunderOrbSpeed;
            damage = playerStats.GetComponent<PlayerStats>().thunderDamage * 0.5f;
        }
        
        Collider [] colliders = Physics.OverlapSphere(transform.position, 5, enemyLayer);
        if (colliders.Length == 0)
        {
            gameObject.SetActive(false);
        }

        if (colliders.Length > 0)
        {
            Transform target = colliders[Random.Range(0,colliders.Length)].transform;
            transform.LookAt(target);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStatic) return;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AbstractEnemy enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject,1f);
        }
        
    }
}
