using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        Heavy,      //2H Axes, Maces, Swords, Spears
        Quick,      //Daggers, Swords, Fists, Maces, 
        Ranged,     //Staffs, Wands, Pistols, Rifles, Bows, Crossbows, Spells
        Defensive   //Shield and Sword, Mace, Spear
    };

    public WeaponType weaponType;

    public GameObject model;
    public GameObject secondaryModel;
    public GameObject projectile;
    public GameObject projectileSpawn;
    public int damage;
    public float attackFrequency;
    public float moveSpeed;
    public float attackDistance;
    public int numberOfAnimations;
    public TrailRenderer weaponTrail;
    public TrailRenderer secondaryWeaponTrail;
    public RuntimeAnimatorController runtimeAnimator;

    Movement move;
    Animator anim;
    WeaponLoadout loadout;

    string activeAbility;

    bool abilityActive;

    public void Start()
    {
        anim = transform.root.GetComponent<Animator>();
        move = transform.root.GetComponent<Movement>();
        loadout = transform.root.GetComponent<WeaponLoadout>();
        abilityActive = false;
    }

    public void Attack()
    {
        if (!abilityActive)
        {
            abilityActive = true;
            Movement.attacking = true;

            int randomAbility = Random.Range(0, numberOfAnimations);
            anim.SetTrigger("Attack" + randomAbility);
            activeAbility = "Attack" + randomAbility;

            CoroutineUtility.instance.StartCoroutine(AbilityCooldown());
        }
    }

    IEnumerator AbilityCooldown()
    {
        yield return new WaitForSeconds(attackFrequency);
        Movement.attacking = false;
        abilityActive = false;
        anim.ResetTrigger(activeAbility);
    }

    //Set weapons to the bars
    //Test out scriptable weapon objects
    //Create weapon animations
}
