using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Movement : MonoBehaviour
{
    public float speed = 60;
    public float rotationSpeed = 100;
    public float attackMoveSpeed = 1000f;
    public float attackDistance = 3f;

    public LayerMask targetLayer;

    public Texture2D mainCursor;
    public Texture2D attackCursor;

    public AudioSource footstepSource;
    public AudioClip[] footsteps;

    public static bool hasTarget;
    public bool isMobile;

    Vector3 input;
    Vector3 targetRotation;
    CharacterController cc;
    Animator anim;

    bool checkingForEnemies;

    public static bool attacking;

    public static List<Transform> enemies = new List<Transform>();

    Transform currentTarget;
    float currentTargetDistance;
    float distance;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(go.transform);
        }

        print("Num of enemies " + enemies.Count);

        currentTargetDistance = 100;
    }

    void FixedUpdate ()
    {
        if (attacking)
        {
            anim.SetBool("IsWalking", true);
            if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.position) <= attackDistance && attacking)
            {
                Vector3 direction = (currentTarget.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }

            cc.SimpleMove(transform.forward * attackMoveSpeed * Time.deltaTime);
            return;
        }

        input = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));
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

        if(!isMobile)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
            {
                if (hit.transform.tag.Equals("Enemy"))
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
        else
        {
            if(!checkingForEnemies)
            {
                checkingForEnemies = true;
                StartCoroutine(CheckForEnemies());
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation.x,
            Mathf.Round(targetRotation.y / 45) * 45, targetRotation.z), Time.fixedDeltaTime * rotationSpeed);
        }
    }

    Transform GetClosestEnemy(List<Transform> enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    public void Step()
    {
        if (attacking) return;
        AudioClip footstep = footsteps[Random.Range(0, footsteps.Length)];
        footstepSource.PlayOneShot(footstep);
    }

    IEnumerator CheckForEnemies()
    {
        currentTarget = GetClosestEnemy(enemies);
        yield return new WaitForSeconds(.5f);
        checkingForEnemies = false;
    }
}
