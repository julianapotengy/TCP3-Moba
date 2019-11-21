using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayableCharacters : Characters
{
    protected Text levelTxt;
    protected Text lifeText;
    protected Text atkDamageText;
    protected Text atkSpeedText;
    protected Text moveSpeedText;
    protected float respawnTimer;
    protected float respawnMax;
    public int minionsFarmed;
    public int kills;
    public int deaths;
    protected bool addedKill;
    public Transform spawnPlace;
    protected float recallTimer;
    protected float recallTime;
    protected bool recalling;
    protected bool moved;
    protected Animator animator;
    [SerializeField] protected GameObject deadScreen;
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
    protected float timeBeingSeen;
    protected bool beingSeen;
    [SerializeField] protected GameObject eyeObj;
    #endregion
    #region Sound Effects
    public AudioClip autoAtkSound;
    public AudioClip skill1Sound;
    public AudioClip skill2Sound;
    public AudioClip skill3Sound;
    public AudioClip deathSound;
    public AudioClip recallSound;
    public AudioSource audioSrc;
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
                if (hit.transform.tag != this.tag)
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
            animator.SetTrigger("Attack");
            audioSrc.PlayOneShot(autoAtkSound);
            AutoAttack();
            if (buffingAtkDamage)
            {
                if(beingSeen)
                {
                    BeingSeen();
                }
                if (quantityAtk == 1)
                {
                    if (stealLife)
                    {
                        StealLife(quantityToSteal);
                        stealLife = false;
                    }
                    if(doubleDamage)
                    {
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
                moved = true;
            }
        }
        else moved = false;

        if(this.transform.position != agent.destination)
        {
            animator.SetBool("isMoving", true);
        }
        else animator.SetBool("isMoving", false);
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
                beingSeen = false;
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
            if (GetComponent<Upgrading2_Skill3_Character1>())
            {
                GetComponent<Upgrading2_Skill3_Character1>().SetInvisible(true);
            }

            if (timeInvisible <= 0 || usedSkill || usedAutoAtk || tookDamage)
            {
                timeInvisible = 0;

                usedSkill = false;
                usedAutoAtk = false;
                tookDamage = false;
                invisible = false;
            }
        }
        else
        {
            color.a = 1;
            GetComponent<MeshRenderer>().material.color = color;
            if (GetComponent<Upgrading2_Skill3_Character1>())
            {
                GetComponent<Upgrading2_Skill3_Character1>().SetInvisible(false);
            }
        }
    }

    public void CanBeSeen(float time)
    {
        beingSeen = true;
        timeBeingSeen = time;
    }

    protected void BeingSeen()
    {
        timeBeingSeen -= Time.deltaTime;
        GameObject eye = Instantiate(eyeObj, this.gameObject.transform);
        if (timeBeingSeen <= 0)
        {
            //eye = 
        }
    }
    #endregion

    #region Experience
    public void GainExperienceInTime(float quantity)
    {
        if(level >= 1 && level <= 9)
        {
            experience += quantity * Time.deltaTime;
            if(experience >= 100)
            {
                level += 1;
                experience = 0;
            }
        }
    }

    public void GainExperienceInMinion(float quantity)
    {
        if (level >= 1 && level <= 9)
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
            life = maxLife;
            LevelUp();
        }
        else if ((level.Equals(2) || level.Equals(4) || level.Equals(5) || level.Equals(7) || level.Equals(8)) && canLevelUp)
        {
            SetMaxLife(Mathf.Round(maxLife / 10));
            life += Mathf.Round(maxLife / 10);
            atkDamage += Mathf.Round(atkDamage * 0.2f);
            temporaryAtkDamage += Mathf.Round(temporaryAtkDamage / 10);
            atkSpeed += Mathf.Round((atkSpeed * -0.15f) * 10) / 10;
            LevelUp();
        }
        else if((level.Equals(3) || level.Equals(6) || level.Equals(9)) && canLevelUp)
        {
            SetMaxLife(Mathf.Round(maxLife / 10));
            life += Mathf.Round(maxLife / 10);
            atkDamage += Mathf.Round(atkDamage * 0.2f);
            temporaryAtkDamage += Mathf.Round(temporaryAtkDamage / 10);
            atkSpeed += Mathf.Round((atkSpeed * -0.15f) * 10) / 10;
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
                GainExperienceInMinion(100);
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

    protected override void Die()
    {
        if(!addedKill)
        {
            //audioSrc.PlayOneShot(deathSound);
            deadScreen.SetActive(true);
            animator.SetBool("isDead", true);
            animator.SetTrigger("Died");
            deaths += 1;
            agent.SetDestination(this.transform.position);
            if (lastHitter != null)
            {
                lastHitter.GetComponent<PlayableCharacters>().GainExperienceInMinion(level * 2);
                lastHitter.GetComponent<PlayableCharacters>().kills += 1;
            }
            addedKill = true;
        }
        Respawn();
    }

    protected void Respawn()
    {
        respawnTimer += Time.deltaTime;
        if(respawnTimer >= respawnMax)
        {
            deadScreen.SetActive(false);
            animator.SetBool("isDead", false);
            life = maxLife;
            this.transform.position = spawnPlace.position;
            targetDestination = spawnPlace.position;
            agent.SetDestination(targetDestination);
            addedKill = false;
            respawnTimer = 0;
        }
    }

    protected void Recall()
    {
        if(Input.GetKey(InputManager.IM.recall))
        {
            recalling = true;
        }
        if(usedSkill || usedAutoAtk || moved || tookDamage)
        {
            usedSkill = false;
            usedAutoAtk = false;
            tookDamage = false;
            moved = false;
            recalling = false;
        }

        if (recalling)
        {
            agent.SetDestination(this.transform.position);
            animator.SetBool("isRecalling", true);
            recallTimer += Time.deltaTime;
            audioSrc.clip = recallSound;
            audioSrc.Play();
            if (recallTimer >= recallTime)
            {
                this.transform.position = spawnPlace.position;
                targetDestination = spawnPlace.position;
                agent.SetDestination(targetDestination);
                recallTimer = 0;
                recalling = false;
            }
        }
        else
        {
            animator.SetBool("isRecalling", false);
            audioSrc.Stop();
            audioSrc.clip = null;
        }
    }
}