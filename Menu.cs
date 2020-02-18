using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Text deathText;
    public Image deathImage;
    public bool menuPress;
    public Image restartImage;
    public Image quitImage;
    public Button restartButton;
    public Button quitButton;
    public Text restartText;
    public Text quitText;

    // Start is called before the first frame update
    void Start()
    {
        deathText.enabled = false;
        deathImage.enabled = false;
        menuPress = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //SceneManager.LoadScene("FinalCutScene");
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            menuPress = !menuPress;
            if (menuPress)
            {
                Cursor.lockState = CursorLockMode.None;
                restartImage.enabled = true;
                quitImage.enabled = true;
                restartButton.enabled = true;
                quitButton.enabled = true;
                restartText.enabled = true;
                quitText.enabled = true;

            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                restartImage.enabled = false;
                quitImage.enabled = false;
                restartButton.enabled = false;
                quitButton.enabled = false;
                restartText.enabled = false;
                quitText.enabled = false;

            }
        }
    }

    public void restart() //reload the scene
    {

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(scene.buildIndex);
        SceneManager.LoadScene("ReScaledEnvironment");
    }

    public void quit()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(scene.buildIndex);
        SceneManager.LoadScene("MainMenu");
    }

    private void deathScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        var temp = deathImage.color;
        temp.a = .7f;
        deathImage.color = temp;
        deathImage.enabled = true;
        deathText.enabled = true;
        restartImage.enabled = true;
        quitImage.enabled = true;
        restartButton.enabled = true;
        quitButton.enabled = true;
        restartText.enabled = true;
        quitText.enabled = true;
    }
}
