using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayableCharacters : Characters
{
    protected int level;
    protected Text levelTxt;
    protected Text lifeText;
    protected int passiveCharacter;
    protected int passiveClass;
    protected bool canUpSkill;
    protected bool canUpUlt;
    protected bool skillUped;
    protected bool canLevelUp;
    protected int skillsToUp;

    protected List<SkillsBase> skills = new List<SkillsBase>(3);

    protected NavMeshAgent agent;
    protected Vector3 targetDestination;

    protected void PlayableAutoAttack()
    {
        if (detector.GetCharactersOnArea().Count > 0 && Input.GetKeyDown(InputManager.IM.autoAtk))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "RedTeam")
                {
                    for (int i = 0; i < detector.GetCharactersOnArea().Count; i++)
                    {
                        if (detector.GetCharactersOnArea()[i].gameObject == hit.transform.gameObject)
                        {
                            target = detector.GetCharactersOnArea()[i];
                        }
                    }
                }
            }
        }
        else
        {
            if (target != null)
                target = null;
        }

        atkSpeedCount += Time.deltaTime;
        if (atkSpeedCount >= atkSpeed && target != null)
        {
            AutoAttack();
            atkSpeedCount = 0;
        }
    }

    protected void CheckAtkRange()
    {
        if (Input.GetKey(InputManager.IM.seeAtkRange))
        {
            detector.GetComponent<MeshRenderer>().enabled = true;
        }
        else detector.GetComponent<MeshRenderer>().enabled = false;
    }

    protected void Movement()
    {
        if (Input.GetKeyDown(InputManager.IM.walk))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetDestination = hit.point;
                agent.SetDestination(targetDestination);
            }
        }
    }

    #region Experience
    protected void GainExperienceInLane(float quantity)
    {
        if(level >= 1 && level <= 11)
        {
            experience += quantity * Time.deltaTime;
            if(experience >= 100)
            {
                level += 1;
                experience = 0;
            }
        }
    }

    protected void GainExperienceInJungle(float quantity)
    {
        if (level >= 1 && level <= 11)
        {
            experience += quantity;
            if (experience >= 100)
            {
                canLevelUp = true;
                level += 1;
                experience = 0;
            }
        }
    }
    
    protected void ExperienceSystem()
    {
        if(level.Equals(1) && canLevelUp)
        {
            SetMaxLife(baseLife);
            life = maxLife;
            LevelUp();
        }
        else if ((level.Equals(2) || level.Equals(3) || level.Equals(5) || level.Equals(6) || level.Equals(7) || level.Equals(9) || level.Equals(10) || level.Equals(11)) && canLevelUp)
        {
            SetMaxLife(Mathf.Round(maxLife / 10));
            life += Mathf.Round(maxLife / 10);
            LevelUp();
        }
        else if((level.Equals(4) || level.Equals(8) || level.Equals(12)) && canLevelUp)
        {
            SetMaxLife(Mathf.Round(maxLife / 10));
            life += Mathf.Round(maxLife / 10);
            canUpUlt = true;
            LevelUp();
        }
    }

    protected void LevelUp()
    {
        canUpSkill = true;
        skillsToUp += 1;
        canLevelUp = false;
    }

    protected void XPAPRESENTACAO() //apresentacao
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            if (level >= 1 && level <= 11)
            {
                GainExperienceInJungle(100);
            }
        }
    }
    #endregion

    #region Skills
    protected void UseSkills()
    {
        if(Input.GetKeyDown(InputManager.IM.skill1))
        {
            skills[0].DoIt();
        }
        if (Input.GetKeyDown(InputManager.IM.skill2))
        {
            skills[1].DoIt();
        }
        if (Input.GetKeyDown(InputManager.IM.skill3))
        {
            skills[2].DoIt();
        }
    }

    protected void UpSkills()
    {
        if(skillsToUp > 0 && Input.GetKey(InputManager.IM.upSkill) && canUpSkill)
        {
            if (Input.GetKeyDown(InputManager.IM.skill1))
            {
                skills[0].AddLevel(1);
                skillsToUp -= 1;
            }
            else if (Input.GetKeyDown(InputManager.IM.skill2))
            {
                skills[1].AddLevel(1);
                skillsToUp -= 1;
            }
            else if (Input.GetKeyDown(InputManager.IM.skill3) && canUpUlt)
            {
                skills[2].AddLevel(1);
                skillsToUp -= 1;
                canUpUlt = false;
            }
        }
        else if(skillsToUp <= 0)
        {
            canUpSkill = false;
        }
    }
    #endregion
}
