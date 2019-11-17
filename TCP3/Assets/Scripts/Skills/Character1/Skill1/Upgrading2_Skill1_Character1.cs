using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrading2_Skill1_Character1 : Skill1_Character1
{
    private void Awake()
    {
        skillName = "Flanco";
        description = "O efeito da granada passa a ser em área, dando dano reduzido às unidades próximas, " +
            "e aplicando o efeito de lentidão em todos os alvos na área.";
        level = 2;
        BasicAwake();
        damage = baseDamage + 15;
        projectile = Resources.Load<GameObject>("Prefabs/Characters/Projectile_Skill1_Character1");
    }

    private void Update()
    {
        BasicUpdate();
    }
    
    public override void DoIt1(bool skill3)
    {
        if (cooldownCount <= 0 && level >= 1)
        {
            GameObject fire = Instantiate<GameObject>(projectile, shootPivot.transform.position, Quaternion.identity);// PhotonNetwork.InstantiateSceneObject("tower_red_fire", firePivot.transform.position, Quaternion.identity,0,new object[0]); // instanciar no photon ou no tipo que for usar
            fire.transform.rotation = shootPivot.transform.rotation;
            fire.GetComponent<Projectile_Skill1_Character1>().SetParent(this);
            fire.GetComponent<Projectile_Skill1_Character1>().SetDamage(damage);
            fire.GetComponent<Projectile_Skill1_Character1>().SetRange(range);

            if (!skill3)
            {
                gameObject.GetComponent<PlayableCharacters>().SetUsedSkill(true);
                cooldownCount = cooldown;
            }
            else
            {
                skill3 = false;
            }
        }
    }

    protected override void HitTarget()
    {
        if (hitTarget)
        {
            target.GetComponent<Characters>().SetInControlGroup(true);
            CauseDamageInTime(2, damage / 5);
            ChangeSpeed(-0.25f, 2, target);
            hitTarget = false;
        }
    }
}
