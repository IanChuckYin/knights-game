using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    static AudioController audioController;

    const int MAIN_MENU_INDEX = 0;
    const int CREDITS_INDEX = 1;
    const int INSRUCTIONS_INDEX = 2;
    const int MAIN_GAME_INDEX = 3;

    // Loads the main menu
    public void LoadMainMenu()
    {
        audioController = FindObjectOfType<AudioController>();
        audioController.PlayMainMenuMusic();

        SceneManager.LoadScene(MAIN_MENU_INDEX);
    }

    // Loads the edit board
    public void LoadCredits()
    {
        SceneManager.LoadScene(CREDITS_INDEX);
    }

    // Loads the instructions
    public void LoadInstructions()
    {
        SceneManager.LoadScene(INSRUCTIONS_INDEX);
    }

    // Loads the main game
    public void LoadMainGame()
    {
        audioController = FindObjectOfType<AudioController>();
        audioController.PlayInGameBackgroundMusic();

        ButtonClicked();
        SceneManager.LoadScene(MAIN_GAME_INDEX);
    }

    // Quits the application
    public void QuitGame()
    {
        ButtonClicked();
        Application.Quit();
    }

    private void ButtonClicked()
    {
        audioController = FindObjectOfType<AudioController>();
        audioController.PlayButtonClicked();
    }
}
