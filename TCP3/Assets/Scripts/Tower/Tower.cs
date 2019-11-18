
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Tower : Structures
{

    // public GameObject firePivot;
    // public ParticleSystem ShootPivot; // usar depois
    // public GameObject shootPivot;
    
    void Start()
    {
        maxLife = 100;
        life = maxLife;
        SetMaxLife(maxLife);
        atkSpeed = 5;
        atkRange = 25;
        atkDamage = 10;
        atkSpeedCount = 0;
       // this.GetComponentInChildren<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayableCharacters>() != null && other.gameObject.tag == "BlueTeam")
        {
            characteresOnArea.Add(other.gameObject.GetComponent<PlayableCharacters>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayableCharacters>() != null && other.gameObject.tag == "BlueTeam")
        {
            characteresOnArea.Remove(other.gameObject.GetComponent<PlayableCharacters>());
        }
    }
    
    void Update()
    {
        Priority();
    }

    public void Attack()
    {
        //shootPivot.play(); // usar quando tiver particulas
        GameObject fire = Instantiate<GameObject>(firePivot, shootPivot.transform.position, Quaternion.identity, shootPivot.transform);// PhotonNetwork.InstantiateSceneObject("tower_red_fire", firePivot.transform.position, Quaternion.identity,0,new object[0]); // instanciar no photon ou no tipo que for usar
        fire.GetComponent<TowerShoot>().SetDamage(atkDamage);
        fire.GetComponent<TowerShoot>().SetTarget(selected.gameObject);
        fire.GetComponent<TowerShoot>().owner = this.gameObject;
    }

    public void Priority()
    {
        if (characteresOnArea.Count > 0)
        {
            selected = characteresOnArea[0];
        }
        else
        {
            if (selected != null)
                selected = null;
        }
        atkSpeedCount += Time.deltaTime;

        if (atkSpeedCount > atkSpeed && selected != null)
        {
            Attack();
            atkSpeedCount = 0;
        }
    }
}
