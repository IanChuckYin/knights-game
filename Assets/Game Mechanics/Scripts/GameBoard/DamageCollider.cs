using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour {

    Attacker attacker;
    [SerializeField] PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        attacker = collision.GetComponent<Attacker>();

        if (attacker)
        {
            attacker.SetAttackerBounty(0);
            playerController.RemovePlayerLives(1);
            Destroy(collision.gameObject);
        }
    }
}
