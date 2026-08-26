
using UnityEngine;

    

[CreateAssetMenu(fileName = "PowerUpScriptable", menuName = "ScriptableObjects/PowerUp")]
public class ScripablePowerUP : ScriptableObject
{
    public enum cardType
    {
        POWER, AGILITY, VITALITY, SPECIAL
    }
    public cardType card;
    public bool isPernament;
    public int cardLevel;
    public string PowerUpName, PowerUpDescription;
    public Color PowerUpColor;
    [Tooltip("Attack Attributes")]
    public float baseAttack, critDamageMultiplier, critChance;
    [Tooltip("Health Attributes")]
    public float baseHp, maxHp, hpRegenRate;
    [Tooltip("Defense Attributes")]
    public float baseDefense;
    [Tooltip("Weapon Projectile Attributes")]
    public float projectileSpeed, projectileRange;
    [Tooltip("Player Speed and Movement Attributes")]
    public float invisibilityFramesRoll, invisibilityFramesAfterDamage, playerSpeed;
    [Tooltip("SPECIAL Attributes")] public float enemyStopTime, iceChance, frozenTime, fireChance, magnetDistance, AOEDistance, magicPower,
        iceDamage, thunderDamage, earthDamage, fireDamage, knockbackStrength, evadeChance, lifestealamount, lifestealchance, expMultiplier, piercePower,
        magicCritRate, magicCritDamage;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnClickButton(PlayerStats playerStats)
    {
        float getDamage = (playerStats.baseDamage * baseAttack) * cardLevel;
        playerStats.stageExtraDamage += getDamage;
        playerStats.critDamageMultiplier += critDamageMultiplier;
        playerStats.maxCritChance +=  critChance;
        float getHp = (playerStats.maxHp * baseHp) * cardLevel;
        playerStats.hp += getHp;
        playerStats.stageExtraMaxHp += maxHp;
        float getProjectileSPeed = (playerStats.projectileSpeed * projectileSpeed) * cardLevel;
        playerStats.stageExtraProjectileSpeed += projectileSpeed;
        float getProjectileRange = (playerStats.projectileRange * projectileRange) * cardLevel;
        playerStats.stageExtraProjectileRange += projectileRange;
        playerStats.invisibilityFramesRoll += invisibilityFramesRoll;
        playerStats.stageExtraInviAfterDamage += invisibilityFramesAfterDamage;
        playerStats.stageExtraSpeed += playerSpeed;
        playerStats.stageExtraRegenRate += hpRegenRate;
        playerStats.stageExtraStunTime += enemyStopTime;
        playerStats.stageExtraIceChance += iceChance;
        playerStats.stageExtraFireChance += fireChance;
        playerStats.stageExtraMagnetDistance += magnetDistance;
        playerStats.stageExtraMagicDistance += AOEDistance;
        playerStats.magicStrength += magicPower;
        playerStats.iceDamage += iceDamage;
        playerStats.stageExtraThunderPower += thunderDamage;
        playerStats.stageExtraFirePower += fireDamage;
        playerStats.stageExtraEarthPower += earthDamage;
        playerStats.stageExtraKnockBackStrength += knockbackStrength;
        playerStats.stageExtraEvadeChance += evadeChance;
        playerStats.stageExtraDefence += baseDefense;
        playerStats.stageExtraFrozenTime += frozenTime;
        playerStats.stageExtraLifeStealAmount  += lifestealamount;
        playerStats.stageExtraLifeStealChance += lifestealchance;
        playerStats.stageExtraExpMultiplier += expMultiplier;
        playerStats.extraPiercePower += piercePower;
        playerStats.magicExtraCriticalRate += magicCritRate;
        playerStats.magicExtraCriticalDamage += magicCritDamage;
        playerStats.SetPlayerStats();
        Debug.Log("PowerUp Clicked");
        
        
        
        if (!playerStats)
        {
            Debug.Log("No player stats found");
        }

        if (isPernament)
        {
            Debug.Log("Pernament");
        }
    }
}
