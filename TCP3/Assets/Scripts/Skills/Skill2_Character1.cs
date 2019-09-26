using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2_Character1 : SkillsBase
{
    private void Awake()
    {
        skillName = "Skill 2";
        description = "Munição especial. A personagem carrega a arma com uma munição especial, " +
            "e nos próximos 3 ataques básicos dá 20% de dano a mais nos alvos.";
        level = 1;
        baseCooldown = 14;
        cooldown = baseCooldown;
        cooldownCount = cooldown;
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
