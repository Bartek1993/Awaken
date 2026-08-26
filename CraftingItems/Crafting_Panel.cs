using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting_Panel : MonoBehaviour
{
    public CraftingItemSO currentItemSo, currentSelectedItemInList;
    public PlayerStats playerStats;
    public StageProperties stageProperties;
    public Button applyButton;
    public Transform requiredMaterialsParent, recepMaterialsParent;
    public Button inventoryItemButton;
    public Transform itemListParent;
    public List<Image> requiredMaterialsIcons;
    public List<CraftingItemSO> ownedPotions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        ownedPotions.Clear();
        applyButton.gameObject.SetActive(false);
        playerStats = FindFirstObjectByType<PlayerStats>();
        stageProperties = FindFirstObjectByType<StageProperties>();
       
        

    }

    public void ClearReqPanel()
    {
        Image[] img = requiredMaterialsParent.GetComponentsInChildren<Image>(true);
        foreach (Image i in img)
        {
            if (i.transform != requiredMaterialsParent)
            {
                Destroy(i.gameObject);
            }
        }
        StartCoroutine("RefreshRecepPanel");
        Button[] btn = itemListParent.GetComponentsInChildren<Button>(true);
        foreach (var b in btn)
        {
            if (b.transform != itemListParent)
            {
                Destroy(b.gameObject);
            }
        }
       
        for (int a = 0; a < ownedPotions.Count; a++)
        {
            var go = Instantiate(inventoryItemButton, itemListParent.transform);
            go.GetComponent<CraftedItemButton>().itemSo = ownedPotions[a];
            go.onClick.AddListener(() =>
            {
                go.GetComponent<CraftedItemButton>().OnClickItem();
            
            });
            
            
        }
    }

    private IEnumerator RefreshRecepPanel()
    {
        recepMaterialsParent.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(0.001f);
        recepMaterialsParent.gameObject.SetActive(true);
    }


    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        stageProperties.isInDialogue = false;
        stageProperties.isPaused = false;
        
    }


    public void OnApplyPotion(GameObject panel)
    {
        currentItemSo.OnCraftItem(playerStats, this);
        applyButton.gameObject.SetActive(false);
        ClearReqPanel();
    }

    public void OnUseItem()
    {
        currentItemSo.UseItem(playerStats);
    }

    public void AddItemToPotionList(CraftingItemSO item)
    {
        ownedPotions.Add(item);
    }

    public void RemoveItemFromPotionList()
    {
        ownedPotions.Remove(currentSelectedItemInList);
    }
}
