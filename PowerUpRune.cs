using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = System.Random;

public class PowerUpRune : MonoBehaviour
{
    public ScripablePowerUP [] powerUps;
    public GameObject player;
    public Transform powerUpButtonWindow;
    public GameObject buttonPrefab;
    private GameObject go;
    [SerializeField]private List<GameObject> buttons;
    public List<int> possibleID;

    private void OnEnable()
    {
        for (var i = 1; i < 12; i++)
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
            buttons.Add(go);
        }
        
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
      
      
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
