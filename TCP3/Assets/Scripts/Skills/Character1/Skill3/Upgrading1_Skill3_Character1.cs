using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrading1_Skill3_Character1 : Skill3_Character1
{
    private void Awake()
    {
        skillName = "Aprimoramento 1";
        description = "Ao sair da invisibilidade, a personagem ganha 20% de velocidade de ataque por 4s.";
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
            Invisibility(20, true, 0.2f, 4);
            cooldownCount = 0;
        }
    }
}