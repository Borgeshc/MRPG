using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTriggerEnter : MonoBehaviour
{
    public int minDamage;
    public int maxDamage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag.Equals("Enemy"))
        {
            int randomDamage = Random.Range(minDamage, maxDamage);
            other.transform.GetComponent<Health>().TookDamage(randomDamage);
        }
    }
}
