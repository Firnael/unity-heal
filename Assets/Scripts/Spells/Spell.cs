using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Abstract Spell class.
 * Mother-class for all spells.
 */
public abstract class Spell : ScriptableObject
{
    public string spellName = "Spell Name";
    public float castTime = 0f;
    public int manaCost = 0;

    /**
     * Trigger the spell.
     * Defines what happens when the player press the spell input.
     */
    public abstract void Trigger();

    /**
     * Apply spell behaviour.
     * Defines what happens when the spell is cast successfully.
     */
    public abstract void Behaviour();
}
