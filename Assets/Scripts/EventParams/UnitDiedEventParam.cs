using UnityEngine;
using UnityEditor;
using System;

/**
 * IEventParam implementation.
 * Used by EventManager : UnitDied
 */
public class UnitDiedEventParam : IEventParam
{
    public GameObject unit;

    public UnitDiedEventParam(GameObject unit)
    {
        this.unit = unit;
    }
}