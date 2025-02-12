using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(GameParametres.SceneName.SCENE_MENU);
    }

    public void GoToGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(GameParametres.SceneName.SCENE_GAME);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
