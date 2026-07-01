using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpScriptable", menuName = "ScriptableObjects/PowerUp")]
public class ScripablePowerUP : ScriptableObject
{
    public string PowerUpName, PowerUpDescription;
    [Tooltip("Attack Attributes")]
    public float baseAttack, critDamageMultiplier, critChance;
    public float hpRegen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
