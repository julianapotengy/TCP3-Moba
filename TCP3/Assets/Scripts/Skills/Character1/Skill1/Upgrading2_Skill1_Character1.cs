using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrading2_Skill1_Character1 : Skill1_Character1
{
    private void Awake()
    {
        skillName = "Aprimoramento 2";
        description = "O efeito da granada passa a ser em área, dando dano reduzido às unidades próximas, " +
            "e aplicando o efeito de lentidão em todos os alvos na área.";
        range = 5;
        level = 2;
        maxLevel = 3;
        baseCooldown = 7;
        cooldown = baseCooldown;
        cooldownCount = cooldown;
        baseDamage = 30;
        damage = baseDamage;
        canUse = true;
        choosedUpgrading = true;

        levelTxt = GameObject.Find("Skill1TextLevel").GetComponent<Text>();
        cooldownTxt = GameObject.Find("Skill1TextCD").GetComponent<Text>();
        shootPivot = GameObject.Find("Character");
        projectile = Resources.Load<GameObject>("Prefabs/Characters/Projectile_Skill1_Character1");
    }

    private void Update()
    {
        cooldownCount += Time.deltaTime;
        HitTarget();
        Levels();

        levelTxt.text = "Level: " + level;
        if (cooldownCount <= cooldown)
        {
            cooldownTxt.text = "CD: " + Mathf.Round(cooldownCount) + "s";
        }
        else cooldownTxt.text = "CD: " + Mathf.Round(cooldown) + "s";
    }

    public override void DoIt()
    {
        if (cooldownCount >= cooldown && level >= 1)
        {
            GameObject fire = Instantiate<GameObject>(projectile, shootPivot.transform.position, Quaternion.identity);// PhotonNetwork.InstantiateSceneObject("tower_red_fire", firePivot.transform.position, Quaternion.identity,0,new object[0]); // instanciar no photon ou no tipo que for usar
            fire.transform.rotation = shootPivot.transform.rotation;
            fire.GetComponent<Projectile_Skill1_Character1>().SetParent(this);
            fire.GetComponent<Projectile_Skill1_Character1>().SetDamage(damage);
            fire.GetComponent<Projectile_Skill1_Character1>().SetRange(range);

            cooldownCount = 0;
        }
    }

    private void HitTarget()
    {
        if (hitTarget)
        {
            CauseDamageInTime(2, damage / 5);
            ChangeSpeed(-10, 2);
            hitTarget = false;
        }
    }
}
