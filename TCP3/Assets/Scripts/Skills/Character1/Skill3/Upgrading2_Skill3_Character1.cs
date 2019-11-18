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
        nConjuration = 0;
        timeConjAgain = 5;
        timer = timeConjAgain;
        invisible = false;
    }
    
    private void Update()
    {
        BasicUpdate();
        if(nConjuration == 1 && !invisible)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = timeConjAgain;
                nConjuration = 0;
            }
        }
    }

    public override void DoIt()
    {
        if (!invisible && nConjuration == 1 && timer >= 0)
        {
            cooldownCount = 0;
        }
        if (cooldownCount <= 0 && level >= 1)
        {
            animator.SetTrigger("Skill3");
            nConjuration += 1;
            if(nConjuration == 1)
            {
                Invisibility(timeInvi);
            }
            else if (nConjuration == 2)
            {
                Invisibility(timeInvi / 2);
                nConjuration = 0;
            }
            gameObject.GetComponent<PlayableCharacters>().audioSrc.PlayOneShot(gameObject.GetComponent<PlayableCharacters>().skill3Sound);
            cooldownCount = cooldown;
        }
    }

    public void SetInvisible(bool i)
    {
        invisible = i;
    }
}