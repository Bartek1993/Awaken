using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class hitBoxEnemy : MonoBehaviour
{
    public float speed;
    public float destroyTime;
    public bool isStatic;
    public float damage;
    public StageManager stageManager;
    public GameObject fireOrb, iceOrb;
    public bool isFire, isIce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageManager = FindFirstObjectByType<StageManager>();
        damage += stageManager.waveCount;
        transform.localScale = new Vector3(1.55f, transform.localScale.y, 1.55f);
      
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStatic)
        {
            speed = Random.Range(1.5f, 2f);
            destroyTime = 4f;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            fireOrb.SetActive(isFire);
            iceOrb.SetActive(isIce);
            
        }
        else
        {
            destroyTime = 0.15f;
        }
        
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().TakeDamage(damage);
            Destroy(gameObject,0.001f);
        }
    }
}
