using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject currentTarget;
    public GameObject currentCastingTarget;
    public GameObject precastedTarget;
    public Spell currentCastingSpell;
    public Spell precastedSpell;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Init();
    }

    void Init () {
        // nothing yet
    }

    public bool IsCurrentTargetAlive()
    {
        return IsUnitAlive(currentTarget);
    }

    public bool IsCurrentCastingTargetAlive()
    {
        return IsUnitAlive(currentCastingTarget);
    }

    public bool IsUnitAlive(GameObject unit)
    {
        if(unit != null)
        {
            HealthBar script = unit.GetComponent<HealthBar>();
            if (script.hitPoints > 0)
            {
                return true;
            }
        }
        return false;
    }
}
