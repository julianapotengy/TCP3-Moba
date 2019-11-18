using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed;
    float damage;
    Transform target;
    GameObject owner;

    private void Awake()
    {
        speed = 15;
    }

    private void Update()
    {
		transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void HitTarget()
    {
        target.GetComponent<BaseScript>().ReceiveDamage(damage, owner);
        Destroy(gameObject);
    }
	
	public void SetTarget(Transform target)
    {
        this.target = target;
    }
	
	public void SetDamage(float damage)
    {
        this.damage = damage;
    }
	
    public void SetOwner(GameObject gm)
    {
        owner = gm;
    }

	private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target.gameObject)
        {
            HitTarget();
        }
    }
}
