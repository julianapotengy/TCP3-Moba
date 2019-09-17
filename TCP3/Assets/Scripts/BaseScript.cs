using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    protected int ID;
    protected int select;
    protected string objName;
    protected float baseLife;
    protected float life;
    protected float maxLife;
    protected float atkDamage;
    protected float atkSpeed;
    protected float atkRange;
    protected float experience;
    protected bool dead;

    private void Awake()
    {
        SetMaxLife(baseLife);
        life = maxLife;
    }

    #region Life
    public float GetLife()
    {
        return life;
    }

    public void ReceiveDamage(float damage)
    {
        life -= damage;
    }

    public void Heal(float cure)
    {
        life += cure;
    }

    public void SetMaxLife(float status)
    {
        maxLife += status;
    }

    public void CheckLife()
    {
        if(life >= maxLife)
        {
            life = maxLife;
        }
    }

    public bool IsDead()
    {
        return dead;
    }

    public void Die()
    {
        if (life <= 0)
        {
            dead = true;
        }
        else dead = false;
    }
    #endregion

    #region Basic Attack
    public float GetAtkDamage()
    {
        return atkDamage;
    }

    public float GetAtkSpeed()
    {
        return atkSpeed;
    }

    public float GetAtkRange()
    {
        return atkRange;
    }
    #endregion

    public float GetExperience()
    {
        return experience;
    }

    public string GetName()
    {
        return objName;
    }
}
