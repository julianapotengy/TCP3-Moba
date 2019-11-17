using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    protected int ID;
    protected string objName;
    protected float baseLife;
    protected float life;
    protected float maxLife;
    protected BaseScript target;
    protected float atkDamage;
    protected float atkSpeed;
	protected float atkSpeedCount;
    protected float atkRange;
    protected float experience;
    protected bool dead;
    public GameObject firePivot;
    //public ParticleSystem ShootPivot; // usar depois
    public GameObject shootPivot;
    protected bool hasShield;
    protected GameObject shieldObj;
    protected bool tookDamage;

    #region Life
    public float GetLife()
    {
        return life;
    }

    public void ReceiveDamage(float damage)
    {
        if (hasShield)
        {
            shieldObj.GetComponent<Shield>().SetDamageTaken(damage);
            if (shieldObj.GetComponent<Shield>().GetShieldSkill3() == false)
            {
                tookDamage = true;
            }
        }
        else
        {
            life -= damage;
            tookDamage = true;
        }
    }

    public void OverDamageShield(float damage)
    {
        life -= damage;
        tookDamage = true;
    }

    public void Heal(float cure)
    {
        life += cure;
    }

    public void SetMaxLife(float status)
    {
        maxLife += status;
    }

    protected void CheckLife()
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

    public bool GetHasShield()
    {
        return hasShield;
    }

    public void SetShieldObj(GameObject s)
    {
        shieldObj = s;
    }
    #endregion

    #region Basic Attack
    public Transform GetTarget()
    {
        return target.gameObject.transform;
    }

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
    
   public void AutoAttack()
    {
        //shootPivot.play(); // usar quando tiver particulas
        GameObject fire = Instantiate<GameObject>(firePivot, shootPivot.transform.position, Quaternion.identity);// PhotonNetwork.InstantiateSceneObject("tower_red_fire", firePivot.transform.position, Quaternion.identity,0,new object[0]); // instanciar no photon ou no tipo que for usar
        if (target.GetComponent<Characters>().GetInControlGroup())
        {
            atkDamage = atkDamage * 0.1f;
        }
        fire.GetComponent<Projectile>().SetDamage(atkDamage);
        fire.GetComponent<Projectile>().SetTarget(GetTarget());
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
