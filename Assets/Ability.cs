using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    public int abilityDamage;
    public float abilityCooldown;
    public GameObject abilityEffect;
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

    bool abilityActive;
    Animator anim;

    public void Initialize(Animator _anim)
    {
        anim = _anim;
        abilityActive = false;
    }

    public void ActivateAbility()
    {
        Debug.Log(abilityType.ToString() + " " + abilityActive);
        if (!abilityActive)
        {
            abilityActive = true;
            Movement.attacking = true;
            anim.SetTrigger(abilityType.ToString());
            CoroutineUtility.instance.StartCoroutine(AbilityCooldown());
        }
    }

    IEnumerator AbilityCooldown()
    {
        yield return new WaitForSeconds(abilityCooldown);
        Movement.attacking = false;
        abilityActive = false;
    }
}
