using System.Collections.Generic;
using UnityEngine;
using Esper.ESave;
public class PlayerWeapons : MonoBehaviour
{
    public List<ScriptableWeapon> weapons;
    public ScriptableWeapon weapon;
    public SaveFileSetup saveFileSetup;
    public SaveFile _playerweaponslist, randomNames;
    public string name;
    public List<string> testString;

    public float timer;
    //public GameObject currentWeapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveFileSetup = GetComponent<SaveFileSetup>();
        _playerweaponslist = saveFileSetup.GetSaveFile();
        LoadList();
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            //SaveList(weapon);
            Debug.Log("saved : " + weapon.name);
            LoadList();
        }


    }


    public void SaveList(ScriptableWeapon w)
    {
        weapons.Add(w);
        _playerweaponslist.AddOrUpdateData("playerWeapons", weapons );
        _playerweaponslist.Save();

    }

    public void LoadList()
    {
       
        List<ScriptableWeapon> list = _playerweaponslist.GetData<List<ScriptableWeapon>>("playerWeapons");
        weapons = list;
        
        

    }
}
