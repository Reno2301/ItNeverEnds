using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneActions : MonoBehaviour
{
    private string gameScene = "Game";
    private string mainMenuScene = "MainMenu";
    private string highScoreScene = "HighScore";

    public void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void HighScore()
    {
        SceneManager.LoadScene(highScoreScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
