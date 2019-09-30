using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrading2_Skill2_Character1 : Skill2_Character1
{
    private void Awake()
    {
        skillName = "Aprimoramento 2";
        description = "A munição especial marca o alvo e fornece visão dele por 2s.";
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
            gameObject.GetComponent<PlayableCharacters>().SetUsedSkill(true);
            gameObject.GetComponent<PlayableCharacters>().BuffAttackDamage(3, 0.2f, false, 0, false, 0);
            cooldownCount = 0;
        }
    }
}