using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject instructions;
    public void PlayGame()
    {
        Debug.Log("Starting game");
        SceneManager.LoadScene("TestScene");
    }
    public void PlayBoss()
    {
        Debug.Log("Starting at Boss");
        SceneManager.LoadScene("BossScene");
    }
    public void QuitGame() 
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
    // Swap to Instructions Screen.
    public void Instructions() { 
        mainMenu.SetActive(false);
        instructions.SetActive(true);
    }
    // Return to main menu.
    public void BackButton() {
        mainMenu.SetActive(true);
        instructions.SetActive(false);
    }
}
