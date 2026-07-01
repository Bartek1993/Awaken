using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        damage = player.GetComponent<PlayerStats>().weaponDamage;
        hitboxRangeX = player.GetComponent<PlayerStats>().weaponRange;
        hitboxRangeZ = player.GetComponent<PlayerStats>().weaponRange;
        weaponDistance = player.GetComponent<PlayerStats>().weaponDistance;
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
          if (timer >= .5f)
          {
              Destroy(gameObject);
          }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AbstractEnemy enemy))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip[0]);
            }

            enemy.TakeDamage(damage);
            player.GetComponent<PlayerStats>().comboMeterFillAmount += 5f;
            if (player.GetComponent<PlayerStats>().isCritical)
            {
                Debug.Log("critical damage dealt +" + damage);
            }
        }
    }

   
}
