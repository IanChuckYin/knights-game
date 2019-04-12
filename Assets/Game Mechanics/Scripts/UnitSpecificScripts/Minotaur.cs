using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : MonoBehaviour {

    [SerializeField] Attacker summonUnit;

    const float SPAWN_INTERVAL = 10.0f;

	// Use this for initialization
	void Start () {
        StartCoroutine(SummonUnits());	
	}

    IEnumerator SummonUnits()
    {
        while (true)
        {
            yield return new WaitForSeconds(SPAWN_INTERVAL);

            Instantiate(summonUnit, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
            Instantiate(summonUnit, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
        }
    }
}
