using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public BS_Main_Health playerHealth;
    public CharacterScriptv1 player;
    public GameObject gameOverMenuUI;
    CharacterScriptv1.playerState playerState;
    public bool GameOver = false;
    public float waitTime;

    // Update is called once per frame
    void Update()
    {
        if (playerHealth._health <= 0 && GameOver == false)
        {
            GameOver = true;
            gameOverMenuUI.SetActive(true);
            if (player.hasTriggeredBoss)
            {
                player.BossHealth.SetActive(false); // sets the boss's active health to inactive
            }

            StartCoroutine(GameOverScreen());
        }

    }

    public IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(waitTime);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerState = player.previousState;
        player.setCurrentState(CharacterScriptv1.playerState.pause);
        Time.timeScale = 0f;

    }

    // Ressurects player and continues the game
    public void Continue()
    {
        if (player.hasTriggeredBoss)
        {
            player.BossHealth.SetActive(true); // sets the boss's active health bar to inactive
        }

        playerHealth._health = playerHealth._maxHealth;
        playerHealth.healthBar.fillAmount = playerHealth._health / playerHealth._maxHealth;
        gameOverMenuUI.SetActive(false);
        Time.timeScale = 1f;
        player.setCurrentState(playerState);
        GameOver = false;

    }

    // function to load the main menu
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // quits the game
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    // restarts the level
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ReScaledEnvironment");
    }

}
