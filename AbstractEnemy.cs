using System;
using System.Collections;
using HUDIndicator;
using UnityEngine;

public interface ICommonMethods
{
    public void TakeDamage(float damage);
}

public abstract class AbstractEnemy : MonoBehaviour,  ICommonMethods
{
    public enum EnemyType
    {
        Normal, Tank, Magic, Elemental, Explosive 
    }

    public GameObject onEntryObject;
    public EnemyType enemyType;
    public float animatorStartSpeed;
    public AudioSource audioSource;
    public AudioClip [] audioClip;
    public float damage;
    public GameObject hitBox;
    public GameObject player;
    public float distance;
    public Animator animator;
    public float hp;
    public int level;
    public GameObject parentSpawner;
    public StageManager stageManager;
    public GameObject [] bloodPrefabs;
    public float minHp, maxHp;
    public CapsuleCollider capsule;
    public IndicatorOffScreen offScreen;
    public float multiplier;
    public GameObject[] rewards;
    //public int waveDifficulty;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
       
        
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        transform.LookAt(new  Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if (distance > 20)
        {
           offScreen.visible =  true;
        }
        else
        {
            offScreen.visible = false;
        }

        animator.SetFloat("distance", distance);
        animator.SetFloat("hp",hp);
        
        
    }


    public abstract void TakeDamage(float damage);
    public abstract IEnumerator isOnFire();
    public abstract IEnumerator isOnFrozen();
    public void GetHitBox()
    {
        GameObject go = Instantiate(hitBox, transform.position, Quaternion.identity);
        go.transform.parent = transform;
        Destroy(go, 0.2f);
    }

}
