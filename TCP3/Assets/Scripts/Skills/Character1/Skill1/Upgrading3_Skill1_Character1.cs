using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrading3_Skill1_Character1 : Skill1_Character1
{
    protected Transform initialTarget;

    private void Awake()
    {
        skillName = "Controlador";
        description = "O efeito da granada passa a ser em área a partir do alvo inicial.";
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
            initialTarget = target;
            //Debug.Log("inicial: " + initialTarget.name);
            Collider[] charactersOnArea = Physics.OverlapSphere(initialTarget.position, 10);
            foreach (Collider c in charactersOnArea)
            {
                //Debug.Log(c.name);
                if(c.GetComponent<Characters>() && c.tag != this.tag)
                {
                    target = c.transform;
                    //Debug.Log(target.name + " target");
                    target.GetComponent<Characters>().SetInControlGroup(true);
                    CauseDamage();
                    ChangeSpeed(-0.15f, 2, c.transform);
                }
            }
            hitTarget = false;
        }
    }
}