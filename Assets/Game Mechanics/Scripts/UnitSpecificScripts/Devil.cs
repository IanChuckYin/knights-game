using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour {

    [SerializeField] Attacker summonUnit;

    public void SummonUnit()
    {
        Instantiate(summonUnit, transform.position, transform.rotation);
    }
}
