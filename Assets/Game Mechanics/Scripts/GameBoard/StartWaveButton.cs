using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWaveButton : MonoBehaviour {

    [SerializeField] Text buttonText;
    [SerializeField] WaveController waveController;

	// Use this for initialization
	void Start () {
        InitializeStartButton();
	}

    private void InitializeStartButton()
    {
        UpdateStartButtonText(waveController.GetCurrentWaveIndex());
    }

    public void ShowStartWaveButton()
    {
        gameObject.SetActive(true);
    }

    public void HideStartWaveButton()
    {
        gameObject.SetActive(false);
    }

    public void UpdateStartButtonText(int currentWaveIndex)
    {
        buttonText.text = "Start Wave: " + (currentWaveIndex + 1).ToString();
    }
}
