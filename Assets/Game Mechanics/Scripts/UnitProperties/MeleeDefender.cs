using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDefender : MonoBehaviour {

    AudioController audioController;

    Attacker currentTarget;
    int damage;

    // Use this for initialization
    void Start () {
        damage = GetComponent<Defender>().GetUnitDamage();
        audioController = FindObjectOfType<AudioController>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateAnimationState();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        CollisionTriggerEvent(otherCollider);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CollisionTriggerEvent(other);
    }

    private void CollisionTriggerEvent(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;

        if (otherObject.GetComponent<Attacker>() && !currentTarget)
        {
            currentTarget = otherObject.GetComponent<Attacker>();
            Attack(currentTarget);
        }
    }

    private void Attack(Attacker target)
    {
        GetComponent<Animator>().SetBool("IsAttacking", true);
    }

    private void UpdateAnimationState()
    {
        if (!currentTarget)
        {
            GetComponent<Animator>().SetBool("IsAttacking", false);
        }
        else
        {
            Attack(currentTarget);
        }
    }

    public void HitCurrentTarget()
    {
        if (!currentTarget) { return; }

        Health health = currentTarget.GetComponent<Health>();

        audioController.PlayRandomSlashSound();

        if (health)
        {
            DoDamageAfterCalculations(health);
        }
    }

    private void DoDamageAfterCalculations(Health health)
    {
        if (GetComponent<DamageEnhancer>())
        {
            int calculatedDamage = GetComponent<DamageEnhancer>().AttemptToDealMoreDamage(damage);
            health.DealDamage(calculatedDamage);
        }
        else {
            health.DealDamage(damage);
        }

        if (health.GetHealth() <= 0)
        {
            currentTarget = null;
        }
    }
}
