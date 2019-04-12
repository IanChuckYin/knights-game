using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    [Header("Attacker Spawner Details")]
    [SerializeField] float minSpawnDelay;
    [SerializeField] float maxSpawnDelay;
    [SerializeField] Attacker[] attackerPrefabArray;
    [SerializeField] Attacker boss;

    GameTimerController gameTimerController;
    AudioController audioController;

    private IEnumerator spawnRoutine;

    bool bossSpawned = false;

    // Use this for initialization
    private void Awake()
    {
        spawnRoutine = SpawnAttackersInIntervals();
        gameTimerController = FindObjectOfType<GameTimerController>();
        audioController = FindObjectOfType<AudioController>();
    }

    IEnumerator SpawnAttackersInIntervals()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));

            SpawnAttacker();

            if (!bossSpawned && boss)
            {
                CheckBossSpawnCondition();
            }
        }
    }


    // Creates an attacker at the position of the spawner, and instantiates it to this game object
    private void SpawnAttacker()
    {
        if (attackerPrefabArray.Length != 0)
        {
            var attackerIndex = UnityEngine.Random.Range(0, attackerPrefabArray.Length);

            Spawn(attackerPrefabArray[attackerIndex]);
        }
    }

    private void Spawn(Attacker myAttacker)
    {
        Attacker newAttacker = Instantiate(myAttacker, transform.position, transform.rotation) as Attacker;
        newAttacker.transform.parent = transform;
    }

    public void StopSpawning()
    {
        StopCoroutine(spawnRoutine);
    }

    public void StartSpawning()
    {
        StartCoroutine(spawnRoutine);
    }

    public int GetAttackerPrefabArrayLength()
    {
        return attackerPrefabArray.Length;
    }

    private void CheckBossSpawnCondition()
    {
        if ((gameTimerController.GetInitialGameTime() / 2) > gameTimerController.GetCurrentGameTime())
        {
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        Spawn(boss);
        bossSpawned = true;
        audioController.PlayBossEntered();
    }
}
