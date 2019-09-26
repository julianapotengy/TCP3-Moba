using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Character1 : PlayableCharacters
{
	private void Awake()
    {
        objName = "Character1";
        baseLife = 200;
        atkDamage = 10;
        atkRange = 10;
        atkSpeed = 2;
        baseMoveSpeed = 8;
        moveSpeed = baseMoveSpeed;
        temporaryMoveSpeed = moveSpeed;
		atkSpeedCount = 2;

        //skill1 = Skill1_Character1;
        skills.Add(gameObject.AddComponent<Skill1_Character1>());
        skills.Add(gameObject.AddComponent<Skill2_Character1>());
        skills.Add(gameObject.AddComponent<Skill3_Character1>());

        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        levelTxt = GameObject.Find("LevelText").GetComponent<Text>();
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        level = 1;
        canUpSkill = true;
        skillUped = false;

        SetMaxLife(baseLife);
        life = maxLife;
    }

    private void Start()
    {
        detector = GameObject.Find("Detector_" + GetName()).GetComponent<Detector>();
    }

    private void Update()
    {
        Movement();
        PlayableAutoAttack();
        CheckAtkRange();
        ExperienceSystem();
        CheckLife();
        UseSkills();

        levelTxt.text = "Level: " + level;
        lifeText.text = "Life: " + life + " / " + maxLife;
        
        #region Apresentacao
        XPAPRESENTACAO();
        Debug.Log("experience: " + experience + " level: " + level);
        Debug.Log("Vida: " + GetLife() + "; Nome: " + GetName());
        #endregion
    }
}
