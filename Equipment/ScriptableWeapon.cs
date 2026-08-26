using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/Weapon")]
public class ScriptableWeapon : ScriptableObject
{
    public float baseDamage, piercePower, speed, range;
    public float firePower, icePower, earthPower, thunderPower;
    public float hpRegenRate, maxHp, defence;
    public string weaponName, weaponDescription;
    public int weaponID;




    public void AddWeapon(PlayerWeapons playerWeapons, ScriptableWeapon w)
    {
        w = this;
        playerWeapons.weapons.Add(this);
        playerWeapons.SaveList(w);
    }
}
