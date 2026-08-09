using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Chest : MonoBehaviour
{
    public bool canOpen = true;
    public Animator animator;
    public int cost;
    public int randomItemInit;
    public int chestLevel;
    public ChestItemSO [] chestItems;
    public StageProperties stageProperties;
    public StageManager stageManager;
    public float distance;
    public GameObject player;
    public UIControllsButtons uiControllsButtons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageProperties = FindFirstObjectByType<StageProperties>();
        uiControllsButtons = FindFirstObjectByType<UIControllsButtons>();
        stageManager = FindFirstObjectByType<StageManager>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        cost = Random.Range(50, 60) * stageManager.waveCount;
        randomItemInit = Random.Range(0, chestItems.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
        distance = Vector3.Distance(player.transform.position, transform.position);
    }

    

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerStats>().currentCoins >= cost && canOpen && distance <= 5 && uiControllsButtons.interact)
            {
                
                animator.SetBool("isOpen", true);
                StartCoroutine(stageProperties.OpenChest("Obtained \n" + chestItems[randomItemInit].itemName,  chestItems[randomItemInit].itemDescription,
                    gameObject, chestItems[randomItemInit], cost ));
               
                
            }
        }
    }
}
