using UnityEngine;

[CreateAssetMenu(fileName = "Craft", menuName = "ScriptableObjects/CraftItem")]
public class CraftingItemSO : ScriptableObject
{
    public int greenHerbReq, redHerbReq, yellowHerbReq, blueHerbReq;
    public int [] herbReq;
    public bool isGreenPotion, isRedPotion, isBluePotion, isYellowPotion;
    public bool playerStatGreen,  playerStatRed, playerStatBlue, playerStatYellow;
    public string description, potionName;
    public float toxicityAddition;
    public float maxHpAmount, defenceAmount, hpRegAmount;
    public float physicalDamage,critDamageAmount, critChanceAmount;
    public Sprite potionTexture;
    public Sprite [] requiredMaterials;


    public void OnCraftItem(PlayerStats playerStats, Crafting_Panel crafting_Panel)
    {
        if (isGreenPotion)
        {
            for (int a = 0; a < herbReq.Length; a++)
            {
                playerStats.herbsOwned[a] -= herbReq[a];
            }
            playerStats.potionsOwned[0] += 1;
        }
        
        if (isRedPotion)
        {
            for (int a = 0; a < herbReq.Length; a++)
            {
                playerStats.herbsOwned[a] -= herbReq[a];
            }
            playerStats.potionsOwned[1] += 1;
        }
       
        
        if (isYellowPotion)
        {
            for (int a = 0; a < herbReq.Length; a++)
            {
                playerStats.herbsOwned[a] -= herbReq[a];
            }
            playerStats.potionsOwned[2] += 1;
        }
      

        if (isBluePotion)
        {
            for (int a = 0; a < herbReq.Length; a++)
            {
                playerStats.herbsOwned[a] -= herbReq[a];
            }
            playerStats.potionsOwned[3] += 1;
            
        }
        
        crafting_Panel.AddItemToPotionList(this);
        
    }

    public void UseItem(PlayerStats playerStats)
    {
        playerStats.AddPotionStatus(toxicityAddition,maxHpAmount, defenceAmount, hpRegAmount, physicalDamage, critDamageAmount, critChanceAmount);

        playerStats.setGreenTimer = isGreenPotion;
        playerStats.setRedTimer = isRedPotion;
        if (isGreenPotion)
        {
            playerStats.potionsOwned[0] -= 1;
            playerStats.greenStatTimer += 20f;
        }
        if (isRedPotion)
        {
            playerStats.potionsOwned[1] -= 1;
            playerStats.redStatTimer += 20f;
        }
        if (isYellowPotion)
        {
            playerStats.potionsOwned[2] -= 1;
            playerStats.yellowStatTimer += 20f;
        }
        if (isBluePotion)
        {
            playerStats.potionsOwned[3] -= 1;
            playerStats.blueStatTimer += 20f;
        }
    }

}
