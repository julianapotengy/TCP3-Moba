using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill2_Character1 : SkillsBase
{
    private void Awake()
    {
        skillName = "Munição Especial";
        description = "Munição especial. A personagem carrega a arma com uma munição especial, " +
            "e nos próximos 3 ataques básicos dá 20% de dano a mais nos alvos.";
        level = 0;
        BasicAwake();
    }

    private void Update()
    {
        BasicUpdate();
    }

    public override void DoIt()
    {
        if (cooldownCount <= 0 && level >= 1)
        {
            gameObject.GetComponent<PlayableCharacters>().SetUsedSkill(true);
            gameObject.GetComponent<PlayableCharacters>().SetUsedSkill(true);
            gameObject.GetComponent<PlayableCharacters>().BuffAttackDamage(3, 0.2f);
            cooldownCount = cooldown;
        }
    }

    protected void BasicAwake()
    {
        maxLevel = 3;
        baseCooldown = 14;
        cooldown = baseCooldown;
        cooldownCount = 0;

        levelTxt = GameObject.Find("Skill2TextLevel").GetComponent<Text>();
        cooldownTxt = GameObject.Find("Skill2TextCD").GetComponent<Text>();
    }

    protected void BasicUpdate()
    {
        cooldownCount -= Time.deltaTime;
        Levels(2);

        if (cooldownCount <= 0)
        {
            cooldownTxt.text = "TR: " + Mathf.Round(cooldown) + "s";
        }
        else cooldownTxt.text = "TR: " + Mathf.Round(cooldownCount) + "s";
    }
}
