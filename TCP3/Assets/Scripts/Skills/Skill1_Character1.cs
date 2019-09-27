﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1_Character1 : SkillsBase
{
    protected Vector3 direction;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject shootPivot;

    private void Awake()
    {
        skillName = "Skill 1";
        description = "Granada de pulso elétrico. A personagem joga uma granada, que estoura no primeiro alvo que acertar, " +
            "causando dano e aplicando efeito de lentidão no alvo por 2s.";
        range = 5;
        level = 0;
        baseCooldown = 7;
        cooldown = baseCooldown;
        cooldownCount = cooldown;
        baseDamage = 30;
        damage = baseDamage;
        canUse = true;

        shootPivot = GameObject.Find("Character");
    }

    private void Update()
    {
        cooldownCount += Time.deltaTime;
        HitTarget();
    }

    public override void DoIt()
    {
        if (cooldownCount >= cooldown && level >= 1)
        {
            GameObject fire = Instantiate<GameObject>(projectile, shootPivot.transform.position, Quaternion.identity);// PhotonNetwork.InstantiateSceneObject("tower_red_fire", firePivot.transform.position, Quaternion.identity,0,new object[0]); // instanciar no photon ou no tipo que for usar
            fire.transform.rotation = shootPivot.transform.rotation;
            fire.GetComponent<Projectile_Skill1_Character1>().SetParent(gameObject.GetComponent<Skill1_Character1>());
            fire.GetComponent<Projectile_Skill1_Character1>().SetDamage(damage);
            fire.GetComponent<Projectile_Skill1_Character1>().SetRange(range);

            cooldownCount = 0;
        }
    }

    private void HitTarget()
    {
        if(hitTarget)
        {
            CauseDamage();
            ChangeSpeed(-10, 2);
        }
    }
}
