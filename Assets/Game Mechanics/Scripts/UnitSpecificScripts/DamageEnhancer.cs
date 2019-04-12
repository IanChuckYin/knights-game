using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnhancer : MonoBehaviour {

    [SerializeField] float percentChanceToDealMoreDamage;
    [SerializeField] int extraDamage;

    bool isEnhanced;

    public int AttemptToDealMoreDamage(int damage)
    {
        float randomNumber = Random.Range(0, 100);
        isEnhanced = randomNumber <= percentChanceToDealMoreDamage;

        return isEnhanced ? damage + extraDamage : damage;
    }
}
