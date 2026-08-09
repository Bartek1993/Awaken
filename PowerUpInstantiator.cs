using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = System.Random;

public class PowerUpInstantiator : MonoBehaviour
{
    public GameObject applypowerUpButton, closeWindowButton;
    public ScripablePowerUP [] powerUps;
    public Transform powerUpButtonWindow, ShopWindow;
    public GameObject buttonPrefab;
    private GameObject go;
    [SerializeField]private List<GameObject> buttons;
    public List<int> possibleID;
    public int selectedID;
    public bool closePowerUp;
    public StageManager stageManager;
    public PlayerStats playerStats;
    public StageProperties stageProperties;
    public Text powerupDescription;
    public int currentId;
    public string powerUpDetails;
    private void OnEnable()
    {
        powerupDescription.text = "PICK BOOST";
        applypowerUpButton.SetActive(false);
        playerStats = FindFirstObjectByType<PlayerStats>();
        stageManager = FindFirstObjectByType<StageManager>();
        stageProperties = FindFirstObjectByType<StageProperties>();
        closePowerUp = false;
        ShopWindow.gameObject.SetActive(false);
        powerUpButtonWindow.gameObject.SetActive(true);
        for (var i = 0; i < powerUps.Length; i++)
        {
            possibleID.Add(i);
        }

        for (int a = 0; a < 3; a++)
        {
            int randomIDIndex = UnityEngine.Random.Range(0, possibleID.Count); 
            currentId = possibleID[randomIDIndex];
            possibleID.RemoveAt(randomIDIndex);
            go = Instantiate(buttonPrefab, powerUpButtonWindow);
            powerUpDetails = powerUps[currentId].PowerUpName +  "\n \n"+ powerUps[currentId].PowerUpDescription;
            go.GetComponent<SkillButton>().skillNameText.text = powerUpDetails;
            go.GetComponent<Button>().targetGraphic.color = powerUps[currentId].PowerUpColor;
            go.GetComponent<SkillButton>().buttonID = currentId;
            buttons.Add(go);
            
        }
        
    }
    

    private void OnDisable()
    {
        foreach (var b in buttons)
        {
            Destroy(b);
        }
        
        possibleID.Clear();
    }


    public void ClosePowerUp()
    {
        stageProperties.isPaused = false;
        stageProperties.isLevelingUp = false;
    }

    public void OnApplyPowerUp()
    {
        powerUps[selectedID].OnClickButton(playerStats);
        ShopWindow.gameObject.SetActive(true);
        powerUpButtonWindow.gameObject.SetActive(false);
        applypowerUpButton.SetActive(false);
    }
}
