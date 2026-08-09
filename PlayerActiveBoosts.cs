using UnityEngine;

public class PlayerActiveBoosts : MonoBehaviour
{
    
    
    public PlayerStats playerStats;
    /// <summary>
    /// Traps
    /// </summary>
    public bool fireTrap, iceTrap, thunderTrap;
    public float fireTrapSpawnTime, iceTrapSpawnTime, thunderTrapSpawnTime;
    public float fireTrapTimer, iceTrapTimer, thunderTrapTimer;
    public float fireTrapSize, iceTrapSize, thunderTrapSize;
    public GameObject fireTrapPrefab, iceTrapPrefab, thunderTrapPrefab;
    /// <summary>
    /// Rotating Orbs and other
    /// </summary>
    public bool fireOrb, iceOrb, thunderOrb;
    public int fireOrbAmount,  iceOrbAmount, thunderOrbAmount;
    public float fireOrbRotationSpeed, iceOrbRotationSpeed,  thunderOrbRotationSpeed;
    public float fireOrbRange, iceOrbRange, thunderOrbRange;
    public GameObject fireOrbPrefab, iceOrbPrefab, thunderOrbPrefab;
    /// <summary>
    /// Projectiles
    /// </summary>
    public bool fireProjectile, iceProjectile, thunderProjectile;
    public float fireProjectileSpawnRate, iceProjectileSpawnRate,  thunderProjectileSpawnRate;
    public float fireProjectileTimer, iceProjectileTimer, thunderProjectileTimer;
    public float fireProjectileRotation, iceProjectileRotation, thunderProjectileRotation;
    public GameObject fireProjectilePrefab, iceProjectilePrefab, thunderProjectilePrefab;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
