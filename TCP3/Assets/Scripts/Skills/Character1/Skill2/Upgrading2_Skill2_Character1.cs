using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrading2_Skill2_Character1 : Skill2_Character1
{
    private void Awake()
    {
        skillName = "Flanco";
        description = "O último tiro tem dano dobrado.";
        level = 2;
        BasicAwake();
    }

    private void Update()
    {
        BasicUpdate();
    }
    
    public override void DoIt()
    {
        if (cooldownCount <= 0 && level >= 1)
        {
            animator.SetTrigger("Skill2");
            gameObject.GetComponent<PlayableCharacters>().SetUsedSkill(true);
            gameObject.GetComponent<PlayableCharacters>().BuffAttackDamage(3, 0.2f);
            gameObject.GetComponent<PlayableCharacters>().BoolDoubleDamage(true);
            gameObject.GetComponent<PlayableCharacters>().audioSrc.PlayOneShot(gameObject.GetComponent<PlayableCharacters>().skill2Sound);
            cooldownCount = cooldown;
        }
    }
}