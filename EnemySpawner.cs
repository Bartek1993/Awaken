using UnityEngine;

public class EnemySpawner : Spawner
{
    public StageManager stageManager;
    public bool canSpawn;
    public float timerPosition;
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
       


        if (spawntimer >= spawnDelay && canSpawn)
        {
            spawnOffset[0] = new Vector3(Random.Range(Random.Range(10,15), 0f), 0, -10);
            spawnOffset[1] = new Vector3(Random.Range(0, -10f), 0, 10);
            spawnOffset[2] = new Vector3(-10, 0, Random.Range(Random.Range(5,10), 0f));
            spawnOffset[3] = new Vector3(6, 0, Random.Range(0f, -10f)); 
            spawntimer = 0;
            for (var i = 0; i < spawnRate; i++)
            {
                
                if (stageManager.spawnCount < stageManager.maxEnemySpawn)
                {
                    
                    stageManager.spawnCount += 1;
                    GameObject go = Instantiate(SimpleEnemies[Random.Range(0, enemyVariationMax )], player.transform.position + spawnOffset[Random.Range(0,3)], Quaternion.identity);

                }
                
                
            }
        }
        
        
        
       

     
    }
}
