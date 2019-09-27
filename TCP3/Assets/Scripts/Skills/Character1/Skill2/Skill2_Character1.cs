using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill2_Character1 : SkillsBase
{
    private void Awake()
    {
        skillName = "Skill 2";
        description = "Munição especial. A personagem carrega a arma com uma munição especial, " +
            "e nos próximos 3 ataques básicos dá 20% de dano a mais nos alvos.";
        level = 0;
        maxLevel = 3;
        baseCooldown = 14;
        cooldown = baseCooldown;
        cooldownCount = cooldown;

        levelTxt = GameObject.Find("Skill2TextLevel").GetComponent<Text>();
        cooldownTxt = GameObject.Find("Skill2TextCD").GetComponent<Text>();
    }

    private void Update()
    {
        cooldownCount += Time.deltaTime;
        Levels();

        levelTxt.text = "Level: " + level;
        if (cooldownCount <= cooldown)
        {
            cooldownTxt.text = "CD: " + Mathf.Round(cooldownCount) + "s";
        }
        else cooldownTxt.text = "CD: " + Mathf.Round(cooldown) + "s";
    }

    public override void DoIt()
    {
        if (cooldownCount >= cooldown && level >= 1)
        {
            Debug.Log(skillName);
            cooldownCount = 0;
        }
    }
}
