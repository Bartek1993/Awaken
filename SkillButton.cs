using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkillButton : MonoBehaviour
{
    public ScripablePowerUP [] powerUps;
    public Button skillButton;
    public Text skillNameText;
    public int buttonID;
    public PlayerStats playerStats;
    public StageManager stageManager;
    

    void Start()
    {
        
        //skillButton.onClick.AddListener (() => powerUps[buttonID].OnClickButton());
        
    }
   

    
}
