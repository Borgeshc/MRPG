using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;


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
        health -= damage;
        anim.SetTrigger("Hit");

        if(health <= 0 && !isDead)
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
