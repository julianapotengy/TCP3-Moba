using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : BaseScript
{
    protected float baseMoveSpeed;
    protected float moveSpeed;
    protected float temporaryMoveSpeed;
    protected float timeBuffingSpeed;
    protected float timeToBuffSpeed;
    protected bool buffingSpeed;
    protected float resistance;
    protected string specialAtk;
    protected string movement;
    protected bool inControlGroup;
    
    protected List<BaseScript> characteresOnArea = new List<BaseScript>();
    protected Detector detector;
    
    public List<BaseScript> GetCharactersOnArea()
    {
        return characteresOnArea;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void BuffMoveSpeed(float quantity, float time)
    {
        temporaryMoveSpeed = moveSpeed;
        moveSpeed += moveSpeed * quantity;
        timeToBuffSpeed = time;
        buffingSpeed = true;
    }

    protected void TimeBuffingSpeed()
    {
        if(buffingSpeed)
        {
            timeBuffingSpeed += Time.deltaTime;
            if (inControlGroup)
            {
                inControlGroup = true;
            }
            if (timeBuffingSpeed >= timeToBuffSpeed)
            {
                moveSpeed = temporaryMoveSpeed;
                timeBuffingSpeed = 0;
                if (inControlGroup)
                {
                    inControlGroup = false;
                }
                buffingSpeed = false;
            }
        }
    }

    public void AddMoveSpeed(float q)
    {
        moveSpeed += q;
        temporaryMoveSpeed += q;
    }

    public void SetInControlGroup(bool cg)
    {
        inControlGroup = cg;
    }

    public bool GetInControlGroup()
    {
        return inControlGroup;
    }
}