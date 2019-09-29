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
            gameObject.GetComponent<PlayableCharacters>().SetUsedSkill(true);
            gameObject.GetComponent<PlayableCharacters>().SetUsedSkill(true);
            gameObject.GetComponent<PlayableCharacters>().BuffAttackDamage(3, 0.2f, false, 0, false, 0);
            cooldownCount = 0;
        }
    }

    protected void BasicAwake()
    {
        maxLevel = 3;
        baseCooldown = 14;
        cooldown = baseCooldown;
        cooldownCount = cooldown;

        levelTxt = GameObject.Find("Skill2TextLevel").GetComponent<Text>();
        cooldownTxt = GameObject.Find("Skill2TextCD").GetComponent<Text>();
    }

    protected void BasicUpdate()
    {
        cooldownCount += Time.deltaTime;
        Levels(2);

        levelTxt.text = "Level: " + level;
        if (cooldownCount <= cooldown)
        {
            cooldownTxt.text = "CD: " + Mathf.Round(cooldownCount) + "s";
        }
        else cooldownTxt.text = "CD: " + Mathf.Round(cooldown) + "s";
    }
}
