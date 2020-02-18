using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    private GameObject playerHealthUI;
    public AudioMixer audioMixer;

    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;
    public GameObject GOMenuUI;
    public CharacterScriptv1 player;
    CharacterScriptv1.playerState playerState;


    private void Awake()
    {
        playerHealthUI = transform.GetChild(0).gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel") && GOMenuUI.activeInHierarchy == false)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }

        }
    }

    // Resumes the game
    public void Resume()
    {
        playerHealthUI.SetActive(true);  // Brings back player Health UI

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (player.hasTriggeredBoss)
        {
            player.BossHealth.SetActive(true); // sets the boss's inactive health bar back to active
        }

        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        player.setCurrentState(playerState);

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
    
    // pauses the game
    void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        playerHealthUI.SetActive(false); // Player Health UI disappears

        if (player.hasTriggeredBoss)
        {
            player.BossHealth.SetActive(false); // sets the boss's active health bar to inactive
        }

        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        playerState = player.previousState;
        player.setCurrentState(CharacterScriptv1.playerState.pause);
    }

    //sets the Master volume
    public void SetMaster(float volume)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    //sets the Sound Effects
    public void SetSoundEffects(float volume)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    //sets the Dialogue
    public void SetDialogue(float volume)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        audioMixer.SetFloat("Dialogue", Mathf.Log10(volume) * 20);
    }

    //sets the Music
    public void SetMusic(float volume)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }
}
