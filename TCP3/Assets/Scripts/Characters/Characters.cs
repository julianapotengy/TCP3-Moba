using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Characters : BaseScript
{
    protected float moveSpeed;
    protected float resistance;
    protected string specialAtk;
    protected string movement;

    protected NavMeshAgent agent;
    protected Vector3 targetDestination;

    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    private void Update()
    {
        Movement();
    }

    protected void Movement()
    {
        if (Input.GetKeyDown(InputManager.IM.walk))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetDestination = hit.point;
                agent.SetDestination(targetDestination);
            }
        }
    }
}
