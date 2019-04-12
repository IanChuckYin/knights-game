using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerController : MonoBehaviour {

    const string GAME_TIMER_TEXT = "GAME_TIMER_TEXT";

    [SerializeField] float gameTime;
    [SerializeField] float timeScale = 1;

    Text gameTimeText;

    private bool timerOn;
    float initialGameTime;

    [SerializeField] WaveController waveController;
    [SerializeField] LevelController levelController;

	// Use this for initialization
	void Start () {
        InitializeGameTimer();
        Time.timeScale = timeScale;
	}
	
	// Update is called once per frame
	void Update () {
        if (timerOn)
        {
            CountdownGameTimer();
        }
	}

    // Sets the initial game time, initializes the Text that will display the timer
    private void InitializeGameTimer()
    {
        initialGameTime = gameTime;
        gameTimeText = GameObject.FindWithTag(GAME_TIMER_TEXT).GetComponent<Text>();

        UpdateGameTimeDisplay();
        StopLevelTimer();
    }

    // Countsdown the game timer
    private void CountdownGameTimer()
    {
        gameTime -= Time.deltaTime;

        UpdateGameTimeDisplay();

        if (gameTime < 0)
        {
            NotifyGameTimerHasExpired();
        }
    }

    // Tells the game that the timer has expired
    private void NotifyGameTimerHasExpired()
    {
        waveController.StopWave();
        levelController.CheckWaveCompleteCondition();
    }

    // Updates the text to the current timer
    private void UpdateGameTimeDisplay()
    {
        gameTimeText.text = (Mathf.Round(gameTime)).ToString();
    }

    // Starts the level timer
    public void StartLevelTimer()
    {
        timerOn = true;
    }

    // Stops the level timer
    public void StopLevelTimer()
    {
        timerOn = false;
    }

    // Returns if the timer has stopped or not
    public bool GetTimerStatus()
    {
        return timerOn;
    }

    public float GetCurrentGameTime()
    {
        return gameTime;
    }

    public float GetInitialGameTime()
    {
        return initialGameTime;
    }

    // Resets the current timer to its initial time
    public void ResetGameTimerToInitialValue()
    {
        gameTime = initialGameTime;
    }
}
