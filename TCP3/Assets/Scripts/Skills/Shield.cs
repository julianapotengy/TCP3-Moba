using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private GameObject owner;
    private float duration;
    private float timer;
    private float damageToTake;
    private float damageTaken;
    protected bool shieldSkill3;

    void Start()
    {
        owner.GetComponent<PlayableCharacters>().SetShieldObj(this.gameObject);
        timer = duration;
    }
    
    void Update()
    {
        timer -= Time.deltaTime;
        transform.position = owner.transform.position;
        if(timer <= 0 || damageTaken >= damageToTake)
        {
            owner.GetComponent<BaseScript>().OverDamageShield(damageTaken - damageToTake);
            shieldSkill3 = false;
            DestroyShield();
        }
    }

    void DestroyShield()
    {
        owner.GetComponent<PlayableCharacters>().SetShieldObj(null);
        Destroy (this.gameObject);
    }

    public void SetOwner(GameObject o)
    {
        owner = o;
    }

    public void SetDuration(float d)
    {
        duration = d;
    }

    public void SetDamageToTake(float dtk)
    {
        damageToTake = dtk;
    }

    public void SetDamageTaken(float dt)
    {
        damageTaken += dt;
    }

    public void SetShieldSkill3(bool s)
    {
        shieldSkill3 = s;
    }

    public bool GetShieldSkill3()
    {
        return shieldSkill3;
    }
}