using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoadout : MonoBehaviour
{
    [Header("Primary Weapon")]
    public Weapon primaryWeapon;
    [Space, Header("Heavy Slot")]
    public Weapon heavySlot;            //Slot 1
    [Space, Header("Quick Slot")]
    public Weapon quickSlot;            //Slot 2
    [Space, Header("Ranged Slot")]
    public Weapon rangedSlot;           //Slot 3
    [Space, Header("Defensive Slot")]
    public Weapon defensiveSlot;        //Slot 4

    [Space, Space]
    public Animator anim;
    public Collider damageCollider;
    public TrailRenderer weaponTrail;

    public AudioSource attackSource;
    public AudioClip[] missSounds;

    Movement movement;

    Weapon currentWeapon;

    private void Start()
    {
        movement = GetComponent<Movement>();
        
        heavySlot.Initialize(movement, this);
        quickSlot.Initialize(movement, this);
        rangedSlot.Initialize(movement, this);
        defensiveSlot.Initialize(movement, this);
    }

    public void SetWeapon(int slotNumber)
    {
        switch(slotNumber)
        {
            case 1:
                currentWeapon = heavySlot;
                break;
            case 2:
                currentWeapon = quickSlot;
                break;
            case 3:
                currentWeapon = rangedSlot;
                break;
            case 4:
                currentWeapon = defensiveSlot;
                break;
        }

        SetWeaponValues();
        movement.SetWeaponValues(currentWeapon);
    }

    void SetWeaponValues()
    {
        anim = currentWeapon.anim;
        weaponTrail = currentWeapon.weaponTrail;
    }

    public void Attack()
    {
        currentWeapon.Attack();
    }

    public void DealDamage()
    {
        damageCollider.enabled = true;

        if(!Movement.hasTarget)
        {
            AudioClip missSound = missSounds[Random.Range(0, missSounds.Length)];
            attackSource.PlayOneShot(missSound);
        }
    }

    public void StopDealingDamage()
    {
        damageCollider.enabled = false;
    }

    public void ActivateTrail()
    {
        weaponTrail.enabled = true;
    }

    public void DeactivateTrail()
    {
        weaponTrail.enabled = false;
    }
}
