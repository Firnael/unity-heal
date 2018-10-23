using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class HealthBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image currentHealthBar;
    public Text ratioText;
    public String unitName;
    public int hitPoints = 100;
    public int maxHitPoints = 100;

    private GameObject boss;

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        InvokeRepeating("AttackBoss", 1f, 1f);
        UpdadeHealthBar();
    }

    public void TakeDamage(int amount)
    {
        hitPoints -= amount;
        if(hitPoints <= 0)
        {
            hitPoints = 0;
            EventManager.TriggerEvent("UnitDied", new UnitDiedEventParam(gameObject));
        }
        UpdadeHealthBar();
    }

    public void HealDamage(int amount)
    {
        hitPoints += amount;
        if(hitPoints > maxHitPoints)
        {
            hitPoints = maxHitPoints;
        }
        UpdadeHealthBar();
    }

    private void UpdadeHealthBar()
    {
        float ratio = (float)hitPoints / (float)maxHitPoints;
        currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString("0") + '%';
    }

    private void AttackBoss()
    {
        Encounter bossScript = boss.GetComponent<Encounter>();
        bossScript.TakeDamage(1);
        if(bossScript.currentState.Equals(Encounter.State.DEAD))
        {
            CancelInvoke("AttackBoss");
        }
    }

    // === Inputs

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventManager.TriggerEvent("TargetUnit", new TargetUnitEventParam(gameObject));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventManager.TriggerEvent("TargetUnit", new TargetUnitEventParam(null));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PointerEventData.InputButton buttonClicked = eventData.button;
        Debug.Log("Mouse clicked on " + unitName + " with button " + buttonClicked);

        if (buttonClicked.Equals(PointerEventData.InputButton.Left))
        {
            TakeDamage(10);
        }
        if(buttonClicked.Equals(PointerEventData.InputButton.Right))
        {
            HealDamage(10);
        }
    }
}

