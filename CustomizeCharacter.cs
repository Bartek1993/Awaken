using UnityEngine;
using UnityEngine.UI;

public class CustomizeCharacter : MonoBehaviour
{
    public GameObject[] hair, beard;
    public GameObject currentHair, currentBeard;
    public int hairID, beardID;
    public Button hairButton,  beardButton;
    public Slider   hairColorR, hairColorG, hairColorB, skinColorR, skinColorG, skinColorB;
    public Color customCollor;
    public Color customColorSkin;
    public Material playerMaterial;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PlayerPrefs.HasKey("hairID"))
        {
            PlayerPrefs.SetInt("hairID", 4);
        }
        else
        {
            hairID = PlayerPrefs.GetInt("hairID");
        }

        if (!PlayerPrefs.HasKey("beardID"))
        {
            PlayerPrefs.SetInt("beardID", 4);
        }
        else
        {
            beardID = PlayerPrefs.GetInt("beardID");
        }

        if (!PlayerPrefs.HasKey("skinColorR"))
        {
            PlayerPrefs.SetFloat("skinColorR", 0.55f);
        }

        if (!PlayerPrefs.HasKey("skinColorG"))
        {
            PlayerPrefs.SetFloat("skinColorG", 0.35f);
        }

        if (!PlayerPrefs.HasKey("skinColorB"))
        {
            PlayerPrefs.SetFloat("skinColorB", 0.35f);
        }


        hairID = PlayerPrefs.GetInt("hairID");
        beardID = PlayerPrefs.GetInt("beardID");
        hairColorR.value = PlayerPrefs.GetFloat("hairColorR");
        hairColorG.value = PlayerPrefs.GetFloat("hairColorG");
        hairColorB.value = PlayerPrefs.GetFloat("hairColorB");
        skinColorR.value = PlayerPrefs.GetFloat("skinColorR");
        skinColorG.value = PlayerPrefs.GetFloat("skinColorG");
        skinColorB.value = PlayerPrefs.GetFloat("skinColorB");
        customCollor = new Color(hairColorR.value,hairColorG.value,hairColorB.value);
        hairButton.GetComponentInChildren<Text>().text = "Hair + " + hairID;
      
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (var h in hair)
        {
            h.SetActive(false);
        }

        foreach (var b in beard)
        {
            b.SetActive(false);
        }

        if (beardID > beard.Length)
        {
            beardID = 0;
        }

        if (hairID > hair.Length)
        {
            hairID = 0;
        }

        currentHair = hair[hairID];
        currentHair.SetActive(true);
        currentBeard = beard[beardID];
        currentBeard.SetActive(true);
        customCollor = new Color(hairColorR.value,hairColorG.value,hairColorB.value);
        customColorSkin = new Color(skinColorR.value, skinColorG.value, skinColorB.value);
        playerMaterial.SetColor("_Color2",customCollor);
        playerMaterial.SetColor("_Color1",customColorSkin);
        SetHaircolorSliders();
        SetSkinColorSliders();
        
        
    }

    public void SetHaircolorSliders()
    {
        hairColorR.onValueChanged.AddListener(delegate { PlayerPrefs.SetFloat("hairColorR",hairColorR.value); });
        hairColorG.onValueChanged.AddListener(delegate { PlayerPrefs.SetFloat("hairColorG",hairColorG.value); });
        hairColorB.onValueChanged.AddListener(delegate { PlayerPrefs.SetFloat("hairColorB",hairColorB.value); });
    }

    public void SetSkinColorSliders()
    {
        skinColorR.onValueChanged.AddListener(delegate { PlayerPrefs.SetFloat("skinColorR",skinColorR.value); });
        skinColorG.onValueChanged.AddListener(delegate { PlayerPrefs.SetFloat("skinColorG",skinColorG.value); });
        skinColorB.onValueChanged.AddListener(delegate { PlayerPrefs.SetFloat("skinColorB",skinColorB.value); });
    }

    public void SetCurrentHairID()
    {
        hairID+=1;
        hairButton.GetComponentInChildren<Text>().text = "Hair " + hairID;
        PlayerPrefs.SetInt("hairID", hairID);
        
    }
    
    public void SetCurrentBeardID()
    {
        beardID+=1;
        beardButton.GetComponentInChildren<Text>().text = "Beard " + beardID;
        PlayerPrefs.SetInt("beardID", beardID);
        
    }
    
    public void RandomizeCharacter()
    {
        hairID = Random.Range(0, hair.Length);
        beardID = Random.Range(0, beard.Length);
        hairColorR.value = Random.Range(0, 1);
        hairColorG.value = Random.Range(0, 1);
        hairColorB.value = Random.Range(0, 1);
        skinColorR.value = Random.Range(0, 1f);
        skinColorG.value = Random.Range(0, 1f);
        skinColorB.value = Random.Range(0, 1f);
        PlayerPrefs.SetInt("beardID", beardID);
        PlayerPrefs.SetInt("hairID", hairID);
        
    }


}
