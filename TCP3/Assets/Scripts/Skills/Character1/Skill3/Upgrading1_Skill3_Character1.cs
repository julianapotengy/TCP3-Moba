using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrading1_Skill3_Character1 : Skill3_Character1
{
    private void Awake()
    {
        skillName = "Combatente";
        description = "Ganha um escudo que absorve dano até ser destruído. " +
            "Caso receba dano e o escudo ainda esteja invisível, não perderá a invisibilidade.";
        level = 2;
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
            Invisibility(20);
            cooldownCount = 0;
        }
    }
}