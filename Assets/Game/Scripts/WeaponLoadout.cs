using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoadout : MonoBehaviour
{
    [Header("Current Weapon")]
    public Weapon currentWeapon;
    [Space, Header("Heavy Slot")]
    public Weapon heavySlot;            //Slot 1
    [Space, Header("Quick Slot")]
    public Weapon quickSlot;            //Slot 2
    [Space, Header("Ranged Slot")]
    public Weapon rangedSlot;           //Slot 3
    [Space, Header("Defensive Slot")]
    public Weapon defensiveSlot;        //Slot 4

    [Space, Space]
    public Collider heavyDamageCollider;
    public Collider quickDamageCollider;
    public Collider defensiveDamageCollider;

    public AudioSource attackSource;
    public AudioClip[] missSounds;

    TrailRenderer weaponTrail;
    Movement movement;
    Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<Movement>();

        SetWeaponValues();
        movement.SetWeaponValues(currentWeapon);
    }

    public void SetWeapon(int slotNumber)
    {

        currentWeapon.model.SetActive(false);

        if (currentWeapon.secondaryModel)
            currentWeapon.secondaryModel.SetActive(false);

        switch (slotNumber)
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

        currentWeapon.model.SetActive(true);

        if (currentWeapon.secondaryModel)
            currentWeapon.secondaryModel.SetActive(true);

        SetWeaponValues();
        movement.SetWeaponValues(currentWeapon);
    }

    void SetWeaponValues()
    {
        anim.runtimeAnimatorController = currentWeapon.runtimeAnimator;
        weaponTrail = currentWeapon.weaponTrail;
    }

    public void Attack()
    {
        currentWeapon.Attack();
    }

    public void DealDamage()
    {
        switch (currentWeapon.weaponType)
        {
            case Weapon.WeaponType.Heavy:
                heavyDamageCollider.enabled = true;
                break;
            case Weapon.WeaponType.Quick:
                quickDamageCollider.enabled = true;
                break;
            case Weapon.WeaponType.Defensive:
                defensiveDamageCollider.enabled = true;
                break;
        }


        if(!Movement.hasTarget)
        {
            AudioClip missSound = missSounds[Random.Range(0, missSounds.Length)];
            attackSource.PlayOneShot(missSound);
        }
    }

    public void StopDealingDamage()
    {
        heavyDamageCollider.enabled = false;
        quickDamageCollider.enabled = false;
        defensiveDamageCollider.enabled = false;
    }

    public void ActivateTrail()
    {
        weaponTrail.enabled = true;

        if (currentWeapon.secondaryWeaponTrail)
            weaponTrail.enabled = true;
    }

    public void DeactivateTrail()
    {
        weaponTrail.enabled = false;

        if (currentWeapon.secondaryWeaponTrail)
            weaponTrail.enabled = false;
    }

    public void SpawnProjectile()
    {
        if(movement.currentTarget)
        {
            GameObject projectile = Instantiate(currentWeapon.projectile, currentWeapon.projectileSpawn.transform.position, currentWeapon.projectileSpawn.transform.rotation);

            if (movement.currentTarget != null)
                projectile.GetComponent<Projectile>().SetValues(movement.currentTarget, currentWeapon.damage);
            else
                projectile.GetComponent<Projectile>().SetValues(currentWeapon.damage);
        }
    }
}
