using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = true;
    }

    public void EnvironmentButton()
    {
        SceneManager.LoadScene("IntroCutScene");
    }

    public void FinalCutscene()
    {
        SceneManager.LoadScene("FinalCutScene");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
