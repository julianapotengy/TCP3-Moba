using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character1 : PlayableCharacters
{
	private void Awake()
    {
        objName = "Character1";
        baseLife = 200;
        atkDamage = 10;
        atkRange = 10;
        atkSpeed = 2;
        moveSpeed = 8;
		atkSpeedCount = 2;
        Debug.Log("Vida: " + GetLife() + "; Nome: " + GetName());

        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    private void Start()
    {
        detector = GameObject.Find("Detector_" + GetName()).GetComponent<Detector>();
    }

    private void Update()
    {
        Movement();
        PlayableAutoAttack();
        CheckAtkRange();
    }
}
