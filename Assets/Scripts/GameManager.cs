using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // UI
    public TextMeshProUGUI healthText;
    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize UI on Start
        healthText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Trigger Game Over.
    public void GameOver() { 
       gameOverScreen.SetActive(true);
    }
    // Restart Game when Restart button is clicked.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
