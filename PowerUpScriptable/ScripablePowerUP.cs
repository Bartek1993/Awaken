using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpScriptable", menuName = "ScriptableObjects/PowerUp")]
public class ScripablePowerUP : ScriptableObject
{
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
    public float invisibilityFramesRoll, invisibilityFramesAfterDamage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnClickButton(PlayerStats playerStats)
    {
        float getDamage = playerStats.baseDamage * baseAttack;
        playerStats.baseDamage += getDamage;
        playerStats.critDamageMultiplier += critDamageMultiplier;
        playerStats.maxCritChance +=  critChance;
        float getHp = playerStats.maxHp * baseHp;
        playerStats.hp += getHp;
        playerStats.maxHp += maxHp;
        playerStats.defence += baseDefense;
        playerStats.projectileSpeed += projectileSpeed;
        playerStats.projectileRange += projectileRange;
        playerStats.invisibilityFramesRoll += invisibilityFramesRoll;
        playerStats.invisibilityFramesAfterDamage += invisibilityFramesAfterDamage;
        Debug.Log("PowerUp Clicked");
        if (!playerStats)
        {
            Debug.Log("No player stats found");
        }
    }
}
