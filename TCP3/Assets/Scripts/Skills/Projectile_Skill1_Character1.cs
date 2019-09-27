using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Skill1_Character1 : MonoBehaviour
{
    float speed;
    float damage;
    Transform target;
    Vector3 direction;
    Skill1_Character1 parent;
    float range;
    float startPosZ;

    private void Awake()
    {
        speed = 300;
        startPosZ = gameObject.transform.position.z;
    }

    private void FixedUpdate()
    {
        if(startPosZ >= 0)
        {
            if (gameObject.transform.position.z <= startPosZ + range)
            {
                GetComponent<Rigidbody>().velocity = transform.forward * speed * Time.deltaTime;
            }
            else Destroy(this.gameObject);
        }
    }

    public void SetParent(Skill1_Character1 p)
    {
        parent = p;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetRange(float r)
    {
        range = r;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    public Transform GetTarget()
    {
        return target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("RedTeam") && other.GetComponent<Characters>() != null)
        {
            target = other.gameObject.transform;
            parent.SetTarget(GetTarget());
            parent.Hitted(true);
            Destroy(gameObject);
        }
    }
}
