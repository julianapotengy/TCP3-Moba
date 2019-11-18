using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrading3_Skill3_Character1 : Skill3_Character1
{
    protected List<SkillsBase> skills = new List<SkillsBase>();

    private void Awake()
    {
        skillName = "Controlador";
        description = "Solta a granada (habilidade 1) após conjurar essa habilidade.";
        level = 2;
        BasicAwake();
        skills = gameObject.GetComponent<Character1>().GetSkills();
    }

    private void Update()
    {
        BasicUpdate();
    }

    public override void DoIt()
    {
        if (cooldownCount <= 0 && level >= 1)
        {
            animator.SetTrigger("Skill3");
            Invisibility(timeInvi);
            skills[0].DoIt1(true);
            gameObject.GetComponent<PlayableCharacters>().audioSrc.PlayOneShot(gameObject.GetComponent<PlayableCharacters>().skill3Sound);
            cooldownCount = cooldown;
        }
    }
}
