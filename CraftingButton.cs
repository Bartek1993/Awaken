using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingButton : MonoBehaviour
{
    
    public CraftingItemSO craftingItem;
    public Button button;
    public Text descriptionText, potionName;
    public Crafting_Panel crafting_Panel;
    public PlayerStats playerStats;
    public Sprite potionTexture;
    public Image potionIcon;
    public Image requiredMaterialsIcon;
    
    private void OnEnable()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        crafting_Panel = FindFirstObjectByType<Crafting_Panel>();
        potionName = GetComponentInChildren<Text>();
       // potionIcon = GameObject.Find("PotionIcon").GetComponent<Image>();
        potionTexture = craftingItem.potionTexture;
        potionName.text = craftingItem.potionName;
        potionIcon.sprite = potionTexture;
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            SetButtonParams();
            
        });

       // button.onClick.AddListener(() => { craftingItem.SetParamsAndConditions(playerStats, crafting_Panel); });
    }

    private void Update()
    {
        
    }

    private void SetButtonParams()
    {
        crafting_Panel.applyButton.gameObject.SetActive(true);
        descriptionText.text = craftingItem.description;
        crafting_Panel.currentItemSo = craftingItem; 
        crafting_Panel.ClearReqPanel();
        string herbsOwned;
        for (int i = 0; i < craftingItem.requiredMaterials.Length; i++)
        {var go = Instantiate(requiredMaterialsIcon, crafting_Panel.requiredMaterialsParent);
            requiredMaterialsIcon.sprite = craftingItem.requiredMaterials[i];
            herbsOwned = playerStats.herbsOwned[i].ToString();
            go.GetComponentInChildren<Text>().text = herbsOwned + "/ " + craftingItem.herbReq[i];
        }
    }
}
