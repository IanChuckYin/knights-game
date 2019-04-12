using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : MonoBehaviour {

    [SerializeField] Attacker spawnUponDeathUnit;

    bool alreadySummoned;

	// Use this for initialization
	void Start () {
        alreadySummoned = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!alreadySummoned)
        {
            CheckIfDead();
        }
	}

    private void CheckIfDead()
    {
        if (GetComponent<Health>().GetHealth() <= 0)
        {
            Instantiate(spawnUponDeathUnit, transform.position, transform.rotation);
            alreadySummoned = true;
        }
    }
}
