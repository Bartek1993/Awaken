using System;
using a;
using DevPlz.CombatText;
using HUDIndicator;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleEnemy : AbstractEnemy
{
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        offScreen = GetComponent<IndicatorOffScreen>();
        capsule = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        stageManager = FindAnyObjectByType<StageManager>();
        hp = Random.Range(minHp, maxHp) * (stageManager.waveCount + 1);
        stageManager.enemyCount++;
        if (stageManager.waveCount % 1 == 0)
        {
            animatorStartSpeed+= 0.05f;
            damage += 0.1f;
        }
        if (animator.speed > 2.5f)
        {
            animator.speed = 2.5f;
        }
        animator.speed = animatorStartSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

    }

    public override void TakeDamage(float damage)
    {
        CombatText.Spawn(TextStyle.DamageEnemy,"-" +damage, transform.position, null);
        hp -= damage;
        audioSource.PlayOneShot(audioClip[2]);
        GameObject go = Instantiate(bloodPrefabs[Random.Range(0, bloodPrefabs.Length)], transform.position, Quaternion.identity);
        Destroy(go, 1f);
        if (hp <= 0)
        {
            animator.speed = 0;
           // audioSource.PlayOneShot(audioClip[Random.Range(0, 1)]);
            Destroy(gameObject,0.2f);
            float extraGain = player.GetComponent<PlayerStats>().extraAdrenalineGain;
            player.GetComponent<PlayerStats>().adrenaline += 3f + extraGain;
            capsule.enabled = false;
        }
    }
    
    private void OnDestroy()
    {
        
        stageManager.killCount++;
        player.GetComponent<PlayerStats>().comboKillCount += 1;
        stageManager.enemyCount--;
        Vector3 coinPos = new Vector3(0, 1f, 0);
        GameObject go =Instantiate(rewards[Random.Range(0, rewards.Length)], transform.position+ coinPos, Quaternion.identity);
        go.transform.parent = null;
       
    }

    
}
