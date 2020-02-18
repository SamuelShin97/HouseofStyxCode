using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public Button pauseMenu;
    bool menuPress;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.interactable = false;
        pauseMenu.gameObject.SetActive(false);
        menuPress = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuPress)
            {
                pauseMenu.interactable = false;
                pauseMenu.gameObject.SetActive(false);
                menuPress = false;
            }
            else
            {
                pauseMenu.interactable = true;
                pauseMenu.gameObject.SetActive(true);
                menuPress = true;
            }
        }
    }

    public void ExitToButton()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(scene.buildIndex);
        SceneManager.LoadScene("MainMenu");
    }
}
