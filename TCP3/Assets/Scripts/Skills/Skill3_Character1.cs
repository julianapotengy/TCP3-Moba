using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3_Character1 : SkillsBase
{
    private void Awake()
    {
        skillName = "Skill 3";
        description = "A personagem entra em modo furtivo, ganhando invisibilidade por um período máximo de tempo, " +
            "ou até usar alguma habilidade ativa, de item ou ataque básico.";
        level = 0;
        baseCooldown = 60;
        cooldown = baseCooldown;
        cooldownCount = cooldown;
    }

    private void Update()
    {
        cooldownCount += Time.deltaTime;
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
