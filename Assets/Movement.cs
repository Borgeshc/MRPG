using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 60;
    public float rotationSpeed = 100;
    public float attackMoveSpeed = 1000f;

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

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation.x,
            Mathf.Round(targetRotation.y / 45) * 45, targetRotation.z), Time.fixedDeltaTime * rotationSpeed);
        
    }
}
