using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Simple healing spell.
 */
[CreateAssetMenu (menuName = "Spells/HealSpell")]
public class HealSpell : Spell
{
    public int power = 10;
    private GameObject castBar;
    private GameObject manaBar;

    private void OnEnable()
    {
        manaBar = GameObject.FindGameObjectWithTag("ManaBar");
        castBar = GameObject.FindGameObjectWithTag("CastBar");
    }

    public override void Trigger()
    {
        Debug.Log("HealSpell.Trigger");

        if (GameManager.instance.IsCurrentCastingTargetAlive()
            && castBar.GetComponent<CastBar>().CanCast()
            && (manaBar.GetComponent<ManaBar>().manaPoints >= manaCost))
        {
            manaBar.GetComponent<ManaBar>().UseMana(manaCost);
            castBar.GetComponent<CastBar>().StartCasting(castTime);
        }
        else
        {
            GameManager.instance.currentCastingTarget = null;
            GameManager.instance.currentCastingSpell = null;
        }
    }

    public override void Behaviour()
    {
        Debug.Log("HealSpell.Behaviour");

        if(GameManager.instance.IsCurrentCastingTargetAlive())
        {
            GameObject target = GameManager.instance.currentCastingTarget;
            target.GetComponent<HealthBar>().HealDamage(power);
        } 
    }
}
