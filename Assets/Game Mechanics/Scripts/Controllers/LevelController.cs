using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    [Header("Panels")]
    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] GameObject levelFailedPanel;

    const string DIED_ON_TEXT = "DIED_ON_TEXT";
    const string HIGH_SCORE_TEXT = "HIGH_SCORE_TEXT";

    [Header("Controllers")]
    [SerializeField] GameTimerController gameTimerController;
    [SerializeField] PlayerController playerController;
    [SerializeField] WaveController waveController;
    [SerializeField] PlayerPrefsController playerPrefsController;
    [SerializeField] AudioController audioController;

    [Header("Game Board")]
    [SerializeField] DefenderGrid defenderGrid;
    [SerializeField] StartWaveButton startWaveButton;

    int numberOfAttackers;

    bool playerLoseStatus;

    // Use this for initialization
    void Start () {
        InitializeLevel();
	}

    private void InitializeLevel()
    {
        playerLoseStatus = false;

        DisableLevelCompletePanel();
        DisableLevelFailedPanel();
    }

    public void AttackerSpawned()
    {
        numberOfAttackers++;
    }

    public void AttackerKilled()
    {
        numberOfAttackers--;

        CheckWaveCompleteCondition();
    }

    public void CheckWaveCompleteCondition()
    {
        if (numberOfAttackers <= 0 && !gameTimerController.GetTimerStatus() && !playerLoseStatus)
        {
            HandleLevelComplete();
        }
    }

    public void HandleLevelComplete()
    {
        EnableLevelCompletePanel();

        playerController.AddPlayerGold(FindObjectOfType<Wave>().GetWaveCompleteGold());

        waveController.IncreaseWaveIndex();
        waveController.DestroyAllWaves();

        startWaveButton.ShowStartWaveButton();
        startWaveButton.UpdateStartButtonText(waveController.GetCurrentWaveIndex());

        gameTimerController.ResetGameTimerToInitialValue();

        RestoreAllDefendersToMaxHealth();

        audioController.PlayLevelComplete();
    }

    public void HandleLevelFailed()
    {
        audioController.PlayPlayerDefeated();

        playerLoseStatus = true;
        EnableLevelFailedPanel();
        Time.timeScale = 0;
        playerPrefsController.AttemptToUpdateHighScore(waveController.GetCurrentWaveIndex());
        defenderGrid.DestroyAllDefenders();
        UpdateCurrentWaveText();
        UpdateHighScoreText();
        waveController.ResetWaveIndex();
    }

    public void DisableLevelCompletePanel()
    {
        levelCompletePanel.SetActive(false);
    }

    public void EnableLevelCompletePanel()
    {
        levelCompletePanel.SetActive(true);
    }

    public void DisableLevelFailedPanel()
    {
        levelFailedPanel.SetActive(false);
    }

    public void EnableLevelFailedPanel()
    {
        levelFailedPanel.SetActive(true);
    }

    private void UpdateCurrentWaveText()
    {
        Text diedOnText = GameObject.FindWithTag(DIED_ON_TEXT).GetComponent<Text>();
        diedOnText.text = "Died on Wave: " + (waveController.GetCurrentWaveIndex() + 1).ToString();
    }

    private void UpdateHighScoreText()
    {
        Text highScoreText = GameObject.FindWithTag(HIGH_SCORE_TEXT).GetComponent<Text>();
        highScoreText.text = "Highest Wave Completed: " + playerPrefsController.GetHighScore().ToString();
    }

    private void RestoreAllDefendersToMaxHealth()
    {
        Defender[] defenders = FindObjectsOfType<Defender>();

        foreach(Defender defender in defenders)
        {
            defender.GetComponent<Health>().RestoreToMaxHealth();
        }
    }
}
