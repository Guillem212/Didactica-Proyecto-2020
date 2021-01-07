using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject appBase;
    public void StartGame(string sceneName) //El nombre se le pasa desde el botón
    {
        appBase.SetActive(true);
        menu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
