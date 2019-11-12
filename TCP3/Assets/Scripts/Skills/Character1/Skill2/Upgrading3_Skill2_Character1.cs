using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrading3_Skill2_Character1 : Skill2_Character1
{
    private void Awake()
    {
        skillName = "Controlador";
        description = "Revela uma área em volta daqueles atingidos pelos tiros por breves momentos. " +
            "Atirar novamente tem o mesmo efeito.";
        level = 2;
        BasicAwake();
    }

    private void Update()
    {
        BasicUpdate();
    }
    // SERVIDOR
    public override void DoIt()
    {
        if (cooldownCount >= cooldown && level >= 1)
        {
            gameObject.GetComponent<PlayableCharacters>().SetUsedSkill(true);
            target.GetComponent<Characters>().SetInControlGroup(true);
            gameObject.GetComponent<PlayableCharacters>().BuffAttackDamage(3, 0.2f);
            cooldownCount = 0;
        }
    }
}