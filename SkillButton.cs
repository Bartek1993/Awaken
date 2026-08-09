using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkillButton : MonoBehaviour
{
    public ScripablePowerUP powerUp;
    public Text skillNameText;
    public int buttonID;
    public PowerUpInstantiator powerUpInstantiator;
    public Button btn;
    private void Start()
    {
        powerUpInstantiator = FindFirstObjectByType<PowerUpInstantiator>();
        btn = GetComponent<Button>();
       
    }

    private void Update()
    {
        btn.GetComponent<Button>().onClick.AddListener(() => SetPowerUpDescription(powerUpInstantiator));
    }

    public void SetPowerUpDescription(PowerUpInstantiator powerUpI)
    {
        powerUpI.selectedID = buttonID;
        powerUpI.powerupDescription.text = skillNameText.text;
        powerUpI.applypowerUpButton.SetActive(true);
        
    }



}
