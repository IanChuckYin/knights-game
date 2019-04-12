using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] Projectile projectile;
    [SerializeField] GameObject gun;

    int projectileDamage;

    AttackerSpawner myLaneSpawner;
    Defender defender;

    Animator animator;

    WaveController waveController;
    AudioController audioController;

    // Use this for initialization
    void Start()
    {
        waveController = FindObjectOfType<WaveController>();
        audioController = FindObjectOfType<AudioController>();

        animator = GetComponent<Animator>();
        defender = GetComponent<Defender>();

        projectileDamage = defender.GetUnitDamage();
    }

   
    // Update is called once per frame
    void Update()
    {
        UpdateAnimationStatus();
    }

    private void UpdateAnimationStatus()
    {
        if (IsAttackerInLane())
        {
            StartShooting();
        }
        else
        {
            StopShooting();
        }
    }

    private void StartShooting()
    {
        animator.SetBool("IsAttacking", true);
    }

    private void StopShooting()
    {
        animator.SetBool("IsAttacking", false);
    }

    private void CheckLaneSpawner()
    {
        if (!myLaneSpawner)
        {
            SetLaneSpawner();
        }
    }

    // Iterate through all Spawners to determine which spawner is in this Shooter's lane
    private void SetLaneSpawner()
    {
        AttackerSpawner[] attackerSpawners = FindObjectsOfType<AttackerSpawner>();

        if (attackerSpawners.Length != 0)
        {
            foreach (AttackerSpawner spawner in attackerSpawners)
            {
                bool isCloseEnough = (Mathf.Abs(spawner.transform.position.y - transform.position.y) <= Mathf.Epsilon);

                if (isCloseEnough)
                {
                    myLaneSpawner = spawner;
                }
            }
        }
    }

    // Check if there are Attackers spawning from the AttackerSpawner
    private bool IsAttackerInLane()
    {
        if (!myLaneSpawner)
        {
            SetLaneSpawner();
            return false;
        }
        else 
        {
            return myLaneSpawner.transform.childCount > 0;
        }
    }

    // Creates a projectile at the position of the gun
    public void Shoot()
    {
        Projectile newProjectile = Instantiate(projectile, gun.transform.position, gun.transform.rotation) as Projectile;
        if (GetComponent<DamageEnhancer>())
        {
            int newDamage = GetComponent<DamageEnhancer>().AttemptToDealMoreDamage(projectileDamage);
            newProjectile.SetDamage(newDamage);
        }
        else
        {
            newProjectile.SetDamage(projectileDamage);
        }

        audioController.PlayArrowShootSound();

        return;
    }
}
