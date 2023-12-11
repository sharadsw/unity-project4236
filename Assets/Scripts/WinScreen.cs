using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Restarting game");
        SceneManager.LoadScene("TestScene");
    }
    public void MenuButton()
    {
        Debug.Log("Back to main menu");
        SceneManager.LoadScene("MenuScene");
    }
}
