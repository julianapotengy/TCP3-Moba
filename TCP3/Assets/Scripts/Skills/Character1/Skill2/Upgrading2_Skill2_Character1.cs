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
    // FAZER
    public override void DoIt()
    {
        if (cooldownCount >= cooldown && level >= 1)
        {
            gameObject.GetComponent<PlayableCharacters>().SetUsedSkill(true);
            gameObject.GetComponent<PlayableCharacters>().BuffAttackDamage(3, 0.2f);
            gameObject.GetComponent<PlayableCharacters>().BoolDoubleDamage(true);
            cooldownCount = 0;
        }
    }
}