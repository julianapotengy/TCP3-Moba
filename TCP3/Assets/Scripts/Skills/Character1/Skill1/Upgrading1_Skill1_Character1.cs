using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrading1_Skill1_Character1 : Skill1_Character1
{
    private void Awake()
    {
        skillName = "Combatente";
        description = "Recebe um escudo que decai com o tempo após utilizar a habilidade.";
        level = 2;
        BasicAwake();
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
            animator.SetTrigger("Skill1");
            GameObject fire = Instantiate<GameObject>(projectile, shootPivot.transform.position, Quaternion.identity);// PhotonNetwork.InstantiateSceneObject("tower_red_fire", firePivot.transform.position, Quaternion.identity,0,new object[0]); // instanciar no photon ou no tipo que for usar
            fire.transform.rotation = shootPivot.transform.rotation;
            fire.GetComponent<Projectile_Skill1_Character1>().SetParent(this);
            fire.GetComponent<Projectile_Skill1_Character1>().SetDamage(damage);
            fire.GetComponent<Projectile_Skill1_Character1>().SetRange(range);
            Shield(this.gameObject, 15);
            shield.GetComponent<Shield>().SetDuration(10);
            shield.GetComponent<Shield>().SetShieldSkill3(false);
            gameObject.GetComponent<PlayableCharacters>().audioSrc.PlayOneShot(gameObject.GetComponent<PlayableCharacters>().skill1Sound);

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
            ChangeSpeed(-0.15f, 2, target);
            hitTarget = false;
        }
    }
}