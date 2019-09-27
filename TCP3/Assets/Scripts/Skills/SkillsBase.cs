﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsBase : MonoBehaviour
{
    protected string skillName;
    protected string description;
    protected Image icon;
    protected float range;
    protected int level;
    protected float baseCooldown;
    protected float cooldown;
    protected float cooldownCount;
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

    #region O que faz
    protected void Invisibility()
    {

    }

    protected void Heal()
    {

    }

    protected void Shield()
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

    protected void ChangeSpeed(float quantity, float time)
    {
        target.GetComponent<Characters>().BuffMoveSpeed(quantity, time);
    }

    protected void Dash()
    {

    }

    protected void LifeSteal()
    {

    }

    protected void CauseDamage()
    {
        target.GetComponent<BaseScript>().ReceiveDamage(damage);
    }
    #endregion

    public float GetCooldown()
    {
        return cooldown;
    }

    public int GetLevel()
    {
        return level;
    }

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

    public virtual void DoIt()
    {

    }
}
