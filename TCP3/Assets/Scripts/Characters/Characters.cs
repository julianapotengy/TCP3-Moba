using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : BaseScript
{
    protected float baseMoveSpeed;
    protected float moveSpeed;
    protected float temporaryMoveSpeed;
    protected float timeBuffingSpeed;
    protected float resistance;
    protected string specialAtk;
    protected string movement;
    
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
        moveSpeed += quantity;
        timeBuffingSpeed += Time.deltaTime;
        if (timeBuffingSpeed <= time)
        {
            moveSpeed = temporaryMoveSpeed;
        }
    }

    public void AddMoveSpeed(float quantity)
    {
        moveSpeed += quantity;
        temporaryMoveSpeed = moveSpeed;
    }
}
