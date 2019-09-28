using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrading1_Skill2_Character1 : Skill2_Character1
{
    private void Awake()
    {
        skillName = "Aprimoramento 1";
        description = "Aumenta o range da pistola em 10% enquanto estiver com a munição especial.";
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
            gameObject.GetComponent<PlayableCharacters>().BuffAttackDamage(3, 0.2f, true, 0.1f, false, 0);
            cooldownCount = 0;
        }
    }
}