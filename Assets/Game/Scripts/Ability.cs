using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    public float abilityCooldown;
    public GameObject abilityEffect;
    public int numberOfAnimations;

    public enum AbilityType
    {
        None,
        PrimaryAbility,
        AbilityOne,
        AbilityTwo,
        AbilityThree,
        AbilityFour
    };

    public AbilityType abilityType;

    string activeAbility;

    bool abilityActive;
    Animator anim;

    public void Initialize(Animator _anim)
    {
        anim = _anim;
        abilityActive = false;
    }

    public void ActivateAbility()
    {
        if (!abilityActive)
        {
            abilityActive = true;
            Movement.attacking = true;

            if(abilityType == AbilityType.PrimaryAbility)
            {
                int randomAbility = Random.Range(0, numberOfAnimations);
                anim.SetTrigger(abilityType.ToString() + randomAbility);
                activeAbility = abilityType.ToString() + randomAbility;
            }
            else
            {
                anim.SetTrigger(abilityType.ToString());
                activeAbility = abilityType.ToString();
            }

            CoroutineUtility.instance.StartCoroutine(AbilityCooldown());
        }
    }

    IEnumerator AbilityCooldown()
    {
        yield return new WaitForSeconds(abilityCooldown);
        Movement.attacking = false;
        abilityActive = false;
        anim.ResetTrigger(activeAbility);
    }
}
