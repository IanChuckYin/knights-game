using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    float health;

    Attacker attacker;
    Defender defender;

    HealthBar healthBar;

	// Use this for initialization
	void Start () {
        attacker = GetComponent<Attacker>();
        defender = GetComponent<Defender>();

        SetHealthValue();

        healthBar = GetComponentInChildren<HealthBar>();

        healthBar.SetMaxValueForHealthBar(health);
	}

    private void SetHealthValue()
    {
        health = defender ? defender.GetUnitHealth() : attacker.GetUnitHealth();
    }

    // Deals damage to the unit
    public void DealDamage(float damage)
    {
        if (GetComponent<DamageBlocker>())
        {
            float blockedDamage = GetComponent<DamageBlocker>().AttemptToBlockDamage(damage);
            DealDamageAfterCalculations(blockedDamage);
        }
        else
        {
            DealDamageAfterCalculations(damage);
        }
    }

    private void DealDamageAfterCalculations(float damage)
    {
        health -= damage;

        healthBar.DecreaseHealthBar(damage);

        if (health <= 0)
        {
            InitializeDeath();
        }
    }

    // Kills the unit
    private void InitializeDeath()
    {
        if (attacker)
        {
            attacker.KillAttacker();
        }
        else
        {
            defender.KillDefender();
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public void RestoreToMaxHealth()
    {
        GetComponent<Defender>().SetUnitHealth((int)healthBar.GetMaxHealth());
        SetHealthValue();
        healthBar.ResetHealthBar();
    }
}
