using UnityEngine;

public enum itemType
{
    Potion, Armor, Weapon, PowerUp
}

[CreateAssetMenu(menuName = "Chest/ChestItemSO",  fileName = "ChestItemSO")]
public class ChestItemSO : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int cost;
    public Texture icon;
    public itemType itemType;
    public int itemLevel;
    [Tooltip("for potion")]
    public float hpAddition;
    [Tooltip("for armor and weapon")]
    public bool isEquipable;
    public float physical, range, speed, hpReg;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(PlayerStats playerStats, StageManager stageManager)
    {
        switch (itemType)
        {
            case itemType.Potion:
                itemLevel += stageManager.waveCount;
                playerStats.hp += hpAddition * itemLevel;
                playerStats.baseDamage += physical;
                playerStats.hpRegenRate += hpReg;
                break;
            
        }
        
    }
}
