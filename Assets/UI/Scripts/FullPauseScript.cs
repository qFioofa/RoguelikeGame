using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FullPauseScript : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public GameObject InfoCanvas;
    public GameObject DefaultCanvas;

    private bool isPaused = false;
    private bool inShop = false; 

    public TextMeshProUGUI seedText;

    void Start()
    {
        pauseMenuCanvas.SetActive(false);
        UpdateSeedText();
    }

    public void UpdateSeedText()
    {
        seedText.text = "Seed: " + FullSettingsScript.seed.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inShop)
            {
                // Если пользователь в магазине, вернуться в паузу
                GoToPauseMenu();
            }
            else if (isPaused)
            {
                // Если пользователь в паузе, выйти из неё
                ResumeGame();
            }
            else
            {
                // Если пользователь не в паузе, войти в паузу
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            EnterPause();
        }
        else
        {
            ResumeGame();
        }
    }

    public void EnterPause()
    {
        Time.timeScale = 0f;
        InfoCanvas.SetActive(false);
        DefaultCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
        DefaultCanvas.SetActive(true);
        InfoCanvas.SetActive(true);
        isPaused = false;
        inShop = false; 
    }

    public void GoToPauseMenu()
    {
        pauseMenuCanvas.SetActive(true);
        inShop = false; 
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Menu");
    }
}
