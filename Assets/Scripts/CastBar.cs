using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastBar : MonoBehaviour {

    public Image currentCastBar;
    public Text ratioText;
    public bool isCasting = false;

    private float totalCastTime = 0f;
    private float currentCastTime = 0f;

	
    public void StartCasting(float castTime)
    {
        totalCastTime = castTime;
        currentCastTime = 0f;
        isCasting = true;
    }

    public void ResetBar()
    {
        currentCastTime = 0f;
        totalCastTime = 0f;
        isCasting = false;
        UpdadeCastBar();
    }

    public bool CanCast()
    {
        return !isCasting;
    }

    private void Update()
    {
        if (isCasting)
        {
            currentCastTime += Time.deltaTime;
            if(currentCastTime >= totalCastTime)
            {
                ResetBar();
                EventManager.TriggerEvent("FinishedCast", null);
                
            } else
            {
                UpdadeCastBar();
            }
        }
    }

    private void UpdadeCastBar()
    {
        float ratio = 0f;
        if (totalCastTime > 0f)
        {
            ratio = currentCastTime / totalCastTime;
        }
        currentCastBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (totalCastTime - currentCastTime).ToString("0.0");
    }
}
