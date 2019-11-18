using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill3_Character1 : SkillsBase
{
    protected int nConjuration;
    protected float timeConjAgain;
    protected float timer;
    protected bool invisible;
    protected float timeInvi;

    private void Awake()
    {
        skillName = "Modo Furtivo";
        description = "A personagem entra em modo furtivo, ganhando invisibilidade por um período máximo de tempo, " +
            "ou até usar alguma habilidade ativa, de item ou ataque básico.";
        level = 0;
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
            animator.SetTrigger("Skill3");
            Invisibility(timeInvi);
            gameObject.GetComponent<PlayableCharacters>().audioSrc.PlayOneShot(gameObject.GetComponent<PlayableCharacters>().skill3Sound);
            cooldownCount = cooldown;
        }
    }

    protected void BasicAwake()
    {
        maxLevel = 3;
        baseCooldown = 60;
        cooldown = baseCooldown;
        cooldownCount = 0;
        timeInvi = 15;
        animator = this.GetComponent<Animator>();
        levelTxt = GameObject.Find("Skill3TextLevel").GetComponent<Text>();
        cooldownTxt = GameObject.Find("Skill3TextCD").GetComponent<Text>();
    }

    protected void BasicUpdate()
    {
        cooldownCount -= Time.deltaTime;
        Levels(3);

        levelTxt.text = "Nível: " + level;
        if (cooldownCount <= 0)
        {
            cooldownTxt.text = "TR: " + Mathf.Round(cooldown) + "s";
        }
        else cooldownTxt.text = "TR: " + Mathf.Round(cooldownCount) + "s";
    }
}