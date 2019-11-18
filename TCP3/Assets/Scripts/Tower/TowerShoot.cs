using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    private GameObject target;
    public float damage;
    public GameObject owner;
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 15 * Time.deltaTime);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BlueTeam"))
        {
            other.gameObject.GetComponent<PlayableCharacters>().ReceiveDamage(damage, owner);
            Destroy(this.gameObject);
        }
    }
}
