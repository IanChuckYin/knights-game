using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    AttackerSpawner[] attackerSpawners;
    [SerializeField] int waveCompleteGold;

    public void SummonAttackersFromAttackerSpawners()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<AttackerSpawner>().StartSpawning();
        }
    }

    public void StopSummoningAttackers()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<AttackerSpawner>().StopSpawning();
        }
    }

    public int GetWaveCompleteGold()
    {
        return waveCompleteGold;
    }
}
