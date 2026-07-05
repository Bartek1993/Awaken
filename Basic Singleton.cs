using System;
using UnityEngine;

public class BasicSingleton : MonoBehaviour
{
    
    public static BasicSingleton Instance{get; private set;}

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
