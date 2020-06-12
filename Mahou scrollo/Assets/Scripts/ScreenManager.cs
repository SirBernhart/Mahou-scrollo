using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScreenManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetPauseMenuActive()
    {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);

        if (pauseMenu.activeInHierarchy)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
