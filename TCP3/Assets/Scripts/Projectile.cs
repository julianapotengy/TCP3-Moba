using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed;
    float damage;
    Transform target;

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
        target.GetComponent<BaseScript>().ReceiveDamage(damage);
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
	
	private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target.gameObject)
        {
            HitTarget();
        }
    }
}
