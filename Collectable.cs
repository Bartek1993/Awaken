using System;
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
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 360, 0) * Time.deltaTime);
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= 6f)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, 20f * Time.deltaTime);
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
