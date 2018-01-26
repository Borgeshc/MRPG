using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public float minAttackFrequency = 1;
    public float maxAttackFrequency = 2;
    public float pullRadius = 10f;

    public AudioSource source;
    public AudioClip[] pulledSounds;
    public AudioClip[] attackSounds;

    NavMeshAgent agent;
    
    Animator anim;
    GameObject player;
    Health health;

    Coroutine attack;
    bool attacking;
    bool pulled;

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        health = GetComponent<Health>();
	}
	
	void Update ()
    {
        if(health.isDead)
        {
            agent.isStopped = true;
            return;
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > pullRadius) return;
        else if (!pulled)
        {
            pulled = true;
            AudioClip pulledSound = pulledSounds[Random.Range(0, pulledSounds.Length)];
            source.PlayOneShot(pulledSound);
        }

        agent.SetDestination(player.transform.position);

        if(agent.velocity == Vector3.zero)
            anim.SetBool("IsWalking", false);
        else
            anim.SetBool("IsWalking", true);


        if(distance <= agent.stoppingDistance)
        {
            if(!attacking)
            {
                attacking = true;
                attack = StartCoroutine(Attack());
            }
        }
        else
        {
            if(attack != null)
            {
                StopCoroutine(attack);
                attacking = false;
            }
        }
    }

    IEnumerator Attack()
    {
        AudioClip attackSound = attackSounds[Random.Range(0, attackSounds.Length)];
        source.PlayOneShot(attackSound);

        anim.SetTrigger("Attack");
        float randomAttackFrequency = Random.Range(minAttackFrequency, maxAttackFrequency);
        yield return new WaitForSeconds(randomAttackFrequency);
        attacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}
