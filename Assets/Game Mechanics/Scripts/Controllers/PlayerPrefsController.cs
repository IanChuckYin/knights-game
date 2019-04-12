using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour {

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore");
    }

    public void SetHighScore(int highScore)
    {
        PlayerPrefs.SetInt("highScore", highScore);
    }

    public void AttemptToUpdateHighScore(int score)
    {
        if (score > GetHighScore())
        {
            SetHighScore(score);
            Debug.Log("Setting high score to: " + score.ToString());
        }
    }
}
