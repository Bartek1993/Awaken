using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpriteManager : MonoBehaviour
{
    public GameObject [] stageSprites;
    public GameObject [] spritePrefabs;
    public GameObject player;
    public Camera playerCamera;
    Vector3 targetPosition;
    public bool isPotatoMode;

    public float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = Camera.main;
        for (int a = 0; a < 20000; a++)
        {
            GameObject sprite = spritePrefabs[Random.Range(0, spritePrefabs.Length)];
            sprite.transform.localScale = new Vector3(Random.Range(0.6f,0.7f), Random.Range(0.7f,0.8f), 1);
            Instantiate(sprite, player.transform.position + new Vector3(Random.Range(-300,200),0.1f,Random.Range(-300,200)), sprite.transform.rotation);
        }
        stageSprites = GameObject.FindGameObjectsWithTag("Sprite");
    }

    public void Update()
    {
        timer += Time.deltaTime;
       
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isPotatoMode)
        {
            if (timer >= 0.15f)
            {
                targetPosition = playerCamera.transform.position;
                targetPosition.y = 0;
                foreach (GameObject sprite in stageSprites)
                {
            
                    sprite.transform.LookAt(new Vector3(targetPosition.x, sprite.transform.position.y, targetPosition.z));
                    sprite.transform.Rotate(0, 180, 0);
            
                }
                timer = 0;
            }
      
        }
        
    }
}
