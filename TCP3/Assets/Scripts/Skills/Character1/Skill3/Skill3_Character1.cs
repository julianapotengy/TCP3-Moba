using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill3_Character1 : SkillsBase
{
    private void Awake()
    {
        skillName = "Skill 3";
        description = "A personagem entra em modo furtivo, ganhando invisibilidade por um período máximo de tempo, " +
            "ou até usar alguma habilidade ativa, de item ou ataque básico.";
        level = 0;
        maxLevel = 3;
        baseCooldown = 60;
        cooldown = baseCooldown;
        cooldownCount = cooldown;

        levelTxt = GameObject.Find("Skill3TextLevel").GetComponent<Text>();
        cooldownTxt = GameObject.Find("Skill3TextCD").GetComponent<Text>();
    }

    private void Update()
    {
        cooldownCount += Time.deltaTime;
        Levels(3);

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
            Invisibility(20);
            cooldownCount = 0;
        }
    }
}
