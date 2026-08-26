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
    public ScripablePowerUP [] powerUpsPhysical, powerUpsVitality, powerUpsWisdom, powerUpsAgility, powerUpsElemental;
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
        System.GC.Collect();
        powerupDescription.text = "PICK BOOST";
        applypowerUpButton.SetActive(false);
        playerStats = FindFirstObjectByType<PlayerStats>();
        stageManager = FindFirstObjectByType<StageManager>();
        stageProperties = FindFirstObjectByType<StageProperties>();
        closePowerUp = false;
        ShopWindow.gameObject.SetActive(false);
        powerUpButtonWindow.gameObject.SetActive(true);
        SetPath();
    }

    private void SetPath()
    {
        if (stageProperties.path[0])
        {
            for (var i = 0; i < powerUpsVitality.Length; i++)
            {
                possibleID.Add(i);
            }

            for (int a = 0; a < 3; a++)
            {
                int randomIDIndex = UnityEngine.Random.Range(0, possibleID.Count); 
                currentId = possibleID[randomIDIndex];
                possibleID.RemoveAt(randomIDIndex);
                go = Instantiate(buttonPrefab, powerUpButtonWindow);
                powerUpDetails = powerUpsVitality[currentId].PowerUpName +  "\n \n"+ powerUpsVitality[currentId].PowerUpDescription;
                go.GetComponent<SkillButton>().skillNameText.text = powerUpDetails;
                go.GetComponent<Button>().targetGraphic.color = powerUpsVitality[currentId].PowerUpColor;
                go.GetComponent<SkillButton>().buttonID = currentId;
                buttons.Add(go);
            
            }
        }
        if (stageProperties.path[1])
        {
            for (var i = 0; i < powerUpsWisdom.Length; i++)
            {
                possibleID.Add(i);
            }

            for (int a = 0; a < 3; a++)
            {
                int randomIDIndex = UnityEngine.Random.Range(0, possibleID.Count); 
                currentId = possibleID[randomIDIndex];
                possibleID.RemoveAt(randomIDIndex);
                go = Instantiate(buttonPrefab, powerUpButtonWindow);
                powerUpDetails = powerUpsWisdom[currentId].PowerUpName +  "\n \n"+ powerUpsWisdom[currentId].PowerUpDescription;
                go.GetComponent<SkillButton>().skillNameText.text = powerUpDetails;
                go.GetComponent<Button>().targetGraphic.color = powerUpsWisdom[currentId].PowerUpColor;
                go.GetComponent<SkillButton>().buttonID = currentId;
                buttons.Add(go);
            
            }
        }
        if (stageProperties.path[2])
        {
            for (var i = 0; i < powerUpsAgility.Length; i++)
            {
                possibleID.Add(i);
            }

            for (int a = 0; a < 3; a++)
            {
                int randomIDIndex = UnityEngine.Random.Range(0, possibleID.Count); 
                currentId = possibleID[randomIDIndex];
                possibleID.RemoveAt(randomIDIndex);
                go = Instantiate(buttonPrefab, powerUpButtonWindow);
                powerUpDetails = powerUpsAgility[currentId].PowerUpName +  "\n \n"+ powerUpsAgility[currentId].PowerUpDescription;
                go.GetComponent<SkillButton>().skillNameText.text = powerUpDetails;
                go.GetComponent<Button>().targetGraphic.color = powerUpsAgility[currentId].PowerUpColor;
                go.GetComponent<SkillButton>().buttonID = currentId;
                buttons.Add(go);
            
            }
        }
        
        if (stageProperties.path[3])
        {
            for (var i = 0; i < powerUpsPhysical.Length; i++)
            {
                possibleID.Add(i);
            }

            for (int a = 0; a < 3; a++)
            {
                int randomIDIndex = UnityEngine.Random.Range(0, possibleID.Count); 
                currentId = possibleID[randomIDIndex];
                possibleID.RemoveAt(randomIDIndex);
                go = Instantiate(buttonPrefab, powerUpButtonWindow);
                powerUpDetails = powerUpsPhysical[currentId].PowerUpName +  "\n \n"+ powerUpsPhysical[currentId].PowerUpDescription;
                go.GetComponent<SkillButton>().skillNameText.text = powerUpDetails;
                go.GetComponent<Button>().targetGraphic.color = powerUpsPhysical[currentId].PowerUpColor;
                go.GetComponent<SkillButton>().buttonID = currentId;
                buttons.Add(go);
            
            }
        }
        
        if (stageProperties.path[4])
        {
            for (var i = 0; i < powerUpsElemental.Length; i++)
            {
                possibleID.Add(i);
            }

            for (int a = 0; a < 3; a++)
            {
                int randomIDIndex = UnityEngine.Random.Range(0, possibleID.Count); 
                currentId = possibleID[randomIDIndex];
                possibleID.RemoveAt(randomIDIndex);
                go = Instantiate(buttonPrefab, powerUpButtonWindow);
                powerUpDetails = powerUpsElemental[currentId].PowerUpName +  "\n \n"+ powerUpsElemental[currentId].PowerUpDescription;
                go.GetComponent<SkillButton>().skillNameText.text = powerUpDetails;
                go.GetComponent<Button>().targetGraphic.color = powerUpsElemental[currentId].PowerUpColor;
                go.GetComponent<SkillButton>().buttonID = currentId;
                buttons.Add(go);
            
            }
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
        if (stageProperties.path[0])
        {
            powerUpsVitality[selectedID].OnClickButton(playerStats);
        }
        if (stageProperties.path[1])
        {
            powerUpsWisdom[selectedID].OnClickButton(playerStats);
        }
        if (stageProperties.path[2])
        {
            powerUpsAgility[selectedID].OnClickButton(playerStats);
        }
        if (stageProperties.path[3])
        {
            powerUpsPhysical[selectedID].OnClickButton(playerStats);
        }
        if (stageProperties.path[4])
        {
            powerUpsElemental[selectedID].OnClickButton(playerStats);
        }

        
        ShopWindow.gameObject.SetActive(true);
        powerUpButtonWindow.gameObject.SetActive(false);
        applypowerUpButton.SetActive(false);
    }
}
