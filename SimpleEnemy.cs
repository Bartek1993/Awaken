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
        hp = Random.Range(minHp* stageManager.difficulty, maxHp* stageManager.difficulty) ;
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
        damage *= stageManager.difficulty;

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

    }

    public override void TakeDamage(float damage)
    {
        CombatText.Spawn(TextStyle.DamageEnemy,"-" +damage.ToString("F1"), transform.position, null);
        hp -= damage;
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClip[2]);
        }

        GameObject go = Instantiate(bloodPrefabs[Random.Range(0, bloodPrefabs.Length)], transform.position+ Vector3.up, Quaternion.identity);
        if (enemyType != EnemyType.Tank)
        {
            StartCoroutine("SlowDown");
        }
        
        Destroy(go, 1f);
        if (hp <= 0)
        {
            capsule.enabled = false;
            animator.speed = 0;
           // audioSource.PlayOneShot(audioClip[Random.Range(0, 1)]);
            Destroy(gameObject,0.1f);
           
        }
    }

    public override IEnumerator isOnFire()
    {
        CombatText.Spawn(TextStyle.DamagePlayer,"ON FIRE", transform.position,null);
        TakeDamage(0.05f * Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
        yield return null;

    }

    public override IEnumerator isOnFrozen()
    {
        CombatText.Spawn(TextStyle.DamagePlayer,"FROZEN", transform.position,null);
        animator.speed = 0;
        yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        animator.speed = animatorStartSpeed;
        yield return null;
    }

    private IEnumerator SlowDown()
    {
        animator.speed = 0;
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
        stageManager.enemyCount--;
        Vector3 coinPos = new Vector3(0, 1f, 0);
        GameObject go =Instantiate(rewards[Random.Range(0, rewards.Length)], transform.position+ coinPos, Quaternion.identity);
        go.transform.parent = null;
       
    }

    
}
