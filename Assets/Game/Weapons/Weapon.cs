using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon")]
public class Weapon : ScriptableObject
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
    public int damage;
    public float attackFrequency;
    public float moveSpeed;
    public float attackDistance;
    public int numberOfAnimations;
    public TrailRenderer weaponTrail;
    public Animator anim;

    Movement move;
    WeaponLoadout loadout;

    string activeAbility;

    bool abilityActive;

    public void Initialize(Movement _move, WeaponLoadout _loadout)
    {
        move = _move;
        loadout = _loadout;

        abilityActive = false;
    }

    public void Attack()
    {
        if (!abilityActive)
        {
            abilityActive = true;
            Movement.attacking = true;

            int randomAbility = Random.Range(0, numberOfAnimations);
            anim.SetTrigger(weaponType.ToString() + randomAbility);
            activeAbility = weaponType.ToString() + randomAbility;

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
