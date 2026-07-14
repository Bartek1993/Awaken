using UnityEngine;

public class GetCustomatization : MonoBehaviour
{
    public GameObject[] hair;
    public GameObject[] beard;
    public GameObject currentHair, currentBeard;
    public int hairID, beardID;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hairID = PlayerPrefs.GetInt("hairID");
        beardID = PlayerPrefs.GetInt("beardID");
        foreach (var h in hair)
        {
            h.SetActive(false);
        }

        foreach (var b in beard)
        {
            b.SetActive(false);
        }
        currentHair = hair[hairID];
        currentHair.SetActive(true);
        currentBeard = beard[beardID];
        currentBeard.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
