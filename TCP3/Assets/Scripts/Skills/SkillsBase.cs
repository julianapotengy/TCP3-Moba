using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsBase : MonoBehaviour
{
    protected string skillName;
    protected string description;
    protected Image icon;
    [SerializeField] protected Text levelTxt;
    [SerializeField] protected Text cooldownTxt;
    protected float range;
    protected int level;
    protected int maxLevel;
    protected float baseCooldown;
    protected float cooldown;
    protected float cooldownCount;
    protected float timeCount;
    protected bool canUse;
    protected float baseDamage;
    protected float damage;
    protected Transform target;
    protected int improvement1;
    protected int improvement2;
    protected int improvement3;
    protected bool improved;
    protected float duration;
    protected int status;
    protected bool hitTarget;
    protected bool dashing;
    
    protected bool choosedUpgrading;
    protected SkillsBase upgradingChoosen;
    protected PlayableCharacters parent;
    protected GameObject shieldPrefab;
    protected GameObject shield;

    protected Animator animator;

    #region O que faz
    protected void Invisibility(float timeInvisible)
    {
        gameObject.GetComponent<PlayableCharacters>().BecomeInvisible(timeInvisible);
    }

    protected void Heal(float h)
    {
        target.GetComponent<PlayableCharacters>().Heal(h);
    }

    protected void Shield(GameObject owner, float damage)
    {
        shieldPrefab = Resources.Load<GameObject>("Prefabs/Shield");
        shield = (GameObject)Instantiate(shieldPrefab, owner.transform.position, Quaternion.identity);
        Shield shieldScript = shield.GetComponent<Shield>();
        shieldScript.SetOwner(owner);
        shieldScript.SetDamageToTake(damage);
    }

    protected void SeeEnemy(GameObject target)
    {

    }

    protected void Stun()
    {

    }

    protected void Desability()
    {

    }

    protected void Root()
    {

    }

    protected void Pull()
    {

    }

    protected void ChangeSpeed(float quantity, float time, Transform t)
    {
        t.GetComponent<Characters>().BuffMoveSpeed(quantity, time);
    }

    protected void ChangeAtkSpeed(float quantity, float time, Transform t)
    {
        t.GetComponent<PlayableCharacters>().BuffAttackSpeed(quantity, time);
    }

    protected void Dash(int direction, float dashDistance, Rigidbody rb)
    {
        /*if (direction == 0)
        {
            gameObject.transform.position = gameObject.transform.position;
        }
        else if (direction == 1)
        {
            gameObject.transform.position = gameObject.transform.position;
        }
        else if (direction == 2)
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime)));
            rb.AddForce(dashVelocity, ForceMode.VelocityChange);
        }
        else if (direction == 3)
        {
            gameObject.transform.position = gameObject.transform.position;
        }*/
    }

    protected void LifeSteal(float damage)
    {
        Heal(damage * 0.15f);
    }

    protected void CauseDamage()
    {
        target.GetComponent<BaseScript>().ReceiveDamage(damage, this.gameObject);
    }

    protected void CauseDamageInTime(float t, float d)
    {
        target.GetComponent<BaseScript>().ReceiveDamage(damage, this.gameObject);
        while (timeCount <= t)
        {
            timeCount += Time.deltaTime;
            target.GetComponent<BaseScript>().ReceiveDamage(d, this.gameObject);
        }
    }
    #endregion

    #region Pegar atributos
    public float GetCooldown()
    {
        return cooldown;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetMaxLevel()
    {
        return maxLevel;
    }

    public SkillsBase GetUpgraingChoosen()
    {
        return upgradingChoosen;
    }
    #endregion

    public void AddLevel(int quantity)
    {
        level += quantity;
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    public void Hitted(bool hitted)
    {
        hitTarget = hitted;
    }

    protected void Levels(int skillNumber)
    {
        if (level == 1)
        {

        }
        else if (level == 2)
        {
            if (!choosedUpgrading)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    if (skillNumber.Equals(1))
                        gameObject.AddComponent<Upgrading1_Skill1_Character1>();
                    else if (skillNumber.Equals(2))
                        gameObject.AddComponent<Upgrading1_Skill2_Character1>();
                    else if (skillNumber.Equals(3))
                        gameObject.AddComponent<Upgrading1_Skill3_Character1>();

                    choosedUpgrading = true;
                    Destroy(this);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    if (skillNumber.Equals(1))
                        gameObject.AddComponent<Upgrading2_Skill1_Character1>();
                    else if (skillNumber.Equals(2))
                        gameObject.AddComponent<Upgrading2_Skill2_Character1>();
                    else if (skillNumber.Equals(3))
                        gameObject.AddComponent<Upgrading2_Skill3_Character1>();

                    choosedUpgrading = true;
                    Destroy(this);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    if (skillNumber.Equals(1))
                        gameObject.AddComponent<Upgrading3_Skill1_Character1>();
                    else if (skillNumber.Equals(2))
                        gameObject.AddComponent<Upgrading3_Skill2_Character1>();
                    else if (skillNumber.Equals(3))
                        gameObject.AddComponent<Upgrading3_Skill3_Character1>();

                    choosedUpgrading = true;
                    Destroy(this);
                }
            }
        }
        else if (level == 3)
        {

        }
    }

    public virtual void DoIt()
    {

    }

    public virtual void DoIt1(bool skill3)
    {

    }
}