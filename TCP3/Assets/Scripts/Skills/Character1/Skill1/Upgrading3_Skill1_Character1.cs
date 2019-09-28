using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrading3_Skill1_Character1 : Skill1_Character1
{
    private void Awake()
    {
        skillName = "Aprimoramento 3";
        description = "Ao jogar a granada, a personagem executa um pulo na direção inversa à granada, se afastando de uma possível ameaça.";
        level = 2;
        BasicAwake();
        projectile = Resources.Load<GameObject>("Prefabs/Characters/Projectile_Skill1_Character1");
    }

    private void Update()
    {
        BasicUpdate();
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

            ChangeSpeed(0.15f, 2, gameObject.transform);
            cooldownCount = 0;
        }
    }

    private void HitTarget()
    {
        if (hitTarget)
        {
            Debug.Log(target.name);
            CauseDamage();
            ChangeSpeed(-0.15f, 2, target);
            hitTarget = false;
        }
    }
}