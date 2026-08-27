using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
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
    public GameObject[] hitFX;
    public float iceValue, fireValue, thunderValue, earthValue, lifestealchanceValue;
    public bool isCameraShake;
    public CinemachineBasicMultiChannelPerlin [] basicMultiChannelPerlin;
    public bool isStatic;
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
        
        isCameraShake = true;
        player = GameObject.FindGameObjectWithTag("Player");
        damage = player.GetComponent<PlayerStats>().baseDamage;
        hitboxRangeX = player.GetComponent<PlayerStats>().projectileRange * 0.1f;
        hitboxRangeZ = player.GetComponent<PlayerStats>().projectileRange * 0.1f;
        
        isStatic = player.GetComponent<PlayerStats>().isStaticHitBox;
        if (isStatic)
        {
            weaponDistance = 0.15f;
        }
        else
        {
            weaponDistance = player.GetComponent<PlayerStats>().projectileSpeed;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var VARIABLE in basicMultiChannelPerlin)
        {
            VARIABLE.FrequencyGain = 1f;
        }
        fireValue = Random.value;
        iceValue = Random.value;
        lifestealchanceValue = Random.value;
        if (iceValue < player.GetComponent<PlayerStats>().iceChance)
        {
            hitFX[0].SetActive(true);
        }
        if (fireValue < player.GetComponent<PlayerStats>().fireChance)
        {
            hitFX[1].SetActive(true);
        }
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

            if (isCameraShake)
            {
                StartCoroutine("ShakeCamera");
            }

            if (enemy.enemyType == AbstractEnemy.EnemyType.Tank)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject, player.GetComponent<PlayerStats>().piercePower);
            }

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip[0]);
            }

            if (Random.value < player.GetComponent<PlayerStats>().maxCritChance)
            {
                damage += player.GetComponent<PlayerStats>().critDamageMultiplier;
            }

            if (fireValue < player.GetComponent<PlayerStats>().fireChance)
            {
                enemy.StartCoroutine(enemy.isOnFire());
               
            }
            if (iceValue < player.GetComponent<PlayerStats>().iceChance)
            {
                enemy.StartCoroutine(enemy.isOnFrozen());
                hitFX[0].SetActive(true);
            }

            enemy.TakeDamage(damage + player.GetComponent<PlayerStats>().bravery * 0.05f);
            player.GetComponent<PlayerStats>().bravery += 0.5f;
        }
    }

    private IEnumerator ShakeCamera()
    {
        foreach (var VARIABLE in basicMultiChannelPerlin)
        {
            VARIABLE.FrequencyGain = 45f;
            VARIABLE.AmplitudeGain = 1.6f;
                ;
        }
        yield return new WaitForSecondsRealtime(0.045f);
        foreach (var VARIABLE in basicMultiChannelPerlin)
        {
            VARIABLE.AmplitudeGain = 1f;
            VARIABLE.FrequencyGain = 1f;
        }
    }

    private void OnDestroy()
    {
        foreach (var VARIABLE in basicMultiChannelPerlin)
        {
            VARIABLE.AmplitudeGain = 1f;
            VARIABLE.FrequencyGain = 1f;
        }
        StopAllCoroutines();
    }
}
