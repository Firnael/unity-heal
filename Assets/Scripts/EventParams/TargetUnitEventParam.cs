using UnityEngine;
using UnityEditor;
using System;

/**
 * IEventParam implementation.
 * Used by EventManager : TargetUnit
 */
public class TargetUnitEventParam : IEventParam
{
    public GameObject unit;

    public TargetUnitEventParam(GameObject unit)
    {
        this.unit = unit;
    }
}