using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    const string PLAYER_LIVES_TEXT = "PLAYER_LIVES_TEXT";
    const string PLAYER_GOLD_TEXT = "PLAYER_GOLD_TEXT";

    [Header("Player")]
    [SerializeField] int playerLives;
    [SerializeField] int playerGold;

    [Header("Controllers")]
    [SerializeField] LevelController levelController;
    [SerializeField] AudioController audioController;

    Text playerLivesText;
    Text playerGoldText;

	// Use this for initialization
	void Start () {
        playerLivesText = GameObject.FindWithTag(PLAYER_LIVES_TEXT).GetComponent<Text>();
        playerGoldText = GameObject.FindWithTag(PLAYER_GOLD_TEXT).GetComponent<Text>();

        UpdatePlayerLivesText();
        UpdatePlayerGoldText();
	}

    // Adds lives to the player's total amount
    public void AddPlayerLives(int lives)
    {
        playerLives += lives;

        UpdatePlayerLivesText();
    }

    // Removes lives from the player, if it reaches 0 then end game
    public void RemovePlayerLives(int lives)
    {
        playerLives -= lives;

        audioController.PlayLostLifeSound();

        UpdatePlayerLivesText();

        if (playerLives <= 0)
        {
            levelController.HandleLevelFailed();
        }
    }

    // Updates the player's lives on the Text object
    private void UpdatePlayerLivesText()
    {
        playerLivesText.text = playerLives.ToString();
    }

    // Adds gold to the player's total amount
    public void AddPlayerGold(int gold)
    {
        playerGold += gold;

        UpdatePlayerGoldText();
    }

    // Removes gold from the player
    public void RemovePlayerGold(int gold)
    {
        playerGold -= gold;

        UpdatePlayerGoldText();
    }

    public bool HaveEnoughGold(int price)
    {
        return playerGold >= price;
    }

    // Updates the player's gold on the Text object
    private void UpdatePlayerGoldText()
    {
        playerGoldText.text = playerGold.ToString();
    }


    //  GETTERS AND SETTERS
    public void SetPlayerLives(int lives)
    {
        playerLives = lives;
    }

    public int GetPlayerLives()
    {
        return playerLives;
    }

    public void SetPlayerGold(int gold)
    {
        playerGold = gold;
    }

    public int GetPlayerGold()
    {
        return playerGold;
    }
}
