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
    // PRECISA DO SERVIDOR
    public override void DoIt()
    {
        if (cooldownCount <= 0 && level >= 1)
        {
            animator.SetTrigger("Skill2");
            gameObject.GetComponent<PlayableCharacters>().SetUsedSkill(true);
            target.GetComponent<Characters>().SetInControlGroup(true);
            if (target.GetComponent<PlayableCharacters>())
            {
                target.GetComponent<PlayableCharacters>().CanBeSeen(1f);
            }
            gameObject.GetComponent<PlayableCharacters>().audioSrc.PlayOneShot(gameObject.GetComponent<PlayableCharacters>().skill2Sound);
            cooldownCount = cooldown;
        }
    }
}