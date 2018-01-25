﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public float minAttackFrequency = 1;
    public float maxAttackFrequency = 2;

    NavMeshAgent agent;
    
    Animator anim;
    GameObject player;
    Health health;

    Coroutine attack;
    bool attacking;

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
        agent.SetDestination(player.transform.position);

        if(agent.velocity == Vector3.zero)
            anim.SetBool("IsWalking", false);
        else
            anim.SetBool("IsWalking", true);

        float distance = Vector3.Distance(transform.position, player.transform.position);

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
        anim.SetTrigger("Attack");
        float randomAttackFrequency = Random.Range(minAttackFrequency, maxAttackFrequency);
        yield return new WaitForSeconds(randomAttackFrequency);
        attacking = false;
    }
}
