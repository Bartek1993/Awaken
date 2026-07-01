using UnityEngine;

public class EnemySpawner : Spawner
{
    public StageManager stageManager;
    public bool canSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageManager = FindFirstObjectByType<StageManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        spawnRate = stageManager.spawnRate;
        spawnDelay = stageManager.spawnTimer;
        canSpawn = stageManager.canSpawn;
        distance = Vector3.Distance(player.transform.position, transform.position);
        spawntimer += Time.deltaTime;
        if (spawntimer >= spawnDelay && canSpawn)
        {
            spawntimer = 0;
            for (int i = 0; i < spawnRate; i++)
            {
                if (stageManager.spawnCount < stageManager.maxEnemySpawn)
                {
                    stageManager.spawnCount++;
                    spawnOffset = new Vector3(Random.Range(-20f, 25f), 0.15f, Random.Range(-25f, 20f));
                    GameObject go = Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)], player.transform.position + spawnOffset, Quaternion.identity);

                }
            }
                
        }

     
    }
}
