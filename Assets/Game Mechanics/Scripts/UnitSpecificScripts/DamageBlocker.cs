using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlocker : MonoBehaviour {

    [SerializeField] float percentChanceToBlock;
    [SerializeField] int damageBlocked;

    bool isBlocked; 

	public float AttemptToBlockDamage(float damage)
    {
        float randomNumber = Random.Range(0, 100);
        isBlocked = randomNumber <= percentChanceToBlock;

        if (isBlocked)
        {
            float calculatedDamage = damage - damageBlocked;

            if (calculatedDamage < 0)
            {
                calculatedDamage = 1;
            }

            return calculatedDamage;
        }
        else
        {
            return damage;
        }
    }
}
