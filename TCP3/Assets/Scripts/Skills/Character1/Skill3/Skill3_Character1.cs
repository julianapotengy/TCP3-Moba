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
        BasicAwake();
    }

    private void Update()
    {
        BasicUpdate();
    }

    public override void DoIt()
    {
        if (cooldownCount >= cooldown && level >= 1)
        {
            Invisibility(20, false, 0, 0);
            cooldownCount = 0;
        }
    }

    protected void BasicAwake()
    {
        maxLevel = 3;
        baseCooldown = 60;
        cooldown = baseCooldown;
        cooldownCount = cooldown;

        levelTxt = GameObject.Find("Skill3TextLevel").GetComponent<Text>();
        cooldownTxt = GameObject.Find("Skill3TextCD").GetComponent<Text>();
    }

    protected void BasicUpdate()
    {
        cooldownCount += Time.deltaTime;
        Levels(3);

        levelTxt.text = "Nível: " + level;
        if (cooldownCount <= cooldown)
        {
            cooldownTxt.text = "TR: " + Mathf.Round(cooldownCount) + "s";
        }
        else cooldownTxt.text = "TR: " + Mathf.Round(cooldown) + "s";
    }
}