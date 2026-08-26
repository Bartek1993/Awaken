using System;
using UnityEngine;

public class MasterSkelleton : MonoBehaviour
{
    
    public PlayerStats playerStats;
    public UIControllsButtons uiControllsButtons;
    public GameObject skelletorMenu;
    public StageProperties stageProperties;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        uiControllsButtons = FindFirstObjectByType<UIControllsButtons>();
        stageProperties = FindFirstObjectByType<StageProperties>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && uiControllsButtons.interact)
        {
            stageProperties.dialogueUI.gameObject.SetActive(true);
            stageProperties.isInDialogue = true;
            //stageProperties.isPaused =  true;
            uiControllsButtons.interact = false;
        }
    }


    public void OnCloseMenu(GameObject menu)
    {
        menu.SetActive(false);
        stageProperties.isPaused = false;
    }
}
