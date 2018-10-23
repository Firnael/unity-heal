using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {
	
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.TriggerEvent("CancelCast", null);
        }

		if(Input.GetKeyDown("1"))
        {
            EventManager.TriggerEvent("Heal", null);
        }

        if (Input.GetKeyDown("2"))
        {
            EventManager.TriggerEvent("FlashHeal", null);
        }
    }
}
