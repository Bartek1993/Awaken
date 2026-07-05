
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
    [Tooltip("SPECIAL Attributes")] public float enemyStopTime, iceChance, fireChance;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnClickButton(PlayerStats playerStats)
    {
        float getDamage = (playerStats.baseDamage * baseAttack) * cardLevel;
        playerStats.baseDamage += getDamage;
        playerStats.critDamageMultiplier += critDamageMultiplier;
        playerStats.maxCritChance +=  critChance;
        float getHp = (playerStats.maxHp * baseHp) * cardLevel;
        playerStats.hp += getHp;
        playerStats.maxHp += maxHp;
        playerStats.defence += baseDefense;
        float getProjectileSPeed = (playerStats.projectileSpeed * projectileSpeed) * cardLevel;
        playerStats.projectileSpeed += getProjectileSPeed;
        float getProjectileRange = (playerStats.projectileRange * projectileRange) * cardLevel;
        playerStats.projectileRange += getProjectileRange;
        playerStats.invisibilityFramesRoll += invisibilityFramesRoll;
        playerStats.invisibilityFramesAfterDamage += invisibilityFramesAfterDamage;
        float getPlayerSpeed = (playerStats.animator.speed * playerSpeed) * cardLevel;
        playerStats.animator.speed += getPlayerSpeed;
        playerStats.hpRegenRate += hpRegenRate;
        playerStats.enemyStunTime += enemyStopTime;
        playerStats.iceChance += iceChance;
        playerStats.fireChance += fireChance;
        Debug.Log("PowerUp Clicked");
        playerStats.PowerUpNotifier("POP UP");
        
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
