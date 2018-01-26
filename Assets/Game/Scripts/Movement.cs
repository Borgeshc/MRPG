﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 60;
    public float rotationSpeed = 100;
    public float attackMoveSpeed = 1000f;

    public LayerMask targetLayer;

    public Texture2D mainCursor;
    public Texture2D attackCursor;

    public AudioSource footstepSource;
    public AudioClip[] footsteps;

    public static bool hasTarget;

    Vector3 input;
    Vector3 targetRotation;
    CharacterController cc;
    Animator anim;

    public static bool attacking;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate ()
    {
        if (attacking)
        {
            anim.SetBool("IsWalking", true);
            cc.SimpleMove(transform.forward * attackMoveSpeed * Time.deltaTime);
            return;
        }

        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (input.sqrMagnitude > 1f)
        {
            input.Normalize();
        }

        if (input != Vector3.zero)
        {

            targetRotation = Quaternion.LookRotation(input).eulerAngles;
            anim.SetBool("IsWalking", true);
            cc.SimpleMove(transform.forward * speed * Time.deltaTime);
        }
        else
            anim.SetBool("IsWalking", false);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
        {
            if(hit.transform.tag.Equals("Enemy"))
            {
                Vector3 direction = (hit.transform.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

                Cursor.SetCursor(attackCursor, Vector2.zero, CursorMode.Auto);
                hasTarget = true;
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation.x,
            Mathf.Round(targetRotation.y / 45) * 45, targetRotation.z), Time.fixedDeltaTime * rotationSpeed);


            Cursor.SetCursor(mainCursor, Vector2.zero, CursorMode.Auto);
            hasTarget = false;
        }
    }

    public void Step()
    {
        if (attacking) return;
        AudioClip footstep = footsteps[Random.Range(0, footsteps.Length)];
        footstepSource.PlayOneShot(footstep);
    }
}
