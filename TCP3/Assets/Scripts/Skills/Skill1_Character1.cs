using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1_Character1 : SkillsBase
{
    private void Awake()
    {
        skillName = "Skill 1";
        description = "Granada de pulso elétrico. A personagem joga uma granada, que estoura no primeiro alvo que acertar, " +
            "causando dano e aplicando efeito de lentidão no alvo por 2s.";
        range = 10;
        level = 1;
        baseCooldown = 7;
        cooldown = baseCooldown;
        cooldownCount = cooldown;
        baseDamage = 30;
        damage = baseDamage;
        canUse = true;
    }

    private void Update()
    {
        cooldownCount += Time.deltaTime;
    }

    public override void DoIt()
    {
        if (cooldownCount >= cooldown)
        {
            Debug.Log(skillName);
            cooldownCount = 0;
        }
    }
}
