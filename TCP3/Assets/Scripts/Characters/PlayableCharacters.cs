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
    protected Text atkDamageText;
    protected Text atkSpeedText;
    protected Text moveSpeedText;
    #region Skills
    protected int passiveCharacter;
    protected int passiveClass;
    protected bool canUpSkill;
    protected bool canLevelUp;
    protected int skillsToUp;
    protected int ultToUp;
    protected bool usedSkill;
    protected bool usedAutoAtk;
    #endregion
    #region Skills effect
    protected float temporaryAtkDamage;
   [SerializeField] protected int quantityAtk;
    protected bool buffingAtkDamage;
    protected float timeToBuffAtkSpeed;
    protected float temporaryAtkRange;
    protected bool buffAtkRange;
    protected float quantitytoBuffRange;
    protected bool addedAtkRange;
    protected bool stealLife;
    protected bool doubleDamage;
    protected float quantityToSteal;
    protected bool invisible;
    protected float timeInvisible;
    protected Color color;
    protected float temporaryAtkSpeed;
    protected bool buffingAtkSpeed;
    protected float timeBuffingAtkSpeed;
    protected float qAtkSpeed;
    protected float timeAtkSpeed;
    #endregion

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
                if (hit.transform.tag == "RedTeam" || hit.transform.tag == "NormalType")
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
            usedAutoAtk = true;
            AutoAttack();
            if (buffingAtkDamage)
            {
                if (quantityAtk == 1)
                {
                    if (stealLife)
                    {
                        StealLife(quantityToSteal);
                        Debug.Log("Habilidade 2 Roubar Vida");
                        stealLife = false;
                    }
                    if(doubleDamage)
                    {
                        Debug.Log("Habilidade 2 Dano em dobro");
                        doubleDamage = false;
                    }
                }
                quantityAtk -= 1;
            }
            atkSpeedCount = 0;
            if(quantityAtk == 1 && doubleDamage)
                CauseDoubleDamage();
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

    #region Skills effect
    public void BuffAttackDamage(int qAtk, float qBuff)
    {
        if(!buffingAtkDamage)
        {
            temporaryAtkDamage = atkDamage;
            atkDamage += atkDamage * qBuff;
            quantityAtk = qAtk;

            buffingAtkDamage = true;
        }
    }

    protected void BuffingAttackDamage()
    {
        if(buffingAtkDamage)
        {
            if(quantityAtk <= 0)
            {
                atkDamage = temporaryAtkDamage;

                stealLife = false;
                doubleDamage = false;
                buffingAtkDamage = false;
            }
        }
    }

    protected void BuffAtkRange(float q)
    {
        temporaryAtkRange = atkRange;
        atkRange += atkRange * q;
        addedAtkRange = true;
    }

    public void BoolStealLife(bool stealLife)
    {
        this.stealLife = stealLife;
    }

    protected void StealLife(float qStealLife)
    {
        Heal(atkDamage * qStealLife);
    }

    public void BoolDoubleDamage(bool doubleDamage)
    {
        this.doubleDamage = doubleDamage;
    }

    protected void CauseDoubleDamage()
    {
        atkDamage = atkDamage * 2;
    }

    public void BuffAttackSpeed(float q, float time)
    {
        temporaryAtkSpeed = atkSpeed;
        atkSpeed += atkSpeed * q;
        timeToBuffAtkSpeed = time;
        buffingAtkSpeed = true;
    }

    protected void BuffingAttackSpeed()
    {
        if(buffingAtkSpeed)
        {
            timeBuffingAtkSpeed += Time.deltaTime;
            if(timeBuffingAtkSpeed >= timeToBuffAtkSpeed)
            {
                atkSpeed = temporaryAtkSpeed;
                timeBuffingAtkSpeed = 0;
                buffingAtkSpeed = false;
            }
        }
    }

    public void BecomeInvisible(float time)
    {
        invisible = true;
        timeInvisible = time;
        /*this.buffAtkSpeed = buffAtkSpeed;
        this.qAtkSpeed = qAtkSpeed;
        this.timeAtkSpeed = timeAtkSpeed;*/
    }
    
    protected void Invisibility()
    {
        if (invisible)
        {
            timeInvisible -= Time.deltaTime;
            color.a = 0.5f;
            GetComponent<MeshRenderer>().material.color = color;

            if (timeInvisible <= 0 || usedSkill || usedAutoAtk)
            {
                timeInvisible = 0;

                if(skills[2] == GetComponent<Upgrading2_Skill3_Character1>())
                {
                    
                }

                usedSkill = false;
                usedAutoAtk = false;
                invisible = false;
            }
        }
        else
        {
            color.a = 1;
            GetComponent<MeshRenderer>().material.color = color;
        }
    }
    #endregion

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
        else if ((level.Equals(2) || level.Equals(4) || level.Equals(5) || level.Equals(7) || level.Equals(8)) && canLevelUp)
        {
            SetMaxLife(Mathf.Round(maxLife / 10));
            life += Mathf.Round(maxLife / 10);
            LevelUp();
        }
        else if((level.Equals(3) || level.Equals(6) || level.Equals(9)) && canLevelUp)
        {
            SetMaxLife(Mathf.Round(maxLife / 10));
            life += Mathf.Round(maxLife / 10);
            ultToUp += 1;
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
            if (level >= 1 && level <= 8)
            {
                GainExperienceInJungle(100);
            }
        }
    }
    #endregion

    #region Skills
    protected void UseSkills()
    {
        if(!Input.GetKey(InputManager.IM.upSkill))
        {
            if (Input.GetKeyDown(InputManager.IM.skill1))
            {
                skills[0].DoIt1(false);
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
    }

    protected void UpSkills()
    {
        if(skillsToUp > 0 && Input.GetKey(InputManager.IM.upSkill) && canUpSkill)
        {
            if (Input.GetKeyDown(InputManager.IM.skill1) && skills[0].GetLevel() < skills[0].GetMaxLevel())
            {
                skills[0].AddLevel(1);
                skillsToUp -= 1;
            }
            else if (Input.GetKeyDown(InputManager.IM.skill2) && skills[1].GetLevel() < skills[1].GetMaxLevel())
            {
                skills[1].AddLevel(1);
                skillsToUp -= 1;
            }
            else if (Input.GetKeyDown(InputManager.IM.skill3) && skills[2].GetLevel() < skills[2].GetMaxLevel() && ultToUp > 0)
            {
                skills[2].AddLevel(1);
                ultToUp -= 1;
                skillsToUp -= 1;
            }
        }
        else if(skillsToUp <= 0)
        {
            canUpSkill = false;
        }
    }

    public List<SkillsBase> GetSkills()
    {
        return skills;
    }

    public void SetUsedSkill(bool b)
    {
        usedSkill = b;
    }
    #endregion
}