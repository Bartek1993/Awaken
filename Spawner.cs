using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    public GameObject player;
    public int maxspawnObjects;
    public GameObject[] objectsToSpawn;
    public int spawnRate;
    public float spawntimer;
    public float distance;
    public float spawnDelay;
    public Vector3 spawnOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
