using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTriggerEnter : MonoBehaviour
{
    public int minDamage;
    public int maxDamage;

    public AudioSource attackSource;
    public AudioClip[] hitSounds;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag.Equals("Enemy"))
        {
            int randomDamage = Random.Range(minDamage, maxDamage);
            other.transform.GetComponent<Health>().TookDamage(randomDamage);

            AudioClip hitSound = hitSounds[Random.Range(0, hitSounds.Length)];
            attackSource.PlayOneShot(hitSound);
        }
    }
}
