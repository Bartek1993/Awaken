using System;
using DevPlz.CombatText;
using UnityEngine;

public interface ICollectable
{
    public void Collect();
   
}

public class Collectable : MonoBehaviour, ICollectable
{
    float distance;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player =  GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= transform.localScale * 0.1f * Time.deltaTime;
        if (transform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }

        transform.Rotate(new Vector3(0, 360, 0) * Time.deltaTime);
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= 2f)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + Vector3.up, 10f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerStats>(out var playerStats)) return;
        playerStats.CollectReward();
        Debug.Log("REWARDD");
        Destroy(gameObject,0.1f);
        
    }
    

    public void Collect()
    {
        throw new NotImplementedException();
    }
}
