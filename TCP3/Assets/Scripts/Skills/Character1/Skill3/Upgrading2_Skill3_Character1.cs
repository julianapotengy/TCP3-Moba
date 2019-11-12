using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrading2_Skill3_Character1 : Skill3_Character1
{
    private void Awake()
    {
        skillName = "Aprimoramento 2";
        description = "Permite usar a habilidade novamente após o período de invisibilidade terminar. " +
            "A segunda conjuração deve ser feita dentro de um curto espaço de tempo, " +
            "e a duração da segunda invisibilidade é menor.";
        level = 2;
        BasicAwake();
    }
    // FAZER
    private void Update()
    {
        BasicUpdate();
    }

    public override void DoIt()
    {
        if (cooldownCount >= cooldown && level >= 1)
        {
            Invisibility(20);
            ChangeSpeed(0.15f, 4, this.gameObject.transform);
            cooldownCount = 0;
        }
    }
}