using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Starting game");
        SceneManager.LoadScene("TestScene");
    }
    public void QuitGame() 
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
