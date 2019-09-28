using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Character1 : PlayableCharacters
{
	private void Awake()
    {
        objName = "Character 1";
        baseLife = 200;
        atkDamage = 10;
        atkRange = 10;
        atkSpeed = 2;
        baseMoveSpeed = 8;
        moveSpeed = baseMoveSpeed;
        temporaryMoveSpeed = moveSpeed;
		atkSpeedCount = 2;
        
        skills.Add(gameObject.AddComponent<Skill1_Character1>());
        skills.Add(gameObject.AddComponent<Skill2_Character1>());
        skills.Add(gameObject.AddComponent<Skill3_Character1>());

        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        levelTxt = GameObject.Find("LevelText").GetComponent<Text>();
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        level = 1;
        canLevelUp = true;
        canUpSkill = true;
    }

    private void Start()
    {
        detector = GameObject.Find("Detector_" + GetName()).GetComponent<Detector>();
    }

    private void Update()
    {
        PlayableAutoAttack();
        Movement();
        agent.speed = moveSpeed;
        CheckAtkRange();
        ExperienceSystem();
        CheckLife();
        UpSkills();
        UseSkills();
        TimeBuffingSpeed();
        BuffingAttackDamage();
        BuffingAttackSpeed();

        levelTxt.text = "Level: " + level;
        lifeText.text = "Life: " + life + " / " + maxLife;
        
        #region Apresentacao
        XPAPRESENTACAO();
        Debug.Log("experience: " + experience + " level: " + level);
        #endregion

        if (skills[0] == null)
        {
            if (GetComponent<Upgrading1_Skill1_Character1>() != null)
            {
                skills[0] = GetComponent<Upgrading1_Skill1_Character1>();
            }
            else if (GetComponent<Upgrading2_Skill1_Character1>() != null)
            {
                skills[0] = GetComponent<Upgrading2_Skill1_Character1>();
            }
            else if (GetComponent<Upgrading3_Skill1_Character1>() != null)
            {
                skills[0] = GetComponent<Upgrading3_Skill1_Character1>();
            }
        }
        if (skills[1] == null)
        {
            if (GetComponent<Upgrading1_Skill2_Character1>() != null)
            {
                skills[1] = GetComponent<Upgrading1_Skill2_Character1>();
            }
            else if (GetComponent<Upgrading2_Skill2_Character1>() != null)
            {
                skills[1] = GetComponent<Upgrading2_Skill2_Character1>();
            }
            else if (GetComponent<Upgrading3_Skill2_Character1>() != null)
            {
                skills[1] = GetComponent<Upgrading3_Skill2_Character1>();
            }
        }
        if (skills[2] == null)
        {
            if (GetComponent<Upgrading1_Skill3_Character1>() != null)
            {
                skills[2] = GetComponent<Upgrading1_Skill3_Character1>();
            }
            else if (GetComponent<Upgrading2_Skill3_Character1>() != null)
            {
                skills[2] = GetComponent<Upgrading2_Skill3_Character1>();
            }
            else if (GetComponent<Upgrading3_Skill3_Character1>() != null)
            {
                skills[2] = GetComponent<Upgrading3_Skill3_Character1>();
            }
        }
    }
}
