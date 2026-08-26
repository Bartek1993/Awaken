using UnityEngine;

public class EnemySpawner : Spawner
{
    public StageManager stageManager;
    public bool canSpawn;
    public float timerPosition;
    public GameObject [] spawnPoints;
    public int spawnPointIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageManager = FindFirstObjectByType<StageManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        positionCounter = 0;
        enemyVariationMax = stageManager.enemyVariationMax;
        spawnRate = stageManager.spawnRate;
        spawnDelay = stageManager.spawnTimer;
        canSpawn = stageManager.canSpawn;
        distance = Vector3.Distance(player.transform.position, transform.position);
        spawntimer += Time.deltaTime;
        if (spawnPointIndex > 29)
        {
            spawnPointIndex = 0;
        }


        if (spawntimer >= spawnDelay && canSpawn)
        {
            spawnOffset[0] = new Vector3(Random.Range(Random.Range(8,12), 0f), 0, -8);
            spawnOffset[1] = new Vector3(Random.Range(0, -7f), 0, 9);
            spawnOffset[2] = new Vector3(-8, 0, Random.Range(Random.Range(8,11), 0f));
            spawnOffset[3] = new Vector3(9, 0, Random.Range(0f, -9f)); 
            spawntimer = 0;
            for (var i = 0; i < spawnRate; i++)
            {
                spawnPointIndex += 1;
                if (stageManager.enemyCount < stageManager.maxEnemySpawn)
                {
                    
                    //stageManager.enemyCount += 1;
                    GameObject go = Instantiate(SimpleEnemies[Random.Range(0, enemyVariationMax )], spawnPoints[spawnPointIndex].transform.position
                        + spawnOffset[Random.Range(0,3)], Quaternion.identity);

                }
                
                
            }
        }
        
        
        
       

     
    }
}
