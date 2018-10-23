using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Encounter.
 * Invoke "AttackRandomUnit"
 */
public class Encounter : MonoBehaviour {

    public Image currentHealthBar;
    public Text ratioText;
    public State currentState = State.ALIVE;
    public int hitPoints = 100;
    public int maxHitPoints = 100;
    public int autoAttackDamages = 10;
    public float attackSpeed = 1f;

    private GameObject[] units;


    private void Start() {
        units = GameObject.FindGameObjectsWithTag("Unit");
        InvokeRepeating("AttackRandomUnit", 1f, attackSpeed);
        UpdadeHealthBar();
    }

    private void Update()
    {
        // nothing yet
    }

    private void UpdadeHealthBar()
    {
        float ratio = (float)hitPoints / (float)maxHitPoints;
        currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString("0") + '%';
    }

    private void AttackRandomUnit() {
        int index = Random.Range(0, units.Length);
        HealthBar script = units[index].GetComponent<HealthBar>();
        script.TakeDamage(autoAttackDamages);
        Debug.Log("Attacking unit " + script.unitName + " (index=" + index + ")");
    }

    public void TakeDamage(int amount)
    {
        hitPoints -= amount;
        if (hitPoints <= 0)
        {
            hitPoints = 0;
            currentState = State.DEAD;
            CancelInvoke("AttackRandomUnit");
        } 
        UpdadeHealthBar();
    }

    public enum State {
        ALIVE,
        DEAD
    }
}
