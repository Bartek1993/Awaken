using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class hitBox : MonoBehaviour
{
    public float damage;
    public float hitboxRangeX,hitboxRangeY,hitboxRangeZ;
    public float weaponDistance;
    public Vector3 hitboxPosition;
    public float timer;
    public GameObject player;
    public GameObject [] slash;
    public GameObject slashRotObject;
    public AudioSource audioSource;
    public AudioClip [] audioClip;

    private void OnDisable()
    {
        timer = 0;
        gameObject.transform.parent = player.transform;
        gameObject.transform.localPosition = hitboxPosition;
        gameObject.transform.localRotation =  new Quaternion(0,0,0,0);
        foreach (var s in slash)
        {
            s.SetActive(false);
        }


    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        damage = player.GetComponent<PlayerStats>().baseDamage;
        hitboxRangeX = player.GetComponent<PlayerStats>().projectileRange;
        hitboxRangeZ = 1.8f;
        weaponDistance = player.GetComponent<PlayerStats>().projectileSpeed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        

          transform.localScale = new Vector3(hitboxRangeX,hitboxRangeY,hitboxRangeZ);
          transform.Translate(new Vector3(0,0,1) * weaponDistance * Time.deltaTime);
          timer += Time.deltaTime;
          if (timer >= 1f)
          {
              Destroy(gameObject);
          }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AbstractEnemy enemy))
        {
            if (enemy.enemyType == AbstractEnemy.EnemyType.Tank)
            {
                Destroy(gameObject);
            }

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip[0]);
            }

            if (Random.value < player.GetComponent<PlayerStats>().maxCritChance)
            {
                damage += player.GetComponent<PlayerStats>().critDamageMultiplier;
            }

            enemy.TakeDamage(damage);
            if (enemy.GetComponent<Rigidbody>())
            {
                Rigidbody rb = enemy.GetComponent<Rigidbody>();
                rb.AddForce(Vector3.back * 10 * Time.fixedDeltaTime, ForceMode.Impulse);
                Debug.Log("enemy has rigidbody");
            }

            if (player.GetComponent<PlayerStats>().isCritical)
            {
                Debug.Log("critical damage dealt +" + damage);
            }
        }
    }

   
}
