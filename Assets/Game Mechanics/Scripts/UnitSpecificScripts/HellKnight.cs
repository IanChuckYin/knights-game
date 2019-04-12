using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellKnight : MonoBehaviour {

    [SerializeField] Attacker spawnUponDeathUnit;

    bool alreadySummoned;

    // Use this for initialization
    void Start () {
        alreadySummoned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!alreadySummoned)
        {
            CheckIfDead();
        }
    }

    private void CheckIfDead()
    {
        if (GetComponent<Health>().GetHealth() <= 0)
        {
            Instantiate(spawnUponDeathUnit, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
            Instantiate(spawnUponDeathUnit, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
            alreadySummoned = true;
        }
    }
}
