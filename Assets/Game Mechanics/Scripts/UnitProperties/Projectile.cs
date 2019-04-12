using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] float projectileSpeed = 1f;
    int damage;
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }

    // Moves the projectile to the right
    private void Move()
    {
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }

    // If the projectile hits a target, reduce the target's HP or kill it
    private void OnTriggerEnter2D(Collider2D target)
    {
        var targetHealth = target.GetComponent<Health>();
        var attacker = target.GetComponent<Attacker>();

        if (targetHealth && attacker)
        {
            targetHealth.DealDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
