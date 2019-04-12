using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour {

    const string BUTTON_START_LEVEL = "BUTTON_START_LEVEL";

    GameTimerController gameTimerController;
    AudioController audioController;
    StartWaveButton startWaveButton;

    static bool waveStarted;

    [SerializeField] Wave[] waves;

    // REMOVE STATIC FIELD FOR TESTING DIFFERENT WAVES
    [SerializeField] static int currentWaveIndex = 0;

	// Use this for initialization
	void Awake () {
        FindObjects();
        ResetWaveIndex();
    }

    public void StartWave()
    {
        FindObjects();

        audioController.PlayStartLevelSound();
        gameTimerController.StartLevelTimer();
        startWaveButton.HideStartWaveButton();

        Wave wave = CreateWaveObject();
        SummonFromWave(wave);

        waveStarted = true;
    }

    public void StopWave()
    {
        gameTimerController.StopLevelTimer();

        StopWaveObjects();

        waveStarted = false;
    }

    private Wave CreateWaveObject()
    {
        return Instantiate(waves[currentWaveIndex], transform.position, transform.rotation);
    }

    private void SummonFromWave(Wave wave)
    {
        wave.SummonAttackersFromAttackerSpawners();
    }

    private void StopWaveObjects()
    {
        Wave[] currentWaves = FindObjectsOfType<Wave>();
        foreach(Wave wave in currentWaves)
        {
            wave.StopSummoningAttackers();
        }
    }

    public void DestroyAllWaves()
    {
        Wave[] currentWaves = FindObjectsOfType<Wave>();
        foreach (Wave wave in currentWaves)
        {
            Destroy(wave.gameObject);
        }
    }

    private void FindObjects()
    {
        gameTimerController = FindObjectOfType<GameTimerController>();
        startWaveButton = FindObjectOfType<StartWaveButton>();
        audioController = FindObjectOfType<AudioController>();
    }

    public void IncreaseWaveIndex()
    {
        currentWaveIndex++;
    }

    public void ResetWaveIndex()
    {
        currentWaveIndex = 0;
    }

    public int GetCurrentWaveIndex()
    {
        return currentWaveIndex;
    }

    public bool IsWaveRunning()
    {
        return waveStarted;
    }
}
