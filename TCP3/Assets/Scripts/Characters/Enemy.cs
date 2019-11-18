using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters
{
    private void Awake()
    {
        life = 100;
        maxLife = 100;
        moveSpeed = 3;
        hasShield = false;
    }

    private void Update()
    {
        //Debug.Log("Vida: " + life + " " + name);
        //Debug.Log("Vel mov: " + moveSpeed + " " + name);
        TimeBuffingSpeed();
        CheckLife();
    }

    protected override void Die()
    {
        if(lastHitter != null)
        {
            lastHitter.GetComponent<PlayableCharacters>().GainExperienceInMinion(7);
            lastHitter.GetComponent<PlayableCharacters>().minionsFarmed += 1;
        }
        Destroy(this.gameObject);
    }
}
