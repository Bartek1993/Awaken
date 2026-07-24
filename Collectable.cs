using System;
using DevPlz.CombatText;
using UnityEngine;

public interface ICollectable
{
    public void Collect();
   
}

public class Collectable : MonoBehaviour, ICollectable
{
    public int collectableID;
    float distance;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player =  GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, 15f);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(new Vector3(0, 360, 0) * Time.deltaTime);
        distance = Vector3.Distance(player.transform.position, transform.position);
        switch (collectableID)
        {
            case 1:
                if (distance <= 5f)
                {
                    transform.position = Vector3.Lerp(transform.position, player.transform.position + Vector3.up, 12f * Time.deltaTime);
                }

                break;
            case 2:
                if (distance <= 3f)
                {
                    transform.position = Vector3.Lerp(transform.position, player.transform.position + Vector3.up, 10f * Time.deltaTime);
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerStats>(out var playerStats)) return;
        switch (collectableID)
        {
            case 1:
                playerStats.CollectReward();
                break;
            case 2:
                playerStats.exp += 10;
                break;
        }
        
        Debug.Log("REWARDD");
        Destroy(gameObject,0.01f);
        
    }
    

    public void Collect()
    {
        throw new NotImplementedException();
    }
}
