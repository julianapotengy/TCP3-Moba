using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrading1_Skill2_Character1 : Skill2_Character1
{
    private void Awake()
    {
        skillName = "Combatente";
        description = "Ativa o efeito de Roubo de Vida no último tiro, recuperando uma porcentagem do dano causado.";
        level = 2;
        BasicAwake();
    }

    private void Update()
    {
        BasicUpdate();
    }
    // AJUSTAR PARA SER APENAS NO ULTIMO
    public override void DoIt()
    {
        if (cooldownCount >= cooldown && level >= 1)
        {
            gameObject.GetComponent<PlayableCharacters>().SetUsedSkill(true);
            gameObject.GetComponent<PlayableCharacters>().BuffAttackDamage(3, 0.2f);
            gameObject.GetComponent<PlayableCharacters>().BoolStealLife(true);
            cooldownCount = 0;
        }
    }
}