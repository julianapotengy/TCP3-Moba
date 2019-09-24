using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : Characters
{
    private void Awake()
    {
        this.name = "Detector_" + gameObject.transform.parent.GetComponent<Character1>().GetName();
        this.transform.localScale = new Vector3(gameObject.transform.parent.GetComponent<Character1>().GetAtkRange(), 0.0005f, gameObject.transform.parent.GetComponent<BaseScript>().GetAtkRange());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BaseScript>() != null && other.gameObject != gameObject.transform.parent.gameObject)
        {
            characteresOnArea.Add(other.gameObject.GetComponent<BaseScript>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<BaseScript>() != null && other.gameObject != gameObject.transform.parent.gameObject)
        {
            characteresOnArea.Remove(other.gameObject.GetComponent<BaseScript>());
        }
    }

}
