using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public GameObject hitEffect;

    [HideInInspector]
    public bool isDead;

    int health;
    Animator anim;
    
	void OnEnable ()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
	}
	
	public void TookDamage(int damage)
    {
        if (isDead) return;

        health -= damage;

        if (hitEffect)
            hitEffect.SetActive(true);

        anim.SetTrigger("Hit");

        if (health <= 0 && !isDead)
        {
            isDead = true;
            Died();
        }
    }

    void Died()
    {
        anim.SetBool("Died", true);
    }
}
