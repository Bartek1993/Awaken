using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = System.Random;

public class PowerUpInstantiator : MonoBehaviour
{
    public ScripablePowerUP [] powerUps;
    public Transform powerUpButtonWindow;
    public GameObject buttonPrefab;
    private GameObject go;
    [SerializeField]private List<GameObject> buttons;
    public List<int> possibleID;
    public bool closePowerUp;
    public StageManager stageManager;
    public PlayerStats playerStats;
    private void OnEnable()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        stageManager = FindFirstObjectByType<StageManager>();
        closePowerUp = false;
        for (var i = 1; i < powerUps.Length; i++)
        {
            possibleID.Add(i);
        }

        for (int a = 0; a < 3; a++)
        {
            int randomIDIndex = UnityEngine.Random.Range(0, possibleID.Count); 
            int currentId = possibleID[randomIDIndex];
            possibleID.RemoveAt(randomIDIndex);
            go = Instantiate(buttonPrefab, powerUpButtonWindow);
            go.GetComponent<SkillButton>().buttonID = currentId;
            string powerUpDetails = powerUps[currentId].PowerUpName +"\n"+ powerUps[currentId].PowerUpDescription;
            go.GetComponent<SkillButton>().skillNameText.text = powerUpDetails;
            go.GetComponent<Button>().onClick.AddListener(() => powerUps[currentId].OnClickButton(playerStats));
            go.GetComponent<Button>().onClick.AddListener(() => stageManager.isWaveFinished = false);
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
}
