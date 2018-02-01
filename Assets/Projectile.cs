using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    [HideInInspector]
    public Transform target;

    Rigidbody rb;
    int damage;
    bool isTargeted;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float smoothTime = speed * Time.deltaTime;

        if (!isTargeted)
        {
            transform.LookAt(transform.position + (transform.forward * speed));
            rb.velocity = ((transform.position + (transform.forward * speed)) - transform.position) * smoothTime;
        }
        else
        {
            transform.LookAt(target);
            rb.velocity = (target.position - transform.position) * smoothTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Enemy"))
        {
            other.transform.GetComponent<Health>().TookDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetValues(Transform newTarget, int _damage)
    {
        isTargeted = true;
        target = newTarget;
        damage = _damage;
    }

    public void SetValues(int _damage)
    {
        damage = _damage;
    }
}
