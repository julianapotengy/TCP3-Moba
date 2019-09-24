using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayableCharacters : Characters
{
    protected int level;
    protected int passiveCharacter;
    protected int passiveClass;

    //skills

    protected NavMeshAgent agent;
    protected Vector3 targetDestination;

    protected void PlayableAutoAttack()
    {
        if (detector.GetCharactersOnArea().Count > 0 && Input.GetKeyDown(InputManager.IM.autoAtk))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Enemy")
                {
                    for (int i = 0; i < detector.GetCharactersOnArea().Count; i++)
                    {
                        if (detector.GetCharactersOnArea()[i].gameObject == hit.transform.gameObject)
                        {
                            target = detector.GetCharactersOnArea()[i];
                        }
                    }
                }
            }
        }
        else
        {
            if (target != null)
                target = null;
        }

        atkSpeedCount += Time.deltaTime;
        if (atkSpeedCount >= atkSpeed && target != null)
        {
            AutoAttack();
            atkSpeedCount = 0;
        }
    }

    protected void CheckAtkRange()
    {
        if (Input.GetKey(InputManager.IM.seeAtkRange))
        {
            detector.GetComponent<MeshRenderer>().enabled = true;
        }
        else detector.GetComponent<MeshRenderer>().enabled = false;
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
