using System;
using FischlWorks_FogWar;
using UnityEngine;

public class AdvancePortal : MonoBehaviour
{

    float timer = 0;
    public int portalID;
    public StageProperties stageProp;
    public StageManager stageManager;
    public UIControllsButtons uiControls;
    public csFogWar fogWar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageProp = FindFirstObjectByType<StageProperties>();
        stageManager =FindFirstObjectByType<StageManager>();
        uiControls = FindFirstObjectByType<UIControllsButtons>();
        fogWar = FindFirstObjectByType<csFogWar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            if(uiControls.interact)
            {
                Destroy(gameObject);
                uiControls.interact = false;
                //stageProp.isPaused = true;
               // stageProp.isLevelingUp = true;
                stageManager.canSpawn = true;
                fogWar.keepRevealedTiles = false;

            }
            fogWar.keepRevealedTiles = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fogWar.keepRevealedTiles = true;
        }
    }

    private void OnDestroy()
    {
        fogWar.keepRevealedTiles = true;
    }
}
