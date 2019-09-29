using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrading3_Skill2_Character1 : Skill2_Character1
{
    private void Awake()
    {
        skillName = "Aprimoramento 3";
        description = "Adiciona 15% de roubo de vida com a munição especial.";
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
            gameObject.GetComponent<PlayableCharacters>().BuffAttackDamage(3, 0.2f, false, 0, true, 0.15f);
            cooldownCount = 0;
        }
    }
}