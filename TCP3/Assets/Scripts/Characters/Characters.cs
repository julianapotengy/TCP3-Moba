using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : BaseScript
{
    protected float moveSpeed;
    protected float resistance;
    protected string specialAtk;
    protected string movement;
    
    protected List<BaseScript> characteresOnArea = new List<BaseScript>();
    protected Detector detector;
    
    public List<BaseScript> GetCharactersOnArea()
    {
        return characteresOnArea;
    }
}
