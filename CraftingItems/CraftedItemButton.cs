using Unity.VisualScripting;
using UnityEngine;

public class CraftedItemButton : MonoBehaviour
{
    
    public CraftingItemSO itemSo;
    public PlayerStats playerStats;
    public Crafting_Panel crafting_Panel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        crafting_Panel = FindFirstObjectByType<Crafting_Panel>();
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
