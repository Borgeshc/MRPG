using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLoadout : MonoBehaviour
{
    [Header("Primary Ability")]
    public Ability primaryAbility;
    [Space, Header("Ability One")]
    public Ability abilityOne;
    [Space, Header("Ability Two")]
    public Ability abilityTwo;
    [Space, Header("Ability Three")]
    public Ability abilityThree;
    [Space, Header("Ability Four")]
    public Ability abilityFour;

    [Space, Space]
    public Animator anim;

    public bool isMobile;

    private void Start()
    {
        primaryAbility.Initialize(anim);
        abilityOne.Initialize(anim);
        abilityTwo.Initialize(anim);
        abilityThree.Initialize(anim);
        abilityFour.Initialize(anim);
    }

    void Update ()
    {
        if (isMobile) return;

		if(primaryAbility && Input.GetKeyDown(KeyCode.Mouse0))
        {
            primaryAbility.ActivateAbility();
        }

        if (abilityOne && Input.GetKeyDown(KeyCode.Alpha1))
        {
            abilityOne.ActivateAbility();
        }

        if (abilityTwo && Input.GetKeyDown(KeyCode.Alpha2))
        {
            abilityTwo.ActivateAbility();
        }

        if (abilityThree && Input.GetKeyDown(KeyCode.Alpha3))
        {
            abilityThree.ActivateAbility();
        }

        if (abilityFour && Input.GetKeyDown(KeyCode.Alpha4))
        {
            abilityFour.ActivateAbility();
        }
    }

    public void ActivateAbility(int ability)
    {
        switch(ability)
        {
            case 0:
                primaryAbility.ActivateAbility();
                break;
            case 1:
                abilityOne.ActivateAbility();
                break;
            case 2:
                abilityTwo.ActivateAbility();
                break;
            case 3:
                abilityThree.ActivateAbility();
                break;
            case 4:
                abilityFour.ActivateAbility();
                break;
        }
    }
}
