using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour {

    public Spell healSpell;
    public Spell flashHealSpell;
    private GameObject castBar;
    private GameObject manaBar;

    private void Start()
    {
        castBar = GameObject.FindGameObjectWithTag("CastBar");
        manaBar = GameObject.FindGameObjectWithTag("ManaBar");
    }

    private void OnEnable()
    {
        EventManager.StartListening("CancelCast", CancelCast);
        EventManager.StartListening("FinishedCast", FinishedCast);
        EventManager.StartListening("UnitDied", UnitDied);
        EventManager.StartListening("TargetUnit", TargetUnit);

        EventManager.StartListening("Heal", Heal);
        EventManager.StartListening("FlashHeal", FlashHeal);
    }

    private void OnDisable()
    {
        EventManager.StopListening("CancelCast", CancelCast);
        EventManager.StopListening("FinishedCast", FinishedCast);
        EventManager.StopListening("UnitDied", UnitDied);
        EventManager.StopListening("TargetUnit", TargetUnit);

        EventManager.StopListening("Heal", Heal);
        EventManager.StopListening("FlashHeal", FlashHeal);
    }

    void CancelCast(IEventParam parameters)
    {
        castBar.GetComponent<CastBar>().ResetBar();

        Spell spell = GameManager.instance.currentCastingSpell;
        if (spell != null)
        {
            manaBar.GetComponent<ManaBar>().GainMana(spell.manaCost);
            GameManager.instance.currentCastingSpell = null;
        }
    }

    void FinishedCast(IEventParam parameters)
    {
        Debug.Log("FinishedCast was triggered.");
        GameManager.instance.currentCastingSpell.Behaviour();
        GameManager.instance.currentCastingSpell = null;
        GameManager.instance.currentCastingTarget = null;

        if (GameManager.instance.precastedSpell != null)
        {
            TriggerSpell(GameManager.instance.precastedSpell);
        }
    }

    void UnitDied(IEventParam parameters)
    {
        UnitDiedEventParam eventParams = (UnitDiedEventParam)parameters;
        GameObject unit = eventParams.unit;
        Debug.Log("Unit " + unit.GetComponent<HealthBar>().unitName + " died.");

        if (!GameManager.instance.IsCurrentCastingTargetAlive())
        {
            CancelCast(null);
        }
    }

    void TargetUnit(IEventParam parameters)
    {
        TargetUnitEventParam eventParams = (TargetUnitEventParam)parameters;
        GameManager.instance.currentTarget = eventParams.unit;
        Debug.Log("Target unit : " + eventParams.unit);
    }

    void Heal(IEventParam parameters)
    {
        TriggerSpell(healSpell);
    }

    void FlashHeal(IEventParam parameters)
    {
        TriggerSpell(flashHealSpell);
    }

    void TriggerSpell(Spell spell)
    {
        if (GameManager.instance.currentTarget != null || GameManager.instance.precastedTarget != null) {
            if (GameManager.instance.currentCastingSpell != null)
            {
                Debug.Log(spell.spellName + " was PRECAST.");
                GameManager.instance.precastedSpell = spell;
                GameManager.instance.precastedTarget = GameManager.instance.currentTarget;
            }
            else
            {
                Debug.Log(spell.spellName + " was CAST.");

                if (GameManager.instance.precastedTarget != null)
                {
                    GameManager.instance.currentCastingTarget = GameManager.instance.precastedTarget;
                    GameManager.instance.precastedTarget = null;
                    GameManager.instance.precastedSpell = null;
                }
                else
                {
                    GameManager.instance.currentCastingTarget = GameManager.instance.currentTarget;
                }
                GameManager.instance.currentCastingSpell = spell;
                GameManager.instance.currentCastingSpell.Trigger();
            }
        }
    }
}
