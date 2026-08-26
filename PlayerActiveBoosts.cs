using UnityEngine;

public class PlayerActiveBoosts : MonoBehaviour
{
    
    
    public PlayerStats playerStats;
    public LayerMask enemylayerMask;
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
    public GameObject [] fireOrbs, iceOrbs, thunderOrbs;
    public GameObject fireOrbPosition, iceOrbPosition, thunderOrbPosition;
    public Vector3 fireOrbRotation, iceOrbRotation, thunderOrbRotation;
    /// <summary>
    /// Projectiles
    /// </summary>
    public bool fireProjectile, iceProjectile, thunderProjectile;
    public float fireProjectileSpawnRate, iceProjectileSpawnRate,  thunderProjectileSpawnRate;
    public float fireProjectileTimer, iceProjectileTimer, thunderProjectileTimer;
    public float fireProjectileRotationSpeed, iceProjectileRotationSpeed, thunderProjectileRotationSpeed;
    public float fireProjectileSpeed, iceProjectileSpeed, thunderProjectileSpeed;
    public float fireProjectileRange, iceProjectileRange, thunderProjectileRange;
    public GameObject fireProjectilePrefab, iceProjectilePrefab, thunderProjectilePrefab;
    public GameObject fireProjectilePrefabPosition, iceProjectilePrefabPosition, thunderProjectilePrefabPosition;
    public Vector3 fireProjectileRotation, iceProjectileRotation, thunderProjectileRotation;
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    private void Update()
    {


        RotatingOrbs();
        Projectiles();
        Traps();
    }

    private void Traps()
    {
        switch (fireTrap)
        {
            case true:
                fireTrapTimer += Time.deltaTime;
                if (fireTrapTimer >= fireTrapSpawnTime)
                {
                    fireTrapTimer = 0;
                    GameObject trap = Instantiate(fireTrapPrefab, transform.position + new Vector3(Random.Range(-2,2),0,Random.Range(-2,2)), transform.rotation);
                    trap.transform.localScale = new Vector3(fireTrapSize + playerStats.AOEDistance * 0.05f, fireTrapSize, fireTrapSize + playerStats.AOEDistance *0.05f);
                    Destroy(trap, 3f);
                    trap.transform.parent = null;
                }
                break;
        }

        switch (iceTrap)
        {
            case true:
                iceTrapTimer += Time.deltaTime;
                if (iceTrapTimer >= iceTrapSpawnTime)
                {
                    iceTrapTimer = 0;
                    GameObject trap = Instantiate(iceTrapPrefab, transform.position + new Vector3(Random.Range(-2,2),0,Random.Range(-2,2)), transform.rotation);
                    trap.transform.localScale = new Vector3(iceTrapSize + playerStats.AOEDistance * 0.05f, iceTrapSize, iceTrapSize + playerStats.AOEDistance * 0.05f);
                    Destroy(trap, 2f);
                    trap.transform.parent = null;
                }
                break;
        }
        switch (thunderTrap)
        {
            case true:
                thunderTrapTimer += Time.deltaTime;
                if (thunderTrapTimer >= thunderTrapSpawnTime)
                {
                    thunderTrapTimer = 0;
                    GameObject trap = Instantiate(thunderTrapPrefab, transform.position + new Vector3(Random.Range(-3,3),0,Random.Range(-3,3)), transform.rotation);
                    trap.transform.localScale = new Vector3(thunderTrapSize + playerStats.AOEDistance * 0.05f, thunderTrapSize, thunderTrapSize + playerStats.AOEDistance * 0.05f);
                    Destroy(trap, 2f);
                    trap.transform.parent = null;
                }
                break;
        }
    }

    private void RotatingOrbs()
    {
        fireOrbPosition.transform.Rotate(fireOrbRotation *fireOrbRotationSpeed * Time.deltaTime);
        iceOrbPosition.transform.Rotate(iceOrbRotation *iceOrbRotationSpeed * Time.deltaTime);
        thunderOrbPosition.transform.Rotate(thunderOrbRotation *thunderOrbRotationSpeed * Time.deltaTime);
        if (fireOrb)
        {
            for (var a = 0; a < fireOrbAmount; a++)
            {
                fireOrbs[a].SetActive(true);
            }
        }
        else
        {
            fireOrbs[fireOrbAmount].SetActive(false);
        }

       
       // iceOrbs[iceOrbAmount].SetActive(true);
      //  thunderOrbs[thunderOrbAmount].SetActive(true);
    }

    private void Projectiles()
    {
        switch (fireProjectile)
        {
            case true:
                fireProjectilePrefabPosition.SetActive(true);
                fireProjectileTimer +=  Time.deltaTime;
                if (fireProjectileTimer >= fireProjectileSpawnRate)
                {
                    GameObject fireProj = Instantiate(fireProjectilePrefab, fireProjectilePrefabPosition.transform.position,  fireProjectilePrefabPosition.transform.rotation);
                    fireProj.transform.parent = null;
                    Destroy(fireProj, fireProjectileRange);
                    fireProjectileTimer = 0;
                }
                break;
            case false:
                fireProjectilePrefabPosition.SetActive(false);
                break;
        }

        switch (iceProjectile)
        {
            case true:
                iceProjectilePrefabPosition.SetActive(true);
                iceProjectilePrefabPosition.transform.Rotate(iceProjectileRotation * iceProjectileRotationSpeed * Time.deltaTime);
                iceProjectileTimer +=  Time.deltaTime;
                if (iceProjectileTimer >= iceProjectileSpawnRate)
                {
                    GameObject iceProj = Instantiate(iceProjectilePrefab, iceProjectilePrefabPosition.transform.position,  iceProjectilePrefabPosition.transform.rotation);
                    iceProj.transform.parent = null;
                    Destroy(iceProj, iceProjectileRange);
                    iceProjectileTimer = 0;
                }
                break;
            case false:
                iceProjectilePrefabPosition.SetActive(false);
                break;
        }


        switch (thunderProjectile)
        {
            case true:
                thunderProjectilePrefabPosition.SetActive(true);
                thunderProjectilePrefabPosition.transform.Rotate(thunderProjectileRotation * thunderProjectileRotationSpeed * Time.deltaTime);
                thunderProjectileTimer += Time.deltaTime;
                if (thunderProjectileTimer >= thunderProjectileSpawnRate)
                {
                    GameObject thunderProj = Instantiate(thunderProjectilePrefab, thunderProjectilePrefabPosition.transform.position, thunderProjectilePrefabPosition.transform.rotation);
                    thunderProj.transform.parent = null;
                    Destroy(thunderProj, thunderProjectileRange);
                    thunderProjectileTimer = 0;
                }
                break;
            case false:
                thunderProjectilePrefabPosition.SetActive(false);
                break;
        }
     
    }
}
