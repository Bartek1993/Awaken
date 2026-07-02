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
    public float invisibilityFramesRoll, invisibilityFramesAfterDamage, playerSpeed;
    
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
        float getProjectileSPeed = playerStats.projectileSpeed * projectileSpeed;
        playerStats.projectileSpeed += getProjectileSPeed;
        float getProjectileRange = playerStats.projectileRange * projectileRange;
        playerStats.projectileRange += getProjectileRange;
        playerStats.invisibilityFramesRoll += invisibilityFramesRoll;
        playerStats.invisibilityFramesAfterDamage += invisibilityFramesAfterDamage;
        float getPlayerSpeed = playerStats.animator.speed * playerSpeed;
        playerStats.animator.speed += getPlayerSpeed;
        playerStats.hpRegenRate += hpRegenRate;
        Debug.Log("PowerUp Clicked");
        if (!playerStats)
        {
            Debug.Log("No player stats found");
        }
    }
}
