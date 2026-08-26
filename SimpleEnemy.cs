using System;
using System.Collections;
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
        //waveDifficulty = stageManager.difficulty;
        GameObject go = Instantiate(onEntryObject,transform.position,transform.rotation);
        go.transform.parent = null;
        Destroy(go,1);
        offScreen = GetComponent<IndicatorOffScreen>();
        capsule = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        stageManager = FindAnyObjectByType<StageManager>();
        hp = Random.Range(minHp, maxHp) + stageManager.difficulty;
        hp += stageManager.hpAddition;
        stageManager.enemyCount+=1;
       
        if (animator.speed > 3f)
        {
            animator.speed = 3f;
        }

        animator.speed = animatorStartSpeed + stageManager.speedIncrease;
        damage = Random.Range(minDamage + stageManager.damageIncrease, maxDamage + stageManager.damageIncrease) * stageManager.difficulty;
       


    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        

    }

    public override void TakeDamage(float damage)
    {
        float randval = Random.Range(0f, 1f);
        CombatText.Spawn(TextStyle.DamageEnemy,"-" +damage.ToString("F1"), transform.position, null);
        hp -= damage;
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClip[2]);
        }
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(-transform.forward * player.GetComponent<PlayerStats>().knockBasckStrength, ForceMode.Impulse);
        GameObject go = Instantiate(bloodPrefabs[Random.Range(0, bloodPrefabs.Length)], transform.position+ Vector3.up, Quaternion.identity);
        if (enemyType != EnemyType.Tank)
        {
            StartCoroutine(SlowDown(rb));
        }
        
        Destroy(go, 1f);
        if (hp <= 0)
        {
            capsule.enabled = false;
            animator.speed = 0;
           // audioSource.PlayOneShot(audioClip[Random.Range(0, 1)]);
            Destroy(gameObject,0.01f);
           
        }
        
    }

    public override IEnumerator isOnFire()
    {
        CombatText.Spawn(TextStyle.DamagePlayer,"ON FIRE", transform.position,null);
        TakeDamage(player.GetComponent<PlayerStats>().fireDamage * Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
        yield return null;

    }

    public override IEnumerator isOnFrozen()
    {
        CombatText.Spawn(TextStyle.DamagePlayer,"FROZEN", transform.position,transform);
        animator.speed = 0.25f;
        yield return new WaitForSeconds(player.GetComponent<PlayerStats>().frozenTimer);
        animator.speed = animatorStartSpeed;
        yield return null;
    }

    private IEnumerator SlowDown(Rigidbody rb)
    {
        animator.speed = 0f;
        rb.AddForce(-transform.forward * player.GetComponent<PlayerStats>().knockBasckStrength, ForceMode.Impulse);
        yield return new WaitForSeconds(player.GetComponent<PlayerStats>().enemyStunTime);
        animator.speed = animatorStartSpeed;
        
    }

    private void OnDestroy()
    {
        GameObject exitObject = Instantiate(onEntryObject,transform.position,transform.rotation);
        exitObject.transform.parent = null;
        Destroy(exitObject,0.5f);
        stageManager.killCount++;
        player.GetComponent<PlayerStats>().comboKillCount += 1;
        player.GetComponent<PlayerStats>().totalEnemiesKilled += 1;
        stageManager.spawnCount--;
        Vector3 coinPos = new Vector3(0, 1f, 0);
        if (enemyType == EnemyType.Explosive)
        {
            GameObject exp =Instantiate(rewards[1], transform.position+ coinPos, Quaternion.identity);
            GameObject go2 = Instantiate(rewards[2], transform.position, transform.rotation);
            go2.transform.parent = null;
            exp.transform.parent = null;
            Destroy(go2,.5f);
        }
        else
        {
            GameObject exp =Instantiate(rewards[1], transform.position+ coinPos, Quaternion.identity);
            GameObject gold =Instantiate(rewards[0], transform.position+ coinPos, Quaternion.identity);
            exp.transform.parent = null;
            gold.transform.parent = null;

        }
        
        float lifestealChance = player.GetComponent<PlayerStats>().lifeStealChance;
        float stealChance = Random.value;
        if (stealChance < lifestealChance)
        {
            player.GetComponent<PlayerStats>().hp += player.GetComponent<PlayerStats>().lifestealamount;
        }

    }

    
}
