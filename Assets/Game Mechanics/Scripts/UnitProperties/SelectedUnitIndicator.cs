using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUnitIndicator : MonoBehaviour {

    Color32 defenderSelectedColor = new Color32(0, 255, 20, 255);
    Color32 attackerSelectedColor = new Color32(185, 20, 0, 255);
    Color32 indicatorColor;

    Attacker attacker;
    Defender defender;

    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        defender = gameObject.GetComponentInParent<Defender>();
        attacker = gameObject.GetComponentInParent<Attacker>();

        if (defender)
        {
            indicatorColor = defenderSelectedColor;
        }
        else if (attacker)
        {
            indicatorColor = attackerSelectedColor;
        }
	}

    public void RemoveSelectedIndicator()
    {
        spriteRenderer.enabled = false;
    }

    public void AddSelectedIndicator()
    {
        ApplyIndicatorAccordingToUnitType();
        spriteRenderer.enabled = true;
    }

    private void ApplyIndicatorAccordingToUnitType()
    {
        spriteRenderer.color = indicatorColor;
    }
}
