using System;
using UnityEngine;
using UnityEngine.UI;

public class IngameLevelUp : MonoBehaviour
{
    public PlayerStats playerStats;
    public Animator playerAnimator;
    public Text skillpointsText;
    public Text [] playerStatsText;
    public int currentSkillPoints;
    public Text[] playerStatNamesText;
    public Button[] buttonsSkills;
    public StageProperties stageProperties;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        playerAnimator = playerStats.GetComponent<Animator>();
        GetPlayerStats();
    }

   

    // Update is called once per frame
    void Update()
    {
        if (currentSkillPoints <= 0)
        {
            currentSkillPoints = 0;
        }
        if (currentSkillPoints <= 0)
        {
            foreach (Button button in buttonsSkills)
            {
                button.interactable = false;
            }
            
        }
        else
        {
            foreach (Button button in buttonsSkills)
            {
                button.interactable = true;
            }
        }
    }
    
    
    public void GetPlayerStats()
    {
       
        currentSkillPoints =  PlayerPrefs.GetInt("currentSkillPoints");
        skillpointsText.text = "SKILL POINTS: " + currentSkillPoints;
        playerStatsText[0].text = "" + playerStats.maxHp;
        playerStatsText[1].text = "" + playerStats.staminaCoolDown.ToString("F2") + " sec";
        playerStatsText[2].text = "" + playerStats.baseDamage;
        playerStatsText[3].text = "" + playerStats.maxCritChance.ToString("F2") + " %";;
        playerStatsText[4].text = "" + playerStats.critDamageMultiplier.ToString("F2") + " %";
        playerStatsText[5].text = "" + playerStats.magicCooldown.ToString("F1") + "sec";
        playerStatsText[6].text = "" + playerStats.magicCooldown +  " sec";
        playerStatsText[7].text = "" + playerStats.magicStrength;
        playerStatsText[8].text = "" + playerStats.fireChance.ToString("F2") + " %";
        playerStatsText[9].text = "" + playerStats.iceChance.ToString("F2") + "%";
        playerStatsText[10].text = "" + playerStats.animator.speed;
        playerStatsText[11].text = "" + playerStats.projectileRange.ToString("F2");
        playerStatsText[12].text = "" + playerStats.projectileSpeed.ToString("F2");
        playerStatsText[12].text = "" + playerStats.expMultiplier.ToString("F2");
        playerStatNamesText[0].text = "VITALITY LV " + PlayerPrefs.GetInt("VitalityLV");
        playerStatNamesText[1].text = "STRENGTH LV " + PlayerPrefs.GetInt("StrengthLV");
        playerStatNamesText[2].text = "AGILITY LV" + PlayerPrefs.GetInt("AgilityLV");
        playerStatNamesText[3].text = "WISDOM LV " + PlayerPrefs.GetInt("MindLV");
        playerStatNamesText[4].text = "ELEMENTAL LV " + PlayerPrefs.GetInt("ElementalLV");
        stageProperties.GetPlayerStats();
        
    }
    
    
    public void AddPlayerStat(int id)
    {
        currentSkillPoints -= 1;
        PlayerPrefs.SetInt("currentSkillPoints", currentSkillPoints);
        int totalSkillPoints = PlayerPrefs.GetInt("totalSkillPoints");
        PlayerPrefs.SetInt("totalSkillPoints", totalSkillPoints + 1);
        switch (id)
        {
            case 1:
                int vit =  PlayerPrefs.GetInt("VitalityLV");
                PlayerPrefs.SetInt("VitalityLV", vit +1);
                float maxHp = PlayerPrefs.GetFloat("maxHp");
                PlayerPrefs.SetFloat("maxHp", maxHp + 5f);
                float staminaCooldownRate = PlayerPrefs.GetFloat("staminaCoolDown");
                PlayerPrefs.SetFloat("staminaCoolDown", staminaCooldownRate + 0.005f);
                break;
            case 2:
                int strength =  PlayerPrefs.GetInt("StrengthLV");
                PlayerPrefs.SetInt("StrengthLV", strength +1);
                float baseattack = PlayerPrefs.GetFloat("physicalAttack");
                PlayerPrefs.SetFloat("physicalAttack", baseattack + 1f);
                float critticalChance =  PlayerPrefs.GetFloat("criticalChance");
                PlayerPrefs.SetFloat("criticalChance", critticalChance + 0.0015f);
                float critDamage = PlayerPrefs.GetFloat("criticalDamage");
                PlayerPrefs.SetFloat("criticalDamage", critDamage + 0.025f);
                float piercePower = PlayerPrefs.GetFloat("PiercePower");
                PlayerPrefs.SetFloat("PiercePower", piercePower + 0.005f);
                break;
            case 3:
                int agility =  PlayerPrefs.GetInt("AgilityLV");
                PlayerPrefs.SetInt("AgilityLV", agility +1);
                float weaponRange = PlayerPrefs.GetFloat("weaponRange");
                PlayerPrefs.SetFloat("weaponRange", weaponRange + 0.05f);
                float weaponReach = PlayerPrefs.GetFloat("weaponReach");
                PlayerPrefs.SetFloat("weaponReach", weaponReach + 0.05f);
                float playermovement = PlayerPrefs.GetFloat("moveSpeed");
                PlayerPrefs.SetFloat("moveSpeed", playermovement + 0.0015f);
                break;
            case 4:
                int mind =  PlayerPrefs.GetInt("MindLV");
                PlayerPrefs.SetInt("MindLV", mind + 1);
                float maxMp = PlayerPrefs.GetFloat("maxMp");
                PlayerPrefs.SetFloat("maxMp", maxMp + 1f);
                float mpRegenRate = PlayerPrefs.GetFloat("magicCooldown");
                PlayerPrefs.SetFloat("magicCooldown", mpRegenRate + 0.015f);
                float magicPower = PlayerPrefs.GetFloat("magicPower");
                PlayerPrefs.SetFloat("magicPower", magicPower + 0.25f);
                float magicCritical = PlayerPrefs.GetFloat("magicCriticalChance");
                PlayerPrefs.SetFloat("magicCriticalChance", magicCritical + 0.005f);
                float magicCriticalDamage = PlayerPrefs.GetFloat("MagicCriticalDamage");
                PlayerPrefs.SetFloat("magicCriticalDamage", magicCriticalDamage + 0.05f);
                break;
            case 5:
                int elemental =  PlayerPrefs.GetInt("ElementalLV");
                PlayerPrefs.SetInt("ElementalLV", elemental + 1);
                float ice = PlayerPrefs.GetFloat("iceChance");
                PlayerPrefs.SetFloat("iceChance", ice + 0.0015f);
                float fire = PlayerPrefs.GetFloat("fireChance");
                PlayerPrefs.SetFloat("fireChance", fire + 0.0015f);
                break;
        }
      
        playerStats.SetPlayerStats();
        stageProperties.GetPlayerStats();
      
        
       
       
    }
    
}
