using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public GameObject hitEffect;
    public float despawnTime = 5f;
    public Material dissolveMaterial;

    [HideInInspector]
    public bool isDead;

    int health;
    Animator anim;
    SkinnedMeshRenderer rend;

    float deathTime;
    
	void OnEnable ()
    {
        anim = GetComponent<Animator>();
        rend = GetComponentInChildren<SkinnedMeshRenderer>();
        health = maxHealth;
	}

    private void Update()
    {
        if(isDead)
        {
            rend.material.SetFloat("_Progress", Mathf.Lerp(1, 0, deathTime));
            deathTime += .2f * Time.deltaTime;
        }
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
            StartCoroutine(Died());
        }
    }

    IEnumerator Died()
    {
        anim.SetBool("Died", true);

        if (gameObject.layer == LayerMask.NameToLayer("Targetable"))
            gameObject.layer = LayerMask.NameToLayer("Default");

        if(rend && dissolveMaterial)
        {
            Material[] mats = rend.materials;
            mats[0] = dissolveMaterial;
            rend.materials = mats;

        }
            yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
