using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkillButton : MonoBehaviour
{
    public Button skillButton;
    public Text skillNameText;
    public int buttonID;
    public PlayerStats playerStats;
    public StageManager stageManager;
    

    private void Awake()
    {
        stageManager = FindFirstObjectByType<StageManager>();
    }

    private void OnEnable()
    {
       
        gameObject.SetActive(true);
       
    }

    void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        skillButton = GetComponent<Button>();
        skillNameText = skillButton.gameObject.GetComponentInChildren<Text>();
        
        
        switch (buttonID)
        {
            case 1:
                skillNameText.text = "ATTACK UP";
                break;
            case 2:
                skillNameText.text = "CRIT CHANCE UP";
                break;
            case 3:
                skillNameText.text = "CRIT DAMAGE UP";
                break;
            case 4:
                skillNameText.text = "REGEN 25% OF YOUR MAX HEALTH";
                break;
            case 5:
                skillNameText.text = "REDUCE DAMAGE TAKEN";
                break;
            case 6:
                skillNameText.text = "WEAPON RANGE UP";
                break;
            case 7:
                skillNameText.text = "SLASH DISTANCE UP";
                break;
            case 8:
                skillNameText.text = "GAIN MORE ADRENALINE PER ATTACK";
                break;
            case 9:
                skillNameText.text = "GAIN ADRENALINE PER DAMAGE TAKEN";
                break;
            case 10:
                skillNameText.text = "PLAYER MOVEMENT UP";
                break;
            case 11:
                skillNameText.text = "ORB SHIELD ON DAMAGE UP";
                break;
            case 12:
                skillNameText.text = "REGEN 50% OF YOUR MAX HEALTH";
                break;
        }
        skillButton.onClick.AddListener (() => playerStats.PlayerSkill(buttonID, skillButton));
        skillButton.onClick.AddListener (() => Destroy(gameObject));
        skillButton.onClick.AddListener (() => stageManager.isWaveFinished = false);
        
        
    }

    private void LateUpdate()
    {
       
    }

    private void FixedUpdate()
    {
        
    }

   

    
}
