using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    Slider slider;
    float maxHealth;

	// Use this for initialization
	void Awake () {
        slider = GetComponent<Slider>();
        slider.value = 0;
        slider.enabled = false;
	}

    public void SetMaxValueForHealthBar(float maxHealth)
    {
        slider.maxValue = maxHealth;
        this.maxHealth = maxHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void DecreaseHealthBar(float damage)
    {
        slider.value += damage;
    }

    public void ResetHealthBar()
    {
        slider.value = 0;
    }
	
}
