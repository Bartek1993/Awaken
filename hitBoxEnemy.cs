using System;
using UnityEngine;

public class hitBoxEnemy : MonoBehaviour
{
    public float damage;
    public StageManager stageManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageManager = FindFirstObjectByType<StageManager>();
        damage = GetComponentInParent<AbstractEnemy>().damage;
        damage += stageManager.waveCount;
        transform.localScale = new Vector3(1.55f, transform.localScale.y, 1.55f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
