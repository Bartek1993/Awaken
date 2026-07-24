using UnityEngine;

public class StageProperties : MonoBehaviour
{
    public bool isPaused = false;
    public bool isLevelingUp = false;
    public GameObject playerLevelUpWindow;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        
        
        playerLevelUpWindow.SetActive(isLevelingUp);
    }


    public void PauseSwitch()
    {
        isPaused = !isPaused;
    }
}
