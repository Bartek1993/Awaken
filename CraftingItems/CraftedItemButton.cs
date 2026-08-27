using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftedItemButton : MonoBehaviour
{
    
    public CraftingItemSO itemSo;
    public PlayerStats playerStats;
    public Crafting_Panel crafting_Panel;
    public Sprite buttonIcon;
    public Image buttonImage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        crafting_Panel = FindFirstObjectByType<Crafting_Panel>();
        //////FIX THIS/////
        //buttonImage.Find("ButtonImage").GetComponentInChildren<Image>();
        buttonImage.sprite = buttonIcon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickItem()
    {
        crafting_Panel.currentSelectedItemInList = itemSo;
        itemSo.UseItem(playerStats);
        Destroy(gameObject);
        crafting_Panel.RemoveItemFromPotionList();
        crafting_Panel.ClearReqPanel();
    }
    
}
