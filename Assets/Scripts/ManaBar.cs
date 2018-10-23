using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Mana bar.
 * Invoke "RegenMana"
 */
public class ManaBar : MonoBehaviour {

    public Image currentManaBar;
    public Text ratioText;
    public int manaPoints = 100;
    public int maxManaPoints = 100;
    public int manaRegenPerSecond = 1;

    private void Start () {
        InvokeRepeating("RegenMana", 1f, 1f);
        UpdadeManaBar();
    }
	
	private void UpdadeManaBar() {
        float ratio = (float)manaPoints / (float)maxManaPoints;
        currentManaBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString("0") + '%';
    }

    public void UseMana(int amount)
    {
        manaPoints -= amount;
        if (manaPoints <= 0)
        {
            manaPoints = 0;
        }
        UpdadeManaBar();
    }

    public void GainMana(int amount)
    {
        manaPoints += amount;
        if(manaPoints > maxManaPoints)
        {
            manaPoints = maxManaPoints;
        }
        UpdadeManaBar();
    }

    private void RegenMana()
    {
        manaPoints += manaRegenPerSecond;
        if(manaPoints > maxManaPoints)
        {
            manaPoints = maxManaPoints;
        }
        UpdadeManaBar();
    }
}
